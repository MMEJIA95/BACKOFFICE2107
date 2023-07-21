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


namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmAgregarMarca : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public int cod_marca = 0;

        public frmAgregarMarca()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmAgregarMarca_Load(object sender, EventArgs e)
        {
            btnGuardar.Appearance.BackColor = Program.Sesion.Colores.Verde;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescMarca.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre de la marca.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDescMarca.Focus(); return; }
                if (txtAbreviadoMarca.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre abreviado de la marca.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtAbreviadoMarca.Focus(); return; }
                eProveedor_Marca obj = new eProveedor_Marca();
                obj.cod_marca = Convert.ToInt32(txtCodigoMarca.Text);
                obj.dsc_marca = txtDescMarca.Text.Trim();
                obj.dsc_abreviado = txtAbreviadoMarca.Text.Trim();
                obj.flg_activo = chkFlgActivoMarca.CheckState == CheckState.Checked ? "SI" : "NO";
                obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                obj = unit.Logistica.Insertar_Actualizar_Marcas<eProveedor_Marca>(obj);
                if (obj == null) { MessageBox.Show("Error al crear marca", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                cod_marca = obj.cod_marca;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}