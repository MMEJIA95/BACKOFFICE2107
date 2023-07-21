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
using BL_BackOffice;
using System.Globalization;

namespace UI_BackOffice.Formularios.Creditos
{
    public partial class frmCancelarCredito : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        public List<eCreditoVehicular.eCronogramaDetalle> listCronograma = new List<eCreditoVehicular.eCronogramaDetalle>();
        public string cod_credito, cod_cronograma, num_placa;
        public decimal TotalCapital = 0, TotalCredito = 0;
        
        public frmCancelarCredito()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmCancelarCredito_Load(object sender, EventArgs e)
        {
            btnAceptar.Appearance.BackColor = Program.Sesion.Colores.Verde;
            Inicializar();
        }

        private void Inicializar()
        {
            CultureInfo info = new CultureInfo("es-PE");
            string symbol = info.NumberFormat.CurrencySymbol;
            info.NumberFormat.CurrencySymbol = symbol;
            txtMontoPagar.Properties.Mask.Culture = info;
            txtMontoPagar.Text = grdbOpcionesPago.SelectedIndex == 0 ? TotalCapital.ToString() : TotalCredito.ToString();
        }

        private void grdbOpcionesPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMontoPagar.Text = grdbOpcionesPago.SelectedIndex == 0 ? TotalCapital.ToString() : TotalCredito.ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                eCreditoVehicular.eCronogramaCabecera eCab = unit.CreditoVehicular.ObtenerDatos_CreditoVehicular<eCreditoVehicular.eCronogramaCabecera>(9, cod_credito, cod_cronograma, num_placa);
                eCab.flg_activo = "NO"; eCab.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                eCab = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_CronogramaCabecera<eCreditoVehicular.eCronogramaCabecera>(eCab);
                foreach(eCreditoVehicular.eCronogramaDetalle obj in listCronograma)
                {
                    obj.flg_pagado = "SI"; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    eCreditoVehicular.eCronogramaDetalle objDet = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_CronogramaDetalle<eCreditoVehicular.eCronogramaDetalle>(obj);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}