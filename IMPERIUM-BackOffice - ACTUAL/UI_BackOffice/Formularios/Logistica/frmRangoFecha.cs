using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BE_BackOffice;
using BL_BackOffice;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmRangoFecha : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        

        public frmRangoFecha()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            fechaInicio = Convert.ToDateTime(dtFechaInicio.EditValue);
            fechaFin = Convert.ToDateTime(dtFechaFin.EditValue);

            this.Close();
        }

        private void FrmRangoFecha_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;

            dtFechaInicio.EditValue = new DateTime(DateTime.Today.Year, 01, 01);
            dtFechaFin.EditValue = date;
        }
    }

}