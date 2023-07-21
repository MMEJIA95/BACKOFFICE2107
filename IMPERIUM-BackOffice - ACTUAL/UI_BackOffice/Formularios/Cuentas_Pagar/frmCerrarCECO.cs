using BE_BackOffice;
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

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmCerrarCECO : HNG_Tools.ModalForm
    {
        private readonly UnitOfWork unit;


        public frmCerrarCECO()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            ConfigurarFormulario();
        }

        private void frmCerrarCECO_Load(object sender, EventArgs e)
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            this.TitleForeColor = Color.White;
            List<eParametrosGenerales> lista = unit.Factura.Obtener_MaestrosGenerales<eParametrosGenerales>(63, "");
            bsEmpresas.DataSource = lista; gvListaEmpresas.RefreshData();
        }

        private void ConfigurarFormulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListaEmpresas, gvListaEmpresas);
            gvListaEmpresas.OptionsBehavior.Editable = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                for (int x = 0; x <= gvListaEmpresas.RowCount - 1; x++)
                {
                    eParametrosGenerales obj = gvListaEmpresas.GetRow(x) as eParametrosGenerales;
                    if (obj == null) continue;
                    obj = unit.Factura.BloqueoCECOxEmpresa<eParametrosGenerales>(obj);
                }
                HNG.MessageSuccess("Se guardo los cambios de manera satisfactoria.", "CERRAR CECO");
                this.Close();
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }

        private void frmCerrarCECO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}