using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BE_BackOffice;
using BL_BackOffice;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraNavBar;
using DevExpress.Images;
using UI_BackOffice.Formularios.Shared;

namespace UI_BackOffice.Formularios.Sistema.Configuraciones_Maestras
{
    public partial class frmMantUnidades_Negocio : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        List<eEmpresa> listEmpresa = new List<eEmpresa>();
        List<eUnidadNegocio> listUnidadNegocio = new List<eUnidadNegocio>();
        List<eTipoGastoCosto> listTipoGastoCosto = new List<eTipoGastoCosto>();
        bool Buscar = false;

        public frmMantUnidades_Negocio()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmUnidades_Negocio_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        private void Inicializar()
        {
            //listEmpresa = unit.Factura.Obtener_MaestrosGenerales<eEmpresa>(8, "");
            //bsEmpresas.DataSource = listEmpresa;
            rlkpflgDefecto.DataSource = unit.Factura.CombosEnGridControl<eUnidadNegocio>("FlagDefecto");
            CargarOpcionesMenu();
            CargarListado("TODOS", "");
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            lblTitulo.Text = navBarControl1.SelectedLink.Group.Caption + ": " + navBarControl1.SelectedLink.Item.Caption;
            picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 0;
            Buscar = true;
        }

        internal void CargarOpcionesMenu()
        {
            List<eCliente_Empresas> ListClienteEmp = unit.Clientes.ListarOpcionesMenu<eCliente_Empresas>(36);
            Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
            Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

            NavBarGroup NavEmpresa = navBarControl1.Groups.Add();
            NavEmpresa.Caption = "Por Empresa"; NavEmpresa.Expanded = true; NavEmpresa.SelectedLinkIndex = 0;
            NavEmpresa.GroupCaptionUseImage = NavBarImage.Large; NavEmpresa.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavEmpresa.ImageOptions.LargeImage = imgEmpresaLarge; NavEmpresa.ImageOptions.SmallImage = imgEmpresaSmall;

            foreach (eCliente_Empresas obj in ListClienteEmp)
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
            //picTitulo.Image = e.Group.ImageOptions.LargeImage;
            if (!Buscar) e.Group.SelectedLinkIndex = 0;
            if (e.Group.SelectedLink != null && Buscar)
            {
                ActiveGroupChanged(e.Group.Caption + ": " + e.Group.SelectedLink.Item.Caption, e.Group.ImageOptions.LargeImage);
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CargarListado(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
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

                listUnidadNegocio = unit.Factura.Obtener_MaestrosGenerales<eUnidadNegocio>(9, cod_empresa);
                bsUnidadNegocio.DataSource = listUnidadNegocio;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvEmpresas_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eEmpresa obj = gvEmpresas.GetFocusedRow() as eEmpresa;
                    listUnidadNegocio = unit.Factura.Obtener_MaestrosGenerales<eUnidadNegocio>(9, obj.cod_empresa);
                    bsUnidadNegocio.DataSource = listUnidadNegocio;
                    listTipoGastoCosto = unit.Factura.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, obj.cod_empresa);
                    bsTipoGastoCosto.DataSource = listTipoGastoCosto;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvEmpresas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eEmpresa obj = gvEmpresas.GetFocusedRow() as eEmpresa;
                    listUnidadNegocio = unit.Factura.Obtener_MaestrosGenerales<eUnidadNegocio>(9, obj.cod_empresa);
                    bsUnidadNegocio.DataSource = listUnidadNegocio;
                    listTipoGastoCosto = unit.Factura.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, obj.cod_empresa);
                    bsTipoGastoCosto.DataSource = listTipoGastoCosto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvUnidadNegocio_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                eEmpresa objEmp = gvEmpresas.GetFocusedRow() as eEmpresa;
                eUnidadNegocio objUN = gvUnidadNegocio.GetFocusedRow() as eUnidadNegocio;
                if (objUN != null)
                {
                    objUN.cod_empresa = objEmp.cod_empresa;
                    eUnidadNegocio obj = unit.Factura.InsertarUnidadNegocio<eUnidadNegocio>(objUN);
                    if (obj == null)
                    {
                        MessageBox.Show("Error al insertar unidad de negocio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        listUnidadNegocio = unit.Factura.Obtener_MaestrosGenerales<eUnidadNegocio>(9, objEmp.cod_empresa);
                        bsUnidadNegocio.DataSource = listUnidadNegocio;
                        return;
                    }
                    objUN.cod_und_negocio = obj.cod_und_negocio;
                    gvUnidadNegocio.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvEmpresas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvEmpresas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvUnidadNegocio_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void rbtnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            eUnidadNegocio obj = gvUnidadNegocio.GetFocusedRow() as eUnidadNegocio;
            if (obj == null) return;
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string result = unit.Factura.EliminarMaestrosGenerales(2, cod_empresa: obj.cod_empresa, cod_und_negocio: obj.cod_und_negocio);
                if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                eEmpresa objEmp = gvEmpresas.GetFocusedRow() as eEmpresa;
                listUnidadNegocio = unit.Factura.Obtener_MaestrosGenerales<eUnidadNegocio>(9, objEmp.cod_empresa);
                bsUnidadNegocio.DataSource = listUnidadNegocio;
            }
        }

        private void gvTipoGastoCosto_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvTipoGastoCosto_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvTipoGastoCosto_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                //eEmpresa objEmp = gvEmpresas.GetFocusedRow() as eEmpresa;
                //eTipoGastoCosto objTip = gvTipoGastoCosto.GetFocusedRow() as eTipoGastoCosto;
                //if (objTip != null)
                //{
                //    objTip.cod_empresa = objEmp.cod_empresa; //objTip.cod_tipo_gasto = "00001";
                //    eTipoGastoCosto obj = unit.Factura.InsertarTipoGastoCosto<eTipoGastoCosto>(objTip);
                //    if (obj == null)
                //    {
                //        MessageBox.Show("Error al insertar tipo de gasto-costo", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        listTipoGastoCosto = unit.Factura.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, objEmp.cod_empresa);
                //        bsTipoGastoCosto.DataSource = listTipoGastoCosto;
                //        return;
                //    }
                //    objTip.cod_tipo_gasto = obj.cod_tipo_gasto;
                //    gvTipoGastoCosto.RefreshData();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbtnEliminarTipoGasto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            eTipoGastoCosto obj = gvTipoGastoCosto.GetFocusedRow() as eTipoGastoCosto;
            if (obj == null) return;
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eEmpresa objEmp = gvEmpresas.GetFocusedRow() as eEmpresa;
                //obj.cod_tipo_gasto = "00001";
                string result = unit.Factura.EliminarMaestrosGenerales(1, cod_tipo_gasto: obj.cod_tipo_gasto, cod_empresa: objEmp.cod_empresa);
                if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                listTipoGastoCosto = unit.Factura.Obtener_MaestrosGenerales<eTipoGastoCosto>(10, objEmp.cod_empresa);
                bsTipoGastoCosto.DataSource = listTipoGastoCosto;
            }
        }

        private void gvUnidadNegocio_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    eUnidadNegocio obj = gvUnidadNegocio.GetFocusedRow() as eUnidadNegocio;
                    listTipoGastoCosto = unit.Factura.Obtener_MaestrosGenerales<eTipoGastoCosto>(60, obj.cod_empresa, "", obj.cod_und_negocio);
                    bsTipoGastoCosto.DataSource = listTipoGastoCosto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvUnidadNegocio_RowClick(object sender, RowClickEventArgs e)
        {
            gvUnidadNegocio_FocusedRowChanged(gvUnidadNegocio, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(e.RowHandle - 1, e.RowHandle));
        }

        private void rbtnBuscarCliente_Click(object sender, EventArgs e)
        {
            frmBusquedas frm = new frmBusquedas();
            frm.entidad = frmBusquedas.MiEntidad.ClienteEmpresa;
            frm.cod_condicion1 = navBarControl1.SelectedLink.Item.Tag.ToString();
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            eTipoGastoCosto objTip = gvTipoGastoCosto.GetFocusedRow() as eTipoGastoCosto;
            if (objTip == null) return;
            objTip.cod_cliente = frm.codigo; objTip.dsc_cliente = frm.descripcion;
            gvTipoGastoCosto.RefreshData();
        }

        private void gvTipoGastoCosto_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            eUnidadNegocio objUnd = gvUnidadNegocio.GetFocusedRow() as eUnidadNegocio;
            eTipoGastoCosto obj = gvTipoGastoCosto.GetRow(e.RowHandle) as eTipoGastoCosto;
            obj.flg_activo = "SI"; obj.cod_und_negocio = objUnd.cod_und_negocio;
        }

        private void gvUnidadNegocio_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvUnidadNegocio_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    GridView view = sender as GridView;
                    string campo = e.Column.FieldName;
                    if (view.GetRowCellValue(e.RowHandle, "flg_activo") != null && view.GetRowCellValue(e.RowHandle, "flg_activo").ToString() == "NO") e.Appearance.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvUnidadNegocio_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            eUnidadNegocio obj = gvUnidadNegocio.GetRow(e.RowHandle) as eUnidadNegocio;
            obj.flg_activo = "SI"; obj.flg_defecto = "NO";
        }
    }
}