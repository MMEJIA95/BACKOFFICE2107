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
    public partial class frmPeriodoTributario : HNG_Tools.SimpleModalForm
    {
        private readonly UnitOfWork unit;
        public string tipo_doc = "", serie_documento = "", cod_proveedor = "", empresa = "", numero_documento = "";
        eFacturaProveedor obj = new eFacturaProveedor();
        public frmPeriodoTributario()
        {
            unit = new UnitOfWork();
            InitializeComponent();
            configurar_formulario();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            eFacturaProveedor eFact = new eFacturaProveedor();
            eFact.tipo_documento = tipo_doc;
            eFact.serie_documento = serie_documento;
            eFact.numero_documento = Convert.ToDecimal(numero_documento);
            eFact.cod_proveedor = cod_proveedor;
            eFact.periodo_tributario = eFact.periodo_tributario = dtFechaTributaria.EditValue == null || dtFechaTributaria.EditValue == "" ? "" : Convert.ToDateTime(dtFechaTributaria.EditValue).ToString("MM-yyyy");
            eFact = unit.Factura.InsertarFacturaProveedor<eFacturaProveedor>(eFact);
            if (eFact == null) { HNG.MessageError("Error al registrar documento.", "ERROR"); return; }
            else { HNG.MessageSuccess("Se modifico el Periodo tributario", "EXITO"); }

        }
        private void configurar_formulario()
        {
            this.TitleBackColor = Program.Sesion.Colores.Verde;

        }
    }
}
