using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE_BackOffice;
using BL_BackOffice;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Columns;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmFacturasDetalle : HNG_Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        public string cod_proveedor = "", cod_tipo_fecha = "", cod_moneda = "", cod_empresa = "", cod_tipo_documento = "", cod_bloque_pago = "";
        public int num_linea_banco = 0;
        public string FechaInicio, FechaFin;
        public bool BusquedaAutomatica = true, BusquedaBloquePago = false, BusquedaLogistica = false, MostrarProveedor = false;

        public List<eFacturaProveedor.eFacturaProveedor_NotaCredito> listDocumentosNC = new List<eFacturaProveedor.eFacturaProveedor_NotaCredito>();

        public frmFacturasDetalle()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            bgvProgramacionPagos.OptionsBehavior.Editable = true;
            //unit.Globales.ConfigurarGridView_ClasicStyle(gcProgramacionPagos, bgvProgramacionPagos);
            //bgvProgramacionPagos.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            //bgvProgramacionPagos.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            //bgvProgramacionPagos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            //bgvProgramacionPagos.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
        }

        private void frmFacturasDetalle_Load(object sender, EventArgs e)
        {
            if (BusquedaAutomatica) BuscarFacturas();
            if (BusquedaBloquePago) BuscarFacturasxBloquePago();
            if (!BusquedaAutomatica && !BusquedaBloquePago)
            {
                layoutControlItem10.Visibility = LayoutVisibility.Always;
                layoutControlItem6.Visibility = LayoutVisibility.Always;
                layoutControlItem23.Visibility = LayoutVisibility.Always;
                layoutControlItem7.Visibility = LayoutVisibility.Always;
                emptySpaceItem2.Visibility = LayoutVisibility.Always;
                //layoutControlItem9.Visibility = LayoutVisibility.Always;
                btnGuardar.Text = "BUSCAR";
                emptySpaceItem1.Visibility = LayoutVisibility.Always;
                emptySpaceItem3.Visibility = LayoutVisibility.Always;
                unit.Factura.CargaCombosChecked("TipoDocumento", chkcbTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "");
                unit.Factura.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);
                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtFechaInicio.EditValue = oPrimerDiaDelMes;
                dtFechaFin.EditValue = oUltimoDiaDelMes;
                //chkcbTipoDocumento.EditValue = cod_tipo_documento;
                chkcbTipoDocumento.SetEditValue("TC002");

                if (MostrarProveedor)
                {
                    foreach (GridColumn col in gvFacturasProveedor.Columns)
                    {
                        col.Visible = false;
                        if (col.FieldName == "dsc_tipo_documento" || col.FieldName == "dsc_documento" || col.FieldName == "dsc_glosa" ||
                            col.FieldName == "fch_documento" || col.FieldName == "cod_moneda" || col.FieldName == "imp_tipocambio" ||
                            col.FieldName == "imp_subtotal" || col.FieldName == "imp_igv" || col.FieldName == "imp_total" ||
                            col.FieldName == "imp_saldo" || col.FieldName == "dsc_proveedor") { col.Visible = true; }
                    }
                    gvFacturasProveedor.Columns["dsc_tipo_documento"].VisibleIndex = 0;
                    gvFacturasProveedor.Columns["dsc_documento"].VisibleIndex = 1;
                    gvFacturasProveedor.Columns["dsc_glosa"].VisibleIndex = 2;
                    gvFacturasProveedor.Columns["dsc_proveedor"].VisibleIndex = 3;
                    gvFacturasProveedor.Columns["fch_documento"].VisibleIndex = 4;
                    gvFacturasProveedor.Columns["cod_moneda"].VisibleIndex = 5;
                    gvFacturasProveedor.Columns["imp_tipocambio"].VisibleIndex = 6;
                    gvFacturasProveedor.Columns["imp_subtotal"].VisibleIndex = 7;
                    gvFacturasProveedor.Columns["imp_igv"].VisibleIndex = 8;
                    gvFacturasProveedor.Columns["imp_total"].VisibleIndex = 9;
                    gvFacturasProveedor.Columns["imp_saldo"].VisibleIndex = 10;
                }
                //btnBuscar_Click(btnBuscar, new EventArgs());
                btnGuardar_Click(btnGuardar, new EventArgs());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
                List<eFacturaProveedor> lista = new List<eFacturaProveedor>();
                if (!BusquedaLogistica)
                {
                    lista = unit.Factura.FiltroFactura<eFacturaProveedor>(1, cod_empresa, chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(), cod_tipo_fecha: lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(), FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), cod_proveedor: cod_proveedor, cod_moneda: cod_moneda, flg_CajaChica: "", flg_EntregasRendir: "");
                }
                else
                {
                    lista = unit.Logistica.Obtener_ListaLogistica<eFacturaProveedor>(18, "", cod_empresa, chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(), cod_tipo_fecha: lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(), FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), cod_proveedor: cod_proveedor, cod_moneda: cod_moneda);
                }
                bsFacturasProveedor.DataSource = lista;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }

        private void gvFacturasProveedor_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (!BusquedaAutomatica && !BusquedaBloquePago)
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor;
                    eFacturaProveedor.eFacturaProveedor_NotaCredito eNC = new eFacturaProveedor.eFacturaProveedor_NotaCredito();
                    eNC.tipo_documento = obj.tipo_documento; eNC.serie_documento = obj.serie_documento;
                    eNC.numero_documento = obj.numero_documento; eNC.cod_proveedor = obj.cod_proveedor;
                    eNC.dsc_glosa = obj.dsc_glosa; eNC.dsc_tipo_documento = obj.dsc_tipo_documento;
                    eNC.fch_documento = obj.fch_documento; eNC.cod_moneda = obj.cod_moneda; eNC.dsc_documento = obj.dsc_documento;
                    eNC.imp_tipocambio = obj.imp_tipocambio; eNC.imp_subtotal = obj.imp_subtotal;
                    eNC.imp_igv = obj.imp_igv; eNC.imp_total = obj.imp_total; eNC.imp_saldo = obj.imp_saldo;
                    listDocumentosNC.Add(eNC);
                    this.Close();
                }
            }
        }

        private void frmFacturasDetalle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        public List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!BusquedaAutomatica && !BusquedaBloquePago)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
                    if (!BusquedaLogistica)
                    {
                        lista = unit.Factura.FiltroFactura<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(1, cod_empresa, chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(), cod_tipo_fecha: lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(), FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), cod_proveedor: cod_proveedor, cod_moneda: cod_moneda);
                    }
                    else
                    {
                        lista = unit.Logistica.Obtener_ListaLogistica<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(18, "", cod_empresa, chkcbTipoDocumento.EditValue == null ? "" : chkcbTipoDocumento.EditValue.ToString(), cod_tipo_fecha: lkpTipoFecha.EditValue == null ? "" : lkpTipoFecha.EditValue.ToString(), FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), cod_proveedor: cod_proveedor, cod_moneda: cod_moneda);
                    }
                    bsFacturasProveedor.DataSource = lista;
                    SplashScreenManager.CloseForm();
                }
                if (BusquedaBloquePago)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
                    bgvProgramacionPagos.PostEditor(); bgvProgramacionPagos.RefreshData();
                    for (int x = 0; x <= bgvProgramacionPagos.RowCount; x++)
                    {
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                        if (obj == null) continue;
                        eFacturaProveedor objDetRet = new eFacturaProveedor();
                        objDetRet.tipo_documento = obj.tipo_documento; objDetRet.serie_documento = obj.serie_documento;
                        objDetRet.numero_documento = obj.numero_documento; objDetRet.cod_proveedor = obj.cod_proveedor; 
                        objDetRet.flg_detraccion = obj.flg_detraccion; objDetRet.num_constancia_detraccion = obj.num_constancia_detraccion;
                        objDetRet.fch_constancia_detraccion = obj.fch_constancia_detraccion; objDetRet.fch_pago_ejecutado_detraccion = obj.fch_pago_ejecutado_detraccion;
                        objDetRet.flg_detraccion_aplicada = "SI";
                        objDetRet.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        string result = unit.Factura.AplicarDetraccionMasiva(objDetRet, "DETRACC");
                        if (result != "OK") { HNG.MessageError("Error al aplicar detracción", "ERROR"); continue; }
                        if (result == "OK")
                        {
                            string resultC = unit.Factura.AplicarDetraccionCONCAR(obj.cod_empresa, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.dsc_ruc, obj.num_constancia_detraccion, obj.fch_pago_ejecutado_detraccion, obj.fch_documento);
                            if (resultC != "OK") { HNG.MessageError("Error al aplicar detracción", "ERROR"); continue; }
                        }
                    }
                    SplashScreenManager.CloseForm();
                    HNG.MessageSuccess("Se actualizaron los documentos de manera satisfactoria.", "Actualizar Detracción");
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }

        private void bgvProgramacionPagos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle != 0) return;
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                if (obj == null) return;
                if (e.Column.FieldName == "num_constancia_detraccion")
                {
                    int num_correlativo = 0;
                    num_correlativo = Convert.ToInt32(obj.num_constancia_detraccion);
                    for (int x = 1; x <= bgvProgramacionPagos.RowCount; x++)
                    {
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj2 = bgvProgramacionPagos.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                        if (obj2 == null) continue;
                        num_correlativo += 1;
                        obj2.num_constancia_detraccion = num_correlativo.ToString();
                    }
                    bgvProgramacionPagos.RefreshData();
                }
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }

        private void btnOpcional_Click(object sender, EventArgs e)
        {
            try
            {
                bgvProgramacionPagos.RefreshData(); bgvProgramacionPagos.PostEditor();
                List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDet = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                //List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                //lista = lista.FindAll(x => x.Sel && x.cod_estado != "EJE");
                //if (lista.Count == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                //if (lista.Count == 1 && lista[0].num_linea == 0) { MessageBox.Show("No hay una programación de pago registrada", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                ////if (lista.Count == 1 && lista[0].cod_estado == "EJE") { MessageBox.Show("El pago ya esta ejecutado", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                ////if (bgvProgramacionPagos.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un registro.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                //if (lista.FindAll(x => x.num_linea_banco == 0).Count > 0) { MessageBox.Show("Hay registros sin asignar el BANCO ORIGEN.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                //if (lista.FindAll(x => x.num_linea_banco_prov == 0).Count > 0) { MessageBox.Show("Hay registros sin asignar el BANCO DESTINO.", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                //if (lista.Count == lista.FindAll(x => x.cod_estado == "EJE").Count) { MessageBox.Show("Todos los pago ya esta ejecutados", "Programación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eObj in lista)
                {
                    eObj.dsc_proveedor = eObj.dsc_proveedor.Replace("Ñ", "N").Replace("ñ", "n");
                }

                //frmFacturaPagoBanco frmFact = new frmFacturaPagoBanco();
                //frmFact.Text = "Listado de Factura Pago Banco";
                //frmFact.listDocumentos = lista;
                ////new ToolHelper.Forms().ShowDialog(frmFact);
                //frmFact.ShowDialog();
                //if (frmFact.listDocumentos.Count > 0 && frmFact.GuardarDatos == "SI")
                //{

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
                    decimal imp_montoPago = 0; Double suma_cabecera1 = 0, suma_cabecera2 = 0; string cod_bloque_pago = "", nuevo_bloque_pago = "NO";
                    string tipo_cuenta, nro_cta_abono, tipo_doc, moneda, nro_cta_cargo, sum_cabecera, espacios = " ";
                    foreach (int banco in listBancos)
                    {
                        //////////FILTRA A LOS DOCUMENTOS POR CADA BANCO
                        List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDoc = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                        //listaDoc = lista.FindAll(x => x.num_linea_banco == banco && x.imp_saldo > 0 && x.cod_estado != "EJE" && x.num_linea > 0);
                        listaDoc = lista.FindAll(x => x.num_linea_banco == banco && x.num_linea > 0);

                        if (listaDoc[0].cod_banco_empresa == "BA006")//SCOTIABANK
                        {
                            //////////FILTRA A LOS DOCUMENTOS PARA SCOTIABANK RXH
                            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDocRXH = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                            listaDocRXH = listaDoc.FindAll(x => x.tipo_documento == "TC008");
                            if (listaDocRXH.Count > 0)
                            {
                                cod_bloque_pago = ""; nuevo_bloque_pago = "SI";
                                //////////GENERA EL TXT DE PAGOS
                                StreamWriter sw = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\SCOTIABANK - FORMATO VARIOS - PAGO RXH.txt");
                                foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDocRXH)
                                {
                                    if (obj == null) continue;
                                    //if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                                    //obj.cod_estado = "EJE"; obj.cod_bloque_pago = cod_bloque_pago;
                                    //obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                    //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                                    ////eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                                    //eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj, nuevo_bloque_pago);
                                    //if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //cod_bloque_pago = cod_bloque_pago == "" ? eProgFact.cod_bloque_pago : cod_bloque_pago;
                                    //nuevo_bloque_pago = "NO"; obj.cod_bloque_pago = cod_bloque_pago;
                                    if (obj.cod_tipo_prog == "DETRACC") { listaDet.Add(obj); continue; }

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

                                //////////SUMA TOTAL DE LOS DOCUMENTOS DE LA CUENTA BANCARIA
                                //decimal imp_montoPROV = (from item in lista where item.num_linea_banco == banco.Value select item.imp_total).Sum();
                                imp_montoPago = listaDocRXH.Select(y => y.imp_pago).Sum();
                                //if (imp_montoPago > 0)
                                //{
                                //    //////////INSERTA EN LOS MOVIMIENTOS DE LA CUENTA BANCARIA
                                //    eEmpresa.eDetalleMovimientoBanco_Empresa objB = new eEmpresa.eDetalleMovimientoBanco_Empresa();
                                //    objB.cod_empresa = listaDocRXH[0].cod_empresa; objB.num_linea = (Int32)banco; objB.num_item = 0;
                                //    objB.fch_ejecutada = listaDocRXH[0].fch_ejecucion; objB.fch_efectiva = DateTime.Today; objB.cod_tipo_movimiento = "SALIDA";
                                //    objB.cod_origen_movimiento = "001"; objB.imp_monto = imp_montoPago; objB.cod_bloque_pago = cod_bloque_pago;
                                //    objB.flg_identificado = "SI"; objB.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                //    eEmpresa.eDetalleMovimientoBanco_Empresa objPago = unit.Factura.Insertar_Actualizar_DetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(objB);
                                //    if (objPago == null) { MessageBox.Show("Error al guardar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                                //}
                            }

                            //////////FILTRA A LOS DOCUMENTOS PARA SCOTIABANK DIFERENTES A RXH
                            List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listaDocSinRXH = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
                            listaDocSinRXH = listaDoc.FindAll(x => x.tipo_documento != "TC008");
                            if (listaDocSinRXH.Count > 0)
                            {
                                cod_bloque_pago = ""; nuevo_bloque_pago = "SI";
                                //////////GENERA DETALLE DE TXT DE PAGOS
                                StreamWriter sw2 = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_PagoBancos\\SCOTIABANK - FORMATO PROVEEDORES.txt");
                                foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listaDocSinRXH)
                                {
                                    if (obj == null) continue;
                                    //if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                                    //obj.cod_estado = "EJE"; obj.cod_bloque_pago = cod_bloque_pago;
                                    //obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                    //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                                    ////eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                                    //eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj, nuevo_bloque_pago);
                                    //if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //cod_bloque_pago = cod_bloque_pago == "" ? eProgFact.cod_bloque_pago : cod_bloque_pago;
                                    //nuevo_bloque_pago = "NO"; obj.cod_bloque_pago = cod_bloque_pago;
                                    if (obj.cod_tipo_prog == "DETRACC") { listaDet.Add(obj); continue; }

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

                                //////////SUMA TOTAL DE LOS DOCUMENTOS DE LA CUENTA BANCARIA
                                //decimal imp_montoPROV = (from item in lista where item.num_linea_banco == banco.Value select item.imp_total).Sum();
                                imp_montoPago = listaDocSinRXH.Select(y => y.imp_pago).Sum();
                                //if (imp_montoPago > 0)
                                //{
                                //    //////////INSERTA EN LOS MOVIMIENTOS DE LA CUENTA BANCARIA
                                //    eEmpresa.eDetalleMovimientoBanco_Empresa objB = new eEmpresa.eDetalleMovimientoBanco_Empresa();
                                //    objB.cod_empresa = listaDocSinRXH[0].cod_empresa; objB.num_linea = (Int32)banco; objB.num_item = 0;
                                //    objB.fch_ejecutada = listaDocSinRXH[0].fch_ejecucion; objB.fch_efectiva = DateTime.Today; objB.cod_tipo_movimiento = "SALIDA";
                                //    objB.cod_origen_movimiento = "001"; objB.imp_monto = imp_montoPago; objB.cod_bloque_pago = cod_bloque_pago;
                                //    objB.flg_identificado = "SI"; objB.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                //    eEmpresa.eDetalleMovimientoBanco_Empresa objPago = unit.Factura.Insertar_Actualizar_DetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(objB);
                                //    if (objPago == null) { MessageBox.Show("Error al guardar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                                //}
                            }
                        }

                        if (listaDoc[0].cod_banco_empresa == "BA001")//BCP
                        {
                            cod_bloque_pago = ""; nuevo_bloque_pago = "SI";
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
                                if (obj == null) continue;
                                //if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                                //obj.cod_estado = "EJE"; obj.cod_bloque_pago = cod_bloque_pago;
                                //obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                                ////eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                                //eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj, nuevo_bloque_pago);
                                //if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //cod_bloque_pago = cod_bloque_pago == "" ? eProgFact.cod_bloque_pago : cod_bloque_pago;
                                //nuevo_bloque_pago = "NO"; obj.cod_bloque_pago = cod_bloque_pago;
                                if (obj.cod_tipo_prog == "DETRACC") { listaDet.Add(obj); continue; }

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

                            //////////SUMA TOTAL DE LOS DOCUMENTOS DE LA CUENTA BANCARIA
                            //decimal imp_montoPROV = (from item in lista where item.num_linea_banco == banco.Value select item.imp_total).Sum();
                            imp_montoPago = listaDoc.Select(y => y.imp_pago).Sum();
                            //if (imp_montoPago > 0)
                            //{
                            //    //////////INSERTA EN LOS MOVIMIENTOS DE LA CUENTA BANCARIA
                            //    eEmpresa.eDetalleMovimientoBanco_Empresa objB = new eEmpresa.eDetalleMovimientoBanco_Empresa();
                            //    objB.cod_empresa = listaDoc[0].cod_empresa; objB.num_linea = (Int32)banco; objB.num_item = 0;
                            //    objB.fch_ejecutada = listaDoc[0].fch_ejecucion; objB.fch_efectiva = listaDoc[0].fch_ejecucion; objB.cod_tipo_movimiento = "SALIDA";
                            //    objB.cod_origen_movimiento = "001"; objB.imp_monto = imp_montoPago; objB.cod_bloque_pago = cod_bloque_pago;
                            //    objB.flg_identificado = "SI"; objB.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                            //    eEmpresa.eDetalleMovimientoBanco_Empresa objPago = unit.Factura.Insertar_Actualizar_DetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(objB);
                            //    if (objPago == null) { MessageBox.Show("Error al guardar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            //}

                        }
                    }

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
                            if (obj == null) continue;
                            ////if (obj.imp_saldo == 0 || obj.cod_estado == "EJE" || obj.num_linea == 0) continue;
                            //obj.cod_estado = "EJE"; obj.cod_bloque_pago = cod_bloque_pago;
                            //obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                            //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                            ////eProgFact = unit.Factura.ActualizarEjecutarPago<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
                            //eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj, nuevo_bloque_pago);
                            //if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //cod_bloque_pago = cod_bloque_pago == "" ? eProgFact.cod_bloque_pago  : cod_bloque_pago;
                            //nuevo_bloque_pago = "NO"; obj.cod_bloque_pago = cod_bloque_pago;

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


                        //frmFacturasConstanciaDetraccRetenc frmDetRet = new frmFacturasConstanciaDetraccRetenc();
                        //frmDetRet.listFacturas = listaDet;
                        //frmDetRet.ShowDialog();
                    }

                    //ExportarReportePagoBancos(lista, glosaPrincipal);
                    Process.Start(@"C:\IMPERIUM-Software\Recursos\ArchivosExportados\TXT_PagoBancos\");
                    SplashScreenManager.CloseForm();
                //}
                //else
                //{
                //    MessageBox.Show("Proceso cancelado de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}

                //BuscarFacturas();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }

        private void gvFacturasProveedor_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvFacturasProveedor_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
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
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }
        
        private void bgvProgramacionPagos_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                if (objProg.cod_tipo_prog == "CAJACHICA" || objProg.cod_tipo_prog == "ENTREGARENDIR") return;
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) return;

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
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
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }

        private void gvFacturasProveedor_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    eFacturaProveedor obj = gvFacturasProveedor.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.RUC = obj.dsc_ruc;
                    frmModif.tipo_documento = obj.tipo_documento;
                    frmModif.serie_documento = obj.serie_documento;
                    frmModif.numero_documento = obj.numero_documento;
                    frmModif.cod_proveedor = obj.cod_proveedor;
                        
                    frmModif.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }

        public void BuscarFacturas()
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
                List<eFacturaProveedor> lista = unit.Factura.FiltroFactura<eFacturaProveedor>(1, cod_tipo_fecha: cod_tipo_fecha, FechaInicio: FechaInicio, FechaFin: FechaFin, cod_proveedor: cod_proveedor, cod_moneda: cod_moneda);
                bsFacturasProveedor.DataSource = lista;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }

        public void BuscarFacturasxBloquePago()
        {
            try
            {
                layoutControlItem1.Visibility = LayoutVisibility.Never; layoutControlItem2.Visibility = LayoutVisibility.Always;
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");
                //List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = unit.Factura.Obtener_CuentasBancariasEmpresa<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(3, "", num_linea_banco, cod_bloque_pago);
                lista = unit.Factura.Obtener_CuentasBancariasEmpresa<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(3, cod_empresa, num_linea_banco, cod_bloque_pago);
                bsProgramacionPagos.DataSource = lista;
                if (lista.FindAll(x => x.dsc_tipo_prog == "DETRACCIÓN").Count <= 0)
                {
                    foreach (GridColumn col in bgvProgramacionPagos.Columns)
                    {
                        if (col.FieldName == "num_constancia_detraccion" ) col.Visible = false;
                    }
                    //this.VisibleFooter = false;
                    this.CancelVisible = false;
                    this.GuardarVisible = false;
                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }
    }
}