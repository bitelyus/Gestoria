using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestoria.Model
{
    class Trabajador
    {
        // ZONA DE ATRIBUTOS

        public string nombre;
        public string apellidos;
        private string _dni;        
        private Nomina[] _nominas; // Nóminas para un mes


        /// ZONA DE CONSTRUCCTORES
        /// CONSTRUCCTOR VACIO
        public Trabajador() {       
            nombre = null;
            apellidos = null;
            _dni = null;
            _nominas = null;
        }

        public Trabajador(string nombre, string apellidos, string dni) {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.dni = dni;
        }

        // Getters & Setters

        public string dni
        {
            get {
                if (_dni == null) {
                    throw new Exception("DNI no establecido");
                }
                return _dni;
            }
            set {
                byte tipoError = 0; // 0 - No hay errror
                string mensaje = null;
                if (value.Length != 9) {
                    tipoError = 1;
                }

               tipoError = ComprobarDni(value);

                switch (tipoError) {
                    case 1:
                        //throw new Exception("Tamaño Incorrecto DNI");  >> asi no llega al break. usar variable en su lugar.
                        mensaje = ">> Tamaño Incorrecto de DNI!!\n";
                        break;
                    case 2:
                        //throw new Exception("Formato DNI no valido");
                        mensaje = ">> Formato DNI no válido!!\n";
                        break;
                    case 3:
                        //throw new Exception("Formato DNI no valido");
                        mensaje = ">> Letra DNI no válida!!\n";
                        break;
                }
                if (tipoError != 0) {
                    throw new Exception(mensaje);
                }
                _dni = value;
            }
        }

        public static byte ComprobarDni(string dni) {
            byte codigoError = 0;
            int digitos = 0;

            if (dni.Length != 9) {
                codigoError = 1;
            }

            if (codigoError == 0) {
                //Console.WriteLine("ERROR 0... A COMPROBAR DNI INTERNO!");
                if (Int32.TryParse(dni.Substring(0, 7), out digitos))
                {
                    if (char.IsDigit(dni.ToUpper()[8]))
                    {
                        //Console.WriteLine("dni.oupper[8] error!!!:" + dni.ToUpper()[8]);
                        codigoError = 3; // Letra incorrecta
                    }
                    else {
                        if (!VerificarDNI(dni)) {
                            codigoError = 3;
                        }
                    }
                }
                else
                {
                    codigoError = 2; // Formato incorrecto
                }
            }
            return codigoError;
        }


        // FUNCIÓN PARA COMPROVAR LA LETRA DE UN DNI

        public static bool VerificarDNI(String dni)
        {
            bool valido = false;
            string numero = dni.Substring(0, 8);
            string letra = dni.Substring(8, 1).ToUpper();
            char letrachar;
            int esnumero = 0;
            int esletra = 0;
            int resto = 0;
            char letracalculada;
            char[] letrasdni = { 'T', 'R', 'W', 'A', 'G', 'M', 'Y', 'F', 'P', 'D', 'X', 'B', 'N', 'J', 'Z', 'S', 'Q', 'V', 'H', 'L', 'C', 'K', 'E' };

            //Console.WriteLine("numero " + numero + " letra:" + letra);
            if (Int32.TryParse(numero, out esnumero))
            {
                //Console.WriteLine("ES NUMERO-- CONTINUAMOS PARA LETRA");
                if (!Int32.TryParse(letra, out esletra))
                {
                    //Console.WriteLine("ES LETRA-- A COMPROBAR");
                    resto = esnumero % 23;
                    letracalculada = letrasdni[resto];
                    Char.TryParse(letra, out letrachar);
                    //Console.WriteLine("LETRA CALCULADA: " + letracalculada + " -> LEIDA: " + letra);
                    if (letracalculada == letrachar)
                    {
                        //Console.WriteLine("DNI CON LETRA CORRECTA");
                        valido = true;
                    }
                    else
                    {
                        //Console.WriteLine(">> ERROR EN LA LETRA!!");
                        valido = false;
                    }
                }
            }
            return valido;
        } // END FUNCION VERIFICACIÓN DNI


        public bool AgregarNomina(Nomina nomina) {
            bool agregada = false;
            Nomina[] copia = null;

            if (_nominas == null)
            {
                _nominas = new Nomina[1];
            }
            else {
                if (_nominas.Length < 12)
                {
                    copia = new Nomina[_nominas.Length];
                    _nominas.CopyTo(copia, 0);
                    _nominas = new Nomina[copia.Length + 1];
                    copia.CopyTo(_nominas, 0);
                    copia = null;
                }
                else {
                    throw new Exception("Número Máximo de Nóminas mensuales alcanzado");
                }
            }
            _nominas[_nominas.Length - 1] = nomina;
            agregada = true;
            return agregada;
        }


        public bool EliminarNomina(Nomina nomina) {
            Nomina[] copia = null;
            int contador = 0;
            if (_nominas.Length == 1)
            {
                _nominas = null;
            }
            else {
                copia = new Nomina[_nominas.Length-1];
                for (int i = 0; i < _nominas.Length; i++)
                {
                    if (_nominas[i] != nomina)
                    {
                        copia[contador] = _nominas[i];
                        contador++;
                    }
                }
                _nominas = new Nomina[copia.Length];
                copia.CopyTo(_nominas, 0);

                copia = null;
            }
            return true;
        }


        public Nomina devolverNomina(int mes) {
            return _nominas[mes];
        }

        public Nomina[] nominas {
            get {
                return _nominas;
            }
        }

        public bool modificarNomina(Nomina nomina, Nomina nuevaNomina) {
            bool cambiado = false;
            int contador = 0;
            do
            {
                if (this.nominas[contador] == nomina) {
                    this.nominas[contador] = nuevaNomina;
                    cambiado = true;
                }
                contador++;
            } while (!cambiado);

            return true;
        }

       
        override
        public string ToString() {
            string cadena = "";
            //cadena += "\nSOY UN TRABAJADOR\n";
            //cadena += "==================\n";
            cadena += "\nNOMBRE: " + this.nombre + " " + this.apellidos + "\n";
            cadena += "DNI...: " + this.dni + "\n\n";
            cadena += ">> NÓMINAS:\n";
            cadena += "===========\n";
            if (this._nominas == null)
            {
                cadena += "EL TRABAJADOR NO TIENE NINGUNA NOMINA\n";
            }
            else {
                foreach (Nomina nomina in this._nominas) {
                    cadena += nomina.mes.PadRight(10) + " " + nomina.salarioNeto + "\n";
                }
            }
            return cadena;
        }



    }

}