using BE_BackOffice;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Layout.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UI_BackOffice.Formularios.Shared;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmMantenimientoAprobacionesCP : HNG_Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        List<eCeco> ListCECODisponible = new List<eCeco>();
        List<eCeco> ListCECOAsignado = new List<eCeco>();
        List<eTrabajador.eInfoLaboral_Trabajador> ListaInfolaboral = new List<eTrabajador.eInfoLaboral_Trabajador>();
        List<EAprobaciones.EcuentasPagar> ListadoAprobadores = new List<EAprobaciones.EcuentasPagar>();
        public string cod_empresa = "", infolab="";
        public frmMantenimientoAprobacionesCP()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurar_formulario();
            Inicializar();
        }

        public void Inicializar()
        {
            try
            {
                CargarLookUpEdit();

            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }
        private void CargarLookUpEdit()
        {
            try
            {
                unit.Aprobaciones.CargaCombosLookUp("EmpresasUsuarios", lkpempresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
                List<EAprobaciones> list = unit.Aprobaciones.ListarEmpresas<EAprobaciones>(2, Program.Sesion.Usuario.cod_usuario);
                if (list.Count >= 1) lkpempresa.EditValue = list[0].cod_empresa;

            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }

        public void CargarCECOAsignados(int cod_aprobador)
        {

            ListCECOAsignado = unit.Aprobaciones.ListarAprobadoresCP<eCeco>(49, cod_empresa,cod_aprobador);
            if(ListCECOAsignado.Count <= 0) { CargarCECODisponible(cod_empresa); }
            else
            {
                eCeco objEmp = new eCeco();
                foreach (eCeco obj in ListCECOAsignado)
                {
                    objEmp = ListCECOAsignado.Find(x => x.num_item == obj.num_item);
                    if (objEmp != null) ListCECODisponible.Remove(objEmp);
                }
            }
            bsCECOdisponible.DataSource = null; bsCECOdisponible.DataSource = ListCECODisponible;
            gvCECOdisponible.RefreshData();
            bsCECOasignado.DataSource = null; bsCECOasignado.DataSource = ListCECOAsignado;
            gvCECOasignado.RefreshData();
        }

        private void CargarCECODisponible(string cod_empresa)
        {
            ListCECODisponible = unit.Aprobaciones.ListarAprobadoresCP<eCeco>(50, cod_empresa);
            bsCECOdisponible.DataSource = null; bsCECOdisponible.DataSource = ListCECODisponible;
            gvCECOdisponible.RefreshData();
        }
        private void configurar_formulario()
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            unit.Globales.ConfigurarGridView_ClasicStyle(gcCECOasignado, gvCECOasignado);
            unit.Globales.ConfigurarGridView_ClasicStyle(gcCECOdisponible, gvCECOdisponible);

        }

        private void lkpempresa_EditValueChanged(object sender, EventArgs e)
        {
            cod_empresa = lkpempresa.EditValue.ToString();
            CargarCECODisponible(cod_empresa.ToString());
            ListadoAprobadores = unit.Aprobaciones.ListarAprobadoresCP<EAprobaciones.EcuentasPagar>(47,cod_empresa);
            bsAprobador.DataSource = null; bsAprobador.DataSource = ListadoAprobadores;
            gvAprobador.RefreshData();
        }

        private void obtenerlistadocxp()
        {
            ListadoAprobadores = unit.Aprobaciones.ListarAprobadoresCP<EAprobaciones.EcuentasPagar>(47, cod_empresa);
            bsAprobador.DataSource = null; bsAprobador.DataSource = ListadoAprobadores;
            gvAprobador.RefreshData();
        }


        private void btnasignarCECO_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvCECOdisponible.GetSelectedRows())
            {

                eCeco obj = gvCECOdisponible.GetRow(nRow - valor) as eCeco;
                bsCECOasignado.Add(obj);
                bsCECOdisponible.Remove(obj);
                valor = valor + 1;
            }
        }

        private void btnquitarCECO_Click(object sender, EventArgs e)
        {
            int valor = 0;
            foreach (int nRow in gvCECOasignado.GetSelectedRows())
            {

                eCeco obj = gvCECOasignado.GetRow(nRow - valor) as eCeco;
                bsCECOdisponible.Add(obj);
                bsCECOasignado.Remove(obj);
                valor = valor + 1;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Busqueda("", "AprobacionesTrabajadorcxp");
        }

        private void gvAprobador_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            gvAprobador.PostEditor(); gvAprobador.RefreshData();
            //try
            //{
            //    gvAprobador.PostEditor(); gvAprobador.RefreshData();
            //    EAprobaciones.EcuentasPagar obj = gvAprobador.GetFocusedRow() as EAprobaciones.EcuentasPagar;
            //   // if(gvCECOasignado != null) { HNG.MessageWarning("Es necesario Asignar un CECO al usuario", "ASIGNACIÓN DE CECO");return; }
            //    obj.cod_empresa = lkpempresa.EditValue.ToString();
            //    EAprobaciones.EcuentasPagar eObj = unit.Aprobaciones.Insertar_Actualizar_Aprobacioncp<EAprobaciones.EcuentasPagar>(obj);
            //    if (eObj == null) MessageBox.Show("Error al insertar costo", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    obtenerlistadocxp();
            //    gvAprobador.RefreshData();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void gvAprobador_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvAprobador_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvCECOdisponible_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvAprobador.PostEditor(); gvAprobador.RefreshData();
            //eProductos.eProductosTarifas obj = gvAprobador.GetFocusedRow() as eProductos.eProductosTarifas;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int cod_aprobador = 0;
            string dsc_ceco = "";
            string codigo_ceco = "";
            EAprobaciones.EcuentasPagar obj = gvAprobador.GetFocusedRow() as EAprobaciones.EcuentasPagar;
            if (gvCECOasignado.DataRowCount <= 0) { HNG.MessageWarning("Es necesario Asignar un CECO al usuario", "ASIGNACIÓN DE CECO"); return; }
            obj.cod_empresa = lkpempresa.EditValue.ToString();
            EAprobaciones.EcuentasPagar eObje = unit.Aprobaciones.Insertar_Actualizar_Aprobacioncp<EAprobaciones.EcuentasPagar>(obj);
            cod_aprobador = eObje.cod_aprobador;
            if (eObje == null) MessageBox.Show("Error al insertar Aprobador", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         
            for (int x = 0; x <= gvCECOasignado.RowCount - 1; x++)
            {
                eCeco obje = gvCECOasignado.GetRow(x) as eCeco;
                obj.cod_ceco = obje.num_item;
                obj.dsc_ceco = obje.dsc_CECO_abrev;
                obj.codigo_ceco = obje.cod_CECO;
                obj.cod_aprobador = cod_aprobador;
                EAprobaciones.EcuentasPagar eObj = unit.Aprobaciones.Insertar_Actualizar_Aprobacioncp_detalle<EAprobaciones.EcuentasPagar>(obj);
            }

            obtenerlistadocxp();
            gvAprobador.RefreshData();
        }

        private void gvAprobador_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var a = gvAprobador.GetSelectedRows();
            EAprobaciones.EcuentasPagar objFact = gvAprobador.GetFocusedRow() as EAprobaciones.EcuentasPagar;

        }

        private void gvAprobador_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int valor = 0;
            try
            {
                EAprobaciones.EcuentasPagar objFact = gvAprobador.GetFocusedRow() as EAprobaciones.EcuentasPagar;
                CargarCECOAsignados(objFact.cod_aprobador);
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void Busqueda(string dato, string tipo)
        {
            frmBusquedas frm = new frmBusquedas();
            //frm.user = user;
            frm.filtro = dato;
            //frm.colorVerde = colorVerde;
            //frm.colorPlomo = colorPlomo;
            //frm.colorEventRow = colorEventRow;
            //frm.colorFocus = colorFocus;
            switch (tipo)
            {
                case "AprobacionesTrabajadorcxp":
                    frm.entidad = frmBusquedas.MiEntidad.AprobacionesTrabajadorcxp;
                    frm.cod_empresa = cod_empresa;
                    frm.infolab=infolab;
                    frm.filtro = dato;
                    frm.ListaAprobacionesexistentes = ListadoAprobadores;
                    break;


            }
            frm.ShowDialog();
            if (frm.ListTrabajadorAprobadoresenvio == null) { return; }
            if (frm.gvAyuda.SelectedRowsCount == 0) return;
            switch (tipo)
            {
                case "AprobacionesTrabajadorcxp":
                    bsAprobador.DataSource = null;
                    EAprobaciones.EcuentasPagar objEmp = new EAprobaciones.EcuentasPagar();
                    foreach (EAprobaciones.EcuentasPagar obj in frm.ListTrabajadorAprobadoresenvio)
                    {
                        objEmp = frm.ListTrabajadorAprobadoresenvio.Find(x => x.cod_trabajador == obj.cod_trabajador);
                        if (objEmp != null) ListadoAprobadores.Add(objEmp);
                    }

                    bsAprobador.DataSource = ListadoAprobadores;
                    gvAprobador.RefreshData();

                    


                    //txtCodTrabajador.Text = frm.codigo;
                    //txtTrabajador.Text = frm.descripcion;
                    //eTrabajador.eInfoLaboral_Trabajador obj = new eTrabajador.eInfoLaboral_Trabajador();
                    //obj = blTrab.Obtener_Trabajador<eTrabajador.eInfoLaboral_Trabajador>(5, frm.codigo);
                    //txtUbicacion.Text = obj.dsc_empresa + " - " + obj.dsc_sede_empresa;
                    //txtUbicacion.Tag = obj.cod_sede_empresa;
                    //cod_empresa = obj.cod_empresa; cod_sede_empresa = obj.cod_sede_empresa;
                    break;
            }
        }
    }
}