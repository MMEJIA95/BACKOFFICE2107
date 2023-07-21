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
using BL_BackOffice;
using UI_BackOffice.Formularios.Cuentas_Pagar;
using UI_BackOffice.Formularios.Shared;
using Microsoft.Identity.Client;
using DevExpress.XtraSplashScreen;
using System.IO;
using System.Security;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using BE_BackOffice.DTOs;
using DevExpress.Xpo;

namespace UI_BackOffice.Formularios.Logistica
{
    internal enum GuiaRemision
    {
        Nuevo = 1,
        Editar = 2,
        Vista = 3
    }

    public partial class frmRegistrarGuiaRemisionAlmacen : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        internal GuiaRemision MiAccion = GuiaRemision.Nuevo;
        List<eAlmacen.eProductos_Almacen> listaProd = new List<eAlmacen.eProductos_Almacen>();
        string fmt_nro_doc = "";
        Int16 num_ctd_serie, num_ctd_doc;
        public decimal numero_documento = 0;
        public string cod_empresa = "", cod_sede_empresa = "", cod_almacen = "", cod_guiaremision = "", cod_requerimiento = "", flg_solicitud = "", dsc_anho = "0";
        public string tipo_documento = "", serie_documento = "", TD_sunat = "";
        public bool ActualizarListado = false;

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "", varNombreArchivoSinExtension = "";


        eParametrosGenerales objBloq = new eParametrosGenerales();
        string ceco_seleccionado = "";

        public frmRegistrarGuiaRemisionAlmacen()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmRegistrarEntrada_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            dtFechaDocumento.EditValue = DateTime.Today;
            unit.Logistica.CargaCombosLookUp("Almacen", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa);
            unit.Logistica.CargaCombosLookUp("TipoMovimiento", lkpTipoMovimiento, "cod_tipo_movimiento", "dsc_tipo_movimiento", "", valorDefecto: true, dsc_variable: "GUIA_REMISION", cod_empresa: cod_empresa);
            unit.Logistica.CargaCombosLookUp("TipoMovimiento", lkpMotivoTraslado, "cod_tipo_movimiento", "dsc_tipo_movimiento", "", valorDefecto: true, dsc_variable: "TRASLADO", cod_empresa: cod_empresa);

