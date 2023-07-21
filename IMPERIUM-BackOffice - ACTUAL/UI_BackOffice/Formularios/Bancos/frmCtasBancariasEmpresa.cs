using BE_BackOffice;
using BL_BackOffice;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.OpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_BackOffice.Formularios.Cuentas_Pagar;

namespace UI_BackOffice.Formularios.Bancos
{
    internal enum CtaBanco
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmCtasBancariasEmpresa : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        internal CtaBanco MiAccion = CtaBanco.Nuevo;
        public string cod_empresa;
        public int num_linea;
        public bool ActualizarListado = false;

        public frmCtasBancariasEmpresa()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmBancoEmpresa_Load(object sender, EventArgs e)
        {
            groupControl1.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            groupControl2.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            Inicializar();
        }

        private void Inicializar()
        {
            unit.Trabajador.CargaCombosLookUp("Empresa", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true);
            CargarCombosGridLookup("Banco", glkpBanco, "cod_banco", "dsc_banco", "");
            unit.Proveedores.CargaCombosLookUp("Moneda", lkpMoneda, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
            unit.Proveedores.CargaCombosLookUp("TipoCuentaBancaria", lkpTipoCuenta, "cod_tipo_cuenta", "dsc_tipo_cuenta", "");
            switch (MiAccion)
            {
                case CtaBanco.Nuevo:
                    break;
                case CtaBanco.Editar:
                    Editar();
                    txtCodigo.Select();
                    break;
            }
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

        private void Editar()
        {
            eEmpresa.eBanco_Empresa obj = new eEmpresa.eBanco_Empresa();
            obj.cod_empresa = cod_empresa; obj.num_linea = num_linea;
            obj = unit.Factura.Obtener_CuentasBancoEmpresa<eEmpresa.eBanco_Empresa>(4, obj);
            txtCodigo.Text = obj.num_linea.ToString();
            lkpEmpresa.EditValue = obj.cod_empresa;
            glkpBanco.EditValue = obj.cod_banco;
            lkpMoneda.EditValue = obj.cod_moneda;
            lkpTipoCuenta.EditValue = obj.cod_tipo_cuenta;
            txtNroCtaContable.Text = obj.dsc_cta_contable;
            txtNroCuentaBancaria.Text = obj.dsc_cta_bancaria;
            txtNroCuentaInterbancaria.Text = obj.dsc_cta_interbancaria;
            chkFlgPagoProveedor.CheckState = obj.flg_pago_proveedor == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkFlgPagoHaberes.CheckState = obj.flg_pago_haberes == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkFlgPorDefecto.CheckState = obj.flg_defecto == "SI" ? CheckState.Checked : CheckState.Unchecked;
            txtUsuarioRegistro.Text = obj.dsc_usuario_registro;
            dtFechaRegistro.EditValue = obj.fch_registro;
            txtUsuarioCambio.Text = obj.dsc_usuario_cambio;
            dtFechaCambio.EditValue = obj.fch_cambio;
            lkpEmpresa.Enabled = false;
            glkpBanco.Enabled = false;
            lkpMoneda.Enabled = false;
            lkpTipoCuenta.Enabled = false;
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            //unit.Trabajador.CargaCombosLookUp("SedesEmpresa", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, lkpEmpresa.EditValue.ToString());
            //lkpSedeEmpresa.EditValue = null; 
            //List<eTrabajador.eInfoLaboral_Trabajador> lista = unit.Trabajador.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, lkpEmpresa.EditValue.ToString());
            //if (lista.Count == 1) lkpSedeEmpresa.EditValue = lista[0].cod_sede_empresa;
        }

        private void frmCtasBancariasEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && ActualizarListado) this.Close();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (lkpEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar una empresa", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error); lkpEmpresa.Focus(); return; }
                if (glkpBanco.EditValue == null) { MessageBox.Show("Debe seleccionar un banco", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error); glkpBanco.Focus(); return; }
                if (txtNroCuentaBancaria.Text == "") { MessageBox.Show("Debe ingresar una cuenta bancaria", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error); txtNroCuentaBancaria.Focus(); return; }
                eEmpresa.eBanco_Empresa obj = new eEmpresa.eBanco_Empresa();
                obj = AsignarValores();
                obj = unit.Factura.Insertar_Actualizar_BancoEmpresa<eEmpresa.eBanco_Empresa>(obj);
                if (obj == null) { MessageBox.Show("Error al guardar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                txtCodigo.Text = obj.num_linea.ToString();
                ActualizarListado = true;
                MessageBox.Show("Se registraron los datos de manera satisfactoria.", "", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eEmpresa.eBanco_Empresa AsignarValores()
        {
            eEmpresa.eBanco_Empresa obj = new eEmpresa.eBanco_Empresa();
            obj.num_linea = Convert.ToInt32(txtCodigo.Text);
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            //obj.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
            obj.cod_banco = glkpBanco.EditValue.ToString();
            obj.cod_moneda = lkpMoneda.EditValue.ToString();
            obj.cod_tipo_cuenta = lkpTipoCuenta.EditValue.ToString();
            obj.dsc_cta_contable = txtNroCtaContable.Text.Trim();
            obj.flg_pago_proveedor = chkFlgPagoProveedor.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_pago_haberes = chkFlgPagoHaberes.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_defecto = chkFlgPorDefecto.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_cta_bancaria = txtNroCuentaBancaria.Text;
            obj.dsc_cta_interbancaria = txtNroCuentaInterbancaria.Text;
            obj.flg_activo = "SI";
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                txtCodigo.EditValue = 0;
                lkpSedeEmpresa.EditValue = null;
                glkpBanco.EditValue = null;
                lkpMoneda.EditValue = "SOL";
                lkpTipoCuenta.EditValue = "01";
                txtNroCtaContable.Text = "";
                chkFlgPagoProveedor.CheckState = CheckState.Unchecked;
                chkFlgPagoHaberes.CheckState = CheckState.Unchecked;
                txtNroCuentaBancaria.Text = "";
                txtNroCuentaInterbancaria.Text = "";
                ActualizarListado = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}