using BE_BackOffice;
using BL_BackOffice;
using DevExpress.CodeParser;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraReports.Design;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_BackOffice.Formularios.Cuentas_Pagar;
using UI_BackOffice.Formularios.Sistema.Configuraciones_Maestras;
using Excel = Microsoft.Office.Interop.Excel;

namespace UI_BackOffice.Formularios.Bancos
{
    public partial class frmListadoMovimientoBancos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        List<eEmpresa.eBanco_Empresa> listCuentasBanc = new List<eEmpresa.eBanco_Empresa>();
        List<eEmpresa.eDetalleMovimientoBanco_Empresa> listMovBanco = new List<eEmpresa.eDetalleMovimientoBanco_Empresa>();
        

        public frmListadoMovimientoBancos()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadoMovimientoBancos_Load(object sender, EventArgs e)
        {
            //btnBancoScotiabank.ImageOptions.Image = igcImagenesBancos.Images["Logo_Scotiabank.png"];
            //btnBancoBBVA.ImageOptions.Image = igcImagenesBancos.Images["Logo_BBVA.png"];
            //btnBancoBCP.ImageOptions.Image = igcImagenesBancos.Images["Logo_BCP.png"];
            //btnBancoViaBCP.ImageOptions.Image = igcImagenesBancos.Images["Logo_BCP.png"];
            //btnBancoInterbank.ImageOptions.Image = igcImagenesBancos.Images["Logo_Interbank.png"];
            ////btnBancoBanbif.ImageOptions.Image = igcImagenesBancos.Images["Logo_BANBIF.png"];
            //btnBancoNacion.ImageOptions.Image = igcImagenesBancos.Images["Logo_BancoNacion.jpg"];

            groupControl1.AppearanceCaption.ForeColor = Program.Sesion.Colores.Verde;
            unit.Trabajador.CargaCombosLookUp("Empresa", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true);
            rlkpTipoMovimiento.DataSource = unit.Factura.CombosEnGridControl<eEmpresa.eDetalleMovimientoBanco_Empresa>("TipoMovimiento");
            rlkpOrigenMovimiento.DataSource = unit.Factura.CombosEnGridControl<eEmpresa.eDetalleMovimientoBanco_Empresa>("OrigenMovimiento");
            rlkpIdentificado.DataSource = unit.Factura.CombosEnGridControl<eEmpresa.eDetalleMovimientoBanco_Empresa>("Identificado");
            List<eFacturaProveedor> list = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
            if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            //unit.Trabajador.CargaCombosLookUp("SedesEmpresa", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, lkpEmpresa.EditValue.ToString());
            //lkpSedeEmpresa.EditValue = null;
            //List<eTrabajador.eInfoLaboral_Trabajador> lista = unit.Trabajador.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, lkpEmpresa.EditValue.ToString());
            //if (lista.Count == 1) lkpSedeEmpresa.EditValue = lista[0].cod_sede_empresa;
            if (lkpEmpresa.EditValue != null)
            {
                int nRow = tlvListaCuentasBancarias.FocusedRowHandle;
                listCuentasBanc = unit.Factura.Obtener_CuentasBancariasEmpresa<eEmpresa.eBanco_Empresa>(1, lkpEmpresa.EditValue.ToString()/*, lkpSedeEmpresa.EditValue.ToString()*/);
                bsListaCuentasBancarias.DataSource = listCuentasBanc;
                tlvListaCuentasBancarias.RefreshData();
                nRow = tlvListaCuentasBancarias.RowCount - 1 < nRow || nRow < 0 ? 0 : nRow;
                tlvListaCuentasBancarias.FocusedRowHandle = nRow;
                tlvListaCuentasBancarias_FocusedRowChanged(tlvListaCuentasBancarias, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(tlvListaCuentasBancarias.FocusedRowHandle - 1, tlvListaCuentasBancarias.FocusedRowHandle));
            }
        }

