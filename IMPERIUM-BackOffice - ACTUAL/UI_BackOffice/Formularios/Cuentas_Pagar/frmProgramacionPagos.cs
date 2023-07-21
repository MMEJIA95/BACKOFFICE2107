using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using BE_BackOffice;
using BL_BackOffice;
using DevExpress.XtraSplashScreen;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using System.Security;
using System.Net.Http.Headers;
using DevExpress.XtraEditors;
using Microsoft.Identity.Client;
using FileHelpers;
using UI_BackOffice.Formularios.Shared;
using DevExpress.CodeParser;
using Excel = Microsoft.Office.Interop.Excel;
using UI_BackOffice.Tools;
using System.ComponentModel.DataAnnotations;
using DevExpress.Utils.Extensions;
using DevExpress.XtraPrinting.Native.Properties;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmProgramacionPagos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        TaskScheduler scheduler;
        Timer oTimerLoadMtto; 
        List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaProgramacion = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        Image ImgPDF = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_16x16.png");

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "";

        public frmProgramacionPagos()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            oTimerLoadMtto = new Timer();
            oTimerLoadMtto.Interval = 500;
            oTimerLoadMtto.Tick += oTimerLoadMtto_Tick;
        }
        private void oTimerLoadMtto_Tick(object sender, EventArgs e)
        {
            try
            {
                oTimerLoadMtto.Stop();
                HabilitarBotones();
                Inicializar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void frmProgramacionPagos_Load(object sender, EventArgs e)
        {
            //Inicializar();
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            oTimerLoadMtto.Start();
        }

        private void Inicializar()
        {
            CargarLookUpEdit();
            //Fecha
            DateTime date = DateTime.Now;
            //DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            //DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            //dtFechaInicio.EditValue = oPrimerDiaDelMes;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaInicio.EditValue = new DateTime(DateTime.Today.Year, 1, 1);
            dtFechaFin.EditValue = oUltimoDiaDelMes;
            //FECHA PAGO PROGRAMADO, es el viernes proximo de la fecha de vencimiento
            int nDia = Convert.ToInt32(DateTime.Today.DayOfWeek);
            nDia = nDia <= 5 ? 5 - nDia : nDia;
            dtFechaProgramadoAl.EditValue = DateTime.Today.AddDays(nDia);
            chkcbTipoDocumento.CheckAll();

            //Creamos SUPERTOOLTIP
            DevExpress.Utils.SuperToolTip sToolTip = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.SuperToolTipSetupArgs args = new DevExpress.Utils.SuperToolTipSetupArgs();
            Image resImage = DevExpress.Images.ImageResourceCache.Default.GetImage("images/support/index_32x32.png");
            args.Title.Text = "Marcar/Desmarcar";
            args.Contents.Text = "Los importes de los totales se <b>reducen/incrementan</b> al <b>marcar/desmarcar</b> los documentos del listado.";
            args.ShowFooterSeparator = true;
            args.Footer.Text = "Toda acción es según la fecha seleccionada";
            args.Contents.ImageOptions.Image = resImage;
            sToolTip.Setup(args);
            sToolTip.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            picAyuda.SuperTip = sToolTip;

            BuscarFacturas();
        }
        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.Sesion.Global.Solucion);

            if (listPermisos.Count > 0)
            {
                //grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                //grupoAcciones.Enabled = listPermisos[0].flg_escritura;
                btnAgregarProgramacion.Enabled = listPermisos[0].flg_escritura;
                btnEjecutarPago.Enabled = listPermisos[0].flg_escritura;
                btnPagarBanco.Enabled = listPermisos[0].flg_escritura;
            }
            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
            eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 4);
            //btnAgregarProgramacion.Enabled = oPerfil != null ? true : false;
            btnEjecutarPago.Enabled = oPerfil != null ? true : false;
            btnPagarBanco.Enabled = oPerfil != null ? true : false;
            //bgvProgramacionPagos.Columns["bandedGridColumn1"].OptionsColumn.AllowEdit = oPerfil != null ? true : false;
        }
        private void CargarLookUpEdit()
        {
            try
            {
                unit.Factura.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
                unit.Factura.CargaCombosChecked("TipoDocumento", chkcbTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "");
                unit.Factura.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);

                rlkpTipoDocumento.DataSource = unit.Factura.CombosEnGridControl<eFacturaProveedor>("TipoDocumento");
                //rlkpDocumento.DataSource = unit.Factura.CombosEnGridControl<eFacturaProveedor>("Documento"/*, tipo_documento: obj.tipo_documento*/);
                rlkpEstado.DataSource = unit.Factura.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("EstadoProgramacion");
                rlkpPagar_A.DataSource = unit.Factura.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("Pagar_A");
                rlkpFormaPago.DataSource = unit.Factura.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("FormaPago");
                rlkpTipoProgramacion.DataSource = unit.Factura.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("Tipoprogramacion");
                List<eFacturaProveedor> list = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bgvProgramacionPagos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void bgvProgramacionPagos_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void bgvProgramacionPagos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                unit.Globales.Pintar_EstiloGrilla(sender, e);
                GridView view = sender as GridView;
                if (view.Columns["cod_estado"] != null || view.Columns["dsc_estado_documento"] != null)
                {
                    //decimal saldo = Convert.ToDecimal(view.GetRowCellDisplayText(e.RowHandle, view.Columns["imp_saldo"]));
                    //if (saldo == 0) e.Appearance.ForeColor = Color.Blue;
                    //string estadoP = view.GetRowCellDisplayText(e.RowHandle, view.Columns["cod_estado"]);
                    //if (estadoP == "EJECUTADO") e.Appearance.ForeColor = Color.Blue;
                    string estado = view.GetRowCellDisplayText(e.RowHandle, view.Columns["dsc_estado_documento"]);
                    if (estado == "Anulado") e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void bgvProgramacionPagos_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            if (obj == null) return;
            obj.cod_estado = "PRO";
        }

        private void bgvProgramacionPagos_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (bgvProgramacionPagos.FocusedColumn.FieldName == "Sel") return;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(e.RowHandle) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            if (obj == null) return;
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            BuscarFacturas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
            BuscarFacturas();
            SplashScreenManager.CloseForm();
        }
        public void BuscarFacturas()
        {
            try
            {
                listaProgramacion = unit.Factura.FiltroFactura<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(23, lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString(),
                                                                                        chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(),
                                                                                        "",
                                                                                        "",
                                                                                        lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(),
                                                                                        Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                        Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                                                                                        SinSaldo: Convert.ToInt32(grdbFiltroSaldo.SelectedIndex));
                bsProgramacionPagos.DataSource = listaProgramacion;
                rlkpBancoEmpresa.DataSource = unit.Factura.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("BancoEmpresa", cod_empresa: lkpEmpresa.EditValue.ToString());

                CalcularTOTALES();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CalcularTOTALES()
        {
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listMontoSOLES = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listMontoDOLARES = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listMontoRestaSOLES = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listMontoRestaDOLARES = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            listMontoSOLES = listaProgramacion.FindAll(x => x.fch_pago.ToShortDateString() == Convert.ToDateTime(dtFechaProgramadoAl.EditValue).ToShortDateString() && x.cod_moneda == "SOL" && x.cod_estado == "PRO");
            listMontoDOLARES = listaProgramacion.FindAll(x => x.fch_pago.ToShortDateString() == Convert.ToDateTime(dtFechaProgramadoAl.EditValue).ToShortDateString() && x.cod_moneda == "DOL" && x.cod_estado == "PRO");
            listMontoRestaSOLES = listaProgramacion.FindAll(x => x.fch_pago.ToShortDateString() == Convert.ToDateTime(dtFechaProgramadoAl.EditValue).ToShortDateString() && x.cod_moneda == "SOL" && x.cod_estado == "PRO" && x.Sel);
            listMontoRestaDOLARES = listaProgramacion.FindAll(x => x.fch_pago.ToShortDateString() == Convert.ToDateTime(dtFechaProgramadoAl.EditValue).ToShortDateString() && x.cod_moneda == "DOL" && x.cod_estado == "PRO" && x.Sel);

            decimal MontoSOLES = (from tabla in listMontoSOLES
                                  select tabla.imp_pago).Sum();
            decimal MontoDOLARES = (from tabla in listMontoDOLARES
                                    select tabla.imp_pago).Sum();
            decimal MontoRestaSOLES = (from tabla in listMontoRestaSOLES
                                       select tabla.imp_pago).Sum();
            decimal MontoRestaDOLARES = (from tabla in listMontoRestaDOLARES
                                         select tabla.imp_pago).Sum();
            txtMontoSOLES.EditValue = MontoSOLES - MontoRestaSOLES; txtMontoDOLARES.EditValue = MontoDOLARES - MontoRestaDOLARES;
        }

        private void rlkpEstado_EditValueChanged(object sender, EventArgs e)
        {
            bgvProgramacionPagos.PostEditor();
        }

        private void frmProgramacionPagos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
                BuscarFacturas();
                SplashScreenManager.CloseForm();
            }
        }
        
        private void rlkpDocumento_EditValueChanged(object sender, EventArgs e)
        {
            bgvProgramacionPagos.PostEditor();
        }

        private void rlkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            bgvProgramacionPagos.PostEditor();
        }
        
        private void rbtnEliminarAgregarProgramacion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            decimal monto_restante = 0.00m;
            decimal total_pago = 0.00m;
            decimal imp_total = 0.00m;
            decimal imp_ultimo = 0.00m; 
            GridView view = bgvProgramacionPagos;
            int index = view.FocusedRowHandle;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            if (obj == null) return;

            switch (e.Button.Caption)
            {
                    case "Agregar":
                        if (obj.imp_saldo == 0) return;
                        obj.num_linea = 0; obj.fch_pago = obj.fch_pago_programado; obj.dsc_observacion = null; obj.cod_estado = "PRO"; obj.cod_pagar_a = "PROV";
                        obj.fch_ejecucion = new DateTime(); obj.cod_usuario_ejecucion = null; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        obj.cod_tipo_prog = "REGULAR"; obj.cod_moneda_prog = "";
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "Eliminar":
                    List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos val = unit.Factura.Obtener_ProgramacionesPagos< eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(1,tipo_documento:obj.tipo_documento,serie_documento:obj.serie_documento,
                                                                             numero_documento: obj.numero_documento,cod_proveedor:obj.cod_proveedor);

                    eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 5 || x.cod_perfil == 49);
                    if (obj.cod_estado == "EJE" && oPerfil == null) { MessageBox.Show("No se puede eliminar una programación ya ejecutada.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    if (val.cant_lineasprog > 1)
                    {
                        //List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listado = unit.Factura.ListaDetalleProgramacion<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(2, tipo_documento: obj.tipo_documento, serie_documento: obj.serie_documento,
                        //                     numero_documento: obj.numero_documento, cod_proveedor: obj.cod_proveedor);
                        //int ctd_lineas = 0;
                        // ctd_lineas = listado.FindAll(x => x.cod_estado == "EJE").Count;
                        //if ((listado.Count - ctd_lineas) == ctd_lineas) { MessageBox.Show("La programación no puede estar sin programaciones si no se pago en su totalidad.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                        if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string result = unit.Factura.EliminarDatosFactura(3, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor, num_linea: obj.num_linea);
                            if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listado = unit.Factura.ListaDetalleProgramacion<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(2, tipo_documento: obj.tipo_documento, serie_documento: obj.serie_documento,
                                             numero_documento: obj.numero_documento, cod_proveedor: obj.cod_proveedor);
                            bgvProgramacionPagos.DeleteRow(index);
                            bgvProgramacionPagos.PostEditor(); imp_total = 0;
                            
                            var ultimoitem = listado.Last();
                            if (listado.Count == 0 || listado.Count <= 1)
                            {
                                imp_ultimo = obj.imp_total;
                            }
                            else
                            {
                                monto_restante = total_pago + ultimoitem.imp_pago;
                                obj.imp_pago = obj.imp_pago + monto_restante;
                                imp_ultimo =  obj.imp_pago;
                            }

                            obj.imp_pago = imp_ultimo;
                            obj.cod_estado = "PRO";
                            obj.num_linea = ultimoitem.cod_estado == "EJE" ? 0 : ultimoitem.num_linea;
                            eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if(val.cant_lineasprog==0 || val.cant_lineasprog <= 1)
                    {
                        obj.imp_pago = obj.imp_total;
                        HNG.MessageWarning("La Programación de pago debe tener un detalle", "ELIMINAR PROGRAMACION");
      
                        List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listado2 = unit.Factura.ListaDetalleProgramacion<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(2, tipo_documento: obj.tipo_documento, serie_documento: obj.serie_documento,
                                                                     numero_documento: obj.numero_documento, cod_proveedor: obj.cod_proveedor);


                        if (listado2.Count == 0 || val.cant_lineasprog <= 1) { obj.imp_pago = obj.imp_total; }
                        else
                        {

                            var ultimoitem = listado2.Last();

                            foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos item in listado2)
                            {
                                monto_restante = total_pago + item.imp_pago;
                                obj.imp_pago = obj.imp_pago + monto_restante;
                            }
                            total_pago = obj.imp_total - obj.imp_pago;


                            obj.imp_pago = total_pago*-1;
                        }
                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        if (eProgFact == null) HNG.MessageError("Error al grabar programación de pago.", "ERROR");
                        bgvProgramacionPagos.RefreshData();
                        return;

                        //obj.num_linea = 0; obj.fch_pago = obj.fch_pago_programado; obj.dsc_observacion = null; obj.cod_estado = "PRO"; obj.cod_pagar_a = "PROV";
                        //obj.fch_ejecucion = new DateTime(); obj.cod_usuario_ejecucion = null; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        //obj.cod_tipo_prog = "REGULAR";
                        //eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        //if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //bgvProgramacionPagos.RefreshData();
                        //return;

                    }

                    break;
            }
            BuscarFacturas();
            view.FocusedRowHandle = index;
            bgvProgramacionPagos.RefreshData();
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
        private async void bgvProgramacionPagos_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                if (objProg.cod_tipo_prog == "CAJACHICA" || objProg.cod_tipo_prog == "ENTREGARENDIR") return;
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    if (Application.OpenForms["frmMantFacturaProveedor"] != null)
                    {
                        Application.OpenForms["frmMantFacturaProveedor"].Activate();
                    }
                    else
                    {
                        frmModif.MiAccion = Factura.Vista;
                        frmModif.RUC = obj.dsc_ruc;
                        frmModif.tipo_documento = obj.tipo_documento;
                        frmModif.serie_documento = obj.serie_documento;
                        frmModif.numero_documento = obj.numero_documento;
                        frmModif.cod_proveedor = obj.cod_proveedor;
                        frmModif.habilitar_control = "SI";
                        frmModif.ShowDialog();
                    }
                }
                if (e.Clicks == 1 && e.Column.FieldName == "Sel" && objProg.cod_estado != "EJE")
                {
                    objProg.Sel = objProg.Sel ? false : true;
                    bgvProgramacionPagos.RefreshData();
                    CalcularTOTALES();
                }
                if (e.Clicks == 2 && e.Column.FieldName == "flg_PDF")
                {
                    obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    eFacturaProveedor eFact = unit.Factura.ObtenerFacturaProveedor<eFacturaProveedor>(24, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor);
                    if (eFact.idPDF == null || eFact.idPDF == "")
                    {
                        MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
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
                            string IdOneDriveDoc = IdPDF;

                            var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                            string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + eFact.NombreArchivo + ".pdf";
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgvProgramacionPagos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Sel") return;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            if (obj == null) return;
            if (e.Column.FieldName == "cod_estado" && obj.cod_estado == "EJE")
            {
                if (MessageBox.Show("¿Esta seguro de ejecutar el pago?", "Ejecutar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.dsc_usuario_ejecucion = Program.Sesion.Usuario.dsc_usuario; //obj.fch_ejecucion = DateTime.Today;
                }
                else
                {
                    obj.cod_estado = "PRO";
                }
            }

            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (e.Column.FieldName == "cod_estado" && obj.cod_estado == "EJE")
            {
                int nRow = bgvProgramacionPagos.FocusedRowHandle;
                BuscarFacturas();
                bgvProgramacionPagos.FocusedRowHandle = nRow;
            }
            else
            {
                CalcularTOTALES();
            }
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos val = unit.Factura.Obtener_ProgramacionesPagos<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(1, tipo_documento: obj.tipo_documento, serie_documento: obj.serie_documento,
                                                                             numero_documento: obj.numero_documento, cod_proveedor: obj.cod_proveedor);
            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listado = unit.Factura.ListaDetalleProgramacion<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(2, tipo_documento: obj.tipo_documento, serie_documento: obj.serie_documento,
                                                                             numero_documento: obj.numero_documento, cod_proveedor: obj.cod_proveedor);


            if (e.Column.FieldName == "imp_pago")
            {
                decimal pago = 0.00m;
                decimal total_pago = 0.00m;
                decimal x = obj.imp_pago;
                decimal monto_restante = 0.00m;

           

                if (obj.imp_pago == 0 )
                {

                    decimal imp_ultimo = 0.00m;
                    HNG.MessageError("El importe de pago no puede ser igual a 0", "ERROR");
                   // string result = unit.Factura.EliminarDatosFactura(3, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor, num_linea: obj.num_linea);
                    List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listado2 = unit.Factura.ListaDetalleProgramacion<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(2, tipo_documento: obj.tipo_documento, serie_documento: obj.serie_documento,
                                                                 numero_documento: obj.numero_documento, cod_proveedor: obj.cod_proveedor);
                    //foreach (var item in listado2)
                    //{
                    //    pago = item.imp_pago;
                    //    total_pago = pago + total_pago;
                    //}

                    //monto_restante = total_pago - obj.imp_total;
                    //obj.imp_pago = obj.imp_pago - monto_restante;

                    if (listado2.Count == 0) { obj.imp_pago = obj.imp_total; }
                    else
                    {

                        var ultimoitem = listado2.Last();

                        foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos item in listado2)
                        {
                            monto_restante = total_pago + item.imp_pago;
                            obj.imp_pago = obj.imp_pago + monto_restante;
                        }
                        total_pago = obj.imp_total - obj.imp_pago;
                        //imp_ultimo = ultimoitem.imp_pago + total_pago;


                        obj.imp_pago = total_pago;
                        //obj.num_linea = ultimoitem.num_linea;
                    }
                    eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                    if (eProgFact == null) HNG.MessageError("Error al grabar programación de pago.", "ERROR");

                    return;
                }
                else if (obj.imp_pago < 0 && obj.tipo_documento != "TC006")
                {
                    obj.imp_pago = 0;
                    HNG.MessageError("El importe de pago no puede ser menor o igual a 0", "ERROR");
                    List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listado2 = unit.Factura.ListaDetalleProgramacion<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(2, tipo_documento: obj.tipo_documento, serie_documento: obj.serie_documento,
                                                                 numero_documento: obj.numero_documento, cod_proveedor: obj.cod_proveedor);
                    if (listado2.Count <=1) { obj.imp_pago = obj.imp_total; }
                    else
                    {
                        var ultimoitem = listado2.Last();

                        foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos item in listado2)
                        {
                            if (item.imp_pago < 0) { item.imp_pago = 0; }
                            monto_restante = total_pago + item.imp_pago;
                            obj.imp_pago = obj.imp_pago + monto_restante;
                        }
                        total_pago = obj.imp_total - obj.imp_pago;
                        //imp_ultimo = ultimoitem.imp_pago + total_pago;


                        obj.imp_pago = total_pago;
                        //obj.num_linea = ultimoitem.num_linea;
                    }
                    eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                    if (eProgFact == null) HNG.MessageError("Error al grabar programación de pago.", "ERROR");

                    return;
                }


                if (val.cant_lineasprog <= 1)
                {
                    if (x < obj.imp_total)
                    {
                        if (obj.imp_saldo == 0) return;
                        obj.num_linea = 0; obj.fch_pago = obj.cod_tipo_prog == "CAJACHICA" || obj.cod_tipo_prog == "ENTREGARENDIR" ? obj.fch_pago : obj.fch_pago_programado; 
                        obj.dsc_observacion = null; obj.cod_estado = "PRO"; obj.cod_pagar_a = "PROV"; obj.cod_moneda_prog = "";
                        obj.fch_ejecucion = new DateTime(); obj.cod_usuario_ejecucion = null; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        obj.cod_tipo_prog = obj.cod_tipo_prog == "CAJACHICA" || obj.cod_tipo_prog == "ENTREGARENDIR" ? obj.cod_tipo_prog : "REGULAR"; 
                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        if (eProgFact == null) HNG.MessageError("Error al grabar programación de pago.", "ERROR");

                       // obj.imp_pago = obj.imp_total;
                        return;
                    }
                    else {
                        HNG.MessageError("El monto ingresado excede el monto total a pagar", "ERROR");
                        obj.imp_pago = obj.imp_total;
                    }
                }else if (val.cant_lineasprog > 1)
                {
                    
                    foreach (var item in listado)
                    {
                        pago = item.imp_pago;
                        total_pago = pago + total_pago;
                    }
                    if (total_pago < obj.imp_total) {
                        if (obj.imp_saldo == 0) return;
                        obj.num_linea = 0; obj.fch_pago = obj.cod_tipo_prog == "CAJACHICA" || obj.cod_tipo_prog == "ENTREGARENDIR" ? obj.fch_pago : obj.fch_pago_programado;
                        obj.dsc_observacion = null; obj.cod_estado = "PRO"; obj.cod_pagar_a = "PROV"; obj.cod_moneda_prog = "";
                        obj.fch_ejecucion = new DateTime(); obj.cod_usuario_ejecucion = null; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        obj.cod_tipo_prog = obj.cod_tipo_prog == "CAJACHICA" || obj.cod_tipo_prog == "ENTREGARENDIR" ? obj.cod_tipo_prog : "REGULAR";
                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        if (eProgFact == null) HNG.MessageError("Error al grabar programación de pago.", "ERROR");

                        return; }
                    else if(total_pago>obj.imp_pago)
                    {
                        HNG.MessageError("El monto ingresado excede el monto total a pagar", "ERROR");
                        monto_restante = total_pago - obj.imp_total;
                        obj.imp_pago =  obj.imp_pago - monto_restante;
                    }
                    
                }
                bgvProgramacionPagos.RefreshData();
            }
            bgvProgramacionPagos.RefreshData();
        }
        private void bgvProgramacionPagos_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                if (obj == null) return;
                //if ((bgvProgramacionPagos.FocusedColumn.FieldName == "tipo_documento" || bgvProgramacionPagos.FocusedColumn.FieldName == "cod_documento") && 
                //    obj.flg_guardado == "SI")
                //{
                //    e.Cancel = true;
                //}
                List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
                eVentana oPerfil = listPerfil.Find(x => x.cod_perfil == 5);
                if (obj.cod_estado == "EJE" && obj.imp_saldo == 0 && bgvProgramacionPagos.FocusedColumn.FieldName != "bandedGridColumn1")
                {
                    e.Cancel = oPerfil == null ? true : false;
                }
                if (obj.cod_estado == "EJE" && bgvProgramacionPagos.FocusedColumn.FieldName != "bandedGridColumn1")
                {
                    e.Cancel = oPerfil == null ? true : false;
                }
                //if (bgvProgramacionPagos.FocusedColumn.FieldName == "colBuscarCtaProveedor") e.Cancel = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgvProgramacionPagos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(e.RowHandle) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (e.Column.FieldName == "fch_vencimiento" && obj.fch_vencimiento < DateTime.Today && obj.imp_saldo != 0) e.Appearance.BackColor = Color.LightSalmon;
                    if (e.Column.FieldName == "fch_pago" && obj.fch_pago.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_ejecucion" && obj.fch_ejecucion.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "CantCuentas" && obj.CantCuentas == "NO") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (obj.cod_estado == "EJE") e.Appearance.ForeColor = Color.Blue;
                    if (e.Column.FieldName == "flg_PDF" && obj.flg_PDF == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if ((e.Column.FieldName == "flg_PDF" && obj.cod_estado_pago != "SI") || (e.Column.FieldName == "flg_XML" && obj.cod_estado_pago != "SI"))
                    {
                        e.DisplayText = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregarProgramacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
            //List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
            //lista = listaProgramacion.FindAll(x => x.Sel);
            if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            //if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            
            frmOpcionesProgMasiva frm = new frmOpcionesProgMasiva();
            frm.ShowDialog();
            if (frm.Actualizar == "OK")
            {
                //foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in lista)
                foreach (int nRow in bgvProgramacionPagos.GetSelectedRows())
                {
                    if (nRow < 0) continue;
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(nRow) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (obj == null) continue;
                    if (obj.imp_saldo == 0) continue;
                    obj.num_linea = 0; obj.fch_pago = frm.fch_pago; obj.dsc_observacion = frm.dsc_observacion; obj.cod_estado = "PRO"; obj.cod_pagar_a = frm.cod_pagar_a;
                    obj.fch_ejecucion = new DateTime(); obj.cod_usuario_ejecucion = null; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario; obj.cod_tipo_prog = "REGULAR";
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                    eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                    if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                BuscarFacturas();
            }
        }

        private void chkMarcarTodos_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (bgvProgramacionPagos.RowCount == 0) { MessageBox.Show("Debe haber al menos 1 programación", "Marcar todos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); chkMarcarTodos.CheckState = CheckState.Unchecked; return; }
                for (int x = 0; x <= bgvProgramacionPagos.RowCount - 1; x++)
                {
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (obj != null && obj.cod_estado == "PRO")
                    {
                        if (chkMarcarTodos.CheckState == CheckState.Checked)
                        {
                            obj.Sel = true;
                        }
                        else if (chkMarcarTodos.CheckState == CheckState.Unchecked)
                        {
                            obj.Sel = false;
                        }
                    }
                }
                bgvProgramacionPagos.RefreshData();
                CalcularTOTALES();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAplazarPagoProgramado_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
                //List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                //lista = listaProgramacion.FindAll(x => x.Sel);
                //if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Aplazar Pago Programado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                if (MessageBox.Show("¿Esta seguro de aplazar 1 semana las programaciones de pagos seleccionadas?", "Aplazar Pago Programado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in lista)
                    foreach (int nRow in bgvProgramacionPagos.GetSelectedRows())
                    {
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
                        if (nRow < 0) continue;
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(nRow) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                        if (obj == null) continue;
                        if (bgvProgramacionPagos.SelectedRowsCount == 1 && obj.cod_estado == "EJE") { MessageBox.Show("El pago ya esta ejecutado", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                        if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                        obj.fch_pago = obj.fch_pago.AddDays(7);
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    BuscarFacturas();
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void chkSel_CheckStateChanged(object sender, EventArgs e)
        {
            //bgvProgramacionPagos.PostEditor();
        }

        private void rbtnEliminarAgregarProgramacion_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void dtFechaProgramadoAl_EditValueChanged(object sender, EventArgs e)
        {
            if (dtFechaProgramadoAl.EditValue != null) CalcularTOTALES();
        }

        private void rlkpBancoEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                obj.num_linea_banco = 0; bgvProgramacionPagos.PostEditor(); bgvProgramacionPagos.RefreshData();
            }
        }

        private void btnEjecutarPago_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                lista = listaProgramacion.FindAll(x => x.Sel);

                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDet = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                //listaDet = listaProgramacion.FindAll(x => x.Sel && (x.dsc_observacion == "DETRACCIÓN" || x.dsc_observacion == "RET 4TA"));
                //listaDet = listaProgramacion.FindAll(x => x.Sel && x.cod_tipo_prog == "DETRACC");

                if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                if (lista.Count == 1 && lista[0].num_linea == 0) { MessageBox.Show("No hay una programación de pago registrada", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (lista.Count == 1 && lista[0].cod_estado == "EJE") { MessageBox.Show("El pago ya esta ejecutado", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingrese la fecha de pago ejecutado";
                DateEdit dtFecha = new DateEdit(); dtFecha.Width = 100; args.DefaultResponse = DateTime.Today; args.Editor = dtFecha;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);

                if ((res == DialogResult.OK || res == DialogResult.Yes) && dtFecha.EditValue != null)
                {
                    decimal imp_monto = 0, imp_montoDET = 0; string cod_bloque_pago = "";
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
                    foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in lista)
                    //foreach (int nRow in bgvProgramacionPagos.GetSelectedRows())
                    {
                        //if (nRow < 0) continue;
                        //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(nRow) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                        if (obj == null) continue;
                        if (bgvProgramacionPagos.SelectedRowsCount == 1 && obj.num_linea == 0) { MessageBox.Show("No hay una programación de pago registrada", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                        if (bgvProgramacionPagos.SelectedRowsCount == 1 && obj.cod_estado == "EJE") { MessageBox.Show("El pago ya esta ejecutado", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                        if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                        obj.cod_estado = "EJE"; obj.fch_ejecucion = Convert.ToDateTime(dtFecha.EditValue);
                        obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        //eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (obj.cod_tipo_prog == "DETRACC") listaDet.Add(obj);
                        cod_bloque_pago = eProgFact.cod_bloque_pago;
                        imp_monto = imp_monto + (obj.cod_tipo_prog != "DETRACC" ? obj.imp_total : 0);
                        imp_montoDET = imp_montoDET + (obj.cod_tipo_prog == "DETRACC" ? obj.imp_total : 0);
                    }
                    if (listaDet.Count > 0)
                    {
                        frmFacturasConstanciaDetraccRetenc frmDetRet = new frmFacturasConstanciaDetraccRetenc();
                        frmDetRet.listFacturas = listaDet;
                        frmDetRet.ShowDialog();
                    }
                    if (imp_monto > 0)
                    {
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objP = lista[0];
                        eEmpresa.eDetalleMovimientoBanco_Empresa objB = new eEmpresa.eDetalleMovimientoBanco_Empresa();
                        objB.cod_empresa = objP.cod_empresa; objB.num_linea = objP.num_linea; objB.num_item = 0;
                        objB.fch_ejecutada = DateTime.Today; objB.fch_efectiva = DateTime.Today; objB.cod_tipo_movimiento = "SALIDA";
                        objB.cod_origen_movimiento = "001"; objB.imp_monto = imp_monto; objB.cod_bloque_pago = cod_bloque_pago;
                        objB.flg_identificado = "SI"; objB.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        eEmpresa.eDetalleMovimientoBanco_Empresa objPago = unit.Factura.Insertar_Actualizar_DetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(objB);

                    }
                    BuscarFacturas();
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPagarBanco_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDet = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                lista = listaProgramacion.FindAll(x => x.Sel && x.cod_estado != "EJE");
                if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (lista.Count == 1 && lista[0].num_linea == 0) { MessageBox.Show("No hay una programación de pago registrada", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                //if (lista.Count == 1 && lista[0].cod_estado == "EJE") { MessageBox.Show("El pago ya esta ejecutado", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                //if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (lista.FindAll(x => x.num_linea_banco == 0).Count > 0) { MessageBox.Show("Hay registros sin asignar el BANCO ORIGEN.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (lista.FindAll(x => x.num_linea_banco_prov == 0).Count > 0) { MessageBox.Show("Hay registros sin asignar el BANCO DESTINO.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (lista.Count == lista.FindAll(x => x.cod_estado == "EJE").Count) { MessageBox.Show("Todos los pago ya esta ejecutados", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eObj in lista)
                {
                    eObj.dsc_proveedor = eObj.dsc_proveedor.Replace("Ñ", "N").Replace("ñ", "n");
                }

                frmFacturaPagoBanco frmFact = new frmFacturaPagoBanco();
                frmFact.Text = "Listado de Factura Pago Banco";
                frmFact.listDocumentos = lista;
                //new ToolHelper.Forms().ShowDialog(frmFact);
                frmFact.ShowDialog();
                if (frmFact.listDocumentos.Count > 0 && frmFact.GuardarDatos == "SI")
                {

                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
                    //////////LIMPIA LA CARPETA DONDE SE EXPORTA EL TXT DE PAGOS
                    if (!Directory.Exists("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos")) Directory.CreateDirectory("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos");
                    DirectoryInfo source = new DirectoryInfo("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos");
                    FileInfo[] filesToCopy = source.GetFiles();
                    foreach (FileInfo oFile in filesToCopy)
                    {
                        oFile.Delete();
                    }
                    foreach (string oCarpeta in Directory.GetDirectories("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos"))
                    {
                        Directory.Delete(oCarpeta, true);
                    }

                    //////////TRAE LISTA DE LAS CUENTAS BANCARIAS DE LAS FACTURAS
                    //var listBancos = lista.Select(x => x.num_linea_banco).Distinct();
                    var listBancos = lista.Select(x => x.num_linea_banco).Distinct().ToList();
                    decimal imp_montoPago = 0; Double suma_cabecera1 = 0, suma_cabecera2 = 0; string cod_bloque_pago = "", nuevo_bloque_pago = "SI";
                    string tipo_cuenta, nro_cta_abono, tipo_doc, moneda, nro_cta_cargo, sum_cabecera, espacios = " ", msgError = "";
                    if (lista.Count() != lista.FindAll(x => x.cod_tipo_prog == "DETRACC").Count())
                    {
                        foreach (int banco in listBancos)
                        {
                            //////////FILTRA A LOS DOCUMENTOS POR CADA BANCO
                            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDoc = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                            listaDoc = lista.FindAll(x => x.num_linea_banco == banco && x.imp_saldo > 0 && x.num_linea > 0);

                            if (listaDoc[0].cod_banco_empresa == "BA006")//SCOTIABANK
                            {
                                //////////FILTRA A LOS DOCUMENTOS PARA SCOTIABANK RXH
                                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDocRXH = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                                listaDocRXH = listaDoc.FindAll(x => x.tipo_documento == "TC008");
                                if (listaDocRXH.Count > 0)
                                {
                                    /*cod_bloque_pago = "";*/ /*nuevo_bloque_pago = "SI";*/
                                    //////////GENERA EL TXT DE PAGOS
                                    StreamWriter sw = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\SCOTIABANK - FORMATO VARIOS - PAGO RXH.txt");
                                    foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDocRXH)
                                    {
                                        msgError = obj.cod_banco_prov == "BA006" && (obj.dsc_cta_bancaria_prov == null || obj.dsc_cta_bancaria_prov.Trim() == "") ? obj.dsc_proveedor + " No tiene Cuenta Bancaria." : "";
                                        msgError = obj.cod_banco_prov != "BA006" && (obj.dsc_cta_interbancaria_prov == null || obj.dsc_cta_interbancaria_prov.Trim() == "") ? obj.dsc_proveedor + " No tiene CCI." : "";
                                        if (msgError != "") { SplashScreenManager.CloseForm(); HNG.MessageError(msgError, "ERROR"); return; }
                                        if (obj == null) continue;
                                        //if (obj.cod_tipo_prog == "DETRACC") { listaDet.Add(obj); continue; }
                                        if (obj.cod_tipo_prog == "DETRACC") continue;

                                        tipo_cuenta = obj.cod_banco_prov != "BA006" ? "4" : obj.cod_tipo_cuenta_prov == "02" ? "3" : obj.cod_tipo_cuenta_prov == "01" ? "2" : "";
                                        nro_cta_abono = obj.cod_banco_prov != "BA006" ? obj.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "") : obj.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace(".", "");
                                        sw.WriteLine(obj.dsc_ruc.Substring(2, 8) +
                                                    (obj.dsc_proveedor.Length >= 60 ? obj.dsc_proveedor.Substring(0, 60) : obj.dsc_proveedor).PadRight(60, ' ') +
                                                    ("RH." + obj.serie_documento + "-" + obj.numero_documento.ToString()).PadRight(20, ' ') +
                                                    obj.fch_documento.ToString("yyyyMMdd") +
                                                    Math.Round(obj.imp_pago, 2).ToString().Replace(".", "").PadLeft(11, '0') + tipo_cuenta +
                                                    (tipo_cuenta == "2" || tipo_cuenta == "3" ? nro_cta_abono : espacios.PadRight(26)) +
                                                    (tipo_cuenta == "4" ? nro_cta_abono : espacios.PadRight(36)));
                                    }
                                    sw.Close();
                                }

                                //////////FILTRA A LOS DOCUMENTOS PARA SCOTIABANK DIFERENTES A RXH
                                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDocSinRXH = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                                listaDocSinRXH = listaDoc.FindAll(x => x.tipo_documento != "TC008");
                                if (listaDocSinRXH.Count > 0)
                                {
                                    /*cod_bloque_pago = "";*/ /*nuevo_bloque_pago = "SI";*/
                                    //////////GENERA DETALLE DE TXT DE PAGOS
                                    StreamWriter sw2 = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\SCOTIABANK - FORMATO PROVEEDORES.txt");
                                    foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDocSinRXH)
                                    {
                                        msgError = obj.cod_banco_prov == "BA006" && (obj.dsc_cta_bancaria_prov == null || obj.dsc_cta_bancaria_prov.Trim() == "") ? obj.dsc_proveedor + " No tiene Cuenta Bancaria." : "";
                                        msgError = obj.cod_banco_prov != "BA006" && (obj.dsc_cta_interbancaria_prov == null || obj.dsc_cta_interbancaria_prov.Trim() == "") ? obj.dsc_proveedor + " No tiene CCI." : "";
                                        msgError = obj.cod_banco_prov == "BA006" && obj.dsc_cta_bancaria_prov.Length < 10 ? obj.dsc_proveedor + " Tiene Cuenta Bancaria menor a 10 dígitos." : "";
                                        if (msgError != "") { SplashScreenManager.CloseForm(); HNG.MessageError(msgError, "ERROR"); return; }
                                        if (obj == null) continue;
                                        //if (obj.cod_tipo_prog == "DETRACC") { listaDet.Add(obj); continue; }
                                        if (obj.cod_tipo_prog == "DETRACC") continue;

                                        tipo_cuenta = obj.cod_banco_prov != "BA006" ? "4" : obj.cod_tipo_cuenta_prov == "02" ? "3" : obj.cod_tipo_cuenta_prov == "01" ? "2" : "";
                                        nro_cta_abono = obj.cod_banco_prov != "BA006" ? obj.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "") : obj.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace(".", "");

                                        sw2.WriteLine(obj.dsc_ruc.PadRight(11, '0') +
                                                    (obj.dsc_proveedor.Length >= 60 ? obj.dsc_proveedor.Substring(0, 60) : obj.dsc_proveedor).PadRight(60, ' ') +
                                                    (obj.serie_documento + "-" + obj.numero_documento.ToString().PadLeft(8, '0')).PadRight(14, ' ') +
                                                    obj.fch_documento.ToString("yyyyMMdd") +
                                                    Math.Round(obj.imp_pago, 2).ToString().Replace(".", "").PadLeft(11, '0') + tipo_cuenta +
                                                    (tipo_cuenta == "2" || tipo_cuenta == "3" ? nro_cta_abono.Substring(0, 3) : espacios.PadRight(3)) +
                                                    (tipo_cuenta == "2" || tipo_cuenta == "3" ? nro_cta_abono.Substring(3, 7) : espacios.PadRight(7)) +
                                                    (tipo_cuenta == "1" ? "*" : " ") + espacios.PadRight(30) +
                                                    (tipo_cuenta == "4" ? nro_cta_abono.PadLeft(20, '0') : espacios.PadRight(20)) +
                                                    " " + espacios.PadRight(10) + " ");
                                    }
                                    sw2.Close();
                                }
                            }

                            if (listaDoc[0].cod_banco_empresa == "BA001")//BCP
                            {
                                /*cod_bloque_pago = ""; *//*nuevo_bloque_pago = "SI";*/
                                //////////GENERA EL TXT DE PAGOS
                                StreamWriter sw3 = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\BCP - FORMATO PROVEEDORES.txt");
                                moneda = listaDoc[0].cod_moneda == "SOL" ? "0001" : listaDoc[0].cod_moneda == "DOL" ? "1001" : "";
                                nro_cta_cargo = listaDoc[0].dsc_cta_bancaria_empresa.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "");
                                imp_montoPago = listaDoc.Select(y => Math.Round(y.imp_pago, 2)).Sum();
                                suma_cabecera1 = Convert.ToDouble(nro_cta_cargo.Substring(3, nro_cta_cargo.Length - 3));
                                suma_cabecera2 = listaDoc.Where(x => x.cod_banco_prov != "BA001").
                                                Select(y => y.dsc_cta_interbancaria_prov == "" ? 0 : Convert.ToDouble(y.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "").
                                                Substring(10, y.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "").Length - 10))).Sum();
                                suma_cabecera2 = suma_cabecera2 + listaDoc.Where(x => x.cod_banco_prov == "BA001").
                                                Select(y => Convert.ToDouble(y.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "").
                                                Substring(3, y.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "").Length - 3))).Sum();
                                sum_cabecera = (suma_cabecera1 + suma_cabecera2).ToString();
                                //////////GENERA CABECERA DEL TXT DE PAGOS
                                sw3.WriteLine("1" + listaDoc.Count().ToString().PadLeft(6, '0') + listaDoc[0].fch_ejecucion.ToString("yyyyMMdd") + "C" +
                                            moneda + nro_cta_cargo.PadRight(20, ' ') + imp_montoPago.ToString().PadLeft(17, '0') +
                                            ("PAGO PROVEEDORES").PadRight(40, ' ') + "S" + sum_cabecera.PadLeft(15, '0'));

                                //////////GENERA DETALLE DE TXT DE PAGOS
                                foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDoc)
                                {
                                    msgError = obj.cod_banco_prov == "BA001" && (obj.dsc_cta_bancaria_prov == null || obj.dsc_cta_bancaria_prov.Trim() == "") ? obj.dsc_proveedor + " No tiene Cuenta Bancaria." : "";
                                    msgError = obj.cod_banco_prov != "BA001" && (obj.dsc_cta_interbancaria_prov == null || obj.dsc_cta_interbancaria_prov.Trim() == "") ? obj.dsc_proveedor + " No tiene CCI." : "";
                                    if (msgError != "") { SplashScreenManager.CloseForm(); HNG.MessageError(msgError, "ERROR"); return; }
                                    if (obj == null) continue;
                                    //if (obj.cod_tipo_prog == "DETRACC") { listaDet.Add(obj); continue; }
                                    if (obj.cod_tipo_prog == "DETRACC") continue;

                                    tipo_cuenta = obj.cod_banco_prov != "BA001" ? "B" : obj.cod_tipo_cuenta_prov == "02" ? "A" : obj.cod_tipo_cuenta_prov == "01" ? "C" : "";
                                    nro_cta_abono = obj.cod_banco_prov != "BA001" ? obj.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "") : obj.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace(".", "");
                                    tipo_doc = obj.cod_tipo_documento_prov == "DI004" ? "6" : obj.cod_tipo_documento_prov == "DI001" || obj.cod_tipo_documento_prov == "DI006" ? "1" :
                                                obj.cod_tipo_documento_prov == "DI002" ? "3" : obj.cod_tipo_documento_prov == "DI003" ? "4" : "7";

                                    sw3.WriteLine("2" + tipo_cuenta + nro_cta_abono.PadRight(20, ' ') + "1" + tipo_doc + obj.num_documento_prov.PadRight(12, ' ') +
                                                "   " + (obj.dsc_proveedor.Length >= 75 ? obj.dsc_proveedor.Substring(0, 75) : obj.dsc_proveedor).PadRight(75, ' ') +
                                                ("Referencia Beneficiario " + obj.num_documento_prov).PadRight(40, ' ') +
                                                ("Ref Emp " + obj.num_documento_prov).PadRight(20, ' ') +
                                                (obj.cod_moneda == "SOL" ? "0001" : obj.cod_moneda == "DOL" ? "1001" : "") +
                                                Math.Round(obj.imp_pago, 2).ToString().PadLeft(17, '0') + "S");
                                }
                                sw3.Close();
                            }
                        }
                    }
                    //if (lista.Count() == lista.FindAll(x => x.cod_tipo_prog == "DETRACC").Count())
                    else
                    {
                        listaDet = lista.FindAll(x => x.cod_tipo_prog == "DETRACC");
                        if (listaDet.Count > 0)
                        {
                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objCab = listaDet[0] as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                            //////////GENERA EL TXT DE PAGOS
                            StreamWriter sw4 = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\D" + objCab.dsc_ruc_empresa + "200001.txt");
                            imp_montoPago = listaDet.Select(y => Math.Round(y.imp_pago, 2)).Sum();
                            //////////GENERA CABECERA DEL TXT DE PAGOS
                            sw4.WriteLine("*" + objCab.dsc_ruc_empresa + objCab.dsc_empresa.PadRight(35, ' ') + DateTime.Today.Year.ToString().Substring(2, 2) + "0001" +
                                        Math.Round(imp_montoPago, 2).ToString().Replace(".", "").PadLeft(15, '0'));

                            //////////GENERA DETALLE DE TXT DE PAGOS
                            foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDet)
                            {
                                msgError = obj.cod_banco_prov == "BA005" && (obj.dsc_cta_bancaria_prov == null || obj.dsc_cta_bancaria_prov.Trim() == "") ? obj.dsc_proveedor + " No tiene Cuenta Bancaria." : "";
                                msgError = obj.cod_banco_prov != "BA005" && (obj.dsc_cta_interbancaria_prov == null || obj.dsc_cta_interbancaria_prov.Trim() == "") ? obj.dsc_proveedor + " No tiene CCI." : "";
                                if (msgError != "") { SplashScreenManager.CloseForm(); HNG.MessageError(msgError, "ERROR"); return; }
                                if (obj == null) continue;
                                tipo_cuenta = obj.cod_banco_prov != "BA001" ? "B" : obj.cod_tipo_cuenta_prov == "02" ? "A" : obj.cod_tipo_cuenta_prov == "01" ? "C" : "";
                                nro_cta_abono = obj.cod_banco_prov != "BA005" ? obj.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "") : obj.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace(".", "");
                                tipo_doc = obj.cod_tipo_documento_prov == "DI004" ? "6" : obj.cod_tipo_documento_prov == "DI001" || obj.cod_tipo_documento_prov == "DI006" ? "1" :
                                            obj.cod_tipo_documento_prov == "DI002" ? "3" : obj.cod_tipo_documento_prov == "DI003" ? "4" : "7";

                                sw4.WriteLine("6" + obj.num_documento_prov.PadRight(46, ' ') + "000000000" + obj.cod_concepto_detraccion_SUNAT +
                                            nro_cta_abono + Math.Round(obj.imp_pago, 2).ToString().Replace(".", "").PadLeft(15, '0') +
                                            obj.cod_tipo_transaccion_SUNAT + obj.fch_documento.Year.ToString() +
                                            obj.fch_documento.Month.ToString("00") + obj.cod_sunat + obj.serie_documento +
                                            obj.numero_documento.ToString().PadLeft(8, '0'));
                            }
                            sw4.Close();
                        }
                    }

                    //////////EJECUTA LOS PAGOS EN LA PROGRAMACION
                    foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in lista)
                    {
                        if (obj == null) continue;
                        //if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                        obj.cod_estado = "EJE"; obj.cod_bloque_pago = cod_bloque_pago;
                        obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        //eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj, nuevo_bloque_pago);
                        if (eProgFact == null || eProgFact.cod_bloque_pago.Trim() == "") MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cod_bloque_pago = cod_bloque_pago == "" ? eProgFact.cod_bloque_pago : cod_bloque_pago;
                        nuevo_bloque_pago = "NO"; obj.cod_bloque_pago = cod_bloque_pago;
                    }

                    if (cod_bloque_pago != "")
                    {
                        foreach (int banco in listBancos)
                        {
                            //////////FILTRA A LOS DOCUMENTOS POR CADA BANCO
                            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDoc = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                            listaDoc = lista.FindAll(x => x.num_linea_banco == banco && x.imp_saldo > 0 && x.num_linea > 0);

                            //////////SUMA TOTAL DE LOS DOCUMENTOS DE LA CUENTA BANCARIA
                            imp_montoPago = listaDoc.Select(y => y.imp_pago).Sum();
                            if (imp_montoPago > 0)
                            {
                                //////////INSERTA EN LOS MOVIMIENTOS DE LA CUENTA BANCARIA
                                eEmpresa.eDetalleMovimientoBanco_Empresa objB = new eEmpresa.eDetalleMovimientoBanco_Empresa();
                                objB.cod_empresa = listaDoc[0].cod_empresa; objB.num_linea = (Int32)banco; objB.num_item = 0;
                                objB.fch_ejecutada = listaDoc[0].fch_ejecucion; objB.fch_efectiva = DateTime.Today; objB.cod_tipo_movimiento = "SALIDA";
                                objB.cod_origen_movimiento = "001"; objB.imp_monto = imp_montoPago; objB.cod_bloque_pago = cod_bloque_pago;
                                objB.flg_identificado = "SI"; objB.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                eEmpresa.eDetalleMovimientoBanco_Empresa objPago = unit.Factura.Insertar_Actualizar_DetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(objB);
                                if (objPago == null) { MessageBox.Show("Error al guardar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            }
                        }
                    }

                    //ExportarReportePagoBancos(lista, glosaPrincipal);
                    Process.Start(@"C:\IMPERIUM-Software\Recursos\ArchivosExportados\TXT_PagoBancos\");
                    SplashScreenManager.CloseForm();
                }
                else
                {
                    MessageBox.Show("Proceso cancelado de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                BuscarFacturas();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //private void btnPagarBanco_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
        //        List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDet = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        //        List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        //        lista = listaProgramacion.FindAll(x => x.Sel && x.cod_estado != "EJE");
        //        if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
        //        if (lista.Count == 1 && lista[0].num_linea == 0) { MessageBox.Show("No hay una programación de pago registrada", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
        //        //if (lista.Count == 1 && lista[0].cod_estado == "EJE") { MessageBox.Show("El pago ya esta ejecutado", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
        //        //if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
        //        if (lista.FindAll(x => x.num_linea_banco == 0).Count > 0) { MessageBox.Show("Hay registros sin asignar el BANCO ORIGEN.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
        //        if (lista.FindAll(x => x.num_linea_banco_prov == 0).Count > 0) { MessageBox.Show("Hay registros sin asignar el BANCO DESTINO.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
        //        if (lista.Count == lista.FindAll(x => x.cod_estado == "EJE").Count) { MessageBox.Show("Todos los pago ya esta ejecutados", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
        //        foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eObj in lista)
        //        {
        //            eObj.dsc_proveedor = eObj.dsc_proveedor.Replace("Ñ", "N").Replace("ñ", "n");
        //        }

        //        frmFacturaPagoBanco frmFact = new frmFacturaPagoBanco();
        //        frmFact.Text = "Listado de Factura Pago Banco";
        //        frmFact.listDocumentos = lista;
        //        //new ToolHelper.Forms().ShowDialog(frmFact);
        //        frmFact.ShowDialog();
        //        if (frmFact.listDocumentos.Count > 0 && frmFact.GuardarDatos == "SI")
        //        {

        //            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
        //            //////////LIMPIA LA CARPETA DONDE SE EXPORTA EL TXT DE PAGOS
        //            if (!Directory.Exists("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos")) Directory.CreateDirectory("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos");
        //            DirectoryInfo source = new DirectoryInfo("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos");
        //            FileInfo[] filesToCopy = source.GetFiles();
        //            foreach (FileInfo oFile in filesToCopy)
        //            {
        //                oFile.Delete();
        //            }
        //            foreach (string oCarpeta in Directory.GetDirectories("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos"))
        //            {
        //                Directory.Delete(oCarpeta, true);
        //            }

        //            //////////TRAE LISTA DE LAS CUENTAS BANCARIAS DE LAS FACTURAS
        //            //var listBancos = lista.Select(x => x.num_linea_banco).Distinct();
        //            var listBancos = lista.Select(x => x.num_linea_banco).Distinct().ToList();
        //            decimal imp_montoPago = 0; Double suma_cabecera1 = 0, suma_cabecera2 = 0; string cod_bloque_pago = "", nuevo_bloque_pago = "NO";
        //            string tipo_cuenta, nro_cta_abono, tipo_doc, moneda, nro_cta_cargo, sum_cabecera, espacios = " ";
        //            foreach (int banco in listBancos)
        //            {
        //                //////////FILTRA A LOS DOCUMENTOS POR CADA BANCO
        //                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDoc = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        //                listaDoc = lista.FindAll(x => x.num_linea_banco == banco && x.imp_saldo > 0 && x.cod_estado != "EJE" && x.num_linea > 0);

        //                if (listaDoc[0].cod_banco_empresa == "BA006")//SCOTIABANK
        //                {
        //                    //////////FILTRA A LOS DOCUMENTOS PARA SCOTIABANK RXH
        //                    List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDocRXH = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        //                    listaDocRXH = listaDoc.FindAll(x => x.tipo_documento == "TC008");
        //                    if (listaDocRXH.Count > 0)
        //                    {
        //                        cod_bloque_pago = ""; nuevo_bloque_pago = "SI";
        //                        //////////GENERA EL TXT DE PAGOS
        //                        StreamWriter sw = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\SCOTIABANK - FORMATO VARIOS - PAGO RXH.txt");
        //                        foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDocRXH)
        //                        {
        //                            if (obj == null) continue;
        //                            //if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
        //                            obj.cod_estado = "EJE"; obj.cod_bloque_pago = cod_bloque_pago;
        //                            obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
        //                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
        //                            //eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
        //                            eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj, nuevo_bloque_pago);
        //                            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            cod_bloque_pago = cod_bloque_pago == "" ? eProgFact.cod_bloque_pago : cod_bloque_pago;
        //                            nuevo_bloque_pago = "NO"; obj.cod_bloque_pago = cod_bloque_pago;
        //                            if (obj.cod_tipo_prog == "DETRACC") { listaDet.Add(obj); continue; }

        //                            tipo_cuenta = obj.cod_banco_prov != "BA006" ? "4" : obj.cod_tipo_cuenta_prov == "02" ? "3" : obj.cod_tipo_cuenta_prov == "01" ? "2" : "";
        //                            nro_cta_abono = obj.cod_banco_prov != "BA006" ? obj.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "") : obj.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace(".", "");
        //                            sw.WriteLine(obj.dsc_ruc.Substring(2, 8) +
        //                                        (obj.dsc_proveedor.Length >= 60 ? obj.dsc_proveedor.Substring(0, 60) : obj.dsc_proveedor).PadRight(60, ' ') +
        //                                        ("RH." + obj.serie_documento + "-" + obj.numero_documento.ToString()).PadRight(20, ' ') +
        //                                        obj.fch_documento.ToString("yyyyMMdd") +
        //                                        Math.Round(obj.imp_pago, 2).ToString().Replace(".", "").PadLeft(11, '0') + tipo_cuenta +
        //                                        (tipo_cuenta == "2" || tipo_cuenta == "3" ? nro_cta_abono : espacios.PadRight(26)) +
        //                                        (tipo_cuenta == "4" ? nro_cta_abono : espacios.PadRight(36)));
        //                        }
        //                        sw.Close();

        //                        //////////SUMA TOTAL DE LOS DOCUMENTOS DE LA CUENTA BANCARIA
        //                        //decimal imp_montoPROV = (from item in lista where item.num_linea_banco == banco.Value select item.imp_total).Sum();
        //                        imp_montoPago = listaDocRXH.Select(y => y.imp_pago).Sum();
        //                        if (imp_montoPago > 0)
        //                        {
        //                            //////////INSERTA EN LOS MOVIMIENTOS DE LA CUENTA BANCARIA
        //                            eEmpresa.eDetalleMovimientoBanco_Empresa objB = new eEmpresa.eDetalleMovimientoBanco_Empresa();
        //                            objB.cod_empresa = listaDocRXH[0].cod_empresa; objB.num_linea = (Int32)banco; objB.num_item = 0;
        //                            objB.fch_ejecutada = listaDocRXH[0].fch_ejecucion; objB.fch_efectiva = DateTime.Today; objB.cod_tipo_movimiento = "SALIDA";
        //                            objB.cod_origen_movimiento = "001"; objB.imp_monto = imp_montoPago; objB.cod_bloque_pago = cod_bloque_pago;
        //                            objB.flg_identificado = "SI"; objB.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
        //                            eEmpresa.eDetalleMovimientoBanco_Empresa objPago = unit.Factura.Insertar_Actualizar_DetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(objB);
        //                            if (objPago == null) { MessageBox.Show("Error al guardar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
        //                        }
        //                    }

        //                    //////////FILTRA A LOS DOCUMENTOS PARA SCOTIABANK DIFERENTES A RXH
        //                    List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDocSinRXH = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        //                    listaDocSinRXH = listaDoc.FindAll(x => x.tipo_documento != "TC008");
        //                    if (listaDocSinRXH.Count > 0)
        //                    {
        //                        cod_bloque_pago = ""; nuevo_bloque_pago = "SI";
        //                        //////////GENERA DETALLE DE TXT DE PAGOS
        //                        StreamWriter sw2 = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\SCOTIABANK - FORMATO PROVEEDORES.txt");
        //                        foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDocSinRXH)
        //                        {
        //                            if (obj == null) continue;
        //                            //if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
        //                            obj.cod_estado = "EJE"; obj.cod_bloque_pago = cod_bloque_pago;
        //                            obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
        //                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
        //                            //eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
        //                            eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj, nuevo_bloque_pago);
        //                            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            cod_bloque_pago = cod_bloque_pago == "" ? eProgFact.cod_bloque_pago : cod_bloque_pago;
        //                            nuevo_bloque_pago = "NO"; obj.cod_bloque_pago = cod_bloque_pago;
        //                            if (obj.cod_tipo_prog == "DETRACC") { listaDet.Add(obj); continue; }

        //                            tipo_cuenta = obj.cod_banco_prov != "BA006" ? "4" : obj.cod_tipo_cuenta_prov == "02" ? "3" : obj.cod_tipo_cuenta_prov == "01" ? "2" : "";
        //                            nro_cta_abono = obj.cod_banco_prov != "BA006" ? obj.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "") : obj.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace(".", "");

        //                            sw2.WriteLine(obj.dsc_ruc.PadRight(11, '0') +
        //                                        (obj.dsc_proveedor.Length >= 60 ? obj.dsc_proveedor.Substring(0, 60) : obj.dsc_proveedor).PadRight(60, ' ') +
        //                                        (obj.serie_documento + "-" + obj.numero_documento.ToString().PadLeft(8, '0')).PadRight(14, ' ') +
        //                                        obj.fch_documento.ToString("yyyyMMdd") +
        //                                        Math.Round(obj.imp_pago, 2).ToString().Replace(".", "").PadLeft(11, '0') + tipo_cuenta +
        //                                        (tipo_cuenta == "2" || tipo_cuenta == "3" ? nro_cta_abono.Substring(0, 3) : espacios.PadRight(3)) +
        //                                        (tipo_cuenta == "2" || tipo_cuenta == "3" ? nro_cta_abono.Substring(3, 7) : espacios.PadRight(7)) +
        //                                        (tipo_cuenta == "1" ? "*" : " ") + espacios.PadRight(30) +
        //                                        (tipo_cuenta == "4" ? nro_cta_abono.PadLeft(20, '0') : espacios.PadRight(20)) +
        //                                        " " + espacios.PadRight(10) + " ");
        //                        }
        //                        sw2.Close();

        //                        //////////SUMA TOTAL DE LOS DOCUMENTOS DE LA CUENTA BANCARIA
        //                        //decimal imp_montoPROV = (from item in lista where item.num_linea_banco == banco.Value select item.imp_total).Sum();
        //                        imp_montoPago = listaDocSinRXH.Select(y => y.imp_pago).Sum();
        //                        if (imp_montoPago > 0)
        //                        {
        //                            //////////INSERTA EN LOS MOVIMIENTOS DE LA CUENTA BANCARIA
        //                            eEmpresa.eDetalleMovimientoBanco_Empresa objB = new eEmpresa.eDetalleMovimientoBanco_Empresa();
        //                            objB.cod_empresa = listaDocSinRXH[0].cod_empresa; objB.num_linea = (Int32)banco; objB.num_item = 0;
        //                            objB.fch_ejecutada = listaDocSinRXH[0].fch_ejecucion; objB.fch_efectiva = DateTime.Today; objB.cod_tipo_movimiento = "SALIDA";
        //                            objB.cod_origen_movimiento = "001"; objB.imp_monto = imp_montoPago; objB.cod_bloque_pago = cod_bloque_pago;
        //                            objB.flg_identificado = "SI"; objB.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
        //                            eEmpresa.eDetalleMovimientoBanco_Empresa objPago = unit.Factura.Insertar_Actualizar_DetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(objB);
        //                            if (objPago == null) { MessageBox.Show("Error al guardar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
        //                        }
        //                    }
        //                }

        //                if (listaDoc[0].cod_banco_empresa == "BA001")//BCP
        //                {
        //                    cod_bloque_pago = ""; nuevo_bloque_pago = "SI";
        //                    //////////GENERA EL TXT DE PAGOS
        //                    StreamWriter sw3 = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\BCP - FORMATO PROVEEDORES.txt");
        //                    moneda = listaDoc[0].cod_moneda == "SOL" ? "0001" : listaDoc[0].cod_moneda == "DOL" ? "1001" : "";
        //                    nro_cta_cargo = listaDoc[0].dsc_cta_bancaria_empresa.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "");
        //                    imp_montoPago = listaDoc.Select(y => Math.Round(y.imp_pago, 2)).Sum();
        //                    suma_cabecera1 = Convert.ToDouble(nro_cta_cargo.Substring(3, nro_cta_cargo.Length - 3));
        //                    suma_cabecera2 = listaDoc.Where(x => x.cod_banco_prov != "BA001").
        //                                    Select(y => y.dsc_cta_interbancaria_prov == "" ? 0 : Convert.ToDouble(y.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "").
        //                                    Substring(10, y.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "").Length - 10))).Sum();
        //                    suma_cabecera2 = suma_cabecera2 + listaDoc.Where(x => x.cod_banco_prov == "BA001").
        //                                    Select(y => Convert.ToDouble(y.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "").
        //                                    Substring(3, y.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "").Length - 3))).Sum();
        //                    sum_cabecera = (suma_cabecera1 + suma_cabecera2).ToString();
        //                    //////////GENERA CABECERA DEL TXT DE PAGOS
        //                    sw3.WriteLine("1" + listaDoc.Count().ToString().PadLeft(6, '0') + listaDoc[0].fch_ejecucion.ToString("yyyyMMdd") + "C" +
        //                                moneda + nro_cta_cargo.PadRight(20, ' ') + imp_montoPago.ToString().PadLeft(17, '0') +
        //                                ("PAGO PROVEEDORES").PadRight(40, ' ') + "S" + sum_cabecera.PadLeft(15, '0'));

        //                    //////////GENERA DETALLE DE TXT DE PAGOS
        //                    foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDoc)
        //                    {
        //                        if (obj == null) continue;
        //                        //if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
        //                        obj.cod_estado = "EJE"; obj.cod_bloque_pago = cod_bloque_pago;
        //                        obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
        //                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
        //                        //eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
        //                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj, nuevo_bloque_pago);
        //                        if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        cod_bloque_pago = cod_bloque_pago == "" ? eProgFact.cod_bloque_pago : cod_bloque_pago;
        //                        nuevo_bloque_pago = "NO"; obj.cod_bloque_pago = cod_bloque_pago;
        //                        if (obj.cod_tipo_prog == "DETRACC") { listaDet.Add(obj); continue; }

        //                        tipo_cuenta = obj.cod_banco_prov != "BA001" ? "B" : obj.cod_tipo_cuenta_prov == "02" ? "A" : obj.cod_tipo_cuenta_prov == "01" ? "C" : "";
        //                        nro_cta_abono = obj.cod_banco_prov != "BA001" ? obj.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "") : obj.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace(".", "");
        //                        tipo_doc = obj.cod_tipo_documento_prov == "DI004" ? "6" : obj.cod_tipo_documento_prov == "DI001" || obj.cod_tipo_documento_prov == "DI006" ? "1" :
        //                                    obj.cod_tipo_documento_prov == "DI002" ? "3" : obj.cod_tipo_documento_prov == "DI003" ? "4" : "7";

        //                        sw3.WriteLine("2" + tipo_cuenta + nro_cta_abono.PadRight(20, ' ') + "1" + tipo_doc + obj.num_documento_prov.PadRight(12, ' ') +
        //                                    "   " + (obj.dsc_proveedor.Length >= 75 ? obj.dsc_proveedor.Substring(0, 75) : obj.dsc_proveedor).PadRight(75, ' ') +
        //                                    ("Referencia Beneficiario " + obj.num_documento_prov).PadRight(40, ' ') +
        //                                    ("Ref Emp " + obj.num_documento_prov).PadRight(20, ' ') +
        //                                    (obj.cod_moneda == "SOL" ? "0001" : obj.cod_moneda == "DOL" ? "1001" : "") +
        //                                    Math.Round(obj.imp_pago, 2).ToString().PadLeft(17, '0') + "S");
        //                    }
        //                    sw3.Close();

        //                    //////////SUMA TOTAL DE LOS DOCUMENTOS DE LA CUENTA BANCARIA
        //                    //decimal imp_montoPROV = (from item in lista where item.num_linea_banco == banco.Value select item.imp_total).Sum();
        //                    imp_montoPago = listaDoc.Select(y => y.imp_pago).Sum();
        //                    if (imp_montoPago > 0)
        //                    {
        //                        //////////INSERTA EN LOS MOVIMIENTOS DE LA CUENTA BANCARIA
        //                        eEmpresa.eDetalleMovimientoBanco_Empresa objB = new eEmpresa.eDetalleMovimientoBanco_Empresa();
        //                        objB.cod_empresa = listaDoc[0].cod_empresa; objB.num_linea = (Int32)banco; objB.num_item = 0;
        //                        objB.fch_ejecutada = listaDoc[0].fch_ejecucion; objB.fch_efectiva = listaDoc[0].fch_ejecucion; objB.cod_tipo_movimiento = "SALIDA";
        //                        objB.cod_origen_movimiento = "001"; objB.imp_monto = imp_montoPago; objB.cod_bloque_pago = cod_bloque_pago;
        //                        objB.flg_identificado = "SI"; objB.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
        //                        eEmpresa.eDetalleMovimientoBanco_Empresa objPago = unit.Factura.Insertar_Actualizar_DetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(objB);
        //                        if (objPago == null) { MessageBox.Show("Error al guardar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
        //                    }

        //                }
        //            }

        //            if (listaDet.Count > 0)
        //            {
        //                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objCab = listaDet[0] as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
        //                //////////GENERA EL TXT DE PAGOS
        //                StreamWriter sw4 = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\D" + objCab.dsc_ruc_empresa + "200001.txt");
        //                imp_montoPago = listaDet.Select(y => Math.Round(y.imp_pago, 2)).Sum();
        //                //////////GENERA CABECERA DEL TXT DE PAGOS
        //                sw4.WriteLine("*" + objCab.dsc_ruc_empresa + objCab.dsc_empresa.PadRight(35, ' ') + DateTime.Today.Year.ToString().Substring(2, 2) + "0001" +
        //                            Math.Round(imp_montoPago, 2).ToString().Replace(".", "").PadLeft(15, '0'));

        //                //////////GENERA DETALLE DE TXT DE PAGOS
        //                foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDet)
        //                {
        //                    if (obj == null) continue;
        //                    ////if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
        //                    //obj.cod_estado = "EJE"; obj.cod_bloque_pago = cod_bloque_pago;
        //                    //obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
        //                    //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
        //                    ////eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
        //                    //eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj, nuevo_bloque_pago);
        //                    //if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    //cod_bloque_pago = cod_bloque_pago == "" ? eProgFact.cod_bloque_pago  : cod_bloque_pago;
        //                    //nuevo_bloque_pago = "NO"; obj.cod_bloque_pago = cod_bloque_pago;

        //                    tipo_cuenta = obj.cod_banco_prov != "BA001" ? "B" : obj.cod_tipo_cuenta_prov == "02" ? "A" : obj.cod_tipo_cuenta_prov == "01" ? "C" : "";
        //                    nro_cta_abono = obj.cod_banco_prov != "BA005" ? obj.dsc_cta_interbancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace("–", "").Replace(".", "") : obj.dsc_cta_bancaria_prov.Trim().Replace(" ", "").Replace("-", "").Replace(".", "");
        //                    tipo_doc = obj.cod_tipo_documento_prov == "DI004" ? "6" : obj.cod_tipo_documento_prov == "DI001" || obj.cod_tipo_documento_prov == "DI006" ? "1" :
        //                                obj.cod_tipo_documento_prov == "DI002" ? "3" : obj.cod_tipo_documento_prov == "DI003" ? "4" : "7";

        //                    sw4.WriteLine("6" + obj.num_documento_prov.PadRight(46, ' ') + "000000000" + obj.cod_concepto_detraccion_SUNAT +
        //                                nro_cta_abono + Math.Round(obj.imp_pago, 2).ToString().Replace(".", "").PadLeft(15, '0') +
        //                                obj.cod_tipo_transaccion_SUNAT + obj.fch_documento.Year.ToString() +
        //                                obj.fch_documento.Month.ToString("00") + obj.cod_sunat + obj.serie_documento +
        //                                obj.numero_documento.ToString().PadLeft(8, '0'));
        //                }
        //                sw4.Close();


        //                //frmFacturasConstanciaDetraccRetenc frmDetRet = new frmFacturasConstanciaDetraccRetenc();
        //                //frmDetRet.listFacturas = listaDet;
        //                //frmDetRet.ShowDialog();
        //            }

        //            //ExportarReportePagoBancos(lista, glosaPrincipal);
        //            Process.Start(@"C:\IMPERIUM-Software\Recursos\ArchivosExportados\TXT_PagoBancos\");
        //            SplashScreenManager.CloseForm();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Proceso cancelado de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        }

        //        BuscarFacturas();
        //    }
        //    catch (Exception ex)
        //    {
        //        SplashScreenManager.CloseForm();
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        [DelimitedRecord("|")]
        public class Item
        {
            public string Prop1 { get; set; }
            public string Prop2 { get; set; }
            public string Prop3 { get; set; }
            public string Prop4 { get; set; }
            public string Prop5 { get; set; }
            public string Prop6 { get; set; }
            public string Prop7 { get; set; }
            public string Prop8 { get; set; }
            public string Prop9 { get; set; }
            public string Prop10 { get; set; }
        }


        private void ExportarReportePagoBancos(List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listDocumentos, string dsc_glosa_principal)
        {
            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");
            string ListSeparator = "";

            string entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
            string server = unit.Encripta.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
            string bd = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
            string user = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("UserID")].ToString());
            string pass = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("Password")].ToString());
            string AppName = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("AppName")].ToString());

            string cnxl = "ODBC;DRIVER=SQL Server;SERVER=" + server + ";UID=" + user + ";PWD=" + pass + ";APP=SGI_Excel;DATABASE=" + bd + "";
            string procedure = "";

            ListSeparator = ConfigurationManager.AppSettings["ListSeparator"];
            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();
            objExcel.Visible = true;
            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];

            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exportar_FormatoPagoBancos";
                objExcel.ActiveWindow.DisplayGridlines = false;

                int fila = 0;
                //for (int x = 0; x <= bgvProgramacionPagos.RowCount; x++)
                foreach(eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listDocumentos)
                {
                    //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (obj == null) continue;
                    fila = fila + 1;
                    procedure = "usp_Reporte_ResumenFormatoPagos @cod_proveedor = '" + obj.cod_proveedor +
                                                    "', @tipo_documento = '" + obj.tipo_documento +
                                                    "', @serie_documento = '" + obj.serie_documento +
                                                    "', @numero_documento = '" + obj.numero_documento +
                                                    "', @cod_correlativoSISPAG = '" + obj.cod_correlativoSISPAG + "'" +
                                                    "', @num_linea = " + obj.num_linea +
                                                    "', @dsc_glosa_principal = '" + dsc_glosa_principal + "'";
                    unit.Factura.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + fila, true);
                    if (fila > 1) objExcel.Rows[fila].Delete();
                    fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                }

                objExcel.Range["A:A"].Delete();
                objExcel.Range["A1"].Select();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                //worksheet.Rows(2).Insert();
                //worksheet.Rows(2).Insert();
                fila = fila + 2;
                //int fila = nInLastRow;

                //objExcel.Range["A" + fila].Copy();
                worksheet.Rows(fila).Copy();
                worksheet.Rows(fila + 1).Paste();


                objExcel.Range["A1:AN1"].Select();
                objExcel.Selection.Borders.Color = System.Drawing.Color.FromArgb(0, 0, 0);
                objExcel.Selection.Font.Bold = true;
                objExcel.Selection.Font.Color = System.Drawing.Color.Black;
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFC000");
                objExcel.Range["A1:AN" + fila].Font.Name = "Century Gothic";
                objExcel.Range["A1:AN" + fila].Font.Size = 10;

                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + (fila + 1)].Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN1"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                objExcel.Range["A1:AN1"].Borders.Color = System.Drawing.Color.FromArgb(0, 0, 0);

                objExcel.Range["A1"].RowHeight = 70;
                objExcel.Range["A1:AN" + fila].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["A1:AN" + fila].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                objExcel.Range["A1:AN" + fila].WrapText = true;
                //objExcel.Range["A:AR"].ColumnWidth = 18;


                //objExcel.Range["C4"].Value = "Tipo de Solicitud";
                //objExcel.Range["D4"].Value = "Area";

                objExcel.Range["A1:AN1"].AutoFilter(1, Type.Missing, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                objExcel.Range["A1"].Select();

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                //SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                System.Threading.Thread.CurrentThread.Abort();
                objExcel.ActiveWorkbook.Saved = true;
                objExcel.ActiveWorkbook.Close();
                objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                objExcel.Quit();
                //SplashScreenManager.CloseForm();
                MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAsignarCtaBancoOrigen_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                lista = listaProgramacion.FindAll(x => x.Sel);

                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDet = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (lista.Count == 1 && lista[0].num_linea == 0) { MessageBox.Show("No hay una programación de pago registrada", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Seleccione una cuenta bancaria";
                LookUpEdit lkpCtaBanco = new LookUpEdit(); lkpCtaBanco.Width = 120;
                lkpCtaBanco.Properties.ValueMember = "num_linea_banco"; lkpCtaBanco.Properties.DisplayMember = "dsc_banco_empresa";
                lkpCtaBanco.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[]
                {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_banco_empresa", "Descripción"),
                });
                lkpCtaBanco.Properties.DataSource = unit.Factura.CombosEnGridControl<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>("BancoEmpresa", cod_empresa: lkpEmpresa.EditValue.ToString());
                args.Editor = lkpCtaBanco;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);

                if ((res == DialogResult.OK || res == DialogResult.Yes) && lkpCtaBanco.EditValue != null)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
                    foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in lista)
                    {
                        if (obj == null) continue;

                        if (bgvProgramacionPagos.SelectedRowsCount == 1 && obj.num_linea == 0) { MessageBox.Show("No hay una programación de pago registrada", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                        if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                        obj.cod_estado = "PRO"; obj.num_linea_banco = Convert.ToInt32(lkpCtaBanco.EditValue);
                        obj.cod_usuario_bloque_pago = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                        //obj.cod_bloque_pago = eProgFact.cod_bloque_pago;
                        if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    BuscarFacturas();
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtnBuscarCtaProveedor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                GridView view = bgvProgramacionPagos;
                int index = view.FocusedRowHandle;
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                if (obj == null) return;

                frmBusquedas frm = new frmBusquedas();
                frm.entidad = frmBusquedas.MiEntidad.CtaBancoProveedor;
                frm.cod_proveedor = obj.cod_proveedor;
                frm.ShowDialog();
                if (frm.codigo == "" || frm.codigo == null) { return; }
                obj.num_linea_banco_prov = Convert.ToInt32(frm.codigo);
                obj.dsc_banco_prov = frm.descripcion;
                obj.cod_banco_prov = frm.cod_condicion1;
                obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                view.FocusedRowHandle = index;
                bgvProgramacionPagos.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }
        private void ExportarExcel()
        {
            try
            {
                string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Documentos" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                bgvProgramacionPagos.ExportToXlsx(archivo);
                if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            bgvProgramacionPagos.ShowPrintPreview();
        }

        private void rlkpTipoProgramacion_EditValueChanged(object sender, EventArgs e)
        {
            bgvProgramacionPagos.PostEditor();
        }
    }
}