using DevExpress.XtraEditors;
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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraSplashScreen;
using Microsoft.Identity.Client;
using System.Security;
using System.Net.Http.Headers;
using HNG_Tools;
using DevExpress.CodeParser;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmAuditoriaContable : HNG_Tools.SimpleModalForm
    {
        public List<eFacturaProveedor.eFacturaProveedor_Distribucion> listfacturas = new List<eFacturaProveedor.eFacturaProveedor_Distribucion>();
        public List<eCajaChica.eMovimiento_CajaChica> listcajachica = new List<eCajaChica.eMovimiento_CajaChica>();
        public List<eEntregaRendir.eDetalle_EntregaRendir> listaentregarendir = new List<eEntregaRendir.eDetalle_EntregaRendir>();
        private readonly UnitOfWork unit;
        public string empresa = "", tipo_doc = "", serie_documento = "", cod_proveedor = "", empresas = "", dsc_tab = "", cod_empresa = "", accion = "RESTABLECER", id_historial = "", flg_observacion = "NO", observacionauditoria = "", modulo = "Documentos", cod_caja = "", cod_movimiento_rendido = "",cod_sede_empresa="", cod_entregarendir="";
        public int id_detalle = 0;
        public decimal numero_documento=0,numdoc=0;

        private void gvAuditoria_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            eFacturaProveedor.eFacturaProveedor_Distribucion obj = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            obj = gvAuditoria.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;
            tipo_doc = obj.tipo_documento;
            numdoc = obj.numero_documento;
            serie_documento = obj.serie_documento;
            if(obj.dsc_campo=="TIPO DOCUMENTO")
            {
                obj.tipo_documento = obj.cod_valorantiguo;
            }
            else if (obj.dsc_campo == "NUMERO DOCUMENTO")
            {
                obj.numero_documento = Convert.ToDecimal(obj.valorantiguo);
                obj.tipo_documento = obj.tipo_documento;
            }else if(obj.dsc_campo == "SERIE DOCUMENTO")
            {
                obj.serie_documento = obj.valorantiguo;
            }else if(obj.dsc_campo== "PERIODO TRIBUTARIO")
            {
                cambioperiodotributario();


            }
            else if (obj.dsc_campo== "ELIMINACIÓN DE PAGO DETRACCION")
            {
                HNG.MessageSuccess("No se puede restablecer Pagos", "ERROR AL RESTABLECER"); return;
            }
            obj.tipo_documentoantiguo = tipo_doc;
            obj.serie_documentoantiguo = serie_documento;
            obj.numero_documentoantiguo = numdoc;
            obj.cod_empresa = empresa;
            obj.descripcion = "DOCUMENTO RESTABLECIDO";
            obj.fch_modificacion = DateTime.Today;
            obj.cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
            string result = unit.Factura.RestablecerDocumento(obj, 1);
            string result2 = unit.Factura.RestablecerDocumento(obj, 2);
            if (result != null) { HNG.MessageSuccess("Se restablecio el documento con exito", "CAMBIO EXITOSO"); }
            else { HNG.MessageSuccess("Huebo un error al restablecer documento", "ERROR AL RESTABLECER"); }
            listado();
        }

        private void gcAuditoria_Click(object sender, EventArgs e)
        {

        }



        public frmAuditoriaContable()
        {

            unit = new UnitOfWork();
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
        //    this.Location = Screen.FromControl(this).WorkingArea.Location;
        //    this.Size = Screen.FromControl(this).WorkingArea.Size;
            this.TitleBackColor = Program.Sesion.Colores.Verde;

        }
        private void Inicializar()
        {
            try
            {
                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtdesde.EditValue = oPrimerDiaDelMes;
                dthasta.EditValue = oUltimoDiaDelMes;
                listado();
                listarcajachica();
                listarentregarendir();
                lbldocumento.Properties.NullText = "Ejem: E001-00001111";
                lbldocumento.ForeColor = Color.DarkGray;

                int lastVisibleRowIndex = gvAuditoria.GetVisibleRowHandle(gvAuditoria.RowCount - 1);
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }
      
        private void listado()
        {
           
            listfacturas = unit.Aprobaciones.FiltroFacturaAuditoria<eFacturaProveedor.eFacturaProveedor_Distribucion>(opcion: 38,FechaInicio: Convert.ToString(dtdesde.EditValue), FechaFin: Convert.ToString(dthasta.EditValue),cod_empresa_multiple:empresa);
            bsAuditoria.DataSource = listfacturas;
        }

        private void listarcajachica()
        {
            string dsc = "";
            if(lbldocumento.Text== "Ejem: RP000001") { dsc = ""; } else { dsc = lbldocumento.Text; }
            listcajachica = unit.Aprobaciones.listarcajachica<eCajaChica.eMovimiento_CajaChica>(opcion: 42,Convert.ToDateTime(dtdesde.EditValue), Convert.ToDateTime(dthasta.EditValue),empresa, dsc);
            bscajachica.DataSource = listcajachica; 

        }
        private void listarentregarendir()
        {
            string dsc = "";
            if (lbldocumento.Text == "Ejem: ER000001") { dsc = ""; } else { dsc = lbldocumento.Text; }
            listaentregarendir = unit.Aprobaciones.ListarEntregaRendirAuditoria<eEntregaRendir.eDetalle_EntregaRendir>(opcion: 43, Convert.ToDateTime(dtdesde.EditValue), Convert.ToDateTime(dthasta.EditValue), empresa,dsc);
            bscuentasrendir.DataSource = listaentregarendir;

        }


        private void valoresfactura(string valorantiguo, string valornuevo = "", string cod_proveedor = "", string tipo_documento = "", string serie_documento = "", decimal numero_documento = 0, string dsc_campo = "")
        {
            eFacturaProveedor.eFacturaProveedor_Distribucion objantiguo = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            objantiguo.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            objantiguo.valorantiguo = valorantiguo;
            objantiguo.valoractual = valornuevo;
            objantiguo.descripcion = "CORRECCION DE DOCUMENTOS";
            objantiguo.cod_proveedor = cod_proveedor;
            objantiguo.tipo_documento = tipo_documento;
            objantiguo.serie_documento = serie_documento;
            objantiguo.numero_documento = numero_documento;
            objantiguo.dsc_campo = dsc_campo;

            eFacturaProveedor.eFacturaProveedor_Distribucion ehistorial = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            ehistorial = unit.Factura.InsertarHistorialContable<eFacturaProveedor.eFacturaProveedor_Distribucion>(objantiguo);

        }
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "";

        private async void cambioperiodotributario()
        {
            gvAuditoria.RefreshData();
            if (gvAuditoria.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (gvAuditoria.SelectedRowsCount == 1)
            {
                foreach (int nRow in gvAuditoria.GetSelectedRows())
                {
                    eFacturaProveedor objFP = gvAuditoria.GetRow(nRow) as eFacturaProveedor;
                    if (objFP.cod_estado_registro == "PEN")
                    {
                        HNG.MessageInformation("El documento no se encuentra APROBADO PARA CONTABILIZAR.", "Contabilizar documentos");
                        return;
                    }
                    if (objFP.cod_estado_registro == "RVS")
                    {
                        HNG.MessageInformation("El documento se encuentra solicitado para revisión.", "Contabilizar documentos");
                        return;
                    }
                    if (objFP.cod_estado_registro == "CON")
                    {
                        HNG.MessageInformation("El documento ya se encuentra contabilizado.", "Contabilizar documentos");
                        return;
                    }
                }
            }

            XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingrese el periodo tributario";
            DateEdit dtFecha = new DateEdit(); dtFecha.Width = 100; args.DefaultResponse = DateTime.Today;
            dtFecha.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;
            dtFecha.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
            dtFecha.Properties.Mask.EditMask = "MMMM-yyyy";
            dtFecha.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dtFecha.Properties.Mask.UseMaskAsDisplayFormat = true;
            args.Editor = dtFecha;
            var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);

            if ((res == DialogResult.OK || res == DialogResult.Yes) && dtFecha.EditValue != null)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Contabilizando documentos", "Cargando...");
                foreach (int nRow in gvAuditoria.GetSelectedRows())
                {
                    eFacturaProveedor objTrib = unit.Factura.Obtener_PeriodoTributario<eFacturaProveedor>(50, Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"), cod_empresa);
                    if (objTrib != null && objTrib.flg_cerrado == "SI") { MessageBox.Show("El periodo elegido ya se encuentra CERRADO", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    eFacturaProveedor objF = gvAuditoria.GetRow(nRow) as eFacturaProveedor;
                    if (objF.cod_estado_registro == "CON" || objF.cod_estado_registro == "PEN" || objF.cod_estado_registro == "RVS") continue;
                    objF.cod_estado_registro = "CON"; objF.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario; objF.cod_usuario_contabilizado = Program.Sesion.Usuario.cod_usuario;
                    objF.periodo_tributario = Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"); //Convert.ToDateTime(dtFecha.EditValue);
                    string result = unit.Factura.Actualiar_EstadoRegistroFactura(objF);

                    if (result != "OK") { HNG.MessageError("Error al contabilizar documento", "Contabilizar documentos"); }
                    restablecerperiodotributario();
                    if (objF.imp_percepcion > 0)
                    {
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        if (objF.imp_saldo != 0)
                        {
                            objProg.tipo_documento = objF.tipo_documento; objProg.serie_documento = objF.serie_documento;
                            objProg.numero_documento = objF.numero_documento; objProg.cod_proveedor = objF.cod_proveedor;
                            objProg.num_linea = 0; objProg.cod_empresa = objF.cod_empresa;
                            objProg.imp_pago = objF.imp_saldo; objProg.cod_tipo_prog = objF.tipo_documento == "TC006" ? "NOTACRED" : "REGULAR";
                            objProg.cod_formapago = objF.tipo_documento == "TC006" ? "NOTACRED" : "TRANF";
                            objProg.fch_pago = objF.fch_pago_programado; objProg.dsc_observacion = null;
                            objProg.cod_estado = objF.tipo_documento == "TC006" ? "EJE" : objF.cod_estado_pago == "PAG" ? "EJE" : "PRO"; objProg.cod_pagar_a = "PROV";
                            objProg.fch_ejecucion = objF.cod_estado_pago == "PAG" ? objF.fch_pago_ejecutado : new DateTime(); objProg.cod_usuario_ejecucion = null; objProg.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                            eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(objProg);
                            if (eProgFact == null) HNG.MessageError("Error al grabar programación de pago.", "ERROR");
                        }
                    }


                    ///// MOVEMOS LOS ARCHIVOS A LA CARPETA DEL PERIODO TRIBUTARIO EN EL ONEDRIVE
                    if (objF.idPDF != null || objF.idPDF != "" || objF.idXML != null || objF.idXML != "") await Mover_Eliminar_ArchivoOneDrive(nRow, Convert.ToDateTime(dtFecha.EditValue), objF.idPDF != null && objF.idPDF != "" ? true : false, objF.idXML != null && objF.idXML != "" ? true : false, "MOVER");
                }
                SplashScreenManager.CloseForm();

                HNG.MessageSuccess("Se contabilizaron los documentos de manera satisfactoria", "Contabilizar documentos");
                listado(); 
            }
            else
            {
                HNG.MessageInformation("Debe ingresar el periodo tributario para contabilizar los documentos", "Contabilizar documentos");
            }
        }

        private void gvAuditoria_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            eFacturaProveedor.eFacturaProveedor_Distribucion obj = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            obj = gvAuditoria.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;
            if(obj.dsc_campo=="PROGRAMACION DE PAGOS")
            {
                btnRestaurar.Enabled = false;
                btnRestaurar.BackColor = Color.Gray;
            }
            else
            {
                btnRestaurar.Enabled = true;
            }

        
        }

        private void gvCajachicaAuditoria_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void frmAuditoriaContable_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void gvEntregarendir_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (xtabAuditoria.SelectedTabPage == xtraTabPage1)
            {
                lbldocumento.Properties.NullText = "Ejem: E001-00001111";
                listado();
            }
            else if (xtabAuditoria.SelectedTabPage == xtraTabPage2)
            {
                lbldocumento.Properties.NullText = "Ejem: RP000001";
                listarcajachica();
            }
            else if (xtabAuditoria.SelectedTabPage == xtraTabPage3)
            {
                lbldocumento.Properties.NullText = "Ejem: ER000001";
                listarentregarendir();
            }
        }

        private void xtabAuditoria_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtabAuditoria.SelectedTabPage == xtraTabPage1)
            {
                lbldocumento.Properties.NullText = "Ejem: E001-00001111";
                lbldocumento.ForeColor = Color.Black;
                modulo = "Documentos";
                btnRestaurar.Enabled = true;
            }
            else if (xtabAuditoria.SelectedTabPage == xtraTabPage2)
            {
                lbldocumento.Properties.NullText = "Ejem: RP000001";
                lbldocumento.ForeColor = Color.Black;
                btnRestaurar.Enabled = false;
                modulo = "CajaChica";
            }
            else if (xtabAuditoria.SelectedTabPage == xtraTabPage3)
            {
                lbldocumento.Properties.NullText = "Ejem: ER000001";
                lbldocumento.ForeColor = Color.Black;
                btnRestaurar.Enabled = false;
                modulo = "EntregasRendir";
            }
        }

        private void gvAuditoria_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "valorantiguo") e.Appearance.ForeColor = Color.Red;
            if (e.Column.FieldName == "valoractual") e.Appearance.ForeColor = Color.Blue;

        }

        private void gvCajachicaAuditoria_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "valorantiguo") e.Appearance.ForeColor = Color.Red;
            if (e.Column.FieldName == "valoractual") e.Appearance.ForeColor = Color.Blue;
        }

        private void gvEntregarendir_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "valorantiguo") e.Appearance.ForeColor = Color.Red;
            if (e.Column.FieldName == "valoractual") e.Appearance.ForeColor = Color.Blue;
        }

        private void gvAuditoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) listado();
        }



        private void rbtnAdjuntarentregasrendir_Click(object sender, EventArgs e)
        {
            eEntregaRendir.eDetalle_EntregaRendir obj = new eEntregaRendir.eDetalle_EntregaRendir();
            obj = gvEntregarendir.GetFocusedRow() as eEntregaRendir.eDetalle_EntregaRendir;
            if (obj == null) { HNG.MessageError("DEBE SELECCIONAR UN MOVIMIENTO", "ERROR"); return; }
            else
            {
                frmAdjuntarObservacionContable frmModif = new frmAdjuntarObservacionContable();
                frmModif.cod_empresa = empresa;
                frmModif.observacionauditoria = observacionauditoria;
                frmModif.cod_caja = cod_caja;
                frmModif.cod_movimiento_rendido = cod_movimiento_rendido;
                frmModif.cod_sede_empresa = cod_sede_empresa;
                if (frmModif.flg_observacion == "SI") { frmModif.btnguardar.Text = "Modificar"; } else { frmModif.flg_observacion = flg_observacion; }
                frmModif.modulo = modulo;
                frmModif.cod_entregarendir = obj.cod_entregarendir;
                frmModif.ShowDialog();
                gvEntregarendir.RefreshData();
                listarentregarendir();
            }
        }

        private void gvEntregarendir_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            eEntregaRendir.eDetalle_EntregaRendir obj = new eEntregaRendir.eDetalle_EntregaRendir();
            obj = gvEntregarendir.GetFocusedRow() as eEntregaRendir.eDetalle_EntregaRendir;

            eEntregaRendir.eDetalle_EntregaRendir objTrib = unit.Factura.ObtenerObservacionContable<eEntregaRendir.eDetalle_EntregaRendir>(19, 0, 0, empresa, "", "", obj.cod_sede_empresa,obj.cod_entregarendir);
            if (objTrib.dsc_observacionhistorial != null) { flg_observacion = "SI"; observacionauditoria = objTrib.dsc_observacionhistorial; } else { observacionauditoria = null; }
            cod_entregarendir = obj.cod_entregarendir; cod_sede_empresa = obj.cod_sede_empresa;

        }

        private void xtabAuditoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) listarcajachica();
        }


        private void rbtnAdjuntarcajachica_Click(object sender, EventArgs e)
        {
            eCajaChica.eMovimiento_CajaChica obj = new eCajaChica.eMovimiento_CajaChica();
            obj = gvCajachicaAuditoria.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;
            if (obj == null) { HNG.MessageError("DEBE SELECCIONAR UN MOVIMIENTO", "ERROR"); return; }
            else
            {
                frmAdjuntarObservacionContable frmModif = new frmAdjuntarObservacionContable();
                frmModif.cod_empresa = empresa;
                frmModif.observacionauditoria = observacionauditoria;
                frmModif.cod_caja = cod_caja;
                frmModif.cod_movimiento_rendido = cod_movimiento_rendido;
                frmModif.cod_sede_empresa = cod_sede_empresa;
                if (frmModif.flg_observacion == "SI") { frmModif.btnguardar.Text = "Modificar"; } else { frmModif.flg_observacion = flg_observacion; }
                frmModif.modulo = modulo;
                frmModif.ShowDialog();
                listarcajachica();
            }



        }

        private void gvCajachicaAuditoria_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            eCajaChica.eMovimiento_CajaChica obj = new eCajaChica.eMovimiento_CajaChica();
            obj = gvCajachicaAuditoria.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;

            eCajaChica.eMovimiento_CajaChica objTrib = unit.Factura.ObtenerObservacionContable<eCajaChica.eMovimiento_CajaChica>(18, 0, 0, empresa, obj.cod_movimiento_rendido, obj.cod_caja, obj.cod_sede_empresa);
            if (objTrib.dsc_observacionhistorial != null) { flg_observacion = "SI"; observacionauditoria = objTrib.dsc_observacionhistorial; } else { observacionauditoria = null; }
            cod_caja = obj.cod_caja;cod_movimiento_rendido = obj.cod_movimiento_rendido;cod_sede_empresa = obj.cod_sede_empresa;
        }

        private void gvAuditoria_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            eFacturaProveedor.eFacturaProveedor_Distribucion obj = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            obj = gvAuditoria.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;

            eFacturaProveedor.eFacturaProveedor_Distribucion objTrib = unit.Factura.ObtenerObservacionContable<eFacturaProveedor.eFacturaProveedor_Distribucion>(15, obj.id_detalle, obj.id_historial, empresa);
            if (objTrib.dsc_observacion != null) { flg_observacion = "SI"; observacionauditoria = objTrib.dsc_observacion; } else { observacionauditoria = null; }

        }

        private void gvEntregarendir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) listarentregarendir();
        }

        private void gvAuditoria_RowClick(object sender, RowClickEventArgs e)
        {
            eFacturaProveedor.eFacturaProveedor_Distribucion obj = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            obj = gvAuditoria.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;

            eFacturaProveedor.eFacturaProveedor_Distribucion objTrib = unit.Factura.ObtenerObservacionContable<eFacturaProveedor.eFacturaProveedor_Distribucion>(15, obj.id_detalle, obj.id_historial, empresa);
            if (objTrib.dsc_observacionhistorial != null) { flg_observacion = "SI"; observacionauditoria = objTrib.dsc_observacionhistorial; } else { observacionauditoria = null; }
        }

        private void rbtnAdjuntar_Click(object sender, EventArgs e)
        {
            eFacturaProveedor.eFacturaProveedor_Distribucion obj = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            obj = gvAuditoria.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;
            if(obj == null ) { HNG.MessageError("DEBE SELECCIONAR UN MOVIMIENTO", "ERROR"); return; }
            else
            {
                frmAdjuntarObservacionContable frmModif = new frmAdjuntarObservacionContable();
                frmModif.cod_empresa = empresa;
                frmModif.id_detalle = obj.id_detalle;
                frmModif.id_historial = obj.id_historial;
                frmModif.observacionauditoria = observacionauditoria;
                if(frmModif.flg_observacion == "SI") { frmModif.btnguardar.Text = "Modificar"; } else { frmModif.flg_observacion = flg_observacion; }
                frmModif.modulo = modulo;
                frmModif.ShowDialog();
                if(frmModif.Actualizar == "SI") { listado(); }
                
               
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
        private async Task Mover_Eliminar_ArchivoOneDrive(int nRow, DateTime FechaPeriodo, bool PDF, bool XML, string opcion)
        {
            try
            {
                eFacturaProveedor obj = gvAuditoria.GetRow(nRow) as eFacturaProveedor;
                obj.periodo_tributario = FechaPeriodo.ToString("MM-yyyy");
                if (gvAuditoria.SelectedRowsCount == 1 && (obj.periodo_tributario == null || obj.periodo_tributario == "")) { MessageBox.Show("Debe asignar un periodo tributario para mover los archivos adjuntos", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (obj.periodo_tributario == null || obj.periodo_tributario == "") return;
                string dsc_Carpeta = obj.tipo_documento == "TC008" ? "RxH Proveedor" : "Facturas Proveedor";
                int Anho = Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)); int Mes = Convert.ToInt32(obj.periodo_tributario.Substring(0, 2)); string NombreMes = Convert.ToDateTime(obj.periodo_tributario).ToString("MMMM");
                string IdArchivoAnho = "", IdArchivoMes = "";
                varNombreArchivo = obj.NombreArchivo;

                eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                ClientId = eEmp.ClientIdOnedrive;
                TenantId = eEmp.TenantOnedrive;
                Appl();
                var app = PublicClientApp;
                ////var app = App.PublicClientApp;
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

                //var targetItemFolderId = eEmp.idCarpetaFacturasOnedrive;
                eEmpresa.eOnedrive_Empresa eDatos = new eEmpresa.eOnedrive_Empresa();
                eDatos = unit.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(26, obj.cod_empresa, Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)), dsc_Carpeta: dsc_Carpeta);
                var targetItemFolderId = opcion != "ELIMINAR" ? eDatos.idCarpeta : "";

                //eFacturaProveedor IdCarpetaAnho = unit.Factura.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                eEmpresa.eOnedrive_Empresa IdCarpetaAnho = unit.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, obj.cod_empresa, Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)), dsc_Carpeta: dsc_Carpeta);
                if (IdCarpetaAnho == null && opcion != "ELIMINAR") //Si no existe folder lo crea
                {
                    var driveItem = new Microsoft.Graph.DriveItem
                    {
                        Name = Anho.ToString(),
                        Folder = new Microsoft.Graph.Folder
                        {
                        },
                        AdditionalData = new Dictionary<string, object>()
                                {
                                {"@microsoft.graph.conflictBehavior", "rename"}
                                }
                    };

                    var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderId].Children.Request().AddAsync(driveItem);
                    IdArchivoAnho = driveItemInfo.Id;
                }
                else //Si existe folder obtener id
                {
                    IdArchivoAnho = opcion != "ELIMINAR" ? IdCarpetaAnho.idCarpetaAnho : "";
                }
                var targetItemFolderIdAnho = IdArchivoAnho;

                //eFacturaProveedor IdCarpetaMes = unit.Factura.ObtenerDatosOneDrive<eFacturaProveedor>(14, lkpEmpresaProveedor.EditValue.ToString(), Mes: Convert.ToDateTime(dtFechaRegistro.EditValue).Month);
                eEmpresa.eOnedrive_Empresa IdCarpetaMes = unit.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, obj.cod_empresa, Convert.ToInt32(obj.periodo_tributario.Substring(3, 4)), Convert.ToInt32(obj.periodo_tributario.Substring(0, 2)), dsc_Carpeta);
                if (IdCarpetaMes == null && opcion != "ELIMINAR")
                {
                    var driveItem = new Microsoft.Graph.DriveItem
                    {
                        //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
                        Name = $"{Mes:00}" + ". " + NombreMes.ToUpper(),
                        Folder = new Microsoft.Graph.Folder
                        {
                        },
                        AdditionalData = new Dictionary<string, object>()
                                    {
                                    {"@microsoft.graph.conflictBehavior", "rename"}
                                    }
                    };

                    var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderIdAnho].Children.Request().AddAsync(driveItem);
                    IdArchivoMes = driveItemInfo.Id;
                }
                else //Si existe folder obtener id
                {
                    IdArchivoMes = opcion != "ELIMINAR" ? IdCarpetaMes.idCarpetaMes : "";
                }

                for (int x = 0; x < 2; x++)
                {
                    if (x == 0 && !PDF) continue;
                    if (x == 1 && !XML) continue;
                    //MOVER ARCHIVO A OTRA CARPETA DEL ONEDRIVE
                    var DriveItem = new Microsoft.Graph.DriveItem
                    {
                        ParentReference = new Microsoft.Graph.ItemReference
                        {
                            Id = IdArchivoMes
                        },
                        //Name = varNombreArchivo + (x == 0 ? ".pdf" : ".xml") //Se comenta para que siga MANTENIENDO EL NOMBRE ASIGNADO
                    };

                    if (opcion == "MOVER") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : obj.idXML].Request().UpdateAsync(DriveItem);
                    if (opcion == "ELIMINAR") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();
                    //if (opcion == "ELIMINAR") await GraphClient.Directory.DeletedItems[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void restablecerperiodotributario()
        {

            eFacturaProveedor.eFacturaProveedor_Distribucion obj = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            obj = gvAuditoria.GetFocusedRow() as eFacturaProveedor.eFacturaProveedor_Distribucion;
            obj.valoractual = obj.valoranterior;
            obj.valoranterior = obj.valoractual;

            string resultado = unit.Factura.RestablecerDocumento(obj, 2);
            if (resultado != null) { HNG.MessageSuccess("Se restablecio el documento con exito", "CAMBIO EXITOSO"); }
            else { HNG.MessageSuccess("Huebo un error al restablecer documento", "CAMBIO EXITOSO"); }



         
        }

    }
}