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

namespace UI_BackOffice.Formularios.Sistema.Configuraciones_Maestras
{
    public partial class frmTraerTipoCambio_Fechas : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public string FechaDesde = "", FechaHasta = "";

        public frmTraerTipoCambio_Fechas()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmTraerTipoCambio_Fechas_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        private void Inicializar()
        {
            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            dtFechaDesde.EditValue = DateTime.Today;
            dtFechatHasta.EditValue = DateTime.Today;
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lblTitulo.Select();
            FechaDesde = Convert.ToDateTime(dtFechaDesde.EditValue).ToString("yyyy-MM-dd");
            FechaHasta = Convert.ToDateTime(dtFechatHasta.EditValue).ToString("dd/MM/yyyy");
            this.Close();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}