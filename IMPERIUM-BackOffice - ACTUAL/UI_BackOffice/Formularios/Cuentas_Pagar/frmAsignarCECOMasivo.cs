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

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmAsignarCECOMasivo : HNG_Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        List<eCeco> ListCECODisponible = new List<eCeco>();
        public List<eCeco> ListCECOAsignado = new List<eCeco>();
        public List<eFacturaProveedor.eFacturaProveedor_Distribucion> mylistLineasDetFactura = new List<eFacturaProveedor.eFacturaProveedor_Distribucion>();
        public string cod_empresa = "";
        public frmAsignarCECOMasivo()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoCECO, gvListadoCECO);
            gvListadoCECO.OptionsBehavior.Editable = true;
            gvListadoCECO.OptionsView.ShowIndicator = false;
            gvListadoCECO.OptionsView.ShowGroupPanel = false;
            //gvListadoCECO.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            //gvListadoCECO.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gvListadoCECO.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gvListadoCECO.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
        }

        private void frmAsignarCECOMasivo_Load(object sender, EventArgs e)
        {
            ListCECODisponible = unit.Factura.Obtener_ListadoCECOSNuevos<eCeco>(65, cod_empresa);

            foreach (eFacturaProveedor.eFacturaProveedor_Distribucion obj in mylistLineasDetFactura)
            {
                eCeco obj2 = ListCECODisponible.Find(x => x.cod_nivel1 == obj.cod_tipo_gasto && x.cod_nivel2 == obj.cod_und_negocio && x.cod_nivel3 == obj.cod_cliente && x.cod_nivel4 == obj.cod_proyecto);
                if (obj2 != null) ListCECODisponible.Remove(obj2);
            }
            bsListadoCECO.DataSource = null; bsListadoCECO.DataSource = ListCECODisponible;
            gvListadoCECO.RefreshData();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvListadoCECO.SelectedRowsCount == 0) { HNG.MessageWarning("Debe seleccionar los CECOS a vincular.", "ASIGNAR CECO"); return; }
                ListCECOAsignado.Clear();
                foreach (int nRow in gvListadoCECO.GetSelectedRows())
                {
                    eCeco obj = gvListadoCECO.GetRow(nRow) as eCeco;
                    ListCECOAsignado.Add(obj);
                }
                decimal tot = 0; 
                foreach (eFacturaProveedor.eFacturaProveedor_Distribucion obj2 in mylistLineasDetFactura)
                {
                    tot = tot + obj2.porc_distribucion;
                }
                foreach (eCeco obj3 in ListCECOAsignado)
                {
                    tot = tot + obj3.porc_distribucion;
                }
                if (tot > 1)
                {
                    HNG.MessageWarning("La suma de los porcentajes de distribución no puede exceder el 100%", "ASIGNAR CECO");
                    ListCECOAsignado.Clear();
                    return;
                }

                HNG.MessageSuccess("Se vinculó los CECOS de manera satisfactoria.", "ASIGNAR CECO");
                this.Close();
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }
    }
}
