
namespace UI_BackOffice.Formularios.Personal
{
    partial class frmListaControlHoras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListaControlHoras));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView1 = new DevExpress.XtraCharts.StackedBarSeriesView();
            DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView2 = new DevExpress.XtraCharts.StackedBarSeriesView();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnCostoHora = new DevExpress.XtraBars.BarButtonItem();
            this.btnMultipleRegistros = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.btnGestionActividades = new DevExpress.XtraBars.BarButtonItem();
            this.pageAccGenerales = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoEdicion = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtbListado = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.txtTotalHoras = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.gcListadoControl = new DevExpress.XtraGrid.GridControl();
            this.bsListadoControlHoras = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoControl = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_control_horas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_segmento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_grupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_actividad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_duracion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_ejecucion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_usuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_comentario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colhoras_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colhoras_usuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colhoras_fecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colhoras_fecha_usuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtbReporte = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl2 = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl6 = new DevExpress.XtraLayout.LayoutControl();
            this.gcDetalleListado = new DevExpress.XtraGrid.GridControl();
            this.gvDetalleListado = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_usuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_empresa1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_segmento1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_grupo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_actividad1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcnt_horas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprc_horas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colhoras_empresa1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colhoras_usuario1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_duracion1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_ejecucion1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkcbEmpresa = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.dtFechaInicio = new DevExpress.XtraEditors.DateEdit();
            this.dtFechaFin = new DevExpress.XtraEditors.DateEdit();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.chkcbUsuario = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.timeSpanChartRangeControlClient1 = new DevExpress.XtraEditors.TimeSpanChartRangeControlClient();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtbListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalHoras.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoControlHoras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.xtbReporte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).BeginInit();
            this.layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetalleListado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalleListado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcbEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaInicio.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaInicio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaFin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcbUsuario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanChartRangeControlClient1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnNuevo,
            this.btnEliminar,
            this.barStaticItem1,
            this.btnExportarExcel,
            this.btnCostoHora,
            this.btnMultipleRegistros,
            this.btnImprimir,
            this.btnGestionActividades});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 90;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.pageAccGenerales});
            this.ribbon.Size = new System.Drawing.Size(1126, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "Nuevo";
            this.btnNuevo.Id = 1;
            this.btnNuevo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.ImageOptions.Image")));
            this.btnNuevo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNuevo.ImageOptions.LargeImage")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "Eliminar";
            this.btnEliminar.Id = 2;
            this.btnEliminar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.ImageOptions.Image")));
            this.btnEliminar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.ImageOptions.LargeImage")));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminar_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Presione F5 para actualizar listado";
            this.barStaticItem1.Id = 3;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar Excel";
            this.btnExportarExcel.Id = 4;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.LargeImage")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnCostoHora
            // 
            this.btnCostoHora.Caption = "Costo por Hora";
            this.btnCostoHora.Id = 6;
            this.btnCostoHora.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCostoHora.ImageOptions.Image")));
            this.btnCostoHora.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCostoHora.ImageOptions.LargeImage")));
            this.btnCostoHora.Name = "btnCostoHora";
            this.btnCostoHora.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCostoHora_ItemClick);
            // 
            // btnMultipleRegistros
            // 
            this.btnMultipleRegistros.Caption = "Registros Múltiples";
            this.btnMultipleRegistros.Id = 7;
            this.btnMultipleRegistros.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMultipleRegistros.ImageOptions.Image")));
            this.btnMultipleRegistros.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMultipleRegistros.ImageOptions.LargeImage")));
            this.btnMultipleRegistros.Name = "btnMultipleRegistros";
            this.btnMultipleRegistros.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMultipleRegistros_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 8;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimir_ItemClick);
            // 
            // btnGestionActividades
            // 
            this.btnGestionActividades.Caption = "Gestión de Actividades";
            this.btnGestionActividades.Id = 89;
            this.btnGestionActividades.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGestionActividades.ImageOptions.Image")));
            this.btnGestionActividades.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnGestionActividades.ImageOptions.LargeImage")));
            this.btnGestionActividades.Name = "btnGestionActividades";
            this.btnGestionActividades.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGestionActividades_ItemClick);
            // 
            // pageAccGenerales
            // 
            this.pageAccGenerales.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoEdicion,
            this.grupoReportes,
            this.ribbonPageGroup2});
            this.pageAccGenerales.Name = "pageAccGenerales";
            this.pageAccGenerales.Text = "Opciones Control de Horas";
            // 
            // grupoEdicion
            // 
            this.grupoEdicion.ItemLinks.Add(this.btnNuevo);
            this.grupoEdicion.ItemLinks.Add(this.btnEliminar);
            this.grupoEdicion.ItemLinks.Add(this.btnMultipleRegistros);
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
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnCostoHora);
            this.ribbonPageGroup2.ItemLinks.Add(this.btnGestionActividades);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 655);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1126, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Controls.Add(this.chkcbEmpresa);
            this.layoutControl1.Controls.Add(this.dtFechaInicio);
            this.layoutControl1.Controls.Add(this.dtFechaFin);
            this.layoutControl1.Controls.Add(this.btnBuscar);
            this.layoutControl1.Controls.Add(this.chkcbUsuario);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1126, 497);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(8, 65);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtbListado;
            this.xtraTabControl1.Size = new System.Drawing.Size(1110, 424);
            this.xtraTabControl1.TabIndex = 9;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtbListado,
            this.xtbReporte});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtbListado
            // 
            this.xtbListado.Controls.Add(this.layoutControl2);
            this.xtbListado.Name = "xtbListado";
            this.xtbListado.Size = new System.Drawing.Size(1108, 399);
            this.xtbListado.Text = "Listado de Control de horas";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.layoutControl4);
            this.layoutControl2.Controls.Add(this.gcListadoControl);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1108, 399);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // layoutControl4
            // 
            this.layoutControl4.BackColor = System.Drawing.Color.Gray;
            this.layoutControl4.Controls.Add(this.txtTotalHoras);
            this.layoutControl4.Location = new System.Drawing.Point(12, 326);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2636, 299, 650, 400);
            this.layoutControl4.Root = this.layoutControlGroup3;
            this.layoutControl4.Size = new System.Drawing.Size(1084, 61);
            this.layoutControl4.TabIndex = 2;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // txtTotalHoras
            // 
            this.txtTotalHoras.Location = new System.Drawing.Point(301, 16);
            this.txtTotalHoras.MenuManager = this.ribbon;
            this.txtTotalHoras.Name = "txtTotalHoras";
            this.txtTotalHoras.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.txtTotalHoras.Properties.Appearance.Options.UseFont = true;
            this.txtTotalHoras.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalHoras.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalHoras.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtTotalHoras.Size = new System.Drawing.Size(76, 30);
            this.txtTotalHoras.StyleController = this.layoutControl4;
            this.txtTotalHoras.TabIndex = 0;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.simpleLabelItem3,
            this.simpleLabelItem2});
            this.layoutControlGroup3.Name = "Root";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1084, 61);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem6.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem6.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem6.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem6.Control = this.txtTotalHoras;
            this.layoutControlItem6.Location = new System.Drawing.Point(289, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(80, 34);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(80, 34);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem6.Size = new System.Drawing.Size(80, 41);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 6, 2);
            this.layoutControlItem6.Text = "Total de horas por mes :";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem3.Location = new System.Drawing.Point(369, 0);
            this.simpleLabelItem3.MinSize = new System.Drawing.Size(283, 29);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Size = new System.Drawing.Size(695, 41);
            this.simpleLabelItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem3.Text = " ";
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(285, 25);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.White;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem2.MaxSize = new System.Drawing.Size(289, 41);
            this.simpleLabelItem2.MinSize = new System.Drawing.Size(289, 41);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(289, 41);
            this.simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem2.Text = "Total de horas trabajadas :";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(285, 25);
            // 
            // gcListadoControl
            // 
            this.gcListadoControl.DataSource = this.bsListadoControlHoras;
            this.gcListadoControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoControl.Location = new System.Drawing.Point(12, 12);
            this.gcListadoControl.MainView = this.gvListadoControl;
            this.gcListadoControl.Margin = new System.Windows.Forms.Padding(0);
            this.gcListadoControl.MenuManager = this.ribbon;
            this.gcListadoControl.Name = "gcListadoControl";
            this.gcListadoControl.Size = new System.Drawing.Size(1084, 310);
            this.gcListadoControl.TabIndex = 0;
            this.gcListadoControl.UseEmbeddedNavigator = true;
            this.gcListadoControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoControl});
            // 
            // bsListadoControlHoras
            // 
            this.bsListadoControlHoras.DataSource = typeof(BE_BackOffice.eControlHoras);
            // 
            // gvListadoControl
            // 
            this.gvListadoControl.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoControl.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoControl.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoControl.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoControl.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListadoControl.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoControl.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoControl.ColumnPanelRowHeight = 35;
            this.gvListadoControl.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_control_horas,
            this.coldsc_segmento,
            this.coldsc_grupo,
            this.coldsc_actividad,
            this.coldsc_duracion,
            this.colfch_ejecucion,
            this.colcod_usuario,
            this.coldsc_comentario,
            this.coldsc_empresa,
            this.colhoras_empresa,
            this.colhoras_usuario,
            this.colhoras_fecha,
            this.colhoras_fecha_usuario});
            this.gvListadoControl.CustomizationFormBounds = new System.Drawing.Rectangle(2435, 362, 264, 392);
            this.gvListadoControl.GridControl = this.gcListadoControl;
            this.gvListadoControl.GroupCount = 1;
            this.gvListadoControl.Name = "gvListadoControl";
            this.gvListadoControl.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvListadoControl.OptionsBehavior.Editable = false;
            this.gvListadoControl.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoControl.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoControl.OptionsView.ShowGroupPanel = false;
            this.gvListadoControl.OptionsView.ShowGroupPanelColumnsAsSingleRow = true;
            this.gvListadoControl.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colhoras_fecha, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvListadoControl.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoControl_RowClick);
            this.gvListadoControl.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoControl_CustomDrawColumnHeader);
            this.gvListadoControl.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoControl_RowStyle);
            // 
            // colcod_control_horas
            // 
            this.colcod_control_horas.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_control_horas.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_control_horas.Caption = "Código";
            this.colcod_control_horas.FieldName = "cod_control_horas";
            this.colcod_control_horas.Name = "colcod_control_horas";
            this.colcod_control_horas.OptionsColumn.FixedWidth = true;
            this.colcod_control_horas.Width = 80;
            // 
            // coldsc_segmento
            // 
            this.coldsc_segmento.Caption = "Segmento";
            this.coldsc_segmento.FieldName = "dsc_segmento";
            this.coldsc_segmento.Name = "coldsc_segmento";
            this.coldsc_segmento.Visible = true;
            this.coldsc_segmento.VisibleIndex = 3;
            this.coldsc_segmento.Width = 89;
            // 
            // coldsc_grupo
            // 
            this.coldsc_grupo.Caption = "Grupo";
            this.coldsc_grupo.FieldName = "dsc_grupo";
            this.coldsc_grupo.Name = "coldsc_grupo";
            this.coldsc_grupo.Visible = true;
            this.coldsc_grupo.VisibleIndex = 4;
            this.coldsc_grupo.Width = 89;
            // 
            // coldsc_actividad
            // 
            this.coldsc_actividad.Caption = "Actividad";
            this.coldsc_actividad.FieldName = "dsc_actividad";
            this.coldsc_actividad.Name = "coldsc_actividad";
            this.coldsc_actividad.Visible = true;
            this.coldsc_actividad.VisibleIndex = 5;
            this.coldsc_actividad.Width = 96;
            // 
            // coldsc_duracion
            // 
            this.coldsc_duracion.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_duracion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_duracion.Caption = "Duración";
            this.coldsc_duracion.FieldName = "dsc_duracion";
            this.coldsc_duracion.Name = "coldsc_duracion";
            this.coldsc_duracion.OptionsColumn.FixedWidth = true;
            this.coldsc_duracion.Visible = true;
            this.coldsc_duracion.VisibleIndex = 0;
            this.coldsc_duracion.Width = 50;
            // 
            // colfch_ejecucion
            // 
            this.colfch_ejecucion.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_ejecucion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_ejecucion.Caption = "Fec. Ejecución";
            this.colfch_ejecucion.FieldName = "fch_ejecucion";
            this.colfch_ejecucion.Name = "colfch_ejecucion";
            this.colfch_ejecucion.OptionsColumn.FixedWidth = true;
            this.colfch_ejecucion.Width = 100;
            // 
            // colcod_usuario
            // 
            this.colcod_usuario.Caption = "Usuario";
            this.colcod_usuario.FieldName = "dsc_usuario";
            this.colcod_usuario.Name = "colcod_usuario";
            this.colcod_usuario.OptionsColumn.FixedWidth = true;
            this.colcod_usuario.Visible = true;
            this.colcod_usuario.VisibleIndex = 2;
            this.colcod_usuario.Width = 200;
            // 
            // coldsc_comentario
            // 
            this.coldsc_comentario.Caption = "Comentario";
            this.coldsc_comentario.FieldName = "dsc_comentario";
            this.coldsc_comentario.Name = "coldsc_comentario";
            this.coldsc_comentario.OptionsColumn.FixedWidth = true;
            this.coldsc_comentario.Visible = true;
            this.coldsc_comentario.VisibleIndex = 6;
            this.coldsc_comentario.Width = 350;
            // 
            // coldsc_empresa
            // 
            this.coldsc_empresa.Caption = "Cliente";
            this.coldsc_empresa.FieldName = "dsc_empresa";
            this.coldsc_empresa.Name = "coldsc_empresa";
            this.coldsc_empresa.OptionsColumn.FixedWidth = true;
            this.coldsc_empresa.Visible = true;
            this.coldsc_empresa.VisibleIndex = 1;
            this.coldsc_empresa.Width = 185;
            // 
            // colhoras_empresa
            // 
            this.colhoras_empresa.Caption = "Horas Empresa";
            this.colhoras_empresa.FieldName = "horas_empresa";
            this.colhoras_empresa.Name = "colhoras_empresa";
            // 
            // colhoras_usuario
            // 
            this.colhoras_usuario.Caption = "Horas Usuario";
            this.colhoras_usuario.FieldName = "horas_usuario";
            this.colhoras_usuario.Name = "colhoras_usuario";
            // 
            // colhoras_fecha
            // 
            this.colhoras_fecha.Caption = "Fecha Ejecución";
            this.colhoras_fecha.FieldName = "horas_fecha";
            this.colhoras_fecha.Name = "colhoras_fecha";
            this.colhoras_fecha.Visible = true;
            this.colhoras_fecha.VisibleIndex = 7;
            // 
            // colhoras_fecha_usuario
            // 
            this.colhoras_fecha_usuario.Caption = "Usuario";
            this.colhoras_fecha_usuario.FieldName = "horas_fecha_usuario";
            this.colhoras_fecha_usuario.Name = "colhoras_fecha_usuario";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem5});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1108, 399);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1088, 314);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.layoutControl4;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 314);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 65);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(466, 65);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1088, 65);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // xtbReporte
            // 
            this.xtbReporte.Controls.Add(this.layoutControl3);
            this.xtbReporte.Name = "xtbReporte";
            this.xtbReporte.Size = new System.Drawing.Size(1108, 399);
            this.xtbReporte.Text = "Reporte";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.splitContainerControl1);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(1108, 399);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 12);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.layoutControl5);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl6);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1084, 375);
            this.splitContainerControl1.SplitterPosition = 204;
            this.splitContainerControl1.TabIndex = 5;
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.chartControl2);
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Location = new System.Drawing.Point(0, 0);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup4;
            this.layoutControl5.Size = new System.Drawing.Size(1084, 204);
            this.layoutControl5.TabIndex = 0;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // chartControl2
            // 
            this.chartControl2.DataSource = this.bsListadoControlHoras;
            xyDiagram1.AxisX.Title.Text = "Segmentos";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Title.Text = "Horas";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.Rotated = true;
            this.chartControl2.Diagram = xyDiagram1;
            this.chartControl2.Legend.Title.Text = "Grupos";
            this.chartControl2.Legend.Title.Visible = true;
            this.chartControl2.Location = new System.Drawing.Point(0, 0);
            this.chartControl2.Name = "chartControl2";
            this.chartControl2.SeriesDataMember = "dsc_grupo";
            series1.Name = "Horas";
            series1.QualitativeSummaryOptions.SummaryFunction = "SUM([prc_horas])";
            series1.View = stackedBarSeriesView1;
            series1.Visible = false;
            this.chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl2.SeriesTemplate.ArgumentDataMember = "dsc_segmento";
            this.chartControl2.SeriesTemplate.QualitativeSummaryOptions.SummaryFunction = "SUM([cnt_horas])";
            this.chartControl2.SeriesTemplate.SeriesDataMember = "dsc_grupo";
            this.chartControl2.SeriesTemplate.ValueDataMembersSerializable = "cnt_horas";
            this.chartControl2.SeriesTemplate.View = stackedBarSeriesView2;
            this.chartControl2.Size = new System.Drawing.Size(1084, 204);
            this.chartControl2.TabIndex = 4;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(1084, 204);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.chartControl2;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem11.Size = new System.Drawing.Size(1084, 204);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControl6
            // 
            this.layoutControl6.Controls.Add(this.gcDetalleListado);
            this.layoutControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl6.Location = new System.Drawing.Point(0, 0);
            this.layoutControl6.Name = "layoutControl6";
            this.layoutControl6.Root = this.layoutControlGroup5;
            this.layoutControl6.Size = new System.Drawing.Size(1084, 161);
            this.layoutControl6.TabIndex = 0;
            this.layoutControl6.Text = "layoutControl6";
            // 
            // gcDetalleListado
            // 
            this.gcDetalleListado.DataSource = this.bsListadoControlHoras;
            this.gcDetalleListado.Location = new System.Drawing.Point(2, 2);
            this.gcDetalleListado.MainView = this.gvDetalleListado;
            this.gcDetalleListado.MenuManager = this.ribbon;
            this.gcDetalleListado.Name = "gcDetalleListado";
            this.gcDetalleListado.Size = new System.Drawing.Size(1080, 157);
            this.gcDetalleListado.TabIndex = 0;
            this.gcDetalleListado.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetalleListado});
            // 
            // gvDetalleListado
            // 
            this.gvDetalleListado.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvDetalleListado.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvDetalleListado.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvDetalleListado.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvDetalleListado.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvDetalleListado.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gvDetalleListado.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvDetalleListado.ColumnPanelRowHeight = 35;
            this.gvDetalleListado.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_usuario,
            this.coldsc_empresa1,
            this.coldsc_segmento1,
            this.coldsc_grupo1,
            this.coldsc_actividad1,
            this.colcnt_horas,
            this.colprc_horas,
            this.colhoras_empresa1,
            this.colhoras_usuario1,
            this.coldsc_duracion1,
            this.colfch_ejecucion1});
            this.gvDetalleListado.CustomizationFormBounds = new System.Drawing.Rectangle(2423, 350, 264, 392);
            this.gvDetalleListado.GridControl = this.gcDetalleListado;
            this.gvDetalleListado.GroupCount = 2;
            this.gvDetalleListado.Name = "gvDetalleListado";
            this.gvDetalleListado.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvDetalleListado.OptionsBehavior.Editable = false;
            this.gvDetalleListado.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDetalleListado.OptionsView.ShowAutoFilterRow = true;
            this.gvDetalleListado.OptionsView.ShowGroupPanel = false;
            this.gvDetalleListado.OptionsView.ShowGroupPanelColumnsAsSingleRow = true;
            this.gvDetalleListado.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colhoras_empresa1, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colhoras_usuario1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvDetalleListado.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvDetalleListado_RowClick);
            this.gvDetalleListado.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvDetalleListado_CustomDrawColumnHeader);
            this.gvDetalleListado.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvDetalleListado_RowStyle);
            // 
            // coldsc_usuario
            // 
            this.coldsc_usuario.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_usuario.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_usuario.Caption = "Usuario";
            this.coldsc_usuario.FieldName = "dsc_usuario";
            this.coldsc_usuario.Name = "coldsc_usuario";
            this.coldsc_usuario.Width = 147;
            // 
            // coldsc_empresa1
            // 
            this.coldsc_empresa1.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_empresa1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_empresa1.Caption = "Cliente";
            this.coldsc_empresa1.FieldName = "dsc_empresa";
            this.coldsc_empresa1.Name = "coldsc_empresa1";
            this.coldsc_empresa1.Width = 279;
            // 
            // coldsc_segmento1
            // 
            this.coldsc_segmento1.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_segmento1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_segmento1.Caption = "Segmento";
            this.coldsc_segmento1.FieldName = "dsc_segmento";
            this.coldsc_segmento1.Name = "coldsc_segmento1";
            this.coldsc_segmento1.Visible = true;
            this.coldsc_segmento1.VisibleIndex = 1;
            this.coldsc_segmento1.Width = 273;
            // 
            // coldsc_grupo1
            // 
            this.coldsc_grupo1.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_grupo1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_grupo1.Caption = "Grupo";
            this.coldsc_grupo1.FieldName = "dsc_grupo";
            this.coldsc_grupo1.Name = "coldsc_grupo1";
            this.coldsc_grupo1.Visible = true;
            this.coldsc_grupo1.VisibleIndex = 2;
            this.coldsc_grupo1.Width = 220;
            // 
            // coldsc_actividad1
            // 
            this.coldsc_actividad1.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_actividad1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_actividad1.Caption = "Actividad";
            this.coldsc_actividad1.FieldName = "dsc_actividad";
            this.coldsc_actividad1.Name = "coldsc_actividad1";
            this.coldsc_actividad1.Visible = true;
            this.coldsc_actividad1.VisibleIndex = 3;
            this.coldsc_actividad1.Width = 370;
            // 
            // colcnt_horas
            // 
            this.colcnt_horas.AppearanceCell.Options.UseTextOptions = true;
            this.colcnt_horas.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcnt_horas.AppearanceHeader.Options.UseTextOptions = true;
            this.colcnt_horas.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcnt_horas.Caption = "Horas";
            this.colcnt_horas.FieldName = "cnt_horas";
            this.colcnt_horas.Name = "colcnt_horas";
            this.colcnt_horas.Visible = true;
            this.colcnt_horas.VisibleIndex = 4;
            this.colcnt_horas.Width = 55;
            // 
            // colprc_horas
            // 
            this.colprc_horas.AppearanceCell.Options.UseTextOptions = true;
            this.colprc_horas.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colprc_horas.AppearanceHeader.Options.UseTextOptions = true;
            this.colprc_horas.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colprc_horas.Caption = "Valor";
            this.colprc_horas.FieldName = "prc_horas";
            this.colprc_horas.Name = "colprc_horas";
            this.colprc_horas.Visible = true;
            this.colprc_horas.VisibleIndex = 5;
            this.colprc_horas.Width = 65;
            // 
            // colhoras_empresa1
            // 
            this.colhoras_empresa1.Caption = "Empresa";
            this.colhoras_empresa1.FieldName = "horas_empresa";
            this.colhoras_empresa1.Name = "colhoras_empresa1";
            this.colhoras_empresa1.Visible = true;
            this.colhoras_empresa1.VisibleIndex = 3;
            // 
            // colhoras_usuario1
            // 
            this.colhoras_usuario1.Caption = "Usuario";
            this.colhoras_usuario1.FieldName = "horas_usuario";
            this.colhoras_usuario1.Name = "colhoras_usuario1";
            this.colhoras_usuario1.Visible = true;
            this.colhoras_usuario1.VisibleIndex = 4;
            // 
            // coldsc_duracion1
            // 
            this.coldsc_duracion1.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_duracion1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_duracion1.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_duracion1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_duracion1.Caption = "Duración";
            this.coldsc_duracion1.FieldName = "dsc_duracion";
            this.coldsc_duracion1.Name = "coldsc_duracion1";
            this.coldsc_duracion1.Visible = true;
            this.coldsc_duracion1.VisibleIndex = 0;
            this.coldsc_duracion1.Width = 55;
            // 
            // colfch_ejecucion1
            // 
            this.colfch_ejecucion1.Caption = "Fecha Ejecución";
            this.colfch_ejecucion1.FieldName = "fch_ejecucion";
            this.colfch_ejecucion1.Name = "colfch_ejecucion1";
            this.colfch_ejecucion1.OptionsColumn.FixedWidth = true;
            this.colfch_ejecucion1.Visible = true;
            this.colfch_ejecucion1.VisibleIndex = 6;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(1084, 161);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcDetalleListado;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1084, 161);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1108, 399);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.splitContainerControl1;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(1088, 379);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // chkcbEmpresa
            // 
            this.chkcbEmpresa.Location = new System.Drawing.Point(63, 34);
            this.chkcbEmpresa.MenuManager = this.ribbon;
            this.chkcbEmpresa.Name = "chkcbEmpresa";
            this.chkcbEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcbEmpresa.Size = new System.Drawing.Size(279, 20);
            this.chkcbEmpresa.StyleController = this.layoutControl1;
            this.chkcbEmpresa.TabIndex = 8;
            // 
            // dtFechaInicio
            // 
            this.dtFechaInicio.EditValue = null;
            this.dtFechaInicio.Location = new System.Drawing.Point(618, 34);
            this.dtFechaInicio.Name = "dtFechaInicio";
            this.dtFechaInicio.Properties.Appearance.Options.UseTextOptions = true;
            this.dtFechaInicio.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtFechaInicio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaInicio.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaInicio.Size = new System.Drawing.Size(103, 20);
            this.dtFechaInicio.StyleController = this.layoutControl1;
            this.dtFechaInicio.TabIndex = 6;
            // 
            // dtFechaFin
            // 
            this.dtFechaFin.EditValue = null;
            this.dtFechaFin.Location = new System.Drawing.Point(765, 34);
            this.dtFechaFin.Name = "dtFechaFin";
            this.dtFechaFin.Properties.Appearance.Options.UseTextOptions = true;
            this.dtFechaFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtFechaFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaFin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaFin.Size = new System.Drawing.Size(103, 20);
            this.dtFechaFin.StyleController = this.layoutControl1;
            this.dtFechaFin.TabIndex = 7;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.ImageOptions.Image")));
            this.btnBuscar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(920, 30);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(94, 31);
            this.btnBuscar.StyleController = this.layoutControl1;
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // chkcbUsuario
            // 
            this.chkcbUsuario.EditValue = "";
            this.chkcbUsuario.Location = new System.Drawing.Point(413, 34);
            this.chkcbUsuario.MenuManager = this.ribbon;
            this.chkcbUsuario.Name = "chkcbUsuario";
            this.chkcbUsuario.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcbUsuario.Properties.NullText = "Todos";
            this.chkcbUsuario.Size = new System.Drawing.Size(161, 20);
            this.chkcbUsuario.StyleController = this.layoutControl1;
            this.chkcbUsuario.TabIndex = 10;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleLabelItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem23,
            this.layoutControlItem7,
            this.layoutControlItem9,
            this.emptySpaceItem4,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem8});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(1126, 497);
            this.Root.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(1114, 22);
            this.simpleLabelItem1.Text = "Filtros de Búsqueda";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(147, 18);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(1010, 22);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(104, 35);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(338, 22);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(19, 35);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(19, 35);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(19, 35);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.dtFechaInicio;
            this.layoutControlItem23.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem23.CustomizationFormText = "Desde";
            this.layoutControlItem23.Location = new System.Drawing.Point(570, 22);
            this.layoutControlItem23.MaxSize = new System.Drawing.Size(147, 28);
            this.layoutControlItem23.MinSize = new System.Drawing.Size(147, 28);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 6, 2);
            this.layoutControlItem23.Size = new System.Drawing.Size(147, 35);
            this.layoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem23.Text = "Desde :";
            this.layoutControlItem23.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem23.TextSize = new System.Drawing.Size(35, 13);
            this.layoutControlItem23.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.dtFechaFin;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "Hasta :";
            this.layoutControlItem7.Location = new System.Drawing.Point(717, 22);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 6, 2);
            this.layoutControlItem7.Size = new System.Drawing.Size(147, 35);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "Hasta :";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(35, 20);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnBuscar;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem9.Location = new System.Drawing.Point(912, 22);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(98, 35);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(98, 35);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(98, 35);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "layoutControlItem7";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(864, 22);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(48, 35);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(48, 35);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(48, 35);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkcbEmpresa;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 22);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(338, 28);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(338, 28);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 6, 2);
            this.layoutControlItem3.Size = new System.Drawing.Size(338, 35);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Empresa :";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(50, 20);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.xtraTabControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 57);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1114, 428);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.chkcbUsuario;
            this.layoutControlItem8.Location = new System.Drawing.Point(357, 22);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 6, 2);
            this.layoutControlItem8.Size = new System.Drawing.Size(213, 35);
            this.layoutControlItem8.Text = "Usuario :";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(43, 13);
            this.layoutControlItem8.TextToControlDistance = 5;
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // frmListaControlHoras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 679);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmListaControlHoras";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Listado Control Horas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaControlHoras_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListaControlHoras_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtbListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalHoras.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoControlHoras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.xtbReporte.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).EndInit();
            this.layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDetalleListado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetalleListado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcbEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaInicio.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaInicio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaFin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcbUsuario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanChartRangeControlClient1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage pageAccGenerales;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoEdicion;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcListadoControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private System.Windows.Forms.BindingSource bsListadoControlHoras;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_control_horas;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_usuario;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_comentario;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_duracion;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_ejecucion;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.DateEdit dtFechaInicio;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraEditors.DateEdit dtFechaFin;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_segmento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_grupo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_actividad;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresa;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem btnCostoHora;
        private DevExpress.XtraBars.BarButtonItem btnMultipleRegistros;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkcbEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TimeSpanChartRangeControlClient timeSpanChartRangeControlClient1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtbListado;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTab.XtraTabPage xtbReporte;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraEditors.TextEdit txtTotalHoras;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControl layoutControl6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraCharts.ChartControl chartControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraGrid.GridControl gcDetalleListado;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetalleListado;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_segmento1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_grupo1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_actividad1;
        private DevExpress.XtraGrid.Columns.GridColumn colprc_horas;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresa1;
        private DevExpress.XtraGrid.Columns.GridColumn colcnt_horas;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkcbUsuario;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colhoras_empresa;
        private DevExpress.XtraGrid.Columns.GridColumn colhoras_usuario;
        private DevExpress.XtraGrid.Columns.GridColumn colhoras_empresa1;
        private DevExpress.XtraGrid.Columns.GridColumn colhoras_usuario1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_duracion1;
        private DevExpress.XtraGrid.Columns.GridColumn colhoras_fecha;
        private DevExpress.XtraGrid.Columns.GridColumn colhoras_fecha_usuario;
        private DevExpress.XtraBars.BarButtonItem btnGestionActividades;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_ejecucion1;
    }
}