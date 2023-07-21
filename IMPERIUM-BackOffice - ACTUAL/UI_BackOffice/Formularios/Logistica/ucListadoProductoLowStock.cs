using BE_BackOffice;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using UI_BackOffice.Tools;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class ucListadoProductoLowStock : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly string CodEmpresa;
        //private readonly frmListadoProductoPrecios Handler;
        private readonly UnitOfWork unitOfWork;
        private List<Producto/*eProductos*/> OldListProductos;
        private List<Producto/*eProductos*/> NewListProductos;
        private enum ListProductosType { Agregar, Remover }
        private ModalView Handler;
        public ucListadoProductoLowStock(string codEmpresa, ModalView handler, /*frmListadoProductoPrecios handler,*/ UnitOfWork unitOfWork, List<eProductos> listProductos)
        {
            InitializeComponent();
            CodEmpresa = codEmpresa;
            Handler = handler;
            this.unitOfWork = unitOfWork;


            configurar_formulario();
            //if (OldListProductos == null) return;
            NewListProductos = new List<Producto>();
            OldListProductos = listProductos.Select((s) => new Producto
            {
                cod_tipo_servicio = s.cod_tipo_servicio,
                dsc_tipo_servicio = s.dsc_tipo_servicio,
                cod_subtipo_servicio = s.cod_subtipo_servicio,
                dsc_subtipo_servicio = s.dsc_subtipo_servicio,
                cod_producto = s.cod_producto,
                dsc_producto = s.dsc_producto,
                cod_unidad_medida = s.cod_unidad_medida,
                dsc_unidad_medida = s.dsc_unidad_medida,
                imp_costo_actual = s.imp_costo_actual,
                cod_proveedor = s.cod_proveedor,
                dsc_proveedor = s.dsc_proveedor,
                ctd_stock_actual = s.ctd_stock_actual,
                flg_activo = s.flg_activo,
                ctd_stock_minimo = s.ctd_stock_minimo,
                num_cantidad = (s.ctd_stock_actual < s.ctd_stock_minimo ? s.ctd_stock_actual - s.ctd_stock_minimo : 0)
            }).Where((stock) => stock.num_cantidad < 0)
                .ToList();

            GetListadoProductos();
            //Handler.btnEnviarRequerimientos.ItemClick += (sender, args) => sssss();
        }
        private void EnviarRequerimientos()
        {
            if (NewListProductos != null && NewListProductos.Count > 0)
            {
                frmMantRequerimientosCompra frm = new frmMantRequerimientosCompra(ParentFormType.ListadoProductoLowStock);
                frm.eDetRequerimientoEdit =// new List<eRequerimiento.eRequerimiento_Detalle>();
                NewListProductos.Select((obj) => new eRequerimiento.eRequerimiento_Detalle
                {
                    cod_tipo_servicio = obj.cod_tipo_servicio,
                    dsc_tipo_servicio = obj.dsc_tipo_servicio,
                    cod_subtipo_servicio = obj.cod_subtipo_servicio,
                    dsc_subtipo_servicio = obj.dsc_subtipo_servicio,
                    cod_producto = obj.cod_producto,
                    dsc_producto = obj.dsc_producto,
                    cod_unidad_medida = obj.cod_unidad_medida,
                    dsc_simbolo = obj.dsc_unidad_medida,
                    flg_generaOC = "SI",
                    Sel_generaOC = true,
                    num_cantidad = obj.num_cantidad,
                }).ToList();

                frm.codigoEmpresa = CodEmpresa;
                frm.WindowState = FormWindowState.Maximized;
                this.Hide();
                frm.ShowDialog();
                Handler.Dispose();
                this.Dispose();
            }
        }
        private void btnEnviarRequerimientos_ItemClick(object sender, ItemClickEventArgs e)
        {




            //Handler.btnEnviarRequerimientos.Enabled = false;
            //await Task.Delay(1000);
            //Handler.btnEnviarRequerimientos.Enabled = true;
            //System.Windows.MessageBox.Show("hola mundo");
            //if (string.IsNullOrEmpty(select_cod_empresa)) return;

            //frmMantRequerimientosCompra frm = new frmMantRequerimientosCompra();
            //frm.codigoEmpresa = select_cod_empresa;
            //frm.WindowState = FormWindowState.Maximized;
            //frm.ShowDialog();
        }

        private void ActualizarItems(ListProductosType productosType)
        {
            switch (productosType)
            {
                case ListProductosType.Agregar:
                    Producto objOld = gvListadoProductos.GetFocusedRow() as Producto;
                    if (objOld != null)
                    {
                        if (objOld.num_cantidad < 0)
                        {
                            if (objOld.cod_proveedor == null)// Si no tiene proveedor, no debe permitir la siguiente acción
                            {
                                HNG.MessageWarning("Este producto no tiene proveedor asignado.", "Productos sin Stock");
                                return;
                            }

                            objOld.num_cantidad *= -1;

                            NewListProductos.Add(objOld);
                            OldListProductos.Remove(objOld);
                            gvListadoProductos.RefreshData();
                            gvListadoProductoSeleccionado.RefreshData();
                        }
                    }
                    break;
                case ListProductosType.Remover:
                    Producto objNew = gvListadoProductoSeleccionado.GetFocusedRow() as Producto;
                    if (objNew != null)
                    {
                        objNew.num_cantidad *= -1;

                        OldListProductos.Add(objNew);
                        NewListProductos.Remove(objNew);
                        gvListadoProductos.RefreshData();
                        gvListadoProductoSeleccionado.RefreshData();
                    }
                    break;
            }
        }

        private void configurar_formulario()
        {
            this.unitOfWork.Globales.ConfigurarGridView_ClasicStyle(gcListadoProductos, gvListadoProductos, editable: true);
            this.unitOfWork.Globales.ConfigurarGridView_ClasicStyle(gcListadoProductoSeleccionado, gvListadoProductoSeleccionado, editable: true);
        }
        private void GetListadoProductos()
        {
            gcListadoProductos.DataSource = OldListProductos;
            gcListadoProductoSeleccionado.DataSource = NewListProductos;
        }

        private class Producto
        {
            public string cod_tipo_servicio { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string cod_subtipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public string cod_producto { get; set; }
            public string dsc_producto { get; set; }
            public string cod_unidad_medida { get; set; }
            public string dsc_unidad_medida { get; set; }
            public decimal imp_costo_actual { get; set; }
            public string cod_proveedor { get; set; }
            public string dsc_proveedor { get; set; }
            public decimal ctd_stock_actual { get; set; }
            public string flg_activo { get; set; }
            public decimal ctd_stock_minimo { get; set; }
            public decimal num_cantidad { get; set; }
        }

        private void btnAgregar_Click(object sender, System.EventArgs e)
        {
            ActualizarItems(ListProductosType.Agregar);
        }

        private void btn_Retirar_Click(object sender, System.EventArgs e)
        {
            ActualizarItems(ListProductosType.Remover);
        }

        private void gvListadoProductos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    //if (!(gvListadoProductos.GetRow(e.RowHandle) is Producto obj)) return;
                    //e.DefaultDraw();
                    //if (e.Column.FieldName.Equals("ctd_stock_minimo"))
                    //{
                    //    var valorAReponer = (obj.ctd_stock_actual - obj.ctd_stock_minimo);
                    //    obj.num_cantidad = valorAReponer < 0 ? valorAReponer : 0;
                    //}
                    //if (e.Column.FieldName.Equals("num_cantidad"))
              //      {
                        //var valorAReponer = (obj.ctd_stock_actual - obj.ctd_stock_minimo);
                        //obj.num_cantidad = valorAReponer < 0 ? valorAReponer : 0;
                        //gvListadoProductos.RefreshData();

                        //e.Column.AppearanceCell.BackColor = System.Drawing.Color.Transparent; e.Column.AppearanceCell.ForeColor = System.Drawing.Color.Black;
                        //if (obj.num_cantidad < 0)
                        //{
                        //    e.Column.AppearanceCell.BackColor = System.Drawing.Color.Crimson;
                        //    e.Column.AppearanceCell.ForeColor = System.Drawing.Color.White;
                        //}
               //     }
                }
            }
            catch (Exception ex) { HNG.MessageError(ex.ToString(), ""); }
        }

        private void ucListadoProductoLowStock_Load(object sender, EventArgs e)
        {
            //Handler.btnEnviarRequerimientos.ItemClick += btnEnviarRequerimientos_ItemClick;
        }

        private void btnEnviarRequerimientos_Click(object sender, EventArgs e)
        {
            EnviarRequerimientos();
        }

        private void btnSeleccionarTodos_Click(object sender, EventArgs e)
        {
            SeleccionarTodos();
        }
        void SeleccionarTodos()
        {
            ////OldListProductos.OrderByDescending((or) => or.cod_proveedor).ToList();
            //OldListProductos.Where((wh) => wh.cod_proveedor != null);
            //gvListadoProductos.RefreshData();
            gvListadoProductos.SelectAll();
            foreach (var row in gvListadoProductos.GetSelectedRows())
            //foreach (var objOld in OldListProductos.Where((wh) => wh.cod_proveedor != null))
            {
                Producto objOld = gvListadoProductos.GetRow(row) as Producto;
                if (objOld != null)
                {
                    //if (objOld.cod_proveedor == null) { continue; }

                    if (objOld.num_cantidad < 0)
                    {
                        if (objOld.cod_proveedor == null)// Si no tiene proveedor, no debe permitir la siguiente acción
                        {
                            //HNG.MessageWarning("Este producto no tiene proveedor asignado.", "Productos sin Proveedor");
                            continue;
                        }

                        objOld.num_cantidad *= -1;

                        NewListProductos.Add(objOld);
                        OldListProductos.Remove(objOld);
                        gvListadoProductos.RefreshData();
                        gvListadoProductoSeleccionado.RefreshData();
                    }

                }
            }
        }
    }
}
