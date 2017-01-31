#region License
// (C) - 2017 : Miguel Camacho Sánchez @ www.miguelkiko.com
// GESTION DE NÓMINAS - 2º DAM - DESARROLLO INTERFACES
#endregion

using System;
using Gestoria.Model;
using Gestoria.Controler;

namespace Gestoria.View
{
    static class InterfazNomina
    {
        /// <summary>
        /// Procedimiento para mostar el menu principal para la gestión de nóminas, previa seleccion
        /// de empresa y trabajador
        /// <paramref name="empresa">La empresa activa con los trabajadores</paramref>
        /// <paramref name="trabajador">El trabajador Activo...</paramref>
        /// <paramref name="migestoria">La getoria entera ... ?!?!</paramref>
        /// </summary>
        public static void menuNomina(byte empresa, byte trabajador, Gestora migestoria)
        {
            string salida;
            CH.cls();
            salida = "\n";
            salida += "+---------------------+\n";
            salida += "|   MENU DE NÓMINAS   |\n";
            salida += "+---------------------+\n\n";
            salida += "*> EMPRESA.: " + empresa + ". " + migestoria.empresas[empresa - 1].nombre + " <*\n";
            salida += "*> EMPLEADO: " + trabajador + ". " + migestoria.empresas[empresa - 1].plantilla[trabajador - 1].nombre
                   + " " + migestoria.empresas[empresa - 1].plantilla[trabajador - 1].apellidos + " <*\n";
            CH.lcdColor(salida,ConsoleColor.Cyan);
            salida = "1. AÑADIR UNA NOMINA\n";
            salida += "2. CONSULTAR NOMINA\n";
            salida += "3. MODIFICAR NOMINA\n";
            salida += "4. ELIMINAR NOMINA\n\n";
            salida += "5. LISTAR NOMINAS\n\n";
            salida += "0. << GORVER <<\n";
            CH.lcd(salida);
        }

        /// <summary>
        /// METODO PARA CAMBIAR UNA NÓMINA POR OTRA
        /// RECIBE EL TRABAJADOR POR REFERENCIA Y LA NOMINA A CAMBIAR. 
        /// CREA UNA NUEVA Y LA CAMBIA
        /// <paramref name="nomina">La nomina a sustituir</paramref>
        /// <paramref name="trabajador">El trabajador de la nómina en cuestion</paramref>
        /// </summary>
        public static void pedirNuevaNomina(ref Trabajador trabajador, Nomina nomina)
        {
            Nomina nueva_nomina = new Nomina();
            bool salir = false;
            DatosBase datos;
            datos = ControlerAdministracion.cargarDatos(); 
            do
            {
                try
                {
                    nueva_nomina.nombre = trabajador.nombre;
                    nueva_nomina.apellidos = trabajador.apellidos;
                    Console.WriteLine("i> OK, AMO... VAMOS A CREAR UNA NUEVA NÓMINA PARA SUSTITUIRLA");
                    pedirDatosNomina(ref nueva_nomina);
                    nueva_nomina.eurosHoras = datos.preciojoranda;           // ESTA CONSTANTE ESTA EN CONTROLER GESTORIA, PERO ¿COMO SE LLEGA DE AQUI ALLI?
                    nueva_nomina.calcularBruto(datos.horasbase, datos.incrementoextra); // HAY QUE PASARLE EL MAX. DE HORAS NORMALES (EL RESTO SON EXTRAS) Y EL FACTOR DE INCREMENTO
                    nueva_nomina.calcularImpuestos(datos.impuestos); // EL PORCENTAJE DE LA TASA DE IMPUESTOS
                    Console.WriteLine("\n" + nueva_nomina.ToString());

                    if (trabajador.modificarNomina(nomina,nueva_nomina))
                    {
                        CH.lcdColor("i> NÓMINA MODIFICADA CORRECTAMENTE",ConsoleColor.Green);
                        salir = true;
                        //ConsoleHelper.pausa();
                    }
                    else {
                        CH.lcdColor("\n!> ERROR MODIFICANDO LA NÓMINA",ConsoleColor.Red);
                    };
                }
                catch (Exception ex)
                {
                    CH.lcdColor("!> ERR: " + ex.Message.ToUpper(),ConsoleColor.Red);
                }
            } while (!salir);
        }

        /// <summary>
        /// Procedimiento para listar las nómias con indice del trabajdor.
        /// <paramref name="trabajador">El trabajjador para mostar los datos</paramref>
        /// </summary>
        public static void listarNominas(Trabajador trabajador) {
            if (trabajador.nominas != null)
            {
                Console.WriteLine("\nLISTADO DE NÓMINAS");
                Console.WriteLine("==================");
                for (int i = 0; i < trabajador.nominas.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + trabajador.nominas[i].mes);
                }
            }
            else {
                Console.WriteLine(">> No tiene nóminas registradas!!");
            }

        }

        /// <summary>
        /// Procedimiento para mostar los datos de las nóminas del trabajdor por indice
        /// <paramref name="trabajador">El tabajador en cuestion</paramref>
        /// <paramref name="indice">El indice de la nómina a mostrar</paramref>
        /// </summary>
        public static void muestraDatosNomina(Trabajador trabajador, byte indice)
        {
            Console.WriteLine("\nDATOS DE LA NÓMINA: ");
            Console.WriteLine("=================== ");
            Console.WriteLine(trabajador.nominas[indice - 1].ToString());
        }

        // MÉTODO QUE PIDE LOS DATOS DE UNA NÓMINA: MES Y HORAS TRABAJADAS

        public static void pedirDatosNomina(ref Nomina nomina)
        {

            try
            {
                nomina.mes = leerMes();
                nomina.horas = leerHoras();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // METODOS AUXILIARES PARA PEDIR DATOS CONCRETOS

        public static string leerMes()
        {
            bool salir = false;
            string aux = null;
            int opcion = 0;
            do
            {
                Console.Write("\n>> MES DE LA NÓMINA: ");
                aux = Console.ReadLine();
                if (!Int32.TryParse(aux, out opcion) && aux != "")
                {
                    salir = true;
                }
                else {
                    Console.WriteLine(">> ¿¡Perdona!?... ?@#!!");
                }
            } while (!salir);
            return aux;
        }

        public static int leerHoras()
        {
            bool salir = false;
            string aux = null;
            int horas = 0;
            do
            {
                Console.Write(">> HORAS TRABAJADAS: ");
                aux = Console.ReadLine();
                if (Int32.TryParse(aux, out horas) && (horas <= 250) && (horas > 0))
                {
                    salir = true;
                }
                else
                {
                    Console.WriteLine(">> INTRODUCE UN VALOR VALIDO [1-250]");
                }
            } while (!salir);
            return horas;
        }

        // METODO QUE DEVUELVE LA SELECCION PARA BORRAR DEL LISTADO DE NOMINAS

        public static byte leerOpcionNominaOp(byte tope,string tipo)
        {
            byte opcion = 0;
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("\n>> ¿Y QUÉ NOMINA QUIERES "+ tipo +"?: ");
                aux = Console.ReadLine();
                if (byte.TryParse(aux, out opcion))
                {
                    if (opcion >= 0 && opcion <= tope)
                    {
                        salir = true;
                    }
                    else {
                        Console.WriteLine(">> OPCIÓN FUERA DE RANGO [1-" + tope + "]");
                    }
                }
                else {
                    Console.WriteLine(">> ¿¡Perdona!?... ?@#!!");
                }
            } while (!salir);
            return opcion;
        }


    }
}
