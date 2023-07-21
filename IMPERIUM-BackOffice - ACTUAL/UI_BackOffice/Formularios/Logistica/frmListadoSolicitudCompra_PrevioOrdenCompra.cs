using BE_BackOffice;
using DevExpress.DataAccess.Native.EntityFramework;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Commands;
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

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmListadoSolicitudCompra_PrevioOrdenCompra : HNG_Tools.ModalForm //Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        private readonly frmListadoSolicitudCompra frmHandler;
        private string cod_empresa;
        private string cod_producto;
        public frmListadoSolicitudCompra_PrevioOrdenCompra()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurar_formulario();
        }
        public frmListadoSolicitudCompra_PrevioOrdenCompra(frmListadoSolicitudCompra frmHandler)
        {
            InitializeComponent();
            unit = new UnitOfWork();
            this.frmHandler = frmHandler;
            configurar_formulario();
        }
        private void configurar_formulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gc: gcListadoRequerimientos, gv: gvListadoRequerimientos, editable: true);
            gvListadoRequerimientos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.TitleBackColor = Program.Sesion.Colores.Verde;
        }

        internal void CargarRequerimientos_PrevioOC(string cod_producto, string cod_empresa)
        {
            this.cod_empresa = cod_empresa; this.cod_producto = cod_producto;
            var objList = unit.SolicitudCompra.ConsultaVarias<eSolicitudCompra_Requerimientos>(
                new PQSolicitudCompra() { Opcion = 2, Cod_producto = cod_producto, Cod_empresa = cod_empresa });
            bsListadoRequerimientos.DataSource = null;
            if (objList == null || objList.Count == 0) return;

            bsListadoRequerimientos.DataSource = objList.ToList();
            gvListadoRequerimientos.ExpandAllGroups();
            gvListadoRequerimientos.RefreshData();
        }
        private void frmListadoSolicitudCompra_PrevioOrdenCompra_Load(object sender, EventArgs e)
        {

        }

        private void gvListadoRequerimientos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            string cell = gvListadoRequerimientos.GetRowCellDisplayText(e.RowHandle, e.Column);

            if (cell == "0" || cell == "0." || cell == "0.0" || cell == "0.00" || cell == "0.000" || cell == "0.0000")
            { e.Appearance.ForeColor = Color.Red; }
            else { e.Appearance.ForeColor = Color.Black; }
        }

        private void gvListadoRequerimientos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarReq();
        }

        private void MostrarReq()
        {
            if (!(gvListadoRequerimientos.GetFocusedRow() is eSolicitudCompra_Requerimientos obj)) return;

            List<eRequerimiento> reqCompletos =
                unit.Requerimiento.ListarRequerimiento<eRequerimiento>(
                    10, cod_empresa: cod_empresa,
                    cod_tipo_fecha: "01",
                    fch_inicio: Convert.ToDateTime("2020-01-01").ToString("yyyyMMdd"),
                    fch_fin: Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));

            List<eRequerimiento> reqAprobados = reqCompletos.FindAll(x => x.cod_estado_requerimiento == "APROBADO");

            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5 || x.cod_perfil == 26);

            eRequerimiento req = reqAprobados.FirstOrDefault(
                (r) => r.cod_empresa == cod_empresa && r.cod_requerimiento == obj.cod_requerimiento);

            frmMantRequerimientosCompra frm = new frmMantRequerimientosCompra(ParentFormType.ListadoSolicitudCompra);
            frm.empresa = cod_empresa;
            frm.sede = obj.cod_sede_empresa;
            frm.requerimiento = obj.cod_requerimiento;
            frm.solicitud = "C";// obj.flg_solicitud.ToString().Substring(0, 1); 
            frm.anho = 2022;// obj.dsc_anho; Colocar el año correspondiente
            frm.WindowState = FormWindowState.Maximized;

            frm.accion = RequerimientoCompra.Vista;// oPerfilLog != null ? RequerimientoCompra.Editar : RequerimientoCompra.Vista;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            { CargarRequerimientos_PrevioOC(cod_producto: cod_producto, cod_empresa: cod_empresa); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando Orden de Compra", "Cargando...");

            string empresa = "";
            string sede = "";
            string solicitud = "";
            Int32 anho = 1;
            string requerimientos = "";

            string productos = "";

            /*-----*Aquí se crea la OC de acuerdo a la cantidad de productos que se establece*-----*/
            try
            {
                //LLENA DATOS DE CABECERA PARA LA OC
                foreach (int nRow in gvListadoRequerimientos.GetSelectedRows())
                {
                    //eRequerimiento obj = gvListadoRequerimientos.GetRow(nRow) as eRequerimiento;
                    eSolicitudCompra_Requerimientos obj = gvListadoRequerimientos.GetRow(nRow) as eSolicitudCompra_Requerimientos;
                    //if (obj.flg_solicitud == "COMPRA")
                    {
                        empresa = cod_empresa;// obj.cod_empresa;
                        sede = obj.cod_sede_empresa;
                        solicitud = "COMPRA";// obj.flg_solicitud;
                        anho = obj.dsc_anho;
                        requerimientos = obj.cod_requerimiento + "," + requerimientos;
                        productos = obj.cod_producto + "," + productos;
                    }
                }

                string cod_orden_compra = "", flg_solicitud = "";
                Int32 dsc_anho = 1;

                //TRAE DATOS DEL PROVEEDOR Y PRODUCTOS DE LOS RQ SELECCIONADOS
                List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> prodProvReq;
                prodProvReq = unit.Requerimiento.Cargar_Prod_Prov_Requerimientos<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(12, empresa, sede, requerimientos, solicitud, anho, productos);

                //SE COMIENZA A REVISAR LA OC
                for (int i = 0; i < prodProvReq.Count; i++)
                {
                    //(REVISAR)
                    if (i == 0)
                    {


                        //INSERTA CABECERA DE LA 1RA OC
                        eOrdenCompra_Servicio eOrdCom = CreaCabecera(prodProvReq[i], "C");

                        cod_orden_compra = eOrdCom.cod_orden_compra_servicio;
                        flg_solicitud = eOrdCom.flg_solicitud;
                        dsc_anho = eOrdCom.dsc_anho;

                        prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                        prodProvReq[i].flg_solicitud = flg_solicitud;
                        prodProvReq[i].dsc_anho = dsc_anho;

                        //INSERTA DETALLE DE LA 1RA OC
                        eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = CrearDetalle(prodProvReq[i]);
                    }
                    else
                    {
                        //VALIDA PROVEEDOR PARA GENERAR DISTINTAS OC
                        if (prodProvReq[i].cod_proveedor != prodProvReq[i - 1].cod_proveedor)
                        {
                            //SI ES DIFERENTE SE CREA NUEVA CABECERA
                            eOrdenCompra_Servicio eOrdCom = CreaCabecera(prodProvReq[i], "C");

                            cod_orden_compra = eOrdCom.cod_orden_compra_servicio;
                            flg_solicitud = eOrdCom.flg_solicitud;
                            dsc_anho = eOrdCom.dsc_anho;

                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            //INSERTA DETALLE DE LA OC
                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = CrearDetalle(prodProvReq[i]);
                        }
                        else
                        {
                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            //INSERTA DETALLE DE LA OC
                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = CrearDetalle(prodProvReq[i]);
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
                    if (rq[0].Cantidad == 0)
                        respuesta = unit.Requerimiento.GenerarOC_Requerimiento(empresa, sede, requerimientos, Program.Sesion.Usuario.cod_usuario, solicitud, anho);
                }

                SplashScreenManager.CloseForm();

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Órdenes generadas con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                /*-----*Refrescar GridView*-----*/
                frmHandler.btnBuscador_Click(sender, new EventArgs());
                CargarRequerimientos_PrevioOC(this.cod_producto, this.cod_empresa);
            }
            catch (Exception)
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show("Error al Crear Órdenes.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //BuscarRequerimientos(); // SALIR???
        }
        private class RQ { public int Cantidad { get; set; } }
        private eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle CrearDetalle(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj)
        {
            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOC = new eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle();

            eDetOC.cod_empresa = obj.cod_empresa;
            eDetOC.cod_sede_empresa = obj.cod_sede_empresa;
            eDetOC.cod_orden_compra_servicio = obj.cod_orden_compra_servicio;
            eDetOC.flg_solicitud = obj.flg_solicitud;
            eDetOC.dsc_anho = obj.dsc_anho;
            eDetOC.num_item = obj.num_item;
            eDetOC.cod_requerimiento = obj.cod_requerimiento;
            eDetOC.cod_proveedor = obj.cod_proveedor;
            eDetOC.dsc_ruc = obj.dsc_ruc;
            eDetOC.cod_tipo_servicio = obj.cod_tipo_servicio;
            eDetOC.cod_subtipo_servicio = obj.cod_subtipo_servicio;
            eDetOC.cod_producto = obj.cod_producto;
            eDetOC.dsc_servicio = "";
            eDetOC.cod_unidad_medida = obj.cod_unidad_medida;
            eDetOC.num_cantidad = obj.num_cantidad;
            eDetOC.imp_unitario = obj.imp_unitario;
            eDetOC.imp_total_det = obj.imp_total_det;

            /*-----*Adicionar flg_solicitaOC a producto que se ha ordenado*-----*/
            eDetOC.flg_solicitaOC = "SI"; 

            eDetOC = unit.OrdenCompra_Servicio.Ins_Act_Detalle_OrdenCompra_Servicio<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(eDetOC, Program.Sesion.Usuario.cod_usuario);

            return eDetOC;
        }
        private eOrdenCompra_Servicio CreaCabecera(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj, string solicitud)
        {
            eOrdenCompra_Servicio eOC = new eOrdenCompra_Servicio();

            eOC.cod_empresa = obj.cod_empresa;
            eOC.cod_sede_empresa = obj.cod_sede_empresa;
            eOC.cod_orden_compra_servicio = "";
            eOC.num_cotizacion = "N/A";
            eOC.cod_proveedor = obj.cod_proveedor;
            eOC.dsc_ruc = obj.dsc_ruc;
            eOC.flg_solicitud = solicitud;
            eOC.cod_almacen = "";
            eOC.cod_modalidad_pago = "";
            eOC.dsc_direccion_despacho = "";
            eOC.fch_emision = DateTime.Now;
            eOC.fch_despacho = new DateTime(1999, 01, 01);
            eOC.dsc_terminos_condiciones = "";
            eOC.imp_subtotal = 0;
            eOC.imp_igv = 0;
            eOC.imp_total = 0;
            eOC.dsc_imp_total = "";
            eOC.prc_CV = 0;
            eOC.prc_LI = 0;
            eOC.prc_CB = 0;
            eOC.prc_GG = 0;
            eOC.prc_ADM = 0;
            eOC.prc_OPER = 0;
            eOC.prc_GV = 0;
            eOC.dsc_observaciones = "";

            eOC = unit.OrdenCompra_Servicio.Ins_Act_OrdenCompra_Servicio<eOrdenCompra_Servicio>(eOC, Program.Sesion.Usuario.cod_usuario);

            return eOC;
        }
    }
}