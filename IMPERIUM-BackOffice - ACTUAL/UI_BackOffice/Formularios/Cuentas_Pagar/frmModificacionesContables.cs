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
using DevExpress.Images;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors;
using Microsoft.Identity.Client;
using System.Security;
using System.Net.Http.Headers;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmModificacionesContables : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        List<eFacturaProveedor> listfacturas = new List<eFacturaProveedor>();
        List<eCajaChica.eMovimiento_CajaChica> listarcajachica = new List<eCajaChica.eMovimiento_CajaChica>();
        List<eEntregaRendir.eDetalle_EntregaRendir> listarentregasrendiraprobados = new List<eEntregaRendir.eDetalle_EntregaRendir>();
        List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> programaciones = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        public string cod_empresa = "", accionmodificada = "PERIODO TRIBUTARIO", valorantiguo = "", valornuevo = "";
        public decimal numero_documento = 0;
        public int id_detalle = 0;
        public string EntregaRendir = "NO", cod_entregarendir = "", cod_trabajador = "", pagoejecutado="";


        public frmModificacionesContables()
        {
            unit = new UnitOfWork();
            InitializeComponent();
            Inicializar();
        }
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "";

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();
                //Fecha
                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtFechaInicio.EditValue = oPrimerDiaDelMes;
                dtFechaFin.EditValue = oUltimoDiaDelMes;
                chkcbTipoDocumento.CheckAll();
                btnPeriodoTributario.Enabled = true;
                btnEliminarUltimoPagoEjecutado.Enabled = true;
                btnRetornarReposicionCajaChica.Enabled = false;
                btnRetornarRendicionEntregaRendir.Enabled = false;

            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }
        private void CargarLookUpEdit()
        {
            try
            {
                //CargarCombosGridLookup("TipoComprobante", glkpTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "", valorDefecto: true);
                unit.Factura.CargaCombosChecked("TipoDocumento", chkcbTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "");
                List<eFacturaProveedor> list = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }

        private void btnSeleccionMultiple_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvFactura.OptionsSelection.MultiSelect == true)
            {
                gvFactura.OptionsSelection.MultiSelect = false;
                //btnEliminarUltimoPagoEjecutado.Enabled = true;
                btnRetornarReposicionCajaChica.Enabled = false;
                 btnRetornarRendicionEntregaRendir.Enabled = false; 
                return;
            }
            if (gvFactura.OptionsSelection.MultiSelect == false)
            {
                gvFactura.OptionsSelection.MultiSelect = true;
                //btnEliminarUltimoPagoEjecutado.Enabled = false;
                btnRetornarReposicionCajaChica.Enabled = false;
                btnRetornarRendicionEntregaRendir.Enabled = false;
                return;
            }
        }
        int ctd_empresas = 0;
        private void CargarFiltroTreeList()
        {

            var ListEmp = Program.Sesion.EmpresaList;
            ctd_empresas = ListEmp.Count;

            var emp_sedeList = new List<eFltEmpresaSede>();
            foreach (var obj in ListEmp)
            {

                List<eEmpresa.eSedeEmpresa> ListSedes = unit.Clientes.ListarOpcionesMenu<eEmpresa.eSedeEmpresa>(50, obj.cod_empresa);
                foreach (eEmpresa.eSedeEmpresa objSede in ListSedes)
                {
                    emp_sedeList.Add(new eFltEmpresaSede()
                    {
                        cod_empresa = obj.cod_empresa,
                        dsc_empresa = obj.dsc_empresa

                    });
                }
            }

            if (emp_sedeList != null && emp_sedeList.Count > 0)
            {
                var lst = emp_sedeList;
                var tree = new Tools.TreeListHelper(treeFiltroEmpresa);
                tree.TreeViewParaUnNodo<eFltEmpresaSede>(emp_sedeList,
                      ColumnaCod_Padre: "cod_empresa",
                      ColumnaDsc_Padre: "dsc_empresa"

                    );
                refreshTreeView();
            }
        }

        private void navFilter_MouseClick(object sender, MouseEventArgs e)
        {
            refreshTreeView();
        }
        private void refreshTreeView()
        {
            treeFiltroEmpresa.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Radio;
            for (int i = 0; i < treeFiltroEmpresa.Nodes.Count; i++)
            {
                treeFiltroEmpresa.Nodes[i].ChildrenCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Radio;
                for (int j = 0; j < treeFiltroEmpresa.Nodes[i].Nodes.Count(); j++)
                {
                    treeFiltroEmpresa.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
                }
            }
            treeFiltroEmpresa.Nodes[0].Checked = true;
            //  treeFiltroEmpresa.Nodes[0].Nodes[0].Checked = true;

            treeFiltroEmpresa.CollapseAll();
            treeFiltroEmpresa.Nodes[0].ExpandAll();
            treeFiltroEmpresa.Refresh();
            //treeFiltroEmpresa.CheckAll();


        }

        private void frmAprobacionesContables_Load(object sender, EventArgs e)
        {
            CargarFiltroTreeList();
            if (layoutfiltro.ContentVisible == false)
            {
                layoutfiltro.ContentVisible = true;
                layoutfiltro.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;


                return;
            }
            CargarListado();
            listacajachica();
            listarcuentasrendidas();
        }

        private void btnOcultarFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (layoutfiltro.ContentVisible == true)
            {
                layoutfiltro.ContentVisible = false;
                layoutfiltro.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                Image img = ImageResourceCache.Default.GetImage("images/snap/quickfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Mostrar Filtro";

                return;
            }
            if (layoutfiltro.ContentVisible == false)
            {
                layoutfiltro.ContentVisible = true;
                layoutfiltro.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                Image img = ImageResourceCache.Default.GetImage("images/filter/ignoremasterfilter_32x32.png");
                btnOcultarFiltro.ImageOptions.LargeImage = img;
                btnOcultarFiltro.Caption = "Ocultar Filtro";
                return;
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            CargarListado();
            listacajachica();
            listarcuentasrendidas();
        }
        public void CargarListado()
        {
            try
            {
                string empresas = "";


                var tool = new Tools.TreeListHelper(treeFiltroEmpresa);
                cod_empresa = tool.ObtenerCodigoConcatenadoDeNodoIndex(0);
                empresas = tool.ObtenerCodigoConcatenadoDeNodoIndex(0);
                int nRowHandle = gvFactura.FocusedRowHandle;

                listfacturas.Clear();
                listfacturas = unit.Aprobaciones.FiltroFacturaMenu<eFacturaProveedor>(opcion: 37, cod_empresa_multiple: cod_empresa,
                    chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(),
                    Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsFacturaProvedor.DataSource = listfacturas;
                gvFactura.FocusedRowHandle = nRowHandle;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        private void gvFactura_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvFactura_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {

                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    obj = gvFactura.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.aprobacion_contable = "aprobado";
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.RUC = obj.dsc_ruc;
                    frmModif.tipo_documento = obj.tipo_documento;
                    frmModif.serie_documento = obj.serie_documento;
                    frmModif.numero_documento = obj.numero_documento;
                    frmModif.cod_proveedor = obj.cod_proveedor;
                    frmModif.cod_empresa = obj.cod_empresa;
                    frmModif.EntregaRendir = obj.flg_EntregasRendir;
                    frmModif.CajaChica = obj.flg_CajaChica;
                    frmModif.cod_caja = obj.cod_caja;
                    frmModif.cod_movimiento = obj.cod_movimiento;
                    frmModif.cod_entregarendir = obj.cod_entregarendir;
                    frmModif.cod_empresa = obj.cod_empresa;
                    frmModif.cod_sede_empresa = obj.cod_sede_empresa;

                    frmModif.tipo_documentoantogiuo = obj.tipo_documento;
                    frmModif.serie_documentoantiguo = obj.serie_documento;
                    frmModif.numerodocantiguo = obj.numero_documento;
                    frmModif.id_detalle = obj.id_detalle;
                    frmModif.iddetalle = obj.id_detalle;
                    frmModif.ceco_nuevo = "NO";
                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                    if (frmModif.ActualizarListado) CargarListado();
                }
                EAprobaciones.Ecajachica obj2 = new EAprobaciones.Ecajachica();
                if (e.Clicks == 1 && e.Column.FieldName == "cod_movimiento")
                {
                    obj2 = gvFactura.GetFocusedRow() as EAprobaciones.Ecajachica;
                    if (obj2.cod_movimiento == "APERTURA") { return; }
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    if (obj2 == null) { return; }

                    frmDetalleCajaChicaAprobaciones frmModif = new frmDetalleCajaChicaAprobaciones();
                    frmModif.ObtenerLista_CajaRendida(obj2.cod_caja, obj2.cod_movimiento);
                    frmModif.cod_empresa = cod_empresa;
                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvFactura_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) { }
        }

        private async void btnPeriodoTributario_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvFactura.RefreshData();
            if (gvFactura.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (gvFactura.SelectedRowsCount == 1)
            {
                foreach (int nRow in gvFactura.GetSelectedRows())
                {
                    eFacturaProveedor objFP = gvFactura.GetRow(nRow) as eFacturaProveedor;
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
                foreach (int nRow in gvFactura.GetSelectedRows())
                {
                    eFacturaProveedor objTrib = unit.Factura.Obtener_PeriodoTributario<eFacturaProveedor>(50, Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"), cod_empresa);
                    if (objTrib != null && objTrib.flg_cerrado == "SI") { MessageBox.Show("El periodo elegido ya se encuentra CERRADO", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    eFacturaProveedor objF = gvFactura.GetRow(nRow) as eFacturaProveedor;
                    valorantiguo = objF.periodo_tributario; id_detalle = objF.id_detalle; cod_empresa = objF.cod_empresa;
                    if (objF.cod_estado_registro == "CON" || objF.cod_estado_registro == "PEN" || objF.cod_estado_registro == "RVS") continue;
                    objF.cod_estado_registro = "CON"; objF.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario; objF.cod_usuario_contabilizado = Program.Sesion.Usuario.cod_usuario;
                    objF.periodo_tributario = Convert.ToDateTime(dtFecha.EditValue).ToString("MM-yyyy"); //Convert.ToDateTime(dtFecha.EditValue);
                    string result = unit.Factura.Actualiar_EstadoRegistroFactura(objF);
                    valoresfactura(valorantiguo, objF.periodo_tributario, objF.cod_proveedor, objF.tipo_documento, objF.serie_documento, objF.numero_documento, "PERIODO TRIBUTARIO", cod_empresa);
                    if (result != "OK") { HNG.MessageError("Error al contabilizar documento", "Contabilizar documentos"); }

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
                CargarListado();
            }
            else
            {
                HNG.MessageInformation("Debe ingresar el periodo tributario para contabilizar los documentos", "Contabilizar documentos");
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



        private void btnEliminarUltimoPagoEjecutado_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvFactura.RefreshData();
            if (gvFactura.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (gvFactura.SelectedRowsCount >= 1)
            {
                foreach (int nRow in gvFactura.GetSelectedRows())
                {
                    eFacturaProveedor objFP = gvFactura.GetRow(nRow) as eFacturaProveedor;
                    programaciones.Clear();
                    programaciones = unit.Aprobaciones.Ultimaprogramacion<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(opcion: 12, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor);
                    if (programaciones.Count == 0 && gvFactura.SelectedRowsCount == 1) { HNG.MessageError("El documento seleccionado no tiene pagos ejecutados", "ERROR");  return; }
                    if (programaciones.Count == 0) continue;
                    var linea = programaciones[0].num_linea;
                    var imp_total = programaciones[0].imp_pago;
                    int num_linea = linea;
                    if (objFP.flg_detraccion == "NO")
                    {
                        if (programaciones.Count > 0)
                        {

                            if (HNG.MessageQuestion("El documento se encuentra Pagado" + "\n¿Desea Eliminar el Pago Programado?", "PAGO IDENTIFICADO") == DialogResult.Yes)
                            {
                                Actualizarprogramacionpagofactura(3, imp_total, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor, num_linea);
                                EliminarProgramaciondepago(objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor, imp_total, num_linea);
                                //Agregarprogramacionpendiente(objFP);
                                id_detalle = objFP.id_detalle;
                                valoresfactura("PAGO EJECUTADO:" + Convert.ToString(objFP.fch_pago_ejecutado), "PAGO PROGRAMADO", objFP.cod_proveedor, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, "ULTIMO PAGO EJECUTADO", cod_empresa);

                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (objFP.cod_estado_pago == "PAGADO" || objFP.cod_estado_pago == "APROBADO" && objFP.flg_detraccion == "SI")
                    {
                        if (HNG.MessageQuestion("El documento se encuentra Pagado" + "\n¿Desea Eliminar el Pago Programado?", "PAGO IDENTIFICADO") == DialogResult.Yes)
                        {
                            Actualizarprogramacionpagofactura(3, objFP.imp_total, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor, num_linea);
                            EliminarProgramaciondepago(objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor, objFP.imp_total, num_linea);
                            //Agregarprogramacionpendiente(objFP);
                            id_detalle = objFP.id_detalle;
                            valoresfactura("PAGO EJECUTADO:" + Convert.ToString(objFP.fch_pago_ejecutado.ToShortDateString()), "PAGO PROGRAMADO", objFP.cod_proveedor, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, "ULTIMO PAGO EJECUTADO", cod_empresa);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                if (pagoejecutado == "SI") { HNG.MessageSuccess("Se elimino el pago ejecutado", "PAGO ELIMINADO"); btnbuscar_Click(btnbuscar, new EventArgs()); }
                else { HNG.MessageError("Error al eliminar pago ejecutado", "ERROR"); }
            }
        }

        private void EliminarProgramaciondepago(string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor, decimal imp_saldo, int num_linea)
        {
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objantiguo = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            objantiguo.imp_saldo = imp_saldo;
            objantiguo.tipo_documento = tipo_documento;
            objantiguo.serie_documento = serie_documento;
            objantiguo.numero_documento = numero_documento;
            objantiguo.cod_proveedor = cod_proveedor;
            objantiguo.num_linea = num_linea;
            string resultado;
            resultado = unit.Aprobaciones.EliminarProgramacionpagos(4, objantiguo.tipo_documento, objantiguo.serie_documento, objantiguo.numero_documento, objantiguo.cod_proveedor, objantiguo.imp_saldo, num_linea);
            //resultado = unit.Aprobaciones.EliminarProgramacionpagos(8, objantiguo.tipo_documento, objantiguo.serie_documento, objantiguo.numero_documento, objantiguo.cod_proveedor, 0, num_linea);
            resultado = unit.Aprobaciones.EliminarProgramacionpagos(13, objantiguo.tipo_documento, objantiguo.serie_documento, objantiguo.numero_documento, objantiguo.cod_proveedor, 0, num_linea);
        }

        private void EliminarProgramacionDetraccion(int opcion,string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor)
        {
            eFacturaProveedor objantiguo = new eFacturaProveedor();
            objantiguo.tipo_documento = tipo_documento;
            objantiguo.serie_documento = serie_documento;
            objantiguo.numero_documento = numero_documento;
            objantiguo.cod_proveedor = cod_proveedor;
            string resultado;
            resultado = unit.Factura.EliminarProgramacionpagos(opcion, objantiguo.tipo_documento, objantiguo.serie_documento, objantiguo.numero_documento, objantiguo.cod_proveedor);
        }

        private void chkcbTipoDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) chkcbTipoDocumento.EditValue = null;
        }

        private void chkcbTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            CargarListado();
        }

        private void Actualizarprogramacionpagofactura(int opcion, decimal imp_saldo, string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor, int num_linea = 0)
        {
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objantiguo = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            objantiguo.imp_saldo = imp_saldo;
            objantiguo.tipo_documento = tipo_documento;
            objantiguo.serie_documento = serie_documento;
            objantiguo.numero_documento = numero_documento;
            objantiguo.cod_proveedor = cod_proveedor;
            objantiguo.num_linea = num_linea;
            string resultado;
            resultado = unit.Factura.EliminarProgramacionpagos(opcion, objantiguo.tipo_documento, objantiguo.serie_documento, objantiguo.numero_documento, objantiguo.cod_proveedor, objantiguo.imp_saldo);
        }

        private void gvcajachica_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvcajachica_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) { }
        }

        private void gvcajachica_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {

                eCajaChica.eMovimiento_CajaChica obj2 = new eCajaChica.eMovimiento_CajaChica();
                if (e.Clicks == 1 && e.Column.FieldName == "cod_movimiento")
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    obj2 = gvCajaChica.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;
                    if (obj2 == null) { return; }

                    frmDetalleCajaChicaAprobaciones frmModif = new frmDetalleCajaChicaAprobaciones();
                    frmModif.ObtenerLista_CajaRendida(obj2.cod_caja, obj2.cod_movimiento);
                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRetornarReposicionCajaChica_ItemClick(object sender, ItemClickEventArgs e)
        {
            eCajaChica.eMovimiento_CajaChica obj2 = new eCajaChica.eMovimiento_CajaChica();
            obj2 = gvCajaChica.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;

            eCajaChica.eMovimiento_CajaChica obja = unit.CajaChica.ObtenerDatos_CajaChica<eCajaChica.eMovimiento_CajaChica>(17, obj2.cod_caja);

            try
            {
                if (HNG.MessageQuestion("¿Esta seguro de retornar documento?" , "Esta acción es irreversible.") == DialogResult.Yes)
                {
                    foreach (int nRow in gvCajaChica.GetSelectedRows())
                    {
                        //eEntregaRendir.eDetalle_EntregaRendir obj = gvListadoPreRendicion.GetFocusedRow() as eEntregaRendir.eDetalle_EntregaRendir;
                        eCajaChica.eMovimiento_CajaChica obj = gvCajaChica.GetRow(nRow) as eCajaChica.eMovimiento_CajaChica;
                        if (obj == null) return;
                        obj.cod_sede_empresa = obja.cod_sede_empresa;
                        string result = unit.CajaChica.Retornocajachica(6, obj);
                        obj.valoractual = "POR RENDIR"; obj.valorantiguo = "RENDIDO"; obj.cod_usuario_registro= Program.Sesion.Usuario.cod_usuario;
                        string resultad2 = unit.CajaChica.InsertarHistorialContablecajachica(obj);
                        if (result != "OK") HNG.MessageError("ERROR AL RETORNAR DOCUMENTO", "RETORNO DOCUMENTO");
                        
                        frmDetalleCajaChicaAprobaciones frmModif = new frmDetalleCajaChicaAprobaciones();
                        List<eCajaChica.eMovimiento_CajaChica>listCajaRendida = unit.CajaChica.ListarDatos_CajaChicaAprobaciones<eCajaChica.eMovimiento_CajaChica>(14, obj.cod_caja, obj.cod_movimiento_rendido);
                        foreach (var i in listCajaRendida)
                        {
                            if (i == null) return;
                            i.cod_sede_empresa = obja.cod_sede_empresa;
                            string resulta = unit.CajaChica.Retornocajachica(6, i);
                        }




                    }
                    HNG.MessageSuccess("Se Retorno el documento de manera exitosa", "RETORNO EXITOSO");
                    btnbuscar_Click(btnbuscar, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                HNG.MessageError("ERROR AL RETORNAR DOCUMENTO", "RETORNO DOCUMENTO");
            }
        }



        private void Agregarprogramacionpendiente(eFacturaProveedor obj)
        {
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            if (obj.imp_total == 0) return;
            objProg.tipo_documento = obj.tipo_documento; objProg.serie_documento = obj.serie_documento; objProg.numero_documento = obj.numero_documento; objProg.cod_proveedor = obj.cod_proveedor;
            objProg.num_linea = 0; objProg.cod_empresa = obj.cod_empresa;
            //objProg.fch_pago = Convert.ToDateTime(dtFechaPagoProgramado.EditValue);
            objProg.imp_pago = obj.imp_total;
            objProg.cod_tipo_prog = EntregaRendir == "SI" ? "ENTREGAREN" : obj.tipo_documento == "TC006" ? "NOTACRED" : "REGULAR";
            objProg.cod_formapago = obj.tipo_documento == "TC006" ? "NOTACRED" : "TRANF";
            objProg.fch_pago = EntregaRendir == "SI" ? obj.fch_registro : obj.fch_pago_programado; objProg.dsc_observacion = null;
            objProg.cod_estado = EntregaRendir == "SI" ? "PRO" : obj.cod_estado_pago == "PAG" ? "EJE" : "PRO";
            objProg.cod_pagar_a = EntregaRendir == "SI" ? "TRAB" : "PROV";
            objProg.fch_ejecucion = EntregaRendir == "SI" ? new DateTime() : obj.cod_estado_pago == "PAG" ? obj.fch_pago_ejecutado : new DateTime();
            objProg.cod_usuario_ejecucion = null; objProg.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(objProg);
            if (eProgFact == null) HNG.MessageError("Error al grabar programación de pago.", "ERROR");
        }

        private void gventregasrendir_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eEntregaRendir.eDetalle_EntregaRendir obj = new eEntregaRendir.eDetalle_EntregaRendir();
                    obj = gvEntregasRendir.GetFocusedRow() as eEntregaRendir.eDetalle_EntregaRendir;
                    if (obj == null || obj.abv_estado == "A") return;
                    frmDetalleEntregaRendir frm = new frmDetalleEntregaRendir();
                    frm.MiAccion = DetEntregaRendir.Editar;
                    frm.cod_entregarendir = obj.cod_entregarendir;
                    frm.cod_empresa = obj.cod_empresa;
                    frm.cod_sede_empresa = obj.cod_sede_empresa;
                    frm.cod_entregado_a = obj.cod_entregado_a;
                    if (obj.cod_estado_aprobado == "PEN") { frm.chkFlgPorRendir.Enabled = false; frm.chkFlgRendido.Enabled = false; }
                    frm.ShowDialog();
                    if (frm.ActualizarListado == "SI") btnbuscar_Click(btnbuscar, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gventregasrendir_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void btnRetornarRendicionEntregaRendir_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de retornar a Pre-Rendición?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (int nRow in gvEntregasRendir.GetSelectedRows())
                    {
                        //eEntregaRendir.eDetalle_EntregaRendir obj = gvListadoPreRendicion.GetFocusedRow() as eEntregaRendir.eDetalle_EntregaRendir;
                        eEntregaRendir.eDetalle_EntregaRendir obj = gvEntregasRendir.GetRow(nRow) as eEntregaRendir.eDetalle_EntregaRendir;
                        if (obj == null) return;
                        obj.cod_estado = "PEN";
                        obj.cod_sede_empresa = obj.cod_sede_empresa;
                        string result = unit.CajaChica.Retornoaprerendicion(5, obj);
                        obj.valorantiguo = "RENDIDO"; obj.valoractual = "PRE-RENDICION"; obj.dsc_campo = "RETORNO A PRE-RENDICIÓN"; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        string result2 = unit.CajaChica.InsertarHistorialContableentregasrendir(obj);
                        if (result != "OK") HNG.MessageError("ERROR AL RETORNAR DOCUMENTO", "RETORNO DOCUMENTO");
                    }
                    HNG.MessageSuccess("Se Retorno el documento de manera exitosa", "RETORNO EXITOSO");
                    btnbuscar_Click(btnbuscar, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                HNG.MessageError("ERROR AL RETORNAR DOCUMENTO", "RETORNO DOCUMENTO");
            }
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridView view = new GridView();
            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                case "xtraTabPage1": view = gvFactura; break;
                case "xtraTabPage2": view = gvCajaChica; break;
                case "xtraTabPage3": view = gvEntregasRendir; break;
            }
            ExportarExcel(view);
        }

         private void ExportarExcel(GridView view)
        {
            try
            {
                string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\EntregasRendir" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                view.ExportToXlsx(archivo);
                if (HNG.MessageQuestion("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel") == DialogResult.Yes)
                {
                    Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "EXPORTAR EXCEL");
            }
        }

        private void btnEliminarPagoDetraccion_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvFactura.RefreshData();
            if (gvFactura.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (gvFactura.SelectedRowsCount == 1)
            {
                foreach (int nRow in gvFactura.GetSelectedRows())
                {
                    eFacturaProveedor objFP = gvFactura.GetRow(nRow) as eFacturaProveedor;
                    if (objFP.cod_estado_pago == "PAGADO"  && objFP.flg_detraccion=="SI")
                    {
                        if (HNG.MessageQuestion("El documento se encuentra Pagado" + "\n¿Desea Eliminar la Detracción?", "PAGO IDENTIFICADO") == DialogResult.Yes)
                        {
                            Actualizarprogramacionpagofactura(3, objFP.imp_total, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor);
                            Actualizarprogramacionpagofactura(9,objFP.detraccion_redondo, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor);
                            EliminarProgramacionDetraccion(7,objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor);
                            id_detalle = objFP.id_detalle;
                            valoresfactura("PAGO EJECUTADO: "+Convert.ToString(objFP.fch_pago_ejecutado.ToShortDateString()), "PAGO PROGRAMADO", objFP.cod_proveedor, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, "ELIMINACIÓN DE PAGO DETRACCION", cod_empresa);

                        }
                        else
                        {
                            return;
                        }
                    }


                }
                if (pagoejecutado == "SI") { HNG.MessageSuccess("Se elimino el pago ejecutado", "PAGO ELIMINADO"); btnbuscar_Click(btnbuscar, new EventArgs()); }
                else if(pagoejecutado == "") { HNG.MessageWarning("El documento no se encuentra Pagado", "ADVERTENCIA"); }
                    else{ HNG.MessageError("Error al eliminar pago ejecutado", "ERROR"); }

            }
        }

        private void gcFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) CargarListado();
        }

        private void xtraTabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) listacajachica();
        }

        private void gventregasrendir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) listarcuentasrendidas();
        }

        private void btnEliminarPagoRetencion_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvFactura.RefreshData();
            if (gvFactura.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Contabilizar documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (gvFactura.SelectedRowsCount == 1)
            {
                foreach (int nRow in gvFactura.GetSelectedRows())
                {
                    eFacturaProveedor objFP = gvFactura.GetRow(nRow) as eFacturaProveedor;
                    if (objFP.cod_estado_pago == "PAGADO"  && objFP.flg_retencion == "SI")
                    {
                        if (HNG.MessageQuestion("El documento se encuentra Pagado" + "\n¿Desea Eliminar la retención?", "PAGO IDENTIFICADO") == DialogResult.Yes)
                        {
                            Actualizarprogramacionpagofactura(3, objFP.imp_total, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor);
                            Actualizarprogramacionpagofactura(9, objFP.detraccion_redondo, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor);
                            EliminarProgramacionDetraccion(10,objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor);
                            id_detalle = objFP.id_detalle;
                            valoresfactura("PAGO EJECUTADO: " + Convert.ToString(objFP.fch_constancia_retencion.ToShortDateString()), "PAGO PROGRAMADO", objFP.cod_proveedor, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, "ELIMINACIÓN DE PAGO RETENCIÓN", cod_empresa);

                        }
                        else
                        {
                            return;
                        }
                    }


                }
                if (pagoejecutado == "SI") { HNG.MessageSuccess("Se elimino el pago ejecutado", "PAGO ELIMINADO"); btnbuscar_Click(btnbuscar, new EventArgs()); }
                else { HNG.MessageError("Error al eliminar pago ejecutado", "ERROR"); }

            }
        }

        private void gcFactura_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                btnPeriodoTributario.Enabled = true;
                btnEliminarUltimoPagoEjecutado.Enabled = true;
                btnRetornarReposicionCajaChica.Enabled = false;
                btnRetornarRendicionEntregaRendir.Enabled = false;
                btnEliminarPagoDetraccion.Enabled = true;
                btnEliminarPagoRetencion.Enabled = true;
            }
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            { 
                btnRetornarReposicionCajaChica.Enabled = true;
                btnPeriodoTributario.Enabled = false;
                btnEliminarUltimoPagoEjecutado.Enabled = false;
                btnRetornarRendicionEntregaRendir.Enabled = false;
                btnEliminarPagoDetraccion.Enabled= false;
                btnEliminarPagoRetencion.Enabled = false;
            }
            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                btnRetornarRendicionEntregaRendir.Enabled = true;
                btnPeriodoTributario.Enabled = false;
                btnEliminarUltimoPagoEjecutado.Enabled = false;
                btnRetornarReposicionCajaChica.Enabled = false;
                btnEliminarPagoDetraccion.Enabled = false;
                btnEliminarPagoRetencion.Enabled = false;
            }
        }

        private async Task Mover_Eliminar_ArchivoOneDrive(int nRow, DateTime FechaPeriodo, bool PDF, bool XML, string opcion)
        {
            try
            {
                eFacturaProveedor obj = gvFactura.GetRow(nRow) as eFacturaProveedor;
                obj.periodo_tributario = FechaPeriodo.ToString("MM-yyyy");
                if (gvFactura.SelectedRowsCount == 1 && (obj.periodo_tributario == null || obj.periodo_tributario == "")) { MessageBox.Show("Debe asignar un periodo tributario para mover los archivos adjuntos", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
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

        private void btnAuditoriaDetallada_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmAuditoriaContable frmModif = new frmAuditoriaContable();
            frmModif.empresa = cod_empresa;
            frmModif.ShowDialog();
        }

  
        private void listacajachica()
        {
            int nRowHandle = gvCajaChica.FocusedRowHandle;
            eCajaChica.eMovimiento_CajaChica obj = new eCajaChica.eMovimiento_CajaChica();
            obj = gvCajaChica.GetFocusedRow() as eCajaChica.eMovimiento_CajaChica;
            listarcajachica = unit.Aprobaciones.ListarCajachicaAuditoria<eCajaChica.eMovimiento_CajaChica>(40,cod_empresa:cod_empresa);
            bscajachica.DataSource = listarcajachica;
            gvCajaChica.FocusedRowHandle = nRowHandle;
        }

        private void listarcuentasrendidas()
        {
            int nRowHandle = gvEntregasRendir.FocusedRowHandle;
            eEntregaRendir.eDetalle_EntregaRendir obj = new eEntregaRendir.eDetalle_EntregaRendir();
            obj = gvEntregasRendir.GetFocusedRow() as eEntregaRendir.eDetalle_EntregaRendir;

            listarentregasrendiraprobados = unit.Aprobaciones.ListarEntregaRendirAuditoria<eEntregaRendir.eDetalle_EntregaRendir>(41,Convert.ToDateTime(dtFechaInicio.EditValue), Convert.ToDateTime(dtFechaFin.EditValue), cod_empresa);
            bsentregasarendir.DataSource = listarentregasrendiraprobados;
            gvEntregasRendir.FocusedRowHandle = nRowHandle;
        }

        private void valoresfactura(string valorantiguo, string valornuevo = "", string cod_proveedor = "", string tipo_documento = "", string serie_documento = "", decimal numero_documento = 0, string dsc_campo = "",string cod_empresa="")
        {
            eFacturaProveedor.eFacturaProveedor_Distribucion objantiguo = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            objantiguo.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            objantiguo.valorantiguo = valorantiguo;
            objantiguo.valoractual = valornuevo;
            objantiguo.cod_proveedor = cod_proveedor;
            objantiguo.tipo_documento = tipo_documento;
            objantiguo.serie_documento = serie_documento;
            objantiguo.numero_documento = numero_documento;
            objantiguo.dsc_campo = dsc_campo;
            objantiguo.cod_empresa= cod_empresa;
            objantiguo.id_detalle = id_detalle;

            eFacturaProveedor.eFacturaProveedor_Distribucion ehistorial = new eFacturaProveedor.eFacturaProveedor_Distribucion();
            ehistorial = unit.Factura.InsertarHistorialContable<eFacturaProveedor.eFacturaProveedor_Distribucion>(objantiguo);
            if (ehistorial != null) { pagoejecutado = "SI"; } else { pagoejecutado = "NO"; }
        }

    }
}
