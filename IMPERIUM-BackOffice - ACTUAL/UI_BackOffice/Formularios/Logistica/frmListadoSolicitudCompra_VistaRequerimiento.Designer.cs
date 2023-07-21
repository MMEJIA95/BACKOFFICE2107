namespace UI_BackOffice.Formularios.Logistica
{
    partial class frmListadoSolicitudCompra_VistaRequerimiento
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
            this.gcListadoRequerimientos = new DevExpress.XtraGrid.GridControl();
            this.bsListadoRequerimientos = new System.Windows.Forms.BindingSource(this.components);
            this.gvListadoRequerimientos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcod_requerimiento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_producto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_cantidad = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnum_restante = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_nombre_solicitante = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_CECO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_CECO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoRequerimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoRequerimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoRequerimientos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.Location = new System.Drawing.Point(0, 381);
            this.divFooter.Size = new System.Drawing.Size(1015, 10);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcListadoRequerimientos);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 34);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1015, 347);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcListadoRequerimientos
            // 
            this.gcListadoRequerimientos.DataSource = this.bsListadoRequerimientos;
            this.gcListadoRequerimientos.Location = new System.Drawing.Point(8, 18);
            this.gcListadoRequerimientos.MainView = this.gvListadoRequerimientos;
            this.gcListadoRequerimientos.Name = "gcListadoRequerimientos";
            this.gcListadoRequerimientos.Size = new System.Drawing.Size(999, 321);
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
            this.coldsc_producto,
            this.colnum_cantidad,
            this.colnum_restante,
            this.coldsc_nombre_solicitante,
            this.colcod_CECO,
            this.coldsc_CECO});
            this.gvListadoRequerimientos.GridControl = this.gcListadoRequerimientos;
            this.gvListadoRequerimientos.GroupCount = 1;
            this.gvListadoRequerimientos.Name = "gvListadoRequerimientos";
            this.gvListadoRequerimientos.OptionsBehavior.Editable = false;
            this.gvListadoRequerimientos.OptionsView.ShowIndicator = false;
            this.gvListadoRequerimientos.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coldsc_producto, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colcod_requerimiento
            // 
            this.colcod_requerimiento.Caption = "Requerimiento ";
            this.colcod_requerimiento.FieldName = "cod_requerimiento";
            this.colcod_requerimiento.Name = "colcod_requerimiento";
            this.colcod_requerimiento.OptionsColumn.AllowSize = false;
            this.colcod_requerimiento.OptionsColumn.FixedWidth = true;
            this.colcod_requerimiento.Visible = true;
            this.colcod_requerimiento.VisibleIndex = 0;
            this.colcod_requerimiento.Width = 86;
            // 
            // coldsc_producto
            // 
            this.coldsc_producto.Caption = "Producto";
            this.coldsc_producto.FieldName = "dsc_producto";
            this.coldsc_producto.Name = "coldsc_producto";
            this.coldsc_producto.Visible = true;
            this.coldsc_producto.VisibleIndex = 1;
            this.coldsc_producto.Width = 389;
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
            this.colnum_cantidad.OptionsColumn.AllowSize = false;
            this.colnum_cantidad.OptionsColumn.FixedWidth = true;
            this.colnum_cantidad.Visible = true;
            this.colnum_cantidad.VisibleIndex = 3;
            this.colnum_cantidad.Width = 80;
            // 
            // colnum_restante
            // 
            this.colnum_restante.AppearanceCell.Options.UseTextOptions = true;
            this.colnum_restante.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_restante.AppearanceHeader.Options.UseTextOptions = true;
            this.colnum_restante.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnum_restante.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnum_restante.Caption = "Pendiente de entrega";
            this.colnum_restante.FieldName = "num_restante";
            this.colnum_restante.Name = "colnum_restante";
            this.colnum_restante.OptionsColumn.AllowSize = false;
            this.colnum_restante.OptionsColumn.FixedWidth = true;
            this.colnum_restante.Visible = true;
            this.colnum_restante.VisibleIndex = 4;
            this.colnum_restante.Width = 80;
            // 
            // coldsc_nombre_solicitante
            // 
            this.coldsc_nombre_solicitante.Caption = "Solicitante";
            this.coldsc_nombre_solicitante.FieldName = "dsc_nombre_solicitante";
            this.coldsc_nombre_solicitante.Name = "coldsc_nombre_solicitante";
            this.coldsc_nombre_solicitante.Visible = true;
            this.coldsc_nombre_solicitante.VisibleIndex = 1;
            this.coldsc_nombre_solicitante.Width = 288;
            // 
            // colcod_CECO
            // 
            this.colcod_CECO.FieldName = "cod_CECO";
            this.colcod_CECO.Name = "colcod_CECO";
            // 
            // coldsc_CECO
            // 
            this.coldsc_CECO.Caption = "CECO";
            this.coldsc_CECO.FieldName = "dsc_CECO";
            this.coldsc_CECO.Name = "coldsc_CECO";
            this.coldsc_CECO.Visible = true;
            this.coldsc_CECO.VisibleIndex = 2;
            this.coldsc_CECO.Width = 463;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(1015, 347);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1003, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcListadoRequerimientos;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 10);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1003, 325);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmListadoSolicitudCompra_VistaRequerimiento
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 391);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmListadoSolicitudCompra_VistaRequerimiento";
            this.Text = "frmListadoSolicitudCompra_VistaRequerimiento";
            this.Load += new System.EventHandler(this.frmListadoSolicitudCompra_VistaRequerimiento_Load);
            this.Controls.SetChildIndex(this.divFooter, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcListadoRequerimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsListadoRequerimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvListadoRequerimientos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.GridControl gcListadoRequerimientos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvListadoRequerimientos;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_producto;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_cantidad;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.BindingSource bsListadoRequerimientos;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_requerimiento;
        private DevExpress.XtraGrid.Columns.GridColumn colnum_restante;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_nombre_solicitante;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_CECO;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_CECO;
    }
}