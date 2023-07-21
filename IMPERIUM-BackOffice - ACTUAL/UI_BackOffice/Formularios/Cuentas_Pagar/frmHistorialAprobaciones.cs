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
using DevExpress.XtraEditors.Controls;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    internal enum Cuentas
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmHistorialAprobaciones : HNG_Tools.SimpleModalForm
    {
        private readonly UnitOfWork unit;
        private object checkedItems;
        TaskScheduler scheduler;
        Timer oTimerLoadMtto;
        List<EAprobaciones.eTrabajador> ListaFacturas = new List<EAprobaciones.eTrabajador>();
        public string modulomultiple = "", empresamultiple = "",trabajadormultiple="";
        public frmHistorialAprobaciones()
        {
            unit = new UnitOfWork();
            InitializeComponent();
            configurar_formulario();
            Inicializar();
            unit = new UnitOfWork();
            oTimerLoadMtto = new Timer();
            oTimerLoadMtto.Interval = 500;
            oTimerLoadMtto.Tick += oTimerLoadMtto_Tick;
           
        }
        private void oTimerLoadMtto_Tick(object sender, EventArgs e)
        {
            try
            {
                oTimerLoadMtto.Stop();
                Inicializar();
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }
        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();
                //Fecha
                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtFechaInicio.EditValue = oPrimerDiaDelMes;
                dtFechaFin.EditValue = oUltimoDiaDelMes;
                

            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }
        private void configurar_formulario()
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            unit.Globales.ConfigurarGridView_ClasicStyle(gcHistorial, gvHistorial);

        }
        private void CargarLookUpEdit()
        {
            try
            {
                unit.Aprobaciones.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "",cod_usuario: Program.Sesion.Usuario.cod_usuario);
                List<EAprobaciones> list = unit.Aprobaciones.ListarEmpresas<EAprobaciones>(4, Program.Sesion.Usuario.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
                unit.Aprobaciones.CargaCombosLookUp("Modulos", lkpModulo, "cod_modulo", "dsc_modulo", "");
                //unit.Aprobaciones.CargaCombosChecked("Validacion", chklkpTrabajador, "cod_usuario", "dsc_usuario", "", );
                lkpModulo.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }

        private void chklkpTrabajador_EditValueChanged(object sender, EventArgs e)
        {
            CheckedListBoxItemCollection colecciontrabajador = chklkpTrabajador.Properties.GetItems();
            trabajadormultiple = "";
            foreach (CheckedListBoxItem item in colecciontrabajador)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    trabajadormultiple = item.Value + "," + trabajadormultiple; // Este es el valor seleccionado
                }
            }
            
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            BuscarFacturas();

            
        }

        private void lkpModulo_EditValueChanged(object sender, EventArgs e)
        {
            //CheckedListBoxItemCollection collection = lkpModulo.Properties.GetItems();
            //CheckedListBoxItemCollection collectionempresa = lkpEmpresa.Properties.GetItems();
            
            //foreach (CheckedListBoxItem item in collection)
            //{
            //    if (item.CheckState == CheckState.Checked)
            //    {
            //        modulomultiple = item.Value+","+ modulomultiple; // Este es el valor seleccionado
            //    }
            //}
            //foreach (CheckedListBoxItem item in collectionempresa)
            //{
            //    if (item.CheckState == CheckState.Checked)
            //    {
            //        empresamultiple = item.Value + "," + empresamultiple; // Este es el valor seleccionado
            //    }
            //}

            unit.Aprobaciones.CargaCombosChecked("Trabajadores", chklkpTrabajador, "cod_usuario", "dsc_usuario", "", modulo: lkpModulo.EditValue.ToString(),empresa:lkpEmpresa.EditValue.ToString());

        }
        private void BuscarFacturas()
        {

            EAprobaciones.eTrabajador eDirec = gvHistorial.GetFocusedRow() as EAprobaciones.eTrabajador;
            ListaFacturas = unit.Aprobaciones.ListarFacturasHistorial<EAprobaciones.eTrabajador>(16, Convert.ToDateTime(dtFechaInicio.EditValue), Convert.ToDateTime(dtFechaFin.EditValue), 
                                                                                                trabajadormultiple,lkpEmpresa.EditValue.ToString(),lkpModulo.EditValue.ToString());
            bsFactura.DataSource = ListaFacturas;
        }
    }
}