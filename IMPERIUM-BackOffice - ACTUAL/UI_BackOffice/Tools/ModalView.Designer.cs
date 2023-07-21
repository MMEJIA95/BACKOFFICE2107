namespace UI_BackOffice.Tools
{
    partial class ModalView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModalView));
            this.pnlTopControl = new DevExpress.XtraEditors.PanelControl();
            this.cmdMinim = new System.Windows.Forms.Button();
            this.cmdMaxim = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.pnlContainer = new DevExpress.XtraEditors.PanelControl();
            this.Contenedor = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopControl)).BeginInit();
            this.pnlTopControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).BeginInit();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Contenedor)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopControl
            // 
            this.pnlTopControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTopControl.Controls.Add(this.cmdMinim);
            this.pnlTopControl.Controls.Add(this.cmdMaxim);
            this.pnlTopControl.Controls.Add(this.title);
            this.pnlTopControl.Controls.Add(this.cmdClose);
            this.pnlTopControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopControl.Location = new System.Drawing.Point(0, 0);
            this.pnlTopControl.Name = "pnlTopControl";
            this.pnlTopControl.Size = new System.Drawing.Size(745, 28);
            this.pnlTopControl.TabIndex = 0;
            // 
            // cmdMinim
            // 
            this.cmdMinim.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdMinim.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdMinim.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.cmdMinim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMinim.Font = new System.Drawing.Font("Verdana", 9F);
            this.cmdMinim.Image = ((System.Drawing.Image)(resources.GetObject("cmdMinim.Image")));
            this.cmdMinim.Location = new System.Drawing.Point(622, 0);
            this.cmdMinim.Name = "cmdMinim";
            this.cmdMinim.Size = new System.Drawing.Size(40, 28);
            this.cmdMinim.TabIndex = 1;
            this.cmdMinim.UseVisualStyleBackColor = false;
            this.cmdMinim.Click += new System.EventHandler(this.cmdMinim_Click);
            // 
            // cmdMaxim
            // 
            this.cmdMaxim.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdMaxim.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdMaxim.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.cmdMaxim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMaxim.Font = new System.Drawing.Font("Verdana", 9F);
            this.cmdMaxim.Image = ((System.Drawing.Image)(resources.GetObject("cmdMaxim.Image")));
            this.cmdMaxim.Location = new System.Drawing.Point(662, 0);
            this.cmdMaxim.Name = "cmdMaxim";
            this.cmdMaxim.Size = new System.Drawing.Size(40, 28);
            this.cmdMaxim.TabIndex = 1;
            this.cmdMaxim.UseVisualStyleBackColor = false;
            this.cmdMaxim.Click += new System.EventHandler(this.cmdMaxim_Click);
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(139)))), ((int)(((byte)(125)))));
            this.title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.title.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(702, 28);
            this.title.TabIndex = 0;
            this.title.Text = "Title";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.title.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.title_MouseDoubleClick);
            this.title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.title_MouseDown_1);
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Crimson;
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdClose.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Font = new System.Drawing.Font("Verdana", 9F);
            this.cmdClose.ForeColor = System.Drawing.Color.White;
            this.cmdClose.Location = new System.Drawing.Point(702, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(43, 28);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "X";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlContainer.Appearance.Options.UseBackColor = true;
            this.pnlContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlContainer.Controls.Add(this.Contenedor);
            this.pnlContainer.Location = new System.Drawing.Point(3, 31);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(740, 338);
            this.pnlContainer.TabIndex = 0;
            // 
            // Contenedor
            // 
            this.Contenedor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Contenedor.Location = new System.Drawing.Point(0, 0);
            this.Contenedor.Name = "Contenedor";
            this.Contenedor.Size = new System.Drawing.Size(740, 338);
            this.Contenedor.TabIndex = 0;
            // 
            // ModalView
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(745, 373);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlTopControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ModalView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModalView";
            this.Load += new System.EventHandler(this.ModalView_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ModalView_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ModalView_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopControl)).EndInit();
            this.pnlTopControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Contenedor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTopControl;
        public DevExpress.XtraEditors.PanelControl Contenedor;
        private System.Windows.Forms.Button cmdClose;
        public System.Windows.Forms.Label title;
        public DevExpress.XtraEditors.PanelControl pnlContainer;
        public System.Windows.Forms.Button cmdMinim;
        public System.Windows.Forms.Button cmdMaxim;
    }
}