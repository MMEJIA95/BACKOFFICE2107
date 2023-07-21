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
using BL_BackOffice;
using DevExpress.Images;
using DevExpress.XtraNavBar;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace UI_BackOffice.Formularios.Personal
{
    public partial class frmListadoTrabajador : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        List<eTrabajador> listTrabajador = new List<eTrabajador>();
        bool Buscar = false;
        Brush ConCriterios = Brushes.Green;
        Brush SinCriterios = Brushes.Red;
        Brush NAplCriterio = Brushes.Orange;
        int markWidth = 16;

        public frmListadoTrabajador()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadoTrabajador_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarOpcionesMenu();
            //CargarListado("TODOS", "");
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            lblTitulo.Text = navBarControl1.SelectedLink.Group.Caption + ": " + navBarControl1.SelectedLink.Item.Caption;
            picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 0;
            Buscar = true;
            navBarControl1.SelectedLink = navBarControl1.Groups[0].ItemLinks[0];
            NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
            CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
        }

        internal void CargarOpcionesMenu()
        {
            List<eProveedor_Empresas> listEmpresas = unit.Proveedores.ListarOpcionesMenu<eProveedor_Empresas>(12);
            Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
            Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

            NavBarGroup NavEmpresa = navBarControl1.Groups.Add();
            NavEmpresa.Name = "Por Empresa";
            NavEmpresa.Caption = "Por Empresa"; NavEmpresa.Expanded = true; NavEmpresa.SelectedLinkIndex = 0;
            NavEmpresa.GroupCaptionUseImage = NavBarImage.Large; NavEmpresa.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavEmpresa.ImageOptions.LargeImage = imgEmpresaLarge; NavEmpresa.ImageOptions.SmallImage = imgEmpresaSmall;

            List<eProveedor_Empresas> listEmpresasUsuario = unit.Proveedores.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", Program.Sesion.Usuario.cod_usuario);
            if (listEmpresasUsuario.Count == 0) { MessageBox.Show("Debe tener una empresa asignada para visualizar los datos", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            List<eProveedor_Empresas> listadoEmp = new List<eProveedor_Empresas>();
            eProveedor_Empresas objEmp = new eProveedor_Empresas();
            //objEmp = listEmpresas.Find(x => x.cod_empresa == "ALL");
            //listadoEmp.Add(objEmp);
            if (listEmpresas.Count > 0)
            {
                foreach (eProveedor_Empresas obj2 in listEmpresasUsuario)
                {
                    objEmp = listEmpresas.Find(x => x.cod_empresa == obj2.cod_empresa);
                    if (objEmp != null) listadoEmp.Add(objEmp);
                }
            }

            foreach (eProveedor_Empresas obj in listadoEmp)
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
            CargarListado(e.Link.Group.Caption, e.Link.Item.Tag.ToString());
            SplashScreenManager.CloseForm();
        }

        public void CargarListado(string NombreGrupo, string Codigo)
        {
            try
            {
                string cod_empresa = "";
                switch (NombreGrupo)
                {
                    case "Por Empresa": cod_empresa = Codigo; break;
                }

                listTrabajador = unit.Trabajador.ListarTrabajadores<eTrabajador>(1, "", cod_empresa);
                bsListaTrabajador.DataSource = listTrabajador; gvListadoTrabajador.RefreshData();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CargarListado(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                //SplashScreenManager.CloseForm();
            }
        }

        internal void frmListadoTrabajador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }

        private void gvListadoTrabajador_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eTrabajador obj = gvListadoTrabajador.GetFocusedRow() as eTrabajador;
                    if (obj == null) return;
                    if (Application.OpenForms["frmMantTrabajador"] != null)
                    {
                        Application.OpenForms["frmMantTrabajador"].Activate();
                    }
                    else
                    {
                        frmMantTrabajador frm = new frmMantTrabajador(this);
                        frm.MiAccion = Trabajador.Editar;
                        frm.cod_trabajador = obj.cod_trabajador;
                        frm.cod_empresa = obj.cod_empresa;
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantTrabajador"] != null)
            {
                Application.OpenForms["frmMantTrabajador"].Activate();
            }
            else
            {
                frmMantTrabajador frm = new frmMantTrabajador(this);
                frm.MiAccion = Trabajador.Nuevo;
                frm.ShowDialog();
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
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Personal" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListadoTrabajador.ExportToXlsx(archivo);
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

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void gvListadoTrabajador_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eTrabajador obj = gvListadoTrabajador.GetRow(e.RowHandle) as eTrabajador;
                    if (obj == null) return;
                    if (e.Column.FieldName == "fch_ingreso" && obj.fch_ingreso.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "fch_vencimiento" && obj.fch_vencimiento.ToString().Contains("1/01/0001")) e.DisplayText = "";
                    if (e.Column.FieldName == "dsc_diasvencimiento") e.DisplayText = "";
                    if (obj.flg_activo == "NO") e.Appearance.ForeColor = Color.Red;
                    e.DefaultDraw();
                    if (e.Column.FieldName == "dsc_diasvencimiento")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        int cellValue = Convert.ToInt32(e.CellValue);
                        b = cellValue > 30 ? ConCriterios : cellValue > 5 && cellValue <= 30 ? NAplCriterio : SinCriterios;
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 12, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoTrabajador_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoTrabajador_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
            GridView view = sender as GridView;
            if (view.Columns["flg_activo"] != null)
            {
                string estado = view.GetRowCellDisplayText(e.RowHandle, view.Columns["flg_activo"]);
                if (estado == "NO") e.Appearance.ForeColor = Color.Red;
            }
        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}