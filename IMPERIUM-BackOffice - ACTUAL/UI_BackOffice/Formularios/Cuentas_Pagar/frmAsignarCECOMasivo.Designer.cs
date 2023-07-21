namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    partial class frmAsignarCECOMasivo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAsignarCECOMasivo));
            this.gcListadoCECO = new DevExpress.XtraGrid.GridControl();
            this.bsListadoCECO = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoCECO = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colnum_item = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_nivel1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nivel1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_nivel2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nivel2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_nivel3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nivel3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_nivel4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nivel4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_CECO_abrev = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_CECO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colporc_distribucion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtPorcentaje = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            this.divFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).BeginInit();
            this.layout_footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoCECO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCECO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoCECO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.Location = new System.Drawing.Point(0, 506);
            this.divFooter.Size = new System.Drawing.Size(975, 43);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnCancelar.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.btnCancelar.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseBorderColor = true;
            this.btnCancelar.Appearance.Options.UseFont = true;
            this.btnCancelar.Appearance.Options.UseForeColor = true;
            this.btnCancelar.Location = new System.Drawing.Point(444, 2);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnGuardar.Appearance.BorderColor = System.Drawing.Color.Lime;
            this.btnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.btnGuardar.Appearance.Options.UseBackColor = true;
            this.btnGuardar.Appearance.Options.UseBorderColor = true;
            this.btnGuardar.Appearance.Options.UseFont = true;
            this.btnGuardar.Location = new System.Drawing.Point(557, 2);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            // 
            // layout_footer
            // 
            this.layout_footer.Size = new System.Drawing.Size(975, 41);
            this.layout_footer.Controls.SetChildIndex(this.btnOpcional, 0);
            this.layout_footer.Controls.SetChildIndex(this.btnGuardar, 0);
            this.layout_footer.Controls.SetChildIndex(this.btnCancelar, 0);
            // 
            // btnOpcional
            // 
            this.btnOpcional.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnOpcional.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpcional.Appearance.Options.UseBackColor = true;
            this.btnOpcional.Appearance.Options.UseFont = true;
            this.btnOpcional.Location = new System.Drawing.Point(329, 2);
            // 
            // gcListadoCECO
            // 
            this.gcListadoCECO.DataSource = this.bsListadoCECO;
            this.gcListadoCECO.Location = new System.Drawing.Point(6, 6);
            this.gcListadoCECO.MainView = this.gvListadoCECO;
            this.gcListadoCECO.Name = "gcListadoCECO";
            this.gcListadoCECO.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtPorcentaje});
            this.gcListadoCECO.Size = new System.Drawing.Size(963, 456);
            this.gcListadoCECO.TabIndex = 2;
            this.gcListadoCECO.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoCECO});
            // 
            // bsListadoCECO
            // 
            this.bsListadoCECO.DataSource = typeof(BE_BackOffice.eCeco);
            // 
            // gvListadoCECO
            // 
            this.gvListadoCECO.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListadoCECO.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListadoCECO.ColumnPanelRowHeight = 30;
            this.gvListadoCECO.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colnum_item,
            this.colcod_nivel1,
            this.coldsc_nivel1,
            this.colcod_nivel2,
            this.coldsc_nivel2,
            this.colcod_nivel3,
            this.coldsc_nivel3,
            this.colcod_nivel4,
            this.coldsc_nivel4,
            this.coldsc_CECO_abrev,
            this.colcod_CECO,
            this.colporc_distribucion});
            this.gvListadoCECO.GridControl = this.gcListadoCECO;
            this.gvListadoCECO.Name = "gvListadoCECO";
            this.gvListadoCECO.OptionsSelection.CheckBoxSelectorColumnWidth = 20;
            this.gvListadoCECO.OptionsView.ShowFooter = true;
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
            this.colnum_item.Width = 30;
            // 
            // colcod_nivel1
            // 
            this.colcod_nivel1.FieldName = "cod_nivel1";
            this.colcod_nivel1.Name = "colcod_nivel1";
            this.colcod_nivel1.OptionsColumn.AllowEdit = false;
            this.colcod_nivel1.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_nivel1
            // 
            this.coldsc_nivel1.FieldName = "dsc_nivel1";
            this.coldsc_nivel1.Name = "coldsc_nivel1";
            this.coldsc_nivel1.OptionsColumn.AllowEdit = false;
            this.coldsc_nivel1.OptionsColumn.FixedWidth = true;
            // 
            // colcod_nivel2
            // 
            this.colcod_nivel2.FieldName = "cod_nivel2";
            this.colcod_nivel2.Name = "colcod_nivel2";
            this.colcod_nivel2.OptionsColumn.AllowEdit = false;
            this.colcod_nivel2.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_nivel2
            // 
            this.coldsc_nivel2.FieldName = "dsc_nivel2";
            this.coldsc_nivel2.Name = "coldsc_nivel2";
            this.coldsc_nivel2.OptionsColumn.AllowEdit = false;
            this.coldsc_nivel2.OptionsColumn.FixedWidth = true;
            // 
            // colcod_nivel3
            // 
            this.colcod_nivel3.FieldName = "cod_nivel3";
            this.colcod_nivel3.Name = "colcod_nivel3";
            this.colcod_nivel3.OptionsColumn.AllowEdit = false;
            this.colcod_nivel3.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_nivel3
            // 
            this.coldsc_nivel3.FieldName = "dsc_nivel3";
            this.coldsc_nivel3.Name = "coldsc_nivel3";
            this.coldsc_nivel3.OptionsColumn.AllowEdit = false;
            this.coldsc_nivel3.OptionsColumn.FixedWidth = true;
            // 
            // colcod_nivel4
            // 
            this.colcod_nivel4.FieldName = "cod_nivel4";
            this.colcod_nivel4.Name = "colcod_nivel4";
            this.colcod_nivel4.OptionsColumn.AllowEdit = false;
            this.colcod_nivel4.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_nivel4
            // 
            this.coldsc_nivel4.FieldName = "dsc_nivel4";
            this.coldsc_nivel4.Name = "coldsc_nivel4";
            this.coldsc_nivel4.OptionsColumn.AllowEdit = false;
            this.coldsc_nivel4.OptionsColumn.FixedWidth = true;
            // 
            // coldsc_CECO_abrev
            // 
            this.coldsc_CECO_abrev.Caption = "Centro de Costo";
            this.coldsc_CECO_abrev.FieldName = "dsc_CECO_abrev";
            this.coldsc_CECO_abrev.Name = "coldsc_CECO_abrev";
            this.coldsc_CECO_abrev.OptionsColumn.AllowEdit = false;
            this.coldsc_CECO_abrev.OptionsColumn.FixedWidth = true;
            this.coldsc_CECO_abrev.Visible = true;
            this.coldsc_CECO_abrev.VisibleIndex = 1;
            this.coldsc_CECO_abrev.Width = 350;
            // 
            // colcod_CECO
            // 
            this.colcod_CECO.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_CECO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_CECO.Caption = "Cod. CECO";
            this.colcod_CECO.FieldName = "cod_CECO";
            this.colcod_CECO.Name = "colcod_CECO";
            this.colcod_CECO.OptionsColumn.AllowEdit = false;
            this.colcod_CECO.OptionsColumn.FixedWidth = true;
            this.colcod_CECO.Visible = true;
            this.colcod_CECO.VisibleIndex = 2;
            this.colcod_CECO.Width = 80;
            // 
            // colporc_distribucion
            // 
            this.colporc_distribucion.Caption = "%";
            this.colporc_distribucion.ColumnEdit = this.rtxtPorcentaje;
            this.colporc_distribucion.FieldName = "porc_distribucion";
            this.colporc_distribucion.Name = "colporc_distribucion";
            this.colporc_distribucion.OptionsColumn.FixedWidth = true;
            this.colporc_distribucion.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "porc_distribucion", "{0:#,#.00}")});
            this.colporc_distribucion.Visible = true;
            this.colporc_distribucion.VisibleIndex = 3;
            this.colporc_distribucion.Width = 50;
            // 
            // rtxtPorcentaje
            // 
            this.rtxtPorcentaje.AutoHeight = false;
            this.rtxtPorcentaje.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.rtxtPorcentaje.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.rtxtPorcentaje.MaskSettings.Set("mask", "p");
            this.rtxtPorcentaje.MaskSettings.Set("culture", "es-PE");
            this.rtxtPorcentaje.Name = "rtxtPorcentaje";
            this.rtxtPorcentaje.UseMaskAsDisplayFormat = true;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListadoCECO);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 38);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(975, 468);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.Root.Size = new System.Drawing.Size(975, 468);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoCECO;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(967, 460);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmAsignarCECOMasivo
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 549);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAsignarCECOMasivo";
            this.TitleForeColor = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.frmAsignarCECOMasivo_Load);
            this.Controls.SetChildIndex(this.divFooter, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            this.divFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).EndInit();
            this.layout_footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoCECO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoCECO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoCECO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPorcentaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcListadoCECO;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoCECO;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsListadoCECO;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_item;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_nivel1;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nivel1;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_nivel2;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nivel2;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_nivel3;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nivel3;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_nivel4;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nivel4;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_CECO_abrev;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_CECO;
        private DevExpress.XtraGrid.Columns.GridColumn colporc_distribucion;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPorcentaje;
    }
}
