using BE_BackOffice;
using BL_BackOffice;
using DevExpress.XtraEditors;
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
    public partial class frmFacturaPagoBanco : HNG_Tools.ModalForm
    {
        private readonly UnitOfWork unit;
        public List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos> listDocumentos = new List<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>();
        public string GuardarDatos = "NO";

        public frmFacturaPagoBanco()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            ConfigurarFormulario();
        }

        private void frmFacturaPagoBanco_Load(object sender, EventArgs e)
        {
            //if (BusquedaBloquePago) BuscarFacturasxBloquePago();
            bsProgramacionPagos.DataSource = listDocumentos;
            bgvProgramacionPagos.RefreshData();
            this.TitleBackColor = Program.Sesion.Colores.Verde;
            this.TitleForeColor = Color.White;
            dtFechaEjecucion.EditValue = DateTime.Today;
        }
        private void ConfigurarFormulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcProgramacionPagos, bgvProgramacionPagos);
            bgvProgramacionPagos.OptionsBehavior.Editable = true;
            //bgvProgramacionPagos.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            //bgvProgramacionPagos.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            //bgvProgramacionPagos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            //bgvProgramacionPagos.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;

            //this.TitleBackColor = Program.Sesion.Colores.Verde;
        }

        private void frmFacturaPagoBanco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (listDocumentos.FindAll(x => x.cod_correlativoSISPAG == "" || x.cod_correlativoSISPAG == null).Count() > 0) { MessageBox.Show("Debe ingresar la GLOSA PRINCIPAL.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (txtGlosaPrincipal.Text.Trim() == "") { MessageBox.Show("Debe ingresar la GLOSA PRINCIPAL.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (dtFechaEjecucion.EditValue == null) { MessageBox.Show("Debe seleccionar la FECHA EJECUCIÓN.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                if (txtCorrelativo.Text.Trim() == "") { MessageBox.Show("Debe ingresar el CORRELATIVO.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                //glosaPrincipal = txtGlosaPrincipal.Text;
                //fch_ejecucion = Convert.ToDateTime(dtFechaEjecucion.EditValue);

                string cod_correlativoSISPAG = txtCorrelativo.Text.Trim(), dsc_glosa_principal = txtGlosaPrincipal.Text;
                DateTime fch_ejecucion = Convert.ToDateTime(dtFechaEjecucion.EditValue);
                for (int x = 0; x <= bgvProgramacionPagos.RowCount; x++)
                {
                    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj2 = bgvProgramacionPagos.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                    if (obj2 == null) continue;
                    obj2.cod_correlativoSISPAG = cod_correlativoSISPAG;
                    obj2.fch_ejecucion = fch_ejecucion;
                    obj2.dsc_glosa_principal = dsc_glosa_principal;
                }
                bgvProgramacionPagos.RefreshData();
                GuardarDatos = "SI";
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgvProgramacionPagos_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eFacturaProveedor obj = new eFacturaProveedor();
                eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                if (e.Clicks == 1 && e.Column.FieldName == "dsc_documento")
                {
                    obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor;
                    if (obj == null) return;

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.RUC = obj.dsc_ruc;
                    frmModif.tipo_documento = obj.tipo_documento;
                    frmModif.serie_documento = obj.serie_documento;
                    frmModif.numero_documento = obj.numero_documento;
                    frmModif.cod_proveedor = obj.cod_proveedor;
                    frmModif.habilitar_control = "SI";
                    frmModif.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgvProgramacionPagos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle != 0) return;
            //    eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            //    if (obj == null) return;
            //    if (e.Column.FieldName == "cod_correlativoSISPAG")
            //    {
            //        string correlativo = "";  //mes = "",  int num_correlativo = 0;
            //        //mes = obj.cod_correlativoSISPAG.Substring(0, 2);
            //        //correlativo = obj.cod_correlativoSISPAG.Substring(2, 4);
            //        //num_correlativo = Convert.ToInt32(correlativo);
            //        correlativo = obj.cod_correlativoSISPAG;
            //        for (int x = 1; x <= bgvProgramacionPagos.RowCount; x++)
            //        {
            //            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj2 = bgvProgramacionPagos.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            //            if (obj2 == null) continue;
            //            //num_correlativo += 1;
            //            //obj2.cod_correlativoSISPAG = mes + $"{num_correlativo:0000}";
            //            obj2.cod_correlativoSISPAG = correlativo;
            //        }
            //        bgvProgramacionPagos.RefreshData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void txtCorrelativo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string cod_correlativoSISPAG = txtCorrelativo.Text.Trim(); 
                    for (int x = 0; x <= bgvProgramacionPagos.RowCount; x++)
                    {
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj2 = bgvProgramacionPagos.GetRow(x) as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
                        if (obj2 == null) continue;
                        obj2.cod_correlativoSISPAG = cod_correlativoSISPAG;
                    }
                    bgvProgramacionPagos.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bgvProgramacionPagos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void bgvProgramacionPagos_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnasBandHeader(e);
        }

        private void bgvProgramacionPagos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

    }
}