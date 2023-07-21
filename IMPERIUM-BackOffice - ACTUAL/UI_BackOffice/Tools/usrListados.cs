using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BE_BackOffice;
using BL_BackOffice;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using UI_BackOffice.Formularios.Cuentas_Pagar;
using System.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Security;
using Microsoft.Identity.Client;
using Microsoft.Office.Interop.Excel;
using Rectangle = System.Drawing.Rectangle;

namespace UI_BackOffice.UserControls
{
    public partial class usrListados : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly UnitOfWork unit;
        Image ImgPDF = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_16x16.png");
        Image ImgXML = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttoxml_16x16.png");
        DateTime oPrimerDiaDelMes = new DateTime();
        DateTime oUltimoDiaDelMes = new DateTime();
        string cod_empresa = "";

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "";

        public usrListados()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void ucListados_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            oPrimerDiaDelMes = new DateTime(date.Year, 1, 1);
            oUltimoDiaDelMes = new DateTime(date.Year, 12, 31);
            List<eFacturaProveedor> listEmpresas = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
            //if (listEmpresas.Count >= 1) cod_empresa = listEmpresas[0].cod_empresa;
            cod_empresa = string.Join(",", listEmpresas.Select(t => t.cod_empresa));

            CargarListaFacturasVencidas(cod_empresa);
            CargarListaFacturasPendientes(cod_empresa);
        }        

        private void CargarListaFacturasVencidas(string cod_empresa) 
        {
            List<eFacturaProveedor> lista = unit.Factura.FiltroFactura<eFacturaProveedor>(60, cod_empresa, FechaInicio: oPrimerDiaDelMes.ToString("yyyyMMdd"), FechaFin: oUltimoDiaDelMes.ToString("yyyyMMdd"));
            bsListadoFacturasVencidas.DataSource = lista;
        }

        private void CargarListaFacturasPendientes(string cod_empresa)
        {
            List<eFacturaProveedor> lista = unit.Factura.FiltroFactura<eFacturaProveedor>(61, cod_empresa, FechaInicio: oPrimerDiaDelMes.ToString("yyyyMMdd"), FechaFin: oUltimoDiaDelMes.ToString("yyyyMMdd"));
            bsListadoFacturasPendientes.DataSource = lista;
        }

        private void gcListaFacturasVencidas_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) CargarListaFacturasVencidas(cod_empresa);
        }

        private void gcListaFacturasPendientes_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) CargarListaFacturasPendientes(cod_empresa);
        }

        private void gvListaFacturasVencidas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eFacturaProveedor obj = gvListaFacturasVencidas.GetRow(e.RowHandle) as eFacturaProveedor;
                    if (obj.dsc_estado_pago == "PAGADO") e.Appearance.ForeColor = Color.Blue;
                    if (obj.dsc_estado_documento == "Anulado") e.Appearance.ForeColor = Color.Red;
                    if (e.Column.FieldName == "fch_pago_programado" && obj.fch_pago_programado < DateTime.Today && obj.imp_saldo != 0) e.Appearance.BackColor = Color.LightSalmon;
                    if (e.Column.FieldName == "fch_pago_ejecutado_detraccion" && obj.fch_pago_ejecutado_detraccion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_pago_ejecutado" && obj.fch_pago_ejecutado.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_constancia_detraccion" && obj.fch_constancia_detraccion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_aprobado_reg" && obj.fch_aprobado_reg.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_contabilizado" && obj.fch_contabilizado.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_anulacion" && obj.fch_anulacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "flg_PDF" || e.Column.FieldName == "flg_XML") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_XML" && obj.flg_XML == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgXML, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                   
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaFacturasPendientes_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eFacturaProveedor obj = gvListaFacturasPendientes.GetRow(e.RowHandle) as eFacturaProveedor;
                    if (obj.dsc_estado_pago == "PAGADO") e.Appearance.ForeColor = Color.Blue;
                    if (obj.dsc_estado_documento == "Anulado") e.Appearance.ForeColor = Color.Red;
                    if (e.Column.FieldName == "fch_pago_programado" && obj.fch_pago_programado < DateTime.Today && obj.imp_saldo != 0) e.Appearance.BackColor = Color.LightSalmon;
                    if (e.Column.FieldName == "fch_pago_ejecutado_detraccion" && obj.fch_pago_ejecutado_detraccion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_pago_ejecutado" && obj.fch_pago_ejecutado.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_constancia_detraccion" && obj.fch_constancia_detraccion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_aprobado_reg" && obj.fch_aprobado_reg.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_contabilizado" && obj.fch_contabilizado.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_anulacion" && obj.fch_anulacion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "flg_PDF" || e.Column.FieldName == "flg_XML") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_XML" && obj.flg_XML == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgXML, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }

                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        static void Appl()
        {
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{TenantId}")
                .WithDefaultRedirectUri()
                .Build();
            TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }
        private static string ClientId = "";
        private static string TenantId = "";
        private static string Instance = "https://login.microsoftonline.com/";
        public static IPublicClientApplication _clientApp;
        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
        private async void gvListaFacturasVencidas_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    obj = gvListaFacturasVencidas.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.RUC = obj.dsc_ruc;
                    frmModif.tipo_documento = obj.tipo_documento;
                    frmModif.serie_documento = obj.serie_documento;
                    frmModif.numero_documento = obj.numero_documento;
                    frmModif.cod_proveedor = obj.cod_proveedor;
                    frmModif.orden_servicio = obj.num_OrdenCompraServ;

                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }
                if (e.Clicks == 2 && (e.Column.FieldName != "flg_PDF" && e.Column.FieldName != "flg_XML"))
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    obj = gvListaFacturasVencidas.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.RUC = obj.dsc_ruc;
                    frmModif.tipo_documento = obj.tipo_documento;
                    frmModif.serie_documento = obj.serie_documento;
                    frmModif.numero_documento = obj.numero_documento;
                    frmModif.cod_proveedor = obj.cod_proveedor;
                    frmModif.orden_servicio = obj.num_OrdenCompraServ;

                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }
                if (e.Clicks == 2 && (e.Column.FieldName == "flg_PDF" || e.Column.FieldName == "flg_XML"))
                {
                    obj = gvListaFacturasVencidas.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    eFacturaProveedor eFact = unit.Factura.ObtenerFacturaProveedor<eFacturaProveedor>(24, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);

                    if (e.Column.FieldName == "flg_PDF" && (eFact.idPDF == null || eFact.idPDF == ""))
                    {
                        MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (e.Column.FieldName == "flg_XML" && (eFact.idXML == null || eFact.idXML == ""))
                    {
                        MessageBox.Show("No se cargado ningún XML", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                    if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                    { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    //var app = App.PublicClientApp;
                    ClientId = eEmp.ClientIdOnedrive;
                    TenantId = eEmp.TenantOnedrive;
                    Appl();
                    var app = PublicClientApp;

                    try
                    {
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                        //eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                        string correo = eEmp.UsuarioOnedrive;
                        string password = eEmp.ClaveOnedrive;

                        var securePassword = new SecureString();
                        foreach (char c in password)
                            securePassword.AppendChar(c);

                        authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                        GraphClient = new Microsoft.Graph.GraphServiceClient(
                        new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                        {
                            requestMessage
                                .Headers
                                .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                            return Task.FromResult(0);
                        }));

                        string IdPDF = eFact.idPDF;
                        string IdXML = eFact.idXML;
                        string IdOneDriveDoc = e.Column.FieldName == "flg_PDF" ? IdPDF : IdXML;
                        string Extension = e.Column.FieldName == "flg_PDF" ? ".pdf" : ".xml";

                        var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                        string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + (eFact.NombreArchivo + Extension);
                        if (!System.IO.File.Exists(ruta))
                        {
                            using (var fileStream = new FileStream(ruta, FileMode.Create, System.IO.FileAccess.Write))
                                fileContent.CopyTo(fileStream);
                        }

                        if (!System.IO.Directory.Exists(unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()))) System.IO.Directory.CreateDirectory(unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()));
                        System.Diagnostics.Process.Start(ruta);
                        SplashScreenManager.CloseForm();
                    }
                    catch (Exception ex)
                    {
                        SplashScreenManager.CloseForm();
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void gvListaFacturasPendientes_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    obj = gvListaFacturasPendientes.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.RUC = obj.dsc_ruc;
                    frmModif.tipo_documento = obj.tipo_documento;
                    frmModif.serie_documento = obj.serie_documento;
                    frmModif.numero_documento = obj.numero_documento;
                    frmModif.cod_proveedor = obj.cod_proveedor;
                    frmModif.orden_servicio = obj.num_OrdenCompraServ;

                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }
                if (e.Clicks == 2 && (e.Column.FieldName != "flg_PDF" && e.Column.FieldName != "flg_XML"))
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    obj = gvListaFacturasPendientes.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.RUC = obj.dsc_ruc;
                    frmModif.tipo_documento = obj.tipo_documento;
                    frmModif.serie_documento = obj.serie_documento;
                    frmModif.numero_documento = obj.numero_documento;
                    frmModif.cod_proveedor = obj.cod_proveedor;
                    frmModif.orden_servicio = obj.num_OrdenCompraServ;
                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }
                if (e.Clicks == 2 && (e.Column.FieldName == "flg_PDF" || e.Column.FieldName == "flg_XML"))
                {
                    obj = gvListaFacturasPendientes.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    eFacturaProveedor eFact = unit.Factura.ObtenerFacturaProveedor<eFacturaProveedor>(24, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);

                    if (e.Column.FieldName == "flg_PDF" && (eFact.idPDF == null || eFact.idPDF == ""))
                    {
                        MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (e.Column.FieldName == "flg_XML" && (eFact.idXML == null || eFact.idXML == ""))
                    {
                        MessageBox.Show("No se cargado ningún XML", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                    if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                    { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    //var app = App.PublicClientApp;
                    ClientId = eEmp.ClientIdOnedrive;
                    TenantId = eEmp.TenantOnedrive;
                    Appl();
                    var app = PublicClientApp;

                    try
                    {
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                        //eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                        string correo = eEmp.UsuarioOnedrive;
                        string password = eEmp.ClaveOnedrive;

                        var securePassword = new SecureString();
                        foreach (char c in password)
                            securePassword.AppendChar(c);

                        authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                        GraphClient = new Microsoft.Graph.GraphServiceClient(
                        new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                        {
                            requestMessage
                                .Headers
                                .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                            return Task.FromResult(0);
                        }));

                        string IdPDF = eFact.idPDF;
                        string IdXML = eFact.idXML;
                        string IdOneDriveDoc = e.Column.FieldName == "flg_PDF" ? IdPDF : IdXML;
                        string Extension = e.Column.FieldName == "flg_PDF" ? ".pdf" : ".xml";

                        var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                        string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + (eFact.NombreArchivo + Extension);
                        if (!System.IO.File.Exists(ruta))
                        {
                            using (var fileStream = new FileStream(ruta, FileMode.Create, System.IO.FileAccess.Write))
                                fileContent.CopyTo(fileStream);
                        }

                        if (!System.IO.Directory.Exists(unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()))) System.IO.Directory.CreateDirectory(unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()));
                        System.Diagnostics.Process.Start(ruta);
                        SplashScreenManager.CloseForm();
                    }
                    catch (Exception ex)
                    {
                        SplashScreenManager.CloseForm();
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListaFacturasVencidas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaFacturasPendientes_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListaFacturasVencidas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListaFacturasPendientes_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
    }
}
