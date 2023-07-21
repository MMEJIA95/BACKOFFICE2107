
namespace UI_BackOffice.Formularios.Bancos
{
    partial class frmListadoMovimientoBancos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoMovimientoBancos));
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition3 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition3 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan1 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan2 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan3 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement4 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement5 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.colcol_banco = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcol_defecto = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_tipo_cuenta = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_cta_bancaria = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_moneda = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_banco = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_banco = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.icmbImagenesBancos = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.igcImagenesBancos = new DevExpress.Utils.ImageCollection(this.components);
            this.colcod_moneda = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCtasBancariasEmpresa = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportarExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.btnAsignarNroOperacion = new DevExpress.XtraBars.BarButtonItem();
            this.btnAdjuntar = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.btnExportarFormatoBancos = new DevExpress.XtraBars.BarButtonItem();
            this.btnBancoScotiabank = new DevExpress.XtraBars.BarButtonItem();
            this.btnBancoBBVA = new DevExpress.XtraBars.BarButtonItem();
            this.btnBancoBCP = new DevExpress.XtraBars.BarButtonItem();
            this.btnBancoInterbank = new DevExpress.XtraBars.BarButtonItem();
            this.btnBancoBanbif = new DevExpress.XtraBars.BarButtonItem();
            this.btnBancoNacion = new DevExpress.XtraBars.BarButtonItem();
            this.btnBancoViaBCP = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminarMovimiento = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grupoReportes = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoAcciones = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grupoAccesoBancos = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoDetalleMovimientos = new DevExpress.XtraGrid.GridControl();
            this.bsListadoDetalleMovimientos = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoDetalleMovimientos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colnum_item = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_ejecutada = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rdtFecha = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colfch_efectiva = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_tipo_movimiento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rlkpTipoMovimiento = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colcod_origen_movimiento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rlkpOrigenMovimiento = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.coldsc_comentario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_monto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.coldsc_nro_operacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_bloque_pago = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rlinkCodigo = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colflg_identificado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rlkpIdentificado = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colfch_registro1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_cambio1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.btnConvertirPorDefecto = new DevExpress.XtraEditors.SimpleButton();
            this.lkpSedeEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.gcListaCuentasBancarias = new DevExpress.XtraGrid.GridControl();
            this.bsListaCuentasBancarias = new System.Windows.Forms.BindingSource(this.components);
            this.tlvListaCuentasBancarias = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.colcod_sede_empresa = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colnum_linea = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_tipo_cuenta = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_cta_interbancaria = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colflg_pago_proveedor = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colflg_pago_haberes = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_usuario_registro = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colfch_cambio = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_usuario_cambio = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_empresa = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_empresa = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_ruc = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_direccion = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_apoderado = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_database = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colflg_activo = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colflg_principal = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_telefono = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_movil = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_codigo_licencia = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colflg_independiente = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colflg_sincronizado = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colflg_agente_retencion = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_logo_empresa = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_trabajador_rep = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colflg_tipo_empresa = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_categoria = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_trabajador_rrhh = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_web = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_horario = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_ubigeo_direccion = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_urbanizacion = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_pais = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_departamento = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_provincia = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colcod_distrito = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_razon_comercial = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_dominio = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.coldsc_ruta_onedrive = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colUsuarioOnedrive = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colClaveOnedrive = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colClientIdOnedrive = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colTenantOnedrive = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colidCarpetaOnedrive = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colidRepositorioOnedrive = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colidCarpetaFacturasOnedrive = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colUsuarioOnedrivePersonal = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colClaveOnedrivePersonal = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colflg_defecto = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.icmbImagenesBancos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.igcImagenesBancos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoDetalleMovimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoDetalleMovimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoDetalleMovimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtFecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtFecha.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpTipoMovimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpOrigenMovimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlinkCodigo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpIdentificado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSedeEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaCuentasBancarias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaCuentasBancarias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlvListaCuentasBancarias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // colcol_banco
            // 
            this.colcol_banco.FieldName = "col_banco";
            this.colcol_banco.Name = "colcol_banco";
            this.colcol_banco.Visible = true;
            this.colcol_banco.VisibleIndex = 55;
            // 
            // colcol_defecto
            // 
            this.colcol_defecto.FieldName = "col_defecto";
            this.colcol_defecto.Name = "colcol_defecto";
            this.colcol_defecto.ToolTip = "Cuenta por Defecto";
            this.colcol_defecto.Visible = true;
            this.colcol_defecto.VisibleIndex = 57;
            // 
            // coldsc_tipo_cuenta
            // 
            this.coldsc_tipo_cuenta.FieldName = "dsc_tipo_cuenta";
            this.coldsc_tipo_cuenta.Name = "coldsc_tipo_cuenta";
            this.coldsc_tipo_cuenta.Visible = true;
            this.coldsc_tipo_cuenta.VisibleIndex = 7;
            // 
            // coldsc_cta_bancaria
            // 
            this.coldsc_cta_bancaria.FieldName = "dsc_cta_bancaria";
            this.coldsc_cta_bancaria.Name = "coldsc_cta_bancaria";
            this.coldsc_cta_bancaria.Visible = true;
            this.coldsc_cta_bancaria.VisibleIndex = 8;
            // 
            // coldsc_moneda
            // 
            this.coldsc_moneda.FieldName = "dsc_moneda";
            this.coldsc_moneda.Name = "coldsc_moneda";
            this.coldsc_moneda.Visible = true;
            this.coldsc_moneda.VisibleIndex = 5;
            // 
            // coldsc_banco
            // 
            this.coldsc_banco.FieldName = "dsc_banco";
            this.coldsc_banco.Name = "coldsc_banco";
            this.coldsc_banco.Visible = true;
            this.coldsc_banco.VisibleIndex = 3;
            // 
            // colcod_banco
            // 
            this.colcod_banco.FieldName = "cod_banco";
            this.colcod_banco.Name = "colcod_banco";
            this.colcod_banco.OptionsColumn.AllowEdit = false;
            this.colcod_banco.UnboundDataType = typeof(byte);
            this.colcod_banco.Visible = true;
            this.colcod_banco.VisibleIndex = 2;
            // 
            // icmbImagenesBancos
            // 
            this.icmbImagenesBancos.AutoHeight = false;
            this.icmbImagenesBancos.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbImagenesBancos.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "BA001", 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "BA002", 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "BA003", 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "BA005", 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "BA006", 4)});
            this.icmbImagenesBancos.LargeImages = this.igcImagenesBancos;
            this.icmbImagenesBancos.Name = "icmbImagenesBancos";
            // 
            // igcImagenesBancos
            // 
            this.igcImagenesBancos.ImageSize = new System.Drawing.Size(150, 30);
            this.igcImagenesBancos.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("igcImagenesBancos.ImageStream")));
            this.igcImagenesBancos.Images.SetKeyName(0, "Logo_BBVA.png");
            this.igcImagenesBancos.Images.SetKeyName(1, "Logo_BCP.png");
            this.igcImagenesBancos.Images.SetKeyName(2, "Logo_Banco.png");
            this.igcImagenesBancos.Images.SetKeyName(3, "Logo_BancoNacion.png");
            this.igcImagenesBancos.Images.SetKeyName(4, "Logo_Interbank.png");
            this.igcImagenesBancos.Images.SetKeyName(5, "Logo_Scotiabank.png");
            this.igcImagenesBancos.Images.SetKeyName(6, "apply_32x32.png");
            this.igcImagenesBancos.Images.SetKeyName(7, "Logo_BANBIF.png");
            // 
            // colcod_moneda
            // 
            this.colcod_moneda.FieldName = "cod_moneda";
            this.colcod_moneda.Name = "colcod_moneda";
            this.colcod_moneda.Visible = true;
            this.colcod_moneda.VisibleIndex = 4;
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.btnCtasBancariasEmpresa,
            this.btnExportarExcel,
            this.btnImprimir,
            this.btnAsignarNroOperacion,
            this.btnAdjuntar,
            this.barStaticItem1,
            this.btnExportarFormatoBancos,
            this.btnBancoScotiabank,
            this.btnBancoBBVA,
            this.btnBancoBCP,
            this.btnBancoInterbank,
            this.btnBancoBanbif,
            this.btnBancoNacion,
            this.btnBancoViaBCP,
            this.btnEliminarMovimiento});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 19;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1304, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 3;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // btnCtasBancariasEmpresa
            // 
            this.btnCtasBancariasEmpresa.Caption = "Ctas. Bancarias por Empresa";
            this.btnCtasBancariasEmpresa.Id = 4;
            this.btnCtasBancariasEmpresa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCtasBancariasEmpresa.ImageOptions.Image")));
            this.btnCtasBancariasEmpresa.Name = "btnCtasBancariasEmpresa";
            this.btnCtasBancariasEmpresa.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnCtasBancariasEmpresa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCtasBancariasEmpresa_ItemClick);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Caption = "Exportar Excel";
            this.btnExportarExcel.Id = 5;
            this.btnExportarExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.ImageOptions.Image")));
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarExcel_ItemClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Id = 6;
            this.btnImprimir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.ImageOptions.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnImprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimir_ItemClick);
            // 
            // btnAsignarNroOperacion
            // 
            this.btnAsignarNroOperacion.Caption = "Asignar Nro. Operación";
            this.btnAsignarNroOperacion.Id = 7;
            this.btnAsignarNroOperacion.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignarNroOperacion.ImageOptions.Image")));
            this.btnAsignarNroOperacion.Name = "btnAsignarNroOperacion";
            this.btnAsignarNroOperacion.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAsignarNroOperacion.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAsignarNroOperacion_ItemClick);
            // 
            // btnAdjuntar
            // 
            this.btnAdjuntar.Caption = "Adjuntar Voucher";
            this.btnAdjuntar.Id = 8;
            this.btnAdjuntar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdjuntar.ImageOptions.Image")));
            this.btnAdjuntar.Name = "btnAdjuntar";
            this.btnAdjuntar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnAdjuntar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdjuntar_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Presione F5 para actualizar listado";
            this.barStaticItem1.Id = 9;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // btnExportarFormatoBancos
            // 
            this.btnExportarFormatoBancos.Caption = "Exportar Formato Bancos";
            this.btnExportarFormatoBancos.Id = 10;
            this.btnExportarFormatoBancos.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarFormatoBancos.ImageOptions.Image")));
            this.btnExportarFormatoBancos.Name = "btnExportarFormatoBancos";
            this.btnExportarFormatoBancos.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExportarFormatoBancos.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportarFormatoBancos_ItemClick);
            // 
            // btnBancoScotiabank
            // 
            this.btnBancoScotiabank.Caption = "Scotiabank";
            this.btnBancoScotiabank.Id = 11;
            this.btnBancoScotiabank.ImageOptions.LargeImage = global::UI_BackOffice.Properties.Resources.Logo_Scotiabank;
            this.btnBancoScotiabank.Name = "btnBancoScotiabank";
            this.btnBancoScotiabank.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnBancoScotiabank.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBancoScotiabank_ItemClick);
            // 
            // btnBancoBBVA
            // 
            this.btnBancoBBVA.Caption = "BBVA";
            this.btnBancoBBVA.Id = 12;
            this.btnBancoBBVA.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.Logo_BBVA;
            this.btnBancoBBVA.Name = "btnBancoBBVA";
            this.btnBancoBBVA.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnBancoBBVA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBancoBBVA_ItemClick);
            // 
            // btnBancoBCP
            // 
            this.btnBancoBCP.Caption = "Telecrédito BCP";
            this.btnBancoBCP.Id = 13;
            this.btnBancoBCP.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.Logo_TeleBCP;
            this.btnBancoBCP.Name = "btnBancoBCP";
            this.btnBancoBCP.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnBancoBCP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBancoBCP_ItemClick);
            // 
            // btnBancoInterbank
            // 
            this.btnBancoInterbank.Caption = "Interbank";
            this.btnBancoInterbank.Id = 14;
            this.btnBancoInterbank.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.Logo_Interbank;
            this.btnBancoInterbank.Name = "btnBancoInterbank";
            this.btnBancoInterbank.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnBancoInterbank.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBancoInterbank_ItemClick);
            // 
            // btnBancoBanbif
            // 
            this.btnBancoBanbif.Caption = "Banbif";
            this.btnBancoBanbif.Id = 15;
            this.btnBancoBanbif.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBancoBanbif.ImageOptions.Image")));
            this.btnBancoBanbif.Name = "btnBancoBanbif";
            this.btnBancoBanbif.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnBancoBanbif.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBancoBanbif_ItemClick);
            // 
            // btnBancoNacion
            // 
            this.btnBancoNacion.Caption = "Banco de la Nación";
            this.btnBancoNacion.Id = 16;
            this.btnBancoNacion.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.Logo_BancoNacion;
            this.btnBancoNacion.Name = "btnBancoNacion";
            this.btnBancoNacion.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnBancoNacion.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBancoNacion_ItemClick);
            // 
            // btnBancoViaBCP
            // 
            this.btnBancoViaBCP.Caption = "Vía BCP";
            this.btnBancoViaBCP.Id = 17;
            this.btnBancoViaBCP.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.Logo_BCP;
            this.btnBancoViaBCP.Name = "btnBancoViaBCP";
            this.btnBancoViaBCP.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnBancoViaBCP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBancoViaBCP_ItemClick);
            // 
            // btnEliminarMovimiento
            // 
            this.btnEliminarMovimiento.Caption = "Eliminar Movimiento";
            this.btnEliminarMovimiento.Id = 18;
            this.btnEliminarMovimiento.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarDetalleBanco.ImageOptions.Image")));
            this.btnEliminarMovimiento.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEliminarDetalleBanco.ImageOptions.LargeImage")));
            this.btnEliminarMovimiento.Name = "btnEliminarMovimiento";
            this.btnEliminarMovimiento.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnEliminarMovimiento.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminarMovimiento_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grupoReportes,
            this.grupoAcciones,
            this.grupoAccesoBancos});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones de Listado Movimientos de Bancos";
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
            this.grupoAcciones.ItemLinks.Add(this.btnCtasBancariasEmpresa);
            this.grupoAcciones.ItemLinks.Add(this.btnAsignarNroOperacion);
            this.grupoAcciones.ItemLinks.Add(this.btnAdjuntar);
            this.grupoAcciones.ItemLinks.Add(this.btnExportarFormatoBancos);
            this.grupoAcciones.ItemLinks.Add(this.btnEliminarMovimiento);
            this.grupoAcciones.Name = "grupoAcciones";
            this.grupoAcciones.Text = "Acciones";
            // 
            // grupoAccesoBancos
            // 
            this.grupoAccesoBancos.ItemLinks.Add(this.btnBancoScotiabank);
            this.grupoAccesoBancos.ItemLinks.Add(this.btnBancoBBVA);
            this.grupoAccesoBancos.ItemLinks.Add(this.btnBancoBCP);
            this.grupoAccesoBancos.ItemLinks.Add(this.btnBancoViaBCP);
            this.grupoAccesoBancos.ItemLinks.Add(this.btnBancoInterbank);
            this.grupoAccesoBancos.ItemLinks.Add(this.btnBancoBanbif);
            this.grupoAccesoBancos.ItemLinks.Add(this.btnBancoNacion);
            this.grupoAccesoBancos.Name = "grupoAccesoBancos";
            this.grupoAccesoBancos.Text = "Acceso Bancos";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem1);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 839);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1304, 24);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListadoDetalleMovimientos);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 158);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1304, 681);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListadoDetalleMovimientos
            // 
            this.gcListadoDetalleMovimientos.DataSource = this.bsListadoDetalleMovimientos;
            this.gcListadoDetalleMovimientos.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoDetalleMovimientos.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoDetalleMovimientos.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoDetalleMovimientos.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoDetalleMovimientos.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoDetalleMovimientos.Location = new System.Drawing.Point(285, 8);
            this.gcListadoDetalleMovimientos.MainView = this.gvListadoDetalleMovimientos;
            this.gcListadoDetalleMovimientos.MenuManager = this.ribbon;
            this.gcListadoDetalleMovimientos.Name = "gcListadoDetalleMovimientos";
            this.gcListadoDetalleMovimientos.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rlkpTipoMovimiento,
            this.rtxtImporte,
            this.rdtFecha,
            this.rlkpOrigenMovimiento,
            this.rlkpIdentificado,
            this.rlinkCodigo});
            this.gcListadoDetalleMovimientos.Size = new System.Drawing.Size(1011, 665);
            this.gcListadoDetalleMovimientos.TabIndex = 6;
            this.gcListadoDetalleMovimientos.UseEmbeddedNavigator = true;
            this.gcListadoDetalleMovimientos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoDetalleMovimientos});
            // 
            // bsListadoDetalleMovimientos
            // 
            this.bsListadoDetalleMovimientos.DataSource = typeof(BE_BackOffice.eEmpresa.eDetalleMovimientoBanco_Empresa);
            // 
            // gvListadoDetalleMovimientos
            // 
            this.gvListadoDetalleMovimientos.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListadoDetalleMovimientos.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvListadoDetalleMovimientos.Appearance.FooterPanel.Options.UseFont = true;
            this.gvListadoDetalleMovimientos.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvListadoDetalleMovimientos.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListadoDetalleMovimientos.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListadoDetalleMovimientos.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoDetalleMovimientos.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoDetalleMovimientos.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvListadoDetalleMovimientos.ColumnPanelRowHeight = 35;
            this.gvListadoDetalleMovimientos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colnum_item,
            this.colfch_ejecutada,
            this.colfch_efectiva,
            this.colcod_tipo_movimiento,
            this.colcod_origen_movimiento,
            this.coldsc_comentario,
            this.colimp_monto,
            this.coldsc_nro_operacion,
            this.colcod_bloque_pago,
            this.colflg_identificado,
            this.colfch_registro1,
            this.colfch_cambio1});
            this.gvListadoDetalleMovimientos.GridControl = this.gcListadoDetalleMovimientos;
            this.gvListadoDetalleMovimientos.Name = "gvListadoDetalleMovimientos";
            this.gvListadoDetalleMovimientos.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoDetalleMovimientos.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvListadoDetalleMovimientos.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoDetalleMovimientos.OptionsView.ShowFooter = true;
            this.gvListadoDetalleMovimientos.OptionsView.ShowIndicator = false;
            this.gvListadoDetalleMovimientos.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colfch_efectiva, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvListadoDetalleMovimientos.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvListadoDetalleMovimientos_RowCellClick);
            this.gvListadoDetalleMovimientos.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoDetalleMovimientos_CustomDrawColumnHeader);
            this.gvListadoDetalleMovimientos.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvListadoDetalleMovimientos_RowCellStyle);
            this.gvListadoDetalleMovimientos.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoDetalleMovimientos_RowStyle);
            this.gvListadoDetalleMovimientos.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvListadoDetalleMovimientos_InitNewRow);
            this.gvListadoDetalleMovimientos.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvListadoDetalleMovimientos_ValidateRow);
            // 
            // colnum_item
            // 
            this.colnum_item.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_item.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_item.Caption = "N°";
            this.colnum_item.FieldName = "num_item";
            this.colnum_item.Name = "colnum_item";
            this.colnum_item.OptionsColumn.AllowEdit = false;
            this.colnum_item.OptionsColumn.FixedWidth = true;
            this.colnum_item.Visible = true;
            this.colnum_item.VisibleIndex = 0;
            this.colnum_item.Width = 40;
            // 
            // colfch_ejecutada
            // 
            this.colfch_ejecutada.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_ejecutada.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_ejecutada.Caption = "Fecha de Operación";
            this.colfch_ejecutada.ColumnEdit = this.rdtFecha;
            this.colfch_ejecutada.FieldName = "fch_ejecutada";
            this.colfch_ejecutada.Name = "colfch_ejecutada";
            this.colfch_ejecutada.OptionsColumn.FixedWidth = true;
            this.colfch_ejecutada.Visible = true;
            this.colfch_ejecutada.VisibleIndex = 1;
            this.colfch_ejecutada.Width = 80;
            // 
            // rdtFecha
            // 
            this.rdtFecha.AutoHeight = false;
            this.rdtFecha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rdtFecha.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rdtFecha.Name = "rdtFecha";
            // 
            // colfch_efectiva
            // 
            this.colfch_efectiva.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_efectiva.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_efectiva.Caption = "Fecha de Proceso";
            this.colfch_efectiva.ColumnEdit = this.rdtFecha;
            this.colfch_efectiva.FieldName = "fch_efectiva";
            this.colfch_efectiva.Name = "colfch_efectiva";
            this.colfch_efectiva.OptionsColumn.FixedWidth = true;
            this.colfch_efectiva.Visible = true;
            this.colfch_efectiva.VisibleIndex = 2;
            this.colfch_efectiva.Width = 80;
            // 
            // colcod_tipo_movimiento
            // 
            this.colcod_tipo_movimiento.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_tipo_movimiento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_tipo_movimiento.Caption = "Tipo Movimiento";
            this.colcod_tipo_movimiento.ColumnEdit = this.rlkpTipoMovimiento;
            this.colcod_tipo_movimiento.FieldName = "cod_tipo_movimiento";
            this.colcod_tipo_movimiento.Name = "colcod_tipo_movimiento";
            this.colcod_tipo_movimiento.OptionsColumn.FixedWidth = true;
            this.colcod_tipo_movimiento.Visible = true;
            this.colcod_tipo_movimiento.VisibleIndex = 3;
            this.colcod_tipo_movimiento.Width = 80;
            // 
            // rlkpTipoMovimiento
            // 
            this.rlkpTipoMovimiento.AutoHeight = false;
            this.rlkpTipoMovimiento.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rlkpTipoMovimiento.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_tipo_movimiento", "Descripción")});
            this.rlkpTipoMovimiento.DisplayMember = "dsc_tipo_movimiento";
            this.rlkpTipoMovimiento.Name = "rlkpTipoMovimiento";
            this.rlkpTipoMovimiento.ValueMember = "cod_tipo_movimiento";
            this.rlkpTipoMovimiento.EditValueChanged += new System.EventHandler(this.rlkpTipoMovimiento_EditValueChanged);
            // 
            // colcod_origen_movimiento
            // 
            this.colcod_origen_movimiento.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_origen_movimiento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_origen_movimiento.Caption = "Origen Movimiento";
            this.colcod_origen_movimiento.ColumnEdit = this.rlkpOrigenMovimiento;
            this.colcod_origen_movimiento.FieldName = "cod_origen_movimiento";
            this.colcod_origen_movimiento.Name = "colcod_origen_movimiento";
            this.colcod_origen_movimiento.OptionsColumn.FixedWidth = true;
            this.colcod_origen_movimiento.Visible = true;
            this.colcod_origen_movimiento.VisibleIndex = 4;
            this.colcod_origen_movimiento.Width = 120;
            // 
            // rlkpOrigenMovimiento
            // 
            this.rlkpOrigenMovimiento.AutoHeight = false;
            this.rlkpOrigenMovimiento.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rlkpOrigenMovimiento.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_origen_movimiento", "Descripción")});
            this.rlkpOrigenMovimiento.DisplayMember = "dsc_origen_movimiento";
            this.rlkpOrigenMovimiento.Name = "rlkpOrigenMovimiento";
            this.rlkpOrigenMovimiento.ValueMember = "cod_origen_movimiento";
            // 
            // coldsc_comentario
            // 
            this.coldsc_comentario.Caption = "Comentario";
            this.coldsc_comentario.FieldName = "dsc_comentario";
            this.coldsc_comentario.Name = "coldsc_comentario";
            this.coldsc_comentario.OptionsColumn.FixedWidth = true;
            this.coldsc_comentario.Visible = true;
            this.coldsc_comentario.VisibleIndex = 5;
            this.coldsc_comentario.Width = 250;
            // 
            // colimp_monto
            // 
            this.colimp_monto.Caption = "Monto";
            this.colimp_monto.ColumnEdit = this.rtxtImporte;
            this.colimp_monto.FieldName = "imp_monto";
            this.colimp_monto.Name = "colimp_monto";
            this.colimp_monto.OptionsColumn.FixedWidth = true;
            this.colimp_monto.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_monto", "{0:#,#.00}")});
            this.colimp_monto.Visible = true;
            this.colimp_monto.VisibleIndex = 6;
            this.colimp_monto.Width = 70;
            // 
            // rtxtImporte
            // 
            this.rtxtImporte.AutoHeight = false;
            this.rtxtImporte.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.rtxtImporte.MaskSettings.Set("mask", "n2");
            this.rtxtImporte.MaskSettings.Set("culture", "es-PE");
            this.rtxtImporte.Name = "rtxtImporte";
            this.rtxtImporte.UseMaskAsDisplayFormat = true;
            // 
            // coldsc_nro_operacion
            // 
            this.coldsc_nro_operacion.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.coldsc_nro_operacion.AppearanceCell.Options.UseFont = true;
            this.coldsc_nro_operacion.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_nro_operacion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_nro_operacion.Caption = "Nro Operación";
            this.coldsc_nro_operacion.FieldName = "dsc_nro_operacion";
            this.coldsc_nro_operacion.Name = "coldsc_nro_operacion";
            this.coldsc_nro_operacion.OptionsColumn.AllowEdit = false;
            this.coldsc_nro_operacion.OptionsColumn.FixedWidth = true;
            this.coldsc_nro_operacion.Visible = true;
            this.coldsc_nro_operacion.VisibleIndex = 7;
            this.coldsc_nro_operacion.Width = 80;
            // 
            // colcod_bloque_pago
            // 
            this.colcod_bloque_pago.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colcod_bloque_pago.AppearanceCell.Options.UseFont = true;
            this.colcod_bloque_pago.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_bloque_pago.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_bloque_pago.Caption = "Cod. Bloque Pago";
            this.colcod_bloque_pago.ColumnEdit = this.rlinkCodigo;
            this.colcod_bloque_pago.FieldName = "cod_bloque_pago";
            this.colcod_bloque_pago.Name = "colcod_bloque_pago";
            this.colcod_bloque_pago.OptionsColumn.AllowEdit = false;
            this.colcod_bloque_pago.OptionsColumn.FixedWidth = true;
            this.colcod_bloque_pago.Visible = true;
            this.colcod_bloque_pago.VisibleIndex = 8;
            this.colcod_bloque_pago.Width = 50;
            // 
            // rlinkCodigo
            // 
            this.rlinkCodigo.AutoHeight = false;
            this.rlinkCodigo.Name = "rlinkCodigo";
            // 
            // colflg_identificado
            // 
            this.colflg_identificado.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_identificado.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_identificado.Caption = "Identificado";
            this.colflg_identificado.ColumnEdit = this.rlkpIdentificado;
            this.colflg_identificado.FieldName = "flg_identificado";
            this.colflg_identificado.Name = "colflg_identificado";
            this.colflg_identificado.OptionsColumn.FixedWidth = true;
            this.colflg_identificado.Visible = true;
            this.colflg_identificado.VisibleIndex = 9;
            this.colflg_identificado.Width = 50;
            // 
            // rlkpIdentificado
            // 
            this.rlkpIdentificado.AutoHeight = false;
            this.rlkpIdentificado.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rlkpIdentificado.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("flg_identificado", "Descripción")});
            this.rlkpIdentificado.DisplayMember = "flg_identificado";
            this.rlkpIdentificado.Name = "rlkpIdentificado";
            this.rlkpIdentificado.ValueMember = "flg_identificado";
            // 
            // colfch_registro1
            // 
            this.colfch_registro1.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_registro1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro1.Caption = "Fec. Registro";
            this.colfch_registro1.FieldName = "fch_registro";
            this.colfch_registro1.Name = "colfch_registro1";
            this.colfch_registro1.OptionsColumn.FixedWidth = true;
            this.colfch_registro1.Width = 80;
            // 
            // colfch_cambio1
            // 
            this.colfch_cambio1.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_cambio1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambio1.Caption = "Fec. Cambio";
            this.colfch_cambio1.FieldName = "fch_cambio";
            this.colfch_cambio1.Name = "colfch_cambio1";
            this.colfch_cambio1.OptionsColumn.FixedWidth = true;
            this.colfch_cambio1.Width = 80;
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.Controls.Add(this.layoutControl2);
            this.groupControl1.Location = new System.Drawing.Point(8, 8);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(273, 665);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Empresa / Sede";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.btnConvertirPorDefecto);
            this.layoutControl2.Controls.Add(this.lkpSedeEmpresa);
            this.layoutControl2.Controls.Add(this.lkpEmpresa);
            this.layoutControl2.Controls.Add(this.gcListaCuentasBancarias);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 23);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(269, 640);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // btnConvertirPorDefecto
            // 
            this.btnConvertirPorDefecto.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnConvertirPorDefecto.Appearance.Options.UseFont = true;
            this.btnConvertirPorDefecto.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnConvertirPorDefecto.ImageOptions.Image")));
            this.btnConvertirPorDefecto.Location = new System.Drawing.Point(175, 50);
            this.btnConvertirPorDefecto.Name = "btnConvertirPorDefecto";
            this.btnConvertirPorDefecto.Size = new System.Drawing.Size(92, 22);
            this.btnConvertirPorDefecto.StyleController = this.layoutControl2;
            this.btnConvertirPorDefecto.TabIndex = 48;
            this.btnConvertirPorDefecto.Text = "Por Defecto";
            this.btnConvertirPorDefecto.ToolTip = "Cuenta por defecto para Pago transferencia";
            this.btnConvertirPorDefecto.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnConvertirPorDefecto.Click += new System.EventHandler(this.btnConvertirPorDefecto_Click);
            // 
            // lkpSedeEmpresa
            // 
            this.lkpSedeEmpresa.Location = new System.Drawing.Point(2, 26);
            this.lkpSedeEmpresa.Name = "lkpSedeEmpresa";
            this.lkpSedeEmpresa.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lkpSedeEmpresa.Properties.Appearance.Options.UseForeColor = true;
            this.lkpSedeEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSedeEmpresa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_sede_empresa", "Descripción")});
            this.lkpSedeEmpresa.Properties.NullText = "";
            this.lkpSedeEmpresa.Properties.UseReadOnlyAppearance = false;
            this.lkpSedeEmpresa.Size = new System.Drawing.Size(265, 20);
            this.lkpSedeEmpresa.StyleController = this.layoutControl2;
            this.lkpSedeEmpresa.TabIndex = 47;
            this.lkpSedeEmpresa.EditValueChanged += new System.EventHandler(this.lkpSedeEmpresa_EditValueChanged);
            // 
            // lkpEmpresa
            // 
            this.lkpEmpresa.Location = new System.Drawing.Point(2, 2);
            this.lkpEmpresa.Name = "lkpEmpresa";
            this.lkpEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpEmpresa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_empresa", "Descripción")});
            this.lkpEmpresa.Properties.DisplayMember = "dsc_empresa";
            this.lkpEmpresa.Properties.DropDownRows = 12;
            this.lkpEmpresa.Properties.NullText = "";
            this.lkpEmpresa.Properties.ValueMember = "cod_empresa";
            this.lkpEmpresa.Size = new System.Drawing.Size(265, 20);
            this.lkpEmpresa.StyleController = this.layoutControl2;
            this.lkpEmpresa.TabIndex = 0;
            this.lkpEmpresa.EditValueChanged += new System.EventHandler(this.lkpEmpresa_EditValueChanged);
            // 
            // gcListaCuentasBancarias
            // 
            this.gcListaCuentasBancarias.DataSource = this.bsListaCuentasBancarias;
            this.gcListaCuentasBancarias.Location = new System.Drawing.Point(2, 76);
            this.gcListaCuentasBancarias.MainView = this.tlvListaCuentasBancarias;
            this.gcListaCuentasBancarias.MenuManager = this.ribbon;
            this.gcListaCuentasBancarias.Name = "gcListaCuentasBancarias";
            this.gcListaCuentasBancarias.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.icmbImagenesBancos});
            this.gcListaCuentasBancarias.Size = new System.Drawing.Size(265, 562);
            this.gcListaCuentasBancarias.TabIndex = 2;
            this.gcListaCuentasBancarias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tlvListaCuentasBancarias});
            // 
            // bsListaCuentasBancarias
            // 
            this.bsListaCuentasBancarias.DataSource = typeof(BE_BackOffice.eEmpresa.eBanco_Empresa);
            // 
            // tlvListaCuentasBancarias
            // 
            this.tlvListaCuentasBancarias.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_sede_empresa,
            this.colnum_linea,
            this.colcod_banco,
            this.coldsc_banco,
            this.colcod_moneda,
            this.coldsc_moneda,
            this.colcod_tipo_cuenta,
            this.coldsc_tipo_cuenta,
            this.coldsc_cta_bancaria,
            this.coldsc_cta_interbancaria,
            this.colflg_pago_proveedor,
            this.colflg_pago_haberes,
            this.colfch_registro,
            this.colcod_usuario_registro,
            this.colfch_cambio,
            this.colcod_usuario_cambio,
            this.colcod_empresa,
            this.coldsc_empresa,
            this.coldsc_ruc,
            this.coldsc_direccion,
            this.coldsc_apoderado,
            this.coldsc_database,
            this.colflg_activo,
            this.colflg_principal,
            this.coldsc_telefono,
            this.coldsc_movil,
            this.coldsc_codigo_licencia,
            this.colflg_independiente,
            this.colflg_sincronizado,
            this.colflg_agente_retencion,
            this.coldsc_logo_empresa,
            this.colcod_trabajador_rep,
            this.colflg_tipo_empresa,
            this.colcod_categoria,
            this.colcod_trabajador_rrhh,
            this.coldsc_web,
            this.coldsc_horario,
            this.coldsc_ubigeo_direccion,
            this.coldsc_urbanizacion,
            this.colcod_pais,
            this.colcod_departamento,
            this.colcod_provincia,
            this.colcod_distrito,
            this.coldsc_razon_comercial,
            this.coldsc_dominio,
            this.coldsc_ruta_onedrive,
            this.colUsuarioOnedrive,
            this.colClaveOnedrive,
            this.colClientIdOnedrive,
            this.colTenantOnedrive,
            this.colidCarpetaOnedrive,
            this.colidRepositorioOnedrive,
            this.colidCarpetaFacturasOnedrive,
            this.colUsuarioOnedrivePersonal,
            this.colClaveOnedrivePersonal,
            this.colcol_banco,
            this.colflg_defecto,
            this.colcol_defecto});
            this.tlvListaCuentasBancarias.GridControl = this.gcListaCuentasBancarias;
            this.tlvListaCuentasBancarias.Name = "tlvListaCuentasBancarias";
            this.tlvListaCuentasBancarias.OptionsTiles.GroupTextPadding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.tlvListaCuentasBancarias.OptionsTiles.IndentBetweenGroups = 0;
            this.tlvListaCuentasBancarias.OptionsTiles.IndentBetweenItems = 0;
            this.tlvListaCuentasBancarias.OptionsTiles.ItemSize = new System.Drawing.Size(222, 92);
            this.tlvListaCuentasBancarias.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List;
            this.tlvListaCuentasBancarias.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tlvListaCuentasBancarias.OptionsTiles.Padding = new System.Windows.Forms.Padding(4);
            this.tlvListaCuentasBancarias.OptionsTiles.RowCount = 0;
            tableColumnDefinition1.Length.Value = 125D;
            tableColumnDefinition2.Length.Value = 41D;
            tableColumnDefinition3.Length.Value = 32D;
            this.tlvListaCuentasBancarias.TileColumns.Add(tableColumnDefinition1);
            this.tlvListaCuentasBancarias.TileColumns.Add(tableColumnDefinition2);
            this.tlvListaCuentasBancarias.TileColumns.Add(tableColumnDefinition3);
            tableRowDefinition1.Length.Value = 36D;
            tableRowDefinition2.Length.Value = 22D;
            tableRowDefinition3.Length.Value = 18D;
            this.tlvListaCuentasBancarias.TileRows.Add(tableRowDefinition1);
            this.tlvListaCuentasBancarias.TileRows.Add(tableRowDefinition2);
            this.tlvListaCuentasBancarias.TileRows.Add(tableRowDefinition3);
            tableSpan1.ColumnSpan = 2;
            tableSpan2.ColumnIndex = 1;
            tableSpan2.ColumnSpan = 2;
            tableSpan2.RowIndex = 2;
            tableSpan3.ColumnSpan = 3;
            tableSpan3.RowIndex = 1;
            this.tlvListaCuentasBancarias.TileSpans.Add(tableSpan1);
            this.tlvListaCuentasBancarias.TileSpans.Add(tableSpan2);
            this.tlvListaCuentasBancarias.TileSpans.Add(tableSpan3);
            tileViewItemElement1.Column = this.colcol_banco;
            tileViewItemElement1.ColumnIndex = 1;
            tileViewItemElement1.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement1.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement1.Text = "colcol_banco";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            tileViewItemElement2.Column = this.colcol_defecto;
            tileViewItemElement2.ColumnIndex = 2;
            tileViewItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch;
            tileViewItemElement2.Text = "colcol_defecto";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            tileViewItemElement3.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            tileViewItemElement3.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            tileViewItemElement3.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement3.Appearance.Normal.Options.UseForeColor = true;
            tileViewItemElement3.Column = this.coldsc_tipo_cuenta;
            tileViewItemElement3.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement3.RowIndex = 1;
            tileViewItemElement3.Text = "coldsc_tipo_cuenta";
            tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement4.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 10F);
            tileViewItemElement4.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement4.Column = this.coldsc_cta_bancaria;
            tileViewItemElement4.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement4.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement4.RowIndex = 2;
            tileViewItemElement4.Text = "coldsc_cta_bancaria";
            tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            tileViewItemElement5.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            tileViewItemElement5.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement5.Column = this.coldsc_moneda;
            tileViewItemElement5.ColumnIndex = 1;
            tileViewItemElement5.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement5.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement5.RowIndex = 2;
            tileViewItemElement5.Text = "coldsc_moneda";
            tileViewItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tlvListaCuentasBancarias.TileTemplate.Add(tileViewItemElement1);
            this.tlvListaCuentasBancarias.TileTemplate.Add(tileViewItemElement2);
            this.tlvListaCuentasBancarias.TileTemplate.Add(tileViewItemElement3);
            this.tlvListaCuentasBancarias.TileTemplate.Add(tileViewItemElement4);
            this.tlvListaCuentasBancarias.TileTemplate.Add(tileViewItemElement5);
            this.tlvListaCuentasBancarias.ItemClick += new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.tlvListaCuentasBancarias_ItemClick);
            this.tlvListaCuentasBancarias.ItemDoubleClick += new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.tlvListaCuentasBancarias_ItemDoubleClick);
            this.tlvListaCuentasBancarias.ItemCustomize += new DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventHandler(this.tlvListaCuentasBancarias_ItemCustomize);
            this.tlvListaCuentasBancarias.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.tlvListaCuentasBancarias_FocusedRowChanged);
            // 
            // colcod_sede_empresa
            // 
            this.colcod_sede_empresa.FieldName = "cod_sede_empresa";
            this.colcod_sede_empresa.Name = "colcod_sede_empresa";
            this.colcod_sede_empresa.Visible = true;
            this.colcod_sede_empresa.VisibleIndex = 0;
            // 
            // colnum_linea
            // 
            this.colnum_linea.FieldName = "num_linea";
            this.colnum_linea.Name = "colnum_linea";
            this.colnum_linea.Visible = true;
            this.colnum_linea.VisibleIndex = 1;
            // 
            // colcod_tipo_cuenta
            // 
            this.colcod_tipo_cuenta.FieldName = "cod_tipo_cuenta";
            this.colcod_tipo_cuenta.Name = "colcod_tipo_cuenta";
            this.colcod_tipo_cuenta.Visible = true;
            this.colcod_tipo_cuenta.VisibleIndex = 6;
            // 
            // coldsc_cta_interbancaria
            // 
            this.coldsc_cta_interbancaria.FieldName = "dsc_cta_interbancaria";
            this.coldsc_cta_interbancaria.Name = "coldsc_cta_interbancaria";
            this.coldsc_cta_interbancaria.Visible = true;
            this.coldsc_cta_interbancaria.VisibleIndex = 9;
            // 
            // colflg_pago_proveedor
            // 
            this.colflg_pago_proveedor.FieldName = "flg_pago_proveedor";
            this.colflg_pago_proveedor.Name = "colflg_pago_proveedor";
            this.colflg_pago_proveedor.Visible = true;
            this.colflg_pago_proveedor.VisibleIndex = 10;
            // 
            // colflg_pago_haberes
            // 
            this.colflg_pago_haberes.FieldName = "flg_pago_haberes";
            this.colflg_pago_haberes.Name = "colflg_pago_haberes";
            this.colflg_pago_haberes.Visible = true;
            this.colflg_pago_haberes.VisibleIndex = 11;
            // 
            // colfch_registro
            // 
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.Name = "colfch_registro";
            this.colfch_registro.Visible = true;
            this.colfch_registro.VisibleIndex = 12;
            // 
            // colcod_usuario_registro
            // 
            this.colcod_usuario_registro.FieldName = "cod_usuario_registro";
            this.colcod_usuario_registro.Name = "colcod_usuario_registro";
            this.colcod_usuario_registro.Visible = true;
            this.colcod_usuario_registro.VisibleIndex = 13;
            // 
            // colfch_cambio
            // 
            this.colfch_cambio.FieldName = "fch_cambio";
            this.colfch_cambio.Name = "colfch_cambio";
            this.colfch_cambio.Visible = true;
            this.colfch_cambio.VisibleIndex = 14;
            // 
            // colcod_usuario_cambio
            // 
            this.colcod_usuario_cambio.FieldName = "cod_usuario_cambio";
            this.colcod_usuario_cambio.Name = "colcod_usuario_cambio";
            this.colcod_usuario_cambio.Visible = true;
            this.colcod_usuario_cambio.VisibleIndex = 15;
            // 
            // colcod_empresa
            // 
            this.colcod_empresa.FieldName = "cod_empresa";
            this.colcod_empresa.Name = "colcod_empresa";
            this.colcod_empresa.Visible = true;
            this.colcod_empresa.VisibleIndex = 16;
            // 
            // coldsc_empresa
            // 
            this.coldsc_empresa.FieldName = "dsc_empresa";
            this.coldsc_empresa.Name = "coldsc_empresa";
            this.coldsc_empresa.Visible = true;
            this.coldsc_empresa.VisibleIndex = 17;
            // 
            // coldsc_ruc
            // 
            this.coldsc_ruc.FieldName = "dsc_ruc";
            this.coldsc_ruc.Name = "coldsc_ruc";
            this.coldsc_ruc.Visible = true;
            this.coldsc_ruc.VisibleIndex = 18;
            // 
            // coldsc_direccion
            // 
            this.coldsc_direccion.FieldName = "dsc_direccion";
            this.coldsc_direccion.Name = "coldsc_direccion";
            this.coldsc_direccion.Visible = true;
            this.coldsc_direccion.VisibleIndex = 19;
            // 
            // coldsc_apoderado
            // 
            this.coldsc_apoderado.FieldName = "dsc_apoderado";
            this.coldsc_apoderado.Name = "coldsc_apoderado";
            this.coldsc_apoderado.Visible = true;
            this.coldsc_apoderado.VisibleIndex = 20;
            // 
            // coldsc_database
            // 
            this.coldsc_database.FieldName = "dsc_database";
            this.coldsc_database.Name = "coldsc_database";
            this.coldsc_database.Visible = true;
            this.coldsc_database.VisibleIndex = 21;
            // 
            // colflg_activo
            // 
            this.colflg_activo.FieldName = "flg_activo";
            this.colflg_activo.Name = "colflg_activo";
            this.colflg_activo.Visible = true;
            this.colflg_activo.VisibleIndex = 22;
            // 
            // colflg_principal
            // 
            this.colflg_principal.FieldName = "flg_principal";
            this.colflg_principal.Name = "colflg_principal";
            this.colflg_principal.Visible = true;
            this.colflg_principal.VisibleIndex = 23;
            // 
            // coldsc_telefono
            // 
            this.coldsc_telefono.FieldName = "dsc_telefono";
            this.coldsc_telefono.Name = "coldsc_telefono";
            this.coldsc_telefono.Visible = true;
            this.coldsc_telefono.VisibleIndex = 24;
            // 
            // coldsc_movil
            // 
            this.coldsc_movil.FieldName = "dsc_movil";
            this.coldsc_movil.Name = "coldsc_movil";
            this.coldsc_movil.Visible = true;
            this.coldsc_movil.VisibleIndex = 25;
            // 
            // coldsc_codigo_licencia
            // 
            this.coldsc_codigo_licencia.FieldName = "dsc_codigo_licencia";
            this.coldsc_codigo_licencia.Name = "coldsc_codigo_licencia";
            this.coldsc_codigo_licencia.Visible = true;
            this.coldsc_codigo_licencia.VisibleIndex = 26;
            // 
            // colflg_independiente
            // 
            this.colflg_independiente.FieldName = "flg_independiente";
            this.colflg_independiente.Name = "colflg_independiente";
            this.colflg_independiente.Visible = true;
            this.colflg_independiente.VisibleIndex = 27;
            // 
            // colflg_sincronizado
            // 
            this.colflg_sincronizado.FieldName = "flg_sincronizado";
            this.colflg_sincronizado.Name = "colflg_sincronizado";
            this.colflg_sincronizado.Visible = true;
            this.colflg_sincronizado.VisibleIndex = 28;
            // 
            // colflg_agente_retencion
            // 
            this.colflg_agente_retencion.FieldName = "flg_agente_retencion";
            this.colflg_agente_retencion.Name = "colflg_agente_retencion";
            this.colflg_agente_retencion.Visible = true;
            this.colflg_agente_retencion.VisibleIndex = 29;
            // 
            // coldsc_logo_empresa
            // 
            this.coldsc_logo_empresa.FieldName = "dsc_logo_empresa";
            this.coldsc_logo_empresa.Name = "coldsc_logo_empresa";
            this.coldsc_logo_empresa.Visible = true;
            this.coldsc_logo_empresa.VisibleIndex = 30;
            // 
            // colcod_trabajador_rep
            // 
            this.colcod_trabajador_rep.FieldName = "cod_trabajador_rep";
            this.colcod_trabajador_rep.Name = "colcod_trabajador_rep";
            this.colcod_trabajador_rep.Visible = true;
            this.colcod_trabajador_rep.VisibleIndex = 31;
            // 
            // colflg_tipo_empresa
            // 
            this.colflg_tipo_empresa.FieldName = "flg_tipo_empresa";
            this.colflg_tipo_empresa.Name = "colflg_tipo_empresa";
            this.colflg_tipo_empresa.Visible = true;
            this.colflg_tipo_empresa.VisibleIndex = 32;
            // 
            // colcod_categoria
            // 
            this.colcod_categoria.FieldName = "cod_categoria";
            this.colcod_categoria.Name = "colcod_categoria";
            this.colcod_categoria.Visible = true;
            this.colcod_categoria.VisibleIndex = 33;
            // 
            // colcod_trabajador_rrhh
            // 
            this.colcod_trabajador_rrhh.FieldName = "cod_trabajador_rrhh";
            this.colcod_trabajador_rrhh.Name = "colcod_trabajador_rrhh";
            this.colcod_trabajador_rrhh.Visible = true;
            this.colcod_trabajador_rrhh.VisibleIndex = 34;
            // 
            // coldsc_web
            // 
            this.coldsc_web.FieldName = "dsc_web";
            this.coldsc_web.Name = "coldsc_web";
            this.coldsc_web.Visible = true;
            this.coldsc_web.VisibleIndex = 35;
            // 
            // coldsc_horario
            // 
            this.coldsc_horario.FieldName = "dsc_horario";
            this.coldsc_horario.Name = "coldsc_horario";
            this.coldsc_horario.Visible = true;
            this.coldsc_horario.VisibleIndex = 36;
            // 
            // coldsc_ubigeo_direccion
            // 
            this.coldsc_ubigeo_direccion.FieldName = "dsc_ubigeo_direccion";
            this.coldsc_ubigeo_direccion.Name = "coldsc_ubigeo_direccion";
            this.coldsc_ubigeo_direccion.Visible = true;
            this.coldsc_ubigeo_direccion.VisibleIndex = 37;
            // 
            // coldsc_urbanizacion
            // 
            this.coldsc_urbanizacion.FieldName = "dsc_urbanizacion";
            this.coldsc_urbanizacion.Name = "coldsc_urbanizacion";
            this.coldsc_urbanizacion.Visible = true;
            this.coldsc_urbanizacion.VisibleIndex = 38;
            // 
            // colcod_pais
            // 
            this.colcod_pais.FieldName = "cod_pais";
            this.colcod_pais.Name = "colcod_pais";
            this.colcod_pais.Visible = true;
            this.colcod_pais.VisibleIndex = 39;
            // 
            // colcod_departamento
            // 
            this.colcod_departamento.FieldName = "cod_departamento";
            this.colcod_departamento.Name = "colcod_departamento";
            this.colcod_departamento.Visible = true;
            this.colcod_departamento.VisibleIndex = 40;
            // 
            // colcod_provincia
            // 
            this.colcod_provincia.FieldName = "cod_provincia";
            this.colcod_provincia.Name = "colcod_provincia";
            this.colcod_provincia.Visible = true;
            this.colcod_provincia.VisibleIndex = 41;
            // 
            // colcod_distrito
            // 
            this.colcod_distrito.FieldName = "cod_distrito";
            this.colcod_distrito.Name = "colcod_distrito";
            this.colcod_distrito.Visible = true;
            this.colcod_distrito.VisibleIndex = 42;
            // 
            // coldsc_razon_comercial
            // 
            this.coldsc_razon_comercial.FieldName = "dsc_razon_comercial";
            this.coldsc_razon_comercial.Name = "coldsc_razon_comercial";
            this.coldsc_razon_comercial.Visible = true;
            this.coldsc_razon_comercial.VisibleIndex = 43;
            // 
            // coldsc_dominio
            // 
            this.coldsc_dominio.FieldName = "dsc_dominio";
            this.coldsc_dominio.Name = "coldsc_dominio";
            this.coldsc_dominio.Visible = true;
            this.coldsc_dominio.VisibleIndex = 44;
            // 
            // coldsc_ruta_onedrive
            // 
            this.coldsc_ruta_onedrive.FieldName = "dsc_ruta_onedrive";
            this.coldsc_ruta_onedrive.Name = "coldsc_ruta_onedrive";
            this.coldsc_ruta_onedrive.Visible = true;
            this.coldsc_ruta_onedrive.VisibleIndex = 45;
            // 
            // colUsuarioOnedrive
            // 
            this.colUsuarioOnedrive.FieldName = "UsuarioOnedrive";
            this.colUsuarioOnedrive.Name = "colUsuarioOnedrive";
            this.colUsuarioOnedrive.Visible = true;
            this.colUsuarioOnedrive.VisibleIndex = 46;
            // 
            // colClaveOnedrive
            // 
            this.colClaveOnedrive.FieldName = "ClaveOnedrive";
            this.colClaveOnedrive.Name = "colClaveOnedrive";
            this.colClaveOnedrive.Visible = true;
            this.colClaveOnedrive.VisibleIndex = 47;
            // 
            // colClientIdOnedrive
            // 
            this.colClientIdOnedrive.FieldName = "ClientIdOnedrive";
            this.colClientIdOnedrive.Name = "colClientIdOnedrive";
            this.colClientIdOnedrive.Visible = true;
            this.colClientIdOnedrive.VisibleIndex = 48;
            // 
            // colTenantOnedrive
            // 
            this.colTenantOnedrive.FieldName = "TenantOnedrive";
            this.colTenantOnedrive.Name = "colTenantOnedrive";
            this.colTenantOnedrive.Visible = true;
            this.colTenantOnedrive.VisibleIndex = 49;
            // 
            // colidCarpetaOnedrive
            // 
            this.colidCarpetaOnedrive.FieldName = "idCarpetaOnedrive";
            this.colidCarpetaOnedrive.Name = "colidCarpetaOnedrive";
            this.colidCarpetaOnedrive.Visible = true;
            this.colidCarpetaOnedrive.VisibleIndex = 50;
            // 
            // colidRepositorioOnedrive
            // 
            this.colidRepositorioOnedrive.FieldName = "idRepositorioOnedrive";
            this.colidRepositorioOnedrive.Name = "colidRepositorioOnedrive";
            this.colidRepositorioOnedrive.Visible = true;
            this.colidRepositorioOnedrive.VisibleIndex = 51;
            // 
            // colidCarpetaFacturasOnedrive
            // 
            this.colidCarpetaFacturasOnedrive.FieldName = "idCarpetaFacturasOnedrive";
            this.colidCarpetaFacturasOnedrive.Name = "colidCarpetaFacturasOnedrive";
            this.colidCarpetaFacturasOnedrive.Visible = true;
            this.colidCarpetaFacturasOnedrive.VisibleIndex = 52;
            // 
            // colUsuarioOnedrivePersonal
            // 
            this.colUsuarioOnedrivePersonal.FieldName = "UsuarioOnedrivePersonal";
            this.colUsuarioOnedrivePersonal.Name = "colUsuarioOnedrivePersonal";
            this.colUsuarioOnedrivePersonal.Visible = true;
            this.colUsuarioOnedrivePersonal.VisibleIndex = 53;
            // 
            // colClaveOnedrivePersonal
            // 
            this.colClaveOnedrivePersonal.FieldName = "ClaveOnedrivePersonal";
            this.colClaveOnedrivePersonal.Name = "colClaveOnedrivePersonal";
            this.colClaveOnedrivePersonal.Visible = true;
            this.colClaveOnedrivePersonal.VisibleIndex = 54;
            // 
            // colflg_defecto
            // 
            this.colflg_defecto.FieldName = "flg_defecto";
            this.colflg_defecto.Name = "colflg_defecto";
            this.colflg_defecto.Visible = true;
            this.colflg_defecto.VisibleIndex = 56;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem14,
            this.layoutControlItem1,
            this.simpleLabelItem1,
            this.layoutControlItem3,
            this.layoutControlItem5});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(269, 640);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.lkpEmpresa;
            this.layoutControlItem14.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem14.CustomizationFormText = "Empresa Usuario :";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(269, 24);
            this.layoutControlItem14.Text = "Empresa :";
            this.layoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListaCuentasBancarias;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 74);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(269, 566);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 48);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(0, 23);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(117, 23);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(173, 26);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.Text = "   Cuentas Bancarias";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(113, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lkpSedeEmpresa;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(269, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnConvertirPorDefecto;
            this.layoutControlItem5.Location = new System.Drawing.Point(173, 48);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(96, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(96, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(96, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(1304, 681);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.groupControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(277, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(277, 124);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(277, 669);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcListadoDetalleMovimientos;
            this.layoutControlItem4.Location = new System.Drawing.Point(277, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1015, 669);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "cod_tipo_movimiento";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // frmListadoMovimientoBancos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 863);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "frmListadoMovimientoBancos";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Listado Movimientos de Bancos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListadoMovimientoBancos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoMovimientoBancos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.icmbImagenesBancos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.igcImagenesBancos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoDetalleMovimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoDetalleMovimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoDetalleMovimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtFecha.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtFecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpTipoMovimiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpOrigenMovimiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlinkCodigo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlkpIdentificado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lkpSedeEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaCuentasBancarias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListaCuentasBancarias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlvListaCuentasBancarias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoAcciones;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcListaCuentasBancarias;
        private DevExpress.XtraGrid.Views.Tile.TileView tlvListaCuentasBancarias;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.BindingSource bsListaCuentasBancarias;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_sede_empresa;
        private DevExpress.XtraGrid.Columns.TileViewColumn colnum_linea;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_banco;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_banco;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_moneda;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_moneda;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_tipo_cuenta;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_tipo_cuenta;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_cta_bancaria;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_cta_interbancaria;
        private DevExpress.XtraGrid.Columns.TileViewColumn colflg_pago_proveedor;
        private DevExpress.XtraGrid.Columns.TileViewColumn colflg_pago_haberes;
        private DevExpress.XtraGrid.Columns.TileViewColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_usuario_registro;
        private DevExpress.XtraGrid.Columns.TileViewColumn colfch_cambio;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_usuario_cambio;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_empresa;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_empresa;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_ruc;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_direccion;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_apoderado;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_database;
        private DevExpress.XtraGrid.Columns.TileViewColumn colflg_activo;
        private DevExpress.XtraGrid.Columns.TileViewColumn colflg_principal;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_telefono;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_movil;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_codigo_licencia;
        private DevExpress.XtraGrid.Columns.TileViewColumn colflg_independiente;
        private DevExpress.XtraGrid.Columns.TileViewColumn colflg_sincronizado;
        private DevExpress.XtraGrid.Columns.TileViewColumn colflg_agente_retencion;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_logo_empresa;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_trabajador_rep;
        private DevExpress.XtraGrid.Columns.TileViewColumn colflg_tipo_empresa;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_categoria;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_trabajador_rrhh;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_web;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_horario;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_ubigeo_direccion;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_urbanizacion;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_pais;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_departamento;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_provincia;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcod_distrito;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_razon_comercial;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_dominio;
        private DevExpress.XtraGrid.Columns.TileViewColumn coldsc_ruta_onedrive;
        private DevExpress.XtraGrid.Columns.TileViewColumn colUsuarioOnedrive;
        private DevExpress.XtraGrid.Columns.TileViewColumn colClaveOnedrive;
        private DevExpress.XtraGrid.Columns.TileViewColumn colClientIdOnedrive;
        private DevExpress.XtraGrid.Columns.TileViewColumn colTenantOnedrive;
        private DevExpress.XtraGrid.Columns.TileViewColumn colidCarpetaOnedrive;
        private DevExpress.XtraGrid.Columns.TileViewColumn colidRepositorioOnedrive;
        private DevExpress.XtraGrid.Columns.TileViewColumn colidCarpetaFacturasOnedrive;
        private DevExpress.XtraGrid.Columns.TileViewColumn colUsuarioOnedrivePersonal;
        private DevExpress.XtraGrid.Columns.TileViewColumn colClaveOnedrivePersonal;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LookUpEdit lkpEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraEditors.LookUpEdit lkpSedeEmpresa;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.GridControl gcListadoDetalleMovimientos;
        private System.Windows.Forms.BindingSource bsListadoDetalleMovimientos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoDetalleMovimientos;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_item;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_ejecutada;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rdtFecha;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_efectiva;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_tipo_movimiento;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlkpTipoMovimiento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_comentario;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_monto;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nro_operacion;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro1;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cambio1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_origen_movimiento;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlkpOrigenMovimiento;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_identificado;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlkpIdentificado;
        private DevExpress.XtraBars.BarButtonItem btnCtasBancariasEmpresa;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoReportes;
        private DevExpress.XtraBars.BarButtonItem btnExportarExcel;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.Utils.ImageCollection igcImagenesBancos;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox icmbImagenesBancos;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcol_banco;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_bloque_pago;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rlinkCodigo;
        private DevExpress.XtraEditors.SimpleButton btnConvertirPorDefecto;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.TileViewColumn colflg_defecto;
        private DevExpress.XtraGrid.Columns.TileViewColumn colcol_defecto;
        private DevExpress.XtraBars.BarButtonItem btnAsignarNroOperacion;
        private DevExpress.XtraBars.BarButtonItem btnAdjuntar;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem btnExportarFormatoBancos;
        private DevExpress.XtraBars.BarButtonItem btnBancoScotiabank;
        private DevExpress.XtraBars.BarButtonItem btnBancoBBVA;
        private DevExpress.XtraBars.BarButtonItem btnBancoBCP;
        private DevExpress.XtraBars.BarButtonItem btnBancoInterbank;
        private DevExpress.XtraBars.BarButtonItem btnBancoBanbif;
        private DevExpress.XtraBars.BarButtonItem btnBancoNacion;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grupoAccesoBancos;
        private DevExpress.XtraBars.BarButtonItem btnBancoViaBCP;
        private DevExpress.XtraBars.BarButtonItem btnEliminarMovimiento;
    }
}