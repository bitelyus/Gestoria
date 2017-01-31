using System;
namespace Gestoria.View
{
    static class InterfazAdministracion
    {

        public static void mostrar_menu()
        {

            ConsoleHelper.cls();
            Console.ForegroundColor = ConsoleColor.Cyan;

            string salida = "\n";
            salida += "+============================+\n";
            salida += "|   MENU DE ADMINISTRACION   |\n";
            salida += "|     - - - - - - - - - -    |\n";
            salida += "+============================+\n";
            Console.WriteLine(salida);
            Console.ForegroundColor = ConsoleColor.White;

            salida = "1. MODIFICAR CONTRASEÑA DE ACCESO\n\n";
            salida += "2. MODIFICAR DATOS CONFIG. NÓMINA\n\n";
            salida += "3. MOSTRAR VALORES DE CONFIGURACIÓN\n\n";
            salida += "0. SALIR\n\n";
            Console.Write(salida);
        }





        public static void listarDatosPass(string pass, int maxhoras, int horasbase, float maxeurxhora, float preciojornada, float factorextra, float impuestos)
        {
            string cadena;
            cadena = "\nLISTADO DE VALORES CONFIGURACION\n";
            cadena += "================================";
            CH.lcdColor(cadena, ConsoleColor.Cyan);
            cadena = "1. PASSWORD.........: " + pass + "\n";
            cadena += "---------------------\n";
            cadena += "2. MAXIMO HORAS.....: " + maxhoras + "\n";
            cadena += "3. HORAS SAL. NORMAL: " + horasbase + "\n";
            cadena += "4. EUR MAX. X HORA..: " + maxeurxhora + "\n";
            cadena += "5. PRECIO JORNADA...: " + preciojornada + "\n";
            cadena += "6. FACTOR INC. EXTRA: " + factorextra + "\n";
            cadena += "7. % IMPUESTOS......: " + impuestos + "\n";
            CH.lcd(cadena);
        }


        public static void listarDatos(int maxhoras, int horasbase, float maxeurxhora, float preciojornada, float factorextra, float impuestos)
        {
            string cadena;
            cadena = "\nLISTADO DE VALORES CONFIGURACION\n";
            cadena += "=================================\n";
            CH.lcdColor(cadena, ConsoleColor.Cyan);
            cadena = "1. MAXIMO HORAS.....: " + maxhoras + "\n";
            cadena += "2. HORAS SAL. NORMAL: " + horasbase + "\n";
            cadena += "3. EUR MAX. X HORA..: " + maxeurxhora + "\n";
            cadena += "4. PRECIO JORNADA...: " + preciojornada + "\n";
            cadena += "5. FACTOR INC. EXTRA: " + factorextra + "\n";
            cadena += "6. % IMPUESTOS......: " + impuestos + "\n";
            CH.lcd(cadena);
        }

        public static void listarDatosBin(int maxhoras, int horasbase, float maxeurxhora, float preciojornada, float factorextra, float impuestos)
        {
            string cadena;
            cadena = "\nLISTADO DE VALORES CONFIGURACION !!BINARY!!!\n";
            cadena += "============================================\n";
            CH.lcdColor(cadena, ConsoleColor.Cyan);
            cadena = "1. MAXIMO HORAS.....: " + maxhoras + "\n";
            cadena += "2. HORAS SAL. NORMAL: " + horasbase + "\n";
            cadena += "3. EUR MAX. X HORA..: " + maxeurxhora + "\n";
            cadena += "4. PRECIO JORNADA...: " + preciojornada + "\n";
            cadena += "5. FACTOR INC. EXTRA: " + factorextra + "\n";
            cadena += "6. % IMPUESTOS......: " + impuestos + "\n";
            CH.lcd(cadena);
        }
    }
}
