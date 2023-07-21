using System;
using System.Windows.Forms;

namespace UI_BackOffice
{
    static class Program
    {
        internal static AppSesion Sesion;

        //internal static Sesion Sesion;
        //internal static Acceso SolucionAbrir;
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            /*  Comentar cuando pase a producción
             *  TU_SOLUCION: Colocar el nombre de la solucion; BACKOFFICE,SERVICIOS,PRODUCCION,LOTES,RRHH
             *  HNG: Es el Token de sesión; si no entra con sultar la tabla "scfma_solucion".
             *  ADMINISTRADOR: Usuario.
             *  Versión: 2022.0.7, es la versión de la APP, se saca del App.config
             */
            string __token = "uk2P3nGimWFzCVVHCKairx0s481pA3lVnB"; //buscar de la DB
            string __key = "0c2r0af9t443h4pg9ddb1kk10039mg72"; //buscar de la DB
            if (new ConfigSesion().ObtenerAcceso(args) == null)
            {
                var __solucion = "BACKOFFICE";
                var __usuario = "ADMINISTRADOR";
                var __entorno = "LOCAL";
                args = new string[] { $"{__solucion},", $"{__token},", $"{__usuario},", $"{__key}", $",{__entorno}" };
                //string[] args = new string[] { $"{sol},", $"{sis[0].dsc_token_sesion},", $"{usu}" };
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.LookAndFeel.Basic.DefaultSkin.PineLight);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.LookAndFeel.SkinStyle.Basic);


            //Abrir Solución...
            new ConfigSesion(args, new frmSplashScreen());
            //Formularios.Cuentas_Pagar.frmDocumentosporAprobar()



            //Sesion = new Sesion();
            ////Sesion.Key = __key;


            ///*-------*Validar los parámetros enviados desde el login.*-------*/
            //var acceso = ObtenerAcceso(args);
            //if (acceso == null)
            //{
            //    Application.Exit();
            //    return;
            //}

            ///*-------*Guardar los datos de acceso de Solución*-------*/
            //SolucionAbrir = new Acceso()
            //{
            //    Solucion = acceso.Solucion,
            //    Token = acceso.Token,
            //    User = acceso.User,
            //    Key = acceso.Key,
            //    Entorno = acceso.Entorno
            //};
            ///*-------*Inicializamos el parámetro general "Sesion"*-------*/

            //Cifrado(acceso.Key);
            //Sesion.Global = new eGlobales() { Entorno = acceso.Entorno };//ObtenerEntorno(acceso.Entorno, acceso.Solucion, acceso.Key);


            //Sesion.Key = acceso.Key;
            //var unit = new UnitOfWork();
            //Sesion.Colores = unit.Globales.ObtenerColores();
            ////unit.Globales.Actualizar_appSettings(Sesion.Global);// Ver en que momento se va actualizar cambios. quiza en algunas venrtanas

            ////Encriptar Archivos.
            ////string path = Application.StartupPath + "\\sql_reports.config";
            ////unit.Encripta.DesencryptaFile();

            ///* Obtener el token de la sesión: corresponde a cada solución.*/
            //var sistema = unit.Sistema.ListarSolucion<eSolucion>(opcion: 2, dsc_solucion: acceso.Solucion);
            ///*Primera validación: La consulta debe retornar algún valor.*/
            //if (sistema == null && sistema.Count <= 0) { Application.Exit(); return; }
            ///* Segunda validación: Si el Token enviado desde el login está registrado en la DB.*/
            //if (sistema[0].dsc_token_sesion.ToString().Trim().Equals(acceso.Token.Trim()))
            //{


            //    /*  Cargar información del usuario*/
            //    Sesion.Usuario = new eUsuario();
            //    Sesion.Usuario = unit.Usuario.ObtenerUsuario<eUsuario>(opcion: 2, cod_usuario: acceso.User);
            //    /*  Si el usuario no tiene info; se cierra la APP*/
            //    if (Sesion.Usuario == null) { Application.Exit(); }

            //    /*  Cargar las empresas asociadas al usuario*/
            //    Sesion.EmpresaList = unit.Proveedores.ListarEmpresasProveedor<eProveedor_Empresas>(
            //        opcion: 11, cod_proveedor: "", cod_usuario: acceso.User);

            //    /*  Cargar la Versión, esto se valida en el SplashScreen*/
            //    Sesion.Version = sistema[0].dsc_version;
            //    Sesion.RutaDescarga = sistema[0].dsc_ruta_descarga;
            //    Sesion.Solucion = sistema[0].dsc_solucion;
            //    Sesion.Cod_solucion = sistema[0].cod_solucion;

            //    /*  Iniciar el SplasScreen*/
            //    unit.Dispose();
            //    Application.Run(new Form1());
            //    //Application.Run(new frmSplashScreen());
            //}
        }
    }
}
