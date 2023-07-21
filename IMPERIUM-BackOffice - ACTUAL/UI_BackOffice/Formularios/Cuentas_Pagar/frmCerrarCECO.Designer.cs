namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    partial class frmCerrarCECO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCerrarCECO));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListaEmpresas = new DevExpress.XtraGrid.GridControl();
            this.bsEmpresas = new System.Windows.Forms.BindingSource(this.components);
            this.gvListaEmpresas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colvalor_1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colvalor_2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rswcBloqueo = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
            this.rchkSeleccionado = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rRatingCalificacion = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            this.divFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).BeginInit();
            this.layout_footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListaEmpresas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpresas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaEmpresas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rswcBloqueo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSeleccionado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.Location = new System.Drawing.Point(0, 442);
            this.divFooter.Size = new System.Drawing.Size(692, 43);
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
            this.btnCancelar.Location = new System.Drawing.Point(349, 2);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.btnGuardar.Location = new System.Drawing.Point(462, 2);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            // 
            // layout_footer
            // 
            this.layout_footer.Size = new System.Drawing.Size(692, 41);
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
            this.btnOpcional.Location = new System.Drawing.Point(234, 2);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListaEmpresas);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 38);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(692, 404);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListaEmpresas
            // 
            this.gcListaEmpresas.DataSource = this.bsEmpresas;
            this.gcListaEmpresas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcListaEmpresas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcListaEmpresas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcListaEmpresas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcListaEmpresas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcListaEmpresas.Location = new System.Drawing.Point(8, 35);
            this.gcListaEmpresas.MainView = this.gvListaEmpresas;
            this.gcListaEmpresas.Name = "gcListaEmpresas";
            this.gcListaEmpresas.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkSeleccionado,
            this.rRatingCalificacion,
            this.rswcBloqueo});
            this.gcListaEmpresas.Size = new System.Drawing.Size(676, 361);
            this.gcListaEmpresas.TabIndex = 0;
            this.gcListaEmpresas.UseEmbeddedNavigator = true;
            this.gcListaEmpresas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListaEmpresas});
            // 
            // bsEmpresas
            // 
            this.bsEmpresas.DataSource = typeof(BE_BackOffice.eParametrosGenerales);
            // 
            // gvListaEmpresas
            // 
            this.gvListaEmpresas.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListaEmpresas.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvListaEmpresas.Appearance.FocusedCell.Options.UseFont = true;
            this.gvListaEmpresas.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvListaEmpresas.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListaEmpresas.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvListaEmpresas.Appearance.FocusedRow.Options.UseFont = true;
            this.gvListaEmpresas.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvListaEmpresas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gvListaEmpresas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvListaEmpresas.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvListaEmpresas.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvListaEmpresas.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListaEmpresas.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gvListaEmpresas.Appearance.HideSelectionRow.Options.UseFont = true;
            this.gvListaEmpresas.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvListaEmpresas.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvListaEmpresas.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvListaEmpresas.Appearance.SelectedRow.Options.UseFont = true;
            this.gvListaEmpresas.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvListaEmpresas.ColumnPanelRowHeight = 30;
            this.gvListaEmpresas.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colvalor_1,
            this.coldsc_empresa,
            this.colvalor_2});
            this.gvListaEmpresas.GridControl = this.gcListaEmpresas;
            this.gvListaEmpresas.Name = "gvListaEmpresas";
            this.gvListaEmpresas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvListaEmpresas.OptionsView.ShowGroupPanel = false;
            this.gvListaEmpresas.OptionsView.ShowIndicator = false;
            this.gvListaEmpresas.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coldsc_empresa, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colvalor_1
            // 
            this.colvalor_1.Caption = "Cod. Empresa";
            this.colvalor_1.FieldName = "valor_1";
            this.colvalor_1.Name = "colvalor_1";
            // 
            // coldsc_empresa
            // 
            this.coldsc_empresa.Caption = "Empresa";
            this.coldsc_empresa.FieldName = "dsc_empresa";
            this.coldsc_empresa.Name = "coldsc_empresa";
            this.coldsc_empresa.OptionsColumn.AllowEdit = false;
            this.coldsc_empresa.OptionsColumn.ReadOnly = true;
            this.coldsc_empresa.Visible = true;
            this.coldsc_empresa.VisibleIndex = 0;
            this.coldsc_empresa.Width = 367;
            // 
            // colvalor_2
            // 
            this.colvalor_2.Caption = "Bloqueo";
            this.colvalor_2.ColumnEdit = this.rswcBloqueo;
            this.colvalor_2.FieldName = "valor_2";
            this.colvalor_2.Name = "colvalor_2";
            this.colvalor_2.OptionsColumn.FixedWidth = true;
            this.colvalor_2.Visible = true;
            this.colvalor_2.VisibleIndex = 1;
            this.colvalor_2.Width = 70;
            // 
            // rswcBloqueo
            // 
            this.rswcBloqueo.AutoHeight = false;
            this.rswcBloqueo.Name = "rswcBloqueo";
            this.rswcBloqueo.OffText = "NO";
            this.rswcBloqueo.OnText = "SI";
            this.rswcBloqueo.ValueOff = "NO";
            this.rswcBloqueo.ValueOn = "SI";
            // 
            // rchkSeleccionado
            // 
            this.rchkSeleccionado.AutoHeight = false;
            this.rchkSeleccionado.Name = "rchkSeleccionado";
            // 
            // rRatingCalificacion
            // 
            this.rRatingCalificacion.AutoHeight = false;
            this.rRatingCalificacion.CheckedGlyph = global::UI_BackOffice.Properties.Resources.estrella_azul;
            this.rRatingCalificacion.HoverGlyph = ((System.Drawing.Image)(resources.GetObject("rRatingCalificacion.HoverGlyph")));
            this.rRatingCalificacion.ItemCount = 4;
            this.rRatingCalificacion.ItemIndent = 10;
            this.rRatingCalificacion.Name = "rRatingCalificacion";
            this.rRatingCalificacion.ShowToolTips = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleLabelItem1,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(692, 404);
            this.Root.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.simpleLabelItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(680, 27);
            this.simpleLabelItem1.Text = "Bloquear CECO por Empresa";
            this.simpleLabelItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(270, 23);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListaEmpresas;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 27);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(680, 365);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmCerrarCECO
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 485);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmCerrarCECO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TitleForeColor = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.frmCerrarCECO_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCerrarCECO_KeyDown);
            this.Controls.SetChildIndex(this.divFooter, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            this.divFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).EndInit();
            this.layout_footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListaEmpresas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmpresas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaEmpresas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rswcBloqueo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSeleccionado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rRatingCalificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.GridControl gcListaEmpresas;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListaEmpresas;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSeleccionado;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresa;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl rRatingCalificacion;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsEmpresas;
        private DevExpress.XtraGrid.Columns.GridColumn colvalor_2;
        private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch rswcBloqueo;
        private DevExpress.XtraGrid.Columns.GridColumn colvalor_1;
    }
}