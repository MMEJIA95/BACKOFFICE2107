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
using DevExpress.XtraLayout.Utils;
namespace UI_BackOffice.Formularios.Personal
{
    internal enum ControlHoras
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantControlHoras : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmListaControlHoras frmHandler;
        public eControlHoras ch = new eControlHoras();
        internal ControlHoras MiAccion = ControlHoras.Nuevo;
        //blControlHoras blControlH = new blControlHoras();
        public eControlHoras.eCostoHora eCostoHora = new eControlHoras.eCostoHora();
        decimal costo;
        public int cod_perfil;
        int perfilAdministrador = 43; int perfilVisualizador = 42; int perfilRegistrador = 41; //PRODUCCION
        //int perfilAdministrador = 38; int perfilVisualizador = 37; int perfilRegistrador = 36; //DESARROLLO
        public string cod_control_horas;
        public string cod_usuario_cambio = "";
        public frmMantControlHoras()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        public frmMantControlHoras(frmListaControlHoras frm)
        {
            InitializeComponent();
            frmHandler = frm;
            groupControl1.Visible = false;
            unit = new UnitOfWork();
        }

        private void frmMantControlHoras_Load(object sender, EventArgs e)
        {
            obtenerCostoxUsuario();
            cargarCombo();
            Inicializar();
            txtUsuario.ReadOnly = true;
            txtUsuarioRegistro.ReadOnly = true;
            txtUsuarioCambio.ReadOnly = true;
            dtFechaRegistro.ReadOnly = true;
            dtFechaCambio.ReadOnly = true;
            
            
        }

