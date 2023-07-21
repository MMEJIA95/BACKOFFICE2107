using BE_BackOffice;
using BL_BackOffice;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSplashScreen;
using HNG_Tools;
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

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmAdjuntarObservacionContable : HNG_Tools.SimpleModalForm
    {
        private readonly UnitOfWork unit;
        public int id_detalle, id_historial;
        public string cod_empresa, observacionauditoria, flg_observacion , modulo="", cod_caja="",cod_movimiento_rendido="",cod_sede_empresa="", cod_entregarendir="", Actualizar="SI";

        private void frmAdjuntarObservacionContable_Load(object sender, EventArgs e)
        {
            if (flg_observacion == "SI") { visualizarobservacion(); }
        }

        public frmAdjuntarObservacionContable()
        {
            unit = new UnitOfWork();
            InitializeComponent();
           

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string valor = rch_Adjuntardocumento.HtmlText;
            if (valor == null) { HNG.MessageError("Debe ingresar una descripción", "Contabilizar documentos"); return; }
            else
            {
                valor = valor.Trim();
                string resultado="";
                if (modulo == "Documentos") { 
                    resultado = unit.Factura.AgregarObservacionHistorialContable(14, id_detalle, id_historial, cod_empresa, valor);
                        if (resultado != "OK") { HNG.MessageError("NO SE PUDO CARGAR EL DOCUMENTO , INTENTE DE NUEVO", "ERROR AL CARGAR DOCUMENTO"); return; }
                        else { HNG.MessageSuccess("SE REGISTRO EL DOCUMENTO CON EXITO", "CARGA EXITOSA"); return; }
                }else if(modulo == "CajaChica")
                {
                    resultado = unit.Factura.AgregarObservacionHistorialContable(16, id_detalle, id_historial, cod_empresa, valor,cod_movimiento_rendido,cod_caja,cod_sede_empresa);
                    if (resultado != "OK") { HNG.MessageError("NO SE PUDO CARGAR EL DOCUMENTO , INTENTE DE NUEVO", "ERROR AL CARGAR DOCUMENTO"); return; }
                    else { HNG.MessageSuccess("SE REGISTRO EL DOCUMENTO CON EXITO", "CARGA EXITOSA"); return; }
                }
                else if (modulo == "EntregasRendir")
                {
                    resultado = unit.Factura.AgregarObservacionHistorialContable(17, id_detalle, id_historial, cod_empresa, valor, cod_movimiento_rendido, cod_caja, cod_sede_empresa, cod_entregarendir);
                    if (resultado != "OK") { HNG.MessageError("NO SE PUDO CARGAR EL DOCUMENTO , INTENTE DE NUEVO", "ERROR AL CARGAR DOCUMENTO"); return; }
                    else { HNG.MessageSuccess("SE REGISTRO EL DOCUMENTO CON EXITO", "CARGA EXITOSA"); return; }
                }

                Actualizar = "SI";

            }

        }
        private void visualizarobservacion()
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Cargando documentos", "Cargando...");
            rch_Adjuntardocumento.HtmlText = observacionauditoria;
            SplashScreenManager.CloseForm();

        }
    }
}