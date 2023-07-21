using BE_BackOffice;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_BackOffice.Clientes_Y_Proveedores.Clientes;
using UI_BackOffice.Formularios.Shared;
using UI_BackOffice.Tools.Interfaces;
using UI_BackOffice.Tools.OneDriveServices;
using UI_BackOffice.Tools.OneDriveServices.DTOs;

namespace UI_BackOffice.Formularios.Logistica
{
    internal enum RequerimientoCompra
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public enum ParentFormType
    {
        ListadoRequerimientos, ListadoSolicitudCompra, ListaInventarioAlmacen, ListadoProductoLowStock
    }
    public partial class frmMantRequerimientosCompra : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        private readonly IOneDriveService<RequerimientoAdjuntarDTO, RequerimientoDescargarDTO> oneDrive;
        internal RequerimientoCompra accion = RequerimientoCompra.Nuevo;
        TaskScheduler scheduler;
        public string codigoEmpresa; // CORREGIR, MUestra otro codigo que no esel que se ha seleccionado
        public string empresa, sede, requerimiento, solicitud;
        public string codigoEstadoRequerimiento = "";
        public Int32 anho;
        eRequerimiento eRequerimientoEdit = new eRequerimiento();
        internal List<eRequerimiento.eRequerimiento_Detalle> eDetRequerimientoEdit;
        eParametrosGenerales objBloq = new eParametrosGenerales();
        String codigoCliente = "CLI0000000";
        eVentana oPerfilProd = new eVentana();

        private List<eProductoEmpresaProveedor> ProductoEmpresaProveedorVinculados;


        internal new Form ParentForm;
        private readonly ParentFormType parentFormType;
        public frmMantRequerimientosCompra(ParentFormType parentFormType)
        {
            InitializeComponent();
            unit = new UnitOfWork();
            oneDrive = new RequerimientoOneDriveService(unit);

            this.parentFormType = parentFormType;
            this.DialogResult = DialogResult.Cancel;

            //
            gvProductos.OptionsView.ShowGroupPanel = false;


        }

        private void VisibilidadPanelDeProductosGenerales(bool visible)
        {

            DevExpress.XtraLayout.Utils.LayoutVisibility layoutVisibility = visible ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            controlGProductos.Visibility = layoutVisibility;
            blancoUno.Visibility = layoutVisibility;
            controlAgregar.Visibility = layoutVisibility;
            blancoDos.Visibility = layoutVisibility;
            controlQuitar.Visibility = layoutVisibility;
            blancoTres.Visibility = layoutVisibility;
        }
        private void configurarObjetos()
        {
            switch (parentFormType)
            {
                case ParentFormType.ListadoRequerimientos:
                    break;
                case ParentFormType.ListadoSolicitudCompra:
                    break;
                case ParentFormType.ListaInventarioAlmacen:
                    break;
                case ParentFormType.ListadoProductoLowStock:
                    VisibilidadPanelDeProductosGenerales(false);


                    bsDetalleReq.DataSource = eDetRequerimientoEdit;
                    gvDetalleReq.ExpandAllGroups();


                    break;
            }
        }

        private void frmMantRequerimientos_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Inicializar();

