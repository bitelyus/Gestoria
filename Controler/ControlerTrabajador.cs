using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestoria.View;
using Gestoria.Model;

namespace Gestoria.Controler
{
    static class ControlerTrabajador
    {
        /// <summary>
        /// Procedimiento para mostrar el menu de opciones seccion 3. Trabajadores
        /// <paramref name="migestoria">Recibe la Gestoria por referencia</paramref>
        /// </summary>
        public static void menu_trabajadores(Gestora migestoria)
        {
            if (migestoria.empresas != null)
            {
                bool salir = false;     // CONTROL DE SALIDA MENU
                byte opcion = 0;        // LA OPCIÓN DE MENÚ ELEGIDA
                byte empresaActiva = 0; // GUARDARA LA EMPRESA SOBRE LA CUAL SE GESTIONAN LOS TRABAJADORES
                
                do // MIENTRAS NO DIGAMOS QUE QUEREMOS SALIR.... HACER ESTO...
                {
                    // SI NO HAY EMPRESA ACTIVCA, LA PEDIMOS Y MOSTRAMOS MENU
                    if (empresaActiva == 0) { 
                        empresaActiva = InterfazTrabajador.pedirEmpresa(migestoria); 
                    }
                    InterfazTrabajador.menuTrabajador(empresaActiva, migestoria);
                    opcion = CH.leerOpcion(5);
                    switch (opcion)
                    {
                        case 1:     // REGISTRAR TRABAJADOR
                            ControlerTrabajador.nuevoTrabajador(migestoria,empresaActiva);
                            break;
                        case 2:     // CONSULTAR TRABAJADOR
                            ControlerTrabajador.consultarTrabajador(migestoria, empresaActiva); 
                            break;
                        case 3:     // MODIFICAR TRABAJADOR
                            ControlerTrabajador.modificarTrabajador(ref migestoria, empresaActiva);
                            break;
                        case 4:     // ELIMINAR TRABAJADOR
                            ControlerTrabajador.despedirTrabajador(ref migestoria, empresaActiva);
                            break;
                        case 5:     // CONSULTAR TRABAJADORES
                            InterfazEmpresa.listaTrabajadores(migestoria,empresaActiva);
                            break;
                        case 0:     // SALIR 
                            salir = true;
                            break;
                    }
                } while (!salir);
            }
            else {
                CH.lcdColor("\n!> NO HAY NINGUNA 'EMPRESA' EN LA GESTORÍA COMO PARA TENER TRABAJADORES QUE GESTIONAR...\ni> CRÉA ALGUNA Y CONTRATA!!, BONIKO", ConsoleColor.Red);
                CH.pausa();
            }

        }

        /// <summary>
        /// Procedimiento para añadir un nuevo trabajador a la empresa activa.. Pdría recibir sólo la empresa activa. >> V2.0
        /// <paramref name="empresaActiva">El indice de la empresa activa para gestionar sus trajadores</paramref>
        /// <paramref name="migestoria">Le Gestoria con todas las empresa</paramref>
        public static void nuevoTrabajador(Gestora migestoria, byte empresaActiva) {

            // 1. PEDIR DATOS TRABAJADOR.
            // 2. AÑADIR AL ARRAY DE TRABAJADORES DE LA EMPRESA ACTIVA!

            Console.WriteLine("i> TU MANDAS, AMO... VAMOS A CONTRATAR UN NUEVO TRABAJADOR");

            Trabajador trabajador = new Trabajador();
            trabajador = InterfazTrabajador.pedirTrabajador();
            migestoria.empresas[empresaActiva - 1].contratar(trabajador);
            
            CH.lcdColor("\ni> SE HA REGISTRADO AL TRABAJADOR CORRECTAMENTE EN LA EMPRESA!",ConsoleColor.Green);
            ConsoleHelper.pausa();
        }

        /// <summary>
        /// Procedimiento para consultar los datos de una nómida de un trabajador. 
        /// <paramref name="empresaActiva">El indice de la empresa activa para gestionar sus trajadores</paramref>
        /// <paramref name="migestoria">Le Gestoria con todas las empresa</paramref>
        public static void consultarTrabajador(Gestora migestoria, byte empresaActiva)
        {
            byte opcion;
            
            opcion = 0;

            InterfazTrabajador.listarTrabajadores(migestoria.empresas[empresaActiva-1]);
            if (migestoria.empresas[empresaActiva - 1].plantilla != null)
            {
                opcion = InterfazTrabajador.leerOpcionTrabajadorOp((byte)migestoria.empresas[empresaActiva - 1].plantilla.Length, "VER");
                if (opcion != 0)
                {
                    InterfazTrabajador.muestraDatosTrabajador(migestoria.empresas[empresaActiva - 1], opcion);
                }
            }

            ConsoleHelper.pausa();
        }

        /// <summary>
        /// Procedimiento para modificar los datos de un trabajador.. Mejorar listando campos a modificar en lugar de uno por uno >> V2.0
        /// <paramref name="empresaActiva">El indice de la empresa activa para gestionar sus trajadores</paramref>
        /// <paramref name="migestoria">Le Gestoria con todas las empresa</paramref>
        public static void modificarTrabajador(ref Gestora migestoria, byte empresaActiva) {
            byte opcion = 0;
            InterfazTrabajador.listarTrabajadores(migestoria.empresas[empresaActiva - 1]);
            if (migestoria.empresas[empresaActiva - 1].plantilla != null)
            {
                opcion = InterfazTrabajador.leerOpcionTrabajadorOp((byte)migestoria.empresas[empresaActiva - 1].plantilla.Length, "MODIFICAR");
                if (opcion!=0) { 
                    InterfazTrabajador.modificarTrabajador(ref migestoria.empresas[empresaActiva - 1].plantilla[opcion-1]);
                }
            }
           ConsoleHelper.pausa();
           }

        /// <summary>
        /// Procedimiento para elminar un trabador. 
        /// <paramref name="empresaActiva">El indice de la empresa activa para gestionar sus trajadores</paramref>
        /// <paramref name="migestoria">Le Gestoria con todas las empresa</paramref>
        public static void despedirTrabajador(ref Gestora migestoria, byte empresaActiva) {
            // 1. LISTAR TRABAJADORES CON INDICE
            // 2. PEDIR TRABAJADOR
            // 3. ELIMINAR TRABAJADOR > MÉTODO .despedir MODELO Trabajador
            byte opcion = 0;
            InterfazTrabajador.listarTrabajadores(migestoria.empresas[empresaActiva - 1]);
            if (migestoria.empresas[empresaActiva - 1].plantilla != null)
            {
                opcion = InterfazTrabajador.leerOpcionTrabajadorOp((byte)migestoria.empresas[empresaActiva - 1].plantilla.Length, "ELIMINAR");
                if (opcion!=0)
                { 
                    Trabajador trabajador = migestoria.empresas[empresaActiva - 1].plantilla[opcion - 1];
                    if (migestoria.empresas[empresaActiva - 1].despedir(trabajador))
                    {
                        CH.lcdColor("i> SE HA DESPEDIDO AL TRABAJADOR!",ConsoleColor.Red);
                    }
                }
            }
            ConsoleHelper.pausa();
        }

    }
}
