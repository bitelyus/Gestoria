using System;

namespace Gestoria.Model
{
    class Nomina
    {
        // ZONA DE ATRIBUTOS

        public string nombre;
        public string apellidos;
        private string _mes;
        private int _horas;
        private float _eurosHoras;
        private int _horasExtra;
        private float _salarioBase;
        private float _salarioExtra;
        private float _impuestos;


        // ZONA DE CONSTRUCTORES

        public Nomina() {
            this.nombre = null;
            this.apellidos = null;
            this._mes = null;
            this._horas = 0;
            this._eurosHoras = 0.0F;
            this._horasExtra = 0;
            this._salarioBase = 0.0F;
            this._salarioExtra = 0.0F;
            this._impuestos = 0.0F;
        }

        public Nomina(string nombre, string apellidos, string mes, int horas, float euxhoras) {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.mes = mes;
            this.horas = horas;
            this.eurosHoras = euxhoras;
            this.calcularBruto(160, 1.25F); // horas jornada max. normal e incremento para horas extra 
            this.calcularImpuestos(12.33F); // porcentaje impuestos
        }

        // Getters & Setters: CON CONTROL DE ERRORES 

        public string mes
        {
            get {
                return _mes;
            }
            set {
                this._mes = value;
            }
        }

        public int horas
        {
            get
            {
                return _horas;
            }
            set
            {
                if (value > 0 && value <= 250) 
                {
                    _horas = value;
                }
                else
                {
                    throw new Exception("Número de Horas Invalido!"); //Excepcion
                }
            }
        }

        public float eurosHoras
        {
            get
            {
                return _eurosHoras;
            }
            set
            {
                if (value > 0)
                {
                    _eurosHoras = value;
                }
                else {
                    throw new Exception("Importe Inválido _eurosHoras!"); //Excepcion

                }

            }
        }

        public float salarioBase
        {
            get
            {
                if (_salarioBase <= 0)
                {
                    throw new Exception("Importe Inválido _salarioBase!"); //Excepcion
                }
                return _salarioBase;
            }
        }

        public float salarioExtra
        {
            get
            {
                if (_salarioBase <= 0)
                {
                    throw new Exception("Inporte inválido salarioExtra!"); //Excepcion
                }
                return _salarioExtra;
            }
        }

        public int horasExtras
        {
            get
            {
                if (_salarioBase <= 0)
                {
                    throw new Exception("Número de Horas Extra Invalido!"); //Excepcion
                }
                return _horasExtra;
            }
        }

        public float salarioBruto
        {
            get
            {
                //Al acceder a los gets y no a los miembros, ya esta controlado por excepciones
                return (salarioBase + salarioExtra);
            }
        }

        public float impuestos
        {
            get
            {
                if (_salarioBase <= 0)
                {
                    //excepcion
                }
                return _impuestos;
            }
        }

        public float salarioNeto
        {
            get
            {
                return salarioBruto - impuestos;
            }
        }


        // ZONA DE MÉTODOS

        // METODOS PÚBLICOS PARA CALCULO DE LOS IMPORTES DE LA NOMINA

        public bool calcularBruto(int jornada, float incrExtra)
        {
            bool correcto = false;
            if (_horas > 0 && _eurosHoras > 0)
            {
                if (_horas > jornada)
                {
                    _horasExtra = _horas - jornada;
                    _salarioBase = jornada * _eurosHoras;
                    _salarioExtra = _horasExtra * _eurosHoras * incrExtra;
                }
                else
                {
                    _salarioBase = _horas * _eurosHoras;
                }
                correcto = true;
            }
            return correcto;
        }

        public void calcularImpuestos(float porcentaje)
        {
            _impuestos = salarioBruto * (porcentaje / 100);
        }


        // SOBREESCRITURA DEL METODO TOSTRING DE LA CLASE

        override
        public string ToString() {
            string salida = "";
            salida += "NOMBRE Y APELLIDOS: " + this.nombre + " " + this.apellidos + "\n";
            salida += "MES DE LA NÓNINA..: " + this.mes + "\n";
            salida += "HORAS TRABAJADAS..: " + this.horas + "\n";
            salida += "SALARIO BASE......: " + this.salarioBase + "\n";
            salida += "SALARIO EXTRA.....: " + this.salarioExtra + "\n";
            salida += "SALARIO BRUTO.....: " + this.salarioBruto + "\n";
            salida += "IMPUESTOS.........: " + this.impuestos + "\n";
            salida += "SALARIO NETO......: " + this.salarioNeto+ "\n";
            return salida;
        }

    }
}