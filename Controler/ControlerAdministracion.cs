using System;
using System.IO;
using Gestoria.Model;
using Gestoria.View;

namespace Gestoria.Controler
{
    static class ControlerAdministracion
    {

        private static DatosBase datosBase;
        private static DatosBase datosBaseBin;
        private static string pass;
        private static string ruta = "Data/config.txt";
        private static string rutabin = "Data/config.data";


        public static void menu_adminstracion()
        {
            bool salir;
            int opcion;

            opcion = 0;
            salir = false;

            if (ControlerAdministracion.logIn())
            {
                do
                {
                    InterfazAdministracion.mostrar_menu();
                    opcion = ConsoleHelper.leerOpcion(3);
                    switch (opcion)
                    {
                        case 1:
                            ControlerAdministracion.modificarPass(pass);
                            break;
                        case 2:
                            ControlerAdministracion.modificarValores();
                            break;
                        case 3:
                            InterfazAdministracion.listarDatos(datosBase.maxhoras, datosBase.horasbase, datosBase.maxeuxhora, datosBase.preciojoranda, datosBase.incrementoextra, datosBase.impuestos);
                            InterfazAdministracion.listarDatosBin(datosBaseBin.maxhoras, datosBaseBin.horasbase, datosBaseBin.maxeuxhora, datosBaseBin.preciojoranda, datosBaseBin.incrementoextra, datosBaseBin.impuestos);
                            CH.pausa();
                            break;
                        case 0:
                            salir = true;
                            break;
                    }
                } while (!salir);
            }
            else
            {
                CH.lcdColor("!> CONTRASEÑA INCORRECTA", ConsoleColor.Red);
                CH.pausa();
            }
        }

        ///<summary>
        ///Método para logearse al sistema de administración de valores de configuración de la nómina por defecto
        ///</summary>
        public static bool logIn()
        {

            bool logeado = false;
            string cadena = "";
            string aux = "";
            string[] pass_split = null;
            StreamReader sr;

            cadena = CH.leerCadena("INTRODUCE CONTRASEÑA DE ACCESO");

            if (File.Exists(ruta))
            {
                CH.lcdColor("i> ACCEDIENDO AL FICHERO DE CONFIGURACION...", ConsoleColor.Green);
                try
                {
                    //File.OpenText(ruta);
                    sr = File.OpenText(ruta);
                    aux = sr.ReadLine();
                    pass_split = aux.Split(':');
                    pass = pass_split[1];
                    //CH.lcd("PASS:" + pass);
                    if (cadena.Equals(pass))
                    {
                        logeado = true;
                        datosBase = cargarDatos();          // CARGAMOS LOS DATOS EN TEXTO
                        datosBaseBin = cargarDatosBinary(); // CARGAMOS LOS DATOS EN BINARIO
                        CH.lcdColor("i> LOGIN O.K.", ConsoleColor.Yellow);
                        CH.pausa();
                    }
                    else
                    {
                        CH.lcdColor("i> LOGIN K.O..", ConsoleColor.Red);
                    }
                }
                catch (Exception ex)
                {
                    CH.lcdColor(ex.Message, ConsoleColor.Red);
                }
            }
            else
            {
                CH.lcdColor("i> ERROR. NO SE ENCUENTRA EL FICHERO DE CONFIGURACION", ConsoleColor.Red);
            }
            return logeado;
        }


        /// <summary>
        /// MÉTODO APRA CARGAR LOS DATOS DE CONFIGURAICÓN A UN OBJETO Y DEVOLVERLO
        /// </summary>
        public static DatosBase cargarDatos()
        {
            //string pass;
            int maxhoras, horasbase;
            float maxeurxhora, preciojornada, incrementoextra, impuestos;

            string linea = null;
            string[] tokens = null;
            DatosBase datos = null; ;
            StreamReader sr = null; ;

            try
            {
                datos = new DatosBase();
                sr = File.OpenText(ruta);
                linea = sr.ReadLine();
                /* NO GUARDAMOS LA PASSS EN EL OBJETO!!
                tokens = linea.Split(':');
                pass = tokens[1];
                */
                linea = sr.ReadLine();
                tokens = linea.Split(':');
                Int32.TryParse(tokens[1], out maxhoras);
                datos.maxhoras = maxhoras;

                linea = sr.ReadLine();
                tokens = linea.Split(':');
                Int32.TryParse(tokens[1], out horasbase);
                datos.horasbase = horasbase;

                linea = sr.ReadLine();
                tokens = linea.Split(':');
                Single.TryParse(tokens[1], out maxeurxhora);
                datos.maxeuxhora = maxeurxhora;

                linea = sr.ReadLine();
                tokens = linea.Split(':');
                Single.TryParse(tokens[1], out preciojornada);
                datos.preciojoranda = preciojornada;

                linea = sr.ReadLine();
                tokens = linea.Split(':');
                Single.TryParse(tokens[1], out incrementoextra);
                datos.incrementoextra = incrementoextra;

                linea = sr.ReadLine();
                tokens = linea.Split(':');
                Single.TryParse(tokens[1], out impuestos);
                datos.impuestos = impuestos;

            }
            catch (Exception ex)
            {
                CH.lcdColor("ERROR: " + ex.Message, ConsoleColor.Red);
                CH.pausa();
            }
            finally
            {
                sr.Dispose();
            }

            //Console.WriteLine("{0} | {1} | {2} | {3} | {4}",pass,maxhoras,maxeurxhora,preciojornada,factorextra);
            //CH.pausa();
            return datos;
        }

