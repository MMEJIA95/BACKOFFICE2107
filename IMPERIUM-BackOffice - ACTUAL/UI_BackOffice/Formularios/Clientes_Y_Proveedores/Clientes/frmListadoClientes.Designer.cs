namespace UI_BackOffice.Clientes_Y_Proveedores.Clientes
{
    partial class frmListadoClientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoClientes));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnActivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnInactivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.barHeaderItem1 = new DevExpress.XtraBars.BarHeaderItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnConsultarSunat = new DevExpress.XtraBars.BarButtonItem();
            this.pageAccGenerales = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoEdicion = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.picTitulo = new DevExpress.XtraEditors.PictureEdit();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.gcListaClientes = new DevExpress.XtraGrid.GridControl();
            this.bsListaClientes = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaClientes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_cliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_cliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_razon_comercial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_cliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_calificacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_categoria = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_contacto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_email = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_telefono_1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_telefono_2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_direccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_cadena_direccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_pais = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_distrito = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_provincia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_departamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_sexo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_juridico = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_cambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colvalorRating = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rRatingCalificacion = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.coldsc_empresas_vinculadas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rmmTexto = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnNuevo,
            this.btnActivar,
            this.btnInactivar,
            this.btnExportarExcel,
            this.btnImprimir,
            this.barHeaderItem1,
            this.barStaticItem1,
            this.btnEliminar,
            this.btnConsultarSunat});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 10;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.pageAccGenerales});
            this.ribbon.Size = new System.Drawing.Size(1166, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "Nuevo";
            this.btnNuevo.Id = 1;
            this.btnNuevo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.ImageOptions.Image")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnActivar
            // 
            this.btnActivar.Caption = "Activar";
            this.btnActivar.Enabled = false;
            this.btnActivar.Id = 2;
            this.btnActivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActivar.ImageOptions.Image")));
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnActivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnActivar_ItemClick);
            // 
            // btnInactivar
            // 
            this.btnInactivar.Caption = "Inactivar";
            this.btnInactivar.Enabled = false;
            this.btnInactivar.Id = 3;
            this.btnInactivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInactivar.ImageOptions.Image")));
            this.btnInactivar.Name = "btnInactivar";
            this.btnInactivar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnInactivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInactivar_ItemClick);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar a Excel";
            this.btnExportarExcel.Id = 4;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 5;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimir_ItemClick);
            // 
            // barHeaderItem1
            // 
            this.barHeaderItem1.Caption = "barHeaderItem1";
            this.barHeaderItem1.Id = 6;
            this.barHeaderItem1.Name = "barHeaderItem1";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Presione F5 para actualizar listado";
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "Eliminar";
            this.btnEliminar.Id = 8;
            this.btnEliminar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.ImageOptions.Image")));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminar_ItemClick);
            // 
            // btnConsultarSunat
            // 
            this.btnConsultarSunat.Caption = "Consultar RUC en Sunat";
            this.btnConsultarSunat.Id = 9;
            this.btnConsultarSunat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultarSunat.ImageOptions.Image")));
            this.btnConsultarSunat.Name = "btnConsultarSunat";
            this.btnConsultarSunat.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // pageAccGenerales
            // 
            this.pageAccGenerales.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoEdicion,
            this.grupoReportes});
            this.pageAccGenerales.Name = "pageAccGenerales";
            this.pageAccGenerales.Text = "Opciones de Clientes";
            // 
            // grupoEdicion
            // 
            this.grupoEdicion.ItemLinks.Add(this.btnNuevo);
            this.grupoEdicion.ItemLinks.Add(this.btnActivar);
            this.grupoEdicion.ItemLinks.Add(this.btnInactivar);
            this.grupoEdicion.ItemLinks.Add(this.btnEliminar);
            this.grupoEdicion.Name = "grupoEdicion";
            this.grupoEdicion.Text = "Edición";
            // 
            // grupoReportes
            // 
            this.grupoReportes.ItemLinks.Add(this.btnExportarExcel);
            this.grupoReportes.ItemLinks.Add(this.btnImprimir);
            this.grupoReportes.Name = "grupoReportes";
            this.grupoReportes.Text = "Reportes";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 638);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1166, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblTitulo);
            this.layoutControl1.Controls.Add(this.picTitulo);
            this.layoutControl1.Controls.Add(this.navBarControl1);
            this.layoutControl1.Controls.Add(this.gcListaClientes);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1166, 480);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(49)))), ((int)(((byte)(35)))));
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(129, 7);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1030, 31);
            this.lblTitulo.StyleController = this.layoutControl1;
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "<<Titulo de grupo>>";
            // 
            // picTitulo
            // 
            this.picTitulo.Location = new System.Drawing.Point(66, 7);
            this.picTitulo.MenuManager = this.ribbon;
            this.picTitulo.Name = "picTitulo";
            this.picTitulo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTitulo.Properties.Appearance.Options.UseBackColor = true;
            this.picTitulo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTitulo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTitulo.Size = new System.Drawing.Size(59, 31);
            this.picTitulo.StyleController = this.layoutControl1;
            this.picTitulo.TabIndex = 1;
            // 
            // navBarControl1
            // 
            this.navBarControl1.BackColor = System.Drawing.Color.Transparent;
            this.navBarControl1.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInGroup;
            this.navBarControl1.Location = new System.Drawing.Point(7, 7);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.NavigationPaneGroupClientHeight = 160;
            this.navBarControl1.NavigationPaneMaxVisibleGroups = 6;
            this.navBarControl1.OptionsNavPane.CollapsedWidth = 55;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 164;
            this.navBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(55, 466);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.View = new DevExpress.XtraNavBar.ViewInfo.StandardSkinNavigationPaneViewInfoRegistrator("DevExpress Style");
            this.navBarControl1.SelectedLinkChanged += new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventHandler(this.navBarControl1_SelectedLinkChanged);
            this.navBarControl1.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.navBarControl1_ActiveGroupChanged);
            // 
            // gcListaClientes
            // 
            this.gcListaClientes.DataSource = this.bsListaClientes;
            this.gcListaClientes.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListaClientes.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListaClientes.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListaClientes.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListaClientes.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListaClientes.Location = new System.Drawing.Point(66, 42);
            this.gcListaClientes.MainView = this.gvListaClientes;
            this.gcListaClientes.MenuManager = this.ribbon;
            this.gcListaClientes.Name = "gcListaClientes";
            this.gcListaClientes.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rRatingCalificacion,
            this.rmmTexto});
            this.gcListaClientes.Size = new System.Drawing.Size(1093, 431);
            this.gcListaClientes.TabIndex = 0;
            this.gcListaClientes.UseEmbeddedNavigator = true;
            this.gcListaClientes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaClientes});
            // 
            // bsListaClientes
            // 
            this.bsListaClientes.DataSource = typeof(BE_BackOffice.eCliente);
            // 
            // gvListaClientes
            // 
            this.gvListaClientes.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvListaClientes.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvListaClientes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaClientes.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvListaClientes.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListaClientes.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaClientes.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaClientes.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListaClientes.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaClientes.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvListaClientes.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gvListaClientes.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvListaClientes.ColumnPanelRowHeight = 35;
            this.gvListaClientes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_cliente,
            this.coldsc_cliente,
            this.coldsc_razon_comercial,
            this.coldsc_tipo_documento,
            this.coldsc_documento,
            this.coldsc_tipo_cliente,
            this.coldsc_calificacion,
            this.coldsc_categoria,
            this.coldsc_tipo_contacto,
            this.coldsc_email,
            this.coldsc_telefono_1,
            this.coldsc_telefono_2,
            this.coldsc_tipo_direccion,
            this.coldsc_cadena_direccion,
            this.coldsc_pais,
            this.coldsc_distrito,
            this.coldsc_provincia,
            this.coldsc_departamento,
            this.coldsc_sexo,
            this.colflg_juridico,
            this.colfch_registro,
            this.coldsc_usuario_registro,
            this.colfch_cambio,
            this.coldsc_usuario_cambio,
            this.colvalorRating,
            this.coldsc_empresas_vinculadas});
            this.gvListaClientes.GridControl = this.gcListaClientes;
            this.gvListaClientes.Name = "gvListaClientes";
            this.gvListaClientes.OptionsBehavior.Editable = false;
            this.gvListaClientes.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvListaClientes.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListaClientes.OptionsView.RowAutoHeight = true;
            this.gvListaClientes.OptionsView.ShowAutoFilterRow = true;
            this.gvListaClientes.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListaClientes_RowClick);
            this.gvListaClientes.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListaClientes_CustomDrawColumnHeader);
            this.gvListaClientes.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListaClientes_RowStyle);
            this.gvListaClientes.DoubleClick += new System.EventHandler(this.gvListaClientes_DoubleClick);
            // 
            // colcod_cliente
            // 
            this.colcod_cliente.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_cliente.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_cliente.Caption = "Código";
            this.colcod_cliente.FieldName = "cod_cliente";
            this.colcod_cliente.Name = "colcod_cliente";
            this.colcod_cliente.OptionsColumn.FixedWidth = true;
            this.colcod_cliente.Visible = true;
            this.colcod_cliente.VisibleIndex = 0;
            this.colcod_cliente.Width = 90;
            // 
            // coldsc_cliente
            // 
            this.coldsc_cliente.Caption = "Razón Social / Apellidos y Nombres";
            this.coldsc_cliente.FieldName = "dsc_cliente";
            this.coldsc_cliente.Name = "coldsc_cliente";
            this.coldsc_cliente.OptionsColumn.FixedWidth = true;
            this.coldsc_cliente.Visible = true;
            this.coldsc_cliente.VisibleIndex = 1;
            this.coldsc_cliente.Width = 300;
            // 
            // coldsc_razon_comercial
            // 
            this.coldsc_razon_comercial.Caption = "Nombre Comercial";
            this.coldsc_razon_comercial.FieldName = "dsc_razon_comercial";
            this.coldsc_razon_comercial.Name = "coldsc_razon_comercial";
            this.coldsc_razon_comercial.OptionsColumn.FixedWidth = true;
            this.coldsc_razon_comercial.Visible = true;
            this.coldsc_razon_comercial.VisibleIndex = 2;
            this.coldsc_razon_comercial.Width = 200;
            // 
            // coldsc_tipo_documento
            // 
            this.coldsc_tipo_documento.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_tipo_documento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_tipo_documento.Caption = "Tipo Doc";
            this.coldsc_tipo_documento.FieldName = "dsc_tipo_documento";
            this.coldsc_tipo_documento.Name = "coldsc_tipo_documento";
            this.coldsc_tipo_documento.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_documento.Visible = true;
            this.coldsc_tipo_documento.VisibleIndex = 3;
            // 
            // coldsc_documento
            // 
            this.coldsc_documento.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_documento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_documento.Caption = "N° de Documento";
            this.coldsc_documento.FieldName = "dsc_documento";
            this.coldsc_documento.Name = "coldsc_documento";
            this.coldsc_documento.OptionsColumn.FixedWidth = true;
            this.coldsc_documento.Visible = true;
            this.coldsc_documento.VisibleIndex = 4;
            this.coldsc_documento.Width = 100;
            // 
            // coldsc_tipo_cliente
            // 
            this.coldsc_tipo_cliente.Caption = "Tipo de Cliente";
            this.coldsc_tipo_cliente.FieldName = "dsc_tipo_cliente";
            this.coldsc_tipo_cliente.Name = "coldsc_tipo_cliente";
            this.coldsc_tipo_cliente.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_cliente.Visible = true;
            this.coldsc_tipo_cliente.VisibleIndex = 5;
            this.coldsc_tipo_cliente.Width = 120;
            // 
            // coldsc_calificacion
            // 
            this.coldsc_calificacion.Caption = "Clasificación";
            this.coldsc_calificacion.FieldName = "dsc_calificacion";
            this.coldsc_calificacion.Name = "coldsc_calificacion";
            this.coldsc_calificacion.OptionsColumn.FixedWidth = true;
            this.coldsc_calificacion.Visible = true;
            this.coldsc_calificacion.VisibleIndex = 6;
            this.coldsc_calificacion.Width = 120;
            // 
            // coldsc_categoria
            // 
            this.coldsc_categoria.Caption = "Categoría";
            this.coldsc_categoria.FieldName = "dsc_categoria";
            this.coldsc_categoria.Name = "coldsc_categoria";
            this.coldsc_categoria.OptionsColumn.FixedWidth = true;
            this.coldsc_categoria.Visible = true;
            this.coldsc_categoria.VisibleIndex = 7;
            this.coldsc_categoria.Width = 120;
            // 
            // coldsc_tipo_contacto
            // 
            this.coldsc_tipo_contacto.Caption = "Tipo Contacto";
            this.coldsc_tipo_contacto.FieldName = "dsc_tipo_contacto";
            this.coldsc_tipo_contacto.Name = "coldsc_tipo_contacto";
            this.coldsc_tipo_contacto.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_contacto.Visible = true;
            this.coldsc_tipo_contacto.VisibleIndex = 8;
            this.coldsc_tipo_contacto.Width = 120;
            // 
            // coldsc_email
            // 
            this.coldsc_email.Caption = "Email";
            this.coldsc_email.FieldName = "dsc_email";
            this.coldsc_email.Name = "coldsc_email";
            this.coldsc_email.OptionsColumn.FixedWidth = true;
            this.coldsc_email.Visible = true;
            this.coldsc_email.VisibleIndex = 9;
            this.coldsc_email.Width = 160;
            // 
            // coldsc_telefono_1
            // 
            this.coldsc_telefono_1.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_telefono_1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_telefono_1.Caption = "Teléfono 1";
            this.coldsc_telefono_1.FieldName = "dsc_telefono_1";
            this.coldsc_telefono_1.Name = "coldsc_telefono_1";
            this.coldsc_telefono_1.OptionsColumn.FixedWidth = true;
            this.coldsc_telefono_1.Visible = true;
            this.coldsc_telefono_1.VisibleIndex = 10;
            this.coldsc_telefono_1.Width = 82;
            // 
            // coldsc_telefono_2
            // 
            this.coldsc_telefono_2.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_telefono_2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_telefono_2.Caption = "Teléfono 2";
            this.coldsc_telefono_2.FieldName = "dsc_telefono_2";
            this.coldsc_telefono_2.Name = "coldsc_telefono_2";
            this.coldsc_telefono_2.OptionsColumn.FixedWidth = true;
            this.coldsc_telefono_2.Width = 82;
            // 
            // coldsc_tipo_direccion
            // 
            this.coldsc_tipo_direccion.Caption = "Tipo dirección";
            this.coldsc_tipo_direccion.FieldName = "dsc_tipo_direccion";
            this.coldsc_tipo_direccion.Name = "coldsc_tipo_direccion";
            this.coldsc_tipo_direccion.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_direccion.Width = 100;
            // 
            // coldsc_cadena_direccion
            // 
            this.coldsc_cadena_direccion.Caption = "Dirección";
            this.coldsc_cadena_direccion.FieldName = "dsc_cadena_direccion";
            this.coldsc_cadena_direccion.Name = "coldsc_cadena_direccion";
            this.coldsc_cadena_direccion.OptionsColumn.FixedWidth = true;
            this.coldsc_cadena_direccion.Width = 180;
            // 
            // coldsc_pais
            // 
            this.coldsc_pais.Caption = "País";
            this.coldsc_pais.FieldName = "dsc_pais";
            this.coldsc_pais.Name = "coldsc_pais";
            this.coldsc_pais.OptionsColumn.FixedWidth = true;
            this.coldsc_pais.Width = 100;
            // 
            // coldsc_distrito
            // 
            this.coldsc_distrito.Caption = "Distrito";
            this.coldsc_distrito.FieldName = "dsc_distrito";
            this.coldsc_distrito.Name = "coldsc_distrito";
            this.coldsc_distrito.OptionsColumn.FixedWidth = true;
            this.coldsc_distrito.Width = 100;
            // 
            // coldsc_provincia
            // 
            this.coldsc_provincia.Caption = "Provincia";
            this.coldsc_provincia.FieldName = "dsc_provincia";
            this.coldsc_provincia.Name = "coldsc_provincia";
            this.coldsc_provincia.OptionsColumn.FixedWidth = true;
            this.coldsc_provincia.Width = 100;
            // 
            // coldsc_departamento
            // 
            this.coldsc_departamento.Caption = "Departamento";
            this.coldsc_departamento.FieldName = "dsc_departamento";
            this.coldsc_departamento.Name = "coldsc_departamento";
            this.coldsc_departamento.OptionsColumn.FixedWidth = true;
            this.coldsc_departamento.Width = 100;
            // 
            // coldsc_sexo
            // 
            this.coldsc_sexo.Caption = "Sexo";
            this.coldsc_sexo.FieldName = "dsc_sexo";
            this.coldsc_sexo.Name = "coldsc_sexo";
            this.coldsc_sexo.OptionsColumn.FixedWidth = true;
            this.coldsc_sexo.Width = 70;
            // 
            // colflg_juridico
            // 
            this.colflg_juridico.Caption = "Es Jurid.";
            this.colflg_juridico.FieldName = "flg_juridico";
            this.colflg_juridico.Name = "colflg_juridico";
            this.colflg_juridico.OptionsColumn.FixedWidth = true;
            this.colflg_juridico.ToolTip = "Es persona jurídica";
            this.colflg_juridico.Width = 70;
            // 
            // colfch_registro
            // 
            this.colfch_registro.Caption = "Fecha registro";
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.Name = "colfch_registro";
            this.colfch_registro.OptionsColumn.FixedWidth = true;
            this.colfch_registro.Width = 80;
            // 
            // coldsc_usuario_registro
            // 
            this.coldsc_usuario_registro.Caption = "Usuario registro";
            this.coldsc_usuario_registro.FieldName = "dsc_usuario_registro";
            this.coldsc_usuario_registro.Name = "coldsc_usuario_registro";
            this.coldsc_usuario_registro.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_registro.Width = 120;
            // 
            // colfch_cambio
            // 
            this.colfch_cambio.Caption = "Fecha modificación";
            this.colfch_cambio.FieldName = "fch_cambio";
            this.colfch_cambio.Name = "colfch_cambio";
            this.colfch_cambio.OptionsColumn.FixedWidth = true;
            this.colfch_cambio.Width = 80;
            // 
            // coldsc_usuario_cambio
            // 
            this.coldsc_usuario_cambio.Caption = "Usuario modificación";
            this.coldsc_usuario_cambio.FieldName = "dsc_usuario_cambio";
            this.coldsc_usuario_cambio.Name = "coldsc_usuario_cambio";
            this.coldsc_usuario_cambio.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_cambio.Width = 120;
            // 
            // colvalorRating
            // 
            this.colvalorRating.Caption = "Calificación";
            this.colvalorRating.ColumnEdit = this.rRatingCalificacion;
            this.colvalorRating.FieldName = "valorRating";
            this.colvalorRating.Name = "colvalorRating";
            this.colvalorRating.OptionsColumn.AllowEdit = false;
            this.colvalorRating.OptionsColumn.FixedWidth = true;
            this.colvalorRating.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colvalorRating.Visible = true;
            this.colvalorRating.VisibleIndex = 11;
            this.colvalorRating.Width = 100;
            // 
            // rRatingCalificacion
            // 
            this.rRatingCalificacion.AutoHeight = false;
            this.rRatingCalificacion.CheckedGlyph = global::UI_BackOffice.Properties.Resources.estrella_azul;
            this.rRatingCalificacion.FillPrecision = DevExpress.XtraEditors.RatingItemFillPrecision.Exact;
            this.rRatingCalificacion.ItemCount = 4;
            this.rRatingCalificacion.ItemIndent = 10;
            this.rRatingCalificacion.Name = "rRatingCalificacion";
            // 
            // coldsc_empresas_vinculadas
            // 
            this.coldsc_empresas_vinculadas.Caption = "Empresas vinculadas";
            this.coldsc_empresas_vinculadas.ColumnEdit = this.rmmTexto;
            this.coldsc_empresas_vinculadas.FieldName = "dsc_empresas_vinculadas";
            this.coldsc_empresas_vinculadas.Name = "coldsc_empresas_vinculadas";
            this.coldsc_empresas_vinculadas.OptionsColumn.FixedWidth = true;
            this.coldsc_empresas_vinculadas.Width = 250;
            // 
            // rmmTexto
            // 
            this.rmmTexto.Name = "rmmTexto";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1166, 480);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListaClientes;
            this.layoutControlItem1.Location = new System.Drawing.Point(59, 35);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1097, 435);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.navBarControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(59, 470);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.picTitulo;
            this.layoutControlItem3.Location = new System.Drawing.Point(59, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(63, 35);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(63, 35);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(63, 35);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblTitulo;
            this.layoutControlItem4.Location = new System.Drawing.Point(122, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(222, 28);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1034, 35);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // frmListadoClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 662);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Name = "frmListadoClientes";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Listado de Clientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoClientes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoClientes_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage pageAccGenerales;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoEdicion;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnActivar;
        private DevExpress.XtraBars.BarButtonItem btnInactivar;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraGrid.GridControl gcListaClientes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsListaClientes;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_cliente;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_cliente;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_documento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_calificacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_categoria;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_cliente;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_email;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_telefono_1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_telefono_2;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_documento;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.PictureEdit picTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_contacto;
        public DevExpress.XtraGrid.Views.Grid.GridView gvListaClientes;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_pais;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_distrito;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_provincia;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_departamento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_sexo;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_juridico;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_direccion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_cadena_direccion;
        private DevExpress.XtraBars.BarButtonItem btnConsultarSunat;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_razon_comercial;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_registro;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cambio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_cambio;
        private DevExpress.XtraGrid.Columns.GridColumn colvalorRating;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl rRatingCalificacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresas_vinculadas;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit rmmTexto;
    }
}