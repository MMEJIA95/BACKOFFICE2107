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
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraSplashScreen;
using ExcelDataReader;

namespace UI_BackOffice.Formularios.Creditos
{
    public partial class frmListadoCreditos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        public frmPrincipal frmHandler = new frmPrincipal();
        List<eCreditoVehicular.eCronogramaDetalle> listCronograma = new List<eCreditoVehicular.eCronogramaDetalle>();
        Brush SemAlDia = Brushes.Green;
        Brush SemAtrasado = Brushes.Red;
        int markWidth = 16;

        public frmListadoCreditos()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
        public frmListadoCreditos(frmPrincipal frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmListadoCreditos_Load(object sender, EventArgs e)
        {
            CultureInfo info = new CultureInfo("es-PE");
            string symbol = info.NumberFormat.CurrencySymbol;
            info.NumberFormat.CurrencySymbol = symbol;
            rtxtImporte.Mask.Culture = info;
            rtxtImporteInteres.Mask.Culture = info;
            rtxtImporteInteres2.Mask.Culture = info;
            Inicializar();
        }

        private void Inicializar()
        {
            //Fecha
            DateTime date = DateTime.Now;
            //DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            //DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaDesembolsoInicio.EditValue = new DateTime(date.Year, 1, 1);
            dtFechaDesembolsoFin.EditValue = new DateTime(date.Year, 12, 31);
        }

        private void chkFechaDesembolso_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkFechaDesembolso.CheckState == CheckState.Checked)
            {
                dtFechaDesembolsoInicio.Enabled = true; dtFechaDesembolsoFin.Enabled = true;
                //Fecha
                DateTime date = DateTime.Now;
                dtFechaDesembolsoInicio.EditValue = new DateTime(date.Year, 1, 1);
                dtFechaDesembolsoFin.EditValue = new DateTime(date.Year, 12, 31);
                chkFechaProximoPago.CheckState = CheckState.Unchecked; dtFechaProximoPago.Enabled = false; dtFechaProximoPago.EditValue = null;
            }
            else
            {
                chkFechaProximoPago.CheckState = CheckState.Checked;
            }
        }

        private void chkFechaProximoPago_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkFechaProximoPago.CheckState == CheckState.Checked)
            {
                dtFechaProximoPago.Enabled = true; dtFechaProximoPago.EditValue = DateTime.Today;
                dtFechaDesembolsoInicio.Enabled = false; dtFechaDesembolsoFin.Enabled = false;
                chkFechaDesembolso.CheckState = CheckState.Unchecked; dtFechaDesembolsoInicio.EditValue = null; dtFechaDesembolsoFin.EditValue = null;
            }
            else
            {
                chkFechaDesembolso.CheckState = CheckState.Checked;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo datos", "Cargando...");
                List<eCreditoVehicular.eCronogramaCabecera> ListCronogramas = new List<eCreditoVehicular.eCronogramaCabecera>();
                ListCronogramas = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaCabecera>(1, "", fch_inicial: Convert.ToDateTime(dtFechaDesembolsoInicio.EditValue),
                                                                                                               fch_final: Convert.ToDateTime(dtFechaDesembolsoFin.EditValue),
                                                                                                               fch_proximo_pago: Convert.ToDateTime(dtFechaProximoPago.EditValue));
                bsListadoCreditos.DataSource = ListCronogramas;

                List<eCreditoVehicular.eCronogramaDetalle> ListResumenIntereses = new List<eCreditoVehicular.eCronogramaDetalle>();
                ListResumenIntereses = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaDetalle>(12, "", fch_inicial: Convert.ToDateTime(dtFechaDesembolsoInicio.EditValue),
                                                                                                               fch_final: Convert.ToDateTime(dtFechaDesembolsoFin.EditValue),
                                                                                                               fch_proximo_pago: Convert.ToDateTime(dtFechaProximoPago.EditValue));
                bsResumenIntereses.DataSource = ListResumenIntereses;

                List<eCreditoVehicular.eCronogramaDetalle> ListResumenPagos = new List<eCreditoVehicular.eCronogramaDetalle>();
                if (chkFechaDesembolso.CheckState == CheckState.Checked)
                {
                    ListResumenPagos = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.eCronogramaDetalle>(17, "", fch_inicial: Convert.ToDateTime(dtFechaDesembolsoInicio.EditValue),
                                                                                                                   fch_final: Convert.ToDateTime(dtFechaDesembolsoFin.EditValue),
                                                                                                                   fch_proximo_pago: Convert.ToDateTime(dtFechaProximoPago.EditValue));
                    bsResumenPagos.DataSource = ListResumenPagos;
                }
                else
                {
                    ListResumenPagos.Clear(); bsResumenPagos.DataSource = ListResumenPagos;
                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevoCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmClienteCredito frm = new frmClienteCredito();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSimuladorCuotasFijas_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSimuladorCuotasFijas frm = new frmSimuladorCuotasFijas();
            frm.MdiParent = frmHandler;
            frm.Show();
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
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\ListadoCreditos" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                //gvListadoCronogramaCabecera.ExportToXlsx(archivo);
                if (xtraTabControl1.SelectedTabPage == xtabListadoCreditos) gvListadoCronogramaCabecera.ExportToXlsx(archivo);
                if (xtraTabControl1.SelectedTabPage == xtabResumenFacturar) pivotResumenFacturar.ExportToXlsx(archivo);
                if (xtraTabControl1.SelectedTabPage == xtabResumenPagos) pivotResumenPagos.ExportToXlsx(archivo);
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
            gvListadoCronogramaCabecera.ShowPrintPreview();
        }

        private void gvListadoCronogramaCabecera_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eCreditoVehicular.eCronogramaCabecera obj = gvListadoCronogramaCabecera.GetFocusedRow() as eCreditoVehicular.eCronogramaCabecera;
                    frmSimuladorCuotasFijas frm = new frmSimuladorCuotasFijas();
                    frm.MdiParent = frmHandler;
                    frm.cod_credito = obj.cod_credito;
                    frm.cod_cronograma = obj.cod_cronograma;
                    frm.num_placa = obj.num_placa;
                    frm.dsc_nombres_completos = obj.dsc_nombres_completos;
                    frm.TotalCapital = obj.imp_capitalvigente;
                    frm.TotalCredito = obj.imp_totalvigente;
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmListadoCreditos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnBuscar_Click(btnBuscar, new EventArgs());
            }
        }

        private void gvListadoCronogramaCabecera_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "imp_Capital") { e.Appearance.ForeColor = Color.Blue; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                if (e.Column.FieldName == "imp_capitalvigente") { e.Appearance.ForeColor = Color.Black; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                if (e.Column.FieldName == "imp_montopagado") { e.Appearance.ForeColor = Color.DarkGreen; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                if (e.Column.FieldName == "imp_capitalatrasado") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                if (e.Column.FieldName == "imp_interesatrasado") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                if (e.Column.FieldName == "imp_montoatrasado") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImportarPagosCOFIDE_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string result = "";
                result = ImportarPagos_COFIDE("COFIDE");
                //result = "OK";
                if (result == "OK")
                {
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Aplicando pagos", "Cargando...");
                    //AplicarPagos("COFIDE");
                    //SplashScreenManager.CloseForm();
                    //MessageBox.Show("Se aplicaron los pagos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBuscar_Click(btnBuscar, new EventArgs());
                }
                gvListadoCronogramaCabecera.RefreshData();
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
                //result = "OK";
                if (result == "OK")
                {
                    //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Aplicando pagos", "Cargando...");
                    //AplicarPagos("BBVA");
                    //SplashScreenManager.CloseForm();
                    //MessageBox.Show("Se aplicaron los pagos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBuscar_Click(btnBuscar, new EventArgs());
                }
                gvListadoCronogramaCabecera.RefreshData();
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
                        foreach (eCreditoVehicular.ePagosDetalle ePago in ListPagos)
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

                            //ePago.flg_pagoaplicado = resta_amortizacion <= 0 ? "SI" : "NO";

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
                                //eCuota.num_cuota += 1;
                                if (ListCuotas.Count > 1)
                                {
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

        private void btnListadoPagos_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eCreditoVehicular.eCronogramaCabecera obj = gvListadoCronogramaCabecera.GetFocusedRow() as eCreditoVehicular.eCronogramaCabecera;
                frmPagosDetalleCuota frm = new frmPagosDetalleCuota();
                frm.MiAccion = frmPagosDetalleCuota.PagoCuota.Todos;
                //frm.cod_credito = obj.cod_credito;
                //frm.cod_cronograma = obj.cod_cronograma;
                //frm.num_placa = obj.num_placa;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAplicarPagosPendientes_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro aplicar aplicar los pagos faltantes?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Aplicando pagos", "Cargando...");
                    AplicarPagos("COFIDE");
                    AplicarPagos("BBVA");
                    SplashScreenManager.CloseForm();
                    MessageBox.Show("Se aplicaron los pagos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBuscar_Click(btnBuscar, new EventArgs());
                    gvListadoCronogramaCabecera.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminarTodosPagos_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de eliminar todos los pagos de la base de datos?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Eliminando pagos", "Cargando...");
                    string result = unit.CreditoVehicular.EliminarCronogramaDetalle(3, "", "", "");
                    SplashScreenManager.CloseForm();
                    MessageBox.Show("Se eliminaron los pagos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBuscar_Click(btnBuscar, new EventArgs());
                    gvListadoCronogramaCabecera.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminarCredito_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de eliminar el crédito seleccionado?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eCreditoVehicular.eCronogramaCabecera obj = gvListadoCronogramaCabecera.GetFocusedRow() as eCreditoVehicular.eCronogramaCabecera;
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Eliminando crédito", "Cargando...");
                    string result = unit.CreditoVehicular.EliminarCronogramaDetalle(4, obj.cod_credito, "", "");
                    SplashScreenManager.CloseForm();
                    MessageBox.Show("Se eliminó el crédito de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBuscar_Click(btnBuscar, new EventArgs());
                    gvListadoCronogramaCabecera.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoCronogramaCabecera_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoCronogramaCabecera_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCreditoVehicular.eCronogramaCabecera obj = gvListadoCronogramaCabecera.GetRow(e.RowHandle) as eCreditoVehicular.eCronogramaCabecera;
                    if (e.Column.FieldName == "num_cuotavigente" && obj.flg_semaforo == "NO") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (e.Column.FieldName == "imp_capitalatrasado" && obj.imp_capitalatrasado > 0) { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (e.Column.FieldName == "imp_interesatrasado" && obj.imp_interesatrasado > 0) { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (e.Column.FieldName == "imp_montoatrasado" && obj.imp_montoatrasado > 0) { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    e.DefaultDraw();
                    if (e.Column.FieldName == "flg_semaforo")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        if (cellValue == "SI") { b = SemAlDia; } else { b = SemAtrasado; }
                        e.Graphics.FillEllipse(b, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoCronogramaCabecera_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

    }
}