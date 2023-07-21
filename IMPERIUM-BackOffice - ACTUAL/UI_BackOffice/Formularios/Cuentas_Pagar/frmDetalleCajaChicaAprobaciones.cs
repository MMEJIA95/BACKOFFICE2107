using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_BackOffice;
using BL_BackOffice;
using Microsoft.Identity.Client;
using DevExpress.XtraSplashScreen;
using System.Configuration;
using System.Security;
using System.IO;
using System.Net.Http.Headers;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.XtraEditors;
using Excel = Microsoft.Office.Interop.Excel;
namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmDetalleCajaChicaAprobaciones : HNG_Tools.SimpleModalForm
    {
        private readonly UnitOfWork unit;
        List<eCajaChica.eDetalleMov_CajaChica> listCajaRendida = new List<eCajaChica.eDetalleMov_CajaChica>();
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "";
        public string cod_empresa = "";
        int markWidth = 16;
        Brush RENDIDO = Brushes.Green;
        Brush PENDIENTE = Brushes.Red;
        Brush APERTURA = Brushes.Purple;
        Image ImgPDF = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_16x16.png");
        public string cod_movimientos="";
        public frmDetalleCajaChicaAprobaciones()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurar_formulario();

        }
        public void ObtenerLista_CajaRendida(string cod_caja,string cod_movimiento_rendido)
        {
            listCajaRendida = unit.CajaChica.ListarDatos_CajaChicaAprobaciones<eCajaChica.eDetalleMov_CajaChica>(14, cod_caja, cod_movimiento_rendido);
            bsDetallecaja.DataSource = listCajaRendida; bgvListaCajaRendida.RefreshData();
        }
        private void configurar_formulario()
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListaCajaRendida, bgvListaCajaRendida);

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

        private async void bgvListaCajaRendida_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eCajaChica.eDetalleMov_CajaChica obj = new eCajaChica.eDetalleMov_CajaChica();
                obj = bgvListaCajaRendida.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
                if (obj == null) return;
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                   
                    if (obj.tipo_documento != null && obj.tipo_documento != "TC045")
                    {
                        frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                        frmModif.MiAccion = Factura.Vista;
                        frmModif.RUC = obj.dsc_ruc;
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;

                        frmModif.ShowDialog();
                    }
                    else if (obj.dsc_documento.Substring(0, 2) == "ER")
                    {
                        frmDetalleEntregaRendir frm = new frmDetalleEntregaRendir();
                        frm.MiAccion = DetEntregaRendir.Vista;
                        frm.cod_entregarendir = obj.dsc_documento;
                        frm.cod_empresa = cod_empresa;
                        frm.ShowDialog();
                    }
                    else
                    {
                        frmMantDocumentoInterno frmModif = new frmMantDocumentoInterno();
                        frmModif.MiAccion = DocInterno.Vista;
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;
                        frmModif.cod_empresa = cod_empresa;
                        frmModif.CajaChica = "SI";

                        frmModif.ShowDialog();
                    }
                }
                if (e.Clicks == 1 && e.Column.FieldName == "Sel")
                {
                    if (obj.tipo_documento != null && obj.tipo_documento != "" && obj.tipo_documento != "TC045") obj.Sel = obj.Sel ? false : true;
                    bgvListaCajaRendida.RefreshData();
                }
                if (e.Clicks == 2 && (e.Column.FieldName != "dsc_documento" && e.Column.FieldName != "flg_PDF" && e.Column.FieldName != "flg_XML" && e.Column.FieldName != "Sel"))
                {
                    if (obj == null || obj.dsc_tipo == "APERTURA") return;
                    frmDetalleMovimiento frm = new frmDetalleMovimiento();
                    frm.MiAccion = DetMovimiento.Vista;
                    frm.cod_caja = obj.cod_caja;
                    frm.cod_movimiento = obj.cod_movimiento;
                    frm.cod_empresa = obj.cod_empresa;
                    frm.btnNuevo.Enabled = false;
                    //frm.eMovCaja = obj;
                    frm.ShowDialog();
                    //if (frm.ActualizarListado == "SI") btnBuscar_Click(btnBuscar, new EventArgs());
                }
                if (e.Clicks == 2 && (e.Column.FieldName == "flg_PDF" || e.Column.FieldName == "flg_XML"))
                {
                    obj = bgvListaCajaRendida.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
                    if (obj == null) { return; }

                    eFacturaProveedor eFact = new eFacturaProveedor();
                    if (obj.tipo_documento == "TC045")
                    {
                        eFact = unit.Factura.ObtenerFacturaProveedor<eFacturaProveedor>(49, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);
                    }
                    else
                    {
                        eFact = unit.Factura.ObtenerFacturaProveedor<eFacturaProveedor>(24, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);
                    }

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

                    //else
                    //{
                    eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, cod_empresa);
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
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                        return;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bgvListaCajaRendida_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                //eCajaChica.eDetalleMov_CajaChica obj = bgvListaCajaRendida.GetFocusedRow() as eCajaChica.eDetalleMov_CajaChica;
                //if (obj == null) return;
                if (bgvListaCajaRendida.FocusedColumn != null && bgvListaCajaRendida.FocusedColumn.FieldName != "Sel") e.Cancel = true;
                //if (bgvListaCajaRendida.FocusedColumn.FieldName == "Sel")
                //{
                //    if (obj.tipo_documento != null && obj.tipo_documento != "" && obj.tipo_documento != "TC045") obj.Sel = obj.Sel ? false : true;
                //    bgvListaCajaRendida.RefreshData();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgvListaCajaRendida_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCajaChica.eDetalleMov_CajaChica obj = bgvListaCajaRendida.GetRow(e.RowHandle) as eCajaChica.eDetalleMov_CajaChica;
                    if (obj == null) return;
                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true;
                        e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "abv_estado") e.DisplayText = "";
                    if (e.Column.FieldName == "flg_PDF") e.DisplayText = "";
                    if (e.Column.FieldName == "imp_entregado" && obj.imp_entregado == 0) e.DisplayText = "";
                    if (e.Column.FieldName == "imp_monto" && obj.imp_monto == 0) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_documento" && obj.fch_documento.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (obj.fch_documento.ToString().Contains("1/01/0001")) e.Appearance.FontStyleDelta = FontStyle.Bold;
                    if (obj.cod_rendicion == "MAS DE 1" || obj.cod_rendicion == "SOLO 1") e.Appearance.BackColor = Color.LightGray;
                    if (obj.dsc_tipo == "APERTURA") { e.Appearance.BackColor = Color.FromArgb(221, 235, 247); e.Appearance.ForeColor = Color.DarkGoldenrod; }
                    if (obj.periodo_tributario != null && obj.tipo_documento != null) e.Appearance.ForeColor = Color.Blue;
                    //if (obj.cod_estado_registro == "RVS") { e.Appearance.ForeColor = Color.Purple; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    //if (obj.cod_estado_registro == "REV") { e.Appearance.ForeColor = Color.Green; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    //if (obj.dsc_tipo == "REPOSICION") e.Appearance.BackColor = Color.DarkGray;
                    //if (obj.dsc_tipo != "SALIDA") { e.Appearance.ForeColor = Color.DarkGoldenrod; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    if (obj.dsc_tipo != null && obj.dsc_tipo.Trim() != "")
                    {
                        if (obj.dsc_tipo.Trim().Substring(0, 1) == "R") { e.Appearance.ForeColor = Color.Purple; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                        if (obj.dsc_tipo.Trim().Substring(0, 1) == "D") { e.Appearance.ForeColor = Color.DarkGreen; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    }
                    if (obj.cod_movimiento != null && obj.cod_movimiento.Trim().Substring(0, 2) == "RP") { e.Appearance.BackColor = Color.DarkGray; e.Appearance.ForeColor = Color.DarkBlue; e.Appearance.FontStyleDelta = FontStyle.Bold; }
                    e.DefaultDraw();
                    if (e.Column.FieldName == "abv_estado" && obj.fch_documento.ToString().Contains("1/01/0001") && (obj.dsc_tipo == "SALIDA" || obj.dsc_tipo == "APERTURA"))
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        //if (cellValue == "P") { b = PENDIENTE; } else if (cellValue == "R") { b = RENDIDO; } else { b = APERTURA; }
                        b = APERTURA;
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}