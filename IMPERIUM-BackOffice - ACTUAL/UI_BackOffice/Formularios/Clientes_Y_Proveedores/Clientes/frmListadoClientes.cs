using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.Utils.Drawing;
using DevExpress.XtraSplashScreen;
using BE_BackOffice;
using BL_BackOffice;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.Images;
using DevExpress.XtraNavBar;
using DevExpress.XtraGrid.Views.Grid;

namespace UI_BackOffice.Clientes_Y_Proveedores.Clientes
{
    public partial class frmListadoClientes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        bool Buscar = false;

        public frmListadoClientes()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadoClientes_Load(object sender, EventArgs e)
        {
            HabilitarBotones();
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
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.Sesion.Global.Solucion);

            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                //grupoAcciones.Enabled = listPermisos[0].flg_escritura;
            }
        }
        //private void CargarOpcionesMenu()
        //{
        //    //List<eCliente> ListCabecera = new List<eCliente>();
        //    //ListCabecera = unit.Clientes.ListarOpcionesMenu<eCliente>(1);
        //    //Image img1 = ImageResourceCache.Default.GetImage("images/actions/group_32x32.png");
        //    //Image img2 = ImageResourceCache.Default.GetImage("images/actions/group_32x32.png");
        //    //Image img3 = ImageResourceCache.Default.GetImage("images/actions/group_32x32.png");

        //    //int count = 0;
        //    //foreach (eCliente objCabecera in ListCabecera)
        //    //{
        //    //    count = count + 1;
        //    //    NavBarGroup navGroup = navBarControl1.Groups.Add();
        //    //    navGroup.Name = objTipo.cod_tipo_equipo; navGroup.Caption = objTipo.dsc_tipo_equipo; navGroup.SelectedLinkIndex = 0;
        //    //    navGroup.Expanded = true; navGroup.GroupCaptionUseImage = NavBarImage.Large; navGroup.GroupStyle = NavBarGroupStyle.SmallIconsText;
        //    //    navGroup.ImageOptions.LargeImage = count == 1 ? img1 : count == 2 ? img2 : img3;
        //    //    navGroup.ImageOptions.SmallImage = count == 1 ? img1 : count == 2 ? img2 : img3;
        //    //    List<eCliente> ListDetalle = new List<eCliente>();
        //    //    ListDetalle = unit.Clientes.ListarOpcionesMenu<eCliente>(20, objTipo.cod_tipo_equipo);
        //    //    foreach (eCliente objDetalle in ListDetalle)
        //    //    {
        //    //        NavBarItem navDetalle = navBarControl1.Items.Add();
        //    //        navDetalle.Name = objSubTipo.cod_subtipo_equipo; navDetalle.Caption = objSubTipo.dsc_subtipo_equipo;

        //    //        navGroup.ItemLinks.Add(navDetalle);
        //    //    }
        //    //}
        //}

        internal void CargarOpcionesMenu()
        {
            List<eCliente> ListCliente = new List<eCliente>();
            ListCliente = unit.Clientes.ListarOpcionesMenu<eCliente>(1);
            Image imgTipoCliLarge = ImageResourceCache.Default.GetImage("images/business%20objects/bodepartment_32x32.png");
            Image imgTipoCliSmall = ImageResourceCache.Default.GetImage("images/business%20objects/bodepartment_16x16.png");

            NavBarGroup NavTipoCli = navBarControl1.Groups.Add();
            NavTipoCli.Caption = "Por Tipo Cliente"; NavTipoCli.Expanded = true; NavTipoCli.SelectedLinkIndex = 0;
            NavTipoCli.GroupCaptionUseImage = NavBarImage.Large; NavTipoCli.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavTipoCli.ImageOptions.LargeImage = imgTipoCliLarge; NavTipoCli.ImageOptions.SmallImage = imgTipoCliSmall;

            foreach (eCliente obj in ListCliente)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_tipo_cliente; NavDetalle.Name = obj.cod_tipo_cliente;
                NavDetalle.Caption = obj.dsc_tipo_cliente; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavTipoCli.ItemLinks.Add(NavDetalle);
            }


            ListCliente = unit.Clientes.ListarOpcionesMenu<eCliente>(2);
            Image imgCategCliLarge = ImageResourceCache.Default.GetImage("images/richedit/differentoddevenpages_32x32.png");
            Image imgCategCliSmall = ImageResourceCache.Default.GetImage("images/richedit/differentoddevenpages_16x16.png");

            NavBarGroup NavCategCli = navBarControl1.Groups.Add();
            NavCategCli.Caption = "Por Categoría Cliente"; NavCategCli.Expanded = true; NavCategCli.SelectedLinkIndex = 0;
            NavCategCli.GroupCaptionUseImage = NavBarImage.Large; NavCategCli.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavCategCli.ImageOptions.LargeImage = imgCategCliLarge; NavCategCli.ImageOptions.SmallImage = imgCategCliSmall;

            foreach (eCliente obj in ListCliente)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_categoria; NavDetalle.Name = obj.cod_categoria;
                NavDetalle.Caption = obj.dsc_categoria; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavCategCli.ItemLinks.Add(NavDetalle);
            }


            //ListCliente = unit.Clientes.ListarOpcionesMenu<eCliente>(3);
            //Image imgTipoDocLarge = ImageResourceCache.Default.GetImage("images/business%20objects/bonote_32x32.png");
            //Image imgTipoDocSmall = ImageResourceCache.Default.GetImage("images/business%20objects/bonote_16x16.png");

            //NavBarGroup NavTipoDoc = navBarControl1.Groups.Add();
            //NavTipoDoc.Caption = "Por Tipo Documento"; NavTipoDoc.Expanded = true; NavTipoDoc.SelectedLinkIndex = 0;
            //NavTipoDoc.GroupCaptionUseImage = NavBarImage.Large; NavTipoDoc.GroupStyle = NavBarGroupStyle.SmallIconsText;
            //NavTipoDoc.ImageOptions.LargeImage = imgTipoDocLarge; NavTipoDoc.ImageOptions.SmallImage = imgTipoDocSmall;

            //foreach (eCliente obj in ListCliente)
            //{
            //    NavBarItem NavDetalle = navBarControl1.Items.Add();
            //    NavDetalle.Tag = obj.cod_tipo_documento; NavDetalle.Name = obj.cod_tipo_documento;
            //    NavDetalle.Caption = obj.dsc_tipo_documento; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

            //    NavTipoDoc.ItemLinks.Add(NavDetalle);
            //}

            ListCliente = unit.Clientes.ListarOpcionesMenu<eCliente>(4);
            Image imgCalifCliLarge = ImageResourceCache.Default.GetImage("images/filter%20elements/checkbuttons_32x32.png");
            Image imgCalifCliSmall = ImageResourceCache.Default.GetImage("images/filter%20elements/checkbuttons_16x16.png");

            NavBarGroup NavCalifCli = navBarControl1.Groups.Add();
            NavCalifCli.Caption = "Por Calificación"; NavCalifCli.Expanded = true; NavCalifCli.SelectedLinkIndex = 0;
            NavCalifCli.GroupCaptionUseImage = NavBarImage.Large; NavCalifCli.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavCalifCli.ImageOptions.LargeImage = imgCalifCliLarge; NavCalifCli.ImageOptions.SmallImage = imgCalifCliSmall;

            foreach (eCliente obj in ListCliente)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_calificacion; NavDetalle.Name = obj.cod_calificacion;
                NavDetalle.Caption = obj.dsc_calificacion; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavCalifCli.ItemLinks.Add(NavDetalle);
            }

            //ListCliente = unit.Clientes.ListarOpcionesMenu<eCliente>(5);
            //Image imgTipoContLarge = ImageResourceCache.Default.GetImage("images/business%20objects/bocontact2_32x32.png");
            //Image imgTipoContSmall = ImageResourceCache.Default.GetImage("images/business%20objects/bocontact2_16x16.png");

            //NavBarGroup NavTipoCont = navBarControl1.Groups.Add();
            //NavTipoCont.Caption = "Por Tipo Contacto"; NavTipoCont.Expanded = true; NavTipoCont.SelectedLinkIndex = 0;
            //NavTipoCont.GroupCaptionUseImage = NavBarImage.Large; NavTipoCont.GroupStyle = NavBarGroupStyle.SmallIconsText;
            //NavTipoCont.ImageOptions.LargeImage = imgTipoContLarge; NavTipoCont.ImageOptions.SmallImage = imgTipoContSmall;

            //foreach (eCliente obj in ListCliente)
            //{
            //    NavBarItem NavDetalle = navBarControl1.Items.Add();
            //    NavDetalle.Tag = obj.cod_tipo_contacto; NavDetalle.Name = obj.cod_tipo_contacto;
            //    NavDetalle.Caption = obj.dsc_tipo_contacto; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

            //    NavTipoCont.ItemLinks.Add(NavDetalle);
            //}

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

            //NavTipoCont.SelectedLinkIndex = 0;
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
                string cod_tipo_cliente = "", cod_categoria = "", cod_tipo_documento = "", cod_calificacion = "", cod_tipo_contacto = "", cod_empresa = "";
                switch (NombreGrupo)
                {
                    case "Por Tipo Cliente": cod_tipo_cliente = Codigo; break;
                    case "Por Categoría Cliente": cod_categoria = Codigo; break;
                    case "Por Tipo Documento": cod_tipo_documento = Codigo; break;
                    case "Por Calificación": cod_calificacion = Codigo; break;
                    case "Por Tipo Contacto": cod_tipo_contacto = Codigo; break;
                    case "Por Empresa": cod_empresa = Codigo; break;
                }

                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                List<eCliente> ListCliente = new List<eCliente>();
                ListCliente = unit.Clientes.ListarClientes<eCliente>(1, cod_tipo_cliente, cod_categoria, cod_tipo_documento, cod_calificacion, cod_tipo_contacto, cod_empresa);
                /*bsListaClientes.DataSource = null; */bsListaClientes.DataSource = ListCliente;
                //SplashScreenManager.CloseForm();
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
            //picTitulo.Image = e.Group.ImageOptions.LargeImage;
            if(!Buscar) e.Group.SelectedLinkIndex = 0;
            if (e.Group.SelectedLink != null && Buscar)
            {
                ActiveGroupChanged(e.Group.Caption + ": " + e.Group.SelectedLink.Item.Caption, e.Group.ImageOptions.LargeImage);
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CargarListado(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
            }
        }
        
        private void gvListaClientes_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        internal void frmListadoClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
                CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
                SplashScreenManager.CloseForm();
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
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Clientes" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListaClientes.ExportToXlsx(archivo);
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
        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListaClientes.ShowPrintPreview();
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmMantCliente frm = new frmMantCliente();
                frm.MiAccion = Cliente.Nuevo;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaClientes_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eCliente obj = gvListaClientes.GetFocusedRow() as eCliente;

                    frmMantCliente frm = new frmMantCliente(this);
                    frm.cod_cliente = obj.cod_cliente;
                    frm.MiAccion = Cliente.Editar;
                    frm.cod_empresa = navBarControl1.SelectedLink.Item.Tag.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListaClientes_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    eCliente obj = gvListaClientes.GetFocusedRow() as eCliente;
                
            //    frmMantCliente frm = new frmMantCliente(this);
            //    frm.cod_cliente = obj.cod_cliente;
            //    frm.MiAccion = Cliente.Editar;
            //    frm.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
        
        private void gvListaClientes_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }
        

        private void btnActivar_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnInactivar_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eCliente eCli = gvListaClientes.GetFocusedRow() as eCliente;

                eCliente eCliVal = unit.Clientes.ValidacionEliminar<eCliente>(31, eCli.cod_cliente);
                if (eCliVal != null) { MessageBox.Show("No se puede eliminar el cliente ya que tiene comprobantess.", "Eliminar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                eCliVal = unit.Clientes.ValidacionEliminar<eCliente>(32, eCli.cod_cliente);
                if (eCliVal != null) { MessageBox.Show("No se puede eliminar el cliente ya que tiene pedidos.", "Eliminar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                eCliVal = unit.Clientes.ValidacionEliminar<eCliente>(33, eCli.cod_cliente);
                if (eCliVal != null) { MessageBox.Show("No se puede eliminar el cliente ya que tiene ventas.", "Eliminar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este cliente?", "Eliminar cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {

                    string result = unit.Clientes.Eliminar_Cliente(eCli.cod_cliente);
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    CargarListado(navBarControl1.SelectedLink.Group.Caption, navBarControl1.SelectedLink.Item.Tag.ToString());
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}