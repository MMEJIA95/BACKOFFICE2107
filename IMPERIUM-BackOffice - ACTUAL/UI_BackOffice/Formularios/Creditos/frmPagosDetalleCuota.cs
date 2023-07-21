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
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace UI_BackOffice.Formularios.Creditos
{
    public partial class frmPagosDetalleCuota : DevExpress.XtraEditors.XtraForm
    {
        internal enum PagoCuota
        {
            PorCuota = 0,
            Todos = 1
        }

        private readonly UnitOfWork unit;
        internal PagoCuota MiAccion = PagoCuota.PorCuota;
        List<eCreditoVehicular.ePagosDetalle> ListPagoCuota = new List<eCreditoVehicular.ePagosDetalle>();
        public string cod_credito, cod_cronograma, num_placa;
        public int num_cuota;

        public frmPagosDetalleCuota()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmCuotasDetallePago_Load(object sender, EventArgs e)
        {
            CultureInfo info = new CultureInfo("es-PE");
            string symbol = info.NumberFormat.CurrencySymbol;
            info.NumberFormat.CurrencySymbol = symbol;
            rtxtImporte.Mask.Culture = info;
            Inicializar();
        }

        private void gvListadoPagosDetalleCuota_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            try
            {
                //    if (e.Column.FieldName == "imp_bruto") { e.Appearance.ForeColor = Color.Blue; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                //    if (e.Column.FieldName == "imp_cofide") { e.Appearance.ForeColor = Color.Black; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                if (e.Column.FieldName == "imp_neto") { e.Appearance.ForeColor = Color.Red; /*e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);*/ }
                if (e.Column.FieldName == "imp_aplicado") { e.Appearance.ForeColor = Color.Blue; /*e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);*/ }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Inicializar()
        {
            switch (MiAccion)
            {
                case PagoCuota.PorCuota:
                    ListPagoCuota = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.ePagosDetalle>(7, cod_credito, cod_cronograma, num_placa, num_cuota: num_cuota);
                    bsPagosDetalleCuota.DataSource = ListPagoCuota;
                    break;
                case PagoCuota.Todos:
                    bar2.Visible = true;
                    simpleLabelItem2.Visibility = LayoutVisibility.Always;
                    layoutControlItem2.Visibility = LayoutVisibility.Always;
                    simpleLabelItem3.Visibility = LayoutVisibility.Always;
                    layoutControlItem3.Visibility = LayoutVisibility.Always;
                    dtFechaInicial.EditValue = DateTime.Today.AddMonths(-1);
                    dtFechaFinal.EditValue = DateTime.Today;
                    gvListadoPagosDetalleCuota.OptionsSelection.MultiSelect = true;
                    gvListadoPagosDetalleCuota.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                    gvListadoPagosDetalleCuota.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
                    ListPagoCuota = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.ePagosDetalle>(14, cod_credito, cod_cronograma, num_placa, fch_inicial: Convert.ToDateTime(dtFechaInicial.EditValue), fch_final: Convert.ToDateTime(dtFechaFinal.EditValue));
                    bsPagosDetalleCuota.DataSource = ListPagoCuota;
                    foreach (GridColumn col in gvListadoPagosDetalleCuota.Columns)
                    {
                        if (col.FieldName == "num_cuota" || col.FieldName == "imp_aplicado") { col.Visible = false; }
                    }
                    break;
            }
        }

        private void gvListadoPagosDetalleCuota_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle != 0) return;
                eCreditoVehicular.ePagosDetalle obj = gvListadoPagosDetalleCuota.GetFocusedRow() as eCreditoVehicular.ePagosDetalle;
                if (obj == null) return;
                if (e.Column.FieldName == "fch_deposito")
                {
                    DateTime fch_deposito; int num_correlativo = 0;
                    fch_deposito = obj.fch_deposito;
                    for (int x = 1; x <= gvListadoPagosDetalleCuota.RowCount; x++)
                    {
                        eCreditoVehicular.ePagosDetalle obj2 = gvListadoPagosDetalleCuota.GetRow(x) as eCreditoVehicular.ePagosDetalle;
                        if (obj2 == null) continue;
                        num_correlativo += 1;
                        obj2.fch_deposito = fch_deposito;
                    }
                    gvListadoPagosDetalleCuota.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnValidarPago_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gvListadoPagosDetalleCuota.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar al menos 1 registro", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Validando pagos", "Cargando...");
                foreach (int nRow in gvListadoPagosDetalleCuota.GetSelectedRows())
                {
                    eCreditoVehicular.ePagosDetalle obj = gvListadoPagosDetalleCuota.GetRow(nRow) as eCreditoVehicular.ePagosDetalle;
                    obj.flg_pagovalidado = "SI"; obj.fch_pagovalidado = DateTime.Today;

                    string result = unit.CreditoVehicular.InsertarActualizar_CreditoVeh_PagosDetalle(obj);
                }
                ListPagoCuota.Clear();
                ListPagoCuota = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.ePagosDetalle>(14, cod_credito, cod_cronograma, num_placa, fch_inicial: Convert.ToDateTime(dtFechaInicial.EditValue), fch_final: Convert.ToDateTime(dtFechaFinal.EditValue));
                bsPagosDetalleCuota.DataSource = ListPagoCuota; gvListadoPagosDetalleCuota.RefreshData();
                SplashScreenManager.CloseForm();
                XtraMessageBox.Show("Se validaron los pagos de manera satisfactoria", "", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoPagosDetalleCuota_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoPagosDetalleCuota_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoPagosDetalleCuota_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eCreditoVehicular.ePagosDetalle obj = gvListadoPagosDetalleCuota.GetRow(e.RowHandle) as eCreditoVehicular.ePagosDetalle;
                    if (e.Column.FieldName == "flg_pagovalidado") { e.Appearance.ForeColor = Color.Blue; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (e.Column.FieldName == "imp_neto") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (e.Column.FieldName == "imp_aplicado") { e.Appearance.ForeColor = Color.Blue; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (e.Column.FieldName == "fch_deposito") { e.Appearance.ForeColor = Color.DarkGreen; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                    if (e.Column.FieldName == "fch_deposito" && obj.fch_deposito.ToString().Contains("1/01/0001")) e.DisplayText = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmPagosDetalleCuota_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.F5)
            {
                gvListadoPagosDetalleCuota.Focus();
                ListPagoCuota.Clear();
                ListPagoCuota = unit.CreditoVehicular.ListarDatos_CreditoVehicular<eCreditoVehicular.ePagosDetalle>(14, cod_credito, cod_cronograma, num_placa, fch_inicial: Convert.ToDateTime(dtFechaInicial.EditValue), fch_final: Convert.ToDateTime(dtFechaFinal.EditValue));
                bsPagosDetalleCuota.DataSource = ListPagoCuota; gvListadoPagosDetalleCuota.RefreshData();
            }
            if (e.KeyCode == Keys.F7)
            {
                XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Seleccione la fecha a asignar";
                DateEdit dtFecha = new DateEdit(); dtFecha.Width = 100; args.DefaultResponse = DateTime.Today;
                //dtFecha.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;
                //dtFecha.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                //dtFecha.Properties.Mask.EditMask = "MMMM-yyyy";
                dtFecha.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                dtFecha.Properties.Mask.UseMaskAsDisplayFormat = true;
                args.Editor = dtFecha;
                var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
                if ((res == DialogResult.OK || res == DialogResult.Yes) && dtFecha.EditValue != null)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Contabilizando documentos", "Cargando...");
                    foreach (int nRow in gvListadoPagosDetalleCuota.GetSelectedRows())
                    {
                        eCreditoVehicular.ePagosDetalle obj = gvListadoPagosDetalleCuota.GetRow(nRow) as eCreditoVehicular.ePagosDetalle;
                        obj.fch_deposito = Convert.ToDateTime(dtFecha.EditValue);
                    }
                    gvListadoPagosDetalleCuota.RefreshData();
                    SplashScreenManager.CloseForm();
                }
            }
        }

    }
}