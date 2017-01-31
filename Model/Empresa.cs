using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestoria.Model
{
    class Empresa
    {

        // ZONA DE ATRIBUTOS

        public string nombre;               // EL NOMBRE DE LA EMPRESA
        private Trabajador[] _plantilla;    // LA PLANTILLA DE TRABAJADORES
        

        // ZONA DE CONSTRUCCTORES

        public Empresa() {
            this.nombre = null;
            this._plantilla = null;
        }

        public Empresa(String nombre)
        {
            this.nombre = nombre;
            this._plantilla = null;
        }

        public Empresa(string nombre, Trabajador[] plantilla) {
            this.nombre = nombre;
            this._plantilla = plantilla;
        }

        // Getters && Setters

        public Trabajador[] plantilla {
            get {
                return this._plantilla;
            }
        }

        // ZONA DE METODOS

        public void contratar(Trabajador trabajador)
        {
            Trabajador[] copiaplantilla;
            if (this.plantilla == null)
            {
                this._plantilla = new Trabajador[1];
            }
            else {
                copiaplantilla = new Trabajador[this._plantilla.Length];
                this._plantilla.CopyTo(copiaplantilla, 0);
                this._plantilla = new Trabajador[copiaplantilla.Length + 1];
                copiaplantilla.CopyTo(this._plantilla, 0);
                copiaplantilla = null;
            }
            this._plantilla[this._plantilla.Length - 1] = trabajador;
            //Console.WriteLine(">> SE HA CONTRATADO UN NUEVO TRABAJADOR!\n");
            //ConsoleHelper.pausa();

        }

        public bool despedir(Trabajador trabajador) 
        {
            Trabajador[] copia = null;
            int contador = 0;
            if (_plantilla.Length == 1)
            {
                _plantilla = null;
            }
            else {
                copia = new Trabajador[_plantilla.Length - 1];
                for (int i = 0; i < _plantilla.Length; i++)
                {
                    if (_plantilla[i] != trabajador)
                    {
                        copia[contador] = _plantilla[i];
                        contador++;
                    }
                }
                _plantilla = new Trabajador[copia.Length];
                copia.CopyTo(_plantilla, 0);
                copia = null;
            }
            return true;
        }

        override
        public string ToString() {
            string salida;
            int trabajadores;

            trabajadores = 0;
            salida="\n";

            if (plantilla!=null) {
                trabajadores = plantilla.Length;
            }

            salida+="SOY UNA EMPRESA\n===============\n\n";
            salida+="NOMBRE......: " + this.nombre + "\n";
            salida+="TRABAJADORES: " + trabajadores + "\n";
            if (trabajadores>0) {
                foreach (Trabajador t in plantilla) {
                    salida+=t.ToString();
                }
            }

            return salida;
        }


    }
}
