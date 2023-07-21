﻿
namespace UI_BackOffice.Formularios.Logistica
{
    partial class frmListadoProductoPrecios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoProductoPrecios));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnClonar = new DevExpress.XtraBars.BarButtonItem();
            this.btnInactivar = new DevExpress.XtraBars.BarButtonItem();
            this.btnActivar = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.chkPrecioUltimo = new DevExpress.XtraBars.BarCheckItem();
            this.chkPrecioTodos = new DevExpress.XtraBars.BarCheckItem();
            this.chkActivo = new DevExpress.XtraBars.BarCheckItem();
            this.chkInactivo = new DevExpress.XtraBars.BarCheckItem();
            this.btnEnviarRequerimientos = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoEdicion = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoAcciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemHypertextLabel1 = new DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblTitulo = new DevExpress.XtraEditors.LabelControl();
            this.picTitulo = new DevExpress.XtraEditors.PictureEdit();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtabProductos = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoProductos = new DevExpress.XtraGrid.GridControl();
            this.bsListadoProductos = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoProductos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_unidad_medida = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colctd_stock_actual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_proveedor1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_costo_total = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImportePR = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colimp_costo_unitario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_costo_actual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_costo_ponderado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colctd_stock_minimo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_empresas_vinculadas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rmmTexto = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtabTarifas = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoProductosTarifa = new DevExpress.XtraGrid.GridControl();
            this.bsListadoProductosTarifa = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoProductosTarifa = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_producto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_subtipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_producto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_inicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_costo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.coldsc_proveedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_marca = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_fin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_observaciones = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_subtipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_producto_SUNAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_proveedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_marca = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtabHistoricoTarifa = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.xtabHistoricoProductos = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.bsListadoHistoricoTarifa = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHypertextLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtabProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImportePR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.xtabTarifas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProductosTarifa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProductosTarifa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProductosTarifa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.xtabHistoricoTarifa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            this.xtabHistoricoProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoHistoricoTarifa)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnExportarExcel,
            this.btnImprimir,
            this.btnNuevo,
            this.btnClonar,
            this.btnInactivar,
            this.btnActivar,
            this.barStaticItem1,
            this.chkPrecioUltimo,
            this.chkPrecioTodos,
            this.chkActivo,
            this.chkInactivo,
            this.btnEnviarRequerimientos});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 23;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHypertextLabel1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.ribbon.Size = new System.Drawing.Size(1178, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar Excel";
            this.btnExportarExcel.Id = 1;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 2;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "Nuevo";
            this.btnNuevo.Id = 3;
            this.btnNuevo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.ImageOptions.Image")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnClonar
            // 
            this.btnClonar.Caption = "Clonar";
            this.btnClonar.Id = 4;
            this.btnClonar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClonar.ImageOptions.Image")));
            this.btnClonar.Name = "btnClonar";
            this.btnClonar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnClonar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClonar_ItemClick);
            // 
            // btnInactivar
            // 
            this.btnInactivar.Caption = "Inactivar";
            this.btnInactivar.Id = 5;
            this.btnInactivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInactivar.ImageOptions.Image")));
            this.btnInactivar.Name = "btnInactivar";
            this.btnInactivar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnInactivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInactivar_ItemClick);
            // 
            // btnActivar
            // 
            this.btnActivar.Caption = "Activar";
            this.btnActivar.Id = 6;
            this.btnActivar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnActivar.ImageOptions.Image")));
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnActivar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnActivar_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Presione F5 para actualizar listado         ";
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // chkPrecioUltimo
            // 
            this.chkPrecioUltimo.BindableChecked = true;
            this.chkPrecioUltimo.Caption = "Mostrar últimos precios";
            this.chkPrecioUltimo.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.chkPrecioUltimo.Checked = true;
            this.chkPrecioUltimo.Id = 18;
            this.chkPrecioUltimo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("chkPrecioUltimo.ImageOptions.Image")));
            this.chkPrecioUltimo.Name = "chkPrecioUltimo";
            this.chkPrecioUltimo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.chkPrecioUltimo.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkPrecioUltimo_CheckedChanged);
            this.chkPrecioUltimo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.chkPrecioUltimo_ItemClick);
            // 
            // chkPrecioTodos
            // 
            this.chkPrecioTodos.Caption = "Mostrar todos los precios";
            this.chkPrecioTodos.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.chkPrecioTodos.Id = 19;
            this.chkPrecioTodos.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("chkPrecioTodos.ImageOptions.Image")));
            this.chkPrecioTodos.Name = "chkPrecioTodos";
            this.chkPrecioTodos.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.chkPrecioTodos.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkPrecioTodos_CheckedChanged);
            this.chkPrecioTodos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.chkPrecioTodos_ItemClick);
            // 
            // chkActivo
            // 
            this.chkActivo.BindableChecked = true;
            this.chkActivo.Caption = "Mostrar productos activos";
            this.chkActivo.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.chkActivo.Checked = true;
            this.chkActivo.Id = 20;
            this.chkActivo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("chkActivo.ImageOptions.Image")));
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkActivo_CheckedChanged);
            this.chkActivo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.chkActivo_ItemClick);
            // 
            // chkInactivo
            // 
            this.chkInactivo.Caption = "Mostrar productos inactivos";
            this.chkInactivo.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.chkInactivo.Id = 21;
            this.chkInactivo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("chkInactivo.ImageOptions.Image")));
            this.chkInactivo.Name = "chkInactivo";
            this.chkInactivo.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.chkInactivo_CheckedChanged);
            this.chkInactivo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.chkInactivo_ItemClick);
            // 
            // btnEnviarRequerimientos
            // 
            this.btnEnviarRequerimientos.Caption = "Enviar a requerimientos";
            this.btnEnviarRequerimientos.Enabled = false;
            this.btnEnviarRequerimientos.Id = 22;
            this.btnEnviarRequerimientos.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.switchrowcolumn_16x16;
            this.btnEnviarRequerimientos.ImageOptions.LargeImage = global::UI_BackOffice.Properties.Resources.switchrowcolumn_32x32;
            this.btnEnviarRequerimientos.Name = "btnEnviarRequerimientos";
            this.btnEnviarRequerimientos.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoEdicion,
            this.grupoReportes,
            this.grupoAcciones});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones producto y precios";
            // 
            // grupoEdicion
            // 
            this.grupoEdicion.ItemLinks.Add(this.btnNuevo);
            this.grupoEdicion.ItemLinks.Add(this.btnActivar);
            this.grupoEdicion.ItemLinks.Add(this.btnInactivar);
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
            // grupoAcciones
            // 
            this.grupoAcciones.ItemLinks.Add(this.btnClonar);
            this.grupoAcciones.ItemLinks.Add(this.btnEnviarRequerimientos);
            this.grupoAcciones.Name = "grupoAcciones";
            this.grupoAcciones.Text = "Acciones";
            // 
            // repositoryItemHypertextLabel1
            // 
            this.repositoryItemHypertextLabel1.Name = "repositoryItemHypertextLabel1";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.ItemLinks.Add(this.chkPrecioUltimo);
            this.ribbonStatusBar.ItemLinks.Add(this.chkPrecioTodos);
            this.ribbonStatusBar.ItemLinks.Add(this.chkActivo);
            this.ribbonStatusBar.ItemLinks.Add(this.chkInactivo);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 731);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1178, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblTitulo);
            this.layoutControl1.Controls.Add(this.picTitulo);
            this.layoutControl1.Controls.Add(this.navBarControl1);
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1178, 573);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(130)))), ((int)(((byte)(53)))));
            this.lblTitulo.Appearance.Options.UseFont = true;
            this.lblTitulo.Appearance.Options.UseForeColor = true;
            this.lblTitulo.Location = new System.Drawing.Point(130, 6);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1042, 38);
            this.lblTitulo.StyleController = this.layoutControl1;
            this.lblTitulo.TabIndex = 9;
            this.lblTitulo.Text = "<<Titulo de grupo>>";
            // 
            // picTitulo
            // 
            this.picTitulo.Location = new System.Drawing.Point(65, 6);
            this.picTitulo.MenuManager = this.ribbon;
            this.picTitulo.Name = "picTitulo";
            this.picTitulo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picTitulo.Properties.Appearance.Options.UseBackColor = true;
            this.picTitulo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picTitulo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picTitulo.Size = new System.Drawing.Size(61, 38);
            this.picTitulo.StyleController = this.layoutControl1;
            this.picTitulo.TabIndex = 8;
            // 
            // navBarControl1
            // 
            this.navBarControl1.BackColor = System.Drawing.Color.Transparent;
            this.navBarControl1.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInGroup;
            this.navBarControl1.Location = new System.Drawing.Point(6, 6);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.NavigationPaneGroupClientHeight = 160;
            this.navBarControl1.NavigationPaneMaxVisibleGroups = 5;
            this.navBarControl1.OptionsNavPane.CollapsedWidth = 55;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 164;
            this.navBarControl1.OptionsNavPane.NavPaneState = DevExpress.XtraNavBar.NavPaneState.Collapsed;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(55, 561);
            this.navBarControl1.TabIndex = 6;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.SelectedLinkChanged += new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventHandler(this.navBarControl1_SelectedLinkChanged);
            this.navBarControl1.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.navBarControl1_ActiveGroupChanged);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(65, 48);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtabProductos;
            this.xtraTabControl1.Size = new System.Drawing.Size(1107, 519);
            this.xtraTabControl1.TabIndex = 5;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtabProductos,
            this.xtabTarifas,
            this.xtabHistoricoTarifa,
            this.xtabHistoricoProductos});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtabProductos
            // 
            this.xtabProductos.Controls.Add(this.layoutControl2);
            this.xtabProductos.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtabProductos.ImageOptions.Image")));
            this.xtabProductos.Name = "xtabProductos";
            this.xtabProductos.Size = new System.Drawing.Size(1105, 491);
            this.xtabProductos.Text = "Maestro de Productos";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcListadoProductos);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1105, 491);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcListadoProductos
            // 
            this.gcListadoProductos.DataSource = this.bsListadoProductos;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoProductos.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoProductos.Location = new System.Drawing.Point(8, 8);
            this.gcListadoProductos.MainView = this.gvListadoProductos;
            this.gcListadoProductos.MenuManager = this.ribbon;
            this.gcListadoProductos.Name = "gcListadoProductos";
            this.gcListadoProductos.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImportePR,
            this.rmmTexto});
            this.gcListadoProductos.Size = new System.Drawing.Size(1089, 475);
            this.gcListadoProductos.TabIndex = 5;
            this.gcListadoProductos.UseEmbeddedNavigator = true;
            this.gcListadoProductos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoProductos});
            // 
            // bsListadoProductos
            // 
            this.bsListadoProductos.DataSource = typeof(BE_BackOffice.eProductos);
            // 
            // gvListadoProductos
            // 
            this.gvListadoProductos.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gvListadoProductos.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvListadoProductos.Appearance.FooterPanel.Options.UseFont = true;
            this.gvListadoProductos.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvListadoProductos.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoProductos.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoProductos.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoProductos.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoProductos.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListadoProductos.ColumnPanelRowHeight = 35;
            this.gvListadoProductos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.coldsc_unidad_medida,
            this.colctd_stock_actual,
            this.coldsc_proveedor1,
            this.colimp_costo_total,
            this.colimp_costo_unitario,
            this.colimp_costo_actual,
            this.colimp_costo_ponderado,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.colctd_stock_minimo,
            this.coldsc_empresas_vinculadas});
            this.gvListadoProductos.GridControl = this.gcListadoProductos;
            this.gvListadoProductos.Name = "gvListadoProductos";
            this.gvListadoProductos.OptionsBehavior.Editable = false;
            this.gvListadoProductos.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoProductos.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoProductos.OptionsView.ShowFooter = true;
            this.gvListadoProductos.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoProductos_RowClick);
            this.gvListadoProductos.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoProductos_CustomDrawColumnHeader);
            this.gvListadoProductos.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoProductos_RowStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Cod. Producto";
            this.gridColumn1.FieldName = "cod_producto";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Width = 80;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tipo";
            this.gridColumn2.FieldName = "dsc_tipo_servicio";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "SubTipo";
            this.gridColumn3.FieldName = "dsc_subtipo_servicio";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 150;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Producto";
            this.gridColumn4.FieldName = "dsc_producto";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 300;
            // 
            // coldsc_unidad_medida
            // 
            this.coldsc_unidad_medida.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_unidad_medida.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_unidad_medida.Caption = "Und. Medida";
            this.coldsc_unidad_medida.FieldName = "dsc_unidad_medida";
            this.coldsc_unidad_medida.Name = "coldsc_unidad_medida";
            this.coldsc_unidad_medida.OptionsColumn.FixedWidth = true;
            this.coldsc_unidad_medida.Visible = true;
            this.coldsc_unidad_medida.VisibleIndex = 4;
            this.coldsc_unidad_medida.Width = 50;
            // 
            // colctd_stock_actual
            // 
            this.colctd_stock_actual.AppearanceCell.Options.UseTextOptions = true;
            this.colctd_stock_actual.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colctd_stock_actual.Caption = "Stock";
            this.colctd_stock_actual.FieldName = "ctd_stock_actual";
            this.colctd_stock_actual.Name = "colctd_stock_actual";
            this.colctd_stock_actual.OptionsColumn.FixedWidth = true;
            this.colctd_stock_actual.Width = 50;
            // 
            // coldsc_proveedor1
            // 
            this.coldsc_proveedor1.Caption = "Proveedor";
            this.coldsc_proveedor1.FieldName = "dsc_proveedor";
            this.coldsc_proveedor1.Name = "coldsc_proveedor1";
            this.coldsc_proveedor1.OptionsColumn.FixedWidth = true;
            this.coldsc_proveedor1.Visible = true;
            this.coldsc_proveedor1.VisibleIndex = 3;
            this.coldsc_proveedor1.Width = 250;
            // 
            // colimp_costo_total
            // 
            this.colimp_costo_total.Caption = "Valor Total";
            this.colimp_costo_total.ColumnEdit = this.rtxtImportePR;
            this.colimp_costo_total.FieldName = "imp_costo_total";
            this.colimp_costo_total.Name = "colimp_costo_total";
            this.colimp_costo_total.OptionsColumn.FixedWidth = true;
            this.colimp_costo_total.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_costo_total", "S/ {0:#.##}")});
            this.colimp_costo_total.Width = 80;
            // 
            // rtxtImportePR
            // 
            this.rtxtImportePR.AutoHeight = false;
            this.rtxtImportePR.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.rtxtImportePR.MaskSettings.Set("culture", "es-PE");
            this.rtxtImportePR.MaskSettings.Set("valueType", typeof(decimal));
            this.rtxtImportePR.MaskSettings.Set("mask", "c");
            this.rtxtImportePR.Name = "rtxtImportePR";
            this.rtxtImportePR.UseMaskAsDisplayFormat = true;
            // 
            // colimp_costo_unitario
            // 
            this.colimp_costo_unitario.Caption = "Valor Unitario";
            this.colimp_costo_unitario.ColumnEdit = this.rtxtImportePR;
            this.colimp_costo_unitario.FieldName = "imp_costo_unitario";
            this.colimp_costo_unitario.Name = "colimp_costo_unitario";
            this.colimp_costo_unitario.OptionsColumn.FixedWidth = true;
            this.colimp_costo_unitario.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_costo_unitario", "S/ {0:#.##}")});
            this.colimp_costo_unitario.Width = 80;
            // 
            // colimp_costo_actual
            // 
            this.colimp_costo_actual.Caption = "Último Precio";
            this.colimp_costo_actual.ColumnEdit = this.rtxtImportePR;
            this.colimp_costo_actual.FieldName = "imp_costo_actual";
            this.colimp_costo_actual.Name = "colimp_costo_actual";
            this.colimp_costo_actual.OptionsColumn.FixedWidth = true;
            this.colimp_costo_actual.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_costo_actual", "S/ {0:#,#.##}")});
            this.colimp_costo_actual.Visible = true;
            this.colimp_costo_actual.VisibleIndex = 5;
            this.colimp_costo_actual.Width = 80;
            // 
            // colimp_costo_ponderado
            // 
            this.colimp_costo_ponderado.Caption = "Importe Ponderado";
            this.colimp_costo_ponderado.ColumnEdit = this.rtxtImportePR;
            this.colimp_costo_ponderado.FieldName = "imp_costo_ponderado";
            this.colimp_costo_ponderado.Name = "colimp_costo_ponderado";
            this.colimp_costo_ponderado.OptionsColumn.FixedWidth = true;
            this.colimp_costo_ponderado.Width = 80;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "Cod. Tipo";
            this.gridColumn11.FieldName = "cod_tipo_servicio";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.FixedWidth = true;
            this.gridColumn11.Width = 50;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "Cod. SubTipo";
            this.gridColumn12.FieldName = "cod_subtipo_servicio";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.FixedWidth = true;
            this.gridColumn12.Width = 50;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "Cod. Sunat";
            this.gridColumn13.FieldName = "cod_producto_SUNAT";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.FixedWidth = true;
            this.gridColumn13.Width = 50;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "Cod. Proveedor";
            this.gridColumn14.FieldName = "cod_proveedor";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.FixedWidth = true;
            this.gridColumn14.Width = 50;
            // 
            // colctd_stock_minimo
            // 
            this.colctd_stock_minimo.Caption = "Stock Mínimo";
            this.colctd_stock_minimo.FieldName = "ctd_stock_minimo";
            this.colctd_stock_minimo.Name = "colctd_stock_minimo";
            this.colctd_stock_minimo.OptionsColumn.AllowSize = false;
            this.colctd_stock_minimo.Visible = true;
            this.colctd_stock_minimo.VisibleIndex = 6;
            // 
            // coldsc_empresas_vinculadas
            // 
            this.coldsc_empresas_vinculadas.Caption = "Empresa vinculada";
            this.coldsc_empresas_vinculadas.ColumnEdit = this.rmmTexto;
            this.coldsc_empresas_vinculadas.FieldName = "dsc_empresas_vinculadas";
            this.coldsc_empresas_vinculadas.Name = "coldsc_empresas_vinculadas";
            this.coldsc_empresas_vinculadas.OptionsColumn.FixedWidth = true;
            this.coldsc_empresas_vinculadas.Visible = true;
            this.coldsc_empresas_vinculadas.VisibleIndex = 7;
            this.coldsc_empresas_vinculadas.Width = 150;
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
            this.layoutControlItem3});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1105, 491);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcListadoProductos;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1093, 479);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // xtabTarifas
            // 
            this.xtabTarifas.Controls.Add(this.layoutControl3);
            this.xtabTarifas.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtabTarifas.ImageOptions.Image")));
            this.xtabTarifas.Name = "xtabTarifas";
            this.xtabTarifas.Size = new System.Drawing.Size(1105, 491);
            this.xtabTarifas.Text = "Maestro Precios";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.gcListadoProductosTarifa);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(1105, 491);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // gcListadoProductosTarifa
            // 
            this.gcListadoProductosTarifa.DataSource = this.bsListadoProductosTarifa;
            this.gcListadoProductosTarifa.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoProductosTarifa.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoProductosTarifa.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoProductosTarifa.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoProductosTarifa.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoProductosTarifa.Location = new System.Drawing.Point(8, 8);
            this.gcListadoProductosTarifa.MainView = this.gvListadoProductosTarifa;
            this.gcListadoProductosTarifa.MenuManager = this.ribbon;
            this.gcListadoProductosTarifa.Name = "gcListadoProductosTarifa";
            this.gcListadoProductosTarifa.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporte});
            this.gcListadoProductosTarifa.Size = new System.Drawing.Size(1089, 475);
            this.gcListadoProductosTarifa.TabIndex = 4;
            this.gcListadoProductosTarifa.UseEmbeddedNavigator = true;
            this.gcListadoProductosTarifa.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoProductosTarifa});
            // 
            // bsListadoProductosTarifa
            // 
            this.bsListadoProductosTarifa.DataSource = typeof(BE_BackOffice.eProductos.eProductosTarifas);
            // 
            // gvListadoProductosTarifa
            // 
            this.gvListadoProductosTarifa.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoProductosTarifa.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoProductosTarifa.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoProductosTarifa.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoProductosTarifa.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListadoProductosTarifa.ColumnPanelRowHeight = 35;
            this.gvListadoProductosTarifa.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_producto,
            this.coldsc_tipo_servicio,
            this.coldsc_subtipo_servicio,
            this.coldsc_producto,
            this.colfch_inicio,
            this.colimp_costo,
            this.coldsc_proveedor,
            this.coldsc_marca,
            this.colfch_fin,
            this.coldsc_observaciones,
            this.colcod_tipo_servicio,
            this.colcod_subtipo_servicio,
            this.colcod_producto_SUNAT,
            this.colcod_proveedor,
            this.colcod_marca});
            this.gvListadoProductosTarifa.GridControl = this.gcListadoProductosTarifa;
            this.gvListadoProductosTarifa.Name = "gvListadoProductosTarifa";
            this.gvListadoProductosTarifa.OptionsBehavior.Editable = false;
            this.gvListadoProductosTarifa.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoProductosTarifa.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoProductosTarifa.OptionsView.ShowIndicator = false;
            this.gvListadoProductosTarifa.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoProductosTarifa_RowClick);
            this.gvListadoProductosTarifa.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoProductosTarifa_CustomDrawColumnHeader);
            this.gvListadoProductosTarifa.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoProductosTarifa_RowStyle);
            // 
            // colcod_producto
            // 
            this.colcod_producto.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_producto.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_producto.Caption = "Cod. Producto";
            this.colcod_producto.FieldName = "cod_producto";
            this.colcod_producto.Name = "colcod_producto";
            this.colcod_producto.OptionsColumn.FixedWidth = true;
            this.colcod_producto.Width = 80;
            // 
            // coldsc_tipo_servicio
            // 
            this.coldsc_tipo_servicio.Caption = "Tipo";
            this.coldsc_tipo_servicio.FieldName = "dsc_tipo_servicio";
            this.coldsc_tipo_servicio.Name = "coldsc_tipo_servicio";
            this.coldsc_tipo_servicio.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_servicio.Visible = true;
            this.coldsc_tipo_servicio.VisibleIndex = 0;
            this.coldsc_tipo_servicio.Width = 120;
            // 
            // coldsc_subtipo_servicio
            // 
            this.coldsc_subtipo_servicio.Caption = "SubTipo";
            this.coldsc_subtipo_servicio.FieldName = "dsc_subtipo_servicio";
            this.coldsc_subtipo_servicio.Name = "coldsc_subtipo_servicio";
            this.coldsc_subtipo_servicio.OptionsColumn.FixedWidth = true;
            this.coldsc_subtipo_servicio.Visible = true;
            this.coldsc_subtipo_servicio.VisibleIndex = 1;
            this.coldsc_subtipo_servicio.Width = 120;
            // 
            // coldsc_producto
            // 
            this.coldsc_producto.Caption = "Producto";
            this.coldsc_producto.FieldName = "dsc_producto";
            this.coldsc_producto.Name = "coldsc_producto";
            this.coldsc_producto.OptionsColumn.FixedWidth = true;
            this.coldsc_producto.Visible = true;
            this.coldsc_producto.VisibleIndex = 2;
            this.coldsc_producto.Width = 180;
            // 
            // colfch_inicio
            // 
            this.colfch_inicio.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_inicio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_inicio.Caption = "Fecha";
            this.colfch_inicio.FieldName = "fch_inicio";
            this.colfch_inicio.Name = "colfch_inicio";
            this.colfch_inicio.OptionsColumn.FixedWidth = true;
            this.colfch_inicio.Visible = true;
            this.colfch_inicio.VisibleIndex = 3;
            this.colfch_inicio.Width = 65;
            // 
            // colimp_costo
            // 
            this.colimp_costo.Caption = "Ultimo Precio";
            this.colimp_costo.ColumnEdit = this.rtxtImporte;
            this.colimp_costo.FieldName = "imp_costo";
            this.colimp_costo.Name = "colimp_costo";
            this.colimp_costo.OptionsColumn.FixedWidth = true;
            this.colimp_costo.Visible = true;
            this.colimp_costo.VisibleIndex = 4;
            this.colimp_costo.Width = 50;
            // 
            // rtxtImporte
            // 
            this.rtxtImporte.AutoHeight = false;
            this.rtxtImporte.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.rtxtImporte.MaskSettings.Set("culture", "es-PE");
            this.rtxtImporte.MaskSettings.Set("valueType", typeof(decimal));
            this.rtxtImporte.MaskSettings.Set("mask", "c");
            this.rtxtImporte.Name = "rtxtImporte";
            this.rtxtImporte.UseMaskAsDisplayFormat = true;
            // 
            // coldsc_proveedor
            // 
            this.coldsc_proveedor.Caption = "Proveedor";
            this.coldsc_proveedor.FieldName = "dsc_proveedor";
            this.coldsc_proveedor.Name = "coldsc_proveedor";
            this.coldsc_proveedor.OptionsColumn.FixedWidth = true;
            this.coldsc_proveedor.Visible = true;
            this.coldsc_proveedor.VisibleIndex = 5;
            this.coldsc_proveedor.Width = 120;
            // 
            // coldsc_marca
            // 
            this.coldsc_marca.Caption = "Marca";
            this.coldsc_marca.FieldName = "dsc_marca";
            this.coldsc_marca.Name = "coldsc_marca";
            this.coldsc_marca.OptionsColumn.FixedWidth = true;
            this.coldsc_marca.Visible = true;
            this.coldsc_marca.VisibleIndex = 6;
            this.coldsc_marca.Width = 120;
            // 
            // colfch_fin
            // 
            this.colfch_fin.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_fin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_fin.Caption = "Fec. Fin";
            this.colfch_fin.FieldName = "fch_fin";
            this.colfch_fin.Name = "colfch_fin";
            this.colfch_fin.OptionsColumn.FixedWidth = true;
            this.colfch_fin.Width = 65;
            // 
            // coldsc_observaciones
            // 
            this.coldsc_observaciones.Caption = "Observaciones";
            this.coldsc_observaciones.FieldName = "dsc_observacion";
            this.coldsc_observaciones.Name = "coldsc_observaciones";
            this.coldsc_observaciones.OptionsColumn.FixedWidth = true;
            this.coldsc_observaciones.Visible = true;
            this.coldsc_observaciones.VisibleIndex = 7;
            this.coldsc_observaciones.Width = 80;
            // 
            // colcod_tipo_servicio
            // 
            this.colcod_tipo_servicio.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_tipo_servicio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_tipo_servicio.Caption = "Cod. Tipo";
            this.colcod_tipo_servicio.FieldName = "cod_tipo_servicio";
            this.colcod_tipo_servicio.Name = "colcod_tipo_servicio";
            this.colcod_tipo_servicio.OptionsColumn.FixedWidth = true;
            this.colcod_tipo_servicio.Width = 50;
            // 
            // colcod_subtipo_servicio
            // 
            this.colcod_subtipo_servicio.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_subtipo_servicio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_subtipo_servicio.Caption = "Cod. SubTipo";
            this.colcod_subtipo_servicio.FieldName = "cod_subtipo_servicio";
            this.colcod_subtipo_servicio.Name = "colcod_subtipo_servicio";
            this.colcod_subtipo_servicio.OptionsColumn.FixedWidth = true;
            this.colcod_subtipo_servicio.Width = 50;
            // 
            // colcod_producto_SUNAT
            // 
            this.colcod_producto_SUNAT.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_producto_SUNAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_producto_SUNAT.Caption = "Cod. Sunat";
            this.colcod_producto_SUNAT.FieldName = "cod_producto_SUNAT";
            this.colcod_producto_SUNAT.Name = "colcod_producto_SUNAT";
            this.colcod_producto_SUNAT.OptionsColumn.FixedWidth = true;
            this.colcod_producto_SUNAT.Width = 50;
            // 
            // colcod_proveedor
            // 
            this.colcod_proveedor.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_proveedor.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_proveedor.Caption = "Cod. Proveedor";
            this.colcod_proveedor.FieldName = "cod_proveedor";
            this.colcod_proveedor.Name = "colcod_proveedor";
            this.colcod_proveedor.OptionsColumn.FixedWidth = true;
            this.colcod_proveedor.Width = 50;
            // 
            // colcod_marca
            // 
            this.colcod_marca.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_marca.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_marca.Caption = "Cod. Marca";
            this.colcod_marca.FieldName = "cod_marca";
            this.colcod_marca.Name = "colcod_marca";
            this.colcod_marca.OptionsColumn.FixedWidth = true;
            this.colcod_marca.Width = 50;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1105, 491);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoProductosTarifa;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1093, 479);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // xtabHistoricoTarifa
            // 
            this.xtabHistoricoTarifa.Controls.Add(this.layoutControl5);
            this.xtabHistoricoTarifa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtabHistoricoTarifa.ImageOptions.Image")));
            this.xtabHistoricoTarifa.Name = "xtabHistoricoTarifa";
            this.xtabHistoricoTarifa.PageVisible = false;
            this.xtabHistoricoTarifa.Size = new System.Drawing.Size(1105, 491);
            this.xtabHistoricoTarifa.Text = "Histórico Precios";
            // 
            // layoutControl5
            // 
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Location = new System.Drawing.Point(0, 0);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup4;
            this.layoutControl5.Size = new System.Drawing.Size(1105, 491);
            this.layoutControl5.TabIndex = 0;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlGroup4.Size = new System.Drawing.Size(1105, 491);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // xtabHistoricoProductos
            // 
            this.xtabHistoricoProductos.Controls.Add(this.layoutControl4);
            this.xtabHistoricoProductos.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtabHistoricoProductos.ImageOptions.Image")));
            this.xtabHistoricoProductos.Name = "xtabHistoricoProductos";
            this.xtabHistoricoProductos.PageVisible = false;
            this.xtabHistoricoProductos.Size = new System.Drawing.Size(1105, 491);
            this.xtabHistoricoProductos.Text = "Histórico Productos";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup3;
            this.layoutControl4.Size = new System.Drawing.Size(1105, 491);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1105, 491);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.Root.Size = new System.Drawing.Size(1178, 573);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.navBarControl1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(59, 565);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.picTitulo;
            this.layoutControlItem5.Location = new System.Drawing.Point(59, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(65, 42);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(65, 42);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(65, 42);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lblTitulo;
            this.layoutControlItem6.Location = new System.Drawing.Point(124, 0);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(222, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1046, 42);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.xtraTabControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(59, 42);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1111, 523);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmListadoProductoPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 755);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmListadoProductoPrecios";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Listado de productos y precios";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoPreciosProducto_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoProductoPrecios_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHypertextLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtabProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImportePR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmTexto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.xtabTarifas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoProductosTarifa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoProductosTarifa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoProductosTarifa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.xtabHistoricoTarifa.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            this.xtabHistoricoProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoHistoricoTarifa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoEdicion;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcListadoProductosTarifa;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoProductosTarifa;
        private System.Windows.Forms.BindingSource bsListadoProductosTarifa;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_inicio;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_fin;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_costo;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_tipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_subtipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_producto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_producto;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_observaciones;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_producto_SUNAT;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_subtipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_proveedor;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_marca;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_proveedor;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_marca;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtabProductos;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraTab.XtraTabPage xtabTarifas;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTab.XtraTabPage xtabHistoricoTarifa;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gcListadoProductos;
        private System.Windows.Forms.BindingSource bsListadoProductos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoProductos;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImportePR;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource bsListadoHistoricoTarifa;
        private DevExpress.XtraGrid.Columns.GridColumn colctd_stock_actual;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_unidad_medida;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_costo_total;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_costo_unitario;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_costo_actual;
        private DevExpress.XtraBars.BarButtonItem btnClonar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoAcciones;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraTab.XtraTabPage xtabHistoricoProductos;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraBars.BarButtonItem btnInactivar;
        private DevExpress.XtraBars.BarButtonItem btnActivar;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_proveedor1;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_costo_ponderado;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresas_vinculadas;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit rmmTexto;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.PictureEdit picTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel repositoryItemHypertextLabel1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraBars.BarCheckItem chkPrecioUltimo;
        private DevExpress.XtraBars.BarCheckItem chkPrecioTodos;
        private DevExpress.XtraBars.BarCheckItem chkActivo;
        private DevExpress.XtraBars.BarCheckItem chkInactivo;
        private DevExpress.XtraGrid.Columns.GridColumn colctd_stock_minimo;
        internal DevExpress.XtraBars.BarButtonItem btnEnviarRequerimientos;
    }
}