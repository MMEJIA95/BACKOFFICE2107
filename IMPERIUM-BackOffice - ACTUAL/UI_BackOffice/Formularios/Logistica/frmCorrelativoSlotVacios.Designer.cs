namespace UI_BackOffice.Formularios.Logistica
{
    partial class frmCorrelativoSlotVacios
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
            this.gcCorrelativoList = new DevExpress.XtraGrid.GridControl();
            this.gvCorrelativoList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_codigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_descripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_valor = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Contenedor)).BeginInit();
            this.Contenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).BeginInit();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCorrelativoList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCorrelativoList)).BeginInit();
            this.SuspendLayout();
            // 
            // Contenedor
            // 
            this.Contenedor.Controls.Add(this.gcCorrelativoList);
            this.Contenedor.Size = new System.Drawing.Size(246, 351);
            // 
            // title
            // 
            this.title.ForeColor = System.Drawing.Color.Black;
            this.title.Size = new System.Drawing.Size(211, 28);
            this.title.Text = "Correlativos";
            // 
            // pnlContainer
            // 
            this.pnlContainer.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlContainer.Appearance.Options.UseBackColor = true;
            this.pnlContainer.Location = new System.Drawing.Point(4, 33);
            this.pnlContainer.Size = new System.Drawing.Size(246, 351);
            // 
            // cmdMinim
            // 
            this.cmdMinim.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.cmdMinim.Location = new System.Drawing.Point(131, 0);
            this.cmdMinim.Visible = false;
            // 
            // cmdMaxim
            // 
            this.cmdMaxim.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.cmdMaxim.Location = new System.Drawing.Point(171, 0);
            this.cmdMaxim.Visible = false;
            // 
            // gcCorrelativoList
            // 
            this.gcCorrelativoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCorrelativoList.Location = new System.Drawing.Point(0, 0);
            this.gcCorrelativoList.MainView = this.gvCorrelativoList;
            this.gcCorrelativoList.Name = "gcCorrelativoList";
            this.gcCorrelativoList.Size = new System.Drawing.Size(246, 351);
            this.gcCorrelativoList.TabIndex = 0;
            this.gcCorrelativoList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCorrelativoList});
            // 
            // gvCorrelativoList
            // 
            this.gvCorrelativoList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_codigo,
            this.col_descripcion,
            this.col_valor});
            this.gvCorrelativoList.GridControl = this.gcCorrelativoList;
            this.gvCorrelativoList.Name = "gvCorrelativoList";
            this.gvCorrelativoList.OptionsView.ShowIndicator = false;
            // 
            // col_codigo
            // 
            this.col_codigo.Caption = "Código";
            this.col_codigo.FieldName = "Codigo";
            this.col_codigo.Name = "col_codigo";
            this.col_codigo.Visible = true;
            this.col_codigo.VisibleIndex = 0;
            this.col_codigo.Width = 96;
            // 
            // col_descripcion
            // 
            this.col_descripcion.Caption = "Descripción";
            this.col_descripcion.FieldName = "Descripcion";
            this.col_descripcion.Name = "col_descripcion";
            this.col_descripcion.Visible = true;
            this.col_descripcion.VisibleIndex = 1;
            this.col_descripcion.Width = 136;
            // 
            // col_valor
            // 
            this.col_valor.Caption = "Valor";
            this.col_valor.FieldName = "Valor";
            this.col_valor.Name = "col_valor";
            // 
            // frmCorrelativoSlotVacios
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 394);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmCorrelativoSlotVacios";
            this.Text = "frmCorrelativoSlotVacios";
            this.Load += new System.EventHandler(this.frmCorrelativoSlotVacios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Contenedor)).EndInit();
            this.Contenedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCorrelativoList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCorrelativoList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCorrelativoList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCorrelativoList;
        private DevExpress.XtraGrid.Columns.GridColumn col_codigo;
        private DevExpress.XtraGrid.Columns.GridColumn col_descripcion;
        private DevExpress.XtraGrid.Columns.GridColumn col_valor;
    }
}