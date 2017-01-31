using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestoria.View
{
    class ConsoleHelper
    {
        public static void pausa()
        {
            Console.Write("\nPulse una telca para continuar...");
            Console.ReadKey();
        }

        public static void cls()
        {
            Console.Clear();
        }



        /*
        public static string SolicitarNombre(ref byte control, string nombre)//control y salida se modifican y afecta fuera
                                                                             //nombre se modifica dentro y afecta solo al metodo
        {
            string aux = null;
            int numero = 0;
            if (control == 1)
            {
                Console.Write("INTRODUZCA EL NOMBRE: ");
                aux = Console.ReadLine();
                if ((String.IsNullOrEmpty(aux)) || (Int32.TryParse(aux, out numero)))
                {
                    Console.WriteLine("Introduce un nombre compuesto por letras");
                }
                else
                {
                    nombre = aux;
                    //actualizacion control de entrada
                    control = 2;
                }

            }
            else if (control > 1)
            { // el dato ya esta introducido correctamente

                Console.Clear();
                Console.WriteLine("NOMBRE EMPRESA: {0}", nombre);
            }

            return nombre;

        }*/


        // Método que pide una opcion desde 0 hasta el tope que se marque

        public static byte leerOpcion(byte tope)
        {
            byte opcion = 0;
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("?> OK, AMO.. ¿QUÉ DESEAS HACER?: ");
                aux = Console.ReadLine();
                if (byte.TryParse(aux, out opcion))
                {
                    if (opcion >= 0 && opcion <= tope)
                    {
                        salir = true;
                    }
                    else {
                        Console.WriteLine("!> OPCIÓN FUERA DE RANGO [0-" + tope + "]");
                    }

                }
                else {
                    Console.WriteLine("!> ¿¡Perdona!?... ?@#!!");
                }
            } while (!salir);
            return opcion;
        }


        public static byte leerOpcionNomina(byte tope, string tipo)
        {
            byte opcion = 0;
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("\n?> ¿Y QUÉ NOMINA QUIERES " + tipo + "?: ");
                aux = Console.ReadLine();
                if (byte.TryParse(aux, out opcion))
                {
                    if (opcion > 0 && opcion <= tope)
                    {
                        salir = true;
                    }
                    else {
                        Console.WriteLine("!> OPCIÓN FUERA DE RANGO [1-" + tope + "]");
                    }

                }
                else {
                    Console.WriteLine("?> ¿¡Perdona!?... ?@#!!");
                }
            } while (!salir);
            return opcion;
        }




        public static byte leerEmpresa(byte tope)
        {
            byte opcion = 0;
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("?> SELECCIONA UNA EMPRESA DE LA LISTA: ");
                aux = Console.ReadLine();
                if (byte.TryParse(aux, out opcion))
                {
                    if (opcion > 0 && opcion <= tope)
                    {
                        salir = true;
                    }
                    else {
                        Console.WriteLine("!> OPCIÓN FUERA DE RANGO [1-" + tope + "]");
                    }

                }
                else {
                    Console.WriteLine("!> ¿¡Perdona!?... ?@#!!");
                }
            } while (!salir);
            return opcion;
        }

        public static byte leerTrabajador(byte tope)
        {
            byte opcion = 0;
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("?> SELECCIONA UN TRABAJADOR DE LA LISTA: ");
                aux = Console.ReadLine();
                if (byte.TryParse(aux, out opcion))
                {
                    if (opcion > 0 && opcion <= tope)
                    {
                        salir = true;
                    }
                    else {
                        Console.WriteLine("!> OPCIÓN FUERA DE RANGO [1-" + tope + "]");
                    }

                }
                else {
                    Console.WriteLine("!> ¿¡Perdona!?... ?@#!!");
                }
            } while (!salir);
            return opcion;
        }


        public static string leerNombre()
        {
            bool salir = false;
            string aux = null;
            int opcion = 0;
            do
            {
                Console.Write("\n?> NOMBRE TRABAJADOR...: ");
                aux = Console.ReadLine();
                if (!Int32.TryParse(aux, out opcion) && aux!="")
                {
                    salir = true;
                }
                else {
                    Console.WriteLine("!> ¿¡Perdona!?... ?@#!!");
                }
            } while (!salir);
            return aux;
        }


        public static string leerApellidos()
        {
            bool salir = false;
            string aux = null;
            int opcion = 0;
            do
            {
                Console.Write("?> APELLIDOS TRABAJADOR: ");
                aux = Console.ReadLine();
                if (!Int32.TryParse(aux, out opcion) && aux != "")
                {
                    salir = true;
                }
                else {
                    Console.WriteLine("!> ¿¡Perdona!?... ?@#!!");
                }
            } while (!salir);
            return aux;
        }

        public static string leerDni()
        {
            bool salir = false;
            string aux = null;
            int opcion = 0;
            do
            {
                Console.Write("?> DNI DEL TRABAJADOR..: ");
                aux = Console.ReadLine();
                if (!Int32.TryParse(aux, out opcion) && aux != "")
                {
                    salir = true;
                }
                else {
                    Console.WriteLine("!> ¿¡Perdona!?... ?@#!!");
                }
            } while (!salir);
            return aux;
        }



    }

}