            switch (MiAccion)
            {
                case GuiaRemision.Nuevo:
                    dtFechaDocumento.EditValue = DateTime.Today;
                    dtFechaTraslado.EditValue = DateTime.Today;
                    lkpAlmacen.EditValue = cod_almacen; lkpTipoMovimiento.EditValue = "009"; lkpMotivoTraslado.EditValue = "014";
                    MostrarCorrelativoComputarizado();

                    if (cod_requerimiento != "")
                    {
                        eRequerimiento eReq = unit.Requerimiento.Cargar_Requerimiento<eRequerimiento>(4, cod_empresa, cod_sede_empresa, cod_requerimiento, flg_solicitud, Convert.ToInt32(dsc_anho));
                        txtNroRequerimiento.Text = cod_requerimiento;
                        txtGlosaRequerimiento.Text = eReq.dsc_solicitante;
                        txtDireccion.Text = eReq.dsc_direccion_cliente;
                        // lkpDistribucionCECO.EditValue = eReq.cod_CECO;
                        // unit.Factura.CargaCombosLookUp("DistribucionCECO", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa, cod_cliente: eReq.cod_cliente);
                        ceco_seleccionado = eReq.cod_CECO;
                        CargarComboCECOS(eReq.fch_requerimiento.ToString());

                        List<eFacturaProveedor.eFacturaProveedor_Distribucion> listCECOS = unit.Factura.ObtenerListadoCECOS<eFacturaProveedor.eFacturaProveedor_Distribucion>(32, cod_empresa, eReq.cod_cliente);
                        if (listCECOS.Count == 1) lkpDistribucionCECO.EditValue = listCECOS[0].cod_CECO;

                        listaProd = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(26, lkpAlmacen.EditValue.ToString(), cod_empresa, cod_sede_empresa, cod_requerimiento: cod_requerimiento);
                        bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();


                    }
                    break;
                case GuiaRemision.Vista:
                    CargarComboCECOS(DateTime.Now.ToString());// ??donde colocar?
                    ObtenerDatos_GuiaRemision();
                    BloqueoControles(false, true, false);
                    gvListadoProductos.Columns["num_cantidad_stock"].Visible = false;
                    gvListadoProductos.Columns["num_cantidad_stock_nuevo"].Visible = false;
                    btnAdjuntarArchivo.Enabled = true;
                    btnVerPDF.Enabled = true;

                    //Check para habilitar correlativo manual, en edición estará deshabilitado.
                    cbxCorrelativoManual.Enabled = false;
                    break;
            }
        }

        private void MostrarCorrelativoComputarizado()
        {
            //Mostrar correlativo computarizado:
            var correlativo = unit.Logistica.ObtenerCorrelativoLogistica(
                variable: "GR",
                codEmpresa: cod_empresa,
                codSedeEmpresa: cod_sede_empresa);
            lblCorrelativo.Text = $"Correlativo computarizado: [ {correlativo} ]";
            //txtCodigo.Text = correlativo.ToString();
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnGuardar.Enabled = Enabled;
            txtCodigo.ReadOnly = ReadOnly;
            lkpAlmacen.ReadOnly = ReadOnly;
            lkpTipoMovimiento.ReadOnly = ReadOnly;
            dtFechaDocumento.ReadOnly = ReadOnly;
            txtNroRequerimiento.ReadOnly = ReadOnly;
            txtGlosaRequerimiento.ReadOnly = ReadOnly;
            dtFechaTraslado.ReadOnly = ReadOnly;
            lkpDistribucionCECO.ReadOnly = ReadOnly;
            txtPlacaTransportista.ReadOnly = ReadOnly;
            txtTransportista.ReadOnly = ReadOnly;
            txtDireccion.ReadOnly = ReadOnly;
            lkpMotivoTraslado.ReadOnly = ReadOnly;
            picBuscarTransportista.Enabled = Enabled;
            picBuscarRequerimiento.Enabled = Enabled;
            gvListadoProductos.OptionsBehavior.Editable = Editable;
        }

        private void ObtenerDatos_GuiaRemision()
        {
            eAlmacen.eGuiaRemision_Cabecera obj = unit.Logistica.Obtener_DatosLogistica<eAlmacen.eGuiaRemision_Cabecera>(24, cod_almacen, cod_empresa, cod_sede_empresa, cod_guiaremision: cod_guiaremision);
            if (obj == null) return;

            txtCodigo.Text = obj.cod_guiaremision;
            lkpAlmacen.EditValue = obj.cod_almacen;
            lkpTipoMovimiento.EditValue = obj.cod_tipo_movimiento;
            dtFechaDocumento.EditValue = obj.fch_documento;
            txtNroRequerimiento.Text = obj.cod_requerimiento;
            txtGlosaRequerimiento.Text = obj.dsc_solicitante;
            dtFechaTraslado.EditValue = obj.fch_traslado;
            lkpDistribucionCECO.EditValue = obj.dsc_pref_ceco;
            txtPlacaTransportista.Text = obj.placa_transportista;
            txtRucTransportista1.Text = obj.ruc_transportista;
            txtTransportista.Tag = obj.cod_transportista;
            txtTransportista.Text = obj.dsc_transportista;
            txtDireccion.EditValue = obj.dsc_direccion;
            lkpMotivoTraslado.EditValue = obj.cod_motivo_traslado;
            tipo_documento = obj.tipo_documento;
            serie_documento = obj.serie_documento;
            numero_documento = Convert.ToDecimal(obj.cod_guiaremision);

            listaProd = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(25, cod_almacen, cod_empresa, cod_sede_empresa, cod_guiaremision: cod_guiaremision);
            bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
        }

        private void CargarComboCECOS(string date)
        {
            objBloq.valor_1 = cod_empresa;
            objBloq = unit.Factura.Obtener_BloqueoCECOxEmpresa<eParametrosGenerales>(64, objBloq);
            if (objBloq == null)
            {
                HNG.MessageWarning("No se ha encontrado los parámetros, vuelve a intentarlo.", "Cargar CECOS");
                return;
            }

            if (objBloq.valor_2 == "NO" && Convert.ToDateTime(date).Year < 2023) //  cambiar por fecha de registro.
            {
                unit.Factura.CargaCombosLookUp("DistribucionCECO", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa);
            }
            else
            {
                unit.Factura.CargaCombosLookUp("DistribucionCECO_Nuevo", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa);
            }

            lkpDistribucionCECO.EditValue = ceco_seleccionado;
        }

        private void frmRegistrarGuiaRemisionAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion != GuiaRemision.Nuevo) this.Close();
        }

        private void picBuscarRequerimiento_Click(object sender, EventArgs e)
        {
            //string doc_referencia = "";

            //frmOpcionDocReferencia frm = new frmOpcionDocReferencia();
            //frm.ShowDialog();
            //doc_referencia = frm.doc_referencia;
            //switch (doc_referencia)
            //{
            //    case "01": Busqueda("", "OrdenesCompra"); break;
            //    case "02": Buscar_DocReferencia();  break;
            //}
            Busqueda("", "Requerimiento");
        }

        private void picBuscarTransportista_Click(object sender, EventArgs e)
        {
            Busqueda("", "Transportista");
        }

        private void btnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MiAccion == GuiaRemision.Nuevo)
            {
                eAlmacen.eProductos_Almacen objP = gvListadoProductos.GetFocusedRow() as eAlmacen.eProductos_Almacen;
                listaProd.Remove(objP);
                int n_Orden = 1;
                foreach (eAlmacen.eProductos_Almacen obj in listaProd)
                {
                    obj.n_Orden = n_Orden;
                    n_Orden += 1;
                }
                bsListadoProductos.DataSource = listaProd;
                gvListadoProductos.RefreshData();
            }
        }

        private void dtFechaTraslado_EditValueChanged(object sender, EventArgs e)
        {
            //CargarComboCECOS(dtFechaRequerimiento.Text); ?? Activar cambio de CECOS?
            CargarComboCECOS(dtFechaTraslado.Text);
        }

        private void cbxCorrelativoManual_CheckedChanged(object sender, EventArgs e)
        {
            txtCodigo.ReadOnly = !cbxCorrelativoManual.Checked;
            txtCodigo.Text = cbxCorrelativoManual.Checked ? lblCorrelativo.Text.Split('[')[1].Split(']')[0] : "";
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSaltoCorrelativos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmCorrelativoSlotVacios(
                codEmpresa: cod_empresa,
                unitOfWork: unit,
                tipoDocumento: "Guia_Remision");
            f.Text = "Correlativos";
            f.ShowDialog();
        }

        private void btnAdjuntarArchivo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnVerPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        public void Busqueda(string dato, string tipo)
        {
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }

            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;

            switch (tipo)
            {
                case "Requerimiento":
                    frm.entidad = frmBusquedas.MiEntidad.Requerimiento;
                    frm.cod_almacen = cod_almacen;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = cod_sede_empresa;
                    frm.filtro = dato;
                    break;
                case "Transportista":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.flg_transportista = "SI";
                    frm.cod_empresa = cod_empresa;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Requerimiento":
                    //txtNroRequerimiento.Text = frm.codigo;
                    //txtGlosaRequerimiento.Tag = frm.cod_condicion1;
                    //txtGlosaRequerimiento.Text = frm.descripcion;
                    //tipo_documento_REFERENCIA = ""; serie_documento_REFERENCIA = ""; numero_documento_REFERENCIA = 0;
                    txtNroRequerimiento.Text = frm.codigo;
                    txtGlosaRequerimiento.Text = frm.descripcion;
                    txtDireccion.Text = frm.dsc_condicion2;
                    unit.Factura.CargaCombosLookUp("DistribucionCECO", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa, cod_cliente: frm.dsc_condicion1);
                    listaProd = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(26, lkpAlmacen.EditValue.ToString(), cod_empresa, cod_sede_empresa, cod_requerimiento: frm.codigo);
                    bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
                    break;
                case "Transportista":
                    txtPlacaTransportista.Text = frm.cod_condicion1;
                    txtRucTransportista1.Text = frm.ruc;
                    txtTransportista.Tag = frm.codigo;
                    txtTransportista.Text = frm.descripcion;
                    break;
            }
        }

        private void Buscar_DocReferencia()
        {
            frmFacturasDetalle frm = new frmFacturasDetalle();
            frm.cod_empresa = cod_empresa;
            frm.cod_proveedor = "";
            frm.cod_moneda = "SOL";
            frm.BusquedaLogistica = true;
            frm.MostrarProveedor = true;
            frm.ShowDialog();
            if (frm.listDocumentosNC.Count > 0)
            {
                eFacturaProveedor eFact = new eFacturaProveedor();
                eFact = unit.Factura.ObtenerFacturaProveedor<eFacturaProveedor>(2, frm.listDocumentosNC[0].tipo_documento, frm.listDocumentosNC[0].serie_documento, frm.listDocumentosNC[0].numero_documento, frm.listDocumentosNC[0].cod_proveedor);

                eTipoComprobante obj = new eTipoComprobante();
                obj = unit.Factura.BuscarTipoComprobante<eTipoComprobante>(27, eFact.tipo_documento);
                num_ctd_serie = obj.num_ctd_serie; num_ctd_doc = obj.num_ctd_doc;
                fmt_nro_doc = new string('0', num_ctd_doc);

                //tipo_documento_REFERENCIA = eFact.tipo_documento;
                //serie_documento_REFERENCIA = eFact.serie_documento;
                //numero_documento_REFERENCIA = eFact.numero_documento; 
                txtNroRequerimiento.Text = eFact.serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", eFact.numero_documento);
                txtGlosaRequerimiento.Tag = eFact.cod_proveedor;
                txtGlosaRequerimiento.Text = eFact.dsc_proveedor;
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                if (lkpTipoMovimiento.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de movimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoMovimiento.Focus(); return; }
                if (txtNroRequerimiento.Text.Trim() == "") { MessageBox.Show("Debe seleccionar el requerimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroRequerimiento.Focus(); return; }
                if (lkpDistribucionCECO.EditValue == null) { MessageBox.Show("Debe seleccionar el centro de costo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpDistribucionCECO.Focus(); return; }
                //if (txtTransportista.Tag.ToString().Trim() == "") { MessageBox.Show("Debe seleccionar el transportista.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtTransportista.Focus(); return; }
                if (txtDireccion.Text.Trim() == "") { MessageBox.Show("Debe la dirección.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDireccion.Focus(); return; }
                if (lkpMotivoTraslado.EditValue == null) { MessageBox.Show("Debe seleccionar el motivo de traslado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpMotivoTraslado.Focus(); return; }
                int nTotal = 0;
                foreach (eAlmacen.eProductos_Almacen obj in listaProd)
                {
                    if (obj.num_cantidad_stock_nuevo < 0) nTotal = nTotal + 1;
                }
                if (nTotal > 0) { MessageBox.Show("La cantidad del producto no puede ser mayor a la del stock actual.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                eAlmacen.eGuiaRemision_Cabecera eGuia = AsignarValores_Cabecera();

                // Validar si se ha selecionado el transportista: es importante
                if (string.IsNullOrEmpty(eGuia.cod_transportista))
                {
                    HNG.MessageWarning("No se ha seleccionado los datos del transportista, este dato es importante.", "Guia");
                    return;
                }

                var result = unit.Logistica.Insertar_Actualizar_GuiaRemisionCabecera<GuiaRemisionCreateDTO>(eGuia);
                eGuia.cod_guiaremision = result.Entity.cod_guiaremision;
                eGuia.cod_almacen = result.Entity.cod_almacen;
                eGuia.cod_empresa = result.Entity.cod_empresa;
                eGuia.cod_sede_empresa = result.Entity.cod_sede_empresa;
                eGuia.cod_requerimiento = result.Entity.cod_requerimiento;

                //eGuia = unit.Logistica.Insertar_Actualizar_GuiaRemisionCabecera<eAlmacen.eGuiaRemision_Cabecera>(eGuia);
                if (result.Success)
                //if (eGuia != null)
                {
                    txtCodigo.Text = eGuia.cod_guiaremision;
                    if (gvListadoProductos.RowCount > 0)
                    {
                        for (int nRow = 0; nRow < gvListadoProductos.RowCount; nRow++)
                        {
                            eAlmacen.eProductos_Almacen eProd = gvListadoProductos.GetRow(nRow) as eAlmacen.eProductos_Almacen;
                            if (eProd.num_cantidad == 0) continue;
                            eAlmacen.eGuiaRemision_Detalle eDet = new eAlmacen.eGuiaRemision_Detalle();
                            eDet.cod_guiaremision = eGuia.cod_guiaremision;
                            eDet.cod_almacen = cod_almacen;
                            eDet.cod_empresa = cod_empresa;
                            eDet.cod_sede_empresa = cod_sede_empresa;
                            eDet.cod_tipo_servicio = eProd.cod_tipo_servicio;
                            eDet.cod_subtipo_servicio = eProd.cod_subtipo_servicio;
                            eDet.cod_producto = eProd.cod_producto;
                            eDet.cod_unidad_medida = eProd.cod_unidad_medida;
                            eDet.num_cantidad = eProd.num_cantidad;
                            eDet.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                            eDet = unit.Logistica.Insertar_Actualizar_GuiaRemisionDetalle<eAlmacen.eGuiaRemision_Detalle>(eDet);
                            if (eDet == null) MessageBox.Show("Error al registrar producto", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    ActualizarListado = true;
                    MessageBox.Show(result.Message /*"Se realizó la salida de productos de manera satisfactoria"*/, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MiAccion == GuiaRemision.Nuevo)
                    {
                        MiAccion = GuiaRemision.Vista;
                        BloqueoControles(false, true, false);
                        cbxCorrelativoManual.Enabled = false;

                        gvListadoProductos.Columns["num_cantidad_stock"].Visible = false;
                        gvListadoProductos.Columns["num_cantidad_stock_nuevo"].Visible = false;
                        btnAdjuntarArchivo.Enabled = true; btnVerPDF.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show(result.Message/*"Error al registrar salida"*/, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private eAlmacen.eGuiaRemision_Cabecera AsignarValores_Cabecera()
        {
            /*Validamos que el código de la Guía, es manual o automático.*/
            bool nuevo = MiAccion == GuiaRemision.Nuevo;
            bool manual = cbxCorrelativoManual.Checked;
            string codigoGuia = nuevo ? (manual ? txtCodigo.Text : null) : txtCodigo.Text;

            string codTransportista = string.Empty;
            if (txtTransportista.Tag != null)
            {
                codTransportista = txtTransportista.Tag.ToString();
            }


            eAlmacen.eGuiaRemision_Cabecera obj = new eAlmacen.eGuiaRemision_Cabecera();
            obj.cod_guiaremision = codigoGuia;
            obj.cod_almacen = cod_almacen;
            obj.cod_tipo_movimiento = lkpTipoMovimiento.EditValue.ToString();
            obj.fch_documento = Convert.ToDateTime(dtFechaDocumento.EditValue);
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = cod_sede_empresa;
            obj.cod_requerimiento = txtNroRequerimiento.Text;
            obj.fch_traslado = Convert.ToDateTime(dtFechaTraslado.EditValue);
            obj.dsc_pref_ceco = lkpDistribucionCECO.EditValue.ToString();
            obj.cod_transportista = codTransportista;//txtTransportista.Tag.ToString();
            obj.dsc_direccion = txtDireccion.Text;
            obj.cod_motivo_traslado = lkpMotivoTraslado.EditValue.ToString();
            obj.flg_activo = "SI";
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private void gvListadoProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProductos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    string colName = e.Column.FieldName;
                    eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetRow(e.RowHandle) as eAlmacen.eProductos_Almacen;
                    if (colName == "num_cantidad_stock" || colName == "num_cantidad_stock_nuevo") e.Appearance.ForeColor = Color.Blue;
                    if (colName == "num_cantidad_stock" && objProd.num_cantidad_stock <= 0) e.Appearance.ForeColor = Color.Red;
                    if (colName == "num_cantidad_stock_nuevo" && objProd.num_cantidad_stock_nuevo <= 0) e.Appearance.ForeColor = Color.Red;
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoProductos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetFocusedRow() as eAlmacen.eProductos_Almacen;
                if (objProd != null)
                {
                    if (e.Column.FieldName == "num_cantidad")
                    {
                        if (objProd.num_cantidad > objProd.num_cantidad_x_recibir)
                        {
                            MessageBox.Show("No puede digitar una cantidad mayor al requerimiento inicial", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objProd.num_cantidad = objProd.num_cantidad_x_recibir;

                            if (objProd.num_cantidad > objProd.num_cantidad_stock)
                            {
                                MessageBox.Show("No puede digitar una cantidad mayor a la del stock", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                objProd.num_cantidad = objProd.num_cantidad_stock;
                                objProd.num_cantidad_stock_nuevo = 0;
                                gvListadoProductos.RefreshData();
                                return;
                            }
                            gvListadoProductos.RefreshData();
                            return;
                        }
                        if (objProd.num_cantidad > objProd.num_cantidad_stock)
                        {
                            MessageBox.Show("No puede digitar una cantidad mayor a la del stock", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objProd.num_cantidad = objProd.num_cantidad_stock;
                            objProd.num_cantidad_stock_nuevo = 0;
                            gvListadoProductos.RefreshData();
                            return;
                        }
                        objProd.num_cantidad_stock_nuevo = objProd.num_cantidad_stock - objProd.num_cantidad;
                    }
                    gvListadoProductos.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}