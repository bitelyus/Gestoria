using System;
using Gestoria.Model;
using Gestoria.View;

namespace Gestoria.Controler
{


    static class ControlerNomina
    {
        

        // MÉTODO PARA MOSTRAR EL MENU DE NOMINAS. RECIBE LA GESTORIA POR REFERENCIA
        // PARA PODER APLICARLE LOS CAMIOS QUE SE REALIZEN


        public static void menu_nominas(ref Gestora migestoria)
        {
            
            if (migestoria.empresas != null)
            {
                // 1. PEDIR EMPRESA
                // 2. PEDIR TRABAJADOR
                // 3. MOSTRAR MENU NOMINAS

                bool salir = false;         // FLAG PARA SALIR DEL PROGRAMA
                int opcion = 0;             // OPCIÓN ELEGIDA DEL MENU
                byte empresaActiva = 0;     // EMPRESA DE TRABAJO SELECCIONADA
                byte trabajadorActivo = 0;  // TRABAJADOR ACTIVO SELECCIONADO
               

                do
                {
                    if (empresaActiva == 0) {
                        empresaActiva = InterfazTrabajador.pedirEmpresa(migestoria);
                    }
                    if (migestoria.empresas[empresaActiva - 1].plantilla != null)
                    {
                        //InterfazTrabajador.menuTrabajador(empresaActiva, migestoria);
                        if (trabajadorActivo == 0) {
                            trabajadorActivo = InterfazTrabajador.seleccionarTrabajador(migestoria,empresaActiva);
                        }
                        InterfazNomina.menuNomina(empresaActiva,trabajadorActivo,migestoria);
                        opcion = ConsoleHelper.leerOpcion(5);
                        Trabajador trabajador = migestoria.empresas[empresaActiva - 1].plantilla[trabajadorActivo - 1];
                        switch (opcion)
                        {
                            case 1:
                                ControlerNomina.pedirNomina(ref trabajador);
                                break;
                            case 2:
                                ControlerNomina.consultarDatosNomina(trabajador);
                                ConsoleHelper.pausa();
                                break;
                            case 3:
                                ControlerNomina.modificarNomina(ref trabajador);
                                ConsoleHelper.pausa();
                                break;
                            case 4:
                                ControlerNomina.eliminarNomina(ref trabajador);
                                break;
                            case 5:
                                ControlerNomina.listadoNominas(migestoria.empresas[empresaActiva - 1].plantilla[trabajadorActivo - 1]);
                                ConsoleHelper.pausa();
                                break;
                            case 0:
                                //Console.WriteLine("\nBYE BYE!! .. MiK.. VUELVE PRONTO :)\n");
                                salir = true;
                                break;
                        }
                    }
                    else
                    {
                        CH.lcdColor("\n!> ¡¡NO HAY NINGUN TRABAJADOR EN LA EMPRESA ACTIVA!!...\n!> CONTRATA ALGUIEN, RATA",ConsoleColor.Red);
                        CH.pausa();
                        salir = true;
                    }

                } while (!salir);
            }
            else
            {
                CH.lcdColor("\n!> NO HAY NINGUNA 'EMPRESA' EN LA GESTORÍA...\ni> CRÉA ALGUNA Y CONTRATA!!, BONIKO", ConsoleColor.Red);
                CH.pausa();
            }
        }


        // MÉTODO PARA CONSULTAR LOS DATOS DE UNA NOMINA DEL TRABAJADOR

        public static void consultarDatosNomina(Trabajador trabajador) {
            byte opcion = 0;
            InterfazNomina.listarNominas(trabajador);
            if (trabajador.nominas != null)
            {
                opcion = ConsoleHelper.leerOpcionNomina((byte)trabajador.nominas.Length, "VER");
                InterfazNomina.muestraDatosNomina(trabajador, opcion);
            }
        }

        // METODO QUE LISTA TODAS LAS NÓMINAS DE UN TRABAJADOR

