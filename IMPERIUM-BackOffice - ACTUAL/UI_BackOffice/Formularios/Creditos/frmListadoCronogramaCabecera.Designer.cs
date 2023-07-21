
namespace UI_BackOffice.Formularios.Creditos
{
    partial class frmListadoCronogramaCabecera
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoCronogramaCabecera = new DevExpress.XtraGrid.GridControl();
            this.bsListadoCronogramaCabecera = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoCronogramaCabecera = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_credito = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nombres_completos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_cronograma = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.rtxtNumero = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoCronogramaCabecera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCronogramaCabecera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoCronogramaCabecera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtNumero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListadoCronogramaCabecera);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1038, 488);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListadoCronogramaCabecera
            // 
            this.gcListadoCronogramaCabecera.DataSource = this.bsListadoCronogramaCabecera;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListadoCronogramaCabecera.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListadoCronogramaCabecera.Location = new System.Drawing.Point(12, 40);
            this.gcListadoCronogramaCabecera.MainView = this.gvListadoCronogramaCabecera;
            this.gcListadoCronogramaCabecera.Name = "gcListadoCronogramaCabecera";
            this.gcListadoCronogramaCabecera.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtImporte,
            this.rtxtPorcentaje,
            this.rtxtNumero,
            this.rtxtPorcentaje2});
            this.gcListadoCronogramaCabecera.Size = new System.Drawing.Size(1014, 436);
            this.gcListadoCronogramaCabecera.TabIndex = 12;
            this.gcListadoCronogramaCabecera.UseEmbeddedNavigator = true;
            this.gcListadoCronogramaCabecera.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoCronogramaCabecera});
            // 
            // bsListadoCronogramaCabecera
            // 
            this.bsListadoCronogramaCabecera.DataSource = typeof(BE_BackOffice.eCreditoVehicular.eCronogramaCabecera);
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
            this.colcod_credito,
            this.coldsc_nombres_completos,
            this.colcod_cronograma,
            this.colnum_placa,
            this.colimp_Capital,
            this.colfch_desembolso,
            this.colnum_cuotas,
            this.colnum_diapago,
            this.colnum_tasaanual,
            this.colnum_tasamensual,
            this.colnum_tasaTIRanual,
            this.colnum_tasaTIRM});
            this.gvListadoCronogramaCabecera.GridControl = this.gcListadoCronogramaCabecera;
            this.gvListadoCronogramaCabecera.Name = "gvListadoCronogramaCabecera";
            this.gvListadoCronogramaCabecera.OptionsBehavior.Editable = false;
            this.gvListadoCronogramaCabecera.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListadoCronogramaCabecera.OptionsView.ShowAutoFilterRow = true;
            this.gvListadoCronogramaCabecera.OptionsView.ShowIndicator = false;
            this.gvListadoCronogramaCabecera.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoCronogramaCabecera_RowClick);
            this.gvListadoCronogramaCabecera.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvListadoCronogramaCabecera_CustomDrawColumnHeader);
            this.gvListadoCronogramaCabecera.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListadoCronogramaCabecera_CustomDrawCell);
            this.gvListadoCronogramaCabecera.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvListadoCronogramaCabecera_RowStyle);
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
            this.colcod_credito.VisibleIndex = 0;
            this.colcod_credito.Width = 60;
            // 
            // coldsc_nombres_completos
            // 
            this.coldsc_nombres_completos.Caption = "Cliente";
            this.coldsc_nombres_completos.FieldName = "dsc_nombres_completos";
            this.coldsc_nombres_completos.Name = "coldsc_nombres_completos";
            this.coldsc_nombres_completos.OptionsColumn.FixedWidth = true;
            this.coldsc_nombres_completos.Visible = true;
            this.coldsc_nombres_completos.VisibleIndex = 1;
            this.coldsc_nombres_completos.Width = 230;
            // 
            // colcod_cronograma
            // 
            this.colcod_cronograma.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_cronograma.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_cronograma.Caption = "Cod. Cronograma";
            this.colcod_cronograma.FieldName = "cod_cronograma";
            this.colcod_cronograma.Name = "colcod_cronograma";
            this.colcod_cronograma.OptionsColumn.FixedWidth = true;
            this.colcod_cronograma.Visible = true;
            this.colcod_cronograma.VisibleIndex = 2;
            this.colcod_cronograma.Width = 100;
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
            this.colnum_placa.VisibleIndex = 3;
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
            this.colimp_Capital.Visible = true;
            this.colimp_Capital.VisibleIndex = 4;
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
            this.colfch_desembolso.VisibleIndex = 5;
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
            this.colnum_cuotas.VisibleIndex = 6;
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
            this.colnum_diapago.VisibleIndex = 7;
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
            this.colnum_tasaanual.VisibleIndex = 8;
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
            this.colnum_tasamensual.VisibleIndex = 9;
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
            this.colnum_tasaTIRanual.VisibleIndex = 10;
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
            this.colnum_tasaTIRM.VisibleIndex = 11;
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
            // rtxtNumero
            // 
            this.rtxtNumero.AutoHeight = false;
            this.rtxtNumero.Mask.EditMask = "n2";
            this.rtxtNumero.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtNumero.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtNumero.Name = "rtxtNumero";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.simpleLabelItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1038, 488);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoCronogramaCabecera;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1018, 440);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
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
            this.simpleLabelItem1.Size = new System.Drawing.Size(1018, 28);
            this.simpleLabelItem1.Text = "Listado de Cronogramas";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(243, 24);
            // 
            // frmListadoCronogramaCabecera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 488);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListadoCronogramaCabecera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmListadoCronogramaCabecera_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListadoCronogramaCabecera_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoCronogramaCabecera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCronogramaCabecera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoCronogramaCabecera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtNumero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcListadoCronogramaCabecera;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoCronogramaCabecera;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_credito;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_desembolso;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nombres_completos;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_cronograma;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_placa;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_cuotas;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtNumero;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_Capital;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtImporte;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_diapago;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_tasaanual;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_tasamensual;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_tasaTIRanual;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_tasaTIRM;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPorcentaje;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private System.Windows.Forms.BindingSource bsListadoCronogramaCabecera;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPorcentaje2;
    }
}