            //Cargar configuración de objectos... modificar para los demás formularios.
            configurarObjetos();
        }

        private void Inicializar()
        {


            try
            {

                CargarLookUpEdit();
                List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
                oPerfilProd = listPerfil.Find(x => x.cod_perfil == 35 || x.cod_perfil == 37 || x.cod_perfil == 5);
                switch (accion)
                {
                    case RequerimientoCompra.Nuevo:
                        ConfigurarForm("Nuevo");
                        CargarListadoProductos();
                        DateTime date = DateTime.Now;
                        dtpFechaRequerimiento.EditValue = date;
                        dtpFechaAtencion.Properties.MinValue = date;
                        dtpFechaAtencion.EditValue = date;
                        break;
                    case RequerimientoCompra.Editar:
                        btnImportadorDeProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        CargarRequerimiento();
                        ConfigurarForm("Editar");
                        break;
                    case RequerimientoCompra.Vista:
                        btnImportadorDeProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        CargarRequerimiento();
                        ConfigurarForm("Vista");
                        break;
                }

                //Cargar Vinculados para validación de productos
                ProductoEmpresaProveedorVinculados = new List<eProductoEmpresaProveedor>();
                ProductoEmpresaProveedorVinculados = unit.Logistica.Obtener_ListadoProductoEmpresaProveedor(cod_empresa: codigoEmpresa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool ValidarVinculacionDeProductos(string tipo_servicio,
            string subtipo_servicio,
            string producto,
            string flg_vigente,
            string flg_con_proveedor)
        {
            return ProductoEmpresaProveedorVinculados.Exists((ex) => ex.cod_tipo_servicio.Equals(tipo_servicio)
            && ex.cod_subtipo_servicio.Equals(subtipo_servicio)
            && ex.cod_producto.Equals(producto)
            && ex.flg_vigente.Equals(flg_vigente)
            && ex.flg_con_proveedor.Equals(flg_con_proveedor));
        }
        private void CargarLookUpEdit()
        {
            try
            {
                unit.Requerimiento.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
                unit.Requerimiento.CargaCombosLookUp("TiposReq", lkpTipo, "cod_tipo", "dsc_tipo", "", valorDefecto: true);

                if (codigoEmpresa == null)
                {
                    List<eFacturaProveedor> list = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
                    if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;

                    codigoEmpresa = list[0].cod_empresa;
                }
                else
                {
                    lkpEmpresa.EditValue = codigoEmpresa;
                }

                unit.Requerimiento.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());

                lkpTipo.EditValue = "REQ";
                lkpSede.EditValue = "00001";
                lkpArea.EditValue = null;

                txtSolicitante.EditValue = Program.Sesion.Usuario.dsc_usuario;

                /*-----*Cargar CECO*-----*/
                CargarComboCECO(cod_empresa: codigoEmpresa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarComboCECO(string cod_empresa = "", string cod_area = "", string nivel_sede = "")
        {
            /*-----*Cargar CECO*-----*/
            // unit.Factura.CargaCombosLookUp("DistribucionCECO", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa, cod_cliente: cod_cliente);

            string cod_nivel_ceco = cod_area != "" ? cod_area : codigoCliente;
            string cod_nivel_sede = nivel_sede != "" ? string.Concat(codigoCliente, "-", nivel_sede) : "";
            objBloq.valor_1 = cod_empresa;
            objBloq = unit.Factura.Obtener_BloqueoCECOxEmpresa<eParametrosGenerales>(64, objBloq);
            if (objBloq == null)
            {
                HNG.MessageWarning("No se ha encontrado los parámetros, vuelve a intentarlo.", "Cargar CECOS");
                return;
            }
            // if (objBloq.valor_2 == "NO" || (objBloq.valor_2 == "SI" && Convert.ToDateTime(dtpFechaRequerimiento.EditValue).Year < 2023))
            //object tipo_ceco = lkpArea.EditValue;
            //if (tipo_ceco == null) { cod_cliente = "CLI0000000"; }

            if (objBloq.valor_2 == "NO" && Convert.ToDateTime(dtpFechaRequerimiento.EditValue).Year < 2023) //  cambiar por fecha de registro.
            {
                unit.Factura.CargaCombosLookUp("DistribucionCECO", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "",
                    valorDefecto: true, cod_empresa: cod_empresa, cod_cliente: cod_nivel_ceco);
            }
            else
            {
                unit.Factura.CargaCombosLookUp("DistribucionCECO_Nuevo", lkpDistribucionCECO, "cod_CECO", "dsc_CECO", "",
                    valorDefecto: true, cod_empresa: cod_empresa, cod_cliente: cod_nivel_ceco, cod_nivel_sede: cod_nivel_sede);
            }
            lkpDistribucionCECO.ItemIndex = 0;
        }

        private void CargarListadoProductos()
        {
            try
            {
                List<eProductos> lista = unit.Requerimiento.ListarProductos<eProductos>(4, lkpEmpresa.EditValue.ToString());
                bsProductos.DataSource = lista;
                gvProductos.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarRequerimiento()
        {

            eRequerimientoEdit = unit.Requerimiento.Cargar_Requerimiento<eRequerimiento>(4, empresa, sede, requerimiento, solicitud, anho);

            txtRequerimiento.EditValue = eRequerimientoEdit.cod_requerimiento;
            lkpEmpresa.EditValue = eRequerimientoEdit.cod_empresa;
            lkpSede.EditValue = eRequerimientoEdit.cod_sede_empresa;
            lkpArea.EditValue = eRequerimientoEdit.cod_area;
            codigoCliente = eRequerimientoEdit.cod_cliente;

            txtCliente.EditValue = eRequerimientoEdit.dsc_razon_social;

            unit.Requerimiento.CargaCombosLookUp("SedesCliente", lkpSedeCliente, "cod_sede_cliente", "dsc_sede_cliente", "", valorDefecto: true, cod_cliente: codigoCliente);

            lkpSedeCliente.EditValue = eRequerimientoEdit.cod_sede_cliente;
            lkpTipo.EditValue = eRequerimientoEdit.cod_tipo;
            txtSolicitante.EditValue = eRequerimientoEdit.dsc_nombre_solicitante;
            dtpFechaRequerimiento.EditValue = eRequerimientoEdit.fch_requerimiento;
            dtpFechaAtencion.EditValue = eRequerimientoEdit.fch_atencion;
            txtJustificacion.EditValue = eRequerimientoEdit.dsc_justificacion;
            txtObservaciones.EditValue = eRequerimientoEdit.dsc_observaciones;
            txtProdRequeridos.EditValue = eRequerimientoEdit.dsc_items_requeridos;



            eDetRequerimientoEdit = unit.Requerimiento.Cargar_Detalle_Requerimiento<eRequerimiento.eRequerimiento_Detalle>(5, empresa, sede, requerimiento, solicitud, anho);

            bsDetalleReq.DataSource = eDetRequerimientoEdit;
            gvDetalleReq.ExpandAllGroups();

            /*-----*Cargar CECO*-----*/
            lkpDistribucionCECO.EditValue = eRequerimientoEdit.cod_CECO;
            //lkpDistribucionCECO.EditValue = eFact.dsc_pref_ceco;
        }

        private void ConfigurarForm(string opcion)
        {
            switch (opcion)
            {
                case "Nuevo":
                    colimp_unitario.Visible = false;
                    colimp_total.Visible = false;
                    coldsc_proveedor.Visible = false;
                    colnum_cantidad_recibido.Visible = false;
                    colnum_cantidad_x_recibir.Visible = false;
                    break;
                case "Editar":
                    btnGenerar.Caption = btnGenerar.Caption.Replace("Generar", "Actualizar");
                    btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnClonar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnImportarDocumentacion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnExportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnAprobar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                    lkpEmpresa.Enabled = false;
                    lkpSede.Enabled = false;
                    lkpArea.Enabled = false;
                    txtCliente.Enabled = false;
                    lkpSedeCliente.Enabled = false;
                    btnBuscarCliente.Enabled = false;
                    if (codigoCliente != "CLI0000000") { btnVerCliente.Enabled = true; } else { btnVerCliente.Enabled = false; }
                    txtSolicitante.Enabled = false;
                    dtpFechaRequerimiento.Enabled = false;
                    lkpTipo.Enabled = false;

                    controlGProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlQuitar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    colimp_unitario.Visible = true;
                    colimp_total.Visible = true;
                    coldsc_proveedor.Visible = true;
                    colnum_cantidad_recibido.Visible = true;
                    colnum_cantidad_x_recibir.Visible = true;
                    break;
                case "Vista":
                    List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
                    eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5 || x.cod_perfil == 26);

                    btnGenerar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    btnClonar.Visibility = oPerfilLog != null ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    btnImportarDocumentacion.Visibility = oPerfilLog != null ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;

                    btnExportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                    lkpEmpresa.Enabled = false;
                    lkpSede.Enabled = false;
                    lkpArea.Enabled = false;
                    txtCliente.Enabled = false;
                    lkpDistribucionCECO.Enabled = false;
                    lkpSedeCliente.Enabled = false;
                    btnBuscarCliente.Enabled = false;
                    if (codigoCliente != "CLI0000000") { btnVerCliente.Enabled = true; } else { btnVerCliente.Enabled = false; }
                    btnBuscarCliente.Enabled = false;
                    dtpFechaRequerimiento.Enabled = false;
                    dtpFechaAtencion.Enabled = false;
                    txtSolicitante.Enabled = false;
                    lkpTipo.Enabled = false;
                    txtJustificacion.Enabled = false;
                    txtObservaciones.Enabled = false;
                    txtProdRequeridos.Enabled = false;

                    controlGProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    controlQuitar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    blancoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    colnum_cantidad.OptionsColumn.AllowEdit = false;
                    colimp_unitario.OptionsColumn.AllowEdit = false;
                    colnum_cantidad_recibido.Visible = true;
                    colnum_cantidad_x_recibir.Visible = true;
                    break;
                case "AgregarProducto":
                    controlGProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    blancoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    blancoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    controlQuitar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    blancoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    colimp_unitario.Visible = false;
                    colimp_total.Visible = false;
                    coldsc_proveedor.Visible = false;
                    colnum_cantidad_recibido.Visible = false;
                    colnum_cantidad_x_recibir.Visible = false;
                    break;
            }

            // habilitar opcion para importar documento al OneDrive .
            if (codigoEstadoRequerimiento.Equals("APROBADO"))
            { btnImportarDocumentacion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; }
            else { btnImportarDocumentacion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never; }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConfigurarForm("Nuevo");
            CargarListadoProductos();

            DateTime date = DateTime.Now;
            dtpFechaRequerimiento.EditValue = date;
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //return;
            if (accion == RequerimientoCompra.Nuevo)
            {
                btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnOcultar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            try
            {
                // el control chxSaldosIniciales, si está activo, permite el registro de cantidades en 0 y cliente varios CLI000000...


                if (lkpDistribucionCECO.EditValue == null) { MessageBox.Show("Debe seleccionar la distribución del CECO.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpDistribucionCECO.Select(); return; }
                if (string.IsNullOrWhiteSpace(lkpDistribucionCECO.EditValue.ToString())) { MessageBox.Show("Debe seleccionar la distribución del CECO.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpDistribucionCECO.Select(); return; }
                if (!chxSaldosIniciales.Checked)
                {
                    if (ValidarCantidades() == "0") { MessageBox.Show("Debe seleccionar productos al requerimiento.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                }
                if (!chxSaldosIniciales.Checked)
                {
                    if (ValidarCantidades() == "1") { MessageBox.Show("Las cantidades no pueden ser iguales a 0.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                }
                if (txtSolicitante.Text == "") { MessageBox.Show("El campo solicitante no puede estar vacío.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSolicitante.Focus(); return; }
                if (!chxSaldosIniciales.Checked)
                {
                    if (lkpArea.EditValue == null && codigoCliente == "CLI0000000") { MessageBox.Show("Debe escoger un área o cliente solicitante.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                }
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando Requerimiento", "Cargando...");

                eRequerimiento eReq = AsignarValores();

                eReq = unit.Requerimiento.Ins_Act_Requerimiento<eRequerimiento>(eReq, Program.Sesion.Usuario.cod_usuario);

                switch (accion)
                {
                    case RequerimientoCompra.Editar:
                        string respuesta = unit.Requerimiento.Limpiar_Det_Requerimiento(eReq.cod_empresa, eReq.cod_sede_empresa, eReq.cod_requerimiento, eReq.flg_solicitud, eReq.dsc_anho);
                        break;
                }

                gvDetalleReq.PostEditor();
                for (int x = 0; x < gvDetalleReq.DataRowCount; x++)
                {
                    eRequerimiento.eRequerimiento_Detalle eDetReq = gvDetalleReq.GetRow(x) as eRequerimiento.eRequerimiento_Detalle;

                    eDetReq.cod_empresa = eReq.cod_empresa;
                    eDetReq.cod_sede_empresa = eReq.cod_sede_empresa;
                    eDetReq.cod_requerimiento = eReq.cod_requerimiento;
                    eDetReq.flg_solicitud = eReq.flg_solicitud;
                    eDetReq.dsc_anho = eReq.dsc_anho;
                    eDetReq.num_restante = eDetReq.num_cantidad;
                    eDetReq.imp_unitario = 0;
                    eDetReq.imp_total = 0;
                    eDetReq.flg_generaOC = eDetReq.Sel_generaOC ? "SI" : "NO";

                    eDetReq = unit.Requerimiento.Ins_Act_Detalle_Requerimiento<eRequerimiento.eRequerimiento_Detalle>(eDetReq, Program.Sesion.Usuario.cod_usuario);
                }

                empresa = eReq.cod_empresa; sede = eReq.cod_sede_empresa; requerimiento = eReq.cod_requerimiento; solicitud = eReq.flg_solicitud.ToString().Substring(0, 1); anho = eReq.dsc_anho;

                switch (accion)
                {
                    case RequerimientoCompra.Nuevo:
                        SplashScreenManager.CloseForm();
                        MessageBox.Show("Requerimiento generado de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRequerimiento.EditValue = eReq.cod_requerimiento.ToString();
                        accion = RequerimientoCompra.Editar;
                        btnClonar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        btnImportarDocumentacion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                        btnExportarExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        btnGenerar.Caption = btnGenerar.Caption.Replace("Generar", "Actualizar");
                        break;
                    case RequerimientoCompra.Editar:
                        SplashScreenManager.CloseForm();
                        MessageBox.Show("Requerimiento actualizado de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarRequerimiento();
                        break;
                }

                this.DialogResult = DialogResult.OK;// para el listado de orden de compra.
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string ValidarCantidades()
        {
            string respuesta = "";

            if (gvDetalleReq.DataRowCount == 0)
            {
                respuesta = "0";
            }
            else
            {
                for (int x = 0; x < gvDetalleReq.DataRowCount; x++)
                {
                    eRequerimiento.eRequerimiento_Detalle eDetReq = gvDetalleReq.GetRow(x) as eRequerimiento.eRequerimiento_Detalle;

                    if (eDetReq.num_cantidad == 0)
                    {
                        respuesta = "1";
                        break;
                    }
                }
            }

            return respuesta;
        }

        private eRequerimiento AsignarValores()
        {
            eRequerimiento eRequ = new eRequerimiento();

            eRequ.cod_empresa = lkpEmpresa.EditValue.ToString();
            eRequ.cod_sede_empresa = lkpSede.EditValue.ToString();
            eRequ.cod_requerimiento = "";
            eRequ.cod_cliente = codigoCliente;
            eRequ.cod_sede_cliente = lkpSedeCliente.EditValue == null ? 0 : Int32.Parse(lkpSedeCliente.EditValue.ToString());
            eRequ.cod_area = lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString();
            eRequ.flg_solicitud = "C";
            eRequ.cod_estado_requerimiento = "";
            eRequ.dsc_nombre_solicitante = txtSolicitante.EditValue == null ? "" : txtSolicitante.EditValue.ToString();
            eRequ.fch_requerimiento = Convert.ToDateTime(dtpFechaRequerimiento.EditValue);
            eRequ.fch_atencion = Convert.ToDateTime(dtpFechaAtencion.EditValue);
            eRequ.dsc_observaciones = txtObservaciones.EditValue == null ? "" : txtObservaciones.EditValue.ToString();
            eRequ.dsc_justificacion = txtJustificacion.EditValue == null ? "" : txtJustificacion.EditValue.ToString();
            eRequ.dsc_items_requeridos = txtProdRequeridos.EditValue == null ? "" : txtProdRequeridos.EditValue.ToString();
            eRequ.cod_tipo = lkpTipo.EditValue.ToString();
            eRequ.cod_CECO = lkpDistribucionCECO.EditValue.ToString();

            switch (accion)
            {
                case RequerimientoCompra.Editar:
                    eRequ.cod_requerimiento = txtRequerimiento.EditValue.ToString();
                    break;
            }

            return eRequ;
        }

        private void btnAgregarProductos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CargarListadoProductos();
            ConfigurarForm("AgregarProducto");

            for (int x = 0; x < gvDetalleReq.DataRowCount; x++)
            {
                eRequerimiento.eRequerimiento_Detalle obj = gvDetalleReq.GetRow(x) as eRequerimiento.eRequerimiento_Detalle;

                for (int i = 0; i < gvProductos.DataRowCount; i++)
                {
                    eProductos obj2 = gvProductos.GetRow(i) as eProductos;

                    if (obj2.cod_tipo_servicio == obj.cod_tipo_servicio && obj2.cod_subtipo_servicio == obj.cod_subtipo_servicio && obj2.cod_producto == obj.cod_producto)
                    {
                        bsProductos.Remove(obj2);
                    }
                }
            }

            btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnOcultar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void btnOcultar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (accion == RequerimientoCompra.Nuevo)
            {
                controlGProductos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                blancoUno.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlAgregar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                blancoDos.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                controlQuitar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                blancoTres.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                colimp_unitario.Visible = true;
                colimp_total.Visible = true;
                coldsc_proveedor.Visible = true;
                colnum_cantidad_recibido.Visible = true;
                colnum_cantidad_x_recibir.Visible = true;
            }
            else
            {
                ConfigurarForm("Editar");
            }

            btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            btnOcultar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void btnClonar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnGenerar.Caption = btnGenerar.Caption.Replace("Actualizar", "Generar");
            btnGenerar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnOcultar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            accion = RequerimientoCompra.Nuevo;
            txtRequerimiento.EditValue = null;

            lkpSede.Enabled = true;
            if (lkpArea.EditValue == null) { lkpArea.Enabled = false; } else { lkpArea.Enabled = true; }
            if (codigoCliente == "CLI0000000") { btnVerCliente.Enabled = false; } else { btnVerCliente.Enabled = true; btnBuscarCliente.Enabled = true; txtCliente.Enabled = true; lkpSedeCliente.Enabled = true; }
            dtpFechaRequerimiento.Enabled = true;
            dtpFechaAtencion.Enabled = true;
            txtSolicitante.Enabled = true;
            txtJustificacion.Enabled = true;
            txtObservaciones.Enabled = true;
            txtProdRequeridos.Enabled = true;

            ConfigurarForm("AgregarProducto");
        }

        private void btnExportarExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportarExcel();
        }

        private void ExportarExcel()
        {
            try
            {
                string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Requerimiento - " + txtRequerimiento.EditValue.ToString() + " - " + DateTime.Now.ToShortDateString().Replace("/", "-").Replace(":", "") + ".xlsx";

                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

                gvDetalleReq.ExportToXlsx(archivo);

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

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Busqueda("", "Cliente");
        }

        private void btnVerCliente_Click(object sender, EventArgs e)
        {
            frmMantCliente frm = new frmMantCliente();
            frm.cod_cliente = codigoCliente;
            frm.MiAccion = Cliente.Vista;
            frm.cod_empresa = lkpEmpresa.EditValue.ToString();
            frm.ShowDialog();
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;

            switch (tipo)
            {
                case "Cliente":
                    frm.entidad = frmBusquedas.MiEntidad.ClienteEmpresa;
                    frm.cod_condicion1 = lkpEmpresa.EditValue.ToString();
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Cliente":
                    codigoCliente = frm.codigo;
                    txtCliente.EditValue = frm.descripcion;

                    unit.Requerimiento.CargaCombosLookUp("SedesCliente", lkpSedeCliente, "cod_sede_cliente", "dsc_sede_cliente", "", valorDefecto: true, cod_cliente: frm.codigo);
                    lkpSedeCliente.EditValue = 1;

                    lkpArea.Enabled = false;
                    btnVerCliente.Enabled = true;
                    break;
            }

            /*-----*CargarCombo CECOS por cliente*-----*/
            var coempresa = lkpEmpresa.EditValue.ToString();
            string nivel_sede = lkpSedeCliente.EditValue == null ? "" : lkpSedeCliente.EditValue.ToString();
            CargarComboCECO(cod_empresa: coempresa, nivel_sede: nivel_sede);

        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Cliente");
            }
            string dato = unit.Globales.pKeyPress(txtCliente, e);
            if (dato != "")
            {
                Busqueda(dato, "Cliente");
            }
            if (dato == "")
            {
                lkpArea.Enabled = true;
                btnVerCliente.Enabled = false;
                codigoCliente = "CLI0000000";
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            unit.Requerimiento.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
            CargarListadoProductos();

            var coempresa = lkpEmpresa.EditValue.ToString();
            CargarComboCECO(cod_empresa: coempresa);

            //Cargar Area:
            lkpSede_EditValueChanged(lkpSede, new EventArgs());

        }

        private void lkpSede_EditValueChanged(object sender, EventArgs e)
        {
            CargarArea();
        }

        private void CargarArea()
        {
            unit.Requerimiento.CargaCombosLookUp("Areas", lkpArea, "cod_area", "dsc_area", "",
                valorDefecto: true,
                cod_empresa: lkpEmpresa.EditValue.ToString(),
                cod_sede_empresa: lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString());

            lkpArea.EditValue = null; lkpArea_EditValueChanged(lkpArea, new EventArgs());
        }
        private void lkpArea_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpArea.EditValue != null)
            {
                txtCliente.Enabled = false;
                btnBuscarCliente.Enabled = false;
                lkpSedeCliente.Enabled = false;

                /*Cargar CECOS definidos por el área*/
                string cod_empresa = lkpEmpresa.EditValue.ToString();
                CargarComboCECO(cod_empresa: cod_empresa, cod_area: lkpArea.EditValue.ToString());
            }
            else
            {
                txtCliente.Enabled = true;
                btnBuscarCliente.Enabled = true;
                lkpSedeCliente.Enabled = true;
            }
        }

        private void lkpArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lkpArea.EditValue = null;
        }

        private void dtpFechaRequerimiento_EditValueChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(dtpFechaRequerimiento.EditValue);

            dtpFechaAtencion.EditValue = date;
            dtpFechaAtencion.Properties.MinValue = date;

            //var coempresa = lkpEmpresa.EditValue.ToString();
            //CargarComboCECO(cod_empresa: coempresa);
            var coempresa = lkpEmpresa.EditValue.ToString();
            CargarComboCECO(cod_empresa: coempresa);
        }

        private void gvProductos_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvProductos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            /*
             * La siguientes 3lienas sirve para obten los productos que no se han podido agregar a la canastilla,
             * esto para mostrar en un Alert y tener en cuenta la asignación d proveedores: nota: colocar formulario de asignación.
             */
            int length = gvProductos.GetSelectedRows().Count();
            string[] productosNoAgregados = new string[length];
            int index = -1;

            int valor = 0;
            foreach (int nRow in gvProductos.GetSelectedRows())
            {
                if (gvProductos.GetRow(nRow - valor) is eProductos obj)
                {
                    eRequerimiento.eRequerimiento_Detalle obj2 = new eRequerimiento.eRequerimiento_Detalle();
                    obj2.cod_tipo_servicio = obj.cod_tipo_servicio;
                    obj2.dsc_tipo_servicio = obj.dsc_tipo_servicio;
                    obj2.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                    obj2.dsc_subtipo_servicio = obj.dsc_subtipo_servicio;
                    obj2.cod_producto = obj.cod_producto;
                    obj2.dsc_producto = obj.dsc_producto;
                    obj2.cod_unidad_medida = obj.cod_unidad_medida;
                    obj2.dsc_simbolo = obj.dsc_simbolo;
                    obj2.flg_generaOC = "SI";
                    obj2.Sel_generaOC = true;

                    /*
                     * Validar si el producto que se ha seleccionado tiene proveedor asignado,
                     * esta validación es para la empresa FACILITA y otras que requieren la
                     * asignación de proveedores, se valida si el "flg_con_proveedor='SI'"..
                     * Nota: Por ahora, solo FACILITA trabaja con proveedores favoritos: (proxm) se debería crear una tabla para guardar este tipo de datos.
                     */
                    if (codigoEmpresa.Equals("00002"))
                    {
                        if (!ValidarVinculacionDeProductos(tipo_servicio: obj.cod_tipo_servicio,
                           subtipo_servicio: obj.cod_subtipo_servicio,
                           producto: obj.cod_producto,
                           flg_vigente: "SI",
                           flg_con_proveedor: "SI"))
                        {
                            index++;
                            productosNoAgregados[index] = obj.dsc_producto.ToString();
                            continue;
                        }
                    }

                    bsDetalleReq.Add(obj2);
                    bsProductos.Remove(obj);

                    //if (obj.flg_con_proveedor != null && obj.flg_con_proveedor.Equals("SI"))
                    //{
                    //    if (!ValidarVinculacionDeProductos(tipo_servicio: obj.cod_tipo_servicio,
                    //        subtipo_servicio: obj.cod_subtipo_servicio,
                    //        producto: obj.cod_producto,
                    //        flg_vigente: "SI"))
                    //    {
                    //        continue;
                    //    }

                    //    bsDetalleReq.Add(obj2);
                    //    bsProductos.Remove(obj);
                    //}
                    //else
                    //{
                    //    //Administrar Agregar/Eliminar productos que no requieren que la asignaciónde prveedores sea necesaria.
                    //    bsDetalleReq.Add(obj2);
                    //    bsProductos.Remove(obj);
                    //}
                }
                valor = valor + 1;
            }
            gvDetalleReq.ExpandAllGroups();

            if (productosNoAgregados.Any())
            {
                if (!string.IsNullOrEmpty(productosNoAgregados[0]))
                {
                    HNG.MessageWarning("Los siguientes productos: " + string.Join(", ", productosNoAgregados) + ". No se les ha asignado un proveedor.", "SIN PROVEEDORES");
                }
            }

        }

        private void AgregarProductoADetalles(eRequerimiento.eRequerimiento_Detalle obj2)
        {
            bsDetalleReq.Add(obj2);
        }
        private void RetirarProductoAgregadosADetalles(eProductos obj)
        {
            bsProductos.Remove(obj);
        }


        private void rchkSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            gvDetalleReq.PostEditor();
        }

        private void gvDetalleReq_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                if (accion == RequerimientoCompra.Editar)
                {
                    if (e.Clicks == 2 && e.RowHandle >= 0)
                    {
                        eRequerimiento.eRequerimiento_Detalle obj = gvDetalleReq.GetFocusedRow() as eRequerimiento.eRequerimiento_Detalle;

                        frmMantProductos frm = new frmMantProductos();
                        frm.MiAccion = oPerfilProd == null ? Producto.Vista : Producto.Editar;
                        frm.cod_tipo_servicio = obj.cod_tipo_servicio;
                        frm.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                        frm.cod_producto = obj.cod_producto;
                        //frm.cod_productoREF = obj.cod_productoREF;
                        frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                        frm.ShowDialog();
                        if (frm.ActualizarListado)
                        {
                            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                            CargarRequerimiento();
                            SplashScreenManager.CloseForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvDetalleReq.GetSelectedRows())
            {
                eRequerimiento.eRequerimiento_Detalle obj = gvDetalleReq.GetRow(nRow - valor) as eRequerimiento.eRequerimiento_Detalle;
                eProductos obj2 = new eProductos();

                obj2.cod_tipo_servicio = obj.cod_tipo_servicio;
                obj2.dsc_tipo_servicio = obj.dsc_tipo_servicio;
                obj2.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                obj2.dsc_subtipo_servicio = obj.dsc_subtipo_servicio;
                obj2.cod_producto = obj.cod_producto;
                obj2.dsc_producto = obj.dsc_producto;
                obj2.cod_unidad_medida = obj.cod_unidad_medida;
                obj2.dsc_simbolo = obj.dsc_simbolo;

                bsProductos.Add(obj2);
                bsDetalleReq.Remove(obj);
                valor = valor + 1;
            }
            gvProductos.ExpandAllGroups();
        }

        private void gvDetalleReq_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvDetalleReq.FocusedColumn.Name == "colnum_cantidad")
            {
                eRequerimiento.eRequerimiento_Detalle obj = gvDetalleReq.GetFocusedRow() as eRequerimiento.eRequerimiento_Detalle;

                if (obj.num_cantidad == 0) { MessageBox.Show("Debe seleccionar una cantidad mayor a cero.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); obj.num_cantidad = 1; return; }

                obj.imp_total = obj.num_cantidad * obj.imp_unitario;
            }
        }

        private void gvDetalleReq_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string cell = gvDetalleReq.GetRowCellDisplayText(e.RowHandle, e.Column);

            if (cell == "0" || cell == "0.0000")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void lkpDistribucionCECO_EditValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(lkpDistribucionCECO.EditValue.ToString());
        }

        private void btnAprobar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*-----*Obtener Credencial de aprobación: para requerimiento comienza con RQ_, orden de compra OC_, etc.*-----*/
            //if (Program.Sesion.Usuario.flg_aprobador == null) { MessageBox.Show("El Usuario " + Program.Sesion.Usuario.cod_usuario + " no tiene permitido aprobar esta OC.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            //string[] coc = Program.Sesion.Usuario.flg_aprobador.Split(',').ToList().Where((c) => c.ToLower().Contains("oc")).ToArray();
            //if (coc == null || coc.Count() == 0) { MessageBox.Show("El Usuario " + Program.Sesion.Usuario.cod_usuario + " no tiene permitido aprobar esta OC.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            //var credencial = unit.SolicitudCompra.ObtenerAprobacion(coc[0]);
            //if (credencial == null) { MessageBox.Show("El Usuario " + Program.Sesion.Usuario.cod_usuario + " no tiene permitido aprobar esta OC.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }



            var f = (frmListadoRequerimientos)this.ParentForm;
            f.btnAprobar_ItemClick(sender, e);//
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lkpSedeCliente_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpSedeCliente.EditValue != null)
            {
                string cod_empresa = lkpEmpresa.EditValue == null ? "" : lkpEmpresa.EditValue.ToString();
                CargarComboCECO(cod_empresa: cod_empresa, nivel_sede: lkpSedeCliente.EditValue.ToString());
            }
        }

        private void btnImportadorDeProductos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnAgregarProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            btnOcultar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            VisibilidadPanelDeProductosGenerales(false);


            //Thread threadMasivos = new Thread(ImportarRequerimientoMasivo);
            //threadMasivos.Join();

            ImportarRequerimientoMasivo();
        }

        private void ImportarRequerimientoMasivo()
        {
            var objList = bsProductos.DataSource as List<eProductos>;
            bsDetalleReq.DataSource = RequerimientosHelper.GetListImportarRequerimientosDeFormatoExcel(
                objList, ProductoEmpresaProveedorVinculados);
            gvDetalleReq.ExpandAllGroups();
        }

        private void btnImportarDocumentacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ImportarDocumentoRequerimientoAOneDrive();
        }

        private void ImportarDocumentoRequerimientoAOneDrive()
        {

            //oneDrive.AdjuntarArchivoPDF_XML();
            oneDrive.AttachFile(
                new RequerimientoAdjuntarDTO(
                fechaRegistro: dtpFechaAtencion.EditValue.ToString(),
                codigoEmpresa: empresa,
                codigoRequerimiento: requerimiento,
                //codAlmacen: "",
                codSedeEmpresa: sede//,
                                    //codEntrada: "",
                                    //tipoDocumento: "",
                                    //serieDocumento: "",
                                    //numeroDocumento: ""
                ));
        }

        private void gvDetalleReq_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvDetalleReq_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvDetalleReq_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }
    }
}