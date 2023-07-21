﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BL_BackOffice;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmOpcionesProgMasiva : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public DateTime fch_pago;
        public string cod_pagar_a = "", dsc_observacion = "", Actualizar = "";
        public frmOpcionesProgMasiva()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmOpcionesProgMasiva_Load(object sender, EventArgs e)
        {
            dtFechaPago.EditValue = DateTime.Today;
            unit.Factura.CargaCombosLookUp("Pagar_A", lkpPagar_A, "cod_pagar_a", "dsc_pagar_a", "", valorDefecto: true);
        }

        private void btnCrearProgramacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Actualizar = "OK";
            fch_pago = Convert.ToDateTime(dtFechaPago.EditValue);
            cod_pagar_a = lkpPagar_A.EditValue.ToString();
            dsc_observacion = mmObservaciones.Text;
            this.Close();
        }
    }
}