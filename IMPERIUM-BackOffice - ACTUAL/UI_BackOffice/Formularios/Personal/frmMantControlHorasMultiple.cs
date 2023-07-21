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
using DevExpress.XtraEditors.Repository;

namespace UI_BackOffice.Formularios.Personal
{
    public partial class frmMantControlHorasMultiple : DevExpress.XtraEditors.XtraForm
    {
        internal enum ControlHorasMulptiple
        {
            Nuevo = 0,
            Editar = 1,
            Vista = 2
        }

        private readonly UnitOfWork unit;
        List<eControlHoras> listActividadesGestionada = new List<eControlHoras>();
        public eControlHoras eControlHoras = new eControlHoras();
        internal ControlHorasMulptiple MiAccion = ControlHorasMulptiple.Nuevo;
        public eControlHoras.eCostoHora eCostoHora = new eControlHoras.eCostoHora();
        int perfilAdministrador = 43;  //PRODUCCION
        //int perfilAdministrador = 38;  //DESARROLLO
        decimal costo;
        public int cod_perfil;

        public frmMantControlHorasMultiple()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmMantControlHorasMultiple_Load(object sender, EventArgs e)
        {
            cargarListadoActividades();
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
                    case ControlHorasMulptiple.Nuevo:
                        Nuevo();
                        break;
                    case ControlHorasMulptiple.Editar:
                        //Editar();
                        lkpEmpresa.Enabled = false;
                        break;
                    case ControlHorasMulptiple.Vista:
                        //Vista();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Nuevo()
        {
            lkpEmpresa.Enabled = true;
            txtUsuario.Text = Program.Sesion.Usuario.cod_usuario;
            dtFechaEjecucion.EditValue = DateTime.Today;
            txtUsuarioRegistro.Text = Program.Sesion.Usuario.cod_usuario;
            txtUsuarioCambio.Text = Program.Sesion.Usuario.cod_usuario;
            dtFechaRegistro.EditValue = DateTime.Today;
            dtFechaCambio.EditValue = DateTime.Today;
            obtenerCostoxUsuario();
            //tmDuracion.
        }

        public void cargarListadoActividades()
        {
            listActividadesGestionada = unit.Trabajador.ListarControlHoras<eControlHoras>(3);
            bsListadoActividades.DataSource = listActividadesGestionada;
            gvListadoActividades.RefreshData();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            foreach (int nRow in gvListadoActividades.GetSelectedRows())
            {
                eControlHoras obj = gvListadoActividades.GetRow(nRow) as eControlHoras;
                obj.dsc_duracion = "00:00";
                bsListadoMultiple.Add(obj);
            }
            gvListadoActividades.ClearSelection();
            cargarListadoActividades();
        }
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvListadoMultiple.GetSelectedRows())
            {
                eControlHoras obj = gvListadoMultiple.GetRow(nRow - valor) as eControlHoras;
                bsListadoMultiple.Remove(obj);
                valor++;
            }
        }

        public void cargarCombo()
        {

            lkpEmpresa.DataSource = unit.Requerimiento.CombosEnGridControl<eControlHoras>("EmpresasUsuarios", Program.Sesion.Usuario.cod_usuario);
            
        }

        private void gvListadoMultiple_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvListadoMultiple.RefreshData();
            eControlHoras obj = gvListadoMultiple.GetFocusedRow() as eControlHoras;
            if (obj == null) return;
            if (e.Column.FieldName == "dsc_duracion")
            {
                DateTime horas = Convert.ToDateTime(e.Value);
                obj.dsc_duracion = horas.Hour.ToString("00") + ":"+ horas.Minute.ToString("00");
            }
        }

        private void btnGrabar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool validador = true;
            try
            {
                gvListadoMultiple.PostEditor(); gvListadoMultiple.RefreshData();
                if (gvListadoMultiple.RowCount > 0)
                {
                    for (int x = gvListadoMultiple.RowCount - 1; x >= 0; x--)
                    {
                        validador = filtro(x);
                        if (!validador) { MessageBox.Show("Favor de revisar que todos los campos estén correctos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); break; }
                    }

                    if (validador)
                    {
                        for (int x = gvListadoMultiple.RowCount - 1; x >= 0; x--)
                        {
                            eControlHoras = AsignarValores_ControlHoras(x);
                            eControlHoras = unit.Trabajador.InsertarActualizar_ControlHoras<eControlHoras>(eControlHoras);
                        }
                        MessageBox.Show("Todos los registros se han guardado satisfactoriamente", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else {
                    MessageBox.Show("No hay información para guardar en el sistema", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        bool filtro(int x)
        {
            eControlHoras objCaptura = gvListadoMultiple.GetRow(x) as eControlHoras;
            if (objCaptura.cod_empresa == null) { /*MessageBox.Show("Debe seleccionar una empresa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);*/ return false; }
            if (objCaptura.dsc_duracion == null || objCaptura.dsc_duracion == "00:00") { /*MessageBox.Show("Debe ingresar el tiempo de duración", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);*/ return false; }
            return true;
        }

        public eControlHoras AsignarValores_ControlHoras(int x)
        {
            txtUsuarioCambio.Text = Program.Sesion.Usuario.cod_usuario;
            dtFechaCambio.EditValue = DateTime.Today;
            eControlHoras obj = new eControlHoras();

            obj.cod_control_horas = "";
            obj.cod_usuario = txtUsuario.Text;
            obj.cod_usuario_registro = txtUsuario.Text;
            obj.fch_ejecucion = Convert.ToDateTime(dtFechaEjecucion.EditValue);
            eControlHoras objCaptura = gvListadoMultiple.GetRow(x) as eControlHoras;
            obj.dsc_comentario = objCaptura.dsc_comentario;
            obj.cod_segmento = objCaptura.cod_segmento;
            obj.cod_grupo = objCaptura.cod_grupo;
            obj.cod_actividad = objCaptura.cod_actividad;
            obj.dsc_duracion = objCaptura.dsc_duracion; //objCaptura.dsc_duracion.ToString().Length == 5 ? objCaptura.dsc_duracion.ToString() : (objCaptura.dsc_duracion.ToString().Split(' ')[1]).Substring(0, 5);
            obj.imp_costo = costo; //costo * Convert.ToDecimal((obj.dsc_duracion.ToString().Split(':')[0]).Substring(0, 2)) + (costo * Convert.ToDecimal((obj.dsc_duracion.ToString().Split(':')[1]).Substring(0, 2))/60);
            obj.cod_empresa_usuaria = objCaptura.cod_empresa.ToString();
            obj.cod_segmento = objCaptura.cod_segmento.ToString();
            obj.cod_grupo = objCaptura.cod_grupo.ToString();
            obj.cod_actividad = objCaptura.cod_actividad.ToString();
            bsListadoMultiple.Remove(objCaptura);
            return obj;
        }

        public void obtenerCostoxUsuario()
        {
            eCostoHora = unit.Trabajador.Obtener_costo_usuario<eControlHoras.eCostoHora>(Program.Sesion.Usuario.cod_usuario);

            if (cod_perfil != perfilAdministrador)
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

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bsListadoMultiple.Clear();
        }

        private void gvListadoActividades_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoActividades_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoMultiple_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoMultiple_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void btnDuplicar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            eControlHoras obj = gvListadoMultiple.GetRow(gvListadoMultiple.FocusedRowHandle) as eControlHoras;
            bsListadoMultiple.Add(obj);
        }
    }
}