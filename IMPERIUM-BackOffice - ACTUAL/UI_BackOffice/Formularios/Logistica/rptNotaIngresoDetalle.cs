using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraReports.UI;
using BE_BackOffice;
using BL_BackOffice;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.DataAccess.ConnectionParameters;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class rptNotaIngresoDetalle : DevExpress.XtraReports.UI.XtraReport
    {
        //private readonly UnitOfWork unit;
        //SqlConnection Conexion_Reporte = new SqlConnection();

        public rptNotaIngresoDetalle()
        {
            InitializeComponent();
            //  unit = new UnitOfWork();
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            //string entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
            //string Servidor = "192.168.0.5";// unit.Encripta.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
            //string BBDD = "BaseGrupoHNG_20221114";// unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
            //string UserID = "userImperiumSoft";// unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("UserID")].ToString());
            //string Password = "$1mp3rium@22";// unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("Password")].ToString());

            var G = Program.Sesion.Global;
            e.ConnectionParameters = new MsSqlConnectionParameters(G.Servidor, G.BBDD, G.UserSQL, G.PasswordSQL, MsSqlAuthorizationType.SqlServer);
            
            //e.ConnectionParameters = new MsSqlConnectionParameters(Servidor, BBDD, UserID, Password, MsSqlAuthorizationType.SqlServer);
            //e.ConnectionParameters = new MsSqlConnectionParameters("192.168.0.5", "BaseGrupoHNG_20221114","userImperiumSoft", "$1mp3rium@22", MsSqlAuthorizationType.SqlServer);
        }
    }
}