        public static DatosBase cargarDatosBinary()
        {
            string pass;
            int maxhoras, horasbase;
            float maxeurxhora, preciojornada, incrementoextra, impuestos;

            DatosBase datos = null;
            BinaryReader br = null;
            FileStream fr = null;

            try
            {
                datos = new DatosBase();
                fr = File.Open(rutabin, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fr);

                pass = br.ReadString().Trim();
                maxhoras = br.ReadInt32();
                horasbase = br.ReadInt32();
                maxeurxhora = br.ReadSingle();
                preciojornada = br.ReadSingle();
                incrementoextra = br.ReadSingle();
                impuestos = br.ReadSingle();

                datos.maxhoras = maxhoras;
                datos.horasbase = horasbase;
                datos.maxeuxhora = maxeurxhora;
                datos.preciojoranda = preciojornada;
                datos.incrementoextra = incrementoextra;
                datos.impuestos = impuestos;

            }
            catch (Exception ex)
            {
                CH.lcdColor("!> ERROR: " + ex.Message, ConsoleColor.Red);
                CH.pausa();
            }
            finally
            {
                br.Dispose();
            }

            //Console.WriteLine("{0} | {1} | {2} | {3} | {4}",pass,maxhoras,maxeurxhora,preciojornada,factorextra);
            //CH.pausa();
            return datos;
        }


        ///<summary>
        ///METODO PARA MODIFICAR LOS VALORES DE CONFIGURAICÓN DEL LA NÓMINA
        ///USA FILE Y SCREEN, POR LO QUE LO DEJAMOS EN EL CONTROLADOR   !!!
        ///</summary>
        public static void modificarPass(string pass)
        {
            // 1. Modificar pidiendo el nuevo dato

            try
            {
                pass = CH.leerPass("NUEVA CONTRASEÑA");
            }
            catch (Exception ex)
            {
                CH.lcdColor("\n" + ex.Message + "\n", ConsoleColor.Red);
            }

            FH.grabarValores(ruta, pass, datosBase.maxhoras, datosBase.horasbase, datosBase.maxeuxhora, datosBase.preciojoranda, datosBase.incrementoextra, datosBase.impuestos);
        }

