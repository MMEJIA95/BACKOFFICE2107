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
using DevExpress.XtraSplashScreen;
using UI_BackOffice.Formularios.Cuentas_Pagar;

namespace UI_BackOffice.Tools.FiltrosAprobaciones
{
    public partial class FiltroCuetasPagar : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly UnitOfWork unit;
        TaskScheduler scheduler;
        Timer oTimerLoadMtto;
        private int width = 200;

        public int WidthL { get => width; set => width = value; }
        public DateTime fechainicio,fechafin;

        public FiltroCuetasPagar()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            oTimerLoadMtto = new Timer();
            oTimerLoadMtto.Interval = 500;
        }
        public void Inicializar()
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
                chkcbTipoDocumento.CheckAll();
                

                //BuscarFacturas();
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }

        private void dtFechaFin_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtFechaFin.EditValue) < Convert.ToDateTime(dtFechaInicio.EditValue)) dtFechaFin.EditValue = Convert.ToDateTime(dtFechaInicio.EditValue);
            fechainicio = Convert.ToDateTime(dtFechaInicio.EditValue);
            fechafin = Convert.ToDateTime(dtFechaFin.EditValue);

        }

        private void CargarLookUpEdit()
        {
            try
            {
                unit.Aprobaciones.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
                unit.Aprobaciones.CargaCombosChecked("TipoDocumento", chkcbTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "");
                unit.Aprobaciones.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);

                List<EAprobaciones> list = unit.Aprobaciones.ListarEmpresas<EAprobaciones>(2, Program.Sesion.Usuario.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }

        

        

 
    }
}
