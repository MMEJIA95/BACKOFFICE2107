namespace UI_BackOffice.Formularios.Logistica
{
    partial class frmListadoSolicitudCompra_PrevioOrdenCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListadoSolicitudCompra_PrevioOrdenCompra));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcListadoRequerimientos = new DevExpress.XtraGrid.GridControl();
            this.bsListadoRequerimientos = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoRequerimientos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_requerimiento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_producto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_producto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_cantidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_restante = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_cantidad_disponer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            this.divFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).BeginInit();
            this.layout_footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoRequerimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoRequerimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoRequerimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.Location = new System.Drawing.Point(0, 340);
            this.divFooter.Size = new System.Drawing.Size(666, 43);
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
            this.btnCancelar.Location = new System.Drawing.Point(341, 2);
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
            this.btnGuardar.Location = new System.Drawing.Point(454, 2);
            this.btnGuardar.Text = "GENERAR";
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
            this.layout_footer.Size = new System.Drawing.Size(666, 41);
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
            this.btnOpcional.Location = new System.Drawing.Point(226, 2);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListadoRequerimientos);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 38);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(666, 302);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListadoRequerimientos
            // 
            this.gcListadoRequerimientos.DataSource = this.bsListadoRequerimientos;
            this.gcListadoRequerimientos.Location = new System.Drawing.Point(8, 8);
            this.gcListadoRequerimientos.MainView = this.gvListadoRequerimientos;
            this.gcListadoRequerimientos.Name = "gcListadoRequerimientos";
            this.gcListadoRequerimientos.Size = new System.Drawing.Size(650, 286);
            this.gcListadoRequerimientos.TabIndex = 6;
            this.gcListadoRequerimientos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvListadoRequerimientos});
            // 
            // bsListadoRequerimientos
            // 
            this.bsListadoRequerimientos.DataSource = typeof(BE_BackOffice.eSolicitudCompra_Requerimientos);
            // 
            // gvListadoRequerimientos
            // 
            this.gvListadoRequerimientos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcod_requerimiento,
            this.colcod_producto,
            this.coldsc_producto,
            this.colnum_cantidad,
            this.colnum_restante,
            this.colnum_cantidad_disponer});
            this.gvListadoRequerimientos.GridControl = this.gcListadoRequerimientos;
            this.gvListadoRequerimientos.GroupCount = 1;
            this.gvListadoRequerimientos.Name = "gvListadoRequerimientos";
            this.gvListadoRequerimientos.OptionsView.ShowIndicator = false;
            this.gvListadoRequerimientos.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colcod_requerimiento, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvListadoRequerimientos.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvListadoRequerimientos_RowClick);
            this.gvListadoRequerimientos.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvListadoRequerimientos_CustomDrawCell);
            // 
            // colcod_requerimiento
            // 
            this.colcod_requerimiento.Caption = "Requerimiento ";
            this.colcod_requerimiento.FieldName = "cod_requerimiento";
            this.colcod_requerimiento.Name = "colcod_requerimiento";
            this.colcod_requerimiento.OptionsColumn.AllowEdit = false;
            this.colcod_requerimiento.Visible = true;
            this.colcod_requerimiento.VisibleIndex = 0;
            // 
            // colcod_producto
            // 
            this.colcod_producto.FieldName = "cod_producto";
            this.colcod_producto.Name = "colcod_producto";
            this.colcod_producto.OptionsColumn.AllowEdit = false;
            // 
            // coldsc_producto
            // 
            this.coldsc_producto.Caption = "Producto";
            this.coldsc_producto.FieldName = "dsc_producto";
            this.coldsc_producto.Name = "coldsc_producto";
            this.coldsc_producto.OptionsColumn.AllowEdit = false;
            this.coldsc_producto.Visible = true;
            this.coldsc_producto.VisibleIndex = 0;
            this.coldsc_producto.Width = 423;
            // 
            // colnum_cantidad
            // 
            this.colnum_cantidad.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_cantidad.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_cantidad.AppearanceHeader.Options.UseTextOptions = true;
            this.colnum_cantidad.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_cantidad.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnum_cantidad.Caption = "Cantidad solicitada";
            this.colnum_cantidad.FieldName = "num_cantidad";
            this.colnum_cantidad.Name = "colnum_cantidad";
            this.colnum_cantidad.OptionsColumn.AllowEdit = false;
            this.colnum_cantidad.OptionsColumn.AllowSize = false;
            this.colnum_cantidad.OptionsColumn.FixedWidth = true;
            this.colnum_cantidad.Visible = true;
            this.colnum_cantidad.VisibleIndex = 1;
            this.colnum_cantidad.Width = 80;
            // 
            // colnum_restante
            // 
            this.colnum_restante.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_restante.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_restante.AppearanceHeader.Options.UseTextOptions = true;
            this.colnum_restante.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_restante.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnum_restante.Caption = "Cantidad restante";
            this.colnum_restante.FieldName = "num_restante";
            this.colnum_restante.Name = "colnum_restante";
            this.colnum_restante.OptionsColumn.AllowEdit = false;
            this.colnum_restante.OptionsColumn.AllowSize = false;
            this.colnum_restante.OptionsColumn.FixedWidth = true;
            this.colnum_restante.Visible = true;
            this.colnum_restante.VisibleIndex = 2;
            this.colnum_restante.Width = 80;
            // 
            // colnum_cantidad_disponer
            // 
            this.colnum_cantidad_disponer.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_cantidad_disponer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_cantidad_disponer.AppearanceHeader.Options.UseTextOptions = true;
            this.colnum_cantidad_disponer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_cantidad_disponer.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnum_cantidad_disponer.Caption = "Cantidad a comprar";
            this.colnum_cantidad_disponer.FieldName = "num_cantidad_disponer";
            this.colnum_cantidad_disponer.Name = "colnum_cantidad_disponer";
            this.colnum_cantidad_disponer.OptionsColumn.AllowSize = false;
            this.colnum_cantidad_disponer.OptionsColumn.FixedWidth = true;
            this.colnum_cantidad_disponer.Visible = true;
            this.colnum_cantidad_disponer.VisibleIndex = 3;
            this.colnum_cantidad_disponer.Width = 80;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(666, 302);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoRequerimientos;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(654, 290);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmListadoSolicitudCompra_PrevioOrdenCompra
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 383);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmListadoSolicitudCompra_PrevioOrdenCompra";
            this.Text = "frmListadoSolicitudCompra_PrevioOrdenCompra";
            this.TitleForeColor = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.frmListadoSolicitudCompra_PrevioOrdenCompra_Load);
            this.Controls.SetChildIndex(this.divFooter, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            this.divFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_footer)).EndInit();
            this.layout_footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoRequerimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoRequerimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoRequerimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcListadoRequerimientos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoRequerimientos;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_requerimiento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_producto;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_cantidad;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsListadoRequerimientos;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_producto;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_cantidad_disponer;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_restante;
    }
}