        private void Inicializar()
        {
            try
            {
                switch (MiAccion)
                {
                    case ControlHoras.Nuevo:
                        Nuevo();
                        break;
                    case ControlHoras.Editar:
                        Editar();
                        //lkpEmpresa.Enabled = false;
                        //dtFechaEjecucion.Enabled = false;
                        break;
                    case ControlHoras.Vista:
                        Vista();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cargarCombo()
        {
            unit.Trabajador.CargaCombosLookUp("Segmento", lkpSegmento, "cod_segmento", "dsc_segmento", "", valorDefecto: true);

            unit.Requerimiento.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
            lkpEmpresa.ItemIndex = 0;
        }

        private void glkpSegmento_EditValueChanged(object sender, EventArgs e)
        {
            unit.Trabajador.CargaCombosLookUp("Grupo", lkpGrupo, "cod_grupo", "dsc_grupo", "", valorDefecto: true,"","","","", lkpSegmento.EditValue == null ? null : lkpSegmento.EditValue.ToString());
        }

        private void glkpGrupo_EditValueChanged(object sender, EventArgs e)
        {
            unit.Trabajador.CargaCombosLookUp("Actividad", lkpActividad, "cod_actividad", "dsc_actividad", "", valorDefecto: true, "", "", "", "", lkpSegmento.EditValue == null ? null : lkpSegmento.EditValue.ToString(), lkpGrupo.EditValue == null ? null : lkpGrupo.EditValue.ToString());
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MiAccion = ControlHoras.Nuevo;
            Inicializar();
            /*btnNuevo.Enabled = false;
            cod_control_horas = "";
            txtUsuario.Text = "";
            mmComentario.Text = "";
            dtFechaEjecucion.EditValue = DateTime.Now;
            glkpActividad.EditValue = null;
            glkpGrupo.EditValue = null;
            glkpSegmento.EditValue = null;
            tmDuracion.EditValue = "00:00";
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.cod_usuario;
            txtUsuarioCambio.Text = Program.Sesion.Usuario.cod_usuario;
            dtFechaRegistro.EditValue = DateTime.Today;
            dtFechaCambio.EditValue = DateTime.Today;*/
            //MessageBox.Show((timeDuracion.EditValue.ToString().Split(' ')[1]).Substring(0,5));
            //MessageBox.Show(txtUsuario.Text + "\n" + (tmDuracion.EditValue.ToString().Split(' ')[1]).Substring(0, 5) + "\n" + dtFechaEjecucion.EditValue + "\n" + glkpEmpresa.EditValue + "\n" + mmComentario.Text);
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txtUsuario.Text.Trim() == "") { MessageBox.Show("No hay usuario seleccionado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtUsuario.Focus(); return; }
                if (tmDuracion.Text.Trim() == "00:00" || tmDuracion.Text.Trim() == "") { MessageBox.Show("Debe ingresar el tiempo de duración", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); tmDuracion.Focus(); return; }
                if (dtFechaEjecucion.EditValue == null) { MessageBox.Show("Debe ingresar la fecha de ejecución", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFechaEjecucion.Focus(); return; }
                if (lkpEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar una empresa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpEmpresa.Focus(); return; }
                if (lkpSegmento.EditValue == null) { MessageBox.Show("Debe seleccionar un segmento", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSegmento.Focus(); return; }
                if (lkpGrupo.EditValue == null) { MessageBox.Show("Debe seleccionar un grupo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpGrupo.Focus(); return; }
                if (lkpActividad.EditValue == null) { MessageBox.Show("Debe seleccionar una actividad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpActividad.Focus(); return; }

                ch = AsignarValores_ControlHoras();
                ch = unit.Trabajador.InsertarActualizar_ControlHoras<eControlHoras>(ch);

                cod_control_horas = ch.cod_control_horas;
                btnNuevo.Enabled = true;
                //lkpEmpresa.Enabled = false;
                //dtFechaEjecucion.Enabled = false;

                MessageBox.Show("Se ha guardado satisfactoriamente", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public eControlHoras AsignarValores_ControlHoras()
        {
            txtUsuarioCambio.Text = Program.Sesion.Usuario.cod_usuario;
            dtFechaCambio.EditValue = DateTime.Today;
            eControlHoras obj = new eControlHoras();

            obj.cod_control_horas = cod_control_horas;
            obj.cod_usuario = Program.Sesion.Usuario.cod_usuario;
            obj.cod_usuario_registro = cod_usuario_cambio == "" ? Program.Sesion.Usuario.cod_usuario : cod_usuario_cambio;
            obj.dsc_comentario = mmComentario.Text;
            //obj.dsc_duracion = tmDuracion.EditValue.ToString().Length == 5 ? tmDuracion.EditValue.ToString() : (tmDuracion.EditValue.ToString().Split(' ')[1]).Substring(0, 5);
            DateTime horas = Convert.ToDateTime(tmDuracion.EditValue);
            obj.dsc_duracion = horas.Hour.ToString("00") + ":" + horas.Minute.ToString("00");
            obj.imp_costo = costo; // costo * Convert.ToDecimal((obj.dsc_duracion.ToString().Split(':')[0]).Substring(0, 2)) + (costo * Convert.ToDecimal((obj.dsc_duracion.ToString().Split(':')[1]).Substring(0, 2)) / 60);
            obj.fch_ejecucion = Convert.ToDateTime(dtFechaEjecucion.EditValue);
            obj.cod_empresa_usuaria = lkpEmpresa.EditValue.ToString();
            obj.cod_segmento = lkpSegmento.EditValue.ToString();
            obj.cod_grupo = lkpGrupo.EditValue.ToString();
            obj.cod_actividad = lkpActividad.EditValue.ToString();
            
            return obj;
        }

        private void Nuevo()
        {
            btnNuevo.Enabled = false;
            lkpEmpresa.Enabled = true;
            cod_control_horas = "";
            mmComentario.Text = "";
            cod_usuario_cambio = "";
            txtUsuario.Text = Program.Sesion.Usuario.dsc_usuario;
            dtFechaEjecucion.EditValue = DateTime.Today;
            lkpActividad.EditValue = null;
            lkpGrupo.EditValue = null;
            lkpSegmento.EditValue = null;
            tmDuracion.EditValue = "00:00";
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.cod_usuario;
            txtUsuarioCambio.Text = Program.Sesion.Usuario.cod_usuario;
            dtFechaRegistro.EditValue = DateTime.Today;
            dtFechaCambio.EditValue = DateTime.Today;
            lkpEmpresa.EditValue = null;
        }

        private void Editar()
        {
            cargarCombo();
            ObtenerDatos_ControlHoras();
        }

        private void Vista()
        {
            Editar();
            txtUsuario.Enabled = false;
            lkpEmpresa.Enabled = false;
            dtFechaEjecucion.Enabled = false;
            dtFechaRegistro.Enabled = false;
            dtFechaCambio.Enabled = false;
            tmDuracion.Enabled = false;
            lkpSegmento.Enabled = false;
            lkpGrupo.Enabled = false;
            lkpActividad.Enabled = false;
            mmComentario.Enabled = false;
            txtUsuarioRegistro.Enabled = false;
            txtUsuarioCambio.Enabled = false;
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = false;
        }

        private void ObtenerDatos_ControlHoras()
        {
            cod_control_horas = ch.cod_control_horas;
            txtUsuario.Text = ch.dsc_usuario;
            txtUsuarioRegistro.Text = ch.cod_usuario_registro;
            txtUsuarioCambio.Text = ch.cod_usuario_cambio;
            dtFechaEjecucion.EditValue = ch.fch_ejecucion;
            tmDuracion.EditValue = ch.dsc_duracion;
            dtFechaRegistro.EditValue = ch.fch_registro;
            dtFechaCambio.EditValue = ch.fch_cambio;
            mmComentario.Text = ch.dsc_comentario;
            lkpEmpresa.EditValue = ch.cod_empresa_usuaria;
            lkpSegmento.EditValue = ch.cod_segmento;
            lkpGrupo.EditValue = ch.cod_grupo;
            lkpActividad.EditValue = ch.cod_actividad;
            cod_usuario_cambio = Program.Sesion.Usuario.cod_usuario;
        }

        public void obtenerCostoxUsuario()
        {
            eCostoHora = unit.Trabajador.Obtener_costo_usuario<eControlHoras.eCostoHora>(Program.Sesion.Usuario.cod_usuario);

            if (cod_perfil != perfilAdministrador && cod_perfil != perfilVisualizador)
            {
                if (eCostoHora == null)
                {
                    MessageBox.Show("Favor de comunicarse con el administrador para que le agregue un costo al tiempo laborado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                else
                {
                    costo = eCostoHora == null ? 0 : eCostoHora.imp_costo;
                }
            }
            else
            {
                costo = eCostoHora == null ? 0 : eCostoHora.imp_costo;
            }
        }

        private void frmMantControlHoras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion == ControlHoras.Editar) this.Close();
        }
    }
}