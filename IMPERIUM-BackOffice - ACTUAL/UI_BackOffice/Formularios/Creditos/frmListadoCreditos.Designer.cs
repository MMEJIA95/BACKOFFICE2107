
namespace UI_BackOffice.Formularios.Creditos
{
    partial class frmListadoCreditos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoCreditos));
            DevExpress.XtraPivotGrid.PivotGridGroup pivotGridGroup1 = new DevExpress.XtraPivotGrid.PivotGridGroup();
            DevExpress.XtraPivotGrid.PivotGridGroup pivotGridGroup2 = new DevExpress.XtraPivotGrid.PivotGridGroup();
            this.fielddsctitular = new DevExpress.XtraPivotGrid.PivotGridField();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNuevoCliente = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.btnSimuladorCuotasFijas = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportarPagosCOFIDE = new DevExpress.XtraBars.BarButtonItem();
            this.btnListadoPagos = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportarPagosBBVA = new DevExpress.XtraBars.BarButtonItem();
            this.btnAplicarPagosPendientes = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminarTodosPagos = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminarCredito = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoAcciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtabListadoCreditos = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoCronogramaCabecera = new DevExpress.XtraGrid.GridControl();
            this.bsListadoCreditos = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoCronogramaCabecera = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSemaforo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_credito = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_titular = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nombres_completos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_placa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_Capital = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colfch_desembolso = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_cuotas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_diapago = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_tasaanual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtPorcentaje = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colnum_tasamensual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_tasaTIRanual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_tasaTIRM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtPorcentaje2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colimp_capitalvigente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_montopagado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_capitalatrasado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_interesatrasado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_montoatrasado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_cuotavigente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtNumero = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtabResumenFacturar = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.pivotResumenFacturar = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.bsResumenIntereses = new System.Windows.Forms.BindingSource(this.components);
            this.fieldcodcredito = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fielddscnombrescompletos = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldnumdiapago = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fielddscmes = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldimpinteres = new DevExpress.XtraPivotGrid.PivotGridField();
            this.rtxtImporteInteres = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.fielddscanho = new DevExpress.XtraPivotGrid.PivotGridField();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtabResumenPagos = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.pivotResumenPagos = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.bsResumenPagos = new System.Windows.Forms.BindingSource(this.components);
            this.fielddscdestino = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fielddsccliente = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldcodcredito1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldnumplaca = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldimpcapital = new DevExpress.XtraPivotGrid.PivotGridField();
            this.rtxtImporteInteres2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.pivotGridField6 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldimpigv = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldimptotal = new DevExpress.XtraPivotGrid.PivotGridField();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.dtFechaDesembolsoFin = new DevExpress.XtraEditors.DateEdit();
            this.dtFechaDesembolsoInicio = new DevExpress.XtraEditors.DateEdit();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.dtFechaProximoPago = new DevExpress.XtraEditors.DateEdit();
            this.chkFechaDesembolso = new DevExpress.XtraEditors.CheckEdit();
            this.chkFechaProximoPago = new DevExpress.XtraEditors.CheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.fieldfchdeposito = new DevExpress.XtraPivotGrid.PivotGridField();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtabListadoCreditos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoCronogramaCabecera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCreditos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoCronogramaCabecera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtNumero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.xtabResumenFacturar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotResumenFacturar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsResumenIntereses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporteInteres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.xtabResumenPagos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotResumenPagos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsResumenPagos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporteInteres2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolsoFin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolsoFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolsoInicio.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolsoInicio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaProximoPago.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaProximoPago.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFechaDesembolso.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFechaProximoPago.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // fielddsctitular
            // 
            this.fielddsctitular.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fielddsctitular.AreaIndex = 1;
            this.fielddsctitular.Caption = "Titular";
            this.fielddsctitular.FieldName = "dsc_titular";
            this.fielddsctitular.Name = "fielddsctitular";
            this.fielddsctitular.Width = 220;
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnNuevoCliente,
            this.btnExportarExcel,
            this.btnImprimir,
            this.btnSimuladorCuotasFijas,
            this.btnImportarPagosCOFIDE,
            this.btnListadoPagos,
            this.btnImportarPagosBBVA,
            this.btnAplicarPagosPendientes,
            this.btnEliminarTodosPagos,
            this.btnEliminarCredito});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 11;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1366, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnNuevoCliente
            // 
            this.btnNuevoCliente.Caption = "Nuevo Cliente";
            this.btnNuevoCliente.Id = 1;
            this.btnNuevoCliente.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.prestamo_a_valor;
            this.btnNuevoCliente.Name = "btnNuevoCliente";
            this.btnNuevoCliente.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevoCliente.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevoCliente_ItemClick);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar Excel";
            this.btnExportarExcel.Id = 2;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 3;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimir_ItemClick);
            // 
            // btnSimuladorCuotasFijas
            // 
            this.btnSimuladorCuotasFijas.Caption = "Siomulador con Cuotas Fijas";
            this.btnSimuladorCuotasFijas.Id = 4;
            this.btnSimuladorCuotasFijas.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSimuladorCuotasFijas.ImageOptions.Image")));
            this.btnSimuladorCuotasFijas.Name = "btnSimuladorCuotasFijas";
            this.btnSimuladorCuotasFijas.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnSimuladorCuotasFijas.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSimuladorCuotasFijas_ItemClick);
            // 
            // btnImportarPagosCOFIDE
            // 
            this.btnImportarPagosCOFIDE.Caption = "Importar Pagos COFIDE";
            this.btnImportarPagosCOFIDE.Id = 5;
            this.btnImportarPagosCOFIDE.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.bill__1_;
            this.btnImportarPagosCOFIDE.Name = "btnImportarPagosCOFIDE";
            this.btnImportarPagosCOFIDE.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImportarPagosCOFIDE.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportarPagosCOFIDE_ItemClick);
            // 
            // btnListadoPagos
            // 
            this.btnListadoPagos.Caption = "Listado de Pagos";
            this.btnListadoPagos.Id = 6;
            this.btnListadoPagos.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnListadoPagos.ImageOptions.Image")));
            this.btnListadoPagos.Name = "btnListadoPagos";
            this.btnListadoPagos.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnListadoPagos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnListadoPagos_ItemClick);
            // 
            // btnImportarPagosBBVA
            // 
            this.btnImportarPagosBBVA.Caption = "Importar Pagos BBVA";
            this.btnImportarPagosBBVA.Id = 7;
            this.btnImportarPagosBBVA.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportarPagosBBVA.ImageOptions.Image")));
            this.btnImportarPagosBBVA.Name = "btnImportarPagosBBVA";
            this.btnImportarPagosBBVA.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImportarPagosBBVA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportarPagosBBVA_ItemClick);
            // 
            // btnAplicarPagosPendientes
            // 
            this.btnAplicarPagosPendientes.Caption = "Aplicar Pagos Pendientes";
            this.btnAplicarPagosPendientes.Id = 8;
            this.btnAplicarPagosPendientes.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAplicarPagosPendientes.ImageOptions.Image")));
            this.btnAplicarPagosPendientes.Name = "btnAplicarPagosPendientes";
            this.btnAplicarPagosPendientes.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAplicarPagosPendientes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAplicarPagosPendientes_ItemClick);
            // 
            // btnEliminarTodosPagos
            // 
            this.btnEliminarTodosPagos.Caption = "Eliminar todos los pagos";
            this.btnEliminarTodosPagos.Id = 9;
            this.btnEliminarTodosPagos.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarTodosPagos.ImageOptions.Image")));
            this.btnEliminarTodosPagos.Name = "btnEliminarTodosPagos";
            this.btnEliminarTodosPagos.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEliminarTodosPagos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminarTodosPagos_ItemClick);
            // 
            // btnEliminarCredito
            // 
            this.btnEliminarCredito.Caption = "Eliminar crédito";
            this.btnEliminarCredito.Id = 10;
            this.btnEliminarCredito.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarCredito.ImageOptions.Image")));
            this.btnEliminarCredito.Name = "btnEliminarCredito";
            this.btnEliminarCredito.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEliminarCredito.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminarCredito_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoAcciones,
            this.grupoReportes});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones Listado de Créditos";
            // 
            // grupoAcciones
            // 
            this.grupoAcciones.ItemLinks.Add(this.btnNuevoCliente);
            this.grupoAcciones.ItemLinks.Add(this.btnSimuladorCuotasFijas);
            this.grupoAcciones.ItemLinks.Add(this.btnImportarPagosCOFIDE);
            this.grupoAcciones.ItemLinks.Add(this.btnImportarPagosBBVA);
            this.grupoAcciones.ItemLinks.Add(this.btnListadoPagos);
            this.grupoAcciones.ItemLinks.Add(this.btnAplicarPagosPendientes);
            this.grupoAcciones.ItemLinks.Add(this.btnEliminarTodosPagos);
            this.grupoAcciones.ItemLinks.Add(this.btnEliminarCredito);
            this.grupoAcciones.Name = "grupoAcciones";
            this.grupoAcciones.Text = "Acciones";
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
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 812);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1366, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Controls.Add(this.dtFechaDesembolsoFin);
            this.layoutControl1.Controls.Add(this.dtFechaDesembolsoInicio);
            this.layoutControl1.Controls.Add(this.btnBuscar);
            this.layoutControl1.Controls.Add(this.dtFechaProximoPago);
            this.layoutControl1.Controls.Add(this.chkFechaDesembolso);
            this.layoutControl1.Controls.Add(this.chkFechaProximoPago);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1366, 654);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 120);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtabListadoCreditos;
            this.xtraTabControl1.Size = new System.Drawing.Size(1342, 522);
            this.xtraTabControl1.TabIndex = 5;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtabListadoCreditos,
            this.xtabResumenFacturar,
            this.xtabResumenPagos});
            // 
            // xtabListadoCreditos
            // 
            this.xtabListadoCreditos.Controls.Add(this.layoutControl2);
            this.xtabListadoCreditos.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.chartsshowlegend_16x16;
            this.xtabListadoCreditos.Name = "xtabListadoCreditos";
            this.xtabListadoCreditos.Size = new System.Drawing.Size(1340, 494);
            this.xtabListadoCreditos.Text = "Listado de Créditos";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcListadoCronogramaCabecera);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1340, 494);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcListadoCronogramaCabecera
            // 
            this.gcListadoCronogramaCabecera.DataSource = this.bsListadoCreditos;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoCronogramaCabecera.Location = new System.Drawing.Point(12, 12);
            this.gcListadoCronogramaCabecera.MainView = this.gvListadoCronogramaCabecera;
            this.gcListadoCronogramaCabecera.Name = "gcListadoCronogramaCabecera";
            this.gcListadoCronogramaCabecera.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporte,
            this.rtxtPorcentaje,
            this.rtxtNumero,
            this.rtxtPorcentaje2});
            this.gcListadoCronogramaCabecera.Size = new System.Drawing.Size(1316, 470);
            this.gcListadoCronogramaCabecera.TabIndex = 0;
            this.gcListadoCronogramaCabecera.UseEmbeddedNavigator = true;
            this.gcListadoCronogramaCabecera.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoCronogramaCabecera});
            // 
            // bsListadoCreditos
            // 
            this.bsListadoCreditos.DataSource = typeof(BE_BackOffice.eCreditoVehicular.eCronogramaCabecera);
            // 
            // gvListadoCronogramaCabecera
            // 
            this.gvListadoCronogramaCabecera.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoCronogramaCabecera.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoCronogramaCabecera.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoCronogramaCabecera.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoCronogramaCabecera.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListadoCronogramaCabecera.ColumnPanelRowHeight = 35;
            this.gvListadoCronogramaCabecera.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSemaforo,
            this.colcod_credito,
            this.coldsc_titular,
            this.coldsc_nombres_completos,
            this.colnum_placa,
            this.colimp_Capital,
            this.colfch_desembolso,
            this.colnum_cuotas,
            this.colnum_diapago,
            this.colnum_tasaanual,
            this.colnum_tasamensual,
            this.colnum_tasaTIRanual,
            this.colnum_tasaTIRM,
            this.colimp_capitalvigente,
            this.colimp_montopagado,
            this.colimp_capitalatrasado,
            this.colimp_interesatrasado,
            this.colimp_montoatrasado,
            this.colimp_cuotavigente});
            this.gvListadoCronogramaCabecera.GridControl = this.gcListadoCronogramaCabecera;
            this.gvListadoCronogramaCabecera.Name = "gvListadoCronogramaCabecera";
            this.gvListadoCronogramaCabecera.OptionsBehavior.Editable = false;
            this.gvListadoCronogramaCabecera.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoCronogramaCabecera.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoCronogramaCabecera.OptionsView.ShowFooter = true;
            this.gvListadoCronogramaCabecera.OptionsView.ShowIndicator = false;
            this.gvListadoCronogramaCabecera.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoCronogramaCabecera_RowClick);
            this.gvListadoCronogramaCabecera.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoCronogramaCabecera_CustomDrawColumnHeader);
            this.gvListadoCronogramaCabecera.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListadoCronogramaCabecera_CustomDrawCell);
            this.gvListadoCronogramaCabecera.CustomDrawFooterCell += new DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventHandler(this.gvListadoCronogramaCabecera_CustomDrawFooterCell);
            this.gvListadoCronogramaCabecera.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoCronogramaCabecera_RowStyle);
            // 
            // colSemaforo
            // 
            this.colSemaforo.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 1F);
            this.colSemaforo.AppearanceCell.ForeColor = System.Drawing.Color.Transparent;
            this.colSemaforo.AppearanceCell.Options.UseFont = true;
            this.colSemaforo.AppearanceCell.Options.UseForeColor = true;
            this.colSemaforo.AppearanceCell.Options.UseTextOptions = true;
            this.colSemaforo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSemaforo.Caption = " ";
            this.colSemaforo.FieldName = "flg_semaforo";
            this.colSemaforo.Name = "colSemaforo";
            this.colSemaforo.OptionsColumn.FixedWidth = true;
            this.colSemaforo.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colSemaforo.Visible = true;
            this.colSemaforo.VisibleIndex = 0;
            this.colSemaforo.Width = 30;
            // 
            // colcod_credito
            // 
            this.colcod_credito.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_credito.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_credito.Caption = "Cod. Crédito";
            this.colcod_credito.FieldName = "cod_credito";
            this.colcod_credito.Name = "colcod_credito";
            this.colcod_credito.OptionsColumn.FixedWidth = true;
            this.colcod_credito.Visible = true;
            this.colcod_credito.VisibleIndex = 1;
            this.colcod_credito.Width = 50;
            // 
            // coldsc_titular
            // 
            this.coldsc_titular.Caption = "Titular";
            this.coldsc_titular.FieldName = "dsc_titular";
            this.coldsc_titular.Name = "coldsc_titular";
            this.coldsc_titular.OptionsColumn.FixedWidth = true;
            this.coldsc_titular.Visible = true;
            this.coldsc_titular.VisibleIndex = 2;
            this.coldsc_titular.Width = 200;
            // 
            // coldsc_nombres_completos
            // 
            this.coldsc_nombres_completos.Caption = "Cliente";
            this.coldsc_nombres_completos.FieldName = "dsc_nombres_completos";
            this.coldsc_nombres_completos.Name = "coldsc_nombres_completos";
            this.coldsc_nombres_completos.OptionsColumn.FixedWidth = true;
            this.coldsc_nombres_completos.Visible = true;
            this.coldsc_nombres_completos.VisibleIndex = 3;
            this.coldsc_nombres_completos.Width = 200;
            // 
            // colnum_placa
            // 
            this.colnum_placa.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_placa.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_placa.Caption = "N° Placa";
            this.colnum_placa.FieldName = "num_placa";
            this.colnum_placa.Name = "colnum_placa";
            this.colnum_placa.OptionsColumn.FixedWidth = true;
            this.colnum_placa.Visible = true;
            this.colnum_placa.VisibleIndex = 4;
            this.colnum_placa.Width = 80;
            // 
            // colimp_Capital
            // 
            this.colimp_Capital.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colimp_Capital.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.colimp_Capital.AppearanceCell.Options.UseFont = true;
            this.colimp_Capital.AppearanceCell.Options.UseForeColor = true;
            this.colimp_Capital.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_Capital.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_Capital.Caption = "Monto Capital";
            this.colimp_Capital.ColumnEdit = this.rtxtImporte;
            this.colimp_Capital.FieldName = "imp_Capital";
            this.colimp_Capital.Name = "colimp_Capital";
            this.colimp_Capital.OptionsColumn.FixedWidth = true;
            this.colimp_Capital.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_Capital", "{0:#,#.##}")});
            this.colimp_Capital.Visible = true;
            this.colimp_Capital.VisibleIndex = 5;
            this.colimp_Capital.Width = 100;
            // 
            // rtxtImporte
            // 
            this.rtxtImporte.AutoHeight = false;
            this.rtxtImporte.Mask.EditMask = "c2";
            this.rtxtImporte.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtImporte.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporte.Name = "rtxtImporte";
            // 
            // colfch_desembolso
            // 
            this.colfch_desembolso.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_desembolso.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_desembolso.Caption = "Fecha Desembolso";
            this.colfch_desembolso.FieldName = "fch_desembolso";
            this.colfch_desembolso.Name = "colfch_desembolso";
            this.colfch_desembolso.OptionsColumn.FixedWidth = true;
            this.colfch_desembolso.Visible = true;
            this.colfch_desembolso.VisibleIndex = 6;
            this.colfch_desembolso.Width = 90;
            // 
            // colnum_cuotas
            // 
            this.colnum_cuotas.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_cuotas.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_cuotas.Caption = "# cuotas";
            this.colnum_cuotas.FieldName = "num_cuotas";
            this.colnum_cuotas.Name = "colnum_cuotas";
            this.colnum_cuotas.OptionsColumn.FixedWidth = true;
            this.colnum_cuotas.Visible = true;
            this.colnum_cuotas.VisibleIndex = 7;
            this.colnum_cuotas.Width = 70;
            // 
            // colnum_diapago
            // 
            this.colnum_diapago.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_diapago.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_diapago.Caption = "Días pago";
            this.colnum_diapago.FieldName = "num_diapago";
            this.colnum_diapago.Name = "colnum_diapago";
            this.colnum_diapago.OptionsColumn.FixedWidth = true;
            this.colnum_diapago.Visible = true;
            this.colnum_diapago.VisibleIndex = 8;
            this.colnum_diapago.Width = 60;
            // 
            // colnum_tasaanual
            // 
            this.colnum_tasaanual.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_tasaanual.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_tasaanual.Caption = "Tasa Anual";
            this.colnum_tasaanual.ColumnEdit = this.rtxtPorcentaje;
            this.colnum_tasaanual.FieldName = "num_tasaanual";
            this.colnum_tasaanual.Name = "colnum_tasaanual";
            this.colnum_tasaanual.OptionsColumn.FixedWidth = true;
            this.colnum_tasaanual.Visible = true;
            this.colnum_tasaanual.VisibleIndex = 9;
            this.colnum_tasaanual.Width = 60;
            // 
            // rtxtPorcentaje
            // 
            this.rtxtPorcentaje.AutoHeight = false;
            this.rtxtPorcentaje.Mask.EditMask = "p2";
            this.rtxtPorcentaje.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtPorcentaje.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtPorcentaje.Name = "rtxtPorcentaje";
            // 
            // colnum_tasamensual
            // 
            this.colnum_tasamensual.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_tasamensual.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_tasamensual.Caption = "Tasa Mensual";
            this.colnum_tasamensual.ColumnEdit = this.rtxtPorcentaje;
            this.colnum_tasamensual.FieldName = "num_tasamensual";
            this.colnum_tasamensual.Name = "colnum_tasamensual";
            this.colnum_tasamensual.OptionsColumn.FixedWidth = true;
            this.colnum_tasamensual.Visible = true;
            this.colnum_tasamensual.VisibleIndex = 10;
            this.colnum_tasamensual.Width = 60;
            // 
            // colnum_tasaTIRanual
            // 
            this.colnum_tasaTIRanual.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_tasaTIRanual.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_tasaTIRanual.Caption = "TIR Anual";
            this.colnum_tasaTIRanual.ColumnEdit = this.rtxtPorcentaje;
            this.colnum_tasaTIRanual.FieldName = "num_tasaTIRanual";
            this.colnum_tasaTIRanual.Name = "colnum_tasaTIRanual";
            this.colnum_tasaTIRanual.OptionsColumn.FixedWidth = true;
            this.colnum_tasaTIRanual.Visible = true;
            this.colnum_tasaTIRanual.VisibleIndex = 11;
            this.colnum_tasaTIRanual.Width = 60;
            // 
            // colnum_tasaTIRM
            // 
            this.colnum_tasaTIRM.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_tasaTIRM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_tasaTIRM.Caption = "TIRM";
            this.colnum_tasaTIRM.ColumnEdit = this.rtxtPorcentaje2;
            this.colnum_tasaTIRM.FieldName = "num_tasaTIRM";
            this.colnum_tasaTIRM.Name = "colnum_tasaTIRM";
            this.colnum_tasaTIRM.OptionsColumn.FixedWidth = true;
            this.colnum_tasaTIRM.Visible = true;
            this.colnum_tasaTIRM.VisibleIndex = 12;
            this.colnum_tasaTIRM.Width = 60;
            // 
            // rtxtPorcentaje2
            // 
            this.rtxtPorcentaje2.AutoHeight = false;
            this.rtxtPorcentaje2.Mask.EditMask = "p3";
            this.rtxtPorcentaje2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtPorcentaje2.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtPorcentaje2.Name = "rtxtPorcentaje2";
            // 
            // colimp_capitalvigente
            // 
            this.colimp_capitalvigente.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colimp_capitalvigente.AppearanceCell.Options.UseFont = true;
            this.colimp_capitalvigente.Caption = "Capital Vigente";
            this.colimp_capitalvigente.ColumnEdit = this.rtxtImporte;
            this.colimp_capitalvigente.FieldName = "imp_capitalvigente";
            this.colimp_capitalvigente.Name = "colimp_capitalvigente";
            this.colimp_capitalvigente.OptionsColumn.FixedWidth = true;
            this.colimp_capitalvigente.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_capitalvigente", "{0:#,#.##}")});
            this.colimp_capitalvigente.Visible = true;
            this.colimp_capitalvigente.VisibleIndex = 13;
            this.colimp_capitalvigente.Width = 100;
            // 
            // colimp_montopagado
            // 
            this.colimp_montopagado.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colimp_montopagado.AppearanceCell.ForeColor = System.Drawing.Color.DarkGreen;
            this.colimp_montopagado.AppearanceCell.Options.UseFont = true;
            this.colimp_montopagado.AppearanceCell.Options.UseForeColor = true;
            this.colimp_montopagado.Caption = "Monto Pagado";
            this.colimp_montopagado.ColumnEdit = this.rtxtImporte;
            this.colimp_montopagado.FieldName = "imp_montopagado";
            this.colimp_montopagado.Name = "colimp_montopagado";
            this.colimp_montopagado.OptionsColumn.FixedWidth = true;
            this.colimp_montopagado.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_montopagado", "{0:#,#.##}")});
            this.colimp_montopagado.Visible = true;
            this.colimp_montopagado.VisibleIndex = 14;
            this.colimp_montopagado.Width = 100;
            // 
            // colimp_capitalatrasado
            // 
            this.colimp_capitalatrasado.Caption = "Capital Atrasado";
            this.colimp_capitalatrasado.ColumnEdit = this.rtxtImporte;
            this.colimp_capitalatrasado.FieldName = "imp_capitalatrasado";
            this.colimp_capitalatrasado.Name = "colimp_capitalatrasado";
            this.colimp_capitalatrasado.OptionsColumn.FixedWidth = true;
            this.colimp_capitalatrasado.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_capitalatrasado", "{0:#,#.##}")});
            this.colimp_capitalatrasado.Visible = true;
            this.colimp_capitalatrasado.VisibleIndex = 15;
            this.colimp_capitalatrasado.Width = 100;
            // 
            // colimp_interesatrasado
            // 
            this.colimp_interesatrasado.Caption = "Interes Atrasado";
            this.colimp_interesatrasado.ColumnEdit = this.rtxtImporte;
            this.colimp_interesatrasado.FieldName = "imp_interesatrasado";
            this.colimp_interesatrasado.Name = "colimp_interesatrasado";
            this.colimp_interesatrasado.OptionsColumn.FixedWidth = true;
            this.colimp_interesatrasado.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_interesatrasado", "{0:#,#.##}")});
            this.colimp_interesatrasado.Visible = true;
            this.colimp_interesatrasado.VisibleIndex = 16;
            this.colimp_interesatrasado.Width = 100;
            // 
            // colimp_montoatrasado
            // 
            this.colimp_montoatrasado.Caption = "Monto Atrasado";
            this.colimp_montoatrasado.ColumnEdit = this.rtxtImporte;
            this.colimp_montoatrasado.FieldName = "imp_montoatrasado";
            this.colimp_montoatrasado.Name = "colimp_montoatrasado";
            this.colimp_montoatrasado.OptionsColumn.FixedWidth = true;
            this.colimp_montoatrasado.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_montoatrasado", "{0:#,#.##}")});
            this.colimp_montoatrasado.Visible = true;
            this.colimp_montoatrasado.VisibleIndex = 17;
            this.colimp_montoatrasado.Width = 100;
            // 
            // colimp_cuotavigente
            // 
            this.colimp_cuotavigente.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_cuotavigente.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_cuotavigente.Caption = "Cuota Vigente";
            this.colimp_cuotavigente.FieldName = "num_cuotavigente";
            this.colimp_cuotavigente.Name = "colimp_cuotavigente";
            this.colimp_cuotavigente.OptionsColumn.FixedWidth = true;
            this.colimp_cuotavigente.Visible = true;
            this.colimp_cuotavigente.VisibleIndex = 18;
            this.colimp_cuotavigente.Width = 40;
            // 
            // rtxtNumero
            // 
            this.rtxtNumero.AutoHeight = false;
            this.rtxtNumero.Mask.EditMask = "n2";
            this.rtxtNumero.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtNumero.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtNumero.Name = "rtxtNumero";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1340, 494);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoCronogramaCabecera;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1320, 474);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // xtabResumenFacturar
            // 
            this.xtabResumenFacturar.Controls.Add(this.layoutControl3);
            this.xtabResumenFacturar.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.pivot_16x16;
            this.xtabResumenFacturar.Name = "xtabResumenFacturar";
            this.xtabResumenFacturar.Size = new System.Drawing.Size(1340, 494);
            this.xtabResumenFacturar.Text = "Resumen para facturar";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.pivotResumenFacturar);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(1340, 494);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // pivotResumenFacturar
            // 
            this.pivotResumenFacturar.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.Cell.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.ColumnHeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.ColumnHeaderArea.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.ColumnHeaderArea.Options.UseTextOptions = true;
            this.pivotResumenFacturar.Appearance.ColumnHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotResumenFacturar.Appearance.CustomizationFormHint.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.CustomizationFormHint.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.CustomTotalCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.CustomTotalCell.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.DataHeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.DataHeaderArea.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.DataHeaderArea.Options.UseTextOptions = true;
            this.pivotResumenFacturar.Appearance.DataHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotResumenFacturar.Appearance.Empty.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.Empty.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.ExpandButton.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.ExpandButton.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.FieldHeader.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.FieldHeader.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.FieldHeader.Options.UseTextOptions = true;
            this.pivotResumenFacturar.Appearance.FieldHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotResumenFacturar.Appearance.FieldValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.pivotResumenFacturar.Appearance.FieldValue.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.FieldValue.Options.UseBackColor = true;
            this.pivotResumenFacturar.Appearance.FieldValue.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotResumenFacturar.Appearance.FieldValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.FieldValueGrandTotal.Options.UseFont = true;
            this.pivotResumenFacturar.Appearance.FieldValueGrandTotal.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotResumenFacturar.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.FieldValueTotal.Options.UseFont = true;
            this.pivotResumenFacturar.Appearance.FieldValueTotal.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.FilterHeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.FilterHeaderArea.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.FilterSeparator.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.FilterSeparator.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.FixedLine.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.FixedLine.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.FocusedCell.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pivotResumenFacturar.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.GrandTotalCell.Options.UseBackColor = true;
            this.pivotResumenFacturar.Appearance.GrandTotalCell.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.HeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.HeaderArea.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.HeaderArea.Options.UseTextOptions = true;
            this.pivotResumenFacturar.Appearance.HeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotResumenFacturar.Appearance.HeaderFilterButton.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.HeaderFilterButton.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.HeaderFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.HeaderFilterButtonActive.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.HeaderGroupLine.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.HeaderGroupLine.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.Lines.BackColor = System.Drawing.Color.LightGray;
            this.pivotResumenFacturar.Appearance.Lines.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.Lines.Options.UseBackColor = true;
            this.pivotResumenFacturar.Appearance.Lines.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.RowHeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.RowHeaderArea.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.RowHeaderArea.Options.UseTextOptions = true;
            this.pivotResumenFacturar.Appearance.RowHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotResumenFacturar.Appearance.SelectedCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.SelectedCell.Options.UseForeColor = true;
            this.pivotResumenFacturar.Appearance.TotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pivotResumenFacturar.Appearance.TotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotResumenFacturar.Appearance.TotalCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenFacturar.Appearance.TotalCell.Options.UseBackColor = true;
            this.pivotResumenFacturar.Appearance.TotalCell.Options.UseFont = true;
            this.pivotResumenFacturar.Appearance.TotalCell.Options.UseForeColor = true;
            this.pivotResumenFacturar.DataSource = this.bsResumenIntereses;
            this.pivotResumenFacturar.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldcodcredito,
            this.fielddsctitular,
            this.fielddscnombrescompletos,
            this.fieldnumdiapago,
            this.fielddscmes,
            this.fieldimpinteres,
            this.fielddscanho});
            pivotGridGroup1.Fields.Add(this.fielddsctitular);
            this.pivotResumenFacturar.Groups.AddRange(new DevExpress.XtraPivotGrid.PivotGridGroup[] {
            pivotGridGroup1});
            this.pivotResumenFacturar.Location = new System.Drawing.Point(12, 12);
            this.pivotResumenFacturar.MenuManager = this.ribbon;
            this.pivotResumenFacturar.Name = "pivotResumenFacturar";
            this.pivotResumenFacturar.OptionsCustomization.AllowEdit = false;
            this.pivotResumenFacturar.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporteInteres});
            this.pivotResumenFacturar.Size = new System.Drawing.Size(1316, 470);
            this.pivotResumenFacturar.TabIndex = 4;
            // 
            // bsResumenIntereses
            // 
            this.bsResumenIntereses.DataSource = typeof(BE_BackOffice.eCreditoVehicular.eCronogramaDetalle);
            // 
            // fieldcodcredito
            // 
            this.fieldcodcredito.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldcodcredito.AreaIndex = 0;
            this.fieldcodcredito.Caption = "Cod. Crédito";
            this.fieldcodcredito.FieldName = "cod_credito";
            this.fieldcodcredito.Name = "fieldcodcredito";
            this.fieldcodcredito.Width = 60;
            // 
            // fielddscnombrescompletos
            // 
            this.fielddscnombrescompletos.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fielddscnombrescompletos.AreaIndex = 2;
            this.fielddscnombrescompletos.Caption = "Cliente";
            this.fielddscnombrescompletos.FieldName = "dsc_nombres_completos";
            this.fielddscnombrescompletos.Name = "fielddscnombrescompletos";
            this.fielddscnombrescompletos.Width = 220;
            // 
            // fieldnumdiapago
            // 
            this.fieldnumdiapago.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldnumdiapago.AreaIndex = 3;
            this.fieldnumdiapago.Caption = "Días";
            this.fieldnumdiapago.FieldName = "num_diapago";
            this.fieldnumdiapago.Name = "fieldnumdiapago";
            this.fieldnumdiapago.Width = 40;
            // 
            // fielddscmes
            // 
            this.fielddscmes.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fielddscmes.AreaIndex = 1;
            this.fielddscmes.Caption = "Meses";
            this.fielddscmes.FieldName = "dsc_mes";
            this.fielddscmes.Name = "fielddscmes";
            // 
            // fieldimpinteres
            // 
            this.fieldimpinteres.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldimpinteres.AreaIndex = 0;
            this.fieldimpinteres.Caption = "Monto Interés";
            this.fieldimpinteres.FieldEdit = this.rtxtImporteInteres;
            this.fieldimpinteres.FieldName = "imp_interes";
            this.fieldimpinteres.Name = "fieldimpinteres";
            // 
            // rtxtImporteInteres
            // 
            this.rtxtImporteInteres.AutoHeight = false;
            this.rtxtImporteInteres.Mask.EditMask = "c2";
            this.rtxtImporteInteres.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtImporteInteres.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporteInteres.Name = "rtxtImporteInteres";
            // 
            // fielddscanho
            // 
            this.fielddscanho.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fielddscanho.AreaIndex = 0;
            this.fielddscanho.Caption = "Año";
            this.fielddscanho.FieldName = "dsc_anho";
            this.fielddscanho.Name = "fielddscanho";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1340, 494);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.pivotResumenFacturar;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1320, 474);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // xtabResumenPagos
            // 
            this.xtabResumenPagos.Controls.Add(this.layoutControl4);
            this.xtabResumenPagos.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.pivottablegroupselectioncontextmenuitem_16x16;
            this.xtabResumenPagos.Name = "xtabResumenPagos";
            this.xtabResumenPagos.Size = new System.Drawing.Size(1340, 494);
            this.xtabResumenPagos.Text = "Resumen Pagos";
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.pivotResumenPagos);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup3;
            this.layoutControl4.Size = new System.Drawing.Size(1340, 494);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // pivotResumenPagos
            // 
            this.pivotResumenPagos.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.Cell.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.ColumnHeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.ColumnHeaderArea.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.CustomizationFormHint.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.CustomizationFormHint.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.CustomTotalCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.CustomTotalCell.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.DataHeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.DataHeaderArea.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.Empty.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.Empty.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.ExpandButton.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.ExpandButton.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.FieldHeader.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.FieldHeader.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.FieldValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.pivotResumenPagos.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotResumenPagos.Appearance.FieldValue.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.FieldValue.Options.UseBackColor = true;
            this.pivotResumenPagos.Appearance.FieldValue.Options.UseFont = true;
            this.pivotResumenPagos.Appearance.FieldValue.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.FieldValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.FieldValueGrandTotal.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pivotResumenPagos.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.FieldValueTotal.Options.UseFont = true;
            this.pivotResumenPagos.Appearance.FieldValueTotal.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.FilterHeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.FilterHeaderArea.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.FilterSeparator.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.FilterSeparator.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.FixedLine.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.FixedLine.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.FocusedCell.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.GrandTotalCell.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.HeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.HeaderArea.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.HeaderFilterButton.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.HeaderFilterButton.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.HeaderFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.HeaderFilterButtonActive.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.HeaderGroupLine.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.HeaderGroupLine.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.Lines.BackColor = System.Drawing.Color.LightGray;
            this.pivotResumenPagos.Appearance.Lines.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.Lines.Options.UseBackColor = true;
            this.pivotResumenPagos.Appearance.Lines.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.RowHeaderArea.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.RowHeaderArea.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.SelectedCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.SelectedCell.Options.UseForeColor = true;
            this.pivotResumenPagos.Appearance.TotalCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pivotResumenPagos.Appearance.TotalCell.ForeColor = System.Drawing.Color.Black;
            this.pivotResumenPagos.Appearance.TotalCell.Options.UseBackColor = true;
            this.pivotResumenPagos.Appearance.TotalCell.Options.UseForeColor = true;
            this.pivotResumenPagos.DataSource = this.bsResumenPagos;
            this.pivotResumenPagos.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldfchdeposito,
            this.fielddscdestino,
            this.pivotGridField3,
            this.fielddsccliente,
            this.fieldcodcredito1,
            this.fieldnumplaca,
            this.fieldimpcapital,
            this.pivotGridField6,
            this.fieldimpigv,
            this.fieldimptotal});
            this.pivotResumenPagos.Groups.AddRange(new DevExpress.XtraPivotGrid.PivotGridGroup[] {
            pivotGridGroup2});
            this.pivotResumenPagos.Location = new System.Drawing.Point(12, 32);
            this.pivotResumenPagos.MenuManager = this.ribbon;
            this.pivotResumenPagos.Name = "pivotResumenPagos";
            this.pivotResumenPagos.OptionsCustomization.AllowEdit = false;
            this.pivotResumenPagos.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporteInteres2});
            this.pivotResumenPagos.Size = new System.Drawing.Size(1316, 450);
            this.pivotResumenPagos.TabIndex = 5;
            // 
            // bsResumenPagos
            // 
            this.bsResumenPagos.DataSource = typeof(BE_BackOffice.eCreditoVehicular.eCronogramaDetalle);
            // 
            // fielddscdestino
            // 
            this.fielddscdestino.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fielddscdestino.AreaIndex = 0;
            this.fielddscdestino.Caption = "Destino";
            this.fielddscdestino.FieldName = "dsc_destino";
            this.fielddscdestino.Name = "fielddscdestino";
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField3.Appearance.Cell.Options.UseForeColor = true;
            this.pivotGridField3.Appearance.CellGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField3.Appearance.CellGrandTotal.Options.UseForeColor = true;
            this.pivotGridField3.Appearance.CellTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField3.Appearance.CellTotal.Options.UseForeColor = true;
            this.pivotGridField3.Appearance.Header.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField3.Appearance.Header.Options.UseForeColor = true;
            this.pivotGridField3.Appearance.Value.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField3.Appearance.Value.Options.UseForeColor = true;
            this.pivotGridField3.Appearance.ValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField3.Appearance.ValueGrandTotal.Options.UseForeColor = true;
            this.pivotGridField3.Appearance.ValueTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField3.Appearance.ValueTotal.Options.UseForeColor = true;
            this.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField3.AreaIndex = 1;
            this.pivotGridField3.Caption = "Titular";
            this.pivotGridField3.FieldName = "dsc_nombres_completos";
            this.pivotGridField3.Name = "pivotGridField3";
            this.pivotGridField3.Width = 200;
            // 
            // fielddsccliente
            // 
            this.fielddsccliente.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.fielddsccliente.Appearance.Cell.Options.UseForeColor = true;
            this.fielddsccliente.Appearance.CellGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fielddsccliente.Appearance.CellGrandTotal.Options.UseForeColor = true;
            this.fielddsccliente.Appearance.CellTotal.ForeColor = System.Drawing.Color.Black;
            this.fielddsccliente.Appearance.CellTotal.Options.UseForeColor = true;
            this.fielddsccliente.Appearance.Header.ForeColor = System.Drawing.Color.Black;
            this.fielddsccliente.Appearance.Header.Options.UseForeColor = true;
            this.fielddsccliente.Appearance.Value.ForeColor = System.Drawing.Color.Black;
            this.fielddsccliente.Appearance.Value.Options.UseForeColor = true;
            this.fielddsccliente.Appearance.ValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fielddsccliente.Appearance.ValueGrandTotal.Options.UseForeColor = true;
            this.fielddsccliente.Appearance.ValueTotal.ForeColor = System.Drawing.Color.Black;
            this.fielddsccliente.Appearance.ValueTotal.Options.UseForeColor = true;
            this.fielddsccliente.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fielddsccliente.AreaIndex = 2;
            this.fielddsccliente.Caption = "Cliente";
            this.fielddsccliente.FieldName = "dsc_cliente";
            this.fielddsccliente.Name = "fielddsccliente";
            this.fielddsccliente.Width = 200;
            // 
            // fieldcodcredito1
            // 
            this.fieldcodcredito1.Appearance.Value.Options.UseTextOptions = true;
            this.fieldcodcredito1.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldcodcredito1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldcodcredito1.AreaIndex = 3;
            this.fieldcodcredito1.Caption = "Nro. Crédito";
            this.fieldcodcredito1.FieldName = "cod_credito";
            this.fieldcodcredito1.Name = "fieldcodcredito1";
            this.fieldcodcredito1.Width = 70;
            // 
            // fieldnumplaca
            // 
            this.fieldnumplaca.Appearance.Value.Options.UseTextOptions = true;
            this.fieldnumplaca.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldnumplaca.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldnumplaca.AreaIndex = 4;
            this.fieldnumplaca.Caption = "Placa";
            this.fieldnumplaca.FieldName = "num_placa";
            this.fieldnumplaca.Name = "fieldnumplaca";
            this.fieldnumplaca.Width = 80;
            // 
            // fieldimpcapital
            // 
            this.fieldimpcapital.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.fieldimpcapital.Appearance.Cell.Options.UseForeColor = true;
            this.fieldimpcapital.Appearance.CellGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimpcapital.Appearance.CellGrandTotal.Options.UseForeColor = true;
            this.fieldimpcapital.Appearance.CellTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimpcapital.Appearance.CellTotal.Options.UseForeColor = true;
            this.fieldimpcapital.Appearance.Header.ForeColor = System.Drawing.Color.Black;
            this.fieldimpcapital.Appearance.Header.Options.UseForeColor = true;
            this.fieldimpcapital.Appearance.Value.ForeColor = System.Drawing.Color.Black;
            this.fieldimpcapital.Appearance.Value.Options.UseForeColor = true;
            this.fieldimpcapital.Appearance.ValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimpcapital.Appearance.ValueGrandTotal.Options.UseForeColor = true;
            this.fieldimpcapital.Appearance.ValueTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimpcapital.Appearance.ValueTotal.Options.UseForeColor = true;
            this.fieldimpcapital.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldimpcapital.AreaIndex = 0;
            this.fieldimpcapital.Caption = "Capital";
            this.fieldimpcapital.FieldEdit = this.rtxtImporteInteres2;
            this.fieldimpcapital.FieldName = "imp_capital";
            this.fieldimpcapital.Name = "fieldimpcapital";
            // 
            // rtxtImporteInteres2
            // 
            this.rtxtImporteInteres2.AutoHeight = false;
            this.rtxtImporteInteres2.Mask.EditMask = "c2";
            this.rtxtImporteInteres2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtImporteInteres2.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporteInteres2.Name = "rtxtImporteInteres2";
            // 
            // pivotGridField6
            // 
            this.pivotGridField6.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField6.Appearance.Cell.Options.UseForeColor = true;
            this.pivotGridField6.Appearance.CellGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField6.Appearance.CellGrandTotal.Options.UseForeColor = true;
            this.pivotGridField6.Appearance.CellTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField6.Appearance.CellTotal.Options.UseForeColor = true;
            this.pivotGridField6.Appearance.Header.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField6.Appearance.Header.Options.UseForeColor = true;
            this.pivotGridField6.Appearance.Header.Options.UseTextOptions = true;
            this.pivotGridField6.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.pivotGridField6.Appearance.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.pivotGridField6.Appearance.Value.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField6.Appearance.Value.Options.UseForeColor = true;
            this.pivotGridField6.Appearance.ValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField6.Appearance.ValueGrandTotal.Options.UseForeColor = true;
            this.pivotGridField6.Appearance.ValueTotal.ForeColor = System.Drawing.Color.Black;
            this.pivotGridField6.Appearance.ValueTotal.Options.UseForeColor = true;
            this.pivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField6.AreaIndex = 1;
            this.pivotGridField6.Caption = "Interés";
            this.pivotGridField6.FieldEdit = this.rtxtImporteInteres2;
            this.pivotGridField6.FieldName = "imp_interes";
            this.pivotGridField6.Name = "pivotGridField6";
            // 
            // fieldimpigv
            // 
            this.fieldimpigv.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.fieldimpigv.Appearance.Cell.Options.UseForeColor = true;
            this.fieldimpigv.Appearance.CellGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimpigv.Appearance.CellGrandTotal.Options.UseForeColor = true;
            this.fieldimpigv.Appearance.CellTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimpigv.Appearance.CellTotal.Options.UseForeColor = true;
            this.fieldimpigv.Appearance.Header.ForeColor = System.Drawing.Color.Black;
            this.fieldimpigv.Appearance.Header.Options.UseForeColor = true;
            this.fieldimpigv.Appearance.Header.Options.UseTextOptions = true;
            this.fieldimpigv.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldimpigv.Appearance.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldimpigv.Appearance.Value.ForeColor = System.Drawing.Color.Black;
            this.fieldimpigv.Appearance.Value.Options.UseForeColor = true;
            this.fieldimpigv.Appearance.ValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimpigv.Appearance.ValueGrandTotal.Options.UseForeColor = true;
            this.fieldimpigv.Appearance.ValueTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimpigv.Appearance.ValueTotal.Options.UseForeColor = true;
            this.fieldimpigv.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldimpigv.AreaIndex = 2;
            this.fieldimpigv.Caption = "IGV";
            this.fieldimpigv.FieldEdit = this.rtxtImporteInteres2;
            this.fieldimpigv.FieldName = "imp_igv";
            this.fieldimpigv.Name = "fieldimpigv";
            // 
            // fieldimptotal
            // 
            this.fieldimptotal.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.fieldimptotal.Appearance.Cell.Options.UseForeColor = true;
            this.fieldimptotal.Appearance.CellGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimptotal.Appearance.CellGrandTotal.Options.UseForeColor = true;
            this.fieldimptotal.Appearance.CellTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimptotal.Appearance.CellTotal.Options.UseForeColor = true;
            this.fieldimptotal.Appearance.Header.ForeColor = System.Drawing.Color.Black;
            this.fieldimptotal.Appearance.Header.Options.UseForeColor = true;
            this.fieldimptotal.Appearance.Header.Options.UseTextOptions = true;
            this.fieldimptotal.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldimptotal.Appearance.Header.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldimptotal.Appearance.Value.ForeColor = System.Drawing.Color.Black;
            this.fieldimptotal.Appearance.Value.Options.UseForeColor = true;
            this.fieldimptotal.Appearance.ValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimptotal.Appearance.ValueGrandTotal.Options.UseForeColor = true;
            this.fieldimptotal.Appearance.ValueTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldimptotal.Appearance.ValueTotal.Options.UseForeColor = true;
            this.fieldimptotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldimptotal.AreaIndex = 3;
            this.fieldimptotal.Caption = "Total Pagado";
            this.fieldimptotal.FieldEdit = this.rtxtImporteInteres2;
            this.fieldimptotal.FieldName = "imp_total";
            this.fieldimptotal.Name = "fieldimptotal";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.simpleLabelItem3});
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(1340, 494);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.pivotResumenPagos;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 20);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1320, 454);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem3.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Size = new System.Drawing.Size(1320, 20);
            this.simpleLabelItem3.Text = "Para este reporte el rango de FECHA DESEMBOLSO se considera como FECHAS DEPOSITO";
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(567, 16);
            // 
            // dtFechaDesembolsoFin
            // 
            this.dtFechaDesembolsoFin.EditValue = null;
            this.dtFechaDesembolsoFin.Location = new System.Drawing.Point(214, 86);
            this.dtFechaDesembolsoFin.Name = "dtFechaDesembolsoFin";
            this.dtFechaDesembolsoFin.Properties.Appearance.Options.UseTextOptions = true;
            this.dtFechaDesembolsoFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtFechaDesembolsoFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaDesembolsoFin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaDesembolsoFin.Size = new System.Drawing.Size(98, 20);
            this.dtFechaDesembolsoFin.StyleController = this.layoutControl1;
            this.dtFechaDesembolsoFin.TabIndex = 7;
            // 
            // dtFechaDesembolsoInicio
            // 
            this.dtFechaDesembolsoInicio.EditValue = null;
            this.dtFechaDesembolsoInicio.Location = new System.Drawing.Point(67, 86);
            this.dtFechaDesembolsoInicio.Name = "dtFechaDesembolsoInicio";
            this.dtFechaDesembolsoInicio.Properties.Appearance.Options.UseTextOptions = true;
            this.dtFechaDesembolsoInicio.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtFechaDesembolsoInicio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaDesembolsoInicio.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaDesembolsoInicio.Size = new System.Drawing.Size(98, 20);
            this.dtFechaDesembolsoInicio.StyleController = this.layoutControl1;
            this.dtFechaDesembolsoInicio.TabIndex = 6;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnBuscar.Appearance.Options.UseFont = true;
            this.btnBuscar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.ImageOptions.Image")));
            this.btnBuscar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(538, 65);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(126, 38);
            this.btnBuscar.StyleController = this.layoutControl1;
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtFechaProximoPago
            // 
            this.dtFechaProximoPago.EditValue = null;
            this.dtFechaProximoPago.Location = new System.Drawing.Point(394, 86);
            this.dtFechaProximoPago.Name = "dtFechaProximoPago";
            this.dtFechaProximoPago.Properties.Appearance.Options.UseTextOptions = true;
            this.dtFechaProximoPago.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtFechaProximoPago.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaProximoPago.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaProximoPago.Size = new System.Drawing.Size(98, 20);
            this.dtFechaProximoPago.StyleController = this.layoutControl1;
            this.dtFechaProximoPago.TabIndex = 7;
            // 
            // chkFechaDesembolso
            // 
            this.chkFechaDesembolso.EditValue = true;
            this.chkFechaDesembolso.Location = new System.Drawing.Point(22, 62);
            this.chkFechaDesembolso.MenuManager = this.ribbon;
            this.chkFechaDesembolso.Name = "chkFechaDesembolso";
            this.chkFechaDesembolso.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkFechaDesembolso.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.chkFechaDesembolso.Properties.Appearance.Options.UseFont = true;
            this.chkFechaDesembolso.Properties.Appearance.Options.UseForeColor = true;
            this.chkFechaDesembolso.Properties.Caption = " Fecha desembolso : ";
            this.chkFechaDesembolso.Size = new System.Drawing.Size(153, 20);
            this.chkFechaDesembolso.StyleController = this.layoutControl1;
            this.chkFechaDesembolso.TabIndex = 8;
            this.chkFechaDesembolso.CheckStateChanged += new System.EventHandler(this.chkFechaDesembolso_CheckStateChanged);
            // 
            // chkFechaProximoPago
            // 
            this.chkFechaProximoPago.Location = new System.Drawing.Point(329, 62);
            this.chkFechaProximoPago.MenuManager = this.ribbon;
            this.chkFechaProximoPago.Name = "chkFechaProximoPago";
            this.chkFechaProximoPago.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkFechaProximoPago.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.chkFechaProximoPago.Properties.Appearance.Options.UseFont = true;
            this.chkFechaProximoPago.Properties.Appearance.Options.UseForeColor = true;
            this.chkFechaProximoPago.Properties.Caption = " Próximo pago :";
            this.chkFechaProximoPago.Size = new System.Drawing.Size(120, 20);
            this.chkFechaProximoPago.StyleController = this.layoutControl1;
            this.chkFechaProximoPago.TabIndex = 9;
            this.chkFechaProximoPago.CheckStateChanged += new System.EventHandler(this.chkFechaProximoPago_CheckStateChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleLabelItem1,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.simpleLabelItem2,
            this.layoutControlItem23,
            this.emptySpaceItem3,
            this.layoutControlItem9,
            this.emptySpaceItem4,
            this.emptySpaceItem5,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem6,
            this.emptySpaceItem7,
            this.layoutControlItem8,
            this.emptySpaceItem8});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1366, 654);
            this.Root.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGreen;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(1346, 28);
            this.simpleLabelItem1.Text = "Listado de Créditos";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(192, 24);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.xtraTabControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1346, 526);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 50);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(10, 48);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 48);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 48);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(659, 50);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(687, 48);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 28);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(1346, 22);
            this.simpleLabelItem2.Text = "Filtros de Búsqueda";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(192, 18);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.dtFechaDesembolsoInicio;
            this.layoutControlItem23.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem23.CustomizationFormText = "Desde";
            this.layoutControlItem23.Location = new System.Drawing.Point(10, 74);
            this.layoutControlItem23.MaxSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem23.MinSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(147, 24);
            this.layoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem23.Text = "Desde :";
            this.layoutControlItem23.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem23.TextSize = new System.Drawing.Size(40, 13);
            this.layoutControlItem23.TextToControlDistance = 5;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 98);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(1346, 10);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnBuscar;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem9.Location = new System.Drawing.Point(523, 50);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(136, 48);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(136, 48);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(136, 48);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem9.Text = "layoutControlItem7";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(304, 50);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(13, 0);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(13, 10);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(13, 48);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(484, 50);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(39, 48);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(39, 48);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(39, 48);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dtFechaProximoPago;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "Hasta :";
            this.layoutControlItem5.Enabled = false;
            this.layoutControlItem5.Location = new System.Drawing.Point(317, 74);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(167, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(167, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(167, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Fecha : ";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 13);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.dtFechaDesembolsoFin;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "Hasta :";
            this.layoutControlItem7.Location = new System.Drawing.Point(157, 74);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(147, 24);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "Hasta :";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(40, 13);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkFechaDesembolso;
            this.layoutControlItem6.Location = new System.Drawing.Point(10, 50);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(157, 24);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(157, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(157, 24);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(167, 50);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(137, 24);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.chkFechaProximoPago;
            this.layoutControlItem8.Location = new System.Drawing.Point(317, 50);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(124, 24);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(124, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(124, 24);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.Location = new System.Drawing.Point(441, 50);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(43, 24);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // fieldfchdeposito
            // 
            this.fieldfchdeposito.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldfchdeposito.AreaIndex = 0;
            this.fieldfchdeposito.Caption = "Fec. Depósito";
            this.fieldfchdeposito.FieldName = "fch_deposito";
            this.fieldfchdeposito.Name = "fieldfchdeposito";
            this.fieldfchdeposito.Width = 85;
            // 
            // frmListadoCreditos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 836);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Name = "frmListadoCreditos";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Listado de Créditos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoCreditos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoCreditos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtabListadoCreditos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoCronogramaCabecera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCreditos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoCronogramaCabecera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtNumero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.xtabResumenFacturar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotResumenFacturar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsResumenIntereses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporteInteres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.xtabResumenPagos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotResumenPagos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsResumenPagos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporteInteres2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolsoFin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolsoFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolsoInicio.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolsoInicio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaProximoPago.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaProximoPago.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFechaDesembolso.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFechaProximoPago.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoAcciones;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcListadoCronogramaCabecera;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoCronogramaCabecera;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_credito;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nombres_completos;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_placa;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_Capital;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_desembolso;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_cuotas;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_diapago;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_tasaanual;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPorcentaje;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_tasamensual;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_tasaTIRanual;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_tasaTIRM;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPorcentaje2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtNumero;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraBars.BarButtonItem btnNuevoCliente;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private System.Windows.Forms.BindingSource bsListadoCreditos;
        private DevExpress.XtraBars.BarButtonItem btnSimuladorCuotasFijas;
        private DevExpress.XtraGrid.Columns.GridColumn colSemaforo;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_titular;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_capitalvigente;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_montopagado;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_capitalatrasado;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_interesatrasado;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_montoatrasado;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_cuotavigente;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotResumenFacturar;
        private System.Windows.Forms.BindingSource bsResumenIntereses;
        private DevExpress.XtraPivotGrid.PivotGridField fieldcodcredito;
        private DevExpress.XtraPivotGrid.PivotGridField fielddsctitular;
        private DevExpress.XtraPivotGrid.PivotGridField fieldnumdiapago;
        private DevExpress.XtraPivotGrid.PivotGridField fielddscnombrescompletos;
        private DevExpress.XtraPivotGrid.PivotGridField fielddscmes;
        private DevExpress.XtraPivotGrid.PivotGridField fieldimpinteres;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtabListadoCreditos;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTab.XtraTabPage xtabResumenFacturar;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraPivotGrid.PivotGridField fielddscanho;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.BarButtonItem btnImportarPagosCOFIDE;
        private DevExpress.XtraBars.BarButtonItem btnListadoPagos;
        private DevExpress.XtraTab.XtraTabPage xtabResumenPagos;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotResumenPagos;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
        private DevExpress.XtraPivotGrid.PivotGridField fieldimpcapital;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField6;
        private DevExpress.XtraPivotGrid.PivotGridField fieldimpigv;
        private DevExpress.XtraPivotGrid.PivotGridField fieldimptotal;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.Windows.Forms.BindingSource bsResumenPagos;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporteInteres;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporteInteres2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraEditors.DateEdit dtFechaDesembolsoFin;
        private DevExpress.XtraEditors.DateEdit dtFechaDesembolsoInicio;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraEditors.DateEdit dtFechaProximoPago;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.CheckEdit chkFechaDesembolso;
        private DevExpress.XtraEditors.CheckEdit chkFechaProximoPago;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraPivotGrid.PivotGridField fielddscdestino;
        private DevExpress.XtraPivotGrid.PivotGridField fieldcodcredito1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldnumplaca;
        private DevExpress.XtraBars.BarButtonItem btnImportarPagosBBVA;
        private DevExpress.XtraBars.BarButtonItem btnAplicarPagosPendientes;
        private DevExpress.XtraBars.BarButtonItem btnEliminarTodosPagos;
        private DevExpress.XtraBars.BarButtonItem btnEliminarCredito;
        private DevExpress.XtraPivotGrid.PivotGridField fielddsccliente;
        private DevExpress.XtraPivotGrid.PivotGridField fieldfchdeposito;
    }
}