using System;
using Gestoria.Model;

namespace Gestoria.View
{
    static class InterfazEmpresa
    {
        
        public static void menu_empresa()
        {
            string salida;
            salida = "\n";
            ConsoleHelper.cls();
            salida += "+----------------------+\n";
            salida += "|   MENU DE EMPRESAS   |\n";
            salida += "+----------------------+\n";
            CH.lcdColor(salida, ConsoleColor.Cyan);
            salida = "1. NUEVA EMPRESA\n";
            salida += "2. CONSULTAR EMPRESA\n";
            salida += "3. MODIFICAR EMPRESA\n";
            salida += "4. BORRAR EMPRESA\n\n";
            salida += "5. LISTAR EMPRESAS\n\n";
            salida += "0. VOLER AL MENÚ PRINCIPAL\n\n";
            Console.Write(salida);
        }

        /// <summary>
        /// Procedimiento para listar las empresas de la gestoria, con o sin indices
        /// <paramref name="indices">True o False para indicar si queremos indices o no</paramref>
        /// <paramref name="migestoria">La Gestoria</paramref>
        /// </summary> 
        public static void listadoEmpresas(Gestora migestoria, bool indices)
        {
            int indice;
            int trabajadores;
            string salida;

            indice = 1;
            trabajadores = 0;
            salida = "\nLISTADO DE EMPRESAS\n===================\n";
            CH.lcd(salida);
            Console.ForegroundColor = ConsoleColor.Blue;
            if (indices)
            {
                Console.WriteLine("{0}{1}{2}", "ID".PadRight(5), "NOMBRE".PadRight(15), "TRABAJADORES");
            }
            else
            {
                Console.WriteLine("{0}{1}", "NOMBRE".PadRight(15), "TRABAJADORES");
            }
            Console.ForegroundColor = ConsoleColor.White;
            salida = "";
           
            if (migestoria.empresas != null)
            {
                if (migestoria.empresas.Length > 0)
                {
                    for (int i = 0; i < migestoria.empresas.Length; i++)
                    {

                        if (migestoria.empresas[i].plantilla != null)
                        {
                            trabajadores = migestoria.empresas[i].plantilla.Length;

                        }
                        if (indices)
                        {
                            Console.WriteLine("{0}{1}{2}", indice.ToString().PadRight(5), migestoria.empresas[i].nombre.PadRight(15), trabajadores);
                        }
                        else
                        {
                            Console.WriteLine("{0}{1}", migestoria.empresas[i].nombre.PadRight(15), trabajadores);
                        }
                        indice++;
                        trabajadores=0;
                    }
                    
                }
            }
            else
            {
                salida += "\n!> NO HAY NINGUNA EMPRESA REGISTRADA, MELÓN!!";
            }
            CH.lcdColor(salida, ConsoleColor.Red);
            //ConsoleHelper.pausa();
        }

        /// <summary>
        /// Procedimiento para listar todos los trabajadore de una empresa. Recibe la Gestoría al completo y el indice de la empresa
        /// que queremos para mostrar los trabajadores. Podría hacerse solo con la lista de trabajadores...
        /// </summary>
        public static void listaTrabajadores(Gestora migestoria, byte empresa)
        {
            string cadena;

            CH.cls();
            cadena = "";
            cadena += "\nLISTADO DE TRABAJADORES EMPRESA: " + migestoria.empresas[empresa - 1].nombre + "\n";
            cadena += "=================================";
            for (int i = 0; i < migestoria.empresas[empresa - 1].nombre.Length; i++)
            {
                cadena += "=";
            }
            cadena += "\n";
            Console.WriteLine(cadena);
            cadena = "";
            if (migestoria.empresas[empresa - 1].plantilla == null)
            {
                CH.lcdColor("i> LA EMPRESA ACTIVA TODAVÍA NO TIENE TRABAJADORES\n", ConsoleColor.Red);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("{0} {1}{2} ", "APELLIDOS".PadRight(10), " NOMBRE".PadLeft(11), "D.N.I.  ".PadLeft(13));
                Console.ForegroundColor = ConsoleColor.White;



                foreach (Trabajador trabajador in migestoria.empresas[empresa - 1].plantilla)
                {
                    Console.WriteLine("{0} {1} {2}", trabajador.apellidos.PadRight(15), trabajador.nombre.PadRight(10), trabajador.dni.PadRight(10));
                }
                //Console.WriteLine(cadena);
            }
            ConsoleHelper.pausa();
        }

        /// <summary>
        /// Función para leer el nombre de una empresa. Comprueva que no sea vacío si sólo números. 
        /// </summary>
        public static string leerEmpresa()
        {
            string salida;
            string aux;
            int numero;
            bool salir;

            salir = false;
            numero = 0;
            aux = "";
            salida = "";
            do
            {
                Console.Write("?> NOMBRE DE LA EMPRESA: ");
                aux = Console.ReadLine();
                if ((String.IsNullOrEmpty(aux)) || (Int32.TryParse(aux, out numero)))
                {
                    CH.lcdColor("!> INTRODUCE UN NOMBRE COMPUESTO POR LETRAS!!", ConsoleColor.Red);
                }
                else
                {
                    salida = aux;
                    salir = true;
                }
            } while (!salir);

            return salida;
        }   // END LEER EMPRESA

    }
}
