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

namespace UI_BackOffice.Formularios.Creditos
{
    public partial class frmClienteCredito : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;

        public frmClienteCredito()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmClienteCredito_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumentoTitular, "cod_tipo_documento", "dsc_tipo_documento", "", valorDefecto: true);
            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumentoCliente, "cod_tipo_documento", "dsc_tipo_documento", "", valorDefecto: true);
            glkpTipoDocumentoCliente.EditValue = "DI001";
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_condicion = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = unit.Proveedores.ObtenerListadoGridLookup(nCombo, cod_condicion);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txtCodCredito.Text.Trim() == "") { MessageBox.Show("Debe ingresar el código del crédito", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtCodCredito.Select(); return; }
                if (glkpTipoDocumentoTitular.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de documento del titular", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); glkpTipoDocumentoTitular.Select(); return; }
                if (txtNroDocumentoTitular.Text.Trim() == "") { MessageBox.Show("Debe ingresar el número de documento del titular", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtNroDocumentoTitular.Select(); return; }
                if (txtNombreTitular.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre del titular", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtNombreTitular.Select(); return; }
                if (glkpTipoDocumentoCliente.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de documento del cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); glkpTipoDocumentoCliente.Select(); return; }
                if (txtNroDocumentoCliente.Text.Trim() == "") { MessageBox.Show("Debe ingresar el número de documento del cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtNroDocumentoCliente.Select(); return; }
                if (txtApellidoPaternoCliente.Text.Trim() == "") { MessageBox.Show("Debe ingresar el apellido paterno del cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtApellidoPaternoCliente.Select(); return; }
                if (txtApellidoMaternoCliente.Text.Trim() == "") { MessageBox.Show("Debe ingresar el apellido materno del cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtApellidoMaternoCliente.Select(); return; }
                if (txtNombreCliente.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre del cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtNombreCliente.Select(); return; }

                eCreditoVehicular obj = new eCreditoVehicular();
                obj.cod_credito = txtCodCredito.Text.Trim();
                obj.cod_tipo_documento_titular = glkpTipoDocumentoTitular.EditValue.ToString();
                obj.dsc_documento_titular = txtNroDocumentoTitular.Text.Trim();
                obj.dsc_titular = txtNombreTitular.Text.Trim();
                obj.cod_tipo_documento = glkpTipoDocumentoCliente.EditValue.ToString();
                obj.dsc_documento = txtNroDocumentoCliente.Text.Trim();
                obj.dsc_apellido_paterno = txtApellidoPaternoCliente.Text.Trim();
                obj.dsc_apellido_materno = txtApellidoMaternoCliente.Text.Trim();
                obj.dsc_nombres = txtNombreCliente.Text.Trim();
                obj.flg_activo = "SI";
                obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                obj = unit.CreditoVehicular.InsertarActualizar_CreditoVehicular<eCreditoVehicular>(obj);
                if (obj == null) { MessageBox.Show("Error al guardar los datos", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                MessageBox.Show("Se guardaron los datos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNuevo.Enabled = true; txtCodCredito.Enabled = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnNuevo.Enabled = false; txtCodCredito.Enabled = true;
            txtCodCredito.Text = ""; glkpTipoDocumentoTitular.EditValue = "DI004";
            txtNroDocumentoTitular.Text = ""; txtNombreTitular.Text = "";
            glkpTipoDocumentoCliente.EditValue = "DI001"; txtApellidoPaternoCliente.Text = "";
            txtApellidoMaternoCliente.Text = ""; txtNombreTitular.Text = "";
        }

        private void txtNroDocumentoTitular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNroDocumentoTitular.Text.Trim() == "20603017847") txtNombreTitular.Text = "HNG inversiones SAC";
                if (txtNroDocumentoTitular.Text.Trim() == "20601952034") txtNombreTitular.Text = "TransNorcom SAC";
            }
        }
    }
}