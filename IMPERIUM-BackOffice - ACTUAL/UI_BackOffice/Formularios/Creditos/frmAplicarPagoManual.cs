using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_BackOffice;
using BL_BackOffice;

namespace UI_BackOffice.Formularios.Creditos
{
    public partial class frmAplicarPagoManual : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public DateTime FechaPago; public decimal MontoPagado; public string dsc_destino = "";

        public frmAplicarPagoManual()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmAplicarPagoManual_Load(object sender, EventArgs e)
        {
            CultureInfo info = new CultureInfo("es-PE");
            string symbol = info.NumberFormat.CurrencySymbol;
            info.NumberFormat.CurrencySymbol = symbol;
            txtMontoPagado.Properties.Mask.Culture = info;
            dtFechaPago.EditValue = DateTime.Today;
            btnAceptar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            unit.CreditoVehicular.CargaCombosLookUp("Destino", lkpDestino, "cod_destino", "dsc_destino", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lkpDestino.EditValue == null) { MessageBox.Show("Debe seleccionar el destino.", "Aplicar pago manual", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (dtFechaPago.EditValue == null || Convert.ToDecimal(txtMontoPagado.EditValue) == 0) { MessageBox.Show("Debe ingresar la fecha de pago y el monto pagado.", "Aplicar pago manual", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            FechaPago = Convert.ToDateTime(dtFechaPago.EditValue);
            MontoPagado = Convert.ToDecimal(txtMontoPagado.EditValue);
            dsc_destino = lkpDestino.EditValue.ToString();
            this.Close();
        }

        private void frmAplicarPagoManual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}