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
using UI_BackOffice.Formularios.Shared;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    internal enum Apertura
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmAperturaCajaChica : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        internal Apertura MiAccion = Apertura.Nuevo;
        public string cod_caja = "", cod_empresa = "", cod_sede_empresa = "";
        public string ActualizarListado = "NO";
        public eCajaChica eCaja = new eCajaChica();


        public frmAperturaCajaChica()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmAperturaCajaChica_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();
                lkpEmpresa.EditValue = cod_empresa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarLookUpEdit()
        {
            try
            {
                unit.Factura.CargaCombosLookUp("EmpresaProveedor", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true);
                unit.CajaChica.CargaCombosLookUp("TipoCaja", lkpTipoCaja, "cod_tipo_caja", "dsc_tipo_caja", "", valorDefecto: true);
                unit.Proveedores.CargaCombosLookUp("Moneda", lkpTipoMoneda, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                unit.CajaChica.CargaCombosLookUp("ModoReposicion", lkpModoReposicion, "cod_modalidad", "dsc_modalidad", "", valorDefecto: true);
                dtFecCreacion.EditValue = DateTime.Today;
                List<eProveedor_Empresas> listEmpresasUsuario = unit.Proveedores.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", Program.Sesion.Usuario.cod_usuario);
                lkpEmpresa.EditValue = listEmpresasUsuario[0].cod_empresa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtResponsable_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                unit.Globales.pKeyDown(txtResponsable, e);
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) txtResponsable.Tag = null; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Responsable");
            }
            string dato = unit.Globales.pKeyPress(txtResponsable, e);
            if (dato != "")
            {
                Busqueda(dato, "Responsable");
            }
        }

        private void frmAperturaCajaChica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion == Apertura.Editar) this.Close();
        }

        private void txtMontoTotal_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtMontoTotal.EditValue) == 0)
            {
                decimal importe = 0;
                importe = Convert.ToDecimal(txtMontoTotal.EditValue) * (decimal)0.8;
                txtImporteAlertar.EditValue = Convert.ToDecimal(txtMontoTotal.EditValue) - importe;
            }
        }

        private void picResponsable_Click(object sender, EventArgs e)
        {
            Busqueda("", "Responsable");
        }

        public void Busqueda(string dato, string tipo)
        {
            if (lkpEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar una empresa", "", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;
            
            switch (tipo)
            {
                case "Responsable":
                    frm.entidad = frmBusquedas.MiEntidad.Trabajador;
                    frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Responsable":
                    
                    eTrabajador.eInfoLaboral_Trabajador obj = new eTrabajador.eInfoLaboral_Trabajador();
                    obj = unit.Trabajador.Obtener_Trabajador<eTrabajador.eInfoLaboral_Trabajador>(5, frm.codigo, lkpEmpresa.EditValue.ToString());
                    if (obj == null) { MessageBox.Show("Debe vincular la sede de la empresa al trabajador", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    txtResponsable.Tag = frm.codigo;
                    txtResponsable.Text = frm.descripcion;
                    txtUbicacion.Text = obj.dsc_empresa + " - " + obj.dsc_sede_empresa;
                    txtUbicacion.Tag = obj.cod_sede_empresa;
                    cod_empresa = obj.cod_empresa; cod_sede_empresa = obj.cod_sede_empresa;
                    break;
            }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                txtCodCajaChica.Text = "";
                cod_caja = "";
                lkpEmpresa.EditValue = null;
                dtFecCreacion.EditValue = DateTime.Today;
                lkpTipoCaja.EditValue = null;
                txtResponsable.Tag = "";
                txtResponsable.Text = "";
                txtUbicacion.Text = "";
                txtMontoTotal.EditValue = 0;
                txtImporteAlertar.EditValue = 0;
                lkpTipoMoneda.EditValue = null;
                lkpModoReposicion.EditValue = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                txtCodCajaChica.Select();
                if (dtFecCreacion.EditValue == null) { MessageBox.Show("Debe seleccionar una fecha", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecCreacion.Focus(); return; }
                if (lkpTipoCaja.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de caja.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoCaja.Focus(); return; }
                if (txtResponsable.Text.Trim() == "") { MessageBox.Show("Debe seleccionar un responsable.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtResponsable.Focus(); return; }
                if (txtUbicacion.Text.Trim() == "") { MessageBox.Show("Asignar la sede de la empresa al empleado en el modulo del trabajor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtUbicacion.Focus(); return; }
                if (lkpTipoMoneda.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de moneda.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoMoneda.Focus(); return; }
                if (Convert.ToDecimal(txtMontoTotal.EditValue) == 0) { MessageBox.Show("El importe debe ser mayor a 0.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtMontoTotal.Focus(); return; }
                if (Convert.ToDecimal(txtImporteAlertar.EditValue) == 0) { MessageBox.Show("El balance inicial debe ser mayor a 0.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtImporteAlertar.Focus(); return; }
                if (lkpModoReposicion.EditValue == null) { MessageBox.Show("Debe seleccionar un modo de reposición.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpModoReposicion.Focus(); return; }

                eCaja = AsignarValores();
                eCaja = unit.CajaChica.InsertarActualizar_AperturaCajaChica<eCajaChica>(eCaja);
                if (eCaja == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                cod_caja = eCaja.cod_caja; txtCodCajaChica.Text = eCaja.cod_caja;

                if (eCaja != null) { MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information); ActualizarListado = "SI"; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eCajaChica AsignarValores()
        {
            eCajaChica obj = new eCajaChica();
            obj.cod_caja = txtCodCajaChica.Text;
            obj.fch_creacion = Convert.ToDateTime(dtFecCreacion.EditValue);
            obj.cod_tipo_caja = lkpTipoCaja.EditValue.ToString();
            obj.cod_responsable = txtResponsable.Tag.ToString();
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = txtUbicacion.Tag.ToString();
            obj.cod_moneda = lkpTipoMoneda.EditValue.ToString();
            obj.imp_monto = Convert.ToDecimal(txtMontoTotal.EditValue);
            obj.imp_alertar = Convert.ToDecimal(txtImporteAlertar.EditValue);
            obj.cod_modalidad = lkpModoReposicion.Text;
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            obj.flg_estado_aprobado = "PEN";

            return obj;
        }

    }
}