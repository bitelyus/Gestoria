// FUNCIONES PUBLICAS DISPONIBLES
// .pausa() - pausa el fluzo hasta pulsar una tecla
// .cls() - para limpiar la pantalla
// .lcd() - para mostrar un mensaje por pantalla
// .lcdColor() - para mostrar un mensaja a color por pantalla
// .leerOpcion() - para leer una opcion por teclado, con tope
// .leerOpcionMsg() - para leer una opcion por teclado mostrando mensaje personalizado
// .leerDni() - para leer un dni con validacon de regexp
// .leerFloat() - para leer un numero con formado decimal
// .leerString() - para leer algo que NO sea un número
// .leerCadena() - para leer una cadena cualquiera de texto, no vacia

using System;
namespace Gestoria.View
{
    class CH
    {
        // FUNCIÓN PARA PAUSAR EL FLUJO DEL PROGRAMA HASTA PULSAR CUALQUIER TECLA
        // VER.2.0 - Ahora con mensaje personalizado aleatorio!!! .. Random .. te quiero!!
        public static void pausa()
        {
            string[] frases = new string[]{"\nKaska una tekla para seguir, boniko...","\nEn la Ponia hace frío pero yo me río... Dále!","\nonKeyPress.toFollow()...","\nY hasta aquí podemos leer... Dale a algo pah seguih >:)"};
            int opcion;
            Random random = new Random();
            opcion=random.Next(4);
            Console.Write(frases[opcion]);
            Console.ReadKey();
        }


        // FUNCIÓN PARA LIMPIAR LA PANTALLA
        public static void cls()
        {
            Console.Clear();
        }

        // FUNCIÓN PARA MOSTRAR UN MENSAJE POR PANTALLA
        // @param - string: el mensaje a mostrar
        public static void lcd(string msg) {
            Console.WriteLine(msg);
        }

        // FUNCION PARA MOSTRAR UN MENSAJE A COLOR POR PANTALLA
        // @param - string: el mensaje a mostrar
        // @param - ConsoleColor: el color del mensaje
        public static void lcdColor(string msg, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        // MÉTODO QUE DEVUELVE UNA OPCIÓN DENTRO DEL TOPE INDICADO
        // @param - tope: la opción máxima posible en byte (no más de 255 opciones)
        // @return - byte: la opción elegida
        public static byte leerOpcion(byte tope)
        {
            byte opcion = 0;
            bool salir = false;
            string aux = null;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("?> OK... ¿QUÉ DESEAS HACER?: ");
                Console.ForegroundColor = ConsoleColor.Gray;
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
                    CH.lcdColor("!> ¿¡Perdona!?... ?@#!!",ConsoleColor.Red);
                }
            } while (!salir);
            return opcion;
        }

