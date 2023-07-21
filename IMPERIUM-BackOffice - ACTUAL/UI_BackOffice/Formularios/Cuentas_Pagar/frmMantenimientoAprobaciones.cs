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
    internal enum Aprobacion
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantenimientoAprobaciones : HNG_Tools.ModalForm
    {
       
        List<EAprobaciones.eTrabajador> ListaAprobaciones = new List<EAprobaciones.eTrabajador>();

        private readonly UnitOfWork unit;
        internal Aprobacion MiAccion = Aprobacion.Editar;
        public frmMantenimientoAprobaciones()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurar_formulario();
            Inicializar();
            ObtenerListadoAprobaciones();
        }
        private void CargarLookUpEdit()
        {
            try
            {
                unit.Aprobaciones.CargaCombosLookUp("Modulos", lkpModulo, "cod_modulo", "dsc_modulo", "", valorDefecto: true);
                lkpModulo.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }
        public void Inicializar()
        {
            switch (MiAccion)
            {
                case Aprobacion.Nuevo:
                    CargarLookUpEdit();
                    Nuevo();
                    break;

                case Aprobacion.Editar:
                    CargarLookUpEdit();
                    Editar();
                    ObtenerListadoAprobaciones();
                    break;
                case Aprobacion.Vista:
                    CargarLookUpEdit();
                    break;
            }

        }
        private void Nuevo()
        {
            EAprobaciones eDirec = gvAprobaciones.GetFocusedRow() as EAprobaciones;
            EAprobaciones obj1 = new EAprobaciones();
            obj1 = unit.Aprobaciones.Obtener_datos<EAprobaciones.Ecajachica>(28,"","",Convert.ToInt32(lkpModulo.EditValue.ToString()));

            if (eDirec != null) {
                lkpModulo.ItemIndex = eDirec.cod_modulo;
            }
            else
            {
                lkpModulo.ItemIndex = 0;
            }
            spTrabajadores.Value = 0;
            txtDescripcion.Text = "";
            txt_maximo.Text = "S/0.00";
            txt_minimo.Text = Convert.ToString(obj1.imp_minimo);
        }
        private void Editar()
        {

        }

        private void configurar_formulario()
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            unit.Globales.ConfigurarGridView_ClasicStyle(gcAprobaciones, gvAprobaciones);

        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void ObtenerListadoAprobaciones()
        {

            EAprobaciones.eTrabajador eDirec = gvAprobaciones.GetFocusedRow() as EAprobaciones.eTrabajador;
            ListaAprobaciones = unit.Aprobaciones.ListarTrabajadores<EAprobaciones.eTrabajador>(13, "", "");
            bsAprobaciones.DataSource = ListaAprobaciones;
           
        }

        private void gvAprobaciones_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    obtenerdatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void obtenerdatos()
        {
            EAprobaciones eDirec = gvAprobaciones.GetFocusedRow() as EAprobaciones;
            lkpModulo.EditValue = eDirec.cod_modulo;
            txtDescripcion.Text = eDirec.descripcion;
            spTrabajadores.Value = eDirec.cant_trabajadores;
            txt_maximo.Text = Convert.ToString(eDirec.imp_maximo);
            txt_minimo.Text= Convert.ToString(eDirec.imp_minimo);
            if (eDirec.flg_activo == "SI") { tsActivo.IsOn = true; } else { tsActivo.IsOn = false; }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lkpModulo.EditValue == null) { MessageBox.Show("Debe seleccionar un modulo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpModulo.Focus(); return; }
                if (btnGuardar.Text == "GUARDAR")
                {
                    EAprobaciones eTrab = AsignarDatos();
                    eTrab = unit.Aprobaciones.InsertarActualizarAprobaciones<EAprobaciones>(eTrab);

                    if (eTrab == null) { HNG.MessageError("Error al guardar los datos.", "ERROR"); return; }
                    if (eTrab != null)
                    {
                        MiAccion = Aprobacion.Editar;
                        HNG.MessageSuccess("Se registraron los datos de manera satisfactoria.", "INFORMACION");
                        ObtenerListadoAprobaciones();
                        gvAprobaciones.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           // Actualizarcombo = "SI";
        }

        private EAprobaciones AsignarDatos()
        {
            EAprobaciones eUsua = new EAprobaciones();

            eUsua.cod_modulo = Convert.ToInt32(lkpModulo.EditValue.ToString());
            eUsua.imp_maximo = Convert.ToDecimal(txt_maximo.EditValue);
            eUsua.imp_minimo = Convert.ToDecimal(txt_minimo.Text);
            eUsua.descripcion = txtDescripcion.Text;
            eUsua.cant_trabajadores = Convert.ToInt32(spTrabajadores.Text);
            eUsua.flg_activo = "SI";
            //return eUsu;

            return eUsua;
        }

        private void txt_importe_EditValueChanged(object sender, EventArgs e)
        {
            txtDescripcion.Text = "Aprobación desde "+txt_minimo.Text+" hasta " + txt_maximo.Text;
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            //string result = "";
            //EAprobaciones eDirec = gvAprobaciones.GetFocusedRow() as EAprobaciones;
            //result = unit.Aprobaciones.EliminarMantenimiento(eDirec.cod_modulo,eDirec.cod_aprobacion);
            //if (result != null) { HNG.MessageSuccess("Se eliminaron los datos de manera satisfactoria.", "INFORMACION");
            //    ObtenerListadoAprobaciones();
            //    gvAprobaciones.RefreshData();
            //}
            //else { HNG.MessageError("Error al eliminar datos", "INFORMACION"); }
        }

        private void txt_minimo_EditValueChanged(object sender, EventArgs e)
        {
            txtDescripcion.Text = "Aprobación desde " + txt_minimo.Text + " hasta " + txt_maximo.Text;
        }
    }
}