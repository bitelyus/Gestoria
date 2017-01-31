#region License
// (C) - 2016 : Miguel Camacho Sánchez @ www.miguelkiko.com
// GESTION DE NÓMINAS - 2º DAM - DESARROLLO INTERFACES
#endregion


namespace Gestoria.Model
{   /// <summary>
    /// MODELO PARA GUARDAR DATOS DE UNA GESTORIA
    /// </sumamry>
    class Gestora
    {
        // ZONA DE ATRIBUTOS

        private string _nombre;         // EL NOMBRE DE LA GESTORIA
        private Empresa[] _empresas;    // LA ESTRUCTURA DE DATOS DE EMRPESAS QUE GESTIONA

        // ZONA DE CONSTRUCTORES
        
        public Gestora(string nombre) {
            this.nombre = nombre;
            this._empresas = null;
        }

        // Getters & Setters

        public string nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                this._nombre = value;
            }
        }

        public Empresa[] empresas
        {
            get
            {
                return this._empresas;
            }
        }

        
        // ZONA DE MÉTODOS DE LA CLASE
        
        // METODO PARA AGREGAR UNA EMPRESA A LA GESTORIA

        public bool agregarEmpresa(Empresa empresa)
        {
            Empresa[] copiaempresas = null;
            if (this._empresas == null)
            {
                this._empresas = new Empresa[1];
            }
            else
            {
                copiaempresas = new Empresa[this._empresas.Length];
                this._empresas.CopyTo(copiaempresas, 0);
                this._empresas = new Empresa[this._empresas.Length + 1];
                copiaempresas.CopyTo(this._empresas, 0);
                copiaempresas = null;
            }
            this._empresas[this._empresas.Length - 1] = empresa;
            return true;
        } 

        public bool eliminarEmpresa(Empresa empresa) 
        {
            Empresa[] copia = null;
            int contador = 0;
            if (_empresas.Length == 1)
            {
                _empresas = null;
            }
            else {
                copia = new Empresa[_empresas.Length - 1];
                for (int i = 0; i < _empresas.Length; i++)
                {
                    if (_empresas[i] != empresa)
                    {
                        copia[contador] = _empresas[i];
                        contador++;
                    }
                }
                _empresas = new Empresa[copia.Length];
                copia.CopyTo(_empresas, 0);
                copia = null;
            }
            return true;
        } 
    }
}
