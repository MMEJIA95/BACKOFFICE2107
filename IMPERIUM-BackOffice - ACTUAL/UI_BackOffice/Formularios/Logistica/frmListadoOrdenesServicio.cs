﻿using BE_BackOffice;
using BL_BackOffice;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
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
using UI_BackOffice.Formularios.Shared;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmListadoOrdenesServicio : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        TaskScheduler scheduler;
        String codigoProveedor = "";
        bool isRunning = false;

        public frmListadoOrdenesServicio()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListadoOrdenesServicio_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                btnNuevaOrdServ.Visibility = BarItemVisibility.Never;

                CargarLookUpEdit();
                
                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtpDesde.EditValue = oPrimerDiaDelMes;
                dtpHasta.EditValue = oUltimoDiaDelMes;
                HabilitarBotones();
                BuscarOrdenesServicio();
                tcOrdenServicio_SelectedPageChanged(tcOrdenServicio, new DevExpress.XtraTab.TabPageChangedEventArgs(null, tpOrdenesGeneradas));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.Sesion.Global.Solucion);
            if (listPermisos.Count > 0)
            {
                grupoEdicion.Enabled = listPermisos[0].flg_escritura;
                grupoAcciones.Enabled = listPermisos[0].flg_escritura;
            }
        }

        private void CargarLookUpEdit()
        {
            try
            {
                unit.OrdenCompra_Servicio.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
                unit.OrdenCompra_Servicio.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);

                List<eFacturaProveedor> list = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;

                unit.OrdenCompra_Servicio.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());

                lkpTipoFecha.ItemIndex = 0;
                lkpSede.EditValue = "00001";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void BuscarOrdenesServicio()
        {
            try
            {
                List<eOrdenCompra_Servicio> ordGeneradas = unit.OrdenCompra_Servicio.ListarOrdenesCompra<eOrdenCompra_Servicio>(1, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "S");

                List<eOrdenCompra_Servicio> ordAprobadas = unit.OrdenCompra_Servicio.ListarOrdenesCompra<eOrdenCompra_Servicio>(4, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "S");

                List<eOrdenCompra_Servicio> ordEnviadas = unit.OrdenCompra_Servicio.ListarOrdenesCompra<eOrdenCompra_Servicio>(7, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "S");

                List<eOrdenCompra_Servicio> ordAtendidas = unit.OrdenCompra_Servicio.ListarOrdenesCompra<eOrdenCompra_Servicio>(5, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "S");

                List<eOrdenCompra_Servicio> ordLiquidadas = unit.OrdenCompra_Servicio.ListarOrdenesCompra<eOrdenCompra_Servicio>(6, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "S");

                List<eOrdenCompra_Servicio> ordAnuladas = unit.OrdenCompra_Servicio.ListarOrdenesCompra<eOrdenCompra_Servicio>(9, lkpEmpresa.EditValue.ToString(),
                                                                                      lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                      txtProveedor.EditValue == null ? "" : codigoProveedor,
                                                                                      lkpTipoFecha.EditValue.ToString(),
                                                                                      Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                      Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd"),
                                                                                      "S");

                bsListadoOrdGeneradas.DataSource = ordGeneradas;
                bsListadoOrdAprobadas.DataSource = ordAprobadas;
                bsListadoOrdEnviadas.DataSource = ordEnviadas;
                bsListadoOrdAtendidas.DataSource = ordAtendidas;
                bsListadoOrdLiquidadas.DataSource = ordLiquidadas;
                bsListadoOrdAnuladas.DataSource = ordAnuladas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNuevaOrdServ_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantOrdenCompra frm = new frmMantOrdenCompra();
            frm.codigoEmpresa = lkpEmpresa.EditValue.ToString();
            frm.ShowDialog();
        }

        private void btnAnularOrdServ_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdGeneradas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdGeneradas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = unit.OrdenCompra_Servicio.Anular_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, Program.Sesion.Usuario.cod_usuario);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Anulación realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Anular los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesServicio();
        }

        private void btnEliminarOrdServ_ItemClick(object sender, ItemClickEventArgs e)
        {

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
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\RequerimientosOC" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                
                if (!Directory.Exists(carpeta)) Directory.CreateDirectory(carpeta);
                
                switch (tcOrdenServicio.SelectedTabPage.Name)
                {
                    case "tpOrdenesGeneradas": gvOrdGeneradas.ExportToXlsx(archivo); break;
                    case "tpOrdenesAprobadas": gvOrdAprobadas.ExportToXlsx(archivo); break;
                    case "tpOrdenesEnviadas": gvOrdEnviadas.ExportToXlsx(archivo); break;
                    case "tpOrdenesAtendidas": gvOrdAtendidas.ExportToXlsx(archivo); break;
                    case "tpOrdenesLiquidadas": gvOrdLiquidadas.ExportToXlsx(archivo); break;
                    case "tpOrdenesAnuladas": gvOrdAnuladas.ExportToXlsx(archivo); break;
                }
                
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
            switch (tcOrdenServicio.SelectedTabPage.Name)
            {
                case "tpOrdenesGeneradas": gvOrdGeneradas.ShowPrintPreview(); break;
                case "tpOrdenesAprobadas": gvOrdAprobadas.ShowPrintPreview(); break;
                case "tpOrdenesEnviadas": gvOrdEnviadas.ShowPrintPreview(); break;
                case "tpOrdenesAtendidas": gvOrdAtendidas.ShowPrintPreview(); break;
                case "tpOrdenesLiquidadas": gvOrdLiquidadas.ShowPrintPreview(); break;
                case "tpOrdenesAnuladas": gvOrdAnuladas.ShowPrintPreview(); break;
            }
        }

        private void btnReporteOrdenServicio_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                eOrdenCompra_Servicio eProv = new eOrdenCompra_Servicio();
                switch (tcOrdenServicio.SelectedTabPage.Name)
                {
                    case "tpOrdenesGeneradas": eProv = gvOrdGeneradas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                    case "tpOrdenesAprobadas": eProv = gvOrdAprobadas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                    case "tpOrdenesEnviadas": eProv = gvOrdEnviadas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                    case "tpOrdenesAtendidas": eProv = gvOrdAtendidas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                    case "tpOrdenesLiquidadas": eProv = gvOrdLiquidadas.GetFocusedRow() as eOrdenCompra_Servicio; break;
                }
                if (eProv == null) { return; }

                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");

                if (eProv.cod_empresa == "00004")
                {
                    rptOrdenServicio report = new rptOrdenServicio();

                    ReportPrintTool printTool = new ReportPrintTool(report);
                    report.RequestParameters = false;
                    printTool.AutoShowParametersPanel = false;
                    report.Parameters["cod_almacen"].Value = eProv.cod_almacen;
                    report.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                    report.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;
                    report.Parameters["cod_proveedor"].Value = eProv.cod_proveedor;
                    report.Parameters["cod_orden_compra_servicio"].Value = eProv.cod_orden_compra_servicio;
                    report.xpb_logo.Image = Properties.Resources.logo_k2;
                    printTool.ShowPreviewDialog();
                    SplashScreenManager.CloseForm();
                }
                else
                {
                    rptOrdenServicio report1 = new rptOrdenServicio();

                    ReportPrintTool printTool1 = new ReportPrintTool(report1);
                    report1.RequestParameters = false;
                    printTool1.AutoShowParametersPanel = false;
                    report1.Parameters["cod_almacen"].Value = eProv.cod_almacen;
                    report1.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                    report1.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;
                    report1.Parameters["cod_proveedor"].Value = eProv.cod_proveedor;
                    report1.Parameters["cod_orden_compra_servicio"].Value = eProv.cod_orden_compra_servicio;

                    if (eProv.cod_empresa == "00001")
                    {
                        report1.xpb_logo.Image = Properties.Resources.Logo_HNG1;


                    }
                    if (eProv.cod_empresa == "00002")
                    {
                        report1.xpb_logo.Image = Properties.Resources.logo_facilita;

                    }
                    if (eProv.cod_empresa == "00003") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00005")
                    {
                        report1.xpb_logo.Image = Properties.Resources.logo_facilita;
                        report1.tbcantidad.BackColor = Color.FromArgb(13, 52, 108);
                        report1.tbdescripcion.BackColor = Color.FromArgb(13, 52, 108);
                        report1.tbitem.BackColor = Color.FromArgb(13, 52, 108);
                        report1.tbpreciototal.BackColor = Color.FromArgb(13, 52, 108);
                        report1.tbpreciou.BackColor = Color.FromArgb(13, 52, 108);
                        report1.tbarea1.BackColor = Color.FromArgb(174, 187, 206);
                        report1.tbarea2.BackColor = Color.FromArgb(174, 187, 206);
                    }

                    if (eProv.cod_empresa == "00006") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00007") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00008") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00009") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    if (eProv.cod_empresa == "00010") { report1.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                    printTool1.ShowPreviewDialog();
                    SplashScreenManager.CloseForm();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAprobar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdGeneradas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdGeneradas.GetRow(nRow) as eOrdenCompra_Servicio;

                    if (obj.dsc_unidad_recepcion == null)
                    {
                        MessageBox.Show("Debe ingresar una unidad de recepción a la orden " + obj.cod_orden_compra_servicio + ".", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (obj.dsc_direccion_despacho == null)
                    {
                        MessageBox.Show("Debe ingresar una dirección de despacho a la orden " + obj.cod_orden_compra_servicio + ".", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (obj.fch_despacho == null)
                    {
                        MessageBox.Show("Debe ingresar una fecha de despacho a la orden " + obj.cod_orden_compra_servicio + ".", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        respuesta = unit.OrdenCompra_Servicio.Aprobar_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, Program.Sesion.Usuario.cod_usuario);
                    }
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Aprobación realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Aprobar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesServicio();
        }

        private void btnDesaprobar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdAprobadas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdAprobadas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = unit.OrdenCompra_Servicio.Desaprobar_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, Program.Sesion.Usuario.cod_usuario);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Desaprobación realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Desaprobar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesServicio();
        }

        private void btnEnviar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdAprobadas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdAprobadas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = unit.OrdenCompra_Servicio.Enviar_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, Program.Sesion.Usuario.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Envío realizado con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Enviar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesServicio();
        }

        private void btnAtender_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdEnviadas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdEnviadas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = unit.OrdenCompra_Servicio.Atender_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, Program.Sesion.Usuario.cod_usuario);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Atención realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Atender los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            BuscarOrdenesServicio();
        }

        private void btnLiquidar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvOrdAtendidas.GetSelectedRows())
                {
                    eOrdenCompra_Servicio obj = gvOrdAtendidas.GetRow(nRow) as eOrdenCompra_Servicio;

                    respuesta = unit.OrdenCompra_Servicio.Liquidar_Orden(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_orden_compra_servicio, obj.flg_solicitud, obj.dsc_anho, Program.Sesion.Usuario.cod_usuario);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Liquidación realizada con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Liquidar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarOrdenesServicio();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarOrdenesServicio();
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            Busqueda("", "Proveedor");
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;
            
            switch (tipo)
            {
                case "Proveedor":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Proveedor":
                    codigoProveedor = frm.codigo;
                    txtProveedor.Text = frm.descripcion;
                    break;
            }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Proveedor");
            }
            string dato = unit.Globales.pKeyPress(txtProveedor, e);
            if (dato != "")
            {
                Busqueda(dato, "Proveedor");
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            unit.OrdenCompra_Servicio.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
        }

        private void frmListadoOrdenesServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) BuscarOrdenesServicio();
        }

        private void tcOrdenServicio_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
            eVentana oPerfilAdm = listPerfil.Find(x => x.cod_perfil == 40 || x.cod_perfil == 5);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 40 || x.cod_perfil == 5 || x.cod_perfil == 38);

            if (tcOrdenServicio.SelectedTabPage == tpOrdenesGeneradas)
            {
                btnAnularOrdServ.Enabled = oPerfilAdm != null ? true : false;
                btnReporteOrdenServicio.Enabled = false;
                btnAprobar.Enabled = oPerfilAdm != null ? true : false;
                btnDesaprobar.Enabled = false;
                btnEnviar.Enabled = false;
                btnAtender.Enabled = false;
                btnLiquidar.Enabled = false;
            }
            else if (tcOrdenServicio.SelectedTabPage == tpOrdenesAprobadas)
            {
                btnAnularOrdServ.Enabled = false;
                btnReporteOrdenServicio.Enabled = true;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = oPerfilAdm != null ? true : false;
                btnEnviar.Enabled = oPerfilLog != null ? true : false;
                btnAtender.Enabled = false;
                btnLiquidar.Enabled = false;
            }
            else if (tcOrdenServicio.SelectedTabPage == tpOrdenesEnviadas)
            {
                btnAnularOrdServ.Enabled = false;
                btnReporteOrdenServicio.Enabled = true;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = false;
                btnEnviar.Enabled = false;
                btnAtender.Enabled = oPerfilLog != null ? true : false; ;
                btnLiquidar.Enabled = false;
            }
            else if (tcOrdenServicio.SelectedTabPage == tpOrdenesAtendidas)
            {
                btnAnularOrdServ.Enabled = false;
                btnReporteOrdenServicio.Enabled = true;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = false;
                btnEnviar.Enabled = false;
                btnAtender.Enabled = false;
                btnLiquidar.Enabled = oPerfilLog != null ? true : false; ;
            }
            else
            {
                btnAnularOrdServ.Enabled = false;
                btnReporteOrdenServicio.Enabled = tcOrdenServicio.SelectedTabPage == tpOrdenesLiquidadas ? true : false;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = false;
                btnEnviar.Enabled = false;
                btnAtender.Enabled = false;
                btnLiquidar.Enabled = false;
            }
        }

        private void gvOrdGeneradas_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (isRunning) return;

            isRunning = true;

            GridView View = sender as GridView;

            if (e.Action == CollectionChangeAction.Add && (string)this.gvOrdGeneradas.GetRowCellValue(this.gvOrdGeneradas.FocusedRowHandle, "cod_estado_orden") == "LIQUIDADO")
            {
                View.UnselectRow(e.ControllerRow);
            }

            if (e.Action == CollectionChangeAction.Refresh && View.SelectedRowsCount > 0)
            {
                View.BeginSelection();

                foreach (int Row in View.GetSelectedRows())
                {
                    if ((string)this.gvOrdGeneradas.GetRowCellValue(Row, "cod_estado_orden") == "LIQUIDADO")
                    {
                        View.UnselectRow(Row);
                    }
                }

                View.EndSelection();
            }

            isRunning = false;
        }

        private void gvOrdGeneradas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesGeneradas");
        }

        private void gvOrdGeneradas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdGeneradas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdGeneradas_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvOrdGeneradas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOrdAprobadas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesAprobadas");
        }

        private void gvOrdAprobadas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdAprobadas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdAprobadas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdAprobadas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOrdEnviadas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesEnviadas");
        }

        private void gvOrdEnviadas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdEnviadas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdEnviadas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdEnviadas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOrdAtendidas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesAtendidas");
        }

        private void gvOrdAtendidas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdAtendidas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdAtendidas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdAtendidas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvOrdLiquidadas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesLiquidadas");
        }

        private void gvOrdLiquidadas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdLiquidadas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdLiquidadas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdLiquidadas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }
        private void gvOrdAnuladas_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC("tpOrdenesAnuladas");
        }

        private void gvOrdAnuladas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvOrdAnuladas_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdAnuladas_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvOrdAnuladas_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void MostrarOC(string tabName)
        {
            eOrdenCompra_Servicio obj = new eOrdenCompra_Servicio();
            frmMantOrdenServicio frm = new frmMantOrdenServicio();
            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 40 || x.cod_perfil == 5 || x.cod_perfil == 38);

            switch (tabName)
            {
                case "tpOrdenesGeneradas": 
                    obj = gvOrdGeneradas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = oPerfilLog != null ? OrdenServicio.Editar : OrdenServicio.Vista;
                    break;
                case "tpOrdenesAprobadas": 
                    obj = gvOrdAprobadas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenServicio.Vista;
                    break;
                case "tpOrdenesEnviadas":
                    obj = gvOrdEnviadas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenServicio.Vista;
                    break;
                case "tpOrdenesAtendidas": 
                    obj = gvOrdAtendidas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenServicio.Vista;
                    break;
                case "tpOrdenesLiquidadas": 
                    obj = gvOrdLiquidadas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenServicio.Vista;
                    break;
                case "tpOrdenesAnuladas":
                    obj = gvOrdAnuladas.GetFocusedRow() as eOrdenCompra_Servicio;
                    frm.accion = OrdenServicio.Vista;
                    break;
            }

            frm.empresa = obj.cod_empresa;
            frm.sede = obj.cod_sede_empresa;
            frm.ordenCompraServicio = obj.cod_orden_compra_servicio;
            frm.solicitud = obj.flg_solicitud;
            frm.anho = obj.dsc_anho;
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();

            BuscarOrdenesServicio();
        }
    }
}