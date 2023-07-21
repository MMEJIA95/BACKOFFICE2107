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

namespace UI_BackOffice.Tools.FiltrosAprobaciones
{
    public partial class FiltroEntregasaRendir : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly UnitOfWork unit;
        public DateTime fechainicio, fechafin;
        public FiltroEntregasaRendir()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaInicio.EditValue = oPrimerDiaDelMes;
            dtFechaFin.EditValue = oUltimoDiaDelMes;
        }
        public void Inicializar()
        {
            try
            {
                CargarLookUpEdit();
                lkpSedeEmpresa.ItemIndex = 0;
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
                unit.Aprobaciones.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
                List<EAprobaciones> list = unit.Aprobaciones.ListarEmpresas<EAprobaciones>(4, Program.Sesion.Usuario.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;

            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            unit.Trabajador.CargaCombosLookUp("SedesEmpresa", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, lkpEmpresa.EditValue.ToString());
            lkpSedeEmpresa.ItemIndex = 0; 
            List<EAprobaciones.Ecajachica> lista = unit.Aprobaciones.ListarCajaChica<EAprobaciones.Ecajachica>(19, lkpEmpresa.EditValue.ToString());
            if (lista.Count == 1) lkpSedeEmpresa.EditValue = lista[0].cod_sede_empresa;

        }

        private void chkConFecha_EditValueChanged(object sender, EventArgs e)
        {
            if (chkConFecha.CheckState == CheckState.Checked)
            {
                dtFechaInicio.Enabled = true; dtFechaFin.Enabled = true;
            }
            else
            {
                dtFechaInicio.Enabled = false; dtFechaFin.Enabled = false;
            }
        }

       

        private void dtFechaFin_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtFechaFin.EditValue) < Convert.ToDateTime(dtFechaInicio.EditValue)) dtFechaFin.EditValue = Convert.ToDateTime(dtFechaInicio.EditValue);
            fechainicio = Convert.ToDateTime(dtFechaInicio.EditValue);
            fechafin = Convert.ToDateTime(dtFechaFin.EditValue);
        }
    }
}
