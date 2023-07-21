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
using DevExpress.XtraSplashScreen;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using BL_BackOffice;
using BE_BackOffice;
using DevExpress.XtraCharts;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraEditors;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid.Columns;

namespace UI_BackOffice.Formularios.Personal
{
    public partial class frmListaControlHoras : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        public eControlHoras ch = new eControlHoras();
        int cod_perfil;
        int perfilAdministrador = 43; int perfilVisualizador = 42; int perfilRegistrador = 41; //PRODUCCION
        //int perfilAdministrador = 38; int perfilVisualizador = 37; int perfilRegistrador = 36; //DESARROLLO
        List<eControlHoras> listControlHoras = new List<eControlHoras>();
        //blControlHoras blControlH = new blControlHoras();
        eVentana oPerfil;
        public frmListaControlHoras()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void btnNuevo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantControlHoras"] != null)
            {
                Application.OpenForms["frmMantControlHoras"].Activate();
            }
            else
            {
                frmMantControlHoras frm = new frmMantControlHoras();
                frm.MiAccion = ControlHoras.Nuevo;
                frm.cod_perfil = cod_perfil;
                frm.ShowDialog();
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                cargarListado();
                SplashScreenManager.CloseForm();
            }
        }

        private void frmListaControlHoras_Load(object sender, EventArgs e)
        {
            Inicializar();
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaInicio.EditValue = oPrimerDiaDelMes;
            dtFechaFin.EditValue = oUltimoDiaDelMes;
            //chkcbUsuario.Enabled = false;
            
            cargarCombo();
            cargarListado();

            //chartControl2.SeriesTemplate.ChangeView(ViewType.StackedBar);
            //chartControl2.SeriesTemplate.SeriesDataMember = "dsc_grupo";
            //chartControl2.SeriesTemplate.SetDataMembers("dsc_segmento", "cnt_horas");

            //chartControl2.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //chartControl2.SeriesTemplate.Label.TextPattern = "${V}M";

            //((BarSeriesLabel)chartControl2.SeriesTemplate.Label).Position = BarSeriesLabelPosition.Center;

            //StackedBarSeriesView view = (StackedBarSeriesView)chartControl2.SeriesTemplate.View;
            //view.BarWidth = 0.8;

            //XYDiagram diagram = (XYDiagram)chartControl2.Diagram;
            //diagram.AxisX.Tickmarks.MinorVisible = false;

            //chartControl2.Titles.Add(new ChartTitle { Text = "Sales by Products" });

            //// Specify legend settings:
            //chartControl2.Legend.MarkerMode = LegendMarkerMode.CheckBoxAndMarker;
            //chartControl2.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            //chartControl2.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            //chartControl2.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
        }

        private void Inicializar()
        {
            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
            oPerfil = listPerfil.Find(x => x.cod_perfil == perfilRegistrador || x.cod_perfil == perfilAdministrador);
            btnNuevo.Enabled = oPerfil != null ? true : false; btnEliminar.Enabled = oPerfil != null ? true : false; btnMultipleRegistros.Enabled = oPerfil != null ? true : false;
            btnCostoHora.Enabled = oPerfil != null && oPerfil.cod_perfil == perfilAdministrador ? true : false; btnExportarExcel.Enabled = oPerfil != null ? true : false; btnImprimir.Enabled = oPerfil != null ? true : false;
            xtbReporte.PageVisible = oPerfil != null && oPerfil.cod_perfil == perfilAdministrador ? true : false; layoutControlItem8.ContentVisible = oPerfil != null && oPerfil.cod_perfil == perfilAdministrador ? true : false; 
            cod_perfil = oPerfil != null ? oPerfil.cod_perfil : perfilVisualizador; btnGestionActividades.Enabled = oPerfil != null && oPerfil.cod_perfil == perfilAdministrador ? true : false;


            if (cod_perfil != perfilAdministrador)
            {
                gvListadoControl.SortInfo.Remove(this.colhoras_fecha_usuario);
                foreach (GridColumn col in gvListadoControl.Columns)
                {
                    if (col.FieldName == "horas_fecha_usuario") { col.Visible = false; }
                }
            }
        }

        public void cargarCombo()
        {
            unit.Requerimiento.CargaCombosChecked("EmpresasUsuarios", chkcbEmpresa, "cod_empresa", "dsc_empresa", "", Program.Sesion.Usuario.cod_usuario);
            unit.Trabajador.CargaCombosChecked("UsuariosControlHoras", chkcbUsuario, "cod_usuario", "dsc_usuario","");
            chkcbEmpresa.CheckAll();
            chkcbUsuario.CheckAll();
        }

        public void cargarListado()
        {
            string codigo; 
            if (layoutControlItem8.ContentVisible == true) { codigo = chkcbUsuario.EditValue.ToString(); }
            else { codigo = Program.Sesion.Usuario.cod_usuario; }
            listControlHoras = unit.Trabajador.ListarControlHoras<eControlHoras>(oPerfil == null || oPerfil.cod_perfil == perfilRegistrador ? 1 : 2, codigo, Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), cod_empresa_multiple: chkcbEmpresa.EditValue == null ? "" : chkcbEmpresa.EditValue.ToString());   
            bsListadoControlHoras.DataSource = listControlHoras;
            gvListadoControl.RefreshData();
            horasTotalesxMes(codigo);


            //gvListadoControl.SortInfo.ClearAndAddRange(new[] {
            //    new GridColumnSortInfo(this.colhoras_fecha, DevExpress.Data.ColumnSortOrder.Ascending)});

            //gvListadoControl.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            //    new GridColumnSortInfo(colhoras_fecha, DevExpress.Data.ColumnSortOrder.Ascending)});
        }

        private void gvListadoControl_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2 && e.RowHandle >= 0)
                {
                    eControlHoras obj = gvListadoControl.GetFocusedRow() as eControlHoras;
                    if (obj == null) return;
                    if (Application.OpenForms["frmMantControlHoras"] != null)
                    {
                        Application.OpenForms["frmMantControlHoras"].Activate();
                    }
                    else
                    {
                        frmMantControlHoras frm = new frmMantControlHoras(this);
                        frm.MiAccion = cod_perfil == perfilRegistrador || cod_perfil == perfilAdministrador ? ControlHoras.Editar : ControlHoras.Vista;
                        frm.cod_perfil = cod_perfil;
                        frm.ch = obj;
                        frm.cod_control_horas = obj.cod_control_horas;
                        frm.ShowDialog();
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                        cargarListado();
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gvListadoControl_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoControl_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            cargarListado();
            SplashScreenManager.CloseForm();
        }

        private void btnEliminar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eControlHoras obj = gvListadoControl.GetFocusedRow() as eControlHoras;
                obj.flg_activo = "NO";
                obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

                DialogResult msgresult = MessageBox.Show("¿Está seguro de eliminar este registro?", "Eliminar registo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msgresult == DialogResult.Yes)
                {
                    ch = unit.Trabajador.InsertarActualizar_ControlHoras<eControlHoras>(obj);
                    MessageBox.Show("Se eliminó el registro de manera correcta.", "Eliminar registo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    cargarListado();
                    SplashScreenManager.CloseForm();
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void frmListaControlHoras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) cargarListado();
        }

        private void btnCostoHora_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantCostoHora"] != null)
            {
                Application.OpenForms["frmMantCostoHora"].Activate();
            }
            else
            {
                frmMantCostoHora frm = new frmMantCostoHora();
                frm.ShowDialog();
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                cargarListado();
                SplashScreenManager.CloseForm();
            }
        }

        private void btnMultipleRegistros_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantControlHorasMultiple"] != null)
            {
                Application.OpenForms["frmMantControlHorasMultiple"].Activate();
            }
            else
            {
                frmMantControlHorasMultiple frm = new frmMantControlHorasMultiple();
                frm.MiAccion = frmMantControlHorasMultiple.ControlHorasMulptiple.Nuevo;
                frm.cod_perfil = cod_perfil;
                frm.ShowDialog();
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                cargarListado();
                SplashScreenManager.CloseForm();
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
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\Personal" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                gvListadoControl.ExportToXlsx(archivo);
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
            gvListadoControl.ShowPrintPreview();
        }

        public void horasTotalesxMes(string codigo)
        {
            List<eControlHoras> horasMes = new List<eControlHoras>();
            horasMes = unit.Trabajador.ListarControlHoras<eControlHoras>(4, codigo, Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"), Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"), cod_empresa_multiple: chkcbEmpresa.EditValue == null ? "" : chkcbEmpresa.EditValue.ToString());
            
            int horas = 0; int minutos = 0;
            for (int i = 0; i < horasMes.Count(); i++)
            {
                horas = Convert.ToInt32((horasMes[i].dsc_duracion.ToString().Split(':')[0]).Substring(0, 2)) + horas;
                minutos = (Convert.ToInt32((horasMes[i].dsc_duracion.ToString().Split(':')[1]).Substring(0, 2)) + minutos);// + Convert.ToInt32((listControlHoras[i].dsc_duracion.ToString().Split(':')[0]).Substring(0, 2)) * 60 + minutos;
            }
            txtTotalHoras.EditValue = Convert.ToString(horas + minutos / 60) + ":" + string.Format("{0:00}", minutos % 60); //Convert.ToString(minutos % 60).ToString("D2");
            txtTotalHoras.Enabled = false;
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(e.Page == xtbReporte)
            {
                chkcbEmpresa.Enabled = false;
                //chkcbUsuario.Enabled = true;
                chkcbEmpresa.CheckAll();
                cargarListado();
            }

            if (e.Page == xtbListado)
            {
                
                chkcbEmpresa.Enabled = true;
                //chkcbUsuario.Enabled = false;
                chkcbUsuario.CheckAll();
                cargarListado();
            }
        }

        private void gvDetalleListado_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvDetalleListado_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvDetalleListado_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
        }

        private void btnGestionActividades_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmListaActividadesGestionHoras"] != null)
            {
                Application.OpenForms["frmListaActividadesGestionHoras"].Activate();
            }
            else
            {
                frmListaActividadesGestionHoras frm = new frmListaActividadesGestionHoras();
                frm.ShowDialog();
                cargarListado();
            }
        }
    }
}