        // FUNCIÓN PARA LEER UN FLOTANTE O DECIMAL POR PANTALLA
        // @param - string: el mensaje a mostrar por pantalla
        // @return - float: el número introducido
        public static float leerFloat(string msg) {
            float cantidad = 0.0F;
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("?> "+msg+": ");
                aux = Console.ReadLine();
                if (!(String.IsNullOrEmpty(aux)) && (Single.TryParse(aux, out cantidad)))
                {
                    if (cantidad<=100 && cantidad>=0) {
                        salir = true;
                    } 
                    else
                    {
                        CH.lcdColor("!> NO MAS DE 100% O MENOS QUE 0%!! ... @#!",ConsoleColor.Red);
                    }       
                }
                else
                {
                    CH.lcdColor("!> Valor NO valido!! ... @#!",ConsoleColor.Red);
                }               
            } while (!salir);
            return cantidad;
        }

         public static float leerCantidad(string msg) {
            float cantidad = 0.0F;
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("?> "+msg+": ");
                aux = Console.ReadLine();
                if (!(String.IsNullOrEmpty(aux)) && (Single.TryParse(aux, out cantidad)))
                {
                    if (cantidad>0) {
                        salir = true;
                    } 
                    else
                    {
                        CH.lcdColor("!> Importe Inválido ... @#!",ConsoleColor.Red);
                    }       
                }
                else
                {
                    CH.lcdColor("!> Valor NO valido!! ... @#!",ConsoleColor.Red);
                }               
            } while (!salir);
            return cantidad;
        }


        // MÉTODO QUE DEVUELVE UNA OPCIÓN DENTRO DEL TOPE INDICADO MOSTRANDO MENSAJE PERSONALIZADO
        // @param tope: la opción máxima posible en byte (no más de 255 opciones)
        // @return - byte: la opción elegida
        public static byte leerOpcionMsg(byte tope, string msg)
        {
            byte opcion = 0;
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("?> "+ msg + ": ");
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

        // FUNCIÓN DE DEVUELVE UN STRING (NO NUMERO REAL) PEDIDO POR TECLADO
        // @param - string: el mensaje a mostrar
        // @return - string: el string introducido
        public static string leerString(string msg)
        {
            bool salir = false;
            string aux = null;
            int numero = 0;
            do
            {
                Console.Write("?> "+msg+": ");
                aux = Console.ReadLine();
                if ((String.IsNullOrEmpty(aux)) || (Int32.TryParse(aux, out numero)))
                {
                    CH.lcdColor("!> ¿¡Perdona!?... ?@#!!",ConsoleColor.Red);
                }
                else
                {
                    salir = true;
                }               
            } while (!salir);
            return aux;
        }

        // FUNCIÓN QUE DEVUELVE UN STRING (CUALQUIER COSA) QUE NO ESTE VACIO PEDIDO POR TECLADO
        // MOSTRANDO UN MENSAJE PERSONALIZADO POR PANTALLA
        // @param - string: un mensaje para mostrar por pantalla
        public static string leerCadena(string msg)
        {
            bool salir = false;
            string aux = null;
            do
            {
                Console.Write("?> "+msg+": ");
                aux = Console.ReadLine();
                if ((String.IsNullOrEmpty(aux)))
                {
                    CH.lcdColor("!> ¿¡Perdona!?... ?@#!!",ConsoleColor.Red);
                }
                else
                {
                    salir = true;
                }               
            } while (!salir);
            return aux;
        }

        // FUNCION PARA LEER UN DNI POR PANTALLA, CON VALIDACON POR REGEXP
        // @return - string: el documento a comprobar
        public static string leerDni()
        {
            bool salir = false;
            string aux = null;
            decimal dec = 0;
            
            do
            {
                Console.Write("?> INTRODUCE UN DNI: ");
                aux = Console.ReadLine();
                if ((String.IsNullOrEmpty(aux)) || (decimal.TryParse(aux, out dec)))
                {
                    CH.lcdColor("!> ¿¡Perdona!?... ?@#!!",ConsoleColor.Red);
                }
                else
                {   
                    if (esValidoCifNifNIE(aux)) {
                        salir = true;
                    } else {
                        CH.lcdColor("!> FORMATO DNI/NIF/CIF ERRONEO!!", ConsoleColor.Red);
                    }
                }               
            } while (!salir);
            return aux;
        }

        public static string leerPass(string msg) {
            string passw = "";
            Console.Write("?> " +msg+" : ");
            while (true) {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) {
                    if (passw.Length<8) {
                        CH.lcdColor("!> LA PASS DEBE TENER MÁS DE 8 LETRAS",ConsoleColor.Red);
                        passw="";
                        Console.Write("?> " +msg+" : ");
                    } else {
                        break;
                    }
                }
                passw += key.KeyChar;
            }
            return passw.Trim();
        }

        public static int leerNumero(string msg) {
            bool salir = false;
            string aux = "";
            int numero = 0;
            do {
                Console.Write("?> "+ msg +": ");
                aux = Console.ReadLine();
                if ((!String.IsNullOrWhiteSpace(aux) && Int32.TryParse(aux, out numero)))  {
                    salir = true;
                } else {
                    CH.lcdColor("!> Valor NO VÁLIDO!",ConsoleColor.Red);
                }
            } while (!salir);
            return numero;
        }

        // FUNCIÓN DE VALIDACIÓN DE DNI/NIF/CIF MEDIANTE EXPRESIÓN REGULAR
        // @param - string: el documentos a comprobar
        // @return - bool: true si valido false sino
        public static bool esValidoCifNifNIE(string docIdentitat)
        {
            string patron = "[A-HJ-NP-SUVW][0-9]{7}[0-9A-J]|\\d{8}[TRWAGMYFPDXBNJZSQVHLCKE]|[X]\\d{7}[TRWAGMYFPDXBNJZSQVHLCKE]|[X]\\d{8}[TRWAGMYFPDXBNJZSQVHLCKE]|[YZ]\\d{0,7}[TRWAGMYFPDXBNJZSQVHLCKE]";
            string sRemp = "";
            bool ret = false;
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(patron);
            sRemp = rgx.Replace(docIdentitat, "OK");
            if (sRemp == "OK") ret = true;
            return ret;
        }

       
    } // END CLASS
} ////// END NAMESPACES


