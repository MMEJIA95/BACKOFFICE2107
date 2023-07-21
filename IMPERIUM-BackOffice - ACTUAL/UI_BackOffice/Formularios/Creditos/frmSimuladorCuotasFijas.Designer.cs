
namespace UI_BackOffice.Formularios.Creditos
{
    partial class frmSimuladorCuotasFijas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSimuladorCuotasFijas));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportarPagosCOFIDE = new DevExpress.XtraBars.BarButtonItem();
            this.btnNuevoCronograma = new DevExpress.XtraBars.BarButtonItem();
            this.btnConsultarCronograma = new DevExpress.XtraBars.BarButtonItem();
            this.btnAplicarPagoManual = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelarCredito = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminarCronograma = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoAcciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoCronograma = new DevExpress.XtraGrid.GridControl();
            this.bsListadoCronograma = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoCronograma = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colnum_cuota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_cuota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_dias = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_capitalinicial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colimp_capitalfinal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_amortizacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_interes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_desgravamen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_portes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_otros = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_cuotasinigv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_coutaigv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_cuotaconigv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_montopagado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_montoporpagar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtPorcentaje = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtMontoSolicitado = new DevExpress.XtraEditors.TextEdit();
            this.txtCuotas = new DevExpress.XtraEditors.TextEdit();
            this.txtTasaAnual = new DevExpress.XtraEditors.TextEdit();
            this.txtTasaMensual = new DevExpress.XtraEditors.TextEdit();
            this.btnCalcularCronograma = new DevExpress.XtraEditors.SimpleButton();
            this.txtTIRM = new DevExpress.XtraEditors.TextEdit();
            this.txtTIRAnual = new DevExpress.XtraEditors.TextEdit();
            this.dtFechaDesembolso = new DevExpress.XtraEditors.DateEdit();
            this.txtDiasPago = new DevExpress.XtraEditors.TextEdit();
            this.btnGuardarCronograma = new DevExpress.XtraEditors.SimpleButton();
            this.txtCodCredito = new DevExpress.XtraEditors.TextEdit();
            this.txtNumeroPlaca = new DevExpress.XtraEditors.TextEdit();
            this.txtCliente = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalCapital = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalCredito = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem4 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnImportarPagosBBVA = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoCronograma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCronograma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoCronograma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoSolicitado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuotas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTasaAnual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTasaMensual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTIRM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTIRAnual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolso.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolso.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiasPago.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodCredito.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlaca.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCapital.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCredito.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
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
            this.btnImportarPagosCOFIDE,
            this.btnNuevoCronograma,
            this.btnConsultarCronograma,
            this.btnAplicarPagoManual,
            this.btnCancelarCredito,
            this.barButtonItem1,
            this.btnEliminarCronograma,
            this.btnImportarPagosBBVA});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 12;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1334, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar a Excel";
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
            this.btnImprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimir_ItemClick);
            // 
            // btnImportarPagosCOFIDE
            // 
            this.btnImportarPagosCOFIDE.Caption = "Importar Pagos COFIDE";
            this.btnImportarPagosCOFIDE.Id = 3;
            this.btnImportarPagosCOFIDE.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.bill__1_;
            this.btnImportarPagosCOFIDE.Name = "btnImportarPagosCOFIDE";
            this.btnImportarPagosCOFIDE.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImportarPagosCOFIDE.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportarPagosCOFIDE_ItemClick);
            // 
            // btnNuevoCronograma
            // 
            this.btnNuevoCronograma.Caption = "Nuevo Cronograma";
            this.btnNuevoCronograma.Id = 4;
            this.btnNuevoCronograma.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoCronograma.ImageOptions.Image")));
            this.btnNuevoCronograma.Name = "btnNuevoCronograma";
            this.btnNuevoCronograma.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnNuevoCronograma.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevoCronograma_ItemClick);
            // 
            // btnConsultarCronograma
            // 
            this.btnConsultarCronograma.Caption = "Consultar Cronograma";
            this.btnConsultarCronograma.Id = 5;
            this.btnConsultarCronograma.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultarCronograma.ImageOptions.Image")));
            this.btnConsultarCronograma.Name = "btnConsultarCronograma";
            this.btnConsultarCronograma.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnConsultarCronograma.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnConsultarCronograma.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnConsultarCronograma_ItemClick);
            // 
            // btnAplicarPagoManual
            // 
            this.btnAplicarPagoManual.Caption = "Aplicar Pago Manual";
            this.btnAplicarPagoManual.Id = 6;
            this.btnAplicarPagoManual.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.pay__1_;
            this.btnAplicarPagoManual.Name = "btnAplicarPagoManual";
            this.btnAplicarPagoManual.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAplicarPagoManual.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAplicarPagoManual_ItemClick);
            // 
            // btnCancelarCredito
            // 
            this.btnCancelarCredito.Caption = "Cancelar Crédito";
            this.btnCancelarCredito.Id = 7;
            this.btnCancelarCredito.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.bank_check__1_;
            this.btnCancelarCredito.Name = "btnCancelarCredito";
            this.btnCancelarCredito.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnCancelarCredito.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancelarCredito_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Presione F5 para actualizar listado";
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.barButtonItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btnEliminarCronograma
            // 
            this.btnEliminarCronograma.Caption = "Eliminar Cronograma";
            this.btnEliminarCronograma.Id = 9;
            this.btnEliminarCronograma.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarCronograma.ImageOptions.Image")));
            this.btnEliminarCronograma.Name = "btnEliminarCronograma";
            this.btnEliminarCronograma.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEliminarCronograma.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminarCronograma_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoReportes,
            this.grupoAcciones});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones Simulador Cuota Fija";
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
            this.grupoAcciones.ItemLinks.Add(this.btnNuevoCronograma);
            this.grupoAcciones.ItemLinks.Add(this.btnConsultarCronograma);
            this.grupoAcciones.ItemLinks.Add(this.btnImportarPagosCOFIDE);
            this.grupoAcciones.ItemLinks.Add(this.btnImportarPagosBBVA);
            this.grupoAcciones.ItemLinks.Add(this.btnAplicarPagoManual);
            this.grupoAcciones.ItemLinks.Add(this.btnCancelarCredito);
            this.grupoAcciones.ItemLinks.Add(this.btnEliminarCronograma);
            this.grupoAcciones.Name = "grupoAcciones";
            this.grupoAcciones.Text = "Acciones";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barButtonItem1);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 710);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1334, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListadoCronograma);
            this.layoutControl1.Controls.Add(this.txtMontoSolicitado);
            this.layoutControl1.Controls.Add(this.txtCuotas);
            this.layoutControl1.Controls.Add(this.txtTasaAnual);
            this.layoutControl1.Controls.Add(this.txtTasaMensual);
            this.layoutControl1.Controls.Add(this.btnCalcularCronograma);
            this.layoutControl1.Controls.Add(this.txtTIRM);
            this.layoutControl1.Controls.Add(this.txtTIRAnual);
            this.layoutControl1.Controls.Add(this.dtFechaDesembolso);
            this.layoutControl1.Controls.Add(this.txtDiasPago);
            this.layoutControl1.Controls.Add(this.btnGuardarCronograma);
            this.layoutControl1.Controls.Add(this.txtCodCredito);
            this.layoutControl1.Controls.Add(this.txtNumeroPlaca);
            this.layoutControl1.Controls.Add(this.txtCliente);
            this.layoutControl1.Controls.Add(this.txtTotalCapital);
            this.layoutControl1.Controls.Add(this.txtTotalCredito);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1334, 552);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListadoCronograma
            // 
            this.gcListadoCronograma.DataSource = this.bsListadoCronograma;
            this.gcListadoCronograma.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoCronograma.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoCronograma.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoCronograma.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoCronograma.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoCronograma.Location = new System.Drawing.Point(12, 106);
            this.gcListadoCronograma.MainView = this.gvListadoCronograma;
            this.gcListadoCronograma.MenuManager = this.ribbon;
            this.gcListadoCronograma.Name = "gcListadoCronograma";
            this.gcListadoCronograma.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporte,
            this.rtxtPorcentaje});
            this.gcListadoCronograma.Size = new System.Drawing.Size(1310, 410);
            this.gcListadoCronograma.TabIndex = 10;
            this.gcListadoCronograma.UseEmbeddedNavigator = true;
            this.gcListadoCronograma.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoCronograma});
            // 
            // bsListadoCronograma
            // 
            this.bsListadoCronograma.DataSource = typeof(BE_BackOffice.eCreditoVehicular.eCronogramaDetalle);
            // 
            // gvListadoCronograma
            // 
            this.gvListadoCronograma.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoCronograma.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoCronograma.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoCronograma.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoCronograma.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListadoCronograma.ColumnPanelRowHeight = 35;
            this.gvListadoCronograma.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colnum_cuota,
            this.colfch_cuota,
            this.colnum_dias,
            this.colimp_capitalinicial,
            this.colimp_capitalfinal,
            this.colimp_amortizacion,
            this.colimp_interes,
            this.colimp_desgravamen,
            this.colimp_portes,
            this.colimp_otros,
            this.colimp_cuotasinigv,
            this.colimp_coutaigv,
            this.colimp_cuotaconigv,
            this.colimp_montopagado,
            this.colimp_montoporpagar});
            this.gvListadoCronograma.GridControl = this.gcListadoCronograma;
            this.gvListadoCronograma.Name = "gvListadoCronograma";
            this.gvListadoCronograma.OptionsBehavior.Editable = false;
            this.gvListadoCronograma.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoCronograma.OptionsView.ShowIndicator = false;
            this.gvListadoCronograma.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoCronograma_RowClick);
            this.gvListadoCronograma.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoCronograma_CustomDrawColumnHeader);
            this.gvListadoCronograma.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListadoCronograma_CustomDrawCell);
            this.gvListadoCronograma.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoCronograma_RowStyle);
            // 
            // colnum_cuota
            // 
            this.colnum_cuota.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_cuota.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_cuota.Caption = "N°";
            this.colnum_cuota.FieldName = "num_cuota";
            this.colnum_cuota.Name = "colnum_cuota";
            this.colnum_cuota.OptionsColumn.FixedWidth = true;
            this.colnum_cuota.Visible = true;
            this.colnum_cuota.VisibleIndex = 0;
            this.colnum_cuota.Width = 30;
            // 
            // colfch_cuota
            // 
            this.colfch_cuota.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_cuota.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cuota.Caption = "Fecha";
            this.colfch_cuota.FieldName = "fch_cuota";
            this.colfch_cuota.Name = "colfch_cuota";
            this.colfch_cuota.OptionsColumn.FixedWidth = true;
            this.colfch_cuota.Visible = true;
            this.colfch_cuota.VisibleIndex = 1;
            this.colfch_cuota.Width = 70;
            // 
            // colnum_dias
            // 
            this.colnum_dias.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_dias.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_dias.Caption = "Días";
            this.colnum_dias.FieldName = "num_dias";
            this.colnum_dias.Name = "colnum_dias";
            this.colnum_dias.OptionsColumn.FixedWidth = true;
            this.colnum_dias.Visible = true;
            this.colnum_dias.VisibleIndex = 2;
            this.colnum_dias.Width = 30;
            // 
            // colimp_capitalinicial
            // 
            this.colimp_capitalinicial.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_capitalinicial.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_capitalinicial.Caption = "Saldo deudor inicial";
            this.colimp_capitalinicial.ColumnEdit = this.rtxtImporte;
            this.colimp_capitalinicial.FieldName = "imp_capitalinicial";
            this.colimp_capitalinicial.Name = "colimp_capitalinicial";
            this.colimp_capitalinicial.OptionsColumn.FixedWidth = true;
            // 
            // rtxtImporte
            // 
            this.rtxtImporte.AutoHeight = false;
            this.rtxtImporte.Mask.EditMask = "c2";
            this.rtxtImporte.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtImporte.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporte.Name = "rtxtImporte";
            // 
            // colimp_capitalfinal
            // 
            this.colimp_capitalfinal.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_capitalfinal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_capitalfinal.Caption = "Saldo deudor final";
            this.colimp_capitalfinal.ColumnEdit = this.rtxtImporte;
            this.colimp_capitalfinal.FieldName = "imp_capitalfinal";
            this.colimp_capitalfinal.Name = "colimp_capitalfinal";
            this.colimp_capitalfinal.OptionsColumn.FixedWidth = true;
            this.colimp_capitalfinal.Visible = true;
            this.colimp_capitalfinal.VisibleIndex = 3;
            // 
            // colimp_amortizacion
            // 
            this.colimp_amortizacion.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_amortizacion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_amortizacion.Caption = "Amortización capital";
            this.colimp_amortizacion.ColumnEdit = this.rtxtImporte;
            this.colimp_amortizacion.FieldName = "imp_amortizacion";
            this.colimp_amortizacion.Name = "colimp_amortizacion";
            this.colimp_amortizacion.OptionsColumn.FixedWidth = true;
            this.colimp_amortizacion.Visible = true;
            this.colimp_amortizacion.VisibleIndex = 4;
            // 
            // colimp_interes
            // 
            this.colimp_interes.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_interes.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_interes.Caption = "Pago de intereses";
            this.colimp_interes.ColumnEdit = this.rtxtImporte;
            this.colimp_interes.FieldName = "imp_interes";
            this.colimp_interes.Name = "colimp_interes";
            this.colimp_interes.OptionsColumn.FixedWidth = true;
            this.colimp_interes.Visible = true;
            this.colimp_interes.VisibleIndex = 5;
            // 
            // colimp_desgravamen
            // 
            this.colimp_desgravamen.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_desgravamen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_desgravamen.Caption = "Desgravamen";
            this.colimp_desgravamen.ColumnEdit = this.rtxtImporte;
            this.colimp_desgravamen.FieldName = "imp_desgravamen";
            this.colimp_desgravamen.Name = "colimp_desgravamen";
            this.colimp_desgravamen.OptionsColumn.FixedWidth = true;
            this.colimp_desgravamen.Visible = true;
            this.colimp_desgravamen.VisibleIndex = 6;
            // 
            // colimp_portes
            // 
            this.colimp_portes.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_portes.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_portes.Caption = "Portes";
            this.colimp_portes.ColumnEdit = this.rtxtImporte;
            this.colimp_portes.FieldName = "imp_portes";
            this.colimp_portes.Name = "colimp_portes";
            this.colimp_portes.OptionsColumn.FixedWidth = true;
            this.colimp_portes.Visible = true;
            this.colimp_portes.VisibleIndex = 7;
            // 
            // colimp_otros
            // 
            this.colimp_otros.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_otros.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_otros.Caption = "Otros";
            this.colimp_otros.ColumnEdit = this.rtxtImporte;
            this.colimp_otros.FieldName = "imp_otros";
            this.colimp_otros.Name = "colimp_otros";
            this.colimp_otros.OptionsColumn.FixedWidth = true;
            this.colimp_otros.Visible = true;
            this.colimp_otros.VisibleIndex = 8;
            // 
            // colimp_cuotasinigv
            // 
            this.colimp_cuotasinigv.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_cuotasinigv.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_cuotasinigv.Caption = "Valor cuota";
            this.colimp_cuotasinigv.ColumnEdit = this.rtxtImporte;
            this.colimp_cuotasinigv.FieldName = "imp_cuotasinigv";
            this.colimp_cuotasinigv.Name = "colimp_cuotasinigv";
            this.colimp_cuotasinigv.OptionsColumn.FixedWidth = true;
            this.colimp_cuotasinigv.Visible = true;
            this.colimp_cuotasinigv.VisibleIndex = 9;
            // 
            // colimp_coutaigv
            // 
            this.colimp_coutaigv.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_coutaigv.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_coutaigv.Caption = "IGV";
            this.colimp_coutaigv.ColumnEdit = this.rtxtImporte;
            this.colimp_coutaigv.FieldName = "imp_coutaigv";
            this.colimp_coutaigv.Name = "colimp_coutaigv";
            this.colimp_coutaigv.OptionsColumn.FixedWidth = true;
            this.colimp_coutaigv.Visible = true;
            this.colimp_coutaigv.VisibleIndex = 10;
            // 
            // colimp_cuotaconigv
            // 
            this.colimp_cuotaconigv.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_cuotaconigv.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_cuotaconigv.Caption = "Total a pagar";
            this.colimp_cuotaconigv.ColumnEdit = this.rtxtImporte;
            this.colimp_cuotaconigv.FieldName = "imp_cuotaconigv";
            this.colimp_cuotaconigv.Name = "colimp_cuotaconigv";
            this.colimp_cuotaconigv.OptionsColumn.FixedWidth = true;
            this.colimp_cuotaconigv.Visible = true;
            this.colimp_cuotaconigv.VisibleIndex = 11;
            // 
            // colimp_montopagado
            // 
            this.colimp_montopagado.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.colimp_montopagado.AppearanceCell.Options.UseForeColor = true;
            this.colimp_montopagado.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_montopagado.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_montopagado.Caption = "Monto Pagado";
            this.colimp_montopagado.ColumnEdit = this.rtxtImporte;
            this.colimp_montopagado.FieldName = "imp_montopagado";
            this.colimp_montopagado.Name = "colimp_montopagado";
            this.colimp_montopagado.OptionsColumn.FixedWidth = true;
            this.colimp_montopagado.Visible = true;
            this.colimp_montopagado.VisibleIndex = 12;
            // 
            // colimp_montoporpagar
            // 
            this.colimp_montoporpagar.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.colimp_montoporpagar.AppearanceCell.Options.UseForeColor = true;
            this.colimp_montoporpagar.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_montoporpagar.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colimp_montoporpagar.Caption = "Monto por Pagar";
            this.colimp_montoporpagar.ColumnEdit = this.rtxtImporte;
            this.colimp_montoporpagar.FieldName = "imp_montoporpagar";
            this.colimp_montoporpagar.Name = "colimp_montoporpagar";
            this.colimp_montoporpagar.OptionsColumn.FixedWidth = true;
            this.colimp_montoporpagar.Visible = true;
            this.colimp_montoporpagar.VisibleIndex = 13;
            // 
            // rtxtPorcentaje
            // 
            this.rtxtPorcentaje.AutoHeight = false;
            this.rtxtPorcentaje.Name = "rtxtPorcentaje";
            // 
            // txtMontoSolicitado
            // 
            this.txtMontoSolicitado.EditValue = "0";
            this.txtMontoSolicitado.Location = new System.Drawing.Point(102, 34);
            this.txtMontoSolicitado.MenuManager = this.ribbon;
            this.txtMontoSolicitado.Name = "txtMontoSolicitado";
            this.txtMontoSolicitado.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.txtMontoSolicitado.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtMontoSolicitado.Properties.Appearance.Options.UseFont = true;
            this.txtMontoSolicitado.Properties.Appearance.Options.UseForeColor = true;
            this.txtMontoSolicitado.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMontoSolicitado.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtMontoSolicitado.Properties.Mask.EditMask = "c2";
            this.txtMontoSolicitado.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMontoSolicitado.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontoSolicitado.Properties.UseReadOnlyAppearance = false;
            this.txtMontoSolicitado.Size = new System.Drawing.Size(113, 22);
            this.txtMontoSolicitado.StyleController = this.layoutControl1;
            this.txtMontoSolicitado.TabIndex = 0;
            // 
            // txtCuotas
            // 
            this.txtCuotas.EditValue = "12";
            this.txtCuotas.Location = new System.Drawing.Point(274, 34);
            this.txtCuotas.MenuManager = this.ribbon;
            this.txtCuotas.Name = "txtCuotas";
            this.txtCuotas.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCuotas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCuotas.Properties.Mask.EditMask = "n0";
            this.txtCuotas.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCuotas.Properties.UseReadOnlyAppearance = false;
            this.txtCuotas.Size = new System.Drawing.Size(50, 20);
            this.txtCuotas.StyleController = this.layoutControl1;
            this.txtCuotas.TabIndex = 2;
            // 
            // txtTasaAnual
            // 
            this.txtTasaAnual.EditValue = "0";
            this.txtTasaAnual.Location = new System.Drawing.Point(408, 34);
            this.txtTasaAnual.MenuManager = this.ribbon;
            this.txtTasaAnual.Name = "txtTasaAnual";
            this.txtTasaAnual.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTasaAnual.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTasaAnual.Properties.Mask.EditMask = "p2";
            this.txtTasaAnual.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTasaAnual.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTasaAnual.Properties.UseReadOnlyAppearance = false;
            this.txtTasaAnual.Size = new System.Drawing.Size(65, 20);
            this.txtTasaAnual.StyleController = this.layoutControl1;
            this.txtTasaAnual.TabIndex = 3;
            this.txtTasaAnual.EditValueChanged += new System.EventHandler(this.txtTasaAnual_EditValueChanged);
            // 
            // txtTasaMensual
            // 
            this.txtTasaMensual.EditValue = "0";
            this.txtTasaMensual.Location = new System.Drawing.Point(408, 58);
            this.txtTasaMensual.MenuManager = this.ribbon;
            this.txtTasaMensual.Name = "txtTasaMensual";
            this.txtTasaMensual.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTasaMensual.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTasaMensual.Properties.Mask.EditMask = "p2";
            this.txtTasaMensual.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTasaMensual.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTasaMensual.Properties.ReadOnly = true;
            this.txtTasaMensual.Properties.UseReadOnlyAppearance = false;
            this.txtTasaMensual.Size = new System.Drawing.Size(65, 20);
            this.txtTasaMensual.StyleController = this.layoutControl1;
            this.txtTasaMensual.TabIndex = 7;
            // 
            // btnCalcularCronograma
            // 
            this.btnCalcularCronograma.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCalcularCronograma.Appearance.Options.UseFont = true;
            this.btnCalcularCronograma.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.pivot_32x32;
            this.btnCalcularCronograma.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCalcularCronograma.ImageOptions.ImageToTextIndent = 4;
            this.btnCalcularCronograma.Location = new System.Drawing.Point(629, 37);
            this.btnCalcularCronograma.Name = "btnCalcularCronograma";
            this.btnCalcularCronograma.Size = new System.Drawing.Size(118, 40);
            this.btnCalcularCronograma.StyleController = this.layoutControl1;
            this.btnCalcularCronograma.TabIndex = 5;
            this.btnCalcularCronograma.Text = "CALCULAR";
            this.btnCalcularCronograma.Click += new System.EventHandler(this.btnCalcularCronograma_Click);
            // 
            // txtTIRM
            // 
            this.txtTIRM.EditValue = "0";
            this.txtTIRM.Location = new System.Drawing.Point(537, 58);
            this.txtTIRM.MenuManager = this.ribbon;
            this.txtTIRM.Name = "txtTIRM";
            this.txtTIRM.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTIRM.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTIRM.Properties.Mask.EditMask = "p3";
            this.txtTIRM.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTIRM.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTIRM.Properties.ReadOnly = true;
            this.txtTIRM.Properties.UseReadOnlyAppearance = false;
            this.txtTIRM.Size = new System.Drawing.Size(65, 20);
            this.txtTIRM.StyleController = this.layoutControl1;
            this.txtTIRM.TabIndex = 8;
            // 
            // txtTIRAnual
            // 
            this.txtTIRAnual.EditValue = "0";
            this.txtTIRAnual.Location = new System.Drawing.Point(537, 34);
            this.txtTIRAnual.MenuManager = this.ribbon;
            this.txtTIRAnual.Name = "txtTIRAnual";
            this.txtTIRAnual.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTIRAnual.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTIRAnual.Properties.Mask.EditMask = "p2";
            this.txtTIRAnual.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTIRAnual.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTIRAnual.Properties.ReadOnly = true;
            this.txtTIRAnual.Properties.UseReadOnlyAppearance = false;
            this.txtTIRAnual.Size = new System.Drawing.Size(65, 20);
            this.txtTIRAnual.StyleController = this.layoutControl1;
            this.txtTIRAnual.TabIndex = 4;
            // 
            // dtFechaDesembolso
            // 
            this.dtFechaDesembolso.EditValue = null;
            this.dtFechaDesembolso.Location = new System.Drawing.Point(102, 60);
            this.dtFechaDesembolso.MenuManager = this.ribbon;
            this.dtFechaDesembolso.Name = "dtFechaDesembolso";
            this.dtFechaDesembolso.Properties.Appearance.Options.UseTextOptions = true;
            this.dtFechaDesembolso.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtFechaDesembolso.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaDesembolso.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaDesembolso.Properties.UseReadOnlyAppearance = false;
            this.dtFechaDesembolso.Size = new System.Drawing.Size(113, 20);
            this.dtFechaDesembolso.StyleController = this.layoutControl1;
            this.dtFechaDesembolso.TabIndex = 9;
            this.dtFechaDesembolso.EditValueChanged += new System.EventHandler(this.dtFechaDesembolso_EditValueChanged);
            // 
            // txtDiasPago
            // 
            this.txtDiasPago.EditValue = "5";
            this.txtDiasPago.Location = new System.Drawing.Point(274, 58);
            this.txtDiasPago.MenuManager = this.ribbon;
            this.txtDiasPago.Name = "txtDiasPago";
            this.txtDiasPago.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDiasPago.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtDiasPago.Properties.Mask.EditMask = "n0";
            this.txtDiasPago.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDiasPago.Properties.UseReadOnlyAppearance = false;
            this.txtDiasPago.Size = new System.Drawing.Size(50, 20);
            this.txtDiasPago.StyleController = this.layoutControl1;
            this.txtDiasPago.TabIndex = 6;
            // 
            // btnGuardarCronograma
            // 
            this.btnGuardarCronograma.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnGuardarCronograma.Appearance.Options.UseFont = true;
            this.btnGuardarCronograma.Enabled = false;
            this.btnGuardarCronograma.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.save_32x324;
            this.btnGuardarCronograma.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnGuardarCronograma.ImageOptions.ImageToTextIndent = 4;
            this.btnGuardarCronograma.Location = new System.Drawing.Point(767, 37);
            this.btnGuardarCronograma.Name = "btnGuardarCronograma";
            this.btnGuardarCronograma.Size = new System.Drawing.Size(118, 40);
            this.btnGuardarCronograma.StyleController = this.layoutControl1;
            this.btnGuardarCronograma.TabIndex = 5;
            this.btnGuardarCronograma.Text = "GUARDAR";
            this.btnGuardarCronograma.Click += new System.EventHandler(this.btnGuardarCronograma_Click);
            // 
            // txtCodCredito
            // 
            this.txtCodCredito.EditValue = "";
            this.txtCodCredito.Location = new System.Drawing.Point(988, 34);
            this.txtCodCredito.MenuManager = this.ribbon;
            this.txtCodCredito.Name = "txtCodCredito";
            this.txtCodCredito.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtCodCredito.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtCodCredito.Properties.Appearance.Options.UseFont = true;
            this.txtCodCredito.Properties.Appearance.Options.UseForeColor = true;
            this.txtCodCredito.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCodCredito.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCodCredito.Properties.ReadOnly = true;
            this.txtCodCredito.Properties.UseReadOnlyAppearance = false;
            this.txtCodCredito.Size = new System.Drawing.Size(77, 20);
            this.txtCodCredito.StyleController = this.layoutControl1;
            this.txtCodCredito.TabIndex = 11;
            this.txtCodCredito.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodCredito_KeyDown);
            // 
            // txtNumeroPlaca
            // 
            this.txtNumeroPlaca.EditValue = "";
            this.txtNumeroPlaca.Location = new System.Drawing.Point(1134, 34);
            this.txtNumeroPlaca.MenuManager = this.ribbon;
            this.txtNumeroPlaca.Name = "txtNumeroPlaca";
            this.txtNumeroPlaca.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtNumeroPlaca.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtNumeroPlaca.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroPlaca.Properties.Appearance.Options.UseForeColor = true;
            this.txtNumeroPlaca.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNumeroPlaca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNumeroPlaca.Properties.ReadOnly = true;
            this.txtNumeroPlaca.Properties.UseReadOnlyAppearance = false;
            this.txtNumeroPlaca.Size = new System.Drawing.Size(90, 20);
            this.txtNumeroPlaca.StyleController = this.layoutControl1;
            this.txtNumeroPlaca.TabIndex = 12;
            this.txtNumeroPlaca.Leave += new System.EventHandler(this.txtNumeroPlaca_Leave);
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(988, 58);
            this.txtCliente.MenuManager = this.ribbon;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtCliente.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtCliente.Properties.Appearance.Options.UseFont = true;
            this.txtCliente.Properties.Appearance.Options.UseForeColor = true;
            this.txtCliente.Properties.ReadOnly = true;
            this.txtCliente.Properties.UseReadOnlyAppearance = false;
            this.txtCliente.Size = new System.Drawing.Size(296, 20);
            this.txtCliente.StyleController = this.layoutControl1;
            this.txtCliente.TabIndex = 13;
            // 
            // txtTotalCapital
            // 
            this.txtTotalCapital.EditValue = "100000";
            this.txtTotalCapital.Location = new System.Drawing.Point(424, 520);
            this.txtTotalCapital.MenuManager = this.ribbon;
            this.txtTotalCapital.Name = "txtTotalCapital";
            this.txtTotalCapital.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtTotalCapital.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtTotalCapital.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtTotalCapital.Properties.Appearance.Options.UseBackColor = true;
            this.txtTotalCapital.Properties.Appearance.Options.UseFont = true;
            this.txtTotalCapital.Properties.Appearance.Options.UseForeColor = true;
            this.txtTotalCapital.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtTotalCapital.Properties.Mask.EditMask = "c2";
            this.txtTotalCapital.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTotalCapital.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTotalCapital.Properties.ReadOnly = true;
            this.txtTotalCapital.Size = new System.Drawing.Size(108, 24);
            this.txtTotalCapital.StyleController = this.layoutControl1;
            this.txtTotalCapital.TabIndex = 14;
            // 
            // txtTotalCredito
            // 
            this.txtTotalCredito.EditValue = "100000";
            this.txtTotalCredito.Location = new System.Drawing.Point(893, 520);
            this.txtTotalCredito.MenuManager = this.ribbon;
            this.txtTotalCredito.Name = "txtTotalCredito";
            this.txtTotalCredito.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtTotalCredito.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtTotalCredito.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.txtTotalCredito.Properties.Appearance.Options.UseBackColor = true;
            this.txtTotalCredito.Properties.Appearance.Options.UseFont = true;
            this.txtTotalCredito.Properties.Appearance.Options.UseForeColor = true;
            this.txtTotalCredito.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtTotalCredito.Properties.Mask.EditMask = "c2";
            this.txtTotalCredito.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTotalCredito.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTotalCredito.Properties.ReadOnly = true;
            this.txtTotalCredito.Size = new System.Drawing.Size(112, 24);
            this.txtTotalCredito.StyleController = this.layoutControl1;
            this.txtTotalCredito.TabIndex = 15;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem41,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem4,
            this.layoutControlItem6,
            this.layoutControlItem3,
            this.layoutControlItem9,
            this.simpleLabelItem1,
            this.simpleLabelItem2,
            this.layoutControlItem10,
            this.emptySpaceItem1,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.emptySpaceItem5,
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.emptySpaceItem7,
            this.emptySpaceItem6,
            this.simpleLabelItem3,
            this.layoutControlItem14,
            this.simpleLabelItem4,
            this.emptySpaceItem8,
            this.layoutControlItem15});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 6);
            this.Root.Size = new System.Drawing.Size(1334, 552);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoCronograma;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 94);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1314, 414);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtMontoSolicitado;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 22);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(207, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(207, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(207, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Monto solicitado : ";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(85, 20);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtTasaAnual;
            this.layoutControlItem4.Location = new System.Drawing.Point(316, 22);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(149, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(149, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(149, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Tasa anual  : ";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(75, 13);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCalcularCronograma;
            this.layoutControlItem5.Location = new System.Drawing.Point(614, 22);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(128, 50);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(128, 50);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem5.Size = new System.Drawing.Size(128, 50);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem41
            // 
            this.layoutControlItem41.Control = this.txtTasaMensual;
            this.layoutControlItem41.Location = new System.Drawing.Point(316, 46);
            this.layoutControlItem41.MaxSize = new System.Drawing.Size(149, 26);
            this.layoutControlItem41.MinSize = new System.Drawing.Size(149, 26);
            this.layoutControlItem41.Name = "layoutControlItem41";
            this.layoutControlItem41.Size = new System.Drawing.Size(149, 26);
            this.layoutControlItem41.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem41.Text = "Tasa mensual  : ";
            this.layoutControlItem41.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem41.TextSize = new System.Drawing.Size(75, 13);
            this.layoutControlItem41.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtTIRM;
            this.layoutControlItem7.Location = new System.Drawing.Point(465, 46);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(129, 26);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(129, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(129, 26);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "TIRM : ";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(55, 13);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtTIRAnual;
            this.layoutControlItem8.Location = new System.Drawing.Point(465, 22);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(129, 24);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(129, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(129, 24);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "TIR anual  : ";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(55, 13);
            this.layoutControlItem8.TextToControlDistance = 5;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(594, 22);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(20, 50);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(20, 50);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(20, 50);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.dtFechaDesembolso;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(207, 24);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(207, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(207, 24);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "Fec. desembolso : ";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(85, 20);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtCuotas;
            this.layoutControlItem3.Location = new System.Drawing.Point(207, 22);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(109, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(109, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(109, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Plazo : ";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(50, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtDiasPago;
            this.layoutControlItem9.Location = new System.Drawing.Point(207, 46);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(109, 26);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(109, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(109, 26);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "Día pago : ";
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(50, 20);
            this.layoutControlItem9.TextToControlDistance = 5;
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
            this.simpleLabelItem1.Size = new System.Drawing.Size(1314, 22);
            this.simpleLabelItem1.Text = "Datos del cronograma";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(235, 18);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGreen;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 72);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(1314, 22);
            this.simpleLabelItem2.Text = "Cronograma de pagos";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(235, 18);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnGuardarCronograma;
            this.layoutControlItem10.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem10.Location = new System.Drawing.Point(752, 22);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(128, 50);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(128, 50);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem10.Size = new System.Drawing.Size(128, 50);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "layoutControlItem5";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(742, 22);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(10, 50);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 50);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 50);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.txtCodCredito;
            this.layoutControlItem11.Location = new System.Drawing.Point(901, 22);
            this.layoutControlItem11.MaxSize = new System.Drawing.Size(156, 24);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(156, 24);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(156, 24);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.Text = "Cod. Crédito : ";
            this.layoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(70, 20);
            this.layoutControlItem11.TextToControlDistance = 5;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.txtNumeroPlaca;
            this.layoutControlItem12.Location = new System.Drawing.Point(1057, 22);
            this.layoutControlItem12.MaxSize = new System.Drawing.Size(159, 24);
            this.layoutControlItem12.MinSize = new System.Drawing.Size(159, 24);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(159, 24);
            this.layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem12.Text = "Num. Placa : ";
            this.layoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(60, 20);
            this.layoutControlItem12.TextToControlDistance = 5;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.txtCliente;
            this.layoutControlItem13.Location = new System.Drawing.Point(901, 46);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(375, 26);
            this.layoutControlItem13.Text = "Cliente : ";
            this.layoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(70, 20);
            this.layoutControlItem13.TextToControlDistance = 5;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(1276, 22);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(38, 50);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(1216, 22);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(60, 24);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(880, 22);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(21, 0);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(21, 10);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(21, 50);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(0, 508);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(220, 28);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(997, 508);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(317, 28);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem3.Location = new System.Drawing.Point(220, 508);
            this.simpleLabelItem3.MaxSize = new System.Drawing.Size(192, 28);
            this.simpleLabelItem3.MinSize = new System.Drawing.Size(192, 28);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Size = new System.Drawing.Size(192, 28);
            this.simpleLabelItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem3.Text = "Total capital vigente : ";
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(235, 19);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.txtTotalCapital;
            this.layoutControlItem14.Location = new System.Drawing.Point(412, 508);
            this.layoutControlItem14.MaxSize = new System.Drawing.Size(112, 28);
            this.layoutControlItem14.MinSize = new System.Drawing.Size(112, 28);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(112, 28);
            this.layoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextVisible = false;
            // 
            // simpleLabelItem4
            // 
            this.simpleLabelItem4.AllowHotTrack = false;
            this.simpleLabelItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Red;
            this.simpleLabelItem4.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem4.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem4.Location = new System.Drawing.Point(642, 508);
            this.simpleLabelItem4.MaxSize = new System.Drawing.Size(239, 28);
            this.simpleLabelItem4.MinSize = new System.Drawing.Size(239, 28);
            this.simpleLabelItem4.Name = "simpleLabelItem4";
            this.simpleLabelItem4.Size = new System.Drawing.Size(239, 28);
            this.simpleLabelItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem4.Text = "Total del crédito por pagar : ";
            this.simpleLabelItem4.TextSize = new System.Drawing.Size(235, 19);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.Location = new System.Drawing.Point(524, 508);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(118, 28);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.txtTotalCredito;
            this.layoutControlItem15.Location = new System.Drawing.Point(881, 508);
            this.layoutControlItem15.MaxSize = new System.Drawing.Size(116, 28);
            this.layoutControlItem15.MinSize = new System.Drawing.Size(116, 28);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(116, 28);
            this.layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextVisible = false;
            // 
            // btnImportarPagosBBVA
            // 
            this.btnImportarPagosBBVA.Caption = "Importar Pagos BBVA";
            this.btnImportarPagosBBVA.Id = 11;
            this.btnImportarPagosBBVA.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportarPagosBBVA.ImageOptions.Image")));
            this.btnImportarPagosBBVA.Name = "btnImportarPagosBBVA";
            this.btnImportarPagosBBVA.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImportarPagosBBVA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportarPagosBBVA_ItemClick);
            // 
            // frmSimuladorCuotasFijas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 734);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmSimuladorCuotasFijas";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Simulador de pagos con cuotas fijas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSimuladorCuotasFijas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSimuladorCuotasFijas_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoCronograma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCronograma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoCronograma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoSolicitado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuotas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTasaAnual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTasaMensual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTIRM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTIRAnual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolso.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaDesembolso.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiasPago.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodCredito.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlaca.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCapital.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCredito.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcListadoCronograma;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoCronograma;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsListadoCronograma;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_cuota;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_capitalinicial;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_capitalfinal;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_amortizacion;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_interes;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_coutaigv;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_cuotasinigv;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_cuotaconigv;
        private DevExpress.XtraEditors.TextEdit txtMontoSolicitado;
        private DevExpress.XtraEditors.TextEdit txtTasaAnual;
        private DevExpress.XtraEditors.TextEdit txtTasaMensual;
        private DevExpress.XtraEditors.SimpleButton btnCalcularCronograma;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem41;
        private DevExpress.XtraEditors.TextEdit txtTIRM;
        private DevExpress.XtraEditors.TextEdit txtTIRAnual;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPorcentaje;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cuota;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_dias;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_desgravamen;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_portes;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_otros;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.DateEdit dtFechaDesembolso;
        private DevExpress.XtraEditors.TextEdit txtDiasPago;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.TextEdit txtCuotas;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.BarButtonItem btnImportarPagosCOFIDE;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoAcciones;
        private DevExpress.XtraBars.BarButtonItem btnNuevoCronograma;
        private DevExpress.XtraBars.BarButtonItem btnConsultarCronograma;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_montopagado;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_montoporpagar;
        private DevExpress.XtraBars.BarButtonItem btnAplicarPagoManual;
        private DevExpress.XtraBars.BarButtonItem btnCancelarCredito;
        private DevExpress.XtraEditors.SimpleButton btnGuardarCronograma;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit txtCodCredito;
        private DevExpress.XtraEditors.TextEdit txtNumeroPlaca;
        private DevExpress.XtraEditors.TextEdit txtCliente;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraEditors.TextEdit txtTotalCapital;
        private DevExpress.XtraEditors.TextEdit txtTotalCredito;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnEliminarCronograma;
        private DevExpress.XtraBars.BarButtonItem btnImportarPagosBBVA;
    }
}