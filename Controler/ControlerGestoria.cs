using System;
using Gestoria.View;
using Gestoria.Model;

namespace Gestoria.Controler
{
    static class ControlerGestoria
    {
    
        // ZONA DE MÉTODOS PUBLICOS PARA OPCIONES DE MENU
        /// <summary>
        /// Procedimiento para comenzar la funcion ...
        /// </summary>
        public static void comenzar() {
            
            // ESTO PARA COMPROBAR LA EXISTENCIA DE LOS ARCHIVOS DE CONFIGURACION
            // SI NO EXISTEN... LOS CREAMOS SEGUIDAMENTE...
            ControlerAdministracion.configCheck();


            // 1. CREAMOS NUESTRA GESTORIA
            // 2. MOSTRAMOS MENU
            // 3. PEDIMOS OPCION
            // 4. EVALUAMOS Y MOSTRAMOS MÓDULO
            // 5. CUANDO NOS JARTEMOS... > SALIR!

            // ZONA DE ATRIBITOS Y CONSTANTES
            const string NOMBRE_GESTORIA = "GESTIONES ACHARQUIA";
            bool salir;
            int opcion;
            Gestora migestoria;
            // ENTRADA
            migestoria = new Gestora(NOMBRE_GESTORIA);
            opcion = 0;
            salir = false;
            // PROCESO
            do
            {
                InterfazGestoria.menu_principal(NOMBRE_GESTORIA);
                opcion = CH.leerOpcion(5);
                switch (opcion)
                {
                    case 1:
                        ControlerEmpresa.menu_empresas(migestoria);
                        break;
                    case 2:
                        ControlerTrabajador.menu_trabajadores(migestoria);
                        break;
                    case 3:
                        ControlerNomina.menu_nominas(migestoria);
                        break;
                    case 4:
                        ControlerAdministracion.menu_adminstracion();
                        break;
                    case 5:
                        ControlerGestoria.modoDebug(migestoria);
                        CH.pausa();
                        break;                    
                    case 0:
                        salir = true;
                        break;
                }
            } while (!salir);
            // SALIDA
            Console.WriteLine("\nBYE BYE!! .. MiK.. VUELVE PRONTO >:)\n");
        }

        /// </summary>
        /// Procedimiento de carga de datos para Modo DEBUG
        /// <param name="migestoria">Recibe la gestoria </param>
        /// </summary>
        public static void modoDebug(Gestora migestoria) {

            // MODO DEBUG //
            // MODO DEBUG // -> NUTRIENDO DE DATOS PARA EL MODO DEBUG!!
            // MODO DEBUG //

            Nomina n1 = new Nomina("ARTURO", "PEREZ", "ENERO", 120, 12.0F);
            Nomina n2 = new Nomina("ARTURO", "PEREZ", "FEBRERO", 150, 12.0F);
            Nomina n3 = new Nomina("ARTURO", "PEREZ", "MARZO", 170, 12.0F);
            Nomina n4 = new Nomina("ARTURO", "PEREZ", "ABRIL", 200, 12.0F);
            Nomina n5 = new Nomina("ARTURO", "PEREZ", "MAYO", 240, 12.0F);
            Nomina n6 = new Nomina("ARTURO", "PEREZ", "JUNIO", 142, 12.0F);
            Nomina n7 = new Nomina("ARTURO", "PEREZ", "JULIO", 160, 12.0F);
            Nomina n8 = new Nomina("ARTURO", "PEREZ", "AGOSTO", 145, 12.0F);
            Nomina n9 = new Nomina("ARTURO", "PEREZ", "SEPTIEMBRE", 90, 12.0F);
            Nomina n10 = new Nomina("ARTURO", "PEREZ", "OCUBRE", 210, 12.0F);
            Nomina n11 = new Nomina("ARTURO", "PEREZ", "NOVIEMBRE", 100, 12.0F);
            Nomina n12 = new Nomina("ARTURO", "PEREZ", "DICIEMBRE", 136, 12.0F);
            Nomina n13 = new Nomina("MIGUEL", "CAMACHO", "ENERO", 136, 12.0F);
            Nomina n14 = new Nomina("MIGUEL", "CAMACHO", "FEBRERO", 146, 12.0F);

            Trabajador trabajador1 = new Trabajador("MIGUEL", "CAMACHO", "25020050Y");
            Trabajador trabajador2 = new Trabajador("ROSA", "LOPEZ", "25020050Y");
            Trabajador trabajador3 = new Trabajador("ARTURO", "PEREZ", "25020050Y");

            Empresa empresa = new Empresa("CENEC", null);
            Empresa empresa2 = new Empresa("PANDORA", null);

            trabajador3.AgregarNomina(n1);
            trabajador3.AgregarNomina(n2);
            trabajador3.AgregarNomina(n3);
            trabajador3.AgregarNomina(n4);
            trabajador3.AgregarNomina(n5);
            trabajador3.AgregarNomina(n6);
            trabajador3.AgregarNomina(n7);
            trabajador3.AgregarNomina(n8);
            trabajador3.AgregarNomina(n9);
            trabajador3.AgregarNomina(n10);
            trabajador3.AgregarNomina(n11);
            trabajador3.AgregarNomina(n12);
            trabajador1.AgregarNomina(n13);
            trabajador1.AgregarNomina(n14);

            empresa.contratar(trabajador1);
            empresa.contratar(trabajador2);
            empresa2.contratar(trabajador3);

            migestoria.agregarEmpresa(empresa);
            migestoria.agregarEmpresa(empresa2);

            CH.lcdColor("\ni> CARGA DE DATOS COMPLETADA",ConsoleColor.Green);

        }
    }
}