        private void lkpSedeEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            //if (lkpSedeEmpresa.EditValue != null)
            //{
            //    listCuentasBanc = unit.Factura.Obtener_CuentasBancariasEmpresa<eEmpresa.eBanco_Empresa>(1, lkpEmpresa.EditValue.ToString(), lkpSedeEmpresa.EditValue.ToString());
            //    bsListaCuentasBancarias.DataSource = listCuentasBanc;
            //    tlvListaCuentasBancarias.RefreshData();
            //}
        }

        private void tlvListaCuentasBancarias_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo registros", "Cargando...");
            tlvListaCuentasBancarias_FocusedRowChanged(tlvListaCuentasBancarias, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(tlvListaCuentasBancarias.FocusedRowHandle - 1, tlvListaCuentasBancarias.FocusedRowHandle));
            SplashScreenManager.CloseForm();
        }

        private void tlvListaCuentasBancarias_ItemDoubleClick(object sender, TileViewItemClickEventArgs e)
        {
            eEmpresa.eBanco_Empresa objBanco = tlvListaCuentasBancarias.GetFocusedRow() as eEmpresa.eBanco_Empresa;
            if (objBanco != null)
            {
                frmCtasBancariasEmpresa frm = new frmCtasBancariasEmpresa();
                frm.MiAccion = CtaBanco.Editar;
                frm.cod_empresa = objBanco.cod_empresa;
                frm.num_linea = objBanco.num_linea;
                frm.ShowDialog();
                if (frm.ActualizarListado) lkpEmpresa_EditValueChanged(lkpEmpresa, new EventArgs());
            }
        }

        private void tlvListaCuentasBancarias_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo registros", "Cargando...");
                if (e.FocusedRowHandle >= 0)
                {
                    eEmpresa.eBanco_Empresa objBanco = tlvListaCuentasBancarias.GetFocusedRow() as eEmpresa.eBanco_Empresa;
                    if (objBanco == null) return;
                    listMovBanco = unit.Factura.Obtener_CuentasBancariasEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(2, objBanco.cod_empresa, /*objBanco.cod_sede_empresa,*/ objBanco.num_linea);
                    bsListadoDetalleMovimientos.DataSource = listMovBanco;
                    gvListadoDetalleMovimientos.RefreshData();
                }
                else
                {
                    listMovBanco.Clear();
                    bsListadoDetalleMovimientos.DataSource = listMovBanco;
                    gvListadoDetalleMovimientos.RefreshData();
                }
                //SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                //SplashScreenManager.CloseForm();
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rlkpTipoMovimiento_EditValueChanged(object sender, EventArgs e)
        {
            gvListadoDetalleMovimientos.PostEditor();
        }

        private void gvListadoDetalleMovimientos_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            eEmpresa.eDetalleMovimientoBanco_Empresa obj = gvListadoDetalleMovimientos.GetFocusedRow() as eEmpresa.eDetalleMovimientoBanco_Empresa;
            obj.num_item = 0; obj.fch_ejecutada = DateTime.Today; obj.fch_efectiva = DateTime.Today; 
            obj.fch_registro = DateTime.Today; obj.cod_usuario_registro = Program.Sesion.Usuario.dsc_usuario;
            obj.cod_tipo_movimiento = "INGRESO"; obj.cod_origen_movimiento = "001";
            obj.flg_identificado = "NO"; obj.imp_monto = 0;
            gvListadoDetalleMovimientos.RefreshData();
        }

        private void gvListadoDetalleMovimientos_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                eEmpresa.eDetalleMovimientoBanco_Empresa obj = gvListadoDetalleMovimientos.GetRow(e.RowHandle) as eEmpresa.eDetalleMovimientoBanco_Empresa;
                if (obj != null)
                {
                    eEmpresa.eBanco_Empresa objBanco = tlvListaCuentasBancarias.GetFocusedRow() as eEmpresa.eBanco_Empresa;
                    obj.cod_empresa = objBanco.cod_empresa; /*obj.cod_sede_empresa = objBanco.cod_sede_empresa;*/
                    obj.num_linea = objBanco.num_linea; obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                    eEmpresa.eDetalleMovimientoBanco_Empresa obj2 = unit.Factura.Insertar_Actualizar_DetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(obj);
                    obj.num_item = obj2.num_item; obj.imp_monto = obj2.imp_monto;
                    if (obj2 == null) { MessageBox.Show("Error al guardar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    gvListadoDetalleMovimientos.RefreshData();
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
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\MovBancos" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListadoDetalleMovimientos.ExportToXlsx(archivo);
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
            gvListadoDetalleMovimientos.ShowPrintPreview();
        }

        private void btnCtasBancariasEmpresa_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCtasBancariasEmpresa frm = new frmCtasBancariasEmpresa();
            frm.ShowDialog();
            if (frm.ActualizarListado) lkpEmpresa_EditValueChanged(lkpEmpresa, new EventArgs());
        }

        private void tlvListaCuentasBancarias_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                TileView view = sender as TileView;
                string val = view.GetRowCellValue(e.RowHandle, "cod_banco").ToString();
                switch (val)
                {
                    case "BA001": e.Item.Elements[0].Image = igcImagenesBancos.Images["Logo_BCP.png"]; break;
                    case "BA002": e.Item.Elements[0].Image = igcImagenesBancos.Images["Logo_Interbank.png"]; break;
                    case "BA003": e.Item.Elements[0].Image = igcImagenesBancos.Images["Logo_BBVA.png"]; break;
                    case "BA005": e.Item.Elements[0].Image = igcImagenesBancos.Images["Logo_BancoNacion.jpg"]; break;
                    case "BA006": e.Item.Elements[0].Image = igcImagenesBancos.Images["Logo_Scotiabank.png"]; break;
                    case "BA021": e.Item.Elements[0].Image = igcImagenesBancos.Images["Logo_BANBIF.png"]; break;
                    default: e.Item.Elements[0].Image = igcImagenesBancos.Images["Logo_Banco.png"]; break;
                }
                string def = view.GetRowCellValue(e.RowHandle, "flg_defecto").ToString();
                if (def == "SI") e.Item.Elements[1].Image = igcImagenesBancos.Images["apply_32x32.png"];
                e.Item.AppearanceItem.Selected.BackColor = Program.Sesion.Colores.EventRow;
                //if (val == "BA006") e.Item.Elements[0].Image = SystemIcons.Question.ToBitmap();
            }
        }

        private void gvListadoDetalleMovimientos_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 1 && e.Column.FieldName == "cod_bloque_pago")
                {
                    eEmpresa.eDetalleMovimientoBanco_Empresa obj = gvListadoDetalleMovimientos.GetFocusedRow() as eEmpresa.eDetalleMovimientoBanco_Empresa;
                    if (obj == null) return;

                    frmFacturasDetalle frm = new frmFacturasDetalle();
                    frm.BusquedaBloquePago = true;
                    frm.BusquedaAutomatica = false;
                    frm.cod_bloque_pago = obj.cod_bloque_pago;
                    frm.num_linea_banco = obj.num_linea;
                    frm.cod_empresa = obj.cod_empresa;
                    frm.ShowDialog();
                }
                if (e.Clicks == 2 && e.Column.FieldName == "dsc_nro_operacion")
                {
                    eEmpresa.eDetalleMovimientoBanco_Empresa obj = gvListadoDetalleMovimientos.GetFocusedRow() as eEmpresa.eDetalleMovimientoBanco_Empresa;
                    if (obj.dsc_nro_operacion == null || obj.dsc_nro_operacion.Trim() == "") btnAsignarNroOperacion_ItemClick(btnAsignarNroOperacion, new ItemClickEventArgs(null, null));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAsignarNroOperacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraInputBoxArgs args = new XtraInputBoxArgs(); args.Caption = "Ingrese el número de operación";
            TextEdit txtNroOperacion = new TextEdit(); txtNroOperacion.Width = 80;
            txtNroOperacion.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            args.Editor = txtNroOperacion;
            var frm = new XtraInputBoxForm(); var res = frm.ShowInputBoxDialog(args);
            if ((res == DialogResult.OK || res == DialogResult.Yes) && txtNroOperacion.Text.Trim() != "")
            {
                eEmpresa.eDetalleMovimientoBanco_Empresa obj = gvListadoDetalleMovimientos.GetFocusedRow() as eEmpresa.eDetalleMovimientoBanco_Empresa;
                obj.dsc_nro_operacion = txtNroOperacion.Text.Trim();
                eEmpresa.eDetalleMovimientoBanco_Empresa obj2 = unit.Factura.Insertar_Actualizar_NroOperacionDetalleBancoEmpresa<eEmpresa.eDetalleMovimientoBanco_Empresa>(obj);
                obj.dsc_nro_operacion = obj2.dsc_nro_operacion;
                if (obj2 == null) { MessageBox.Show("Error al guardar número de operación.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                XtraMessageBox.Show("Se asignó el número de operación de manera satisfactoria", "Asignar número de operación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tlvListaCuentasBancarias_FocusedRowChanged(tlvListaCuentasBancarias, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(tlvListaCuentasBancarias.FocusedRowHandle - 1, tlvListaCuentasBancarias.FocusedRowHandle));
            }
            else
            {
                MessageBox.Show("Debe ingresar el número de operación", "Asignar número de operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConvertirPorDefecto_Click(object sender, EventArgs e)
        {
            eEmpresa.eBanco_Empresa objBanco = tlvListaCuentasBancarias.GetFocusedRow() as eEmpresa.eBanco_Empresa;
            objBanco.flg_defecto = "SI";
            objBanco = unit.Factura.Insertar_Actualizar_BancoEmpresa<eEmpresa.eBanco_Empresa>(objBanco, "SI");
            int nRow = tlvListaCuentasBancarias.FocusedRowHandle;
            listCuentasBanc = unit.Factura.Obtener_CuentasBancariasEmpresa<eEmpresa.eBanco_Empresa>(1, lkpEmpresa.EditValue.ToString()/*, lkpSedeEmpresa.EditValue.ToString()*/);
            bsListaCuentasBancarias.DataSource = listCuentasBanc;
            tlvListaCuentasBancarias.RefreshData();
            tlvListaCuentasBancarias.FocusedRowHandle = nRow;
            tlvListaCuentasBancarias_FocusedRowChanged(tlvListaCuentasBancarias, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(tlvListaCuentasBancarias.FocusedRowHandle - 1, tlvListaCuentasBancarias.FocusedRowHandle));
        }

        private void btnAdjuntar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmListadoMovimientoBancos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo registros", "Cargando...");
                tlvListaCuentasBancarias_FocusedRowChanged(tlvListaCuentasBancarias, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(tlvListaCuentasBancarias.FocusedRowHandle - 1, tlvListaCuentasBancarias.FocusedRowHandle));
                //SplashScreenManager.CloseForm();
            }
        }

        private void btnExportarFormatoBancos_ItemClick(object sender, ItemClickEventArgs e)
        {
            eEmpresa.eBanco_Empresa objBanc = tlvListaCuentasBancarias.GetFocusedRow() as eEmpresa.eBanco_Empresa;
            eEmpresa.eDetalleMovimientoBanco_Empresa objDet = gvListadoDetalleMovimientos.GetFocusedRow() as eEmpresa.eDetalleMovimientoBanco_Empresa;
            if (objBanc.dsc_cta_contable == null) { MessageBox.Show("La cuenta bancaria no tiene cuenta contable asiganada.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (objDet == null) { MessageBox.Show("Debe seleccionar un registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (objDet.dsc_nro_operacion == null) { MessageBox.Show("Debe asignar el número de operación al movimiento seleccionado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            ExportarReportePagoBancos(objBanc.dsc_cta_contable, objDet.dsc_nro_operacion, objDet.cod_bloque_pago, objDet.num_linea, objDet.cod_empresa);
        }


        private void ExportarReportePagoBancos(string dsc_cta_contable, string dsc_nro_operacion, string cod_bloque_pago, int num_linea, string cod_empresa)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");
            string ListSeparator = "";

            string entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
            string server = unit.Encripta.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
            string bd = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
            string user = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("UserID")].ToString());
            string pass = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("Password")].ToString());
            string AppName = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("AppName")].ToString());

            string cnxl = "ODBC;DRIVER=SQL Server;SERVER=" + server + ";UID=" + user + ";PWD=" + pass + ";APP=SGI_Excel;DATABASE=" + bd + "";
            string procedure = "";

            ListSeparator = ConfigurationManager.AppSettings["ListSeparator"];
            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();
            //objExcel.Visible = true;
            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];

            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exportar_FormatoPagoBancos";
                objExcel.ActiveWindow.DisplayGridlines = false;

                int fila = 0;
                fila = fila + 1;
                procedure = "usp_Reporte_ResumenFormatoPagos @dsc_cta_contable = '" + dsc_cta_contable +
                                                        "', @dsc_nro_operacion = '" + dsc_nro_operacion +
                                                        "', @cod_bloque_pago = '" + cod_bloque_pago +
                                                        "', @num_linea = '" + num_linea +
                                                        "', @cod_empresa = '" + cod_empresa + "'";
                unit.Factura.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + fila, true);
                if (fila > 1) objExcel.Rows[fila].Delete();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

                ////for (int x = 0; x <= bgvProgramacionPagos.RowCount; x++)
                //foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj in listDocumentos)
                //{
                //    //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                //    if (obj == null) continue;
                //    fila = fila + 1;
                //    procedure = "usp_Reporte_ResumenFormatoPagos @cod_bloque_pago = '" + obj.cod_bloque_pago;
                //                                    //"@cod_proveedor = '" + obj.cod_proveedor +
                //                                    //"', @tipo_documento = '" + obj.tipo_documento +
                //                                    //"', @serie_documento = '" + obj.serie_documento +
                //                                    //"', @numero_documento = '" + obj.numero_documento +
                //                                    //"', @cod_correlativoSISPAG = '" + obj.cod_correlativoSISPAG + "'" +
                //                                    //"', @num_linea = " + obj.num_linea +
                //                                    //"', @dsc_glosa_principal = '" + dsc_glosa_principal + "'";
                //    unit.Factura.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + fila, true);
                //    if (fila > 1) objExcel.Rows[fila].Delete();
                //    fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                //    System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                //}

                objExcel.Range["A:A"].Delete();
                objExcel.Range["A1"].Select();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                worksheet.Rows(2).Insert();
                worksheet.Rows(2).Insert();
                fila = fila + 2;
                //int fila = nInLastRow;

                objExcel.Range["A1:AN1"].Select();
                objExcel.Selection.Borders.Color = System.Drawing.Color.FromArgb(0, 0, 0);
                objExcel.Selection.Font.Bold = true;
                objExcel.Selection.Font.Color = System.Drawing.Color.Black;
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFC000");
                objExcel.Range["A1:AN" + fila].Font.Name = "Century Gothic";
                objExcel.Range["A1:AN" + fila].Font.Size = 10;

                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + (fila + 1)].Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN" + fila].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AN1"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                objExcel.Range["A1:AN1"].Borders.Color = System.Drawing.Color.FromArgb(0, 0, 0);

                objExcel.Range["A1"].RowHeight = 70;
                objExcel.Range["A1:AN" + fila].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["A1:AN" + fila].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                objExcel.Range["A1:AN" + fila].WrapText = true;
                //objExcel.Range["A:AR"].ColumnWidth = 18;


                //objExcel.Range["C4"].Value = "Tipo de Solicitud";
                //objExcel.Range["D4"].Value = "Area";

                objExcel.Range["A1:AN1"].AutoFilter(1, Type.Missing, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                objExcel.Range["A1"].Select();

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                System.Threading.Thread.CurrentThread.Abort();
                objExcel.ActiveWorkbook.Saved = true;
                objExcel.ActiveWorkbook.Close();
                objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                objExcel.Quit();
                SplashScreenManager.CloseForm();
                MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvListadoDetalleMovimientos_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                eEmpresa.eDetalleMovimientoBanco_Empresa obj = gvListadoDetalleMovimientos.GetRow(e.RowHandle) as eEmpresa.eDetalleMovimientoBanco_Empresa;
                if (e.Column.FieldName == "cod_tipo_movimiento" && obj.cod_tipo_movimiento == "SALIDA") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                if (e.Column.FieldName == "cod_tipo_movimiento" && obj.cod_tipo_movimiento == "INGRESO") { e.Appearance.ForeColor = Color.Blue; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                if (e.Column.FieldName == "flg_identificado" && obj.flg_identificado == "NO") { e.Appearance.ForeColor = Color.Red; e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold); }
                if (e.Column.FieldName == "imp_monto" && obj.cod_tipo_movimiento == "SALIDA") e.Appearance.ForeColor = Color.Red; 
            }
        }

        private void gvListadoDetalleMovimientos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoDetalleMovimientos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void btnBancoScotiabank_ItemClick(object sender, ItemClickEventArgs e)
        {
            eSistema objVersion = unit.Version.ObtenerVersion<eSistema>(11);

            Process process = new Process();
            process.StartInfo.FileName = objVersion.dsc_valor;
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void btnBancoBBVA_ItemClick(object sender, ItemClickEventArgs e)
        {
            eSistema objVersion = unit.Version.ObtenerVersion<eSistema>(12);

            Process process = new Process();
            process.StartInfo.FileName = objVersion.dsc_valor;
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void btnBancoBCP_ItemClick(object sender, ItemClickEventArgs e)
        {
            eSistema objVersion = unit.Version.ObtenerVersion<eSistema>(13);

            Process process = new Process();
            process.StartInfo.FileName = objVersion.dsc_valor;
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void btnBancoViaBCP_ItemClick(object sender, ItemClickEventArgs e)
        {
            eSistema objVersion = unit.Version.ObtenerVersion<eSistema>(14);

            Process process = new Process();
            process.StartInfo.FileName = objVersion.dsc_valor;
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void btnBancoInterbank_ItemClick(object sender, ItemClickEventArgs e)
        {
            eSistema objVersion = unit.Version.ObtenerVersion<eSistema>(15);

            Process process = new Process();
            process.StartInfo.FileName = objVersion.dsc_valor;
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void btnBancoBanbif_ItemClick(object sender, ItemClickEventArgs e)
        {
            eSistema objVersion = unit.Version.ObtenerVersion<eSistema>(16);

            Process process = new Process();
            process.StartInfo.FileName = objVersion.dsc_valor;
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void btnBancoNacion_ItemClick(object sender, ItemClickEventArgs e)
        {
            eSistema objVersion = unit.Version.ObtenerVersion<eSistema>(17);

            Process process = new Process();
            process.StartInfo.FileName = objVersion.dsc_valor;
            process.StartInfo.Verb = "open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                process.Start();
            }
            catch { }
        }

        private void btnEliminarMovimiento_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (HNG.MessageQuestion("¿Esta seguro de eliminar el movimiento seleccionado?", "Eliminar movimiento") == DialogResult.Yes)
                {
                    //eEmpresa.eBanco_Empresa objBanc = tlvListaCuentasBancarias.GetFocusedRow() as eEmpresa.eBanco_Empresa;
                    eEmpresa.eDetalleMovimientoBanco_Empresa objDet = gvListadoDetalleMovimientos.GetFocusedRow() as eEmpresa.eDetalleMovimientoBanco_Empresa;
                    if (objDet == null) { MessageBox.Show("Debe seleccionar un registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> lista = unit.Factura.Obtener_CuentasBancariasEmpresa<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(3, objDet.cod_empresa, objDet.num_linea, objDet.cod_bloque_pago);
                    foreach (eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objFP in lista)
                    {
                        //cambia ESTADO PAGO a APROBADO del documento
                        Actualizarprogramacionpagofactura(3, objFP.imp_pago, objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor, objFP.num_linea);
                        //actualiza saldo y elimina programacion de pao
                        EliminarProgramaciondepago(objFP.tipo_documento, objFP.serie_documento, objFP.numero_documento, objFP.cod_proveedor, objFP.imp_pago, objFP.num_linea);
                    }
                    string result = unit.Factura.EliminarDatosBancoEmpresa(1, objDet.cod_empresa, objDet.num_linea, objDet.num_item);
                    if (result != "OK") { MessageBox.Show("Error al eliminar registro", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    frmListadoMovimientoBancos_KeyDown(null, new KeyEventArgs(Keys.F5));
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Actualizando documentos", "Cargando...");
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                HNG.MessageError(ex.ToString(), "ERROR");
            }
        }

        private void Actualizarprogramacionpagofactura(int opcion, decimal imp_saldo, string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor, int num_linea = 0)
        {
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objantiguo = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            objantiguo.imp_saldo = imp_saldo;
            objantiguo.tipo_documento = tipo_documento;
            objantiguo.serie_documento = serie_documento;
            objantiguo.numero_documento = numero_documento;
            objantiguo.cod_proveedor = cod_proveedor;
            objantiguo.num_linea = num_linea;
            string resultado;
            resultado = unit.Factura.EliminarProgramacionpagos(opcion, objantiguo.tipo_documento, objantiguo.serie_documento, objantiguo.numero_documento, objantiguo.cod_proveedor, objantiguo.imp_saldo);
        }

        private void EliminarProgramaciondepago(string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor, decimal imp_saldo, int num_linea)
        {
            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objantiguo = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            objantiguo.imp_saldo = imp_saldo;
            objantiguo.tipo_documento = tipo_documento;
            objantiguo.serie_documento = serie_documento;
            objantiguo.numero_documento = numero_documento;
            objantiguo.cod_proveedor = cod_proveedor;
            objantiguo.num_linea = num_linea;
            string resultado;
            //actualiza el SALDO del documento
            resultado = unit.Aprobaciones.EliminarProgramacionpagos(4, objantiguo.tipo_documento, objantiguo.serie_documento, objantiguo.numero_documento, objantiguo.cod_proveedor, objantiguo.imp_saldo, num_linea);

            //cambiar el estado del pago a PROGRAMADO
            resultado = unit.Aprobaciones.EliminarProgramacionpagos(13, objantiguo.tipo_documento, objantiguo.serie_documento, objantiguo.numero_documento, objantiguo.cod_proveedor, 0, num_linea);
        }

    }
}