        /// <summary>
        /// Procedimiento para modificar los valores de configuración por defecto de las nóminas. 
        /// Usa tanto file.io como consosole.writre por lo que lo dejaremos de momento EN EL CONTROLADOR!!!
        /// </summary>
        public static void modificarValores()
        {
            // 1. Listar los valores
            // 2. Elegir uno
            // 3. Modificar pidiendo el nuevo dato

            int opcion;
            bool salir;
            byte formato;

            salir = false;

            // LOS MODIFICAMOS EN EL OBJETO DIRECTAMENTE

            do
            {
                try
                {
                    InterfazAdministracion.listarDatos(datosBase.maxhoras, datosBase.horasbase, datosBase.maxeuxhora, datosBase.preciojoranda, datosBase.incrementoextra, datosBase.impuestos);
                    opcion = CH.leerOpcionMsg(6, "Introduce el número del valor a modificar");

                    switch (opcion)
                    {
                        case 1:
                            datosBase.maxhoras = CH.leerNumero("NUEVO MAXIMO HORAS");
                            salir = true;
                            break;
                        case 2:
                            datosBase.horasbase = CH.leerNumero("NUEVO HORAS SALARIO BASE");
                            salir = true;
                            break;
                        case 3:
                            datosBase.maxeuxhora = CH.leerFloat("EUROS MAXIMOS POR HORA");
                            salir = true;
                            break;
                        case 4:
                            datosBase.preciojoranda = CH.leerFloat("PRECIO JORNADA");
                            salir = true;
                            break;
                        case 5:
                            datosBase.incrementoextra = CH.leerFloat("FACTOR EXTRA");
                            salir = true;
                            break;
                        case 6:
                            datosBase.impuestos = CH.leerFloat("PORCENTAJE DE IMPUESTOS");
                            salir = true;
                            break;
                    }

                    // LOS VOLCAMOS DEL OBJETO AL FICHERO DE DATOS DE CONFIGURACION. 
                    // PEDIMOS EL FORMATO Y USAMOS UNO U OTRO. USA FILEHELPER.CLASS > FH!!
                    formato = FH.pedirFormato();
                    switch (formato)
                    {
                        case (1):   // FORMATO TEXTO
                            FH.grabarValores(ruta, pass, datosBase.maxhoras, datosBase.horasbase, datosBase.maxeuxhora, datosBase.preciojoranda, datosBase.incrementoextra, datosBase.impuestos);
                            break;
                        case (2):   // FORMATO BINARIO
                            FH.grabarValoresBinary(rutabin, pass, datosBase.maxhoras, datosBase.horasbase, datosBase.maxeuxhora, datosBase.preciojoranda, datosBase.incrementoextra, datosBase.impuestos);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    CH.lcdColor("!> ERROR: " + ex.Message, ConsoleColor.Red);
                }
            } while (!salir);
        }

        /// <summary>
        /// Procedimiento para comprobar la existencia del archivo de configuracion config.data
        /// Si no existe, se crea si o si ... >:)
        /// </summary>
        public static void configCheck()
        {
            // 1. COMPROBAR EXISTENCIA... 
            // 2. SI NO ESTA.. CREARLO:
            // 3. PEDIR LOS DATOS Y GUARDARLOS A OBJETO DatosBase
            // 4. CON LOS DATOS EN EL OBJETO, LO PASAMOS POR EL FILEHELPER PARA GUARDARLO EN BINARIO
            // 5. SALIMOS... 

            string pass;    // PARA GUARDAR LA CONTRASEÑA
            bool salir;     // CONTROL DE SALIDA SI CREACION CORRECTA

            try
            {
                datosBaseBin = new DatosBase(); // REUSAMOS EL OBJETO DEL CONTROLADOR
                salir = false;                  // SETEAMOS SALIR A FALSE
                
                if (!File.Exists(rutabin))      
                {
                    CH.cls();
                    CH.lcdColor("!> ATENCIÓN!!.. NO EXISTE EL FICHERO DE CONFIGURACION!!", ConsoleColor.Red);
                    CH.lcdColor("\ni> VAMOS A CREARLO AHORA... ALMA DEL AMOL HERRMOZO >:) \ni> Y NO TE EQUIVOQUES.... PUES EMPEZARAS DEL PRINCIPIO", ConsoleColor.DarkYellow);
                    do
                    {
                        try
                        {
                            CH.lcd("");
                            pass = CH.leerCadena("CONTRASEÑA DE ACCESO");
                            datosBaseBin.maxhoras = CH.leerNumero("HORAS MÁXIMAS NOMINA");
                            datosBaseBin.horasbase = CH.leerNumero("HORAS TOPE SAL. BASE");
                            datosBaseBin.maxeuxhora = CH.leerCantidad("EUROS MAX. X HORA...");
                            datosBaseBin.preciojoranda = CH.leerCantidad("PRECIO EUR. JORANDA.");
                            datosBaseBin.incrementoextra = CH.leerCantidad("IMCREMENTO H. EXTRA.");
                            datosBaseBin.impuestos = CH.leerCantidad("% DE IMPUESTOS......");
                            FH.grabarValoresBinary(rutabin, pass, datosBaseBin.maxhoras, datosBaseBin.horasbase, datosBaseBin.maxeuxhora, datosBaseBin.preciojoranda, datosBaseBin.incrementoextra, datosBaseBin.impuestos);
                            salir = true;
                        }
                        catch (Exception ex)
                        {
                            CH.lcdColor("!> ERROR: " + ex.Message, ConsoleColor.Red);
                            CH.pausa();
                        }

                    } while (!salir);

                } else {
                    CH.cls();
                    CH.lcdColor("!> FICHERO DE CONFIGURACION LOCALIZADO...", ConsoleColor.Green);
                    CH.pausa();
                }
            }
            catch (IOException ex)
            {
                CH.lcdColor("!> ERROR I/O: " + ex.Message, ConsoleColor.Red);
                CH.pausa();
            }
            //CH.pausa();

        }

    }
}