using System;

namespace Gestoria.View
{
    static class InterfazGestoria
    {
        public static void menu_principal(string nombre)
        {
            CH.cls();
            
            string salida = "\n";
            salida += "+===============================+\n";
            salida += "|   MENU DE OPCIONES GESTORIA   |\n";
            salida += "|     - - - - - - - - - - -     |\n";
            salida += "|      " + nombre + "      |\n";
            salida += "+===============================+\n";
            CH.lcdColor(salida,ConsoleColor.Cyan);
            salida = "1. GESTIÓN DE EMPRESAS\n";
            salida += "2. GESTIÓN DE TRABAJADORES\n";
            salida += "3. GESTIÓN DE NOMINAS\n\n";
            salida += "4. MÓDULO DE ADMINISTRACIÓN\n\n";
            salida += "5. CARGA DATOS [MODO DEBUG]\n\n";
            salida += "0. SALIR\n\n";
            CH.lcd(salida);
        }
    }
}
