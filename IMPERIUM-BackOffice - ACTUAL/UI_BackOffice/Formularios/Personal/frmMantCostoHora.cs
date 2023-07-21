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
using BL_BackOffice;
using BE_BackOffice;

namespace UI_BackOffice.Formularios.Personal
{
    public partial class frmMantCostoHora : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public eControlHoras.eCostoHora eCostoHora = new eControlHoras.eCostoHora();

        public frmMantCostoHora()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (lkpUsuario.EditValue == null) { MessageBox.Show("Debe seleccionar un usuario", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpUsuario.Focus(); return; }

                eCostoHora = AsignarValores_CostoHoras();
                eCostoHora = unit.Trabajador.InsertarActualizar_CostoHoras<eControlHoras.eCostoHora>(eCostoHora);

                MessageBox.Show("Se ha guardado satisfactoriamente", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public eControlHoras.eCostoHora AsignarValores_CostoHoras()
        {
            eControlHoras.eCostoHora obj = new eControlHoras.eCostoHora();

            obj.cod_usuario = lkpUsuario.EditValue.ToString();
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            obj.imp_costo = Convert.ToDecimal(txtCostoHora.Text);

            return obj;
        }

        private void frmMantCostoHora_Load(object sender, EventArgs e)
        {
            cargarCombo();
        }

        public void cargarCombo()
        {
            unit.Trabajador.CargaCombosLookUp("UsuariosControlHoras", lkpUsuario, "cod_usuario", "dsc_usuario", "", valorDefecto: true);
            lkpUsuario.ItemIndex = 0;
            obtenerCostoxUsuario();
        }

        private void lkpUsuario_EditValueChanged(object sender, EventArgs e)
        {
            obtenerCostoxUsuario();
        }

        public void obtenerCostoxUsuario()
        {
            eCostoHora = unit.Trabajador.Obtener_costo_usuario<eControlHoras.eCostoHora>(lkpUsuario.EditValue.ToString());
            txtCostoHora.Text = eCostoHora == null ? "" : eCostoHora.imp_costo.ToString();
        }
    }
}