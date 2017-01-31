#region License
// (C) - 2016 : Miguel Camacho Sánchez @ www.miguelkiko.com
// GESTION DE NÓMINAS - 2º DAM - DESARROLLO INTERFACES
#endregion

// PROBANDO HERENCIA //
// PROBANDO HERENCIA //
// PROBANDO HERENCIA //

using System;
namespace Gestoria.Model
{

    class NominaEjecutivo:Nomina {

        private float _bono;
        public float bono {
            get {
                return _bono;
            }
            set {
                if (value<0) {
                    throw new Exception("Dáme un valor para el bóno positivo");
                }
                _bono = value;
            }
        }

        public NominaEjecutivo() : base() {
            this._bono = 0.00F;
        }

        public NominaEjecutivo(float bono, string nombre, string apellidos, string mes, int horas, float euxhoras):
            base(nombre, apellidos, mes, horas, euxhoras) {
                this._bono=bono;
        }


        /*

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
        */

    }
}