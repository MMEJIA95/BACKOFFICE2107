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
    public partial class FiltroCajachica : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly UnitOfWork unit;
        TaskScheduler scheduler;
        Timer oTimerLoadMtto;
        public FiltroCajachica()
        {
            InitializeComponent();
            unit = new UnitOfWork();

        }

        public void Inicializar()
        {
            try
            {
                CargarLookUpEdit();
                lkpSedeEmpresa.ItemIndex = 0;
                lkpResponsable.ItemIndex = 0;
                lkpTipoCaja.ItemIndex = 0;


                //BuscarFacturas();
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
            lkpSedeEmpresa.EditValue = null; lkpResponsable.EditValue = null; lkpTipoCaja.EditValue = null;
            List<EAprobaciones.Ecajachica> lista = unit.Aprobaciones.ListarCajaChica<EAprobaciones.Ecajachica>(19, lkpEmpresa.EditValue.ToString());
            if (lista.Count > 0) lkpSedeEmpresa.EditValue = lista[0].cod_sede_empresa;
        }

        private void lkpSedeEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpSedeEmpresa.EditValue != null)
            {
                unit.CajaChica.CargaCombosLookUp("Responsablecajacerradaaprobar", lkpResponsable, "cod_responsable", "dsc_responsable", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
                lkpResponsable.EditValue = null; lkpTipoCaja.EditValue = null;
                List<EAprobaciones.Ecajachica> lista = new List<EAprobaciones.Ecajachica>();
                lista = unit.Aprobaciones.ListarCajaChica<EAprobaciones.Ecajachica>(20, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
                if (lista.Count >0) lkpResponsable.EditValue = lista[0].cod_trabajador;
                EAprobaciones.Ecajachica obj = lista.Find(x => x.cod_trabajador == Program.Sesion.Usuario.cod_trabajador);
                if (obj != null)
                {
                    lkpResponsable.EditValue = Program.Sesion.Usuario.cod_trabajador;
                }
                else
                {
                    if (lista.Count == 1) lkpResponsable.ItemIndex = 0;
                }
            }
        }

        private void lkpResponsable_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpResponsable.EditValue != null)
            {
                unit.CajaChica.CargaCombosLookUp("TipoCajaResponsablecajacerradaaprobar", lkpTipoCaja, "cod_caja", "dsc_caja", "", valorDefecto: true, cod_responsable: lkpResponsable.EditValue.ToString(), cod_empresa: lkpEmpresa.EditValue.ToString());
                lkpTipoCaja.EditValue = null;
                List<EAprobaciones.Ecajachica> lista = new List<EAprobaciones.Ecajachica>();
                lista = unit.Aprobaciones.ListarCajaChica<EAprobaciones.Ecajachica>(21, cod_responsable: lkpResponsable.EditValue.ToString(), cod_empresa: lkpEmpresa.EditValue.ToString());
                if (lista.Count > 0) lkpTipoCaja.ItemIndex = 0;
            }
        }

    
    }
}
