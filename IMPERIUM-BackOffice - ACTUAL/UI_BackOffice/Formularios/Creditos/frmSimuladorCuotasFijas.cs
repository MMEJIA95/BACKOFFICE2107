using DevExpress.XtraBars;
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
using Microsoft.VisualBasic;
using System.Globalization;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using ExcelDataReader;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors;
using static UI_BackOffice.Formularios.Creditos.frmPagosDetalleCuota;

namespace UI_BackOffice.Formularios.Creditos
{
    public partial class frmSimuladorCuotasFijas : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        List<eCreditoVehicular.eCronogramaDetalle> listCronograma = new List<eCreditoVehicular.eCronogramaDetalle>();
        public string cod_credito = "", cod_cronograma = "", num_placa = "", dsc_nombres_completos = "";
        public decimal TotalCapital = 0, TotalCredito = 0;

        public frmSimuladorCuotasFijas()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmSimuladorCuotasFijas_Load(object sender, EventArgs e)
        {
            CultureInfo info = new CultureInfo("es-PE");
            string symbol = info.NumberFormat.CurrencySymbol;
            info.NumberFormat.CurrencySymbol = symbol;
            txtMontoSolicitado.Properties.Mask.Culture = info;
            txtTotalCapital.Properties.Mask.Culture = info;
            txtTotalCredito.Properties.Mask.Culture = info;
            rtxtImporte.Mask.Culture = info;
            dtFechaDesembolso.EditValue = DateTime.Today;

            if (cod_credito != "" && cod_cronograma != "" && num_placa != "")
            {
                txtCodCredito.Text = cod_credito; txtNumeroPlaca.Text = num_placa; txtCliente.Text = dsc_nombres_completos;
                ObtenerCronograma(cod_credito, cod_cronograma, num_placa);
                txtTotalCapital.EditValue = TotalCapital; txtTotalCredito.EditValue = TotalCredito;
            }
        }

        private void gvListadoCronograma_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoCronograma_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void dtFechaDesembolso_EditValueChanged(object sender, EventArgs e)
        {
            int dia = Convert.ToDateTime(dtFechaDesembolso.EditValue).Day;
            txtDiasPago.EditValue = dia >= 5 && dia <= 19 ? 20 : 5;
        }

