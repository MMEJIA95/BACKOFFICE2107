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
using UI_BackOffice.Formularios.Shared;
using BE_BackOffice;
using DevExpress.XtraGrid.Views.Grid;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    internal enum Trabajadores
    {
        Editar = 1,
        Vista = 2
    }
    public partial class frmRegistroTrabajadoresAprobaciones : HNG_Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        List<EAprobaciones.eTrabajador> ListTrabajador = new List<EAprobaciones.eTrabajador>();
        internal Trabajadores MiAccion = Trabajadores.Editar;
        public string empresa = "";
        public int cunt = 0;

        public frmRegistroTrabajadoresAprobaciones()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurar_formulario();
            Inicializar();
        }
        public void Inicializar()
        {
            switch (MiAccion)
            {

                case Trabajadores.Editar:
                    CargarLookUpEdit();
                    gvTrabajadores.OptionsBehavior.Editable = true;
                    break;
                case Trabajadores.Vista:
                    CargarLookUpEdit();
                    Editar();
                    break;
            }

        }

        private void Editar()
        {
            ObtenerListadoTrabajodresAprobaciones();
        }
        private void CargarLookUpEdit()
        {
            try
            {
                unit.Aprobaciones.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
                List<EAprobaciones> list = unit.Aprobaciones.ListarEmpresas<EAprobaciones>(4, Program.Sesion.Usuario.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
                unit.Aprobaciones.CargaCombosLookUp("Modulos", lkpModulo, "cod_modulo", "dsc_modulo", "", valorDefecto: true);
                lkpModulo.ItemIndex = 0;
                //unit.Aprobaciones.CargaCombosLookUp("Validacion", lkpvalidacion, "cod_aprobacion", "descripcion", "", valorDefecto: true, cod_modulo: lkpModulo.EditValue.ToString());
                //lkpvalidacion.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }
        private void configurar_formulario()
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            unit.Globales.ConfigurarGridView_ClasicStyle(gcTrabajadores, gvTrabajadores);

        }

        private void btnbusquedaTrabajador_Click(object sender, EventArgs e)
        {
            //  if (txtnombre.Text == "") { HNG.MessageError("Debe seleccionar un Trabajador", "Trabajadores"); return; }
            Busqueda("", "AprobacionesTrabajador");
        }
        public void Busqueda(string dato, string tipo)
        {
            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;
            switch (tipo)
            {
                case "AprobacionesTrabajador":
                    frm.entidad = frmBusquedas.MiEntidad.AprobacionesTrabajador;
                    frm.filtro = dato;
                    frm.cod_empresa = lkpEmpresa.EditValue.ToString();
                    frm.BotonAgregarVisible = 1;
                    break;
            }
            frm.ShowDialog();
            //if (frm.cod_ == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "AprobacionesTrabajador":
                    if (frm.LisAprobacion.Count > 0)
                    {
                        // lkpTrabajadores.Properties.DataSource = frm.LisAprobacion;
                        // lkpTrabajadores.ItemIndex = 0;
                    }

                    break;
            }
        }

        private void chkImporte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImporte.Checked == true)
            {
                layoutimporte.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else if (chkImporte.Checked == false)
            {
                layoutimporte.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int maximo = Convert.ToInt32(sp_contador.Text);
            int conta = 0;
            string aprobacion = lkpvalidacion.EditValue.ToString();
            string empresa = lkpEmpresa.EditValue.ToString();
            EAprobaciones.eTrabajador eDirec = gvTrabajadores.GetFocusedRow() as EAprobaciones.eTrabajador;

            EAprobaciones obj1 = new EAprobaciones();
            obj1 = unit.Aprobaciones.Obtener_datos<EAprobaciones.Ecajachica>(30, cod_modulo: Convert.ToInt32(lkpModulo.EditValue.ToString()), cod_aprobacion: lkpvalidacion.EditValue.ToString());
            if (obj1.cant_maxima <= sp_contador.Value) {
                try
                {

                    EAprobaciones.eTrabajador eaprob = new EAprobaciones.eTrabajador();

                    eaprob.value = eDirec.seleccionado ? "SI" : "NO";
                   // eaprob.cod_aprobacion = Convert.ToString(aprobacion.ToString());
                    eaprob.cod_usuario = eDirec.cod_usuario;
                    eaprob.cod_empresa = empresa;
                    if (eaprob.value == "SI")
                    {
                        eaprob = unit.Aprobaciones.ActualizarPermisoUsuario<EAprobaciones.eTrabajador>(1, eaprob);
                        if (eaprob != null)
                        {
                            HNG.MessageSuccess("Se registro el permiso", "APROBACIONES POR USUARIO");
                            ObtenerListadoTrabajodresAprobaciones();
                        }

                        if (eaprob == null) { HNG.MessageError("Error al vincular Aprobación", "APROBACIONES POR USUARIO"); return; }
                    }
                    else
                    {
                        eaprob = unit.Aprobaciones.ActualizarPermisoUsuario<EAprobaciones.eTrabajador>(2, eaprob);
                        if (eaprob != null)
                        {
                            HNG.MessageSuccess("Se elimino el permiso asignado", "APROBACIONES POR USUARIO");
                            ObtenerListadoTrabajodresAprobaciones();
                        }

                        if (eaprob == null) { HNG.MessageError("Error al eliminar permiso", "APROBACIONES POR USUARIO"); return; }
                    }
                    gvTrabajadores.RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (obj1.cant_maxima >= sp_contador.Value)
            {
                HNG.MessageError("La aprobación para este modulo solo permite "+sp_contador.Text+" trabajadores como maximo", "APROBACIONES POR USUARIO");
                eDirec.seleccionado = false;
                return;
                
            }
        }
    
        private void lkpModulo_EditValueChanged(object sender, EventArgs e)
        {
            List<EAprobaciones.eTrabajador> listarTrabajadorModulo = new List<EAprobaciones.eTrabajador>();
            if (lkpModulo.EditValue != null)
            {
                unit.Aprobaciones.CargaCombosLookUp("Validacion", lkpvalidacion, "cod_aprobacion", "descripcion", "", valorDefecto: true, cod_modulo: lkpModulo.EditValue.ToString());
                ObtenerListadoTrabajodresAprobaciones();
            }
        }

        private void gvTrabajadores_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

            private void rchSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            gvTrabajadores.PostEditor();
        }

        private void ObtenerListadoTrabajodresAprobaciones()
        {
            EAprobaciones obj1 = new EAprobaciones();
            EAprobaciones.eTrabajador eDirec = gvTrabajadores.GetFocusedRow() as EAprobaciones.eTrabajador;
            ListTrabajador = unit.Aprobaciones.ListarTrabajadores<EAprobaciones.eTrabajador>(12,lkpEmpresa.EditValue.ToString(),"");
            bsTrabajadores.DataSource = ListTrabajador;

            if (MiAccion == Trabajadores.Editar)
            {
                List<EAprobaciones.eTrabajador> lista = unit.Aprobaciones.ListarTrabajadores<EAprobaciones.eTrabajador>(10, cod_empresa:lkpEmpresa.EditValue.ToString(), cod_modulo:lkpModulo.EditValue.ToString());
                foreach (EAprobaciones.eTrabajador obj in lista)
                {
                    EAprobaciones oCliEmp = ListTrabajador.Find(x => x.cod_usuario == obj.cod_usuario);
                    if (oCliEmp != null) { oCliEmp.seleccionado = true; }            
                }
                
                

            }

            gvTrabajadores.RefreshData();
        }

        private void gvTrabajadores_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (e.FocusedRowHandle >= 0)

                obtenerdatos();
          
        }

        private void gvTrabajadores_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
            if (e.RowHandle >= 0)
            {
                GridView vw = sender as GridView;
                bool estado = Convert.ToBoolean(vw.GetRowCellValue(e.RowHandle, vw.Columns["seleccionado"]));
                if (estado) e.Appearance.ForeColor = Color.Blue;
            }
        }

        //public void mostrardatos()
        //{
        //    EAprobaciones.eTrabajador obj = gvTrabajadores.GetFocusedRow() as EAprobaciones.eTrabajador;
        //    obj = gvTrabajadores.GetFocusedRow() as EAprobaciones.eTrabajador;
        //    if (obj == null) return;
        //    if (obj.seleccionado == false) { lkpvalidacion.EditValue = null; } else { lkpvalidacion.EditValue = obj.cod_aprobacion; }
        //    if (obj.cod_modulo == 0) { lkpModulo.EditValue = null; } else { lkpModulo.EditValue = obj.cod_modulo; }
        //    lkpEmpresa.EditValue = obj.cod_empresa;
        //    if (obj.cod_aprobacion == null) { lkpvalidacion.EditValue = null; } else { lkpvalidacion.EditValue = obj.cod_aprobacion; }
        //    lkpvalidacion.EditValue = obj.cod_aprobacion;

        //}

        private void gvTrabajadores_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0) gvTrabajadores_FocusedRowChanged(gvTrabajadores, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
          // mostrardatos();
        }

        private void obtenerdatos()
        {
            EAprobaciones.eTrabajador eDirec = gvTrabajadores.GetFocusedRow() as EAprobaciones.eTrabajador;
            EAprobaciones obj1 = new EAprobaciones();
            obj1 = unit.Aprobaciones.Obtener_datos<EAprobaciones.Ecajachica>(29, eDirec.cod_usuario,cod_modulo:Convert.ToInt32(lkpModulo.EditValue.ToString()));
            if (obj1 == null) { lkpvalidacion.EditValue = null; } else
            {
                lkpvalidacion.EditValue = obj1.cod_aprobacion;
            }
        }

        private void lkpEmpresa_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void lkpvalidacion_EditValueChanged_1(object sender, EventArgs e)
        {
            EAprobaciones obj1 = new EAprobaciones();
            obj1 = unit.Aprobaciones.Obtener_datos<EAprobaciones>(36, cod_modulo: Convert.ToInt32(lkpModulo.EditValue.ToString()),cod_aprobacion:lkpvalidacion.EditValue.ToString() == null ? "" : lkpvalidacion.EditValue.ToString());
            sp_contador.Text = Convert.ToString(obj1.cant_trabajadores);
        }
    }
   
}

