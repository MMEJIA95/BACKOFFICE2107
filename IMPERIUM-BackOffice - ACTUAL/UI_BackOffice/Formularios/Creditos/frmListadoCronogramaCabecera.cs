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
using BE_BackOffice;
using BL_BackOffice;
using System.Globalization;

namespace UI_BackOffice.Formularios.Creditos
{
    public partial class frmListadoCronogramaCabecera : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public eCreditoVehicular.eCronogramaCabecera eCab = new eCreditoVehicular.eCronogramaCabecera();

        public frmListadoCronogramaCabecera()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadoCronogramaCabecera_Load(object sender, EventArgs e)
        {
            CultureInfo info = new CultureInfo("es-PE");
            string symbol = info.NumberFormat.CurrencySymbol;
            info.NumberFormat.CurrencySymbol = symbol;
            rtxtImporte.Mask.Culture = info;
            Inicializar();
        }
        private void Inicializar()
        {
            eCab = null;
            List<eCreditoVehicular.eCronogramaCabecera> ListCronogramas = new List<eCreditoVehicular.eCronogramaCabecera>();
            ListCronogramas = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaCabecera>(8, "");
            bsListadoCronogramaCabecera.DataSource = ListCronogramas;
        }

        private void gvListadoCronogramaCabecera_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void frmListadoCronogramaCabecera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void gvListadoCronogramaCabecera_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoCronogramaCabecera_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Column.FieldName == "imp_Capital") e.Appearance.ForeColor = Color.Blue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoCronogramaCabecera_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eCreditoVehicular.eCronogramaCabecera obj = gvListadoCronogramaCabecera.GetFocusedRow() as eCreditoVehicular.eCronogramaCabecera;
                    eCab = obj;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}