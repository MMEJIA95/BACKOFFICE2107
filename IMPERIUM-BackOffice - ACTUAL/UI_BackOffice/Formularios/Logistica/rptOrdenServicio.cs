using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.ConnectionParameters;
using System.Configuration;
using BE_BackOffice;
using BL_BackOffice;
using System.Data.SqlClient;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class rptOrdenServicio : DevExpress.XtraReports.UI.XtraReport
    {
        //private readonly UnitOfWork unit;
       // SqlConnection Conexion_Reporte = new SqlConnection();
        public rptOrdenServicio()
        {
            InitializeComponent();
            //   unit = new UnitOfWork();
        }

        private void sqlDataSource1_ConfigureDataConnection(object sender, DevExpress.DataAccess.Sql.ConfigureDataConnectionEventArgs e)
        {
            //string entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("Conexion")].ToString());
            //string Servidor = unit.Encripta.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
            //string BBDD = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
            //string UserID = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("UserID")].ToString());
            //string Password = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("Password")].ToString());

            //e.ConnectionParameters = new MsSqlConnectionParameters(Servidor, BBDD, UserID, Password, MsSqlAuthorizationType.SqlServer);
            var G = Program.Sesion.Global;
            e.ConnectionParameters = new MsSqlConnectionParameters(G.Servidor, G.BBDD, G.UserSQL, G.PasswordSQL, MsSqlAuthorizationType.SqlServer);

        }
    }
}
