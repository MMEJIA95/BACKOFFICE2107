
namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    partial class frmHistorialAprobaciones
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
            this.gcHistorial = new DevExpress.XtraGrid.GridControl();
            this.bsFactura = new System.Windows.Forms.BindingSource(this.components);
            this.gvHistorial = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coldsc_tipo_comprobante = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_razon_social = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_registro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcod_moneda = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_total = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colimp_saldo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_vencimiento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnumero_documento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_usuario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldsc_empresa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colfch_aprobado_reg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtFechaInicio = new DevExpress.XtraEditors.DateEdit();
            this.dtFechaFin = new DevExpress.XtraEditors.DateEdit();
            this.chklkpTrabajador = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.btnBusqueda = new DevExpress.XtraEditors.SimpleButton();
            this.lkpEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpModulo = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcHistorial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFactura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHistorial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaInicio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaInicio.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaFin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chklkpTrabajador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpModulo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // divFooter
            // 
            this.divFooter.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.divFooter.Appearance.Options.UseBackColor = true;
            this.divFooter.Location = new System.Drawing.Point(0, 436);
            this.divFooter.Size = new System.Drawing.Size(1226, 10);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcHistorial);
            this.layoutControl1.Controls.Add(this.dtFechaInicio);
            this.layoutControl1.Controls.Add(this.dtFechaFin);
            this.layoutControl1.Controls.Add(this.chklkpTrabajador);
            this.layoutControl1.Controls.Add(this.btnBusqueda);
            this.layoutControl1.Controls.Add(this.lkpEmpresa);
            this.layoutControl1.Controls.Add(this.lkpModulo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 34);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1226, 402);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcHistorial
            // 
            this.gcHistorial.DataSource = this.bsFactura;
            this.gcHistorial.Location = new System.Drawing.Point(4, 30);
            this.gcHistorial.MainView = this.gvHistorial;
            this.gcHistorial.Name = "gcHistorial";
            this.gcHistorial.Size = new System.Drawing.Size(1218, 368);
            this.gcHistorial.TabIndex = 4;
            this.gcHistorial.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHistorial});
            // 
            // bsFactura
            // 
            this.bsFactura.DataSource = typeof(BE_BackOffice.EAprobaciones.eTrabajador);
            // 
            // gvHistorial
            // 
            this.gvHistorial.ColumnPanelRowHeight = 35;
            this.gvHistorial.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coldsc_tipo_comprobante,
            this.coldocumento,
            this.coldsc_razon_social,
            this.colfch_registro,
            this.colfch_documento,
            this.colcod_moneda,
            this.colimp_total,
            this.colimp_saldo,
            this.colfch_vencimiento,
            this.colnumero_documento,
            this.coldsc_usuario,
            this.coldsc_empresa,
            this.colfch_aprobado_reg});
            this.gvHistorial.GridControl = this.gcHistorial;
            this.gvHistorial.GroupCount = 2;
            this.gvHistorial.Name = "gvHistorial";
            this.gvHistorial.OptionsView.ShowGroupPanel = false;
            this.gvHistorial.OptionsView.ShowIndicator = false;
            this.gvHistorial.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coldsc_empresa, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.coldsc_usuario, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // coldsc_tipo_comprobante
            // 
            this.coldsc_tipo_comprobante.AppearanceHeader.Options.UseTextOptions = true;
            this.coldsc_tipo_comprobante.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coldsc_tipo_comprobante.Caption = "Tipo de Comprobante";
            this.coldsc_tipo_comprobante.FieldName = "dsc_tipo_comprobante";
            this.coldsc_tipo_comprobante.Name = "coldsc_tipo_comprobante";
            this.coldsc_tipo_comprobante.Visible = true;
            this.coldsc_tipo_comprobante.VisibleIndex = 0;
            this.coldsc_tipo_comprobante.Width = 111;
            // 
            // coldocumento
            // 
            this.coldocumento.AppearanceCell.Options.UseTextOptions = true;
            this.coldocumento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldocumento.AppearanceHeader.Options.UseTextOptions = true;
            this.coldocumento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coldocumento.Caption = "Documento";
            this.coldocumento.FieldName = "documento";
            this.coldocumento.Name = "coldocumento";
            this.coldocumento.Visible = true;
            this.coldocumento.VisibleIndex = 1;
            this.coldocumento.Width = 74;
            // 
            // coldsc_razon_social
            // 
            this.coldsc_razon_social.Caption = "Razon Social";
            this.coldsc_razon_social.FieldName = "dsc_razon_social";
            this.coldsc_razon_social.Name = "coldsc_razon_social";
            this.coldsc_razon_social.Visible = true;
            this.coldsc_razon_social.VisibleIndex = 2;
            this.coldsc_razon_social.Width = 349;
            // 
            // colfch_registro
            // 
            this.colfch_registro.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_registro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_registro.Caption = "Fecha Registro";
            this.colfch_registro.FieldName = "fch_registro";
            this.colfch_registro.Name = "colfch_registro";
            this.colfch_registro.Visible = true;
            this.colfch_registro.VisibleIndex = 3;
            this.colfch_registro.Width = 119;
            // 
            // colfch_documento
            // 
            this.colfch_documento.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_documento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_documento.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_documento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_documento.Caption = "Fecha Documento";
            this.colfch_documento.FieldName = "fch_documento";
            this.colfch_documento.Name = "colfch_documento";
            this.colfch_documento.Visible = true;
            this.colfch_documento.VisibleIndex = 4;
            this.colfch_documento.Width = 97;
            // 
            // colcod_moneda
            // 
            this.colcod_moneda.AppearanceCell.Options.UseTextOptions = true;
            this.colcod_moneda.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_moneda.AppearanceHeader.Options.UseTextOptions = true;
            this.colcod_moneda.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcod_moneda.Caption = "Moneda";
            this.colcod_moneda.FieldName = "cod_moneda";
            this.colcod_moneda.Name = "colcod_moneda";
            this.colcod_moneda.Visible = true;
            this.colcod_moneda.VisibleIndex = 5;
            this.colcod_moneda.Width = 58;
            // 
            // colimp_total
            // 
            this.colimp_total.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_total.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_total.AppearanceHeader.Options.UseTextOptions = true;
            this.colimp_total.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_total.Caption = "Importe Total";
            this.colimp_total.FieldName = "imp_total";
            this.colimp_total.Name = "colimp_total";
            this.colimp_total.Visible = true;
            this.colimp_total.VisibleIndex = 6;
            this.colimp_total.Width = 89;
            // 
            // colimp_saldo
            // 
            this.colimp_saldo.AppearanceCell.Options.UseTextOptions = true;
            this.colimp_saldo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_saldo.AppearanceHeader.Options.UseTextOptions = true;
            this.colimp_saldo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colimp_saldo.Caption = "Importe Saldo";
            this.colimp_saldo.FieldName = "imp_saldo";
            this.colimp_saldo.Name = "colimp_saldo";
            this.colimp_saldo.Visible = true;
            this.colimp_saldo.VisibleIndex = 7;
            this.colimp_saldo.Width = 78;
            // 
            // colfch_vencimiento
            // 
            this.colfch_vencimiento.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_vencimiento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_vencimiento.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_vencimiento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_vencimiento.Caption = "Fecha Venc.";
            this.colfch_vencimiento.FieldName = "fch_vencimiento";
            this.colfch_vencimiento.Name = "colfch_vencimiento";
            this.colfch_vencimiento.Visible = true;
            this.colfch_vencimiento.VisibleIndex = 8;
            this.colfch_vencimiento.Width = 101;
            // 
            // colnumero_documento
            // 
            this.colnumero_documento.FieldName = "numero_documento";
            this.colnumero_documento.Name = "colnumero_documento";
            // 
            // coldsc_usuario
            // 
            this.coldsc_usuario.Caption = "Usuario";
            this.coldsc_usuario.FieldName = "dsc_usuario";
            this.coldsc_usuario.Name = "coldsc_usuario";
            // 
            // coldsc_empresa
            // 
            this.coldsc_empresa.Caption = "Empresa";
            this.coldsc_empresa.FieldName = "dsc_empresa";
            this.coldsc_empresa.Name = "coldsc_empresa";
            // 
            // colfch_aprobado_reg
            // 
            this.colfch_aprobado_reg.AppearanceCell.BackColor = System.Drawing.Color.Honeydew;
            this.colfch_aprobado_reg.AppearanceCell.Options.UseBackColor = true;
            this.colfch_aprobado_reg.AppearanceCell.Options.UseTextOptions = true;
            this.colfch_aprobado_reg.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_aprobado_reg.AppearanceHeader.Options.UseTextOptions = true;
            this.colfch_aprobado_reg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colfch_aprobado_reg.Caption = "Fecha Aprobación";
            this.colfch_aprobado_reg.FieldName = "fch_aprobado_reg";
            this.colfch_aprobado_reg.Name = "colfch_aprobado_reg";
            this.colfch_aprobado_reg.Visible = true;
            this.colfch_aprobado_reg.VisibleIndex = 9;
            this.colfch_aprobado_reg.Width = 117;
            // 
            // dtFechaInicio
            // 
            this.dtFechaInicio.EditValue = null;
            this.dtFechaInicio.Location = new System.Drawing.Point(889, 4);
            this.dtFechaInicio.Name = "dtFechaInicio";
            this.dtFechaInicio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaInicio.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaInicio.Size = new System.Drawing.Size(113, 20);
            this.dtFechaInicio.StyleController = this.layoutControl1;
            this.dtFechaInicio.TabIndex = 7;
            // 
            // dtFechaFin
            // 
            this.dtFechaFin.EditValue = null;
            this.dtFechaFin.Location = new System.Drawing.Point(1043, 4);
            this.dtFechaFin.Name = "dtFechaFin";
            this.dtFechaFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaFin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFechaFin.Size = new System.Drawing.Size(100, 20);
            this.dtFechaFin.StyleController = this.layoutControl1;
            this.dtFechaFin.TabIndex = 8;
            // 
            // chklkpTrabajador
            // 
            this.chklkpTrabajador.Location = new System.Drawing.Point(566, 4);
            this.chklkpTrabajador.Name = "chklkpTrabajador";
            this.chklkpTrabajador.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chklkpTrabajador.Size = new System.Drawing.Size(280, 20);
            this.chklkpTrabajador.StyleController = this.layoutControl1;
            this.chklkpTrabajador.TabIndex = 6;
            this.chklkpTrabajador.EditValueChanged += new System.EventHandler(this.chklkpTrabajador_EditValueChanged);
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.btnBusqueda.Appearance.Options.UseBackColor = true;
            this.btnBusqueda.Location = new System.Drawing.Point(1147, 4);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(75, 22);
            this.btnBusqueda.StyleController = this.layoutControl1;
            this.btnBusqueda.TabIndex = 10;
            this.btnBusqueda.Text = "Buscar";
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_Click);
            // 
            // lkpEmpresa
            // 
            this.lkpEmpresa.EditValue = "";
            this.lkpEmpresa.Location = new System.Drawing.Point(61, 4);
            this.lkpEmpresa.Name = "lkpEmpresa";
            this.lkpEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpEmpresa.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_empresa", "Descripción")});
            this.lkpEmpresa.Properties.NullText = "";
            this.lkpEmpresa.Size = new System.Drawing.Size(199, 20);
            this.lkpEmpresa.StyleController = this.layoutControl1;
            this.lkpEmpresa.TabIndex = 9;
            // 
            // lkpModulo
            // 
            this.lkpModulo.Location = new System.Drawing.Point(307, 4);
            this.lkpModulo.Name = "lkpModulo";
            this.lkpModulo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpModulo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_modulo", "Descripción")});
            this.lkpModulo.Properties.NullText = "";
            this.lkpModulo.Size = new System.Drawing.Size(193, 20);
            this.lkpModulo.StyleController = this.layoutControl1;
            this.lkpModulo.TabIndex = 5;
            this.lkpModulo.EditValueChanged += new System.EventHandler(this.lkpModulo_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(1226, 402);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcHistorial;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1222, 372);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lkpModulo;
            this.layoutControlItem2.Location = new System.Drawing.Point(260, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(240, 26);
            this.layoutControlItem2.Text = "Modulo:";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(38, 13);
            this.layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chklkpTrabajador;
            this.layoutControlItem3.Location = new System.Drawing.Point(500, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(346, 26);
            this.layoutControlItem3.Text = "Trabajador:";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(57, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dtFechaInicio;
            this.layoutControlItem4.Location = new System.Drawing.Point(846, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(156, 26);
            this.layoutControlItem4.Text = "Desde:";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(34, 13);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dtFechaFin;
            this.layoutControlItem5.Location = new System.Drawing.Point(1002, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItem5.Text = "Hasta:";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(32, 13);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lkpEmpresa;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(260, 26);
            this.layoutControlItem6.Text = "Empresa:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnBusqueda;
            this.layoutControlItem7.Location = new System.Drawing.Point(1143, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(79, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // frmHistorialAprobaciones
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 446);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmHistorialAprobaciones";
            this.Text = "Historial de Aprobación";
            this.Controls.SetChildIndex(this.divFooter, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.divFooter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcHistorial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFactura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHistorial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaInicio.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaInicio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaFin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFechaFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chklkpTrabajador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpModulo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcHistorial;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHistorial;
        private DevExpress.XtraEditors.DateEdit dtFechaInicio;
        private DevExpress.XtraEditors.DateEdit dtFechaFin;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chklkpTrabajador;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnBusqueda;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private System.Windows.Forms.BindingSource bsFactura;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_tipo_comprobante;
        private DevExpress.XtraGrid.Columns.GridColumn coldocumento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_razon_social;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_registro;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_documento;
        private DevExpress.XtraGrid.Columns.GridColumn colcod_moneda;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_total;
        private DevExpress.XtraGrid.Columns.GridColumn colimp_saldo;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_vencimiento;
        private DevExpress.XtraGrid.Columns.GridColumn colnumero_documento;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_usuario;
        private DevExpress.XtraGrid.Columns.GridColumn coldsc_empresa;
        private DevExpress.XtraEditors.LookUpEdit lkpEmpresa;
        private DevExpress.XtraEditors.LookUpEdit lkpModulo;
        private DevExpress.XtraGrid.Columns.GridColumn colfch_aprobado_reg;
    }
}