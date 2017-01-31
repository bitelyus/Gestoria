#region License
// (C) - 2016 : Miguel Camacho Sánchez @ www.miguelkiko.com
// GESTION DE NÓMINAS - 2º DAM - DESARROLLO INTERFACES
#endregion

using System;
using Gestoria.View;
using Gestoria.Model;

namespace Gestoria.Controler
{
    /// <summary>
    /// Clase para gestoinar la Lógia de Negocio de todos los procesos relacionados con las Empresas de la Gestoría
    /// </summary>
    static class ControlerEmpresa
    {
        /// <summary>
        /// Procedimiento para mostrar el menu de opciones sección 1. empresas
        /// <paramref name="migestoria">Recibe la gestoria</paramref>
        /// </summary>
        public static void menu_empresas(Gestora migestoria)
        {

            // ZONA DE ATRIBUTOS E INICIALIZACIÓN
            bool salir;     // CONTROL DE SALIDA DEL MENU
            int opcion;     // OPCION DEL MENU ELEGIDA
            // ENTRADA
            salir = false;
            opcion = 0;
            // PROCESO
            do
            {
                InterfazEmpresa.menu_empresa();
                opcion = CH.leerOpcion(5);
                switch (opcion)
                {
                    case 1:
                        ControlerEmpresa.nuevaEmpresa(migestoria);
                        break;
                    case 2:
                        ControlerEmpresa.consultarEmpresa(migestoria);
                        CH.pausa();
                        break;
                    case 3:
                        ControlerEmpresa.modificarEmpresa(ref migestoria);
                        CH.pausa();
                        break;
                    case 4:
                        ControlerEmpresa.borrarEmpresa(migestoria);
                        CH.pausa();
                        break;
                    case 5:
                        InterfazEmpresa.listadoEmpresas(migestoria, false);
                        CH.pausa();
                        break;
                    case 0:
                        //Console.WriteLine("\nBYE BYE!! .. MiK.. VUELVE PRONTO :)\n");
                        salir = true;
                        break;
                }

            } while (!salir);

            // SALIDA

        }


        /// <summary>
        /// Procedimiento para agregar una nueva empresa al listado de empresas de la Gestoria
        /// <paramref name="migestoria">La Estructura Gestoria por referencia</paramref>
        /// </summary>
        public static void nuevaEmpresa(Gestora migestoria)
        {
            // 1. PEDIR LOS DATOS DE LA EMPRESA
            // 2. SI NO EXISTE ESTRUCTURA EMPRESA, LA CREO
            // 3. SI EXISTE, AÑADO. SE COMPRUEBA EN EL MÉTODO AGREGAR
            string aux;
            Empresa empresa;
            // ENTRADA
            CH.lcd("i> SI, AMO... VAMOS A CREAR UNA NUEVA EMPRESA\n");
            empresa = null;
            aux = null;
            // PROCESO
            aux = InterfazEmpresa.leerEmpresa();
            empresa = new Empresa(aux);
            if (migestoria.agregarEmpresa(empresa))
            {
                aux = "\ni> SE HA CREADO UNA NUEVA EMPRESA Y AGREGADO A LA GESTORIA";
            }
            // SALIDA
            CH.lcdColor(aux, ConsoleColor.Green);
            CH.pausa();
        }

        /// <summary>
        /// Procedimiento para modifiar el nombre de una empresa.
        /// <paramref name="migestoria">Recibe la Gestoria por referencia, ya que modificamos a pelo el nombre</paramref>
        /// </summary>
        private static void modificarEmpresa(ref Gestora migestoria)
        {
            // ZONA DE ATRIBUTOS
            int opcion;
            int empresas;
            byte tope;
            Empresa empresa;
            // ENTRADA
            if (migestoria.empresas != null)
            {
                empresas = migestoria.empresas.Length;
                Byte.TryParse(empresas.ToString(), out tope);
                InterfazEmpresa.listadoEmpresas(migestoria, true);
                opcion = CH.leerOpcionMsg(tope, "SELECCIONA UNA EMPRESA");
                empresa = migestoria.empresas[opcion - 1];
                // PROCESO
                empresa.nombre = CH.leerCadena("NUEVO NOMBRE EMPRESA..");
                // SALIDA
                CH.lcdColor("\ni> SE HA MODIFICADO CORRECTAMENTE EL NOMBRE DE LA EMPRESA", ConsoleColor.Green);
            }
            else
            {
                CH.lcdColor("!> NO HAY NINGUNA EMPRESA CREADA!", ConsoleColor.DarkYellow);
            }

        }
        /// <summary>
        /// Procedimiento para consultar los datos de una empresa seleccionada de un indice de empresas.
        /// <paramref name="migestoria">Recibe el objeto gestoria</</paramref>
        /// </summary>
        public static void consultarEmpresa(Gestora migestoria)
        {

            // 1. LISTAR EMPRESAS
            // 2. SELECCIONAR EMPRESA
            // 3. MOSTRAR DATOS EMPRESA
            int opcion;
            int empresas;
            byte tope;
            Empresa empresa;
            // ENTRADA
            if (migestoria.empresas != null)
            {
                empresas = migestoria.empresas.Length;
                Byte.TryParse(empresas.ToString(), out tope);
                InterfazEmpresa.listadoEmpresas(migestoria, true);
                opcion = CH.leerOpcionMsg(tope, "SELECCIONA UNA EMPRESA");
                empresa = migestoria.empresas[opcion - 1];

                // SALIDA
                CH.lcd(empresa.ToString());
            }
            else
            {
                CH.lcdColor("!> NO HAY NINGUNA EMPRESA CREADA!", ConsoleColor.DarkYellow);
            }

        }

        /// <summary>
        /// Método para eliminar una empresa de la gestoria. 
        /// <paramref name="migestoria">Recibe la gestoria entera</paramref>
        /// </summary>
        public static bool borrarEmpresa(Gestora migestoria) {

            // 1. LISTAR EMPRESAS
            // 2. SELECCIONAR EMPRESA
            // 3. BORRAR EMPRESA

            bool borrada;
            byte opcion;
            byte tope;
            int empresas;
            Empresa empresa;
            string mensaje;

            // ENTRADA
            mensaje = "";
            borrada = false;

            // PROCESO
            if (migestoria.empresas!=null) {
                empresas = migestoria.empresas.Length;
                Byte.TryParse(empresas.ToString(), out tope);
                InterfazEmpresa.listadoEmpresas(migestoria,true);
                opcion = CH.leerOpcionMsg(tope,"SELECCIONA UNA EMPRESA DE LA LISTA");
                empresa = migestoria.empresas[opcion-1];
                if (migestoria.eliminarEmpresa(empresa)) {
                    mensaje = "\ni> SE HA ELIMINADO CORRECTAMENTE LA EMPRESA DE LA GESTORIA";
                    borrada = true;
                } else {
                    mensaje = "\n!> ERROR!! ... ALGO HA PASADO QUE LA COSA NO HA SALIDO MUY BIEN";
                }

            } else {
                mensaje = "\n!> NO HAY NINGUA EMPRESA EN LA GESTORIA!";
            }

            // SALIDA
            if (borrada) {
                CH.lcdColor(mensaje,ConsoleColor.Green);
            } else {
                CH.lcdColor(mensaje,ConsoleColor.Red);
            }

            return borrada;

        }   

    }
}
