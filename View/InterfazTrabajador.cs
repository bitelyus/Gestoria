using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestoria.Model;

namespace Gestoria.View
{
    static class InterfazTrabajador
    {
        public static void menuTrabajador(byte empresa, Gestora migestoria) {
            ConsoleHelper.cls();
            string cadena = null;
            cadena += "\n+======================+\n";
            cadena += "| MENU DE TRABAJADORES |\n";
            cadena += "+======================+\n\n";
            cadena += "*> EMPRESA: " + empresa + ". " + migestoria.empresas[empresa-1].nombre + " <*\n";
            CH.lcdColor(cadena,ConsoleColor.Cyan);
            cadena = "1. REGISTRAR TRABAJADOR\n";
            cadena += "2. CONSULTAR TRABAJADOR\n";
            cadena += "3. MODIFICAR TRABAJADOR\n";
            cadena += "4. DESPEDIR TRABAJADOR\n\n";
            cadena += "5. LISTAR TRABAJADORES\n\n";
            cadena += "0. << GORVER <<\n";
            Console.WriteLine(cadena);
        }

        public static byte pedirEmpresa(Gestora migestoria) {
            string cadena = null;
            int trabajadores = 0;
            byte opcion;
            cadena += "\nLISTADO DE EMPRESAS\n";
            cadena += "===================\n";
            for (int i = 0; i<migestoria.empresas.Length;i++) {
                if (migestoria.empresas[i].plantilla == null) {
                    trabajadores = 0;
                } else {
                    trabajadores = migestoria.empresas[i].plantilla.Length;
                }
                cadena += (i + 1) + ". " + migestoria.empresas[i].nombre+" | TRABAJADORES: " + trabajadores +"\n";
            }
            Console.WriteLine(cadena);
            opcion = ConsoleHelper.leerEmpresa((byte)migestoria.empresas.Length);
            return opcion;
        }

        public static byte seleccionarTrabajador(Gestora migestoria, byte empresa)
        {
            string cadena = null;
            byte opcion;
            cadena += "\nLISTADO DE TRABAJADORES\n";
            cadena += "=======================\n";
            for (int i = 0; i < migestoria.empresas[empresa-1].plantilla.Length; i++)
            {
                cadena += (i + 1) + ". " + migestoria.empresas[empresa-1].plantilla[i].nombre + " " + migestoria.empresas[empresa - 1].plantilla[i].apellidos + "\n";
            }
            Console.WriteLine(cadena);
            opcion = ConsoleHelper.leerTrabajador((byte)migestoria.empresas[empresa-1].plantilla.Length);
            return opcion;
        }


        // MÉTODO QUE CREA UN TRABAJADOR, PIDE LOS DATOS Y LO DEVUELVE

        public static Trabajador pedirTrabajador() {
            Trabajador trabajador = new Trabajador();
            bool salir = false;
            do
            {
                try
                {
                    trabajador.nombre = ConsoleHelper.leerNombre();
                    trabajador.apellidos = ConsoleHelper.leerApellidos();
                    do
                    {
                        trabajador.dni = ConsoleHelper.leerDni();
                        salir = true;
                    } while (!salir);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            } while (!salir);
            return trabajador;
        }


        // METODO PARA LISTAR LAS NOMINAS POR INDICE DEL TRABJADOR. RECIBE EL TIPOTRABAJADOR

        public static void listarTrabajadores(Empresa empresa)
        {
            if (empresa.plantilla != null)
            {
                Console.WriteLine("\nLISTADO DE TRABAJADORES");
                Console.WriteLine("=======================");
                for (int i = 0; i < empresa.plantilla.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + empresa.plantilla[i].nombre + " " + empresa.plantilla[i].apellidos);
                }
            }
            else {
                CH.lcdColor("\n!> LA EMPRESA NO TIENE TRABAJADORES CONTRATADOS!",ConsoleColor.Red);
            }

        }

        public static byte leerOpcionTrabajadorOp(byte tope, string tipo)
        {
            byte opcion = 0;
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("\n?> ¿Y QUÉ TRABAJADOR QUIERES " + tipo + "?: ");
                aux = Console.ReadLine();
                if (byte.TryParse(aux, out opcion))
                {
                    if (opcion >= 0 && opcion <= tope)
                    {
                        salir = true;
                    }
                    else {
                        CH.lcdColor("!> OPCIÓN FUERA DE RANGO [1-" + tope + "]",ConsoleColor.DarkYellow);
                    }
                }
                else {
                    CH.lcdColor("!> ¿¡Perdona!?... ?@#!!",ConsoleColor.Red);
                }
            } while (!salir);
            return opcion;
        }


        // MÉTODO QUE MUESTRA LOS DATOS DE UNA NÓMINA DE UN TRABAJADOR POR ÍNDICE
        public static void muestraDatosTrabajador(Empresa empresa, byte indice)
        {
            Console.WriteLine("\nDATOS DEL TRABAJADOR:");
            Console.WriteLine("=====================");
            Console.WriteLine(empresa.plantilla[indice-1].ToString());
        }

        public static void modificarTrabajador(ref Trabajador trabajador) {
            Console.WriteLine(">> DATOS DEL TRABAJADOR:\n" + trabajador.ToString());
            Console.WriteLine(">> AHORA DAME LOS NUEVOS DATOS...");
            try
            {
                trabajador.nombre = ConsoleHelper.leerNombre();
                trabajador.apellidos = ConsoleHelper.leerApellidos();
                trabajador.dni = ConsoleHelper.leerDni();
                CH.lcdColor("\n>> DATOS DEL TRABAJADOR ACTUALIZADOS!",ConsoleColor.Green);
            }
            catch (Exception ex) {
                Console.WriteLine(">> ERROR: " + ex.Message);
            }
            

        }
    }
}