        private void txtTasaAnual_EditValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToDecimal(txtTasaAnual.EditValue) > 0)
            {
                txtTasaMensual.EditValue = Math.Pow(Convert.ToDouble(1 + Convert.ToDecimal(txtTasaAnual.EditValue)), Convert.ToDouble((1 / (decimal)12))) - 1;
            }
        }

        private void btnNuevoCronograma_ItemClick(object sender, ItemClickEventArgs e)
        {
            BloqueoControles(true, false, true);
            txtMontoSolicitado.EditValue = 0;
            dtFechaDesembolso.EditValue = DateTime.Today;
            txtCuotas.EditValue = 12;
            txtDiasPago.EditValue = 5;
            txtTasaAnual.Text = "0";
            txtTasaMensual.Text = "0";
            txtTIRAnual.Text = "0";
            txtTIRM.Text = "0";
            listCronograma.Clear(); gvListadoCronograma.RefreshData();
            txtCodCredito.Text = "";
            txtNumeroPlaca.Text = "";
            txtCliente.Text = "";
            cod_credito = ""; cod_cronograma = ""; num_placa = "";
        }

        private void btnConsultarCronograma_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmListadoCronogramaCabecera frm = new frmListadoCronogramaCabecera();
            frm.ShowDialog();
            if (frm.eCab != null)
            {
                cod_credito = frm.eCab.cod_credito; cod_cronograma = frm.eCab.cod_cronograma; num_placa = frm.eCab.num_placa;
                txtCodCredito.Text = cod_credito; txtNumeroPlaca.Text = num_placa; txtCliente.Text = frm.eCab.dsc_nombres_completos;
                ObtenerCronograma(cod_credito, cod_cronograma, num_placa);
            }
        }

        private void ObtenerCronograma(string cod_credito, string cod_cronograma, string num_placa)
        {
            eCreditoVehicular.eCronogramaCabecera eCab = unit.CreditoVehicular.ObtenerDatos_CreditoVehicular<eCreditoVehicular.eCronogramaCabecera>(9, cod_credito, cod_cronograma, num_placa);
            if (eCab == null) { MessageBox.Show("No existe cronograma generado en el sistema", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            txtMontoSolicitado.EditValue = eCab.imp_Capital;
            dtFechaDesembolso.EditValue = eCab.fch_desembolso;
            txtCuotas.EditValue = eCab.num_cuotas;
            txtDiasPago.EditValue = eCab.num_diapago;
            txtTasaAnual.EditValue = eCab.num_tasaanual;
            txtTasaMensual.EditValue = eCab.num_tasamensual;
            txtTIRAnual.EditValue = eCab.num_tasaTIRanual;
            txtTIRM.EditValue = eCab.num_tasaTIRM;
            listCronograma = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaDetalle>(10, eCab.cod_credito, eCab.cod_cronograma, eCab.num_placa);
            bsListadoCronograma.DataSource = listCronograma; gvListadoCronograma.RefreshData();
            if (listCronograma.Count > 0) BloqueoControles(false, true, false);
            if (listCronograma.Count == 0) BloqueoControles(true, false, true);
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnCalcularCronograma.Enabled = Enabled;
            btnGuardarCronograma.Enabled = Enabled;
            txtMontoSolicitado.ReadOnly = ReadOnly;
            dtFechaDesembolso.ReadOnly = ReadOnly;
            txtCuotas.ReadOnly = ReadOnly;
            txtDiasPago.ReadOnly = ReadOnly;
            txtTasaAnual.ReadOnly = ReadOnly;
            txtTasaMensual.ReadOnly = ReadOnly;
            txtTIRAnual.ReadOnly = ReadOnly;
            txtTIRM.ReadOnly = ReadOnly;
            txtCodCredito.ReadOnly = ReadOnly;
            txtNumeroPlaca.ReadOnly = ReadOnly;
            //txtCliente.ReadOnly = ReadOnly;
        }

        private void btnCalcularCronograma_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtMontoSolicitado.EditValue) == 0) { MessageBox.Show("El monto solicitado no puede ser 0", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtMontoSolicitado.Focus(); return; }
            if (Convert.ToDecimal(txtTasaAnual.EditValue) == 0) { MessageBox.Show("La tasa anual no puede ser 0", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtTasaAnual.Focus(); return; }

            decimal imp_cuota = 0, tasaMes = 0, prc_igv = 0, imp_couta1 = 0; DateTime fecha_old = DateTime.Today, fecha_new = DateTime.Today;
            fecha_old = Convert.ToDateTime(dtFechaDesembolso.EditValue);
            int dia_fecha = fecha_old.Day;
            int mes_fecha = fecha_old.Month;
            int dia_pago = Convert.ToInt32(txtDiasPago.EditValue);
            fecha_new = dia_fecha >= 5 && dia_fecha <= 19 ?
                new DateTime(mes_fecha == 12 ? fecha_old.Year + 1 : fecha_old.Year, mes_fecha == 12 ? 1 : mes_fecha + 1, dia_pago) :
                dia_fecha >= 20 ? new DateTime(mes_fecha >= 11 ? fecha_old.Year + 1 : fecha_old.Year, mes_fecha >= 11 ? fecha_old.AddMonths(2).Month : mes_fecha + 2, dia_pago) :
                new DateTime(mes_fecha == 12 ? fecha_old.Year + 1 : fecha_old.Year, mes_fecha == 12 ? 1 : mes_fecha + 1, dia_pago);
            imp_couta1 = Convert.ToDecimal(txtMontoSolicitado.EditValue);
            prc_igv = (decimal)0.18;
            tasaMes = Math.Round(Convert.ToDecimal(txtTasaMensual.EditValue), 6);
            imp_cuota = Math.Round(Convert.ToDecimal(txtMontoSolicitado.EditValue) / Convert.ToDecimal((1 - Math.Pow(1 + Convert.ToDouble(tasaMes), -1 * Convert.ToDouble(txtCuotas.EditValue))) / Convert.ToDouble(tasaMes)), 6);
            listCronograma.Clear();

            eCreditoVehicular.eCronogramaCabecera objCab = new eCreditoVehicular.eCronogramaCabecera();
            objCab.imp_Capital = Convert.ToDecimal(txtMontoSolicitado.EditValue);
            objCab.num_cuotas = Convert.ToInt32(txtCuotas.EditValue);
            objCab.num_tasaanual = Convert.ToDecimal(txtTasaAnual.EditValue);
            objCab.num_tasamensual = tasaMes;

            for (int x = 1; x <= Convert.ToInt32(txtCuotas.EditValue); x++)
            {
                eCreditoVehicular.eCronogramaDetalle obj = new eCreditoVehicular.eCronogramaDetalle();
                obj.num_cuota = x;
                obj.fch_cuota = fecha_new;
                obj.num_dias = Convert.ToInt32((fecha_new - fecha_old).TotalDays);
                obj.imp_capitalinicial = imp_couta1;
                obj.imp_cuotasinigv = imp_cuota;
                obj.imp_interes = Math.Round(obj.imp_capitalinicial * objCab.num_tasamensual, 6);
                obj.imp_coutaigv = Math.Round(obj.imp_interes * prc_igv, 6);
                obj.imp_cuotaconigv = Math.Round(imp_cuota + obj.imp_coutaigv, 6);
                obj.imp_amortizacion = Math.Round(obj.imp_cuotasinigv - obj.imp_interes, 6);
                obj.imp_capitalfinal = Math.Round(obj.imp_capitalinicial - obj.imp_amortizacion, 6);
                obj.imp_interes = Math.Round(obj.imp_interes, 6);
                obj.imp_coutaigv = Math.Round(obj.imp_coutaigv, 6);
                obj.imp_coutaigv = Math.Round(obj.imp_coutaigv, 6);
                obj.imp_cuotasinigv = Math.Round(obj.imp_cuotasinigv, 6);
                obj.imp_montoporpagar = obj.imp_cuotaconigv;
                imp_couta1 = obj.imp_capitalfinal;
                fecha_old = obj.fch_cuota;
                fecha_new = fecha_old.AddMonths(1);

                listCronograma.Add(obj);
            }
            bsListadoCronograma.DataSource = listCronograma; gvListadoCronograma.RefreshData();

            //double[] tmpCashflows = new double[] { -2000, 213.91, 213.12, 212.31, 211.47, 210.60, 209.70, 208.78, 207.83, 206.85, 205.84, 204.79, 203.72 };
            double[] tmpCashflows = new double[listCronograma.Count + 1];
            tmpCashflows[0] = -1 * Convert.ToDouble(txtMontoSolicitado.EditValue);
            int y = 1;
            foreach(eCreditoVehicular.eCronogramaDetalle cc in listCronograma)
            {
                tmpCashflows[y] = Convert.ToDouble(cc.imp_cuotaconigv);
                y++;
            }
            double tmpIrr = Microsoft.VisualBasic.Financial.IRR(ref tmpCashflows, 0);

            txtTIRM.EditValue = Math.Round(tmpIrr, 6);
            txtTIRAnual.EditValue = Math.Round(Convert.ToDecimal(Math.Pow((1 + tmpIrr), 12) - 1), 6);
            txtTotalCapital.EditValue = txtMontoSolicitado.EditValue;
            txtTotalCredito.EditValue = txtMontoSolicitado.EditValue;
        }

        private void btnImportarPagosCOFIDE_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string result = "";
                result = ImportarPagos_COFIDE("COFIDE");
                if (result == "OK")
                {
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Aplicando pagos", "Cargando...");
                    //AplicarPagos("COFIDE");
                    //SplashScreenManager.CloseForm();
                    //MessageBox.Show("Se aplicaron los pagos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ObtenerCronograma(cod_credito, cod_cronograma, num_placa);
                }
                gvListadoCronograma.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImportarPagosBBVA_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string result = "";
                result = ImportarPagos_COFIDE("BBVA");
                if (result == "OK")
                {
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Aplicando pagos", "Cargando...");
                    //AplicarPagos("BBVA");
                    //SplashScreenManager.CloseForm();
                    //MessageBox.Show("Se aplicaron los pagos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ObtenerCronograma(cod_credito, cod_cronograma, num_placa);
                }
                gvListadoCronograma.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAplicarPagoManual_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListadoCronograma.RefreshData();
                if (listCronograma.Count == 0 || cod_credito == "") { MessageBox.Show("Debe seleccionar un credito.", "Aplicar pago manual", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                frmAplicarPagoManual frm = new frmAplicarPagoManual();
                frm.ShowDialog();
                if (frm.MontoPagado > 0)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Contabilizando documentos", "Cargando...");
                    eCreditoVehicular.ePagosDetalle obj = new eCreditoVehicular.ePagosDetalle();
                    var validar = "";
                    obj.num_placa = num_placa; obj.cod_cronograma = cod_cronograma;
                    obj.fch_recaudo = frm.FechaPago; obj.dsc_hora = ""; obj.dsc_estacion = "Pago realizado manualmente"; obj.num_tanqueo = 0;
                    obj.imp_bruto = frm.MontoPagado; obj.imp_cofide = 0; obj.imp_neto = frm.MontoPagado;
                    obj.cod_credito = cod_credito; obj.cod_chip = ""; obj.fch_archivo = frm.FechaPago;
                    obj.dsc_origen = "MANUAL"; obj.flg_pagoaplicado = "NO"; obj.flg_pagovalidado = "NO"; 
                    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario; obj.dsc_destino = frm.dsc_destino;
                    validar = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_PagosDetalle(obj);
                    if (validar != "OK") { MessageBox.Show("Error al aplicar pago manual", "Aplicar pago manual", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    AplicarPagos("MANUAL");
                    XtraMessageBox.Show("Se aplicó el pago de manera satisfactoria", "Aplicar pago manual", MessageBoxButtons.OK);
                    ObtenerCronograma(cod_credito, cod_cronograma, num_placa);
                    SplashScreenManager.CloseForm();
                }
                //else
                //{
                //    MessageBox.Show("Debe ingresar el monto a pagar", "Aplicar pago manual", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string ImportarPagos_COFIDE(string dsc_origen)
        {
            try
            {
                OpenFileDialog OpenDialog = new OpenFileDialog();
                OpenDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                OpenDialog.FilterIndex = 1;
                if (OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Importando pagos COFIDE", "Cargando...");
                    FileStream stream = File.Open(OpenDialog.FileName, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet result = excelReader.AsDataSet();
                    var lista = result.Tables;

                    List<eCreditoVehicular.ePagosDetalle> ListPagos = new List<eCreditoVehicular.ePagosDetalle>();
                    for (int i = 1; i < lista[0].Rows.Count; i++)
                    {
                        eCreditoVehicular.ePagosDetalle obj = new eCreditoVehicular.ePagosDetalle();
                        var validar = "";

                        obj.num_placa = lista[0].Rows[i].ItemArray[0] == DBNull.Value ? "" : lista[0].Rows[i].ItemArray[0].ToString();
                        obj.fch_recaudo = lista[0].Rows[i].ItemArray[1] == DBNull.Value ? new DateTime() : Convert.ToDateTime(lista[0].Rows[i].ItemArray[1].ToString());
                        obj.dsc_hora = lista[0].Rows[i].ItemArray[2] == DBNull.Value ? "" : Convert.ToDateTime(lista[0].Rows[i].ItemArray[2].ToString()).ToShortTimeString().Substring(0, 5);
                        obj.dsc_estacion = lista[0].Rows[i].ItemArray[3] == DBNull.Value ? "" : lista[0].Rows[i].ItemArray[3].ToString();
                        obj.num_tanqueo = lista[0].Rows[i].ItemArray[5] == DBNull.Value ? 0 : Convert.ToDecimal(lista[0].Rows[i].ItemArray[5].ToString());
                        obj.imp_bruto = lista[0].Rows[i].ItemArray[8] == DBNull.Value ? 0 : Convert.ToDecimal(lista[0].Rows[i].ItemArray[8].ToString());
                        obj.imp_cofide = lista[0].Rows[i].ItemArray[9] == DBNull.Value ? 0 : Convert.ToDecimal(lista[0].Rows[i].ItemArray[9].ToString());
                        obj.imp_neto = lista[0].Rows[i].ItemArray[10] == DBNull.Value ? 0 : Convert.ToDecimal(lista[0].Rows[i].ItemArray[10].ToString());
                        obj.cod_credito = lista[0].Rows[i].ItemArray[11] == DBNull.Value ? "" : lista[0].Rows[i].ItemArray[11].ToString();
                        obj.cod_chip = lista[0].Rows[i].ItemArray[14] == DBNull.Value ? "" : lista[0].Rows[i].ItemArray[14].ToString();
                        obj.fch_archivo = lista[0].Rows[i].ItemArray[15] == DBNull.Value ? new DateTime() : Convert.ToDateTime(lista[0].Rows[i].ItemArray[15].ToString());
                        obj.dsc_origen = dsc_origen; obj.flg_pagoaplicado = "NO"; obj.flg_pagovalidado = "NO"; 
                        obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario; obj.dsc_destino = dsc_origen == "COFIDE" ? "CML" : dsc_origen;

                        eCreditoVehicular.eCronogramaCabecera eCab = unit.CreditoVehicular.ObtenerDatos_CreditoVehicular<eCreditoVehicular.eCronogramaCabecera>(2, obj.cod_credito, num_placa: obj.num_placa);
                        if (eCab == null)
                        {
                            obj.dsc_mensajeImportar = "ERROR";
                        }
                        else
                        {
                            validar = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_PagosDetalle(obj);
                            obj.dsc_mensajeImportar = validar.Equals("OK") ? "OK" : "ERROR";
                            obj.num_linea = i + 1;
                        }
                        ListPagos.Add(obj);
                    }

                    var buenas = ListPagos.Count(x => x.dsc_mensajeImportar.Equals("OK"));
                    var malas = ListPagos.Count(x => x.dsc_mensajeImportar.Equals("ERROR"));
                    var msg = string.Format("Se importó " + ListPagos.Count() + " registros : {0}", Environment.NewLine);

                    msg += string.Format("Correctas : " + buenas + "{0}", Environment.NewLine);
                    msg += string.Format("Error : " + malas + "{0}", Environment.NewLine);

                    foreach (eCreditoVehicular.ePagosDetalle item in ListPagos.Where(x => x.dsc_mensajeImportar.Equals("ERROR")))
                    {
                        msg += string.Format("Nro crédito : " + item.cod_credito + "  Nro Placa : " + item.num_placa + "  Hora : " + item.dsc_hora + " Fila: " + item.num_linea + " " + item.dsc_estacion + "{0}", Environment.NewLine);
                    }

                    excelReader.Close();
                    excelReader.Dispose();
                    OpenDialog.Dispose();
                    MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SplashScreenManager.CloseForm();
                    return "OK";
                }
                else
                {
                    return "ERROR";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "ERROR";
            }
        }

        private void AplicarPagos(string dsc_origen)
        {
            try
            {
                //TRAER TODOS LOS PAGOS NO APLICADOS
                List<eCreditoVehicular.eCronogramaCabecera> ListCab = new List<eCreditoVehicular.eCronogramaCabecera>();
                ListCab = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaCabecera>(4, "", dsc_origen: dsc_origen);
                if (ListCab.Count > 0)
                {
                    foreach (eCreditoVehicular.eCronogramaCabecera eCab in ListCab)
                    {
                        //TRAER LOS PAGOS NO APLICADOS DE UN CREDITO
                        List<eCreditoVehicular.ePagosDetalle> ListPagos = new List<eCreditoVehicular.ePagosDetalle>();
                        ListPagos = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.ePagosDetalle>(6, eCab.cod_credito, eCab.cod_cronograma, eCab.num_placa, dsc_origen: dsc_origen);
                        if (ListPagos.Count == 0) continue;
                        foreach(eCreditoVehicular.ePagosDetalle ePago in ListPagos)
                        {
                            //TRAER LA ULTIMA CUOTA NO PAGADA TOTALMENTE
                            List<eCreditoVehicular.eCronogramaDetalle> ListCuotas = new List<eCreditoVehicular.eCronogramaDetalle>();
                            ListCuotas = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaDetalle>(3, eCab.cod_credito, eCab.cod_cronograma, eCab.num_placa);
                            if (ListCuotas.Count == 0) continue;
                            //TRAER LOS PAGOS APLICADOS A LA ULTIMA CUOTA NO PAGADA TOTALMENTE
                            List<eCreditoVehicular.ePagoCuota> ListPagoCuota = new List<eCreditoVehicular.ePagoCuota>();
                            ListPagoCuota = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.ePagoCuota>(5, eCab.cod_credito, eCab.cod_cronograma, eCab.num_placa, num_cuota: ListCuotas[0].num_cuota);
                            decimal SumaPago = (from tabla in ListPagoCuota 
                                                select tabla.imp_neto).Sum();
                            decimal resta_interes = 0, resta_igv = 0, resta_amortizacion = 0, resta_total = 0, restante = 0,
                                aplicado_interes = 0, aplicado_igv = 0, aplicado_capital = 0, SumaConPago = 0;
                            SumaConPago = SumaPago + ePago.imp_neto;
                            //VALIDO SI COMPLETA EL IMPORTE PARA INTERES, IGV Y CAPITAL
                            eCreditoVehicular.eCronogramaDetalle eCuota = ListCuotas[0];
                            resta_total = eCuota.imp_cuotaconigv - SumaConPago;

                            resta_interes = eCuota.imp_interes - SumaConPago;
                            aplicado_interes = resta_interes >= 0 ? ePago.imp_neto : ePago.imp_neto + resta_interes < 0 ? 0 : ePago.imp_neto + resta_interes;
                            resta_igv = resta_interes < 0 ? eCuota.imp_coutaigv + resta_interes : resta_igv;
                            aplicado_igv = resta_interes < 0 ? resta_igv >= 0 ? aplicado_interes == 0 ? ePago.imp_neto : Math.Abs(resta_interes) : ePago.imp_neto + resta_igv < 0 ? 0 : ePago.imp_neto + resta_igv > eCuota.imp_coutaigv ? eCuota.imp_coutaigv : ePago.imp_neto + resta_igv : resta_igv;
                            resta_amortizacion = resta_igv < 0 ? eCuota.imp_amortizacion + resta_igv : resta_amortizacion;
                            aplicado_capital = resta_igv < 0 ? resta_amortizacion >= 0 ? aplicado_igv == 0 ? ePago.imp_neto : Math.Abs(resta_igv) : ePago.imp_neto + resta_amortizacion > eCuota.imp_amortizacion ? eCuota.imp_amortizacion : ePago.imp_neto + resta_amortizacion : resta_amortizacion;

                            ////GUARDAR EL MONTO QUE SE ESTA PAGANDO A LA ULTIMA CUOTA Y EL RESTANTE APLICARLO A LA SIGUIENTE
                            eCreditoVehicular.ePagoCuota ePagoCuota = new eCreditoVehicular.ePagoCuota();
                            ePagoCuota.cod_credito = eCab.cod_credito; ePagoCuota.cod_cronograma = eCab.cod_cronograma; 
                            ePagoCuota.num_placa = eCab.num_placa; ePagoCuota.num_cuota = eCuota.num_cuota;
                            ePagoCuota.num_linea = ePago.num_linea; ePagoCuota.imp_neto = resta_total < 0 ? ePago.imp_neto + resta_total : ePago.imp_neto;
                            ePagoCuota.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                            ePagoCuota.imp_interes = aplicado_interes; ePagoCuota.imp_igv = aplicado_igv; ePagoCuota.imp_capital = aplicado_capital;
                            eCreditoVehicular.ePagoCuota ePagoResult = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_PagosDetalleCuota<eCreditoVehicular.ePagoCuota>(ePagoCuota);

                            if (resta_total <= 0)
                            {
                                eCuota.flg_pagado = "SI"; eCuota.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                eCuota = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_CronogramaDetalle<eCreditoVehicular.eCronogramaDetalle>(eCuota);
                            }
                            int num_cuota = 1;
                            ////SI EL PAGO ES MAYOR A LA CUOTA, LE APLICA A LAS CUOTAS RESTANTES
                            while (resta_total < 0)
                            {
                                if (ListCuotas.Count > 1)
                                {
                                    //eCuota.num_cuota += 1;
                                    if (num_cuota >= ListCuotas.Count) { resta_total = 0; continue; }
                                    eCreditoVehicular.eCronogramaDetalle objCuota = ListCuotas[num_cuota];
                                    restante = objCuota.imp_cuotaconigv + resta_total;

                                    resta_interes = 0; resta_igv = 0; resta_amortizacion = 0;
                                    aplicado_interes = 0; aplicado_igv = 0; aplicado_capital = 0; SumaConPago = 0;
                                    SumaConPago = Math.Abs(resta_total); ePago.imp_neto = Math.Abs(resta_total);
                                    resta_interes = objCuota.imp_interes - SumaConPago;
                                    aplicado_interes = resta_interes >= 0 ? ePago.imp_neto : ePago.imp_neto + resta_interes < 0 ? 0 : ePago.imp_neto + resta_interes;
                                    resta_igv = resta_interes < 0 ? objCuota.imp_coutaigv + resta_interes : resta_igv;
                                    aplicado_igv = resta_interes < 0 ? resta_igv >= 0 ? aplicado_interes == 0 ? ePago.imp_neto : Math.Abs(resta_interes) : ePago.imp_neto + resta_igv < 0 ? 0 : ePago.imp_neto + resta_igv > objCuota.imp_coutaigv ? objCuota.imp_coutaigv : ePago.imp_neto + resta_igv : resta_igv;
                                    resta_amortizacion = resta_igv < 0 ? objCuota.imp_amortizacion + resta_igv : resta_amortizacion;
                                    aplicado_capital = resta_igv < 0 ? resta_amortizacion >= 0 ? aplicado_igv == 0 ? ePago.imp_neto : Math.Abs(resta_igv) : ePago.imp_neto + resta_amortizacion > objCuota.imp_amortizacion ? objCuota.imp_amortizacion : ePago.imp_neto + resta_amortizacion : resta_amortizacion;

                                    eCreditoVehicular.ePagoCuota objPagoCuota = new eCreditoVehicular.ePagoCuota();
                                    objPagoCuota.cod_credito = eCab.cod_credito; objPagoCuota.cod_cronograma = eCab.cod_cronograma;
                                    objPagoCuota.num_placa = eCab.num_placa; objPagoCuota.num_cuota = objCuota.num_cuota;
                                    objPagoCuota.num_linea = ePago.num_linea; objPagoCuota.imp_neto = restante < 0 ? Math.Abs(resta_total) + restante : Math.Abs(resta_total);
                                    objPagoCuota.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                    objPagoCuota.imp_interes = aplicado_interes; objPagoCuota.imp_igv = aplicado_igv; objPagoCuota.imp_capital = aplicado_capital;
                                    eCreditoVehicular.ePagoCuota objPagoResult = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_PagosDetalleCuota<eCreditoVehicular.ePagoCuota>(objPagoCuota);

                                    if (restante <= 0)
                                    {
                                        objCuota.flg_pagado = "SI"; objCuota.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                                        objCuota = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_CronogramaDetalle<eCreditoVehicular.eCronogramaDetalle>(objCuota);
                                    }
                                    resta_total = restante; num_cuota += 1;
                                }
                                else
                                {
                                    resta_total = 0;
                                }
                            }

                            //if (resta_total < 0)
                            //{
                            //    ePagoCuota.num_cuota = ePagoCuota.num_cuota + 1; ePagoCuota.imp_neto = resta_total * (-1);
                            //    ePagoCuota = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_PagosDetalleCuota<eCreditoVehicular.ePagoCuota>(ePagoCuota);
                            //}
                            if (ePagoCuota == null) { MessageBox.Show("Error al aplicar pago con la cuota", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            ePago.flg_pagoaplicado = "SI"; ePago.flg_pagovalidado = "NO";
                            string result = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_PagosDetalle(ePago);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCodCredito_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //TRAE LOS NOMBRES DEL CLIENTE
                    eCreditoVehicular obj = new eCreditoVehicular();
                    obj = unit.CreditoVehicular.ObtenerDatos_CreditoVehicular<eCreditoVehicular>(13, txtCodCredito.Text.Trim());
                    if (obj == null) { MessageBox.Show("Crédito no registrado en el sistema", "", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    txtCliente.Text = obj.dsc_nombres_completos;

                    //TRAE LA PLACA Y EL CRONOGRAMA EN CASO EXISTA 
                    List<eCreditoVehicular.eCronogramaCabecera> obj2 = new List<eCreditoVehicular.eCronogramaCabecera>();
                    obj2 = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaCabecera>(15, txtCodCredito.Text.Trim());
                    if (obj2.Count == 0) return;
                    if (obj2.Count == 1)
                    {
                        txtNumeroPlaca.Text = obj2[0].num_placa;
                        cod_credito = txtCodCredito.Text.Trim();
                        cod_cronograma = obj2[0].cod_cronograma;
                        num_placa = obj2[0].num_placa;
                        ObtenerCronograma(cod_credito, cod_cronograma, num_placa);
                        txtTotalCapital.EditValue = 0; txtTotalCredito.EditValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNumeroPlaca_Leave(object sender, EventArgs e)
        {
            try
            {
                //TRAE LA PLACA Y EL CRONOGRAMA EN CASO EXISTA 
                List<eCreditoVehicular.eCronogramaCabecera> obj2 = new List<eCreditoVehicular.eCronogramaCabecera>();
                obj2 = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaCabecera>(16, txtCodCredito.Text.Trim(), num_placa: txtNumeroPlaca.Text.Trim());
                if (obj2.Count == 0) return;
                if (obj2.Count == 1)
                {
                    cod_credito = txtCodCredito.Text.Trim();
                    cod_cronograma = obj2[0].cod_cronograma;
                    num_placa = txtNumeroPlaca.Text.Trim();
                    ObtenerCronograma(cod_credito, cod_cronograma, num_placa);
                    txtTotalCapital.EditValue = 0; txtTotalCredito.EditValue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmSimuladorCuotasFijas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) ObtenerCronograma(cod_credito, cod_cronograma, num_placa);
        }

        private void btnEliminarCronograma_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                List<eCreditoVehicular.ePagosDetalle> lista = new List<eCreditoVehicular.ePagosDetalle>();
                lista = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.ePagosDetalle>(19, cod_credito, cod_cronograma, num_placa);
                if (lista.Count > 0) { MessageBox.Show("No se puede eliminar el cronograma ya que tiene pagos vinculados.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                if (MessageBox.Show("¿Esta seguro de eliminar el cronograma?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string result = unit.CreditoVehicular.EliminarCronogramaDetalle(1, cod_credito, cod_cronograma, num_placa);
                    if (result != "OK") { MessageBox.Show("Error al eliminar el cronograma.", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (result == "OK") MessageBox.Show("Se eliminó el cronograma de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listCronograma.Clear(); bsListadoCronograma.DataSource = listCronograma; gvListadoCronograma.RefreshData();
                    BloqueoControles(true, false, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnCancelarCredito_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                gvListadoCronograma.RefreshData();
                if (listCronograma.Count == 0 || cod_credito == "") { MessageBox.Show("Debe seleccionar un credito.", "Aplicar pago manual", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (MessageBox.Show("¿Esta seguro de cancelar el crédito?", "Cancelar crédito", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Calculando importes", "Cargando...");
                    //decimal TotalCapital = 0, TotalCredito = 0;
                    //listCronograma = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaDetalle>(10, cod_credito, cod_cronograma, num_placa);
                    //bsListadoCronograma.DataSource = listCronograma; gvListadoCronograma.RefreshData();

                    ////TRAER LAS CUOTAS NO PAGADA TOTALMENTE
                    //List<eCreditoVehicular.eCronogramaDetalle> ListCuotas = new List<eCreditoVehicular.eCronogramaDetalle>();
                    //ListCuotas = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaDetalle>(3, cod_credito, cod_cronograma, num_placa);
                    ////TRAER LOS PAGOS APLICADOS A LA ULTIMA CUOTA NO PAGADA TOTALMENTE
                    //List<eCreditoVehicular.ePagoCuota> ListPagoCuota = new List<eCreditoVehicular.ePagoCuota>();
                    //ListPagoCuota = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.ePagoCuota>(5, cod_credito, cod_cronograma, num_placa, num_cuota: ListCuotas[0].num_cuota);
                    //decimal SumaPago = (from tabla in ListPagoCuota
                    //                    select tabla.imp_neto).Sum();
                    //decimal resta_interes = 0, resta_igv = 0, resta_amortizacion = 0, resta_total = 0;
                    //////VALIDO SI COMPLETA EL IMPORTE PARA INTERES, IGV Y CAPITAL
                    //eCreditoVehicular.eCronogramaDetalle eCuota = ListCuotas[0];
                    //resta_total = eCuota.imp_cuotaconigv - SumaPago;

                    //resta_interes = eCuota.imp_interes - SumaPago;
                    //eCuota.imp_interes = resta_interes <= 0 ? 0 : resta_interes;
                    //resta_igv = resta_interes < 0 ? eCuota.imp_coutaigv + resta_interes : eCuota.imp_coutaigv - resta_igv;
                    //eCuota.imp_coutaigv = resta_igv <= 0 ? 0 : resta_igv;
                    //resta_amortizacion = resta_igv < 0 ? eCuota.imp_amortizacion + resta_igv : eCuota.imp_amortizacion - resta_amortizacion;
                    //eCuota.imp_amortizacion = resta_amortizacion < 0 ? 0 : resta_amortizacion;
                    //eCuota.imp_cuotasinigv = eCuota.imp_amortizacion + eCuota.imp_interes;
                    //eCuota.imp_cuotaconigv = eCuota.imp_amortizacion + eCuota.imp_coutaigv + eCuota.imp_interes;

                    //TotalCredito = (from tabla in ListCuotas
                    //                select tabla.imp_cuotaconigv).Sum();

                    //decimal MontoCuotasTotal = (from tabla in ListCuotas
                    //                            where tabla.num_cuota != eCuota.num_cuota
                    //                            select tabla.imp_cuotaconigv).Sum();
                    //decimal MontoCuotasCapital = (from tabla in ListCuotas
                    //                           where tabla.num_cuota != eCuota.num_cuota
                    //                           select tabla.imp_amortizacion).Sum();

                    //TotalCredito = eCuota.imp_cuotaconigv + MontoCuotasTotal;
                    //TotalCapital = eCuota.imp_cuotaconigv + MontoCuotasCapital;

                    //SplashScreenManager.CloseForm();

                    frmCancelarCredito frm = new frmCancelarCredito();
                    frm.cod_credito = cod_credito;
                    frm.cod_cronograma = cod_cronograma;
                    frm.num_placa = num_placa;
                    frm.listCronograma = listCronograma;
                    frm.TotalCapital = TotalCapital;
                    frm.TotalCredito = TotalCredito;
                    frm.ShowDialog();

                    cod_cronograma = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardarCronograma_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodCredito.Text.Trim() == "") { MessageBox.Show("Debe ingresar el número de credito.", "Guardar cronograma", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCodCredito.Select(); return; }
                if (txtNumeroPlaca.Text.Trim() == "") { MessageBox.Show("Debe ingresar la placa.", "Guardar cronograma", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroPlaca.Select(); return; }
                if (txtNumeroPlaca.Text.Contains("-")) { MessageBox.Show("La placa no debe contener guión('-')", "Guardar cronograma", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroPlaca.Select(); return; }
                if (MessageBox.Show("¿Esta seguro de crear el cronograma al crédito seleccionado?", "Guardar cronograma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eCreditoVehicular.eCronogramaCabecera oCab = new eCreditoVehicular.eCronogramaCabecera();
                    oCab.cod_credito = txtCodCredito.Text.Trim(); oCab.cod_cronograma = cod_cronograma; oCab.num_placa = txtNumeroPlaca.Text.Trim();
                    oCab.imp_Capital = Convert.ToDecimal(txtMontoSolicitado.EditValue); oCab.fch_desembolso = Convert.ToDateTime(dtFechaDesembolso.EditValue);
                    oCab.num_cuotas = Convert.ToInt32(txtCuotas.Text); oCab.num_diapago = Convert.ToInt32(txtDiasPago.Text);
                    oCab.num_tasaanual = Convert.ToDecimal(txtTasaAnual.EditValue); oCab.num_tasamensual = Convert.ToDecimal(txtTasaMensual.EditValue);
                    oCab.num_tasaTIRanual = Convert.ToDecimal(txtTIRAnual.EditValue); oCab.num_tasaTIRM = Convert.ToDecimal(txtTIRM.EditValue);
                    oCab.flg_activo = "SI"; oCab.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    oCab = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_CronogramaCabecera<eCreditoVehicular.eCronogramaCabecera>(oCab);
                    cod_credito = oCab.cod_credito; cod_cronograma = oCab.cod_cronograma; num_placa = oCab.num_placa;
                    foreach (eCreditoVehicular.eCronogramaDetalle obj in listCronograma)
                    {
                        obj.cod_credito = cod_credito; obj.cod_cronograma = cod_cronograma; obj.num_placa = num_placa; 
                        obj.flg_pagado = "NO"; obj.fch_registro = DateTime.Today; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        eCreditoVehicular.eCronogramaDetalle objDet = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_CronogramaDetalle<eCreditoVehicular.eCronogramaDetalle>(obj);
                    }
                    if (oCab != null) MessageBox.Show("Se guardo el cronograma de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                gvListadoCronograma.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoCronograma_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCreditoVehicular.eCronogramaDetalle obj = gvListadoCronograma.GetRow(e.RowHandle) as eCreditoVehicular.eCronogramaDetalle;
                    if (obj == null) return;
                    if ((e.Column.FieldName == "imp_interes" && obj.imp_interes == 0) || 
                        (e.Column.FieldName == "imp_coutaigv" && obj.imp_coutaigv == 0) ||
                        (e.Column.FieldName == "imp_amortizacion" && obj.imp_amortizacion == 0))
                    { 
                        e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); 
                    }
                    if (e.Column.FieldName == "imp_montopagado") e.Appearance.ForeColor = Color.Blue;
                    if (e.Column.FieldName == "imp_montoporpagar") e.Appearance.ForeColor = Color.Red;
                    if (obj.imp_montoporpagar == 0) e.Appearance.ForeColor = Color.DarkGreen;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoCronograma_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    if (cod_credito == "") { MessageBox.Show("Debe seleccionar un crédito", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                    eCreditoVehicular.eCronogramaDetalle obj = gvListadoCronograma.GetFocusedRow() as eCreditoVehicular.eCronogramaDetalle;
                    frmPagosDetalleCuota frm = new frmPagosDetalleCuota();
                    frm.MiAccion = PagoCuota.PorCuota;
                    frm.cod_credito = cod_credito;
                    frm.cod_cronograma = cod_cronograma;
                    frm.num_placa = num_placa;
                    frm.num_cuota = obj.num_cuota;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportarExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportarExcel();
        }
        private void ExportarExcel()
        {
            try
            {
                string carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\SimuladorCuotaFija" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListadoCronograma.ExportToXlsx(archivo);
                if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvListadoCronograma.ShowPrintPreview();
        }

    }
}