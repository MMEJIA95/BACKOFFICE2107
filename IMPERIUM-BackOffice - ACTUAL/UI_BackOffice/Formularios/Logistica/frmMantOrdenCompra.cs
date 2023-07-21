using BE_BackOffice;
using BL_BackOffice;
using DevExpress.Data.Extensions;
using DevExpress.DataAccess.Native.EntityFramework;
using DevExpress.Utils.Extensions;
using DevExpress.Utils.MVVM;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_BackOffice.Formularios.Clientes_Y_Proveedores.Proveedores;
using UI_BackOffice.Formularios.Shared;

namespace UI_BackOffice.Formularios.Logistica
{
    internal enum OrdenCompra
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }

    public partial class frmMantOrdenCompra : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        internal OrdenCompra accion = OrdenCompra.Nuevo;
        TaskScheduler scheduler;
        public string codigoEmpresa;
        public string empresa, sede, ordenCompraServicio, solicitud;
        public Int32 anho;
        eOrdenCompra_Servicio eOrdenCompraEdit = new eOrdenCompra_Servicio();
        List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> eDetOrdenCompraEdit;
        List<eRequerimiento.eRequerimiento_Detalle> eRequerimientoDetalle;


        String codigoCliente = "";
        String codigoProveedor = "";
        String codigoProveedorDet = "";
        String dscProveedorDet = "";
        String rucProveedorDet = "";
        decimal subTotal = 0;
        decimal igv = 0;
        decimal total = 0;

        //
        bool AsignarProductosProveedor = false;
        internal bool seEliminar = false;
        string[] selectProveedor;
        public frmMantOrdenCompra()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurar_formulario();
        }
        private void configurar_formulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoSolicitudes_Vista, gvListadoSolicitudes_Vista);
            gvListadoSolicitudes_Vista.OptionsSelection.MultiSelect = true; ;
            gvListadoSolicitudes_Vista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void HabilitarControles()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(cod_usuario: Program.Sesion.Usuario.cod_usuario, dsc_formulario: this.Name, Program.Sesion.Global.Solucion);

            if (listPermisos.Count > 0)
            {
                for (int i = 0; i < listPermisos.Count; i++)
                {
                    layContenedor.Controls.OfType<DateEdit>().ToList().ForEach(item =>
                    {
                        if (item.Name.ToString() == listPermisos[i].dsc_menu.Trim())
                        { item.Enabled = true; }
                    });
                }
            }
        }

        private void frmMantRequerimientos_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Inicializar();

            // esconder buttons de asignar proveedor a producto individual y eliminar producto.
            if (AsignarProductosProveedor)
            {
                colbtnbuscar_eliminar.Visible = false;
                colbtnbuscar_proveedor_det.Visible = false;
                //btnGenerarSolicitudOC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //btnGuardar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //gvDetalleOC.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                gvDetalleOC.SelectRow(0);
                lkpEmpresa.ReadOnly = true;
                eDetOrdenCompraEdit = new List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>();

                unit.OrdenCompra_Servicio.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
                unit.OrdenCompra_Servicio.CargaCombosLookUp("Almacenes", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());

                CargarSolicitudCompra_Vista();
            }

            colbtnbuscar_eliminar.Visible = seEliminar;

        }

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();


                switch (accion)
                {
                    case OrdenCompra.Nuevo:
                        ConfigurarForm("Nuevo");

                        DateTime date = DateTime.Now;
                        dtpFechaEmision.EditValue = date;
                        //dtpFechaDespacho.Properties.MinValue = date;
                        dtpFechaDespacho.EditValue = date;

                        // nuevo requerimiento, asignar productos al proveedor.
                        AsignarProductosProveedor = true;
                        btnProductos.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                        break;
                    case OrdenCompra.Editar:
                        CargarOrdenCompra();
                        ConfigurarForm("Editar");
                        break;
                    case OrdenCompra.Vista:
                        CargarOrdenCompra();
                        ConfigurarForm("Vista");
                        break;
                }
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "INICIAR DATA");
                //MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarLookUpEdit()
        {
            try
            {
                unit.OrdenCompra_Servicio.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);

                if (codigoEmpresa == null)
                {
                    List<eFacturaProveedor> list = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
                    if (list.Count == 1)
                    {
                        lkpEmpresa.EditValue = list[0].cod_empresa;
                        codigoEmpresa = list[0].cod_empresa;
                    }
                    else
                    {
                        lkpEmpresa.EditValue = empresa; codigoEmpresa = empresa;
                    }
                }
                else
                {
                    lkpEmpresa.EditValue = codigoEmpresa;
                }

                unit.OrdenCompra_Servicio.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: codigoEmpresa);
                unit.OrdenCompra_Servicio.CargaCombosLookUp("Almacenes", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: codigoEmpresa);
                unit.OrdenCompra_Servicio.CargaCombosLookUp("Moneda", lkpMoneda, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                unit.OrdenCompra_Servicio.CargaCombosLookUp("ModPago", lkpModPago, "cod_modalidad_pago", "dsc_modalidad_pago", "", valorDefecto: true);

                lkpSede.EditValue = "00001";
                lkpModPago.EditValue = "MP001";
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "CARGAR DATA");
                //MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }





        private void CargarOrdenCompra(string cod_ocs = "")
        {
            /*-----*Inicializar datos Alamcén, Mod.Pago, Fecha de despacho*-----*/
            //dtpFechaDespacho.EditValue = DateTime.Now;
            // lkpAlmacen.Select = "00001";
            //lkpModPago.EditValue = "MP001";


            eOrdenCompraEdit = unit.OrdenCompra_Servicio.Cargar_OrdenCompra_Servicio<eOrdenCompra_Servicio>(2, empresa, sede, cod_ocs != "" ? cod_ocs : ordenCompraServicio, solicitud, anho);
            if (eOrdenCompraEdit == null)
            {
                HNG.MessageWarning("No se ha encontrado Orden de Compra, volver a cargar.", "Cargar Orden Compra");
                lkpModPago.Select();
                return;
            }

            txtOC.EditValue = eOrdenCompraEdit.cod_orden_compra_servicio;
            lkpEmpresa.EditValue = eOrdenCompraEdit.cod_empresa;
            lkpSede.EditValue = eOrdenCompraEdit.cod_sede_empresa;
            txtCotizacion.EditValue = eOrdenCompraEdit.num_cotizacion;
            codigoProveedor = cod_ocs != "" ? codigoProveedor : eOrdenCompraEdit.cod_proveedor;
            txtProveedor.EditValue = cod_ocs != "" ? dscProveedorDet : eOrdenCompraEdit.dsc_proveedor;
            txtRUC.EditValue = eOrdenCompraEdit.dsc_ruc;
            lkpAlmacen.EditValue = eOrdenCompraEdit.cod_almacen ?? "00001";
            txtDireccion.EditValue = eOrdenCompraEdit.dsc_direccion_despacho;
            lkpModPago.EditValue = eOrdenCompraEdit.cod_modalidad_pago ?? "MP001";
            lkpMoneda.EditValue = eOrdenCompraEdit.cod_moneda ?? "SOL";
            dtpFechaEmision.EditValue = eOrdenCompraEdit.fch_emision;
            if (eOrdenCompraEdit.fch_despacho.ToString().Contains("1/01/0001")) { dtpFechaDespacho.EditValue = DateTime.Now; } else { dtpFechaDespacho.EditValue = eOrdenCompraEdit.fch_despacho; }
            txtTermCond.EditValue = eOrdenCompraEdit.dsc_terminos_condiciones;
            txtSubTotal.EditValue = eOrdenCompraEdit.imp_subtotal;
            txtIGV.EditValue = eOrdenCompraEdit.imp_igv;
            txtTotal.EditValue = eOrdenCompraEdit.imp_total;
            txtTotalLetras.EditValue = eOrdenCompraEdit.dsc_imp_total;
            txtCV.EditValue = eOrdenCompraEdit.prc_CV;
            txtLI.EditValue = eOrdenCompraEdit.prc_LI;
            txtCB.EditValue = eOrdenCompraEdit.prc_CB;
            txtGG.EditValue = eOrdenCompraEdit.prc_GG;
            txtADM.EditValue = eOrdenCompraEdit.prc_ADM;
            txtOPER.EditValue = eOrdenCompraEdit.prc_OPER;
            txtGV.EditValue = eOrdenCompraEdit.prc_GV;
            txtObservaciones.EditValue = eOrdenCompraEdit.dsc_observaciones;

            eDetOrdenCompraEdit = unit.OrdenCompra_Servicio.Cargar_Detalle_OrdenCompra_Servicio<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(3, empresa, sede, ordenCompraServicio, solicitud, anho);

            bsDetalleOC.DataSource = eDetOrdenCompraEdit;
        }

        private void ConfigurarForm(string opcion)
        {
            switch (opcion)
            {
                case "Nuevo":
                    txtSubTotal.Enabled = false;
                    txtIGV.Enabled = false;
                    txtTotal.Enabled = false;
                    txtTotalLetras.Enabled = false;
                    break;
                case "Editar":
                    lkpEmpresa.Enabled = false;
                    lkpSede.Enabled = false;
                    dtpFechaEmision.Enabled = false;

                    txtSubTotal.Enabled = false;
                    txtIGV.Enabled = false;
                    txtTotal.Enabled = false;
                    txtTotalLetras.Enabled = false;

                    HabilitarControles();

                    break;
                case "Vista":
                    barOpciones.Visible = false;

                    lkpEmpresa.Enabled = false;
                    lkpSede.Enabled = false;
                    txtCotizacion.Enabled = false;
                    btnVerProveedor.Enabled = false;
                    txtProveedor.Enabled = false;
                    txtRUC.Enabled = false;
                    lkpAlmacen.Enabled = false;
                    lkpModPago.Enabled = false;
                    lkpMoneda.Enabled = false;
                    txtDireccion.Enabled = false;
                    dtpFechaEmision.Enabled = false;
                    dtpFechaDespacho.Enabled = false;
                    txtTermCond.Enabled = false;
                    txtObservaciones.Enabled = false;

                    txtSubTotal.Enabled = false;
                    txtIGV.Enabled = false;
                    txtTotal.Enabled = false;
                    txtTotalLetras.Enabled = false;

                    colnum_cantidad1.OptionsColumn.AllowEdit = false;
                    colimp_unitario.OptionsColumn.AllowEdit = false;
                    colbtnbuscar_proveedor_det.Visible = false;

                    break;
            }
        }

        #region Asignar productos a Proveedores
        private void CargarListadoProductos()
        {
            /*try
            {
                List<eProductos> lista = unit.Requerimiento.ListarProductos<eProductos>(4, lkpEmpresa.EditValue.ToString());
                bsProductos.DataSource = lista;
                gvProductos.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }*/
        }
        private void CargarSolicitudCompra_Vista()
        {
            /*------*Obtener código seleccionado del TreeView*------*/
            //var t = new Tools.TreeListHelper(treeEmpresas);
            var multiple_empresa = lkpEmpresa.EditValue.ToString(); //t.ObtenerCodigoConcatenadoDeNodoIndex(0);
            var multiple_sede = lkpSede.EditValue.ToString();//t.ObtenerCodigoConcatenadoDeNodoIndex(1);

            var objList = unit.SolicitudCompra.ConsultaVarias<eSolicitudCompra_Vista>(new PQSolicitudCompra()
            {
                Opcion = 1,
                Cod_empresa = multiple_empresa,
                Cod_sede_empresa = multiple_sede,
                Cod_almacen = ""//enviar códigos de almacén
            });

            bsListadoSolicitudes_Vista.DataSource = null;
            if (objList == null || objList.Count == 0) return;
            bsListadoSolicitudes_Vista.DataSource = objList.ToList();
            gvListadoSolicitudes_Vista.RefreshData();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            GenerarSolicitudDeCompra();
            InsertProveedorEnGrid();


            /* int valor = 0;
             foreach (int nRow in gvProductos.GetSelectedRows())
             {
                 if (gvProductos.GetRow(nRow - valor) is eProductos obj)
                 {

                     eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj2 = new eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle();
                     //eRequerimiento.eRequerimiento_Detalle obj2 = new eRequerimiento.eRequerimiento_Detalle();
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

                     bsDetalleOC.Add(obj2);//bsDetalleReq.Add(obj2);
                     eDetOrdenCompraEdit.Add(obj2);
                     bsProductos.Remove(obj);
                 }
                 valor = valor + 1;
             }
            */
            //gvDetalleReq.ExpandAllGroups();
        }
        private void btnProductos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnProductos.Down)
            {
                btnProductos.Caption = "Ocultar Productos";
                layProducto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //layDetalleOC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // btnAsignarProveedor.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //btnGenerarSolicitudOC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                btnProductos.Caption = "Agregar Productos";
                layProducto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //layDetalleOC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //btnAsignarProveedor.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //btnGenerarSolicitudOC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            int valor = 0;
            bool result = false;
            foreach (int nRow in gvDetalleOC.GetSelectedRows())// gvDetalleReq.GetSelectedRows())
            {
                //eRequerimiento.eRequerimiento_Detalle obj = gvDetalleOC.GetRow(nRow - valor) as eRequerimiento.eRequerimiento_Detalle;// gvDetalleReq.GetRow(nRow - valor) as eRequerimiento.eRequerimiento_Detalle;
                eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj = gvDetalleOC.GetRow(nRow - valor) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;// gvDetalleReq.GetRow(nRow - valor) as eRequerimiento.eRequerimiento_Detalle;
                                                                                                                                                                  //eProductos obj2 = new eProductos();

                //obj2.cod_tipo_servicio = obj.cod_tipo_servicio;
                //obj2.dsc_tipo_servicio = obj.dsc_tipo_servicio;
                //obj2.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                //obj2.dsc_subtipo_servicio = obj.dsc_subtipo_servicio;
                //obj2.cod_producto = obj.cod_producto;
                //obj2.dsc_producto = obj.dsc_producto;
                //obj2.cod_unidad_medida = obj.cod_unidad_medida;
                //obj2.dsc_simbolo = obj.dsc_simbolo;

                //bsProductos.Add(obj2);
                //bsDetalleOC.Remove(obj);// bsDetalleReq.Remove(obj);



                var r = unit.SolicitudCompra.RestablecerRequerimientoGenerada(
                    cod_empresa: empresa,
                    cod_sede_empresa: sede,
                    cod_requerimiento: obj.cod_requerimiento,
                    cod_producto: obj.cod_producto,
                    cod_orden_compra_servicio: txtOC.Text.Trim(),
                    elim_oc: "SI");

                result = r.IsSuccess;
                valor = valor + 1;
            }
            gvProductos.ExpandAllGroups();
            if (result)
            {
                CargarSolicitudCompra_Vista();
                CargarOrdenCompra(txtOC.Text.Trim());
            }
        }

        private class RQ { public int Cantidad { get; set; } }// analizar
        private List<eSolicitudCompra_Requerimientos> ObtenerRequerimientosPorProductos(string cod_producto)
        {
            var objList = unit.SolicitudCompra.ConsultaVarias<eSolicitudCompra_Requerimientos>(
               new PQSolicitudCompra() { Opcion = 2, Cod_producto = cod_producto, Cod_empresa = lkpEmpresa.EditValue.ToString() });
            return objList;
        }
        private void GenerarSolicitudDeCompra()
        {
            /*-----*Preparar data*-----*/
            if (!(gvListadoSolicitudes_Vista.GetFocusedRow() is eSolicitudCompra_Vista _)) return;
            string[] product = new string[gvListadoSolicitudes_Vista.GetSelectedRows().Count()]; int y = -1;
            foreach (var nRow in gvListadoSolicitudes_Vista.GetSelectedRows())
            {
                y++;
                var obj = gvListadoSolicitudes_Vista.GetRow(nRow) as eSolicitudCompra_Vista;
                product[y] = obj.cod_producto;
            }
            if (string.IsNullOrWhiteSpace(String.Join(",", product))) return;

            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando Orden de Compra", "Cargando...");
            /*-----*Obtener Listado de requerimiento por cada producto*-----*/
            var objRequerimientos = ObtenerRequerimientosPorProductos(String.Join(",", product));

            var helper = new RequerimientosHelper(unit);
            /*-----*Procesar Solicitud de Orden de Compra*-----*/
            string empresa = "";
            string sede = "";
            string solicitud = "";
            Int32 _anho = 1;
            string requerimientos = "";

            string productos = "";

            /*-----*Aquí se crea la OC de acuerdo a la cantidad de productos que se establece*-----*/
            try
            {
                //LLENA DATOS DE CABECERA PARA LA OC
                //foreach (int nRow in gvListadoRequerimientos.GetSelectedRows())
                foreach (var obj in objRequerimientos)
                {
                    //eRequerimiento obj = gvListadoRequerimientos.GetRow(nRow) as eRequerimiento;
                    // eSolicitudCompra_Requerimientos obj = gvListadoRequerimientos.GetRow(nRow) as eSolicitudCompra_Requerimientos;
                    //if (obj.flg_solicitud == "COMPRA")
                    {
                        empresa = lkpEmpresa.EditValue.ToString();//cod_empresa;// obj.cod_empresa;
                        sede = obj.cod_sede_empresa;
                        solicitud = "COMPRA";// obj.flg_solicitud;
                        _anho = obj.dsc_anho;
                        requerimientos = obj.cod_requerimiento + "," + requerimientos;
                        productos = obj.cod_producto + "," + productos;
                    }
                }

                string cod_orden_compra = "", flg_solicitud = "";
                Int32 dsc_anho = 1;

                //TRAE DATOS DEL PROVEEDOR Y PRODUCTOS DE LOS RQ SELECCIONADOS
                List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> prodProvReq;
                prodProvReq = unit.Requerimiento.Cargar_Prod_Prov_Requerimientos<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(12, empresa, sede, requerimientos, solicitud, _anho, productos);

                //SE COMIENZA A REVISAR LA OC
                for (int i = 0; i < prodProvReq.Count; i++)
                {
                    //(REVISAR)
                    if (i == 0)
                    {


                        //INSERTA CABECERA DE LA 1RA OC
                        eOrdenCompra_Servicio eOrdCom = helper.CreaCabecera(prodProvReq[i], "C", this.ordenCompraServicio, cod_ord_estado: "GEN");

                        cod_orden_compra = eOrdCom.cod_orden_compra_servicio;
                        flg_solicitud = eOrdCom.flg_solicitud;
                        dsc_anho = eOrdCom.dsc_anho;

                        prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                        prodProvReq[i].flg_solicitud = flg_solicitud;
                        prodProvReq[i].dsc_anho = dsc_anho;

                        //INSERTA DETALLE DE LA 1RA OC
                        eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = helper.CrearDetalle(prodProvReq[i]);
                    }
                    else
                    {
                        //VALIDA PROVEEDOR PARA GENERAR DISTINTAS OC
                        if (prodProvReq[i].cod_proveedor != prodProvReq[i - 1].cod_proveedor)
                        {
                            //SI ES DIFERENTE SE CREA NUEVA CABECERA
                            eOrdenCompra_Servicio eOrdCom = helper.CreaCabecera(prodProvReq[i], "C", this.ordenCompraServicio, cod_ord_estado: "GEN");

                            cod_orden_compra = eOrdCom.cod_orden_compra_servicio;
                            flg_solicitud = eOrdCom.flg_solicitud;
                            dsc_anho = eOrdCom.dsc_anho;

                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            //INSERTA DETALLE DE LA OC
                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = helper.CrearDetalle(prodProvReq[i]);
                        }
                        else
                        {
                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            //INSERTA DETALLE DE LA OC
                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = helper.CrearDetalle(prodProvReq[i]);
                        }
                    }
                }
                //CAMBIO DE ESTADO DEL RQ
                /*-----*Primero Validar si todos los productos se ha pasado como Solicitud de Compra*-----*/
                List<RQ> rq;
                rq = unit.Requerimiento.Cargar_Prod_Prov_Requerimientos<RQ>(11, empresa, sede, requerimientos.Split(',')[0]);
                string respuesta = "";
                if (rq != null && rq.Count > 0)
                {
                    //CargarRequerimientos_PrevioOC(this.cod_producto, this.cod_empresa);


                    if (rq[0].Cantidad == 0)
                        respuesta = unit.Requerimiento.GenerarOC_Requerimiento(empresa, sede, requerimientos, Program.Sesion.Usuario.cod_usuario, solicitud, _anho);
                }

                SplashScreenManager.CloseForm(false);


                if (respuesta.Contains("OK"))
                {
                    HNG.MessageSuccess("Órdenes generadas con éxito", "INFORMACION");
                    //MessageBox.Show("Órdenes generadas con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                /*-----*Refrescar GridView*-----*/
                //btnBuscador_Click(btnBuscador, new EventArgs());
                this.empresa = lkpEmpresa.EditValue.ToString();
                this.sede = lkpSede.EditValue.ToString();
                PrepararDatosParaInicializar(cod_orden_compra == "" ? ordenCompraServicio : cod_orden_compra, "C", _anho < 100 ? this.anho : _anho);
                //AbrirSolicitudDeCompra_Generada(cod_orden_compra_servicio: cod_orden_compra, flg_solicitud: "C", anho: anho); //solicitud:C
            }
            catch (Exception)
            {
                SplashScreenManager.CloseForm(false);
                HNG.MessageSuccess("Error al Crear Órdenes.", "ERROR");
                //MessageBox.Show("Error al Crear Órdenes.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PrepararDatosParaInicializar(string cod_orden_CS, string flg_solicitud, int anho)
        {
            this.ordenCompraServicio = cod_orden_CS;// obj.cod_orden_compra_servicio;
            this.solicitud = flg_solicitud;
            this.anho = anho;

            CargarOrdenCompra(cod_orden_CS);
            //btnProductos.Down = false;
            //layProducto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //layDetalleOC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            //btnProductos.PerformClick();
            //btnProductos_ItemClick(btnProductos, new EventArgs());

            CargarSolicitudCompra_Vista();
        }


        #endregion Asignar productos a Proveedores
        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // NOTA::::  NO PERMITIR GUARDAR SI NO TIENEN PROVEEDORES, SALE ERROR!!!
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Guardando Orden de Compra", "Cargando...");

                //Solo si se asigna un producto al proveedor.
                //  if (AsignarProductosProveedor) { Re_Cargar_ListaDetalle(); }

                //if (codigoProveedor == "") { MessageBox.Show("Debe seleccionar un proveedor.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtProveedor.Focus(); return; }
                //if (txtRUC.EditValue == null) { MessageBox.Show("Debe ingresar el RUC del proveedor.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtRUC.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { HNG.MessageWarning("Debe seleccionar un almacen.", "ADVERTENCIA"); lkpAlmacen.Focus(); return; }// MessageBox.Show("Debe seleccionar un almacen.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                if (dtpFechaDespacho.EditValue == null) { HNG.MessageWarning("Debe seleccionar una fecha de despacho.", "ADVERTENCIA"); dtpFechaDespacho.Focus(); return; }// MessageBox.Show("Debe seleccionar una fecha de despacho.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtpFechaDespacho.Focus(); return; }

                gvDetalleOC.RefreshData();

                List<eProveedor> listProv = new List<eProveedor>();
                List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> listProd = new List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>();

                foreach (eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj in eDetOrdenCompraEdit)
                {
                    eProveedor eProv = new eProveedor(); eProveedor eProv2 = new eProveedor(); eProv.cod_proveedor = obj.cod_proveedor_det; eProv.num_documento = obj.dsc_ruc_det;
                    eProv2 = listProv.Find(x => x.cod_proveedor == obj.cod_proveedor_det);
                    if (eProv2 == null) listProv.Add(eProv);
                }

                codigoProveedor = listProv[0].cod_proveedor;

                var OrdenCompra = AsignarValores();
                if (OrdenCompra == null)
                {
                    SplashScreenManager.CloseForm(false);
                    HNG.MessageWarning("Vuelve a asignar los valores antes de guardar.", "Guardar O.C.");
                    return;
                }
                eOrdenCompra_Servicio eOrdCom = OrdenCompra;



                if (AsignarProductosProveedor) { eOrdCom.cod_proveedor = codigoProveedorDet; }

                eOrdCom = unit.OrdenCompra_Servicio.Ins_Act_OrdenCompra_Servicio<eOrdenCompra_Servicio>(eOrdCom, Program.Sesion.Usuario.cod_usuario);

                empresa = eOrdCom.cod_empresa; sede = eOrdCom.cod_sede_empresa; ordenCompraServicio = eOrdCom.cod_orden_compra_servicio; solicitud = eOrdCom.flg_solicitud; anho = eOrdCom.dsc_anho;

                foreach (eProveedor obj in listProv)
                {
                    if (obj.cod_proveedor != codigoProveedor)
                    {
                        eOrdCom = OrdenCompra;// AsignarValores();


                        eOrdCom.cod_orden_compra_servicio = "";
                        eOrdCom.cod_proveedor = obj.cod_proveedor;
                        eOrdCom.dsc_ruc = obj.num_documento;

                        eOrdCom = unit.OrdenCompra_Servicio.Ins_Act_OrdenCompra_Servicio<eOrdenCompra_Servicio>(eOrdCom, Program.Sesion.Usuario.cod_usuario);

                        listProd = eDetOrdenCompraEdit.FindAll(x => x.cod_proveedor_det == obj.cod_proveedor);

                        CrearDetalle(listProd, eOrdCom);
                    }
                    else
                    {
                        listProd = eDetOrdenCompraEdit.FindAll(x => x.cod_proveedor_det == codigoProveedor);
                        CrearDetalle(listProd, eOrdCom);
                    }
                }

                SplashScreenManager.CloseForm(false);
                HNG.MessageSuccess("Se registro el documento de manera satisfactoria.", "INFORMACION");
                //MessageBox.Show(, MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarOrdenCompra();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                HNG.MessageError(ex.ToString(), "GUARDAR OC.");
                //MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eOrdenCompra_Servicio AsignarValores()
        {
            if (lkpModPago.EditValue == null)
            {
                SplashScreenManager.CloseForm(false);
                HNG.MessageWarning("No se ha seleccionado la modalidad de pago.", "Orden Compra");
                lkpModPago.Select();
                return null;
            }
            if (lkpMoneda.EditValue == null)
            {
                SplashScreenManager.CloseForm(false);
                HNG.MessageWarning("No se ha seleccionado la moneda.", "Orden Compra");
                lkpMoneda.Select();
                return null;
            }

            eOrdenCompra_Servicio eOC = new eOrdenCompra_Servicio();

            eOC.cod_empresa = lkpEmpresa.EditValue.ToString();
            eOC.cod_sede_empresa = lkpSede.EditValue.ToString();
            eOC.cod_orden_compra_servicio = txtOC.EditValue.ToString();
            eOC.num_cotizacion = txtCotizacion.EditValue.ToString();
            eOC.cod_proveedor = codigoProveedor;
            eOC.dsc_ruc = txtRUC.EditValue.ToString();
            eOC.flg_solicitud = "C";
            eOC.cod_almacen = lkpAlmacen.EditValue == null ? "" : lkpAlmacen.EditValue.ToString();
            eOC.cod_modalidad_pago = lkpModPago.EditValue.ToString();
            eOC.cod_moneda = lkpMoneda.EditValue.ToString();
            eOC.dsc_direccion_despacho = txtDireccion.EditValue == null ? "" : txtDireccion.EditValue.ToString();
            eOC.fch_emision = Convert.ToDateTime(dtpFechaEmision.EditValue);
            eOC.fch_despacho = Convert.ToDateTime(dtpFechaDespacho.EditValue);
            eOC.dsc_terminos_condiciones = txtTermCond.EditValue == null ? "" : txtTermCond.EditValue.ToString();
            eOC.imp_subtotal = 0;
            eOC.imp_igv = 0;
            eOC.imp_total = 0;
            eOC.dsc_imp_total = txtTotalLetras.EditValue == null ? "" : txtTotalLetras.EditValue.ToString();
            eOC.prc_CV = Convert.ToDecimal(txtCV.EditValue);
            eOC.prc_LI = Convert.ToDecimal(txtLI.EditValue);
            eOC.prc_CB = Convert.ToDecimal(txtCB.EditValue);
            eOC.prc_GG = Convert.ToDecimal(txtGG.EditValue);
            eOC.prc_ADM = Convert.ToDecimal(txtADM.EditValue);
            eOC.prc_OPER = Convert.ToDecimal(txtOPER.EditValue);
            eOC.prc_GV = Convert.ToDecimal(txtGV.EditValue);
            eOC.dsc_observaciones = txtObservaciones.EditValue == null ? "" : txtObservaciones.EditValue.ToString();

            return eOC;
        }

        private void CrearDetalle(List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> listProd, eOrdenCompra_Servicio eOrdCom)
        {
            List<eProductos.eProductosProveedor> listProdProv = unit.OrdenCompra_Servicio.Cargar_Detalle_OrdenCompra_Servicio<eProductos.eProductosProveedor>(11, codigoEmpresa, sede, ordenCompraServicio: ordenCompraServicio);
            switch (accion)
            {
                case OrdenCompra.Editar:
                    string respuesta = unit.OrdenCompra_Servicio.Limpiar_Det_OrdenCompra_Servicio(eOrdCom.cod_empresa, eOrdCom.cod_sede_empresa, eOrdCom.cod_orden_compra_servicio, eOrdCom.flg_solicitud, eOrdCom.dsc_anho);
                    break;
                case OrdenCompra.Nuevo:
                    {
                        if (AsignarProductosProveedor)
                        {
                            string rpt = unit.OrdenCompra_Servicio.Limpiar_Det_OrdenCompra_Servicio(eOrdCom.cod_empresa, eOrdCom.cod_sede_empresa, eOrdCom.cod_orden_compra_servicio, eOrdCom.flg_solicitud, eOrdCom.dsc_anho);
                        }
                        break;
                    }
            }

            string cod_qr = "";
            foreach (eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle objDet in listProd)
            {
                cod_qr = objDet.cod_requerimiento;

                objDet.cod_proveedor = objDet.cod_proveedor_det;
                objDet.dsc_ruc = objDet.dsc_ruc_det;
                objDet.cod_empresa = eOrdCom.cod_empresa;
                objDet.cod_sede_empresa = eOrdCom.cod_sede_empresa;
                objDet.cod_orden_compra_servicio = eOrdCom.cod_orden_compra_servicio;
                objDet.flg_solicitud = eOrdCom.flg_solicitud;
                objDet.dsc_anho = eOrdCom.dsc_anho;

                eProductos.eProductosTarifas eProdTar = new eProductos.eProductosTarifas();

                eProdTar.cod_empresa = objDet.cod_empresa;
                eProdTar.cod_tipo_servicio = objDet.cod_tipo_servicio;
                eProdTar.cod_subtipo_servicio = objDet.cod_subtipo_servicio;
                eProdTar.cod_producto = objDet.cod_producto;
                eProdTar.num_item = objDet.num_item;
                eProdTar.fch_inicio = Convert.ToDateTime(DateTime.Now.ToString("d"));
                eProdTar.fch_fin = new DateTime(2999, 12, 31);
                eProdTar.imp_costo = objDet.imp_unitario;
                eProdTar.dsc_observacion = "";
                eProdTar.cod_proveedor = objDet.cod_proveedor;
                eProdTar.dsc_ruc = objDet.dsc_ruc;

                eProdTar = unit.Logistica.Insertar_Actualizar_ProductoCostos<eProductos.eProductosTarifas>(eProdTar);

                if (listProdProv.FindAll(x => x.cod_proveedor == objDet.cod_proveedor && x.cod_producto == objDet.cod_producto).Count == 0)
                {
                    eProductos.eProductosProveedor eProdProv = new eProductos.eProductosProveedor();

                    eProdProv.cod_tipo_servicio = objDet.cod_tipo_servicio;
                    eProdProv.cod_subtipo_servicio = objDet.cod_subtipo_servicio;
                    eProdProv.cod_producto = objDet.cod_producto;
                    eProdProv.dsc_ruc = objDet.dsc_ruc;
                    eProdProv.cod_proveedor = objDet.cod_proveedor;
                    eProdProv.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                    unit.Logistica.Insertar_Actualizar_ProductosProveedor<eProductos.eProductosProveedor>(eProdProv);

                }

                objDet.num_item = eProdTar.num_item;

                eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOC = unit.OrdenCompra_Servicio.Ins_Act_Detalle_OrdenCompra_Servicio<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(objDet, Program.Sesion.Usuario.cod_usuario);
            }
            /*-----*Si se elimina un producto de la Orden de Compra, restablecer en el requerimiento.*-----*/
            if (productos_eliminar != null)
            {
                foreach (var item in productos_eliminar)
                {
                    if (item == null) continue;
                    var result = unit.SolicitudCompra.RestablecerRequerimientoGenerada(
                        cod_empresa: empresa,
                        cod_sede_empresa: sede,
                        cod_requerimiento: cod_qr,
                        cod_producto: item.ToString());
                }
            }
        }

        private void btnVerProveedor_Click(object sender, EventArgs e)
        {
            frmMantProveedor frm = new frmMantProveedor();
            frm.cod_proveedor = codigoProveedor;
            frm.MiAccion = Proveedor.Vista;
            frm.cod_empresa = lkpEmpresa.EditValue.ToString();
            frm.ShowDialog();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Busqueda("", "Cliente");
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;

            switch (tipo)
            {
                case "Proveedor":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    break;
                case "ProveedorDet":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Proveedor":
                    codigoProveedor = frm.codigo;
                    txtProveedor.EditValue = frm.descripcion;
                    txtRUC.EditValue = frm.ruc;
                    lkpModPago.EditValue = frm.cod_condicion2;

                    //MessageBox.Show(frm.cod_condicion2);
                    for (int i = 0; i < gvDetalleOC.RowCount; i++)
                    {
                        eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdComNue = gvDetalleOC.GetRow(i) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;

                        eOrdComNue.dsc_proveedor_det = frm.descripcion;
                        eOrdComNue.dsc_ruc_det = frm.ruc;
                    }

                    gvDetalleOC.RefreshData();
                    break;
                case "ProveedorDet":
                    codigoProveedorDet = frm.codigo;
                    dscProveedorDet = frm.descripcion;
                    rucProveedorDet = frm.ruc;
                    lkpModPago.EditValue = frm.cod_condicion2;
                    txtRUC.EditValue = frm.ruc;
                    //MessageBox.Show(frm.cod_condicion2);
                    lkpModPago.EditValue = frm.cod_condicion2;
                    break;
            }
        }

        private void InsertProveedorEnGrid()
        {

            List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> objList = eDetOrdenCompraEdit;// bsDetalleOC.DataSource as List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>;
            if (objList != null && objList.Count > 0)
            {
                objList.ForEach(o =>
                {
                    o.cod_proveedor_det = codigoProveedorDet;
                    o.dsc_proveedor_det = dscProveedorDet;
                    o.dsc_ruc_det = rucProveedorDet;
                    o.num_item = 0;
                });
                eDetOrdenCompraEdit = objList.ToList();
                bsDetalleOC.DataSource = eDetOrdenCompraEdit;
            }
            gvDetalleOC.RefreshData();
        }
        private void AsignarProveedores()
        {
            var sel = gvDetalleOC.GetSelectedRows();
            if (sel.Count() == 0)
            {
                if (!AsignarProductosProveedor) { HNG.MessageWarning("No se ha seleccionado ningun produto.", "Asignar Proveedor"); return; }
                //MessageBox.Show("No se ha seleccionado ningun produto.", "Asignar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            }


            Busqueda("", "ProveedorDet");
            if (AsignarProductosProveedor)
            {
                txtProveedor.Text = dscProveedorDet;

                //gvDetalleOC.SelectAll();

                InsertProveedorEnGrid();
            }
            else
            {
                foreach (var nRow in gvDetalleOC.GetSelectedRows())
                {
                    eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdComNue =
                        gvDetalleOC.GetRow(nRow) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
                    if (codigoProveedorDet != "")
                    {
                        eOrdComNue.cod_proveedor_det = codigoProveedorDet;
                        eOrdComNue.dsc_proveedor_det = dscProveedorDet;
                        eOrdComNue.dsc_ruc_det = rucProveedorDet;
                        eOrdComNue.num_item = 0;
                    }
                }
            }



            gvDetalleOC.RefreshData();

            //eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdComNue = gvDetalleOC.GetRow(gvDetalleOC.FocusedRowHandle) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;

            //if (codigoProveedorDet != "")
            //{
            //    eOrdComNue.cod_proveedor_det = codigoProveedorDet;
            //    eOrdComNue.dsc_proveedor_det = dscProveedorDet;
            //    eOrdComNue.dsc_ruc_det = rucProveedorDet;
            //    eOrdComNue.num_item = 0;
            //}
            //gvDetalleOC.RefreshData();
        }

        private void lkpAlmacen_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                eAlmacen eAlm = new eAlmacen();
                eAlm = unit.OrdenCompra_Servicio.BuscarAlmacen<eAlmacen>(lkpAlmacen.EditValue.ToString(), lkpEmpresa.EditValue.ToString());
                if (eAlm == null) return;
                txtDireccion.EditValue = eAlm.dsc_direccion;
            }
            catch (Exception ex)
            {
                //Mensaje no se ha encontrado almacén...
                //throw;
            }
        }

        private void dtpFechaEmision_EditValueChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(dtpFechaEmision.EditValue);

            dtpFechaDespacho.EditValue = date;
            //dtpFechaDespacho.Properties.MinValue = date;
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            igv = subTotal * Convert.ToDecimal(0.18);
            txtIGV.EditValue = Math.Round(igv, 2).ToString();
        }

        private void txtIGV_TextChanged(object sender, EventArgs e)
        {
            total = subTotal + igv;
            txtTotal.EditValue = Math.Round(total, 2).ToString();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtTotalLetras.EditValue = unit.OrdenCompra_Servicio.Monto_Letras(total).Rows[0].ItemArray[0].ToString();
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            eRequerimientoDetalle = unit.OrdenCompra_Servicio.BuscarRequerimiento<eRequerimiento.eRequerimiento_Detalle>(codigoCliente);

            bsProductosReq.DataSource = eRequerimientoDetalle;
        }

        private void gvProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }

        private void gvProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvProductos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {

            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Proveedor");
            }
            string dato = unit.Globales.pKeyPress(txtProveedor, e);
            if (dato != "")
            {
                Busqueda(dato, "Proveedor");
            }
        }


        private void gvDetalleReq_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvDetalleReq_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void btnAsignarProveedor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AsignarProveedores();
        }

        int _index = 0;
        string[] productos_eliminar = new string[9];// Aumentar la cantidad de items de la lista.
        private void btnEliminar_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            // ARREGLAR  ELIMINAR  UNA  FILA Y MOSTRAR L
            var obj1 = gvDetalleOC.GetFocusedRow() as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
            // eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdComNue = gvDetalleOC.GetRow(i) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;

            var index = eDetOrdenCompraEdit.FindIndex(o => o == obj1);

            if (eDetOrdenCompraEdit.Count > 1)
            {
                productos_eliminar[_index] = obj1.cod_producto;
                eDetOrdenCompraEdit.RemoveAt(index);
                _index++;
            }

            gvDetalleOC.RefreshData();
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            //Cargar Almacén si se va a agregar un nuevo producto.
            unit.OrdenCompra_Servicio.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());

        }

        private void lkpSede_EditValueChanged(object sender, EventArgs e)
        {
            //Cargar Almacén si se va a agregar un nuevo producto.
            if (lkpSede.EditValue != null)
            {
                unit.OrdenCompra_Servicio.CargaCombosLookUp("Almacenes", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSede.EditValue.ToString());
                lkpAlmacen.EditValue = "00001";
            }
        }


        // Atributos para pintar grilla
        private readonly Brush GREEN = Brushes.MediumSeaGreen;
        private readonly Brush RED = Brushes.Crimson;
        private readonly Brush ORANGE = Brushes.Gold; int markWidth = 16;
        private void gvListadoSolicitudes_Vista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (!(gvListadoSolicitudes_Vista.GetRow(e.RowHandle) is eSolicitudCompra_Vista obj)) return;

                    e.DefaultDraw();
                    if (e.Column.FieldName.Equals("num_cantidad")) { e.Column.AppearanceCell.BackColor = Color.FromArgb(220, 220, 220); }
                    if (e.Column.FieldName.Equals("num_restante")) { e.Column.AppearanceCell.BackColor = Color.FromArgb(173, 217, 147); }
                    if (e.Column.FieldName.Equals("num_cantidad_stock")) { e.Column.AppearanceCell.BackColor = Color.FromArgb(220, 220, 220); }
                    if (e.Column.FieldName.Equals("dsc_diferencia"))
                    {
                        e.Column.AppearanceCell.BackColor = Color.FromArgb(234, 170, 234);
                        Brush brush; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        int cellValue = Convert.ToInt32(e.CellValue);
                        //MessageBox.Show(cellValue.ToString());
                        brush = cellValue > 30 ? GREEN : cellValue >= 0 && cellValue <= 29 ? ORANGE : RED;
                        e.Graphics.FillEllipse(brush, new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                    if (e.Column.FieldName.Equals("imp_unitario")) { e.Column.AppearanceCell.BackColor = Color.White; }
                    if (e.Column.FieldName.Equals("imp_total")) { e.Column.AppearanceCell.BackColor = Color.FromArgb(220, 220, 220); }
                }
            }
            catch (Exception ex) { HNG.MessageError(ex.ToString(), "ERROR"); }//MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void frmMantOrdenCompra_Shown(object sender, EventArgs e)
        {
            lkpSede_EditValueChanged(lkpSede, new EventArgs());
        }

        private void gvDetalleReq_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvDetalleOC_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gvDetalleOC.FocusedColumn.Name == "colnum_cantidad1")
            {
                eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj = gvDetalleOC.GetFocusedRow() as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;

                //nuevo
                if (!AsignarProductosProveedor)
                {
                    if (obj.num_cantidad == 0) { HNG.MessageWarning("Debe seleccionar una cantidad mayor a cero.", "ADVERTENCIA"); obj.num_cantidad = 1; return; }
                    if (obj.num_cantidad > obj.num_cantidad_req) { HNG.MessageWarning("Debe seleccionar una cantidad igual o menor a la requerida.", "ADVERTENCIA"); obj.num_cantidad = obj.num_cantidad_req; return; }
                }

                obj.imp_total_det = obj.num_cantidad * obj.imp_unitario;

                subTotal = 0;
                decimal ctd_total = 0, ctd_req = obj.num_cantidad_req;

                if (!AsignarProductosProveedor)
                {
                    if (obj.num_cantidad <= obj.num_cantidad_req)
                    {
                        eDetOrdenCompraEdit.RemoveAll(x => x.cod_producto == obj.cod_producto && x.cod_proveedor_det == null);

                        ctd_total = eDetOrdenCompraEdit.Where(x => x.cod_producto == obj.cod_producto).Sum(x => x.num_cantidad);

                        if (ctd_req - ctd_total > 0)
                        {
                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle objRestante = new eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle();
                            objRestante.cod_empresa = obj.cod_empresa;
                            objRestante.cod_requerimiento = obj.cod_requerimiento;
                            objRestante.cod_tipo_servicio = obj.cod_tipo_servicio;
                            objRestante.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                            objRestante.cod_producto = obj.cod_producto;
                            objRestante.dsc_producto = obj.dsc_producto;
                            objRestante.cod_unidad_medida = obj.cod_unidad_medida;
                            objRestante.dsc_simbolo = obj.dsc_simbolo;
                            objRestante.cod_proveedor = null;
                            objRestante.cod_proveedor_det = null;
                            objRestante.dsc_ruc = null;
                            objRestante.dsc_ruc_det = null;
                            objRestante.num_cantidad = ctd_req - ctd_total;
                            objRestante.num_cantidad_req = obj.num_cantidad_req;
                            objRestante.num_item = obj.num_item;
                            objRestante.imp_unitario = obj.imp_unitario;
                            objRestante.imp_total_det = objRestante.num_cantidad * objRestante.imp_unitario;

                            eDetOrdenCompraEdit.Add(objRestante);
                        }

                        if (ctd_req - ctd_total < 0) obj.num_cantidad = obj.num_cantidad + (ctd_req - ctd_total);
                    }

                    bsDetalleOC.DataSource = eDetOrdenCompraEdit;
                    gvDetalleOC.RefreshData();
                }

                gvDetalleOC.PostEditor();
                for (int x = 0; x < gvDetalleOC.DataRowCount; x++)
                {
                    eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdCom = gvDetalleOC.GetRow(x) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
                    subTotal = eOrdCom.imp_total_det + subTotal;
                }

                txtSubTotal.EditValue = Math.Round(subTotal, 2).ToString();
            }


            if (gvDetalleOC.FocusedColumn.Name == "colimp_unitario")
            {
                subTotal = 0;

                gvDetalleOC.PostEditor();
                for (int x = 0; x < gvDetalleOC.DataRowCount; x++)
                {
                    eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdCom = gvDetalleOC.GetRow(x) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;
                    eOrdCom.num_item = 0;
                    eOrdCom.imp_total_det = eOrdCom.num_cantidad * eOrdCom.imp_unitario;
                    subTotal = subTotal + eOrdCom.imp_total_det;
                }

                txtSubTotal.EditValue = Math.Round(subTotal, 2).ToString();
            }
        }

        private void rbtnBuscarProveedorDet_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Busqueda("", "ProveedorDet");

            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eOrdComNue = gvDetalleOC.GetRow(gvDetalleOC.FocusedRowHandle) as eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle;

            if (codigoProveedorDet != "")
            {
                eOrdComNue.cod_proveedor_det = codigoProveedorDet;
                eOrdComNue.dsc_proveedor_det = dscProveedorDet;
                eOrdComNue.dsc_ruc_det = rucProveedorDet;
                eOrdComNue.num_item = 0;
            }
            gvDetalleOC.RefreshData();
        }
    }
}