        public static void listadoNominas(Trabajador trabajador) {
            if (trabajador.nominas != null)
            {
                Console.WriteLine("\nLISTADO DE NÓMINAS D.N.I.: " + trabajador.dni + "\n====================================\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7} ","MES".PadRight(10), "HORAS".PadLeft(10),"H.EXTRA".PadLeft(12), "S. BASE".PadLeft(11), " S. EXTRA".PadLeft(10), "S. BRUTO".PadLeft(10), "IMPUESTOS".PadLeft(10), "S. NETO".PadLeft(10));
                Console.ForegroundColor = ConsoleColor.White;

                foreach (Nomina nomina in trabajador.nominas) {
                    Console.WriteLine("{0}{1}{2} {3:F2}{4:F2}{5:F2}{6:F2}{7:F2}",nomina.mes.PadRight(15),nomina.horas.ToString().PadRight(10),nomina.horasExtras.ToString().PadRight(10), nomina.salarioBase.ToString().PadRight(10), nomina.salarioExtra.ToString().PadRight(10), nomina.salarioBruto.ToString().PadRight(10), nomina.impuestos.ToString().PadRight(10), nomina.salarioNeto.ToString().PadRight(10));
                }
            } else {
                Console.WriteLine("!> No hay nóminas que listar!");
            }
        }


        public static void eliminarNomina(ref Trabajador trabajador) {

            // 1. LISTAR NOMINAS CON UN INDICE
            // 2. SELECCIONAR UN INDICE
            // 3. BORRAR -> REDIMENSIONAR ARRAY. 
            //              SE HACE EN LA CLASE LLAMANDO AL METTOD ELIMINAR NOMINA
            byte opcion = 0;
            InterfazNomina.listarNominas(trabajador);
            if (trabajador.nominas != null)
            {
                Console.WriteLine("\n0. CANCELAR");
                opcion = InterfazNomina.leerOpcionNominaOp((byte)trabajador.nominas.Length,"BORRAR");
                if (opcion > 0) {
                    if (trabajador.EliminarNomina(trabajador.nominas[opcion - 1]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("i> Se ha borrado la nómina");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else {
                        Console.WriteLine("!> ERROR borrando nómina");
                    }
                }
            }
            ConsoleHelper.pausa();
        }

        public static void modificarNomina(ref Trabajador trabajador)
        {

            // 1. LISTAR NOMINAS CON UN INDICE
            // 2. SELECCIONAR UN INDICE
            // 3. MODIFICAR -> CAMBIAR UNA NOMINA CREADA POR OTRA 
           
            byte opcion = 0;
            InterfazNomina.listarNominas(trabajador);
            if (trabajador.nominas != null)
            {
                Console.WriteLine("\n0. CANCELAR");
                opcion = InterfazNomina.leerOpcionNominaOp((byte)trabajador.nominas.Length,"MODIFICAR");
                if (opcion > 0)
                {
                    InterfazNomina.pedirNuevaNomina(ref trabajador, trabajador.nominas[opcion - 1]);
                }
            }

           // ConsoleHelper.pausa();


        }

        // METODO QUE RECIBE EL TRABAJADOR POR REFERENCIA PARA AÑADIRLE UNA NÓMINA

        public static void pedirNomina(ref Trabajador trabajador)
        {
                Nomina nomina;
                DatosBase datos;
                bool salir;
                
                salir = false;
                datos = ControlerAdministracion.cargarDatos();
                nomina = new Nomina();

                do
                {
                    try
                    {
                        nomina.nombre = trabajador.nombre;
                        nomina.apellidos = trabajador.apellidos;
                        InterfazNomina.pedirDatosNomina(ref nomina);
                        nomina.eurosHoras = datos.preciojoranda;                        // ESTA CONSTANTE ESTA EN CONTROLER GESTORIA, PERO ¿COMO SE LLEGA DE AQUI ALLI?
                        nomina.calcularBruto(datos.horasbase, datos.incrementoextra);   // HAY QUE PASARLE EL MAX. DE HORAS NORMALES (EL RESTO SON EXTRAS) Y EL FACTOR DE INCREMENTO
                        nomina.calcularImpuestos(datos.impuestos);                // EL PORCENTAJE DE LA TASA DE IMPUESTOS
                        if (trabajador.AgregarNomina(nomina))
                        {
                            CH.lcdColor("\ni> NÓMINA REGISTRADA CORRECTAMENTE!!\n",ConsoleColor.Green);
                            Console.WriteLine(nomina.ToString());
                            ConsoleHelper.pausa();
                            salir = true;
                        }
                        else {
                            CH.lcdColor("!> ERROR REGISTRANDO LA NÓMINA!!",ConsoleColor.Red);
                            CH.pausa();
                        };
                    }
                    catch (Exception ex)
                    {
                        CH.lcdColor("\n!> CLASS ERR: "+ex.Message.ToUpper(),ConsoleColor.Red);
                        CH.pausa();
                    salir = true;
                    }
                } while (!salir);
            
        
        }

    }
}
