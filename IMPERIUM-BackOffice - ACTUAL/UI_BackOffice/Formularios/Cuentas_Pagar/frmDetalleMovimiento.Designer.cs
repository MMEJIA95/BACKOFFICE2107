﻿
namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    partial class frmDetalleMovimiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetalleMovimiento));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnRendirMovimiento = new DevExpress.XtraBars.BarButtonItem();
            this.btnAgregarDocumento = new DevExpress.XtraBars.BarButtonItem();
            this.btnAgregarDocumentoInterno = new DevExpress.XtraBars.BarButtonItem();
            this.btnAgregarDevolucionReembolso = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.picExportarExcel = new DevExpress.XtraEditors.PictureEdit();
            this.gcFacturasProveedor = new DevExpress.XtraGrid.GridControl();
            this.bsListadoFacturas = new System.Windows.Forms.BindingSource(this.components);
            this.gvFacturasProveedor = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNroFactura = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rLinkDocumento = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colMontoFactura = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtImporte = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDescTipoMoneda = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaEmision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaContable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEstadoFactura = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_estado_pago = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRUCProveedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescProveedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescTipoDoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_pago_programado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_pago_ejecutado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescTipoProv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFechaInsert = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserInsert = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_cambio_real = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_anulacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario_anulacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_igv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colflg_igv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_saldo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_subtotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_tipocambio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_distribucion_CECO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rmmDistribucionCECO = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.coldsc_distribucion_CECO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_tipo_servicio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_estado_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_proveedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnObservaciones = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtnVerPDF = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.mmComentario = new DevExpress.XtraEditors.MemoEdit();
            this.grdbTipoMovimiento = new DevExpress.XtraEditors.RadioGroup();
            this.dtFechaEmision = new DevExpress.XtraEditors.DateEdit();
            this.txtMontoEntregado = new DevExpress.XtraEditors.TextEdit();
            this.txtResponsable = new DevExpress.XtraEditors.TextEdit();
            this.picResponsable = new DevExpress.XtraEditors.PictureEdit();
            this.chkFlgPorRendir = new DevExpress.XtraEditors.CheckEdit();
            this.chkFlgRendido = new DevExpress.XtraEditors.CheckEdit();
            this.txtCodMovVinculado = new DevExpress.XtraEditors.TextEdit();
            this.txtReferencia = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem95 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExportarExcel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcFacturasProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoFacturas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturasProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rLinkDocumento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmDistribucionCECO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnObservaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnVerPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmComentario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdbTipoMovimiento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaEmision.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaEmision.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoEntregado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResponsable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResponsable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFlgPorRendir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFlgRendido.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodMovVinculado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReferencia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem95)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnNuevo,
            this.btnGuardar,
            this.btnRendirMovimiento,
            this.btnAgregarDocumento,
            this.btnAgregarDevolucionReembolso,
            this.btnAgregarDocumentoInterno});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 6;
            // 
            // bar2
            // 
            this.bar2.BarName = "Menú principal";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNuevo),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRendirMovimiento),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAgregarDocumento),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAgregarDocumentoInterno),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAgregarDevolucionReembolso)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Menú principal";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "Nuevo";
            this.btnNuevo.Id = 0;
            this.btnNuevo.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.add_32x321;
            this.btnNuevo.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ItemAppearance.Normal.Options.UseFont = true;
            this.btnNuevo.ItemInMenuAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnNuevo.ItemInMenuAppearance.Normal.Options.UseBackColor = true;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Caption = "Guardar";
            this.btnGuardar.Id = 1;
            this.btnGuardar.ImageOptions.Image = global::UI_BackOffice.Properties.Resources.save_32x322;
            this.btnGuardar.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ItemAppearance.Normal.Options.UseFont = true;
            this.btnGuardar.ItemInMenuAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnGuardar.ItemInMenuAppearance.Normal.Options.UseBackColor = true;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // btnRendirMovimiento
            // 
            this.btnRendirMovimiento.Caption = "Rendir Movimiento";
            this.btnRendirMovimiento.Enabled = false;
            this.btnRendirMovimiento.Id = 2;
            this.btnRendirMovimiento.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRendirMovimiento.ImageOptions.Image")));
            this.btnRendirMovimiento.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRendirMovimiento.ItemAppearance.Normal.Options.UseFont = true;
            this.btnRendirMovimiento.Name = "btnRendirMovimiento";
            this.btnRendirMovimiento.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnRendirMovimiento.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRendirMovimiento_ItemClick);
            // 
            // btnAgregarDocumento
            // 
            this.btnAgregarDocumento.Caption = "Agregar documento";
            this.btnAgregarDocumento.Enabled = false;
            this.btnAgregarDocumento.Id = 3;
            this.btnAgregarDocumento.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarDocumento.ImageOptions.Image")));
            this.btnAgregarDocumento.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAgregarDocumento.ItemAppearance.Normal.Options.UseFont = true;
            this.btnAgregarDocumento.Name = "btnAgregarDocumento";
            this.btnAgregarDocumento.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAgregarDocumento.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAgregarDocumento_ItemClick);
            // 
            // btnAgregarDocumentoInterno
            // 
            this.btnAgregarDocumentoInterno.Caption = "Agregar documento interno";
            this.btnAgregarDocumentoInterno.Enabled = false;
            this.btnAgregarDocumentoInterno.Id = 5;
            this.btnAgregarDocumentoInterno.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarDocumentoInterno.ImageOptions.Image")));
            this.btnAgregarDocumentoInterno.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAgregarDocumentoInterno.ItemAppearance.Normal.Options.UseFont = true;
            this.btnAgregarDocumentoInterno.Name = "btnAgregarDocumentoInterno";
            this.btnAgregarDocumentoInterno.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAgregarDocumentoInterno.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAgregarDocumentoInterno_ItemClick);
            // 
            // btnAgregarDevolucionReembolso
            // 
            this.btnAgregarDevolucionReembolso.Caption = "Agregar Devolución";
            this.btnAgregarDevolucionReembolso.Enabled = false;
            this.btnAgregarDevolucionReembolso.Id = 4;
            this.btnAgregarDevolucionReembolso.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarDevolucionReembolso.ImageOptions.Image")));
            this.btnAgregarDevolucionReembolso.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAgregarDevolucionReembolso.ItemAppearance.Normal.Options.UseFont = true;
            this.btnAgregarDevolucionReembolso.Name = "btnAgregarDevolucionReembolso";
            this.btnAgregarDevolucionReembolso.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAgregarDevolucionReembolso.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnAgregarDevolucionReembolso.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAgregarDevolucionReembolso_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(823, 80);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 543);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(823, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 80);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 463);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(823, 80);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 463);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.picExportarExcel);
            this.layoutControl1.Controls.Add(this.gcFacturasProveedor);
            this.layoutControl1.Controls.Add(this.mmComentario);
            this.layoutControl1.Controls.Add(this.grdbTipoMovimiento);
            this.layoutControl1.Controls.Add(this.dtFechaEmision);
            this.layoutControl1.Controls.Add(this.txtMontoEntregado);
            this.layoutControl1.Controls.Add(this.txtResponsable);
            this.layoutControl1.Controls.Add(this.picResponsable);
            this.layoutControl1.Controls.Add(this.chkFlgPorRendir);
            this.layoutControl1.Controls.Add(this.chkFlgRendido);
            this.layoutControl1.Controls.Add(this.txtCodMovVinculado);
            this.layoutControl1.Controls.Add(this.txtReferencia);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 80);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(823, 463);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // picExportarExcel
            // 
            this.picExportarExcel.EditValue = ((object)(resources.GetObject("picExportarExcel.EditValue")));
            this.picExportarExcel.Location = new System.Drawing.Point(797, 207);
            this.picExportarExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picExportarExcel.Name = "picExportarExcel";
            this.picExportarExcel.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picExportarExcel.Size = new System.Drawing.Size(20, 21);
            this.picExportarExcel.StyleController = this.layoutControl1;
            this.picExportarExcel.TabIndex = 40;
            this.picExportarExcel.Click += new System.EventHandler(this.picExportarExcel_Click);
            // 
            // gcFacturasProveedor
            // 
            this.gcFacturasProveedor.DataSource = this.bsListadoFacturas;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcFacturasProveedor.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcFacturasProveedor.Location = new System.Drawing.Point(6, 232);
            this.gcFacturasProveedor.MainView = this.gvFacturasProveedor;
            this.gcFacturasProveedor.Name = "gcFacturasProveedor";
            this.gcFacturasProveedor.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporte,
            this.rbtnObservaciones,
            this.rbtnVerPDF,
            this.rmmDistribucionCECO,
            this.rLinkDocumento});
            this.gcFacturasProveedor.Size = new System.Drawing.Size(811, 225);
            this.gcFacturasProveedor.TabIndex = 15;
            this.gcFacturasProveedor.UseEmbeddedNavigator = true;
            this.gcFacturasProveedor.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFacturasProveedor});
            // 
            // bsListadoFacturas
            // 
            this.bsListadoFacturas.DataSource = typeof(BE_BackOffice.eFacturaProveedor.eFacturaProveedor_Distribucion);
            // 
            // gvFacturasProveedor
            // 
            this.gvFacturasProveedor.Appearance.EvenRow.BackColor = System.Drawing.Color.Azure;
            this.gvFacturasProveedor.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvFacturasProveedor.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFacturasProveedor.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.FocusedCell.Options.UseFont = true;
            this.gvFacturasProveedor.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvFacturasProveedor.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFacturasProveedor.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.FocusedRow.Options.UseFont = true;
            this.gvFacturasProveedor.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gvFacturasProveedor.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.gvFacturasProveedor.Appearance.FooterPanel.Options.UseFont = true;
            this.gvFacturasProveedor.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvFacturasProveedor.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gvFacturasProveedor.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gvFacturasProveedor.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvFacturasProveedor.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvFacturasProveedor.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvFacturasProveedor.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvFacturasProveedor.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvFacturasProveedor.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvFacturasProveedor.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(88)))));
            this.gvFacturasProveedor.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvFacturasProveedor.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvFacturasProveedor.Appearance.SelectedRow.Options.UseFont = true;
            this.gvFacturasProveedor.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvFacturasProveedor.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvFacturasProveedor.ColumnPanelRowHeight = 35;
            this.gvFacturasProveedor.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNroFactura,
            this.colMontoFactura,
            this.colDescTipoMoneda,
            this.colFechaEmision,
            this.colFechaContable,
            this.colDescripcion,
            this.colEstadoFactura,
            this.coldsc_estado_pago,
            this.colRUCProveedor,
            this.colDescProveedor,
            this.colDescTipoDoc,
            this.gridColumn24,
            this.gridColumn25,
            this.colSel,
            this.colfch_pago_programado,
            this.colfch_pago_ejecutado,
            this.colDescTipoProv,
            this.colFechaInsert,
            this.colUserInsert,
            this.colfch_cambio_real,
            this.colUserUpdate,
            this.colfch_anulacion,
            this.coldsc_usuario_anulacion,
            this.colimp_igv,
            this.colflg_igv,
            this.colimp_saldo,
            this.colimp_subtotal,
            this.colimp_tipocambio,
            this.colcod_distribucion_CECO,
            this.coldsc_distribucion_CECO,
            this.colcod_tipo_servicio,
            this.coldsc_tipo_servicio,
            this.coldsc_estado_documento,
            this.coldsc_proveedor});
            this.gvFacturasProveedor.DetailHeight = 284;
            this.gvFacturasProveedor.GridControl = this.gcFacturasProveedor;
            this.gvFacturasProveedor.Name = "gvFacturasProveedor";
            this.gvFacturasProveedor.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.gvFacturasProveedor.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvFacturasProveedor.OptionsView.EnableAppearanceEvenRow = true;
            this.gvFacturasProveedor.OptionsView.RowAutoHeight = true;
            this.gvFacturasProveedor.OptionsView.ShowAutoFilterRow = true;
            this.gvFacturasProveedor.OptionsView.ShowFooter = true;
            this.gvFacturasProveedor.OptionsView.ShowGroupPanel = false;
            this.gvFacturasProveedor.OptionsView.ShowIndicator = false;
            this.gvFacturasProveedor.ViewCaption = " ";
            this.gvFacturasProveedor.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvFacturasProveedor_RowCellClick);
            this.gvFacturasProveedor.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvFacturasProveedor_CustomDrawColumnHeader);
            this.gvFacturasProveedor.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvFacturasProveedor_CustomDrawCell);
            this.gvFacturasProveedor.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvFacturasProveedor_RowStyle);
            // 
            // colNroFactura
            // 
            this.colNroFactura.AppearanceCell.Options.UseTextOptions = true;
            this.colNroFactura.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNroFactura.Caption = "Documento";
            this.colNroFactura.ColumnEdit = this.rLinkDocumento;
            this.colNroFactura.FieldName = "dsc_documento";
            this.colNroFactura.Name = "colNroFactura";
            this.colNroFactura.OptionsColumn.AllowEdit = false;
            this.colNroFactura.OptionsColumn.FixedWidth = true;
            this.colNroFactura.Visible = true;
            this.colNroFactura.VisibleIndex = 1;
            this.colNroFactura.Width = 85;
            // 
            // rLinkDocumento
            // 
            this.rLinkDocumento.AutoHeight = false;
            this.rLinkDocumento.Name = "rLinkDocumento";
            // 
            // colMontoFactura
            // 
            this.colMontoFactura.Caption = "Monto Total";
            this.colMontoFactura.ColumnEdit = this.rtxtImporte;
            this.colMontoFactura.FieldName = "imp_total";
            this.colMontoFactura.Name = "colMontoFactura";
            this.colMontoFactura.OptionsColumn.AllowEdit = false;
            this.colMontoFactura.OptionsColumn.FixedWidth = true;
            this.colMontoFactura.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_total", "{0:#,#.00}")});
            this.colMontoFactura.Visible = true;
            this.colMontoFactura.VisibleIndex = 5;
            this.colMontoFactura.Width = 80;
            // 
            // rtxtImporte
            // 
            this.rtxtImporte.AutoHeight = false;
            this.rtxtImporte.Mask.EditMask = "n2";
            this.rtxtImporte.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtImporte.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtImporte.Name = "rtxtImporte";
            // 
            // colDescTipoMoneda
            // 
            this.colDescTipoMoneda.AppearanceCell.Options.UseTextOptions = true;
            this.colDescTipoMoneda.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescTipoMoneda.Caption = "Moneda";
            this.colDescTipoMoneda.FieldName = "cod_moneda";
            this.colDescTipoMoneda.Name = "colDescTipoMoneda";
            this.colDescTipoMoneda.OptionsColumn.AllowEdit = false;
            this.colDescTipoMoneda.OptionsColumn.FixedWidth = true;
            this.colDescTipoMoneda.Visible = true;
            this.colDescTipoMoneda.VisibleIndex = 4;
            this.colDescTipoMoneda.Width = 60;
            // 
            // colFechaEmision
            // 
            this.colFechaEmision.AppearanceCell.Options.UseTextOptions = true;
            this.colFechaEmision.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFechaEmision.Caption = "Fecha Emisión";
            this.colFechaEmision.FieldName = "fch_documento";
            this.colFechaEmision.Name = "colFechaEmision";
            this.colFechaEmision.OptionsColumn.AllowEdit = false;
            this.colFechaEmision.OptionsColumn.FixedWidth = true;
            this.colFechaEmision.Width = 84;
            // 
            // colFechaContable
            // 
            this.colFechaContable.AppearanceCell.Options.UseTextOptions = true;
            this.colFechaContable.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFechaContable.Caption = "Fecha Vencimiento";
            this.colFechaContable.FieldName = "fch_vencimiento";
            this.colFechaContable.Name = "colFechaContable";
            this.colFechaContable.OptionsColumn.AllowEdit = false;
            this.colFechaContable.OptionsColumn.FixedWidth = true;
            this.colFechaContable.Width = 85;
            // 
            // colDescripcion
            // 
            this.colDescripcion.Caption = "Glosa";
            this.colDescripcion.FieldName = "dsc_glosa";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.OptionsColumn.AllowEdit = false;
            this.colDescripcion.OptionsColumn.FixedWidth = true;
            this.colDescripcion.Visible = true;
            this.colDescripcion.VisibleIndex = 2;
            this.colDescripcion.Width = 200;
            // 
            // colEstadoFactura
            // 
            this.colEstadoFactura.AppearanceCell.Options.UseTextOptions = true;
            this.colEstadoFactura.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEstadoFactura.Caption = "Estado Registro";
            this.colEstadoFactura.FieldName = "dsc_estado_registro";
            this.colEstadoFactura.Name = "colEstadoFactura";
            this.colEstadoFactura.OptionsColumn.AllowEdit = false;
            this.colEstadoFactura.OptionsColumn.FixedWidth = true;
            this.colEstadoFactura.Width = 80;
            // 
            // coldsc_estado_pago
            // 
            this.coldsc_estado_pago.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_estado_pago.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_estado_pago.Caption = "Esado Pago";
            this.coldsc_estado_pago.FieldName = "dsc_estado_pago";
            this.coldsc_estado_pago.Name = "coldsc_estado_pago";
            this.coldsc_estado_pago.OptionsColumn.AllowEdit = false;
            this.coldsc_estado_pago.OptionsColumn.FixedWidth = true;
            this.coldsc_estado_pago.Width = 80;
            // 
            // colRUCProveedor
            // 
            this.colRUCProveedor.AppearanceCell.Options.UseTextOptions = true;
            this.colRUCProveedor.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRUCProveedor.Caption = "RUC";
            this.colRUCProveedor.FieldName = "dsc_ruc";
            this.colRUCProveedor.Name = "colRUCProveedor";
            this.colRUCProveedor.OptionsColumn.AllowEdit = false;
            this.colRUCProveedor.OptionsColumn.FixedWidth = true;
            this.colRUCProveedor.Width = 100;
            // 
            // colDescProveedor
            // 
            this.colDescProveedor.Caption = "Proveedor";
            this.colDescProveedor.FieldName = "dsc_proveedor";
            this.colDescProveedor.Name = "colDescProveedor";
            this.colDescProveedor.OptionsColumn.AllowEdit = false;
            this.colDescProveedor.OptionsColumn.FixedWidth = true;
            this.colDescProveedor.Width = 250;
            // 
            // colDescTipoDoc
            // 
            this.colDescTipoDoc.Caption = "Tipo Documento";
            this.colDescTipoDoc.FieldName = "dsc_tipo_documento";
            this.colDescTipoDoc.Name = "colDescTipoDoc";
            this.colDescTipoDoc.OptionsColumn.AllowEdit = false;
            this.colDescTipoDoc.OptionsColumn.FixedWidth = true;
            this.colDescTipoDoc.Visible = true;
            this.colDescTipoDoc.VisibleIndex = 0;
            // 
            // gridColumn24
            // 
            this.gridColumn24.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn24.Caption = "P";
            this.gridColumn24.FieldName = "NombreArchivo";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.OptionsColumn.AllowEdit = false;
            this.gridColumn24.OptionsColumn.FixedWidth = true;
            this.gridColumn24.ToolTip = "Existe PDF";
            this.gridColumn24.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumn24.Width = 32;
            // 
            // gridColumn25
            // 
            this.gridColumn25.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn25.Caption = "O";
            this.gridColumn25.FieldName = "cod_estado_pago";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.OptionsColumn.AllowEdit = false;
            this.gridColumn25.OptionsColumn.FixedWidth = true;
            this.gridColumn25.ToolTip = "Factura Pagada";
            this.gridColumn25.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gridColumn25.Width = 32;
            // 
            // colSel
            // 
            this.colSel.Caption = " ";
            this.colSel.FieldName = "Sel";
            this.colSel.Name = "colSel";
            this.colSel.OptionsColumn.AllowEdit = false;
            this.colSel.OptionsColumn.FixedWidth = true;
            this.colSel.Width = 28;
            // 
            // colfch_pago_programado
            // 
            this.colfch_pago_programado.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_pago_programado.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_pago_programado.Caption = "Fecha Pago Programado";
            this.colfch_pago_programado.FieldName = "fch_pago_programado";
            this.colfch_pago_programado.Name = "colfch_pago_programado";
            this.colfch_pago_programado.OptionsColumn.AllowEdit = false;
            this.colfch_pago_programado.OptionsColumn.FixedWidth = true;
            this.colfch_pago_programado.Width = 80;
            // 
            // colfch_pago_ejecutado
            // 
            this.colfch_pago_ejecutado.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_pago_ejecutado.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_pago_ejecutado.Caption = "Fecha Pago Ejecutado";
            this.colfch_pago_ejecutado.FieldName = "fch_pago_ejecutado";
            this.colfch_pago_ejecutado.Name = "colfch_pago_ejecutado";
            this.colfch_pago_ejecutado.OptionsColumn.AllowEdit = false;
            this.colfch_pago_ejecutado.OptionsColumn.FixedWidth = true;
            this.colfch_pago_ejecutado.Width = 80;
            // 
            // colDescTipoProv
            // 
            this.colDescTipoProv.Caption = "Tipo Proveedor";
            this.colDescTipoProv.FieldName = "dsc_tipo_proveedor";
            this.colDescTipoProv.Name = "colDescTipoProv";
            this.colDescTipoProv.OptionsColumn.AllowEdit = false;
            this.colDescTipoProv.OptionsColumn.FixedWidth = true;
            this.colDescTipoProv.Width = 84;
            // 
            // colFechaInsert
            // 
            this.colFechaInsert.AppearanceCell.Options.UseTextOptions = true;
            this.colFechaInsert.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFechaInsert.Caption = "Fecha Registro";
            this.colFechaInsert.FieldName = "fch_registro_real";
            this.colFechaInsert.Name = "colFechaInsert";
            this.colFechaInsert.OptionsColumn.AllowEdit = false;
            this.colFechaInsert.OptionsColumn.FixedWidth = true;
            this.colFechaInsert.Width = 80;
            // 
            // colUserInsert
            // 
            this.colUserInsert.Caption = "Usuario Registro";
            this.colUserInsert.FieldName = "dsc_usuario_registro";
            this.colUserInsert.Name = "colUserInsert";
            this.colUserInsert.OptionsColumn.AllowEdit = false;
            this.colUserInsert.OptionsColumn.FixedWidth = true;
            // 
            // colfch_cambio_real
            // 
            this.colfch_cambio_real.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_cambio_real.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_cambio_real.Caption = "Fecha Modificación";
            this.colfch_cambio_real.FieldName = "fch_cambio_real";
            this.colfch_cambio_real.Name = "colfch_cambio_real";
            this.colfch_cambio_real.OptionsColumn.AllowEdit = false;
            this.colfch_cambio_real.OptionsColumn.FixedWidth = true;
            this.colfch_cambio_real.Width = 80;
            // 
            // colUserUpdate
            // 
            this.colUserUpdate.Caption = "Usuario Modificación";
            this.colUserUpdate.FieldName = "dsc_usuario_cambio";
            this.colUserUpdate.Name = "colUserUpdate";
            this.colUserUpdate.OptionsColumn.AllowEdit = false;
            this.colUserUpdate.OptionsColumn.FixedWidth = true;
            this.colUserUpdate.Width = 80;
            // 
            // colfch_anulacion
            // 
            this.colfch_anulacion.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_anulacion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_anulacion.Caption = "Fecha Anulación";
            this.colfch_anulacion.FieldName = "fch_anulacion";
            this.colfch_anulacion.Name = "colfch_anulacion";
            this.colfch_anulacion.OptionsColumn.AllowEdit = false;
            this.colfch_anulacion.OptionsColumn.FixedWidth = true;
            this.colfch_anulacion.Width = 70;
            // 
            // coldsc_usuario_anulacion
            // 
            this.coldsc_usuario_anulacion.Caption = "Usuario Anulación";
            this.coldsc_usuario_anulacion.FieldName = "dsc_usuario_anulacion";
            this.coldsc_usuario_anulacion.Name = "coldsc_usuario_anulacion";
            this.coldsc_usuario_anulacion.OptionsColumn.AllowEdit = false;
            this.coldsc_usuario_anulacion.OptionsColumn.FixedWidth = true;
            this.coldsc_usuario_anulacion.Width = 100;
            // 
            // colimp_igv
            // 
            this.colimp_igv.Caption = "Monto IGV";
            this.colimp_igv.ColumnEdit = this.rtxtImporte;
            this.colimp_igv.FieldName = "imp_igv";
            this.colimp_igv.Name = "colimp_igv";
            this.colimp_igv.OptionsColumn.AllowEdit = false;
            this.colimp_igv.OptionsColumn.FixedWidth = true;
            this.colimp_igv.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_igv", "{0:#,#.##}")});
            this.colimp_igv.Width = 80;
            // 
            // colflg_igv
            // 
            this.colflg_igv.AppearanceCell.Options.UseTextOptions = true;
            this.colflg_igv.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colflg_igv.Caption = "IGV";
            this.colflg_igv.FieldName = "flg_igv";
            this.colflg_igv.Name = "colflg_igv";
            this.colflg_igv.OptionsColumn.AllowEdit = false;
            this.colflg_igv.OptionsColumn.FixedWidth = true;
            this.colflg_igv.Width = 40;
            // 
            // colimp_saldo
            // 
            this.colimp_saldo.Caption = "Monto Saldo";
            this.colimp_saldo.ColumnEdit = this.rtxtImporte;
            this.colimp_saldo.FieldName = "imp_saldo";
            this.colimp_saldo.Name = "colimp_saldo";
            this.colimp_saldo.OptionsColumn.AllowEdit = false;
            this.colimp_saldo.OptionsColumn.FixedWidth = true;
            this.colimp_saldo.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_saldo", "{0:#,#.##}")});
            this.colimp_saldo.Width = 80;
            // 
            // colimp_subtotal
            // 
            this.colimp_subtotal.Caption = "Monto SubTotal";
            this.colimp_subtotal.ColumnEdit = this.rtxtImporte;
            this.colimp_subtotal.FieldName = "imp_subtotal";
            this.colimp_subtotal.Name = "colimp_subtotal";
            this.colimp_subtotal.OptionsColumn.AllowEdit = false;
            this.colimp_subtotal.OptionsColumn.FixedWidth = true;
            this.colimp_subtotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "imp_subtotal", "{0:#,#.##}")});
            this.colimp_subtotal.Width = 80;
            // 
            // colimp_tipocambio
            // 
            this.colimp_tipocambio.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_tipocambio.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_tipocambio.Caption = "Monto TC";
            this.colimp_tipocambio.ColumnEdit = this.rtxtImporte;
            this.colimp_tipocambio.FieldName = "imp_tipocambio";
            this.colimp_tipocambio.Name = "colimp_tipocambio";
            this.colimp_tipocambio.OptionsColumn.AllowEdit = false;
            this.colimp_tipocambio.OptionsColumn.FixedWidth = true;
            this.colimp_tipocambio.Width = 80;
            // 
            // colcod_distribucion_CECO
            // 
            this.colcod_distribucion_CECO.Caption = "Cod. Distribución CECO";
            this.colcod_distribucion_CECO.ColumnEdit = this.rmmDistribucionCECO;
            this.colcod_distribucion_CECO.FieldName = "cod_distribucion_CECO";
            this.colcod_distribucion_CECO.Name = "colcod_distribucion_CECO";
            this.colcod_distribucion_CECO.OptionsColumn.AllowEdit = false;
            this.colcod_distribucion_CECO.OptionsColumn.FixedWidth = true;
            this.colcod_distribucion_CECO.Width = 80;
            // 
            // rmmDistribucionCECO
            // 
            this.rmmDistribucionCECO.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.rmmDistribucionCECO.Name = "rmmDistribucionCECO";
            // 
            // coldsc_distribucion_CECO
            // 
            this.coldsc_distribucion_CECO.Caption = "Distribución CECO";
            this.coldsc_distribucion_CECO.ColumnEdit = this.rmmDistribucionCECO;
            this.coldsc_distribucion_CECO.FieldName = "dsc_distribucion_CECO";
            this.coldsc_distribucion_CECO.Name = "coldsc_distribucion_CECO";
            this.coldsc_distribucion_CECO.OptionsColumn.AllowEdit = false;
            this.coldsc_distribucion_CECO.OptionsColumn.FixedWidth = true;
            this.coldsc_distribucion_CECO.Width = 200;
            // 
            // colcod_tipo_servicio
            // 
            this.colcod_tipo_servicio.Caption = "Cod. Tipo Servicio";
            this.colcod_tipo_servicio.FieldName = "cod_tipo_servicio";
            this.colcod_tipo_servicio.Name = "colcod_tipo_servicio";
            this.colcod_tipo_servicio.OptionsColumn.AllowEdit = false;
            this.colcod_tipo_servicio.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_tipo_servicio
            // 
            this.coldsc_tipo_servicio.Caption = "Tipo Servicio";
            this.coldsc_tipo_servicio.FieldName = "dsc_tipo_servicio";
            this.coldsc_tipo_servicio.Name = "coldsc_tipo_servicio";
            this.coldsc_tipo_servicio.OptionsColumn.AllowEdit = false;
            this.coldsc_tipo_servicio.OptionsColumn.FixedWidth = true;
            this.coldsc_tipo_servicio.Width = 120;
            // 
            // coldsc_estado_documento
            // 
            this.coldsc_estado_documento.AppearanceCell.Options.UseTextOptions = true;
            this.coldsc_estado_documento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldsc_estado_documento.Caption = "Estado Documento";
            this.coldsc_estado_documento.FieldName = "dsc_estado_documento";
            this.coldsc_estado_documento.Name = "coldsc_estado_documento";
            this.coldsc_estado_documento.OptionsColumn.AllowEdit = false;
            this.coldsc_estado_documento.OptionsColumn.FixedWidth = true;
            this.coldsc_estado_documento.Width = 80;
            // 
            // coldsc_proveedor
            // 
            this.coldsc_proveedor.Caption = "Proveedor";
            this.coldsc_proveedor.FieldName = "dsc_proveedor";
            this.coldsc_proveedor.Name = "coldsc_proveedor";
            this.coldsc_proveedor.OptionsColumn.AllowEdit = false;
            this.coldsc_proveedor.OptionsColumn.FixedWidth = true;
            this.coldsc_proveedor.Visible = true;
            this.coldsc_proveedor.VisibleIndex = 3;
            this.coldsc_proveedor.Width = 150;
            // 
            // rbtnObservaciones
            // 
            this.rbtnObservaciones.AutoHeight = false;
            this.rbtnObservaciones.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, false, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.rbtnObservaciones.Name = "rbtnObservaciones";
            this.rbtnObservaciones.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // rbtnVerPDF
            // 
            this.rbtnVerPDF.AutoHeight = false;
            this.rbtnVerPDF.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.rbtnVerPDF.Name = "rbtnVerPDF";
            this.rbtnVerPDF.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // mmComentario
            // 
            this.mmComentario.Location = new System.Drawing.Point(101, 147);
            this.mmComentario.MenuManager = this.barManager1;
            this.mmComentario.Name = "mmComentario";
            this.mmComentario.Size = new System.Drawing.Size(716, 56);
            this.mmComentario.StyleController = this.layoutControl1;
            this.mmComentario.TabIndex = 14;
            // 
            // grdbTipoMovimiento
            // 
            this.grdbTipoMovimiento.Enabled = false;
            this.grdbTipoMovimiento.Location = new System.Drawing.Point(101, 63);
            this.grdbTipoMovimiento.MenuManager = this.barManager1;
            this.grdbTipoMovimiento.Name = "grdbTipoMovimiento";
            this.grdbTipoMovimiento.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.grdbTipoMovimiento.Properties.Appearance.Options.UseBackColor = true;
            this.grdbTipoMovimiento.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.grdbTipoMovimiento.Properties.Columns = 5;
            this.grdbTipoMovimiento.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "SALIDA"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "DEVOLUCIÓN"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "REEMBOLSO"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "REPOSICIÓN"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "CIERRE DE CAJA")});
            this.grdbTipoMovimiento.Size = new System.Drawing.Size(526, 32);
            this.grdbTipoMovimiento.StyleController = this.layoutControl1;
            this.grdbTipoMovimiento.TabIndex = 3;
            // 
            // dtFechaEmision
            // 
            this.dtFechaEmision.EditValue = null;
            this.dtFechaEmision.Location = new System.Drawing.Point(101, 99);
            this.dtFechaEmision.Name = "dtFechaEmision";
            this.dtFechaEmision.Properties.Appearance.Options.UseTextOptions = true;
            this.dtFechaEmision.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtFechaEmision.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaEmision.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaEmision.Properties.NullText = "[Vacío]";
            this.dtFechaEmision.Size = new System.Drawing.Size(108, 20);
            this.dtFechaEmision.StyleController = this.layoutControl1;
            this.dtFechaEmision.TabIndex = 4;
            // 
            // txtMontoEntregado
            // 
            this.txtMontoEntregado.EditValue = "0";
            this.txtMontoEntregado.Location = new System.Drawing.Point(291, 99);
            this.txtMontoEntregado.Name = "txtMontoEntregado";
            this.txtMontoEntregado.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtMontoEntregado.Properties.Appearance.Options.UseFont = true;
            this.txtMontoEntregado.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMontoEntregado.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtMontoEntregado.Properties.Mask.EditMask = "n2";
            this.txtMontoEntregado.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMontoEntregado.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontoEntregado.Size = new System.Drawing.Size(94, 20);
            this.txtMontoEntregado.StyleController = this.layoutControl1;
            this.txtMontoEntregado.TabIndex = 5;
            // 
            // txtResponsable
            // 
            this.txtResponsable.Location = new System.Drawing.Point(101, 123);
            this.txtResponsable.Name = "txtResponsable";
            this.txtResponsable.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtResponsable.Size = new System.Drawing.Size(692, 20);
            this.txtResponsable.StyleController = this.layoutControl1;
            this.txtResponsable.TabIndex = 6;
            this.txtResponsable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResponsable_KeyDown);
            this.txtResponsable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResponsable_KeyPress);
            // 
            // picResponsable
            // 
            this.picResponsable.EditValue = ((object)(resources.GetObject("picResponsable.EditValue")));
            this.picResponsable.Location = new System.Drawing.Point(797, 123);
            this.picResponsable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picResponsable.Name = "picResponsable";
            this.picResponsable.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picResponsable.Size = new System.Drawing.Size(20, 20);
            this.picResponsable.StyleController = this.layoutControl1;
            this.picResponsable.TabIndex = 1;
            this.picResponsable.Click += new System.EventHandler(this.picResponsable_Click);
            // 
            // chkFlgPorRendir
            // 
            this.chkFlgPorRendir.EditValue = true;
            this.chkFlgPorRendir.Location = new System.Drawing.Point(103, 39);
            this.chkFlgPorRendir.MenuManager = this.barManager1;
            this.chkFlgPorRendir.Name = "chkFlgPorRendir";
            this.chkFlgPorRendir.Properties.Caption = "Por Rendir";
            this.chkFlgPorRendir.Size = new System.Drawing.Size(89, 20);
            this.chkFlgPorRendir.StyleController = this.layoutControl1;
            this.chkFlgPorRendir.TabIndex = 0;
            this.chkFlgPorRendir.CheckStateChanged += new System.EventHandler(this.chkFlgPorRendir_CheckStateChanged);
            // 
            // chkFlgRendido
            // 
            this.chkFlgRendido.Location = new System.Drawing.Point(196, 39);
            this.chkFlgRendido.MenuManager = this.barManager1;
            this.chkFlgRendido.Name = "chkFlgRendido";
            this.chkFlgRendido.Properties.Caption = "Rendido";
            this.chkFlgRendido.Size = new System.Drawing.Size(82, 20);
            this.chkFlgRendido.StyleController = this.layoutControl1;
            this.chkFlgRendido.TabIndex = 2;
            this.chkFlgRendido.CheckStateChanged += new System.EventHandler(this.chkFlgRendido_CheckStateChanged);
            // 
            // txtCodMovVinculado
            // 
            this.txtCodMovVinculado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtCodMovVinculado.EditValue = "DV000002";
            this.txtCodMovVinculado.Location = new System.Drawing.Point(729, 99);
            this.txtCodMovVinculado.MenuManager = this.barManager1;
            this.txtCodMovVinculado.Name = "txtCodMovVinculado";
            this.txtCodMovVinculado.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtCodMovVinculado.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtCodMovVinculado.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtCodMovVinculado.Properties.Appearance.Options.UseBackColor = true;
            this.txtCodMovVinculado.Properties.Appearance.Options.UseFont = true;
            this.txtCodMovVinculado.Properties.Appearance.Options.UseForeColor = true;
            this.txtCodMovVinculado.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCodMovVinculado.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCodMovVinculado.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtCodMovVinculado.Properties.ReadOnly = true;
            this.txtCodMovVinculado.Size = new System.Drawing.Size(88, 18);
            this.txtCodMovVinculado.StyleController = this.layoutControl1;
            this.txtCodMovVinculado.TabIndex = 16;
            this.txtCodMovVinculado.Click += new System.EventHandler(this.txtCodMovVinculado_Click);
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(719, 63);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReferencia.Size = new System.Drawing.Size(98, 20);
            this.txtReferencia.StyleController = this.layoutControl1;
            this.txtReferencia.TabIndex = 39;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleLabelItem1,
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem95,
            this.layoutControlItem27,
            this.layoutControlItem5,
            this.emptySpaceItem4,
            this.layoutControlItem6,
            this.simpleLabelItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem6,
            this.layoutControlItem2,
            this.layoutControlItem12,
            this.simpleLabelItem3,
            this.layoutControlItem13,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.Root.Size = new System.Drawing.Size(823, 463);
            this.Root.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Green;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(815, 33);
            this.simpleLabelItem1.Text = "Detalle Caja Chica";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(396, 29);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdbTipoMovimiento;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 57);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(625, 36);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(625, 36);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(625, 36);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Movimiento : ";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(90, 20);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(625, 57);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(13, 36);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem95
            // 
            this.layoutControlItem95.Control = this.dtFechaEmision;
            this.layoutControlItem95.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem95.CustomizationFormText = "Fecha Nac. :";
            this.layoutControlItem95.Location = new System.Drawing.Point(0, 93);
            this.layoutControlItem95.MaxSize = new System.Drawing.Size(207, 24);
            this.layoutControlItem95.MinSize = new System.Drawing.Size(207, 24);
            this.layoutControlItem95.Name = "layoutControlItem95";
            this.layoutControlItem95.Size = new System.Drawing.Size(207, 24);
            this.layoutControlItem95.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem95.Text = "Fecha : ";
            this.layoutControlItem95.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem95.TextSize = new System.Drawing.Size(90, 20);
            this.layoutControlItem95.TextToControlDistance = 5;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.txtResponsable;
            this.layoutControlItem27.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem27.CustomizationFormText = "Nombre :";
            this.layoutControlItem27.Location = new System.Drawing.Point(0, 117);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(791, 24);
            this.layoutControlItem27.Text = "Entregado a :";
            this.layoutControlItem27.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem27.TextSize = new System.Drawing.Size(90, 20);
            this.layoutControlItem27.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.picResponsable;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem5.Location = new System.Drawing.Point(791, 117);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(24, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(24, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(24, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem2";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(383, 93);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(255, 24);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem6.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem6.Control = this.txtMontoEntregado;
            this.layoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem6.CustomizationFormText = "Monto Detracc. :";
            this.layoutControlItem6.Location = new System.Drawing.Point(207, 93);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(176, 24);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(176, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.layoutControlItem6.Size = new System.Drawing.Size(176, 24);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "Importe : ";
            this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(70, 20);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 33);
            this.simpleLabelItem2.MaxSize = new System.Drawing.Size(97, 24);
            this.simpleLabelItem2.MinSize = new System.Drawing.Size(97, 24);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(97, 24);
            this.simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem2.Text = "Tipo movimiento : ";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(396, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkFlgPorRendir;
            this.layoutControlItem3.Location = new System.Drawing.Point(97, 33);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(93, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(93, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(93, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkFlgRendido;
            this.layoutControlItem4.Location = new System.Drawing.Point(190, 33);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(86, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(86, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(86, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(276, 33);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(539, 24);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.mmComentario;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 141);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(815, 60);
            this.layoutControlItem2.Text = "Comentario : ";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(90, 20);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.gcFacturasProveedor;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 226);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(815, 229);
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.BackColor = System.Drawing.Color.LightGray;
            this.simpleLabelItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseBackColor = true;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem3.Location = new System.Drawing.Point(0, 201);
            this.simpleLabelItem3.MaxSize = new System.Drawing.Size(0, 25);
            this.simpleLabelItem3.MinSize = new System.Drawing.Size(218, 25);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.simpleLabelItem3.Size = new System.Drawing.Size(791, 25);
            this.simpleLabelItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem3.Text = "Listado de comprobantes, reembolsos y devoluciones";
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(396, 18);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.txtCodMovVinculado;
            this.layoutControlItem13.Location = new System.Drawing.Point(638, 93);
            this.layoutControlItem13.MaxSize = new System.Drawing.Size(177, 24);
            this.layoutControlItem13.MinSize = new System.Drawing.Size(177, 24);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(177, 24);
            this.layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem13.Text = "Mov. Vinculado : ";
            this.layoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(80, 20);
            this.layoutControlItem13.TextToControlDistance = 5;
            this.layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem7.AppearanceItemCaption.ForeColor = System.Drawing.Color.Maroon;
            this.layoutControlItem7.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem7.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem7.Control = this.txtReferencia;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "Referencia : ";
            this.layoutControlItem7.Location = new System.Drawing.Point(638, 57);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(177, 36);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(177, 36);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(177, 36);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "Referencia : ";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(70, 20);
            this.layoutControlItem7.TextToControlDistance = 5;
            this.layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.picExportarExcel;
            this.layoutControlItem8.Location = new System.Drawing.Point(791, 201);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(24, 25);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(24, 25);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(24, 25);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // frmDetalleMovimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 543);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Name = "frmDetalleMovimiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalle Caja Chica";
            this.Load += new System.EventHandler(this.frmDetalleMovimiento_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDetalleMovimiento_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picExportarExcel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcFacturasProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoFacturas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFacturasProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rLinkDocumento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmmDistribucionCECO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnObservaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnVerPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmComentario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdbTipoMovimiento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaEmision.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaEmision.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoEntregado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResponsable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResponsable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFlgPorRendir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFlgRendido.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodMovVinculado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReferencia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem95)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.DateEdit dtFechaEmision;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem95;
        private DevExpress.XtraEditors.TextEdit txtMontoEntregado;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit txtResponsable;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraEditors.PictureEdit picResponsable;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.MemoEdit mmComentario;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraBars.BarButtonItem btnRendirMovimiento;
        private System.Windows.Forms.BindingSource bsListadoFacturas;
        private DevExpress.XtraBars.BarButtonItem btnAgregarDocumento;
        private DevExpress.XtraGrid.GridControl gcFacturasProveedor;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFacturasProveedor;
        private DevExpress.XtraGrid.Columns.GridColumn colNroFactura;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rLinkDocumento;
        private DevExpress.XtraGrid.Columns.GridColumn colMontoFactura;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraGrid.Columns.GridColumn colDescTipoMoneda;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaEmision;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaContable;
        private DevExpress.XtraGrid.Columns.GridColumn colDescripcion;
        private DevExpress.XtraGrid.Columns.GridColumn colEstadoFactura;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_estado_pago;
        private DevExpress.XtraGrid.Columns.GridColumn colRUCProveedor;
        private DevExpress.XtraGrid.Columns.GridColumn colDescProveedor;
        private DevExpress.XtraGrid.Columns.GridColumn colDescTipoDoc;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn colSel;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_pago_programado;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_pago_ejecutado;
        private DevExpress.XtraGrid.Columns.GridColumn colDescTipoProv;
        private DevExpress.XtraGrid.Columns.GridColumn colFechaInsert;
        private DevExpress.XtraGrid.Columns.GridColumn colUserInsert;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_cambio_real;
        private DevExpress.XtraGrid.Columns.GridColumn colUserUpdate;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_anulacion;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario_anulacion;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_igv;
        private DevExpress.XtraGrid.Columns.GridColumn colflg_igv;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_saldo;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_subtotal;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_tipocambio;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_distribucion_CECO;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit rmmDistribucionCECO;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_distribucion_CECO;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_tipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_servicio;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_estado_documento;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnObservaciones;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnVerPDF;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_proveedor;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        internal DevExpress.XtraBars.BarButtonItem btnAgregarDevolucionReembolso;
        private DevExpress.XtraEditors.TextEdit txtCodMovVinculado;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        internal DevExpress.XtraEditors.RadioGroup grdbTipoMovimiento;
        internal DevExpress.XtraEditors.CheckEdit chkFlgPorRendir;
        internal DevExpress.XtraEditors.CheckEdit chkFlgRendido;
        internal DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnAgregarDocumentoInterno;
        private DevExpress.XtraEditors.TextEdit txtReferencia;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.PictureEdit picExportarExcel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}