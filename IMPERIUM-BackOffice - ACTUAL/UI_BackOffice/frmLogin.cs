﻿using DevExpress.Utils.Gesture;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_BackOffice;
using BL_BackOffice;
using System.Net;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Diagnostics;
using System.Xml;
using static UI_BackOffice.Formularios.Sistema.Sistema.frmAcercaSistema;
using System.Runtime.InteropServices;
using System.Globalization;
using DevExpress.XtraSplashScreen;

namespace UI_BackOffice
{        
    public partial class frmLogin : Form
    {
        private readonly UnitOfWork unit;
        public eGlobales eGlobal = new eGlobales();
        //public string dsc_database = ""; 
        TaskScheduler scheduler;
        internal string nombrePC = "";
        private string nombreUserWindows = "";
        private string nombreDominio = "";
        internal string numIP = "";
        

        public frmLogin()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            eGlobal.Entorno = "LOCAL";
            Asignar_VariablesGlobales();
            grdbConexion.SelectedIndex = eGlobal.Entorno == "LOCAL" ? 0 : 1;

            //string result = Validar_Conexion();
            //if (result == "OK")
            //{
            //    Inicializar();
            //}
            //else
            //{
            //    XtraMessageBox.Show("Error al conectarse a la base de datos", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    frmConfigConexion frm = new frmConfigConexion();
            //    frm.eGlobal = eGlobal;
            //    frm.ShowDialog();
            //    if (frm.DatosGuardados)
            //    {
            //        Inicializar();
            //    }
            //}

            Inicializar();


            //ToolTip toolTip1 = new ToolTip();
            //toolTip1.AutoPopDelay = 5000;
            //toolTip1.InitialDelay = 1000;
            //toolTip1.ReshowDelay = 500;
            //toolTip1.ShowAlways = true;
            ////toolTip1.SetToolTip(this.btnProd, "Solo usar cuando se conecte a la VPN");
            ////toolTip1.SetToolTip(this.btnRemoto, "Solo usar sin conexión a la VPN");
           
            btnIniciarsesion.Appearance.BackColor = Program.Sesion.Colores.Verde;
            groupBox2.BackColor = Program.Sesion.Colores.Verde;
            groupBox3.BackColor = Program.Sesion.Colores.Plomo;
            txtUsuario.Select();
        }

