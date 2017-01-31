#region License
// (C) - 2016 : Miguel Camacho Sánchez @ www.miguelkiko.com
// GESTION DE NÓMINAS - 2º DAM - DESARROLLO INTERFACES
#endregion

using System;

namespace Gestoria.Model
{
    /// <summary>
    /// Clase Modelo para albergar los datos de una nómina de un trabajador
    /// </summary>
    class Nomina
    {
        // ZONA DE ATRIBUTOS

        public string nombre;       // EL NOMBRE DEL TRABAJADOR
        public string apellidos;    // LOS APELLIDOS DEL TRABAJADOR
        private string _mes;        // EL MES CORRESPONDIENTE A LA NOMINA
        private int _horas;         // LAS HORAS TRABAJADAS AL MES
        private float _eurosHoras;  // LOS EUROS QUE COBRA POR HORAS. SE COJE DE FICHERO DE CONFIGURACION
        private int _horasExtra;    // EL TOPE DE HORAS QUE ES SALARIO BASE. EL RESTO SERÍA HORAS EXTRA CON INCREMENTO. FICHERO CONFIG.
        private float _salarioBase; // EL SALARIO BASE. CALCULADO EN NOMINA
        private float _salarioExtra;// EL SALARIO CORRESPONDIENTE A LAS HORAS EXTRA. CALCULADO EN NOMINA
        private float _impuestos;   // EL PORCENTAJE DE IMPUESTOS O RETENCIONES QUE SE EL APLICA A LA NÓMINA. CALCULADO EN NOMINA


        // ZONA DE CONSTRUCTORES
        /// <summary>
        /// Constructor vacio para la clase nómina
        /// </summary>
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

        /// <summary>
        /// Constructor con todos los datos necesarios para crear una Nomina
        /// <paramref name="nombre">El nombre del trabajador</paramref>
        /// <paramref name="apellidos">Los apellidos del trabajador</paramref>
        /// <paramref name="mes">El mes correspondiente a la nómina</paramref>
        /// <paramref name="horas">Las horas trabajadas al mes</paramref>
        /// <paramref name="euxhoras">El precio que cobra por horas</paramref>
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

        /// <summary>
        /// Método para calcular el importe bruto de la nómina
        /// <paramref name="jornada">Las horas consideradas salario base a precio normal. El resto es extra con incremento</paramref>
        /// <paramref name="incrExtra">El tanto por ciento de impuestos a aplicar</paramref>
        /// </summary>
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

        /// <summary>
        /// Método para calcular el importe correspondiente de impuestos
        /// <paramref name="porcentaje">El tanto por ciento de impuesto a aplicar al salario bruto</paramref>
        /// </summary>
        public void calcularImpuestos(float porcentaje)
        {
            _impuestos = salarioBruto * (porcentaje / 100);
        }


        /// <summary>
        /// Sobreescritura del método ToString de la clase para darle formato boniko a los datos de la clase
        /// </summary>
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