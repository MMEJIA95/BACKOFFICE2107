using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BE_BackOffice;
using DevExpress.XtraSplashScreen;
using DevExpress.Images;
using DevExpress.XtraNavBar;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.Utils.Helpers;
using DevExpress.Utils.Extensions;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmListadoProductoPrecios : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        internal List<eProductos> ListProductos = new List<eProductos>();
        List<eProductos.eProductosTarifas> listHistoricoTarifas = new List<eProductos.eProductosTarifas>();
        eVentana oPerfil = new eVentana();

        bool Buscar = false;

        string select_cod_empresa;
        private string mostrar = "ULTIMO";
        private string activo = "SI";
              
        public frmListadoProductoPrecios()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadoPreciosProducto_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarOpcionesMenu();
            CargarListado("TODOS", "");
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            lblTitulo.Text = navBarControl1.SelectedLink.Group.Caption + ": " + navBarControl1.SelectedLink.Item.Caption;
            picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 0;
            Buscar = true;
            HabilitarBotones();
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.Sesion.Global.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
            }
            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
            oPerfil = listPerfil.Find(x => x.cod_perfil == 35 || x.cod_perfil == 37 || x.cod_perfil == 5);
        }

        internal void CargarOpcionesMenu()
        {
            List<eProveedor_Empresas> ListProvEmp = unit.Proveedores.ListarOpcionesMenu<eProveedor_Empresas>(12);
            Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
            Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

            NavBarGroup NavEmpresa = navBarControl1.Groups.Add();
            NavEmpresa.Name = "Por Empresa";
            NavEmpresa.Caption = "Por Empresa"; NavEmpresa.Expanded = true; NavEmpresa.SelectedLinkIndex = 0;
            NavEmpresa.GroupCaptionUseImage = NavBarImage.Large; NavEmpresa.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavEmpresa.ImageOptions.LargeImage = imgEmpresaLarge; NavEmpresa.ImageOptions.SmallImage = imgEmpresaSmall;

            foreach (eProveedor_Empresas obj in ListProvEmp)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_empresa; NavDetalle.Name = obj.cod_empresa;
                NavDetalle.Caption = obj.dsc_empresa; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavEmpresa.ItemLinks.Add(NavDetalle);
            }
        }

        private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            lblTitulo.Text = e.Link.Group.Caption + ": " + e.Link.Caption; picTitulo.Image = e.Link.Group.ImageOptions.LargeImage;
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            select_cod_empresa = e.Link.Item.Tag.ToString();
            CargarListado(e.Link.Group.Caption, e.Link.Item.Tag.ToString());
            SplashScreenManager.CloseForm();
        }

        public void CargarListado(string NombreGrupo, string Codigo)
        {
            try
            {
                string cod_empresa = ""; Codigo = Codigo == "ALL" ? "" : Codigo;
                switch (NombreGrupo)
                {
                    case "Por Empresa": cod_empresa = Codigo; break;
                }

                CargarProductos(cod_empresa);

                CargarPrecios(cod_empresa);

                //CargarSinStock(cod_empresa);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarSinStock(string cod_empresa)
        {
            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");

           
            if (string.IsNullOrEmpty(cod_empresa)) { return; }

            //AbrirTabProductoBajoStock();
            //SplashScreenManager.CloseForm();
        }

        private void CargarProductos(string cod_empresa)
        {
            cod_empresa = string.IsNullOrEmpty(cod_empresa) || cod_empresa.Equals("ALL") ? (cod_empresa.Equals("ALL") ? "" : cod_empresa) : cod_empresa;

            ListProductos.Clear();
            ListProductos = unit.Logistica.Obtener_ListadosProductos<eProductos>(11, cod_empresa: cod_empresa, flg_activo: activo);
            bsListadoProductos.DataSource = ListProductos;
            gvListadoProductos.RefreshData();
        }
        private void CargarPrecios(string cod_empresa)
        {
            cod_empresa = string.IsNullOrEmpty(cod_empresa) ? "" :
                (cod_empresa.Equals("ALL") ? "" : cod_empresa);

            listHistoricoTarifas.Clear();
            listHistoricoTarifas = unit.Logistica.Obtener_ListadosProductos<eProductos.eProductosTarifas>(
                9, cod_empresa: cod_empresa, dsc_mostrar: mostrar);
            bsListadoProductosTarifa.DataSource = listHistoricoTarifas;
            gvListadoProductosTarifa.RefreshData();
        }

        private void navBarControl1_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            e.Group.SelectedLinkIndex = 0;
            navBarControl1_SelectedLinkChanged(navBarControl1, new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs(e.Group, e.Group.SelectedLink));
        }
        void ActiveGroupChanged(string caption, Image imagen)
        {
            lblTitulo.Text = caption; picTitulo.Image = imagen;
        }

        private void navBarControl1_SelectedLinkChanged(object sender, DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs e)
        {
            //e.Group.SelectedLinkIndex = 0;
            if (!Buscar) e.Group.SelectedLinkIndex = 0;
            if (e.Group.SelectedLink != null && Buscar)
            {
                ActiveGroupChanged(e.Group.Caption + ": " + e.Group.SelectedLink.Item.Caption, e.Group.ImageOptions.LargeImage);
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                select_cod_empresa = e.Link.Item.Tag.ToString();
                CargarListado(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
            if (navGrupo.SelectedLink.Item.Tag.ToString() == "ALL") { MessageBox.Show("Debe seleccionar una empresa", "Crear producto", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            frmMantProductos frm = new frmMantProductos();
            //frm.cod_empresa = navGrupo.SelectedLink.Item.Tag.ToString() == "ALL" ? "" : navGrupo.SelectedLink.Item.Tag.ToString();
            frm.cod_empresa = navGrupo.SelectedLink.Item.Tag.ToString();
            frm.ShowDialog();
            if (frm.ActualizarListado)
            {
                if (e.Link.Item.Tag != null)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                    select_cod_empresa = e.Link.Item.Tag.ToString();
                    CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                    SplashScreenManager.CloseForm();
                }
            }
        }

        private void gvListadoProductosTarifa_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductosTarifa_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProductosTarifa_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eProductos.eProductosTarifas obj = gvListadoProductosTarifa.GetFocusedRow() as eProductos.eProductosTarifas;

                    frmMantProductoPrecio frm = new frmMantProductoPrecio(this);
                    frm.MiAccion = TarifaProducto.Editar;
                    frm.cod_tipo_servicio = obj.cod_tipo_servicio;
                    frm.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                    frm.cod_producto = obj.cod_producto;
                    frm.dsc_ruc = obj.dsc_ruc;
                    frm.cod_proveedor = obj.cod_proveedor;
                    frm.dsc_proveedor = obj.dsc_proveedor;
                    frm.dsc_producto = obj.dsc_producto;
                    frm.fch_inicio = obj.fch_inicio;
                    frm.imp_costo = obj.imp_costo;
                    frm.ShowDialog();
                    if (frm.ActualizarListado)
                    {
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                        NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                        select_cod_empresa = navGrupo.SelectedLink.Item.Tag.ToString();
                        CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmListadoProductoPrecios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                select_cod_empresa = navGrupo.SelectedLink.Item.Tag.ToString();
                CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }

        private void btnClonar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eProductos obj = gvListadoProductos.GetFocusedRow() as eProductos;
                NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;

                frmMantProductos frm = new frmMantProductos(this);
                frm.MiAccion = Producto.Clonar;
                frm.cod_tipo_servicio = obj.cod_tipo_servicio;
                frm.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                frm.cod_producto = obj.cod_producto;
                //frm.cod_productoREF = obj.cod_productoREF;
                frm.cod_empresa = navGrupo.SelectedLink.Item.Tag.ToString() == "ALL" ? "" : navGrupo.SelectedLink.Item.Tag.ToString();
                frm.ShowDialog();
                if (frm.ActualizarListado)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                    CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\ListaProductoPrecios" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";

                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListadoProductos.ExportToXlsx(archivo);
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

        private void btnActivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DialogResult msgresult = MessageBox.Show("¿Está seguro de activar el producto?", "Activar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    eProductos eProd = gvListadoProductos.GetFocusedRow() as eProductos;
                    string result = unit.Logistica.Activar_Inactivar_Producto(eProd, "SI");
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    int nRow = gvListadoProductos.FocusedRowHandle;
                    select_cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    CargarListado(navBarControl1.SelectedLink.Group.Caption, navBarControl1.SelectedLink.Item.Tag.ToString());
                    gvListadoProductos.FocusedRowHandle = nRow;
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult msgresult = MessageBox.Show("¿Está seguro de inactivar el producto?", "Inactivar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msgresult == DialogResult.Yes)
            {
                eProductos eProd = gvListadoProductos.GetFocusedRow() as eProductos;
                string result = unit.Logistica.Activar_Inactivar_Producto(eProd, "NO");
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                int nRow = gvListadoProductos.FocusedRowHandle;
                select_cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                CargarListado(navBarControl1.SelectedLink.Group.Caption, navBarControl1.SelectedLink.Item.Tag.ToString());
                gvListadoProductos.FocusedRowHandle = nRow;
                SplashScreenManager.CloseForm();
            }
        }

        private void gvListadoProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProductos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eProductos obj = gvListadoProductos.GetRow(e.RowHandle) as eProductos;
                    NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                    frmMantProductos frm = new frmMantProductos(this);
                    frm.MiAccion = oPerfil == null ? Producto.Vista : Producto.Editar;
                    frm.cod_tipo_servicio = obj.cod_tipo_servicio;
                    frm.cod_subtipo_servicio = obj.cod_subtipo_servicio;
                    string __codEmpresa = navGrupo.SelectedLink.Item.Tag.ToString() == "ALL" ? "" : navGrupo.SelectedLink.Item.Tag.ToString();
                    frm.cod_producto = obj.cod_producto;
                    //frm.cod_productoREF = obj.cod_productoREF;
                    frm.cod_empresa = __codEmpresa;
                    frm.CargarEmpresaAsignadoAProducto(obj.cod_producto, __codEmpresa);// Cargar las empresas de acuerdo al codigo de producto
                    frm.ShowDialog();
                    if (frm.ActualizarListado)
                    {
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
                        select_cod_empresa = navGrupo.SelectedLink.Item.Tag.ToString();
                        CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkPrecioUltimo_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            chkPrecioTodos.Checked = !chkPrecioUltimo.Checked;
            mostrar = chkPrecioUltimo.Checked ? "ULTIMO" : "TODO";
        }

        private void chkPrecioTodos_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            chkPrecioUltimo.Checked = !chkPrecioTodos.Checked;
            mostrar = chkPrecioUltimo.Checked ? "ULTIMO" : "TODO";
        }


        private void chkPrecioTodos_ItemClick(object sender, ItemClickEventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
            CargarPrecios(navBarControl1.SelectedLink.Item.Tag.ToString());
            SplashScreenManager.CloseForm();
        }

        private void chkPrecioUltimo_ItemClick(object sender, ItemClickEventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
            CargarPrecios(navBarControl1.SelectedLink.Item.Tag.ToString());
            SplashScreenManager.CloseForm();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            chkPrecioTodos.Visibility = BarItemVisibility.Never;
            chkPrecioUltimo.Visibility = BarItemVisibility.Never;
            chkActivo.Visibility = BarItemVisibility.Never;
            chkInactivo.Visibility = BarItemVisibility.Never;
            switch (e.Page.Name)
            {
                case "xtabProductos":
                    chkActivo.Visibility = BarItemVisibility.Always;
                    chkInactivo.Visibility = BarItemVisibility.Always;
                    btnClonar.Enabled = false;
                    btnEnviarRequerimientos.Enabled = false;
                    break;
                case "xtabTarifas":
                    chkPrecioTodos.Visibility = BarItemVisibility.Always;
                    chkPrecioUltimo.Visibility = BarItemVisibility.Always;
                    btnEnviarRequerimientos.Enabled = false;
                    break;
                case "xtraSinStock":
                    {
                        btnEnviarRequerimientos.Enabled = true;
                        btnClonar.Enabled = false;
                        //chkPrecioTodos.Visibility = BarItemVisibility.Always;
                        //chkPrecioUltimo.Visibility = BarItemVisibility.Always;

                        //AbrirTabProductoBajoStock();
                        break;
                    }
            }
        }
        //private void AbrirTabProductoBajoStock()
        //{
        //    //Se ha retirado de este módulo...
        //    if (!string.IsNullOrEmpty(select_cod_empresa)) { HNG.OpenUCInPanel(new ucListadoProductoLowStock(select_cod_empresa, this, unit, ListProductos), pnlProductoBajoStock); }
        //    else { xtraTabControl1.SelectedTabPage = xtabProductos; HNG.MessageWarning("Es necesario seleccionar una empresa, vuelve a intentarlo.", "BAJOS EN STOCK"); }
        //}

        private void chkActivo_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            chkInactivo.Checked = !chkActivo.Checked;
            activo = chkActivo.Checked ? "SI" : "NO";
        }

        private void chkInactivo_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            chkActivo.Checked = !chkInactivo.Checked;
            activo = chkActivo.Checked ? "SI" : "NO";
        }

        private void chkActivo_ItemClick(object sender, ItemClickEventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
            CargarProductos(navBarControl1.SelectedLink.Item.Tag.ToString());
            SplashScreenManager.CloseForm();
        }

        private void chkInactivo_ItemClick(object sender, ItemClickEventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");
            CargarProductos(navBarControl1.SelectedLink.Item.Tag.ToString());
            SplashScreenManager.CloseForm();
        }


    }
}