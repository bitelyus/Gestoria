using System;

namespace Gestoria.Model
{
    public class DatosBase {

        //private string _password;
        private int _maxhoras;          // EL MAXIMO DE HORAS PERMITAS EN UNA NOMINA
        private int _horasbase;         // EL NUMERO DE HORAS HASTA SALARIO NORMAL
        private float _maxeurxhora;     // EL MÁXIMO DE EUROS POR HORA
        private float _preciojornada;   // EUROS X HORA
        private float _factorextra;     // EL INCREMENTO PARA LAS HORAS EXTRA
        private float _impuestos;       // EL % DE IMPUESTOS

        public DatosBase() {
            //this._password="";
            this._maxhoras=0;
            this._horasbase=0;
            this._maxeurxhora=0.0F;
            this._preciojornada=0.0F;
            this._factorextra=0.0F;
            this._impuestos=0.0F;
        }

        public DatosBase(int maxhoras, int horasbase, float maxeuxhora, float preciojoranda, float incrementoextra, float impuestos) {
            //this._password=pass;
            this._maxhoras=maxhoras;
            this._horasbase=horasbase;
            this._maxeurxhora=maxeuxhora;
            this._preciojornada=preciojoranda;
            this._factorextra=incrementoextra;
            this._impuestos=impuestos;
        }

        /*
        public string password {
            get {
                return _password;
            }
            set {
                if (value.Length<8) { 
                    throw new Exception("LA PASS DEBE TENER MÁS DE OCHO DÍGITOS");
                }
                _password=value;
            }
        }
        */
        public int maxhoras {
            get {
                return _maxhoras;
            }
            set {
                if (value>250) {
                    throw new Exception("NÚMERO DE HORAS MÁXIMO NO PERMITIDO [<255]");
                }
                _maxhoras = value;
            }
        }

        public int horasbase {
            get {
                return _horasbase;
            }
            set {
                if (value<0) {
                    throw new Exception("INTRODUCE UN VALÓR POSITIVO PARA EL TOPE HORAS PARA SALARIO NORMAL");
                }
                _horasbase = value;
            }
        }

        public float maxeuxhora {
            get {
                return _maxeurxhora;
            }
            set {
                _maxeurxhora = value;
            }
        }

        public float preciojoranda {
            get {
                return _preciojornada;
            }
            set {
                if (value<=0) {
                    throw new Exception("HA DE SER UN VALOR POSITIVO EL PRECIO DE LA JORNADA");
                }
                _preciojornada=value;
            }
        }

        public float incrementoextra {
            get {
                return _factorextra;
            }
            set {
                if (value<=1) {
                    throw new Exception("INCREMENTO HA DE SER MAYOR DE 1!!");
                }
                _factorextra=value;
            }
        }

        public float impuestos {
            get {
                return _impuestos;
            }
            set {
                if (value>100) {
                    throw new Exception("PORCENTAJE INVÁLIDO [<100]");
                }
                _impuestos=value;
            }
        } 


    }
}