        private void Inicializar()
        {
            //eGlobal.Entorno = ConfigurationManager.AppSettings["conexion"].ToString();
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            nombrePC = Environment.MachineName;
            lblHostName.Text = nombrePC != "" ? nombrePC : "<<HostName>>";
            nombreUserWindows = Environment.UserName;
            lblUsuarioWindows.Text = nombreUserWindows != "" ? nombreUserWindows : "<<Usuario Windows>>";
            nombreDominio = Environment.UserDomainName;
            lblNombreDominio.Text = nombreDominio != "" ? nombreDominio : "<<Nombre Dominio>>";
            numIP = ObtenerIP();
            lblIPAddress.Text = numIP != "" ? numIP : "<<IPAddress>>";
            //Int64 tot = PerformanceInfo.GetTotalMemoryInMiB();
            //lblMemoriaRAM.Text = tot + " GB";
            lblVersion.Text = "Versión " + unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("VersionApp")].ToString());

            CargarDatos();
        }

        private void CargarDatos()
        {
            if (eGlobal.UltimoUsuario != "") { txtUsuario.Text = eGlobal.UltimoUsuario; txtUsuario.ForeColor = Color.Black; }
        }

        private void Asignar_VariablesGlobales()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", "").Replace("Config", "config"));
            //xmlDoc.Load("C:\\SG5-Software\\kq_SG5_Controlador.exe.config");
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;
                        switch (unit.Encripta.Desencrypta(node.Attributes[0].Value))
                        {
                            case "conexion": eGlobal.Entorno = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            case "ServidorLOCAL": eGlobal.ServidorLOCAL = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            case "ServidorREMOTO": eGlobal.ServidorREMOTO = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            case "BBDD": eGlobal.BBDD = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            case "FormatoFecha": eGlobal.FormatoFecha = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            case "SeparadorListas": eGlobal.SeparadorListas = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            case "SeparadorDecimal": eGlobal.SeparadorDecimal = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            //case "UltimoLocalidad": eGlobal.UltimoLocalidad = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            case "UltimaEmpresa": eGlobal.UltimaEmpresa = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            case "UltimoUsuario": eGlobal.UltimoUsuario = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                            case "VersionApp": eGlobal.VersionApp = unit.Encripta.Desencrypta(node.Attributes[1].Value); break;
                        }
                    }
                }
            }
        }

        private string Validar_Conexion()
        {
            string result = "";
            result = unit.Usuario.TestConnection();
            return result;
        }

        private string ObtenerIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        private void FormatearClave()
        {
            eUsuario objUser = new eUsuario();
            if (txtUsuario.Text == "" || txtUsuario.Text.ToUpper() == "USUARIO")
            {
                XtraMessageBox.Show("Por favor ingrese un usuario para recuperar la clave", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Se enviará un correo con su nueva clave, ¿Desea continuar?", "Recuperación de clave", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eGlobal.Entorno = Convert.ToInt32(grdbConexion.SelectedIndex) == 0 ? "LOCAL" : "REMOTO";
                Actualizar_appSettings();
                Asignar_VariablesGlobales();
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Validando conexíon", "Conectando...");
                string result = Validar_Conexion();
                SplashScreenManager.CloseForm();

                objUser = unit.Usuario.ObtenerUsuario<eUsuario>(7, txtUsuario.Text);
                if (objUser != null)
                {
                    string sCorreo = objUser.dsc_correo;
                    string sClaveTemp = "Tu nueva contraseña es : " + objUser.dsc_clave;

                    if (unit.Globales.EnviarCorreoElectronico_SMTP(sCorreo, "Restablecer clave de usuario IMPERIUM", sClaveTemp) == true)
                    {
                        txtPassword.Focus();
                    }

                    //if (unit.Globales.EnviarCorreoElectronico_Outlook(sCorreo, "Restablecer clave de usuario IMPERIUM", sClaveTemp) == true)
                    //{
                    //    txtPassword.Focus();
                    //}
                }
                else
                {
                    MessageBox.Show("El usuario ingresado no existe en la base de datos.", "Recuperación de clave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        



        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            //this.Close();
        }

        private void btnIniciarsesion_Click(object sender, EventArgs e)
        {
            eGlobal.Entorno = Convert.ToInt32(grdbConexion.SelectedIndex) == 0 ? "LOCAL" : "REMOTO";
            Actualizar_appSettings();
            Asignar_VariablesGlobales();
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Validando conexíon", "Conectando...");
            string result = Validar_Conexion();
            SplashScreenManager.CloseForm();
            if (result == "OK")
            {
                IniciarSesion(txtUsuario.Text, txtPassword.Text);
            }
            else
            {
                XtraMessageBox.Show("Error al conectarse a la base de datos", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Configurando datos", "Configurando...");
                frmConfigConexion frm = new frmConfigConexion();
                frm.eGlobal = eGlobal;
                SplashScreenManager.CloseForm();
                frm.ShowDialog();
                if (frm.DatosGuardados)
                {
                    Inicializar();
                }
            }
            //IniciarSesion(txtUsuario.Text, txtPassword.Text);
        }

        #region "METODOS PROPIOS"
        private void IniciarSesion(string Usuario, string Password)
        {
            eUsuario objUser = new eUsuario();
            if (Usuario == "" || Usuario.ToUpper() == "USUARIO")
            {
                XtraMessageBox.Show("Por favor ingrese un usuario para iniciar sesión", "Iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (Password == "" || Password.ToUpper() == "CONTRASEÑA")
            {
                XtraMessageBox.Show("Por favor ingrese su contraseña para iniciar sesión", "Iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //if (acctlMenu.SelectedElement == null) { XtraMessageBox.Show("Debe seleccionar una solución", "Iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            Task.Factory.StartNew<eUsuario>(() => {

                eUsuario user = unit.Usuario.ObtenerUsuario<eUsuario>(2, Usuario);
                return user;
            }).ContinueWith((result) => {
                if (result.Result == null)
                {
                    //MessageBox.Show("Error en la conexión al Servidor de base de datos. Por favor verifique si está conectado a una red y si ha seleccionado los parámetros correctos.", "Conexión fallida!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("El usuario ingresado no existe en la base de datos." + Environment.NewLine + "Por favor vuelva a intentarlo", "Permiso denegado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (objUser.flg_activo == "NO") { MessageBox.Show("El usuario que ha ingresado se encuentra inactivo en la base de datos", "Permiso denegado!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (objUser.flg_noexpira == "NO" && DateTime.Now >= objUser.fch_cambioclave)
                {
                    MessageBox.Show("Su contraseña ha caducado, es necesario cambiarla.", "Permiso denegado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    frmCambioPassword frm = new frmCambioPassword();
                    frm.ShowDialog();
                    if (!frm.PasswordCambiado) return;
                }

                if (result.Status == TaskStatus.RanToCompletion)
                {
                    if (result.Result.dsc_clave.ToUpper() == Password.ToUpper())
                    {
                        //Actualizar_appSettings();
                        //Asignar_VariablesGlobales();
                        //MessageBox.Show("Acceso correcto" + Environment.NewLine + "Proceda a seleccionar la solución a la que desea ingresar.", "Iniciar sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUsuario.Enabled = false;
                        txtPassword.Enabled = false;

                        eVersion objVersion = unit.Version.ObtenerVersion<eVersion>(5);
                        if (eGlobal.VersionApp == objVersion.VersionAPP)
                        {
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            frmAlertaVersion frmversion = new frmAlertaVersion(this);
                            frmversion.lblVersion.Text = "Versión " + objVersion.VersionAPP;
                            frmversion.ShowDialog();
                            this.Close();
                        }

                        if (objUser.flg_cambiar_clave != null) { 
                            if (objUser.flg_cambiar_clave == "SI") {
                                Formularios.Sistema.Accesos.frmCambiarContraseña frm = new Formularios.Sistema.Accesos.frmCambiarContraseña();
                                //frm.objUser.flg_cambiar_clave = objUser.flg_cambiar_clave;
                                frm.ShowDialog();
                                this.Close();
                            } 
                        }

                        //frmPrincipal frm = new frmPrincipal();
                        //frm.user = user;
                        //frm.Entorno = Entorno;
                        //
                        //
                        //
                        //
                        //this.Hide();
                        //frm.ShowDialog();
                        //this.Close();
                    }
                    else
                    {
                        MessageBox.Show("La contraseña que ha ingresado es inválida", "Permiso denegado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }, this.scheduler);
        }
        #endregion

        private void Actualizar_appSettings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes == null || node.Attributes.Count == 0) continue;
                        switch (unit.Encripta.Desencrypta(node.Attributes[0].Value))
                        {
                            case "conexion": node.Attributes[1].Value = unit.Encripta.Encrypta(eGlobal.Entorno); break;
                            //case "UltimaEmpresa": node.Attributes[1].Value = unit.Encripta.Encrypta(lkpEmpresa.EditValue.ToString()); break;
                            case "UltimoUsuario": node.Attributes[1].Value = unit.Encripta.Encrypta(txtUsuario.Text); break;
                            //case "BBDD": node.Attributes[1].Value = unit.Encripta.Encrypta(dsc_database); break;
                            case "SeparadorListas": node.Attributes[1].Value = unit.Encripta.Encrypta(CultureInfo.CurrentCulture.TextInfo.ListSeparator); break;
                        }
                    }
                }
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", ""));
            ConfigurationManager.RefreshSection("appSettings");
        }
        

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnIniciarsesion_Click(btnIniciarsesion, new EventArgs());
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnIniciarsesion_Click(btnIniciarsesion, new EventArgs());
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if(txtUsuario.Text == "Usuario") { txtUsuario.Text = ""; txtUsuario.ForeColor = Color.Black; }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if(txtPassword.Text == "Contraseña") { txtPassword.Text = ""; txtPassword.ForeColor = Color.Black; txtPassword.Properties.PasswordChar = '*'; }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if(txtUsuario.Text == "") { txtUsuario.Text = "Usuario"; txtUsuario.ForeColor = Color.Silver; }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if(txtPassword.Text == "") { txtPassword.ForeColor = Color.Silver; txtPassword.Properties.PasswordChar = '\0'; txtPassword.Text = "Contraseña";  }
        }

        private void lblRecuperarclave_Click(object sender, EventArgs e)
        {
            FormatearClave();
        }

        private void grdbConexion_SelectedIndexChanged(object sender, EventArgs e)
        {
            eGlobal.Entorno = Convert.ToInt32(grdbConexion.SelectedIndex) == 0 ? "LOCAL" : "REMOTO";
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape) { DialogResult = DialogResult.Cancel; this.Close(); }
        }


        //public static class PerformanceInfo
        //{
        //    [DllImport("psapi.dll", SetLastError = true)]
        //    [return: MarshalAs(UnmanagedType.Bool)]
        //    public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

        //    [StructLayout(LayoutKind.Sequential)]
        //    public struct PerformanceInformation
        //    {
        //        public int Size;
        //        public IntPtr CommitTotal;
        //        public IntPtr CommitLimit;
        //        public IntPtr CommitPeak;
        //        public IntPtr PhysicalTotal;
        //        public IntPtr PhysicalAvailable;
        //        public IntPtr SystemCache;
        //        public IntPtr KernelTotal;
        //        public IntPtr KernelPaged;
        //        public IntPtr KernelNonPaged;
        //        public IntPtr PageSize;
        //        public int HandlesCount;
        //        public int ProcessCount;
        //        public int ThreadCount;
        //    }

        //    public static Int64 GetTotalMemoryInMiB()
        //    {
        //        PerformanceInformation pi = new PerformanceInformation();
        //        if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
        //        {
        //            return Convert.ToInt64(Math.Round((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / Convert.ToDecimal(1073741824)), 0));
        //        }
        //        else
        //        {
        //            return -1;
        //        }
        //    }
    }
}
