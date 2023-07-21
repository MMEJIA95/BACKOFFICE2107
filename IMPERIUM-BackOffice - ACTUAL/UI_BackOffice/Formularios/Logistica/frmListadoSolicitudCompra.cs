using BE_BackOffice;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraNavBar;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UI_BackOffice.Formularios.Personal;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmListadoSolicitudCompra : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        private string cod_empresa, cod_sede_empresa;
        private readonly Brush GREEN = Brushes.MediumSeaGreen;
        private readonly Brush RED = Brushes.Crimson;
        private readonly Brush ORANGE = Brushes.Gold; int markWidth = 16;
        private string filtrarPorSemaforo = "Azul"; //Verde: 30+ Items, Amarillo: 1-29 Items, Rojo: -0 Items y Azul: Todos los Items.
        public frmListadoSolicitudCompra()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            cod_empresa = cod_sede_empresa = string.Empty;
            configurar_formulario();

        }
        private void configurar_formulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoSolicitudes_Vista, gvListadoSolicitudes_Vista);
            //gv.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            // gvListadoSolicitudes_Vista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            btnBuscador.Appearance.BackColor = Program.Sesion.Colores.Verde;
        }
        private void InicializarDatos()
        {
            CargarEmpresaSede_Tree();
            btnBuscador_Click(btnBuscador, new EventArgs());
        }
        private class FiltoEmpresaSede { public string cod_empresa { get; set; } public string dsc_empresa { get; set; } public string cod_sede { get; set; } public string dsc_sede { get; set; } }
        private class EmpresaSede { public string cod_empresa { get; set; } public string cod_sede_empresa { get; set; } public string dsc_sede_empresa { get; set; } }
        private void CargarEmpresaSede_Tree()
        {
            var treeList = new List<FiltoEmpresaSede>();
            //List<eEmpresa> empresaList = unit.Clientes.ListarOpcionesMenu<eEmpresa>(36);
            //foreach (eEmpresa empresa in empresaList)
            List<eProveedor_Empresas> empresaList = Program.Sesion.EmpresaList;
            foreach (eProveedor_Empresas empresa in empresaList)
            {
                List<EmpresaSede> sedeList = unit.Clientes.ListarOpcionesMenu<EmpresaSede>(opcion: 45, cod_empresa: empresa.cod_empresa);
                foreach (EmpresaSede sede in sedeList)
                {
                    treeList.Add(new FiltoEmpresaSede()
                    {
                        cod_empresa = sede.cod_empresa,
                        dsc_empresa = empresa.dsc_empresa,
                        cod_sede = sede.cod_sede_empresa,
                        dsc_sede = sede.dsc_sede_empresa
                    });
                }
            }

            if (treeList != null && treeList.Count > 0)
            {
                var tree = new Tools.TreeListHelper(treeEmpresas);
                tree.TreeViewParaDosNodos<FiltoEmpresaSede>(treeList,
                     ColumnaCod_Padre: "cod_empresa",
                     ColumnaDsc_Padre: "dsc_empresa",
                     ColumnaCod_Hijo: "cod_sede",
                     ColumnaDsc_Hijo: "dsc_sede"
                   );
                refreshTreeView();
            }
        }
        private void refreshTreeView()
        {
            treeEmpresas.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Radio;
            for (int i = 0; i < treeEmpresas.Nodes.Count; i++)
            {
                treeEmpresas.Nodes[i].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                for (int j = 0; j < treeEmpresas.Nodes[i].Nodes.Count(); j++)
                {
                    treeEmpresas.Nodes[i].Nodes[j].ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                }
            }
            treeEmpresas.CheckAll();
            //treeFiltroEmpresa.Nodes[0].Checked = true;
            //treeFiltroEmpresa.Nodes[0].Nodes[0].Checked = true;
            //treeFiltroEmpresa.Nodes[0].Nodes[0].Nodes.ToList().ForEach((ch) => ch.Checked = true);
            ////  treeFiltroVacaciones.Nodes[0].Nodes[0].Checked = true;
            //treeFiltroEmpresa.CollapseAll();
            treeEmpresas.Refresh();
            treeEmpresas.Nodes[0].ExpandAll();
            //treeFiltroEmpresa.Refresh();

            //trlEmpresaSede.ExpandAll();


        }

        //private void CargarSolicitudCompra_Vista()
        //{
        //    /*------*Obtener código seleccionado del TreeView*------*/
        //    var t = new Tools.TreeListHelper(treeEmpresas);
        //    var multiple_empresa = t.ObtenerCodigoConcatenadoDeNodoIndex(0);
        //    var multiple_sede = t.ObtenerCodigoConcatenadoDeNodoIndex(1);

        //    cod_empresa = multiple_empresa;
        //    cod_sede_empresa = multiple_sede;
        //    var objList = unit.SolicitudCompra.ConsultaVarias<eSolicitudCompra_Vista>(new PQSolicitudCompra()
        //    {
        //        Opcion = 1,
        //        Cod_empresa = multiple_empresa,
        //        Cod_sede_empresa = multiple_sede,
        //        Cod_almacen = ""//enviar códigos de almacén
        //    });//.Where((f) => f.dsc_diferencia);
        //    var newList = new List<eSolicitudCompra_Vista>();
        //    switch (filtrarPorSemaforo)
        //    {
        //        case "Verde":
        //            {
        //                newList = objList.Where((f) => f.dsc_diferencia >= 30).ToList();
        //                break;
        //            }
        //        case "Amarillo":
        //            {
        //                newList = objList.Where((f) => f.dsc_diferencia >= 0 && f.dsc_diferencia < 30).ToList();
        //                break;
        //            }
        //        case "Rojo":
        //            {
        //                newList = objList.Where((f) => f.dsc_diferencia < 0).ToList();
        //                break;
        //            }
        //        case "Azul":
        //            {
        //                newList = objList.ToList();
        //                break;
        //            }
        //    }
        //    var nombre_Empresa = t.ObtenerCodigoConcatenadoDeNodoIndex(0, "Descripcion");
        //    var nombre_sede = t.ObtenerCodigoConcatenadoDeNodoIndex(1, "Descripcion");
        //    lblTitulo.Text = $"Por Empresa: {nombre_Empresa} - {nombre_sede}";
        //    bsListadoSolicitudes_Vista.DataSource = null;
        //    if (newList == null || newList.Count == 0) return;
        //    bsListadoSolicitudes_Vista.DataSource = newList.ToList();
        //    gvListadoSolicitudes_Vista.RefreshData();

        //    lblAviso.Visibility = newList.ToList().Count() > 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        //}

        private void CargarSolicitudCompra_Vista()
        {
            var t = new Tools.TreeListHelper(treeEmpresas);

            cod_empresa = t.ObtenerCodigoConcatenadoDeNodoIndex(0);
            cod_sede_empresa = t.ObtenerCodigoConcatenadoDeNodoIndex(1);

            var solicitudCompraQuery = new PQSolicitudCompra
            {
                Opcion = 1,
                Cod_empresa = cod_empresa,
                Cod_sede_empresa = cod_sede_empresa,
                Cod_almacen = "" // enviar códigos de almacén
            };

            var objList = unit.SolicitudCompra.ConsultaVarias<eSolicitudCompra_Vista>(solicitudCompraQuery);
            var newList = FiltrarPorSemaforo(objList);

            var nombre_Empresa = t.ObtenerCodigoConcatenadoDeNodoIndex(0, "Descripcion");
            var nombre_sede = t.ObtenerCodigoConcatenadoDeNodoIndex(1, "Descripcion");
            lblTitulo.Text = $"Por Empresa: {nombre_Empresa} - {nombre_sede}";

            bsListadoSolicitudes_Vista.DataSource = newList;
            gvListadoSolicitudes_Vista.RefreshData();

            lblAviso.Visibility = newList.Any() ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        private List<eSolicitudCompra_Vista> FiltrarPorSemaforo(List<eSolicitudCompra_Vista> objList)
        {
            switch (filtrarPorSemaforo)
            {
                case "Verde":
                    return objList.Where(f => f.dsc_diferencia >= 30).ToList();
                case "Amarillo":
                    return objList.Where(f => f.dsc_diferencia > 0 && f.dsc_diferencia < 30).ToList();
                case "Rojo":
                    return objList.Where(f => f.dsc_diferencia <= 0).ToList();
                case "Azul":
                default:
                    return objList.ToList();
            }
        }



        private void CargarParaSalidaDeInventario()
        {
            if (!(gvListadoSolicitudes_Vista.GetFocusedRow() is eSolicitudCompra_Vista obj)) return;
            string[] product = new string[gvListadoSolicitudes_Vista.GetSelectedRows().Count()]; int i = -1;
            foreach (var nRow in gvListadoSolicitudes_Vista.GetSelectedRows())
            {
                i++;
                var oj = gvListadoSolicitudes_Vista.GetRow(nRow) as eSolicitudCompra_Vista;
                product[i] = oj.cod_producto;
            }
            if (string.IsNullOrWhiteSpace(String.Join(",", product))) return;

            frmRegistrarSalidaAlmacen frm = new frmRegistrarSalidaAlmacen();
            frm.cod_empresa = cod_empresa;
            frm.cod_sede_empresa = cod_sede_empresa.Split(',')[0];  // obj.cod_se  //lkpSedeEmpresa.EditValue.ToString();
            frm.cod_almacen = "";   //lkpAlmacen.EditValue.ToString();
            frm.cod_requerimiento = obj == null ? "" : "";  //obj.cod_requerimiento;
            frm.flg_solicitud = obj == null ? "" : "";  // obj.flg_solicitud;
            frm.dsc_anho = obj == null ? "" : "";   // obj.ToString;//obj.dsc_anho.ToString();
            frm.ShowDialog();
            if (frm.ActualizarListado)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                btnBuscador_Click(btnBuscador, new EventArgs());
                SplashScreenManager.CloseForm();
            }



            //MessageBox.Show(String.Join(",", product));
            //if (Application.OpenForms["frmMantTrabajador"] != null)
            //{
            //    Application.OpenForms["frmMantTrabajador"].Activate();
            //}
            //else
            //{

            //    frmListadoSolicitudCompra_PrevioOrdenCompra frm =
            //        new frmListadoSolicitudCompra_PrevioOrdenCompra(this) { Text = "Generar Orden de Compra" };
            //    //frm.MiAccion = Trabajador.Editar;

            //    frm.CargarRequerimientos_PrevioOC
            //        (cod_producto: String.Join(",", product), cod_empresa: cod_empresa);
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK) { btnBuscador_Click(btnBuscador, new EventArgs()); }
            //}





        }
        private void frmListadoSolicitudCompra_Load(object sender, EventArgs e)
        {
            InicializarDatos();
        }

        private void CorregirNavFiltro()
        {

            //if (navBarControl1.OptionsNavPane.NavPaneState== NavPaneState.Expanded)
            //{
            //    navBarControl1.OptionsNavPane.ExpandedWidth = 260;
            //    navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            //    //navBarControl1.Width = 266;
            //    //layoutFiltros.MinSize = new Size(266,1);
            //}
            //else
            //{
            //    //layoutFiltros.MinSize = new Size(1, 1);
            //    navBarControl1.OptionsNavPane.ExpandedWidth = 260;
            //    navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            //}
            refreshTreeView();
        }

        internal void btnBuscador_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(navBarControl1.Width.ToString() +"    "+navBarControl1.OptionsNavPane.CollapsedWidth.ToString(), navBarControl1.OptionsNavPane.ExpandedWidth.ToString());
            //navBarControl1.OptionsNavPane.ExpandedWidth = 260;
            //MessageBox.Show(navBarControl1.Width.ToString());
            CargarSolicitudCompra_Vista();
            CorregirNavFiltro();
            //navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;

        }

        private string ObtenerCodigosProductos()
        {
            string[] product = new string[gvListadoSolicitudes_Vista.GetSelectedRows().Count()]; int i = -1;
            foreach (var nRow in gvListadoSolicitudes_Vista.GetSelectedRows())
            {
                i++;
                var obj = gvListadoSolicitudes_Vista.GetRow(nRow) as eSolicitudCompra_Vista;
                product[i] = obj.cod_producto;
            }
            return String.Join(",", product);
        }
        private void MostrarRequerimientos()
        {
            if (!(gvListadoSolicitudes_Vista.GetFocusedRow() is eSolicitudCompra_Vista _)) return;
            string cod_productos = ObtenerCodigosProductos();

            if (string.IsNullOrWhiteSpace(cod_productos)) return;

            //MessageBox.Show(String.Join(",", product));
            frmListadoSolicitudCompra_VistaRequerimiento frm =
                    new frmListadoSolicitudCompra_VistaRequerimiento() { Text = "Productos en Requerimientos" };
            //frm.MiAccion = Trabajador.Editar;
            frm.CargarRequerimientos_ProProductos
                (cod_producto: cod_productos, cod_empresa: cod_empresa);
            frm.ShowDialog();
        }

        #region Espacio para generar Solicitud de compra
        /*-----*Artificio para obtener la cantidad de productos en un requerimiento*-----*/
        private class RQ { public int Cantidad { get; set; } }// analizar

        private List<eSolicitudCompra_Requerimientos> ObtenerRequerimientosPorProductos(string cod_producto)
        {
            var objList = unit.SolicitudCompra.ConsultaVarias<eSolicitudCompra_Requerimientos>(
               new PQSolicitudCompra() { Opcion = 2, Cod_producto = cod_producto, Cod_empresa = cod_empresa });
            return objList;
        }
        private void GenerarSolicitudDeCompra()
        {
            //return;
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
            Int32 anho = 1;
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
                        empresa = cod_empresa;// obj.cod_empresa;
                        sede = obj.cod_sede_empresa;
                        solicitud = "COMPRA";// obj.flg_solicitud;
                        anho = obj.dsc_anho;
                        requerimientos = obj.cod_requerimiento + "," + requerimientos;
                        productos = obj.cod_producto + "," + productos;
                    }
                }

                // LIMPIAR VARIABLES requerimientos y productos;
                var requerimientosLista = requerimientos.Split(',')
                                               .Select(x => x.Trim())
                                               .ToList();
                var requerimientosUnicos = requerimientosLista.Distinct().ToList();
                string requerimientosLimpios = string.Join(",", requerimientosUnicos);

                var productosLista = productos.Split(',')
                                               .Select(x => x.Trim())
                                               .ToList();
                var productosUnicos = productosLista.Distinct().ToList();
                string productosLimpios = string.Join(",", productosUnicos);

                string cod_orden_compra = "", flg_solicitud = "";
                Int32 dsc_anho = 1;

                //TRAE DATOS DEL PROVEEDOR Y PRODUCTOS DE LOS RQ SELECCIONADOS
                List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> prodProvReq;
                prodProvReq = unit.Requerimiento.Cargar_Prod_Prov_Requerimientos<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(12, empresa, sede, requerimientosLimpios, solicitud, anho, productosLimpios);

                //SE COMIENZA A REVISAR LA OC
                for (int i = 0; i < prodProvReq.Count; i++)
                {
                    //(REVISAR)
                    if (i == 0)
                    {


                        //INSERTA CABECERA DE LA 1RA OC
                        eOrdenCompra_Servicio eOrdCom = helper.CreaCabecera(prodProvReq[i], "C");

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
                            eOrdenCompra_Servicio eOrdCom = helper.CreaCabecera(prodProvReq[i], "C");

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
                        respuesta = unit.Requerimiento.GenerarOC_Requerimiento(empresa, sede, requerimientos, Program.Sesion.Usuario.cod_usuario, solicitud, anho);
                }

                SplashScreenManager.CloseForm();


                if (respuesta.Contains("OK"))
                {
                    HNG.MessageSuccess("Órdenes generadas con éxito", "INFORMACION");
                }
                /*-----*Refrescar GridView*-----*/
                btnBuscador_Click(btnBuscador, new EventArgs());

                AbrirSolicitudDeCompra_Generada(cod_orden_compra_servicio: cod_orden_compra, flg_solicitud: "C", anho: anho); //solicitud:C
            }
            catch (Exception)
            {
                SplashScreenManager.CloseForm();
                HNG.MessageError("Error al Crear Órdenes.", "ERROR");
                //MessageBox.Show("Error al Crear Órdenes.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirSolicitudDeCompra_Generada(string cod_orden_compra_servicio, string flg_solicitud, int anho)
        {
            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 31 || x.cod_perfil == 5 || x.cod_perfil == 29);

            frmMantOrdenCompra frm = new frmMantOrdenCompra();
            frm.accion = oPerfilLog != null ? OrdenCompra.Editar : OrdenCompra.Vista;
            frm.empresa = cod_empresa;
            frm.sede = cod_sede_empresa.Split(',')[0];// obj.cod_sede_empresa;
            frm.ordenCompraServicio = cod_orden_compra_servicio;// obj.cod_orden_compra_servicio;
            frm.solicitud = flg_solicitud;
            frm.anho = anho;
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();
        }
        #endregion Espacio para generar Solicitud de compra
        private void gvListadoSolicitudes_Vista_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //try { if (e.Clicks == 2 && e.RowHandle >= 0) { MostrarRequerimientos(); } }
            //catch (Exception ex) { MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }


        private void frmListadoSolicitudCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) { btnBuscador_Click(sender, new EventArgs()); }
        }

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
                    if (e.Column.FieldName.Equals("ctd_stock_minimo")) { e.Column.AppearanceCell.BackColor = Color.FromArgb(220, 220, 220); }
                    if (e.Column.FieldName.Equals("ctd_stock_reposicion")) { e.Column.AppearanceCell.BackColor = Color.FromArgb(180, 173, 225); }
                    if (e.Column.FieldName.Equals("imp_unitario")) { e.Column.AppearanceCell.BackColor = Color.White; }
                    if (e.Column.FieldName.Equals("imp_total")) { e.Column.AppearanceCell.BackColor = Color.FromArgb(220, 220, 220); }
                }
            }
            catch (Exception ex) { HNG.MessageError(ex.ToString(), ""); }//MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void navBarControl1_GetHint(object sender, DevExpress.XtraNavBar.NavBarGetHintEventArgs e)
        {
            refreshTreeView(); CorregirNavFiltro();
            //MessageBox.Show("d");
        }

        private void btnSeleccionMultiple_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (btnSeleccionMultiple.Down)
            {
                gvListadoSolicitudes_Vista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                btnSeleccionMultiple.Caption = "Selección Individual";
                btnSeleccionMultiple.ImageOptions.LargeImage = Properties.Resources.checkbox2_32x32;
                //gvListadoSolicitudes_Vista.OptionsView.ShowIndicator = true;
            }
            else
            {
                gvListadoSolicitudes_Vista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                btnSeleccionMultiple.Caption = "Selección Múltiple";
                btnSeleccionMultiple.ImageOptions.LargeImage = Properties.Resources.checkbuttons_32x32;
                //gvListadoSolicitudes_Vista.OptionsView.ShowIndicator = false;
            }
        }

        private void btnVistaFiltro_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*  if (btnVistaFiltro.Down)
              {
                  layoutFiltros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                  btnVistaFiltro.Caption = "Mostrar Filtros";
                  btnVistaFiltro.ImageOptions.LargeImage = Properties.Resources.viewonweb_32x32;
                  //
                  navBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
                  navBarControl1.OptionsNavPane.ExpandedWidth = 260;
              }
              else
              {
                  layoutFiltros.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                  btnVistaFiltro.Caption = "Ocultar Filtros";
                  btnVistaFiltro.ImageOptions.LargeImage = Properties.Resources.hide_32x32;
                  navBarControl1.OptionsNavPane.ExpandedWidth = 260;
              }*/
        }

        private void btnExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
            string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\RequerimientosOC" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            gvListadoSolicitudes_Vista.ExportToXlsx(archivo);
            if (HNG.MessageQuestion("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel") == DialogResult.Yes)
            // if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { Process.Start(archivo); }
        }

        private void btnExportPDF_ItemClick(object sender, ItemClickEventArgs e)
        {
            string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
            string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\RequerimientosOC" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".pdf";
            if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            gvListadoSolicitudes_Vista.ExportToPdf(archivo);
            if (HNG.MessageQuestion("PDF exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "PDF") == DialogResult.Yes)
            //if (MessageBox.Show("PDF exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "PDF", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { Process.Start(archivo); }
        }
        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListadoSolicitudes_Vista.ShowPrintPreview();
        }
        private void btnGenerarOrdenCompra_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(gvListadoSolicitudes_Vista.GetFocusedRow() is eSolicitudCompra_Vista obj)) return;
            if (HNG.MessageQuestion("Se va a realizar una solicitud de compra de los productos que se han solicitado.\n¿Desea continuar?", "Solicitud de Compra") == DialogResult.Yes)
                //if (MessageBox.Show("Se va a realizar una solicitud de compra.\n¿Desea continuar?", "SOLICITUD DE COMPRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                GenerarSolicitudDeCompra();
        }

        private void btnGenerarSalidaInventario_ItemClick(object sender, ItemClickEventArgs e)
        {
            CargarParaSalidaDeInventario();
        }

        private void bbiVerde_ItemClick(object sender, ItemClickEventArgs e)
        {
            filtrarPorSemaforo = "Verde";
            btnBuscador_Click(btnBuscador, new EventArgs());
        }

        private void bbiAmarillo_ItemClick(object sender, ItemClickEventArgs e)
        {
            filtrarPorSemaforo = "Amarillo";
            btnBuscador_Click(btnBuscador, new EventArgs());
        }

        private void bbiRojo_ItemClick(object sender, ItemClickEventArgs e)
        {
            filtrarPorSemaforo = "Rojo";
            btnBuscador_Click(btnBuscador, new EventArgs());
        }

        private void bbiAzul_ItemClick(object sender, ItemClickEventArgs e)
        {
            filtrarPorSemaforo = "Azul";
            btnBuscador_Click(btnBuscador, new EventArgs());
        }



        private class Rpt
        {
            public string Requerimiento { get; set; }
            public string CodProducto { get; set; }
            public string NombreProducto { get; set; }
            public int Año { get; set; }
            public decimal CantidadPedida { get; set; }
            public decimal PendienteEntrega { get; set; }
            public string CECO { get; set; }
            public string Solicitante { get; set; }
        }
        private void btnExportarProductos_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(gvListadoSolicitudes_Vista.GetFocusedRow() is eSolicitudCompra_Vista _)) return;

            //string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
            //string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\RequerimientosOC" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
            //if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);

            var objList = unit.SolicitudCompra.ConsultaVarias<eSolicitudCompra_Requerimientos>(
               new PQSolicitudCompra() { Opcion = 2, Cod_producto = ObtenerCodigosProductos(), Cod_empresa = cod_empresa });

            var newList = new List<Rpt>();
            objList.ForEach((f) =>
            {
                newList.Add(new Rpt()
                {
                    Requerimiento = f.cod_requerimiento,
                    CodProducto = f.cod_producto,
                    NombreProducto = f.dsc_producto,
                    Año = f.dsc_anho,
                    CantidadPedida = f.num_cantidad,
                    PendienteEntrega = f.num_restante,
                    CECO = $"{f.cod_CECO} - {f.dsc_CECO}",
                    Solicitante = f.dsc_nombre_solicitante
                });
            });
            //BindingSource bs = new BindingSource();
            //bs.DataSource = objList;

            //GridControl grid = new GridControl();
            //grid.DataSource = bs.DataSource;



            //grid.GridDisposing  // "\\RequerimientosOC" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "")

            //grid.ExportToXlsx(archivo);

            //   new Exportar().GenerateExcel<Rpt>(newList, $"RequerimientosOC{DateTime.Now.ToString().Replace("/", "-").Replace(":", "")}");

            HNG.Excel.GenerateExcel_fromList<Rpt>(newList, $"RequerimientosOC{DateTime.Now.ToString().Replace("/", "-").Replace(":", "")}");

            //if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{ Process.Start(archivo); }
        }

        private void btnGenerarOrdenCompraReponerStock_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(gvListadoSolicitudes_Vista.GetFocusedRow() is eSolicitudCompra_Vista obj)) return;
            if (HNG.MessageQuestion("Se va a realizar una solicitud de compra de productos para reponer el Stock Mínimo.\n¿Desea continuar?", "Solicitud de Compra") == DialogResult.Yes)
            //if (MessageBox.Show("Se va a realizar una solicitud de compra.\n¿Desea continuar?", "SOLICITUD DE COMPRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            //Antes de generar la solictud de compra, se debe generar los requerimientos... analizar...

            { }
            //GenerarSolicitudDeCompra();
        }

        private void btnVerReqVinculados_ItemClick(object sender, ItemClickEventArgs e)
        {
            MostrarRequerimientos();
        }


    }
}