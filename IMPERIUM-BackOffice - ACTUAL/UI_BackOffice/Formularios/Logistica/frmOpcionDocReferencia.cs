using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmOpcionDocReferencia : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public string doc_referencia = "";
        public frmOpcionDocReferencia()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmOpcionDocReferencia_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            doc_referencia = grdbOpcionDocReferencia.SelectedIndex == 0 ? "01" : "02";
            this.Close();
        }
    }
}