using System;
using System.IO;

namespace Gestoria.View
{
    /// <summary>
    /// File Helper >> FH >> Clase de Ayuda para los procesos de System.IO u Entrada/Salida > Escritura/Lectura de Ficheros
    /// </summary>
    static class FH {

        /// <summary>
        /// Procedimiento para solicitar el formato deseado para el archivo de configuracion
        /// </summary>
        public static byte pedirFormato()
        {
            byte formato;
            string salida;
            salida = "\nFORMATOS DISPONIBLES PARA EL FICHERO\n";
            salida += "====================================\n\n";
            salida += "1. FORMATO TEXTO .TXT\n";
            salida += "2. FORMATO BINARIO .BIN\n";
            CH.lcd(salida);
            formato = CH.leerOpcionMsg(2, "QUE FORMATO DESEAS, AMO");
            return formato;
        }

        /// <summary>
        /// Función para grabar los valores de configuración en un archivo de TEXTO
        /// <paramref name="ruta">La ruta del archivo donde vamos a guardar los datos</paramref>
        /// <paramref name="pass">La constraseña de acceso al archivo y modulo de configuracion</paramref>
        /// <paramref name="horasbase">Las horas consideradas salario normal. El resto sería horas extra</paramref>
        /// <paramref name="maxeurxhora">El máximo que euros por hora que se puede ganar</paramref>
        /// <paramref name="maxhoras">El máximo de horas que puede tener la nómina. Horas mensuales...</paramref>
        /// <paramref name="preciojornada">El precio al que se cobra la hora de jornada</paramref>
        /// <paramref name="incrementoextra">El incremento que llevan las horas extra</paramref>
        /// <paramref name="impuestos">El porcentaje de impuestos a aplicar</paramref>
        /// <return>True or False</return>
        /// </summary>
        public static bool grabarValores(string ruta, string pass, int maxhoras, int horasbase, float maxeurxhora, float preciojornada, float incrementoextra, float impuestos)
        {
            bool grabado = false;
            
            try
            {
                StreamWriter sw = File.CreateText(ruta);
                sw.WriteLine("pass:" + pass);
                sw.WriteLine("max_horas:" + maxhoras);
                sw.WriteLine("horas_base:" + horasbase);
                sw.WriteLine("max_euros_hora:" + maxeurxhora);
                sw.WriteLine("precio_jornada:" + preciojornada);
                sw.WriteLine("incremento_extra:" + incrementoextra);
                sw.WriteLine("impuestos:" + impuestos);
                CH.lcdColor("\ni> LOS NUEVOS VALORES HAN SIDO GRABADOS EN EL FICHERO DE CONFIGURACIÓN", ConsoleColor.Green);
                CH.pausa();
                sw.Flush();
                sw.Dispose();
                grabado=true;
            }
            catch (IOException ex)
            {
                CH.lcdColor(ex.Message + ". -> Contacte con el Administrador", ConsoleColor.Red);
                CH.pausa();
            }
            return grabado;
        }

        /// <summary>
        /// Función para grabar los valores de configuración en un archivo BINARIO Codificación UTF-8!!!
        /// <paramref name="ruta">La ruta del archivo donde vamos a guardar los datos</paramref>
        /// <paramref name="pass">La constraseña de acceso al archivo y modulo de configuracion</paramref>
        /// <paramref name="horasbase">Las horas consideradas salario normal. El resto sería horas extra</paramref>
        /// <paramref name="maxeurxhora">El máximo que euros por hora que se puede ganar</paramref>
        /// <paramref name="maxhoras">El máximo de horas que puede tener la nómina. Horas mensuales...</paramref>
        /// <paramref name="preciojornada">El precio al que se cobra la hora de jornada</paramref>
        /// <paramref name="incrementoextra">El incremento que llevan las horas extra</paramref>
        /// <paramref name="impuestos">El porcentaje de impuestos a aplicar</paramref>
        /// </summary>
        public static bool grabarValoresBinary(string ruta, string pass, int maxhoras, int horasbase, float maxeurxhora, float preciojornada, float incrementoextra, float impuestos)
        {
            bool grabado = false;

            try
            {
                FileStream fs;
                BinaryWriter bw;
                
                fs = File.Open(ruta,FileMode.OpenOrCreate,FileAccess.Write);
                bw = new BinaryWriter(fs);
                
                bw.Write(pass.PadRight(30));    // ESCRIBIMOS UN STRING > LEER STRING:ReadString
                bw.Write(maxhoras);             // ESCRIBIMOS UN ENTERO > LEER ENTERO:ReadInt32
                bw.Write(horasbase);            // ESCRIBIMOS UN ENTERO > LEER ENTERO:ReadInt32
                bw.Write(maxeurxhora);          // ESCBIRIMOS UN ENTERO > LEER ENTERO:ReadInt32
                bw.Write(preciojornada);        // ESCBIRIMOS UN ENTERO > LEER ENTERO:ReadInt32
                bw.Write(incrementoextra);      // ESCRIBIMOS UN FLOAT > LEER UN FLOAT:ReadSingle
                bw.Write(impuestos);            // ESCRIBIMOS UN FLOAT > LEER UN FLOAT:ReadSingle

                bw.Flush();     // TIRAMOS DE LA CADENA
                //bw.Close();
                bw.Dispose();   // CERRAMOS LA TAPA DEL W.C.
                grabado=true;   

                CH.lcdColor("\ni> LOS VALORES BINARIOS HAN SIDO GRABADOS EN EL FICHERO DE CONFIGURACIÓN 'config.data'", ConsoleColor.Green);
                CH.pausa();
                
            }
            catch (IOException ex)
            {
                CH.lcdColor("!> ERR: " + ex.Message + ". -> Contacte con el Administrador", ConsoleColor.Red);
                CH.pausa();
            }
            return grabado;
        }
        
    }
}