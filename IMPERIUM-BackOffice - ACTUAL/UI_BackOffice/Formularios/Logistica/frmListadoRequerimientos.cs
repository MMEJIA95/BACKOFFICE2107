using BE_BackOffice;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UI_BackOffice.Formularios.Shared;
using UI_BackOffice.Tools;
using UI_BackOffice.Tools.Interfaces;
using UI_BackOffice.Tools.OneDriveServices;
using UI_BackOffice.Tools.OneDriveServices.DTOs;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmListadoRequerimientos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        private readonly IOneDriveService<RequerimientoAdjuntarDTO, RequerimientoDescargarDTO> oneDrive;
        TaskScheduler scheduler;
        String codigoCliente = "";
        bool isRunning = false;

        public frmListadoRequerimientos()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            oneDrive = new RequerimientoOneDriveService(unit);
            crearClearCliente();
        }

        System.Windows.Forms.Button _clearCliente;
        private void crearClearCliente()
        {
            _clearCliente = new System.Windows.Forms.Button()
            {
                Text = "",
                Dock = DockStyle.Right,
                Width = 24,
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Gainsboro,
                Visible = false
            };
            _clearCliente.Image = Properties.Resources.multiply_10px;
            _clearCliente.FlatAppearance.BorderSize = 1;
            _clearCliente.FlatAppearance.BorderColor = Color.FromArgb(248);
            _clearCliente.Click += _clearCliente_Click;
            _clearCliente.BringToFront();
            txtCliente.Controls.Add(_clearCliente);
        }
        private void _clearCliente_Click(object sender, EventArgs e)
        {
            codigoCliente = "";
            txtCliente.Text = "";
        }

        private void frmListadoRequerimientos_Load(object sender, EventArgs e)
        {
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Inicializar();
        }

        private void Inicializar()
        {
            try
            {
                CargarLookUpEdit();

                DateTime date = DateTime.Now;
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
                dtpDesde.EditValue = oPrimerDiaDelMes;
                dtpHasta.EditValue = oUltimoDiaDelMes;
                HabilitarBotones();
                BuscarRequerimientos();
                tcRequerimientos_SelectedPageChanged(tcRequerimientos, new DevExpress.XtraTab.TabPageChangedEventArgs(null, tpReqSolicitados));
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
                unit.Requerimiento.CargaCombosLookUp("EmpresasUsuarios", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true, cod_usuario: Program.Sesion.Usuario.cod_usuario);
                unit.Requerimiento.CargaCombosLookUp("TipoFecha", lkpTipoFecha, "cod_tipo_fecha", "dsc_tipo_fecha", "", valorDefecto: true);

                List<eFacturaProveedor> list = unit.Proveedores.ListarEmpresasProveedor<eFacturaProveedor>(11, "", Program.Sesion.Usuario.cod_usuario);
                if (list.Count >= 1) lkpEmpresa.EditValue = list[0].cod_empresa;

                unit.Requerimiento.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
                List<eTrabajador.eInfoLaboral_Trabajador> lista = unit.Trabajador.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, lkpEmpresa.EditValue.ToString());
                if (lista.Count == 1) lkpSede.EditValue = lista[0].cod_sede_empresa;

                lkpTipoFecha.ItemIndex = 0;
                lkpSede.EditValue = "00001";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void BuscarRequerimientos()
        {
            try
            {
                List<eRequerimiento> reqCompletos = unit.Requerimiento.ListarRequerimiento<eRequerimiento>(10, lkpEmpresa.EditValue.ToString(),
                                                                                          lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                                                                                          txtCliente.EditValue == null ? "" : codigoCliente,
                                                                                          lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString(),
                                                                                          lkpTipoFecha.EditValue.ToString(),
                                                                                          Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                                                                                          Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd")
                                                                                          );
                List<eRequerimiento> reqSolicitados = reqCompletos.FindAll(x => x.cod_estado_requerimiento == "SOLICITADO");
                List<eRequerimiento> reqAprobados = reqCompletos.FindAll(x => x.cod_estado_requerimiento == "APROBADO" || x.cod_estado_requerimiento == "ORDEN GENERADA");
                List<eRequerimiento> reqAtendidos = reqCompletos.FindAll(x => x.cod_estado_requerimiento == "ATENDIDO");
                List<eRequerimiento> reqAnulados = reqCompletos.FindAll(x => x.cod_estado_requerimiento == "ANULADO");

                //List<eRequerimiento> reqSolicitados = unit.Requerimiento.ListarRequerimiento<eRequerimiento>(1, lkpEmpresa.EditValue.ToString(),
                //                                                                          lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                //                                                                          txtCliente.EditValue == null ? "" : codigoCliente,
                //                                                                          lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString(),
                //                                                                          lkpTipoFecha.EditValue.ToString(),
                //                                                                          Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                //                                                                          Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd")
                //                                                                          );

                //List<eRequerimiento> reqAprobados = unit.Requerimiento.ListarRequerimiento<eRequerimiento>(3, lkpEmpresa.EditValue.ToString(),
                //                                                                          lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                //                                                                          txtCliente.EditValue == null ? "" : codigoCliente,
                //                                                                          lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString(),
                //                                                                          lkpTipoFecha.EditValue.ToString(),
                //                                                                          Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                //                                                                          Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd")
                //                                                                          );

                //List<eRequerimiento> reqAtendidos = unit.Requerimiento.ListarRequerimiento<eRequerimiento>(7, lkpEmpresa.EditValue.ToString(),
                //                                                                          lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                //                                                                          txtCliente.EditValue == null ? "" : codigoCliente,
                //                                                                          lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString(),
                //                                                                          lkpTipoFecha.EditValue.ToString(),
                //                                                                          Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                //                                                                          Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd")
                //                                                                          );

                //List<eRequerimiento> reqAnulados = unit.Requerimiento.ListarRequerimiento<eRequerimiento>(9, lkpEmpresa.EditValue.ToString(),
                //                                                                          lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString(),
                //                                                                          txtCliente.EditValue == null ? "" : codigoCliente,
                //                                                                          lkpArea.EditValue == null ? "" : lkpArea.EditValue.ToString(),
                //                                                                          lkpTipoFecha.EditValue.ToString(),
                //                                                                          Convert.ToDateTime(dtpDesde.EditValue).ToString("yyyyMMdd"),
                //                                                                          Convert.ToDateTime(dtpHasta.EditValue).ToString("yyyyMMdd")
                //                                                                          );

                bsListadoReqCompletos.DataSource = reqCompletos;
                bsListadoReqSolicitados.DataSource = reqSolicitados;
                bsListadoReqAprobados.DataSource = reqAprobados;
                bsListadoReqAtendidos.DataSource = reqAtendidos;
                bsListadoReqAnulados.DataSource = reqAnulados;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNuevoRequerimiento_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantRequerimientosCompra frm = new frmMantRequerimientosCompra(ParentFormType.ListadoRequerimientos);
            frm.codigoEmpresa = lkpEmpresa.EditValue.ToString();
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();

            BuscarRequerimientos();
        }

        private void btnNuevoReqServicio_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantRequerimientosServicio frm = new frmMantRequerimientosServicio();
            frm.codigoEmpresa = lkpEmpresa.EditValue.ToString();
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();

            BuscarRequerimientos();
        }

        private void btnAnularRequerimiento_ItemClick(object sender, ItemClickEventArgs e)
        {
            string respuesta = "";

            try
            {
                foreach (int nRow in gvReqSolicitados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqSolicitados.GetRow(nRow) as eRequerimiento;

                    respuesta = unit.Requerimiento.Anular_Requerimiento(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, Program.Sesion.Usuario.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Anulación realizada con éxito.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Anular los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarRequerimientos();
        }

        private void btnEliminarRequerimiento_ItemClick(object sender, ItemClickEventArgs e)
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

                if (!System.IO.Directory.Exists(carpeta)) System.IO.Directory.CreateDirectory(carpeta);

                switch (tcRequerimientos.SelectedTabPage.Name)
                {
                    case "tpReqSolicitados": gvReqSolicitados.ExportToXlsx(archivo); break;
                    case "tpReqAprobados": gvReqAprobados.ExportToXlsx(archivo); break;
                    case "tpReqAtendidos": gvReqAtendidos.ExportToXlsx(archivo); break;
                    case "tpReqAnulados": gvReqAnulados.ExportToXlsx(archivo); break;
                    default: return;
                }

                if (MessageBox.Show("Excel exportado en la ruta " + archivo + Environment.NewLine + "¿Desea abrir el archivo?", "Exportar Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImprimir_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (tcRequerimientos.SelectedTabPage.Name)
            {
                case "tpReqSolicitados": gvReqSolicitados.ShowPrintPreview(); break;
                case "tpReqAprobados": gvReqAprobados.ShowPrintPreview(); break;
                case "tpReqAtendidos": gvReqAtendidos.ShowPrintPreview(); break;
                case "tpReqAnulados": gvReqAnulados.ShowPrintPreview(); break;
            }
        }

        internal void btnAprobar_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Restablecer: flg_aprobador NO para  JAIRO y DANIEL BENITEZ, PGAMARRA
            if (Program.Sesion.Usuario.flg_administrador.Equals("NO") && string.IsNullOrEmpty(Program.Sesion.Usuario.flg_aprobador))
            {
                HNG.MessageWarning("Esta opción es para los usuarios con el perfil de Administrador.", "Aprobación de Requerimientos");
                return;
            }


            int conProv = 0;
            int sinProv = 0;
            string respuesta = "";

            List<eRequerimiento.eRequerimiento_Detalle> eDetRequerimiento;
            string noAprob = "";

            try
            {
                foreach (int nRow in gvReqSolicitados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqSolicitados.GetRow(nRow) as eRequerimiento;

                    if (obj.flg_solicitud == "COMPRA")
                    {
                        eDetRequerimiento = unit.Requerimiento.Cargar_Detalle_Requerimiento<eRequerimiento.eRequerimiento_Detalle>(5, obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, obj.flg_solicitud.ToString().Substring(0, 1), obj.dsc_anho);

                        if (eDetRequerimiento.FindAll(x => x.flg_con_proveedor == "SI").Count > 0)
                        {
                            sinProv = eDetRequerimiento.FindAll(x => x.flg_con_proveedor == "SI" && x.cod_proveedor == null).Count;
                        }

                        //for (int i = 0; i < eDetRequerimiento.Count; i++)
                        //{
                        //    if (eDetRequerimiento[i].imp_total > 0)
                        //    {
                        //        conProv = conProv + 1;
                        //    }
                        //    else
                        //    {
                        //        sinProv = sinProv + 1;
                        //    }
                        //}

                        if (sinProv == 0)
                        {
                            respuesta = unit.Requerimiento.Aprobar_Requerimiento(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, Program.Sesion.Usuario.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                        }
                        else
                        {
                            noAprob = obj.cod_requerimiento + "," + noAprob;
                        }

                        conProv = 0;
                        sinProv = 0;
                    }
                    else
                    {
                        eDetRequerimiento = unit.Requerimiento.Cargar_Detalle_Requerimiento<eRequerimiento.eRequerimiento_Detalle>(5, obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, obj.flg_solicitud.ToString().Substring(0, 1), obj.dsc_anho);

                        for (int i = 0; i < eDetRequerimiento.Count; i++)
                        {
                            if (eDetRequerimiento[i].imp_unitario > 0 && eDetRequerimiento[i].cod_proveedor != "PR000000")
                            {
                                conProv = conProv + 1;
                            }
                            else
                            {
                                sinProv = sinProv + 1;
                            }
                        }

                        if (sinProv == 0)
                        {
                            respuesta = unit.Requerimiento.Aprobar_Requerimiento(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, Program.Sesion.Usuario.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                        }
                        else
                        {
                            noAprob = obj.cod_requerimiento + "," + noAprob;
                        }

                        conProv = 0;
                        sinProv = 0;
                    }
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Aprobación realizada con éxito.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (noAprob != "")
                {
                    MessageBox.Show("Los siguientes requerimientos no cuentan con un proveedor y/o un precio asignado " + noAprob, "Requerimientos No Aprobados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Aprobar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarRequerimientos();
        }

        private void btnDesaprobar_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Program.Sesion.Usuario.flg_administrador.Equals("NO"))
            {
                HNG.MessageWarning("Esta opción es para los usuarios con el perfil de Administrador.", "Desaprobar Requerimientos");
                return;
            }

            string respuesta = "";

            try
            {
                foreach (int nRow in gvReqAprobados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqAprobados.GetRow(nRow) as eRequerimiento;

                    respuesta = unit.Requerimiento.Desaprobar_Requerimiento(obj.cod_empresa, obj.cod_sede_empresa, obj.cod_requerimiento, Program.Sesion.Usuario.cod_usuario, obj.flg_solicitud, obj.dsc_anho);
                }

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Desaprobación realizada con éxito.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Desaprobar los Documentos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarRequerimientos();
        }

        private void btnGenerarOC_ItemClick(object sender, ItemClickEventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando Orden de Compra", "Cargando...");

            string empresa = "";
            string sede = "";
            string solicitud = "";
            Int32 anho = 1;
            string requerimientos = "";

            try
            {
                RequerimientosHelper helper = new RequerimientosHelper(unit);
                //LLENA DATOS DE CABECERA PARA LA OC
                foreach (int nRow in gvReqAprobados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqAprobados.GetRow(nRow) as eRequerimiento;

                    if (obj.flg_solicitud == "COMPRA")
                    {
                        empresa = obj.cod_empresa;
                        sede = obj.cod_sede_empresa;
                        solicitud = obj.flg_solicitud;
                        anho = obj.dsc_anho;
                        requerimientos = obj.cod_requerimiento + "," + requerimientos;

                    }
                }

                string cod_orden_compra = "", flg_solicitud = "";
                Int32 dsc_anho = 1;

                //TRAE DATOS DEL PROVEEDOR Y PRODUCTOS DE LOS RQ SELECCIONADOS
                List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> prodProvReq;
                prodProvReq = unit.Requerimiento.Cargar_Prod_Prov_Requerimientos<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(6, empresa, sede, requerimientos, solicitud, anho)
                    .Where((sol) => sol.flg_solicitaOC.Equals("NO")).ToList(); // Se agrega esta validación para obtener solo los productos que no se han solicitado.

                //SE COMIENZA A REVISAR LA OC
                for (int i = 0; i < prodProvReq.Count; i++)
                {
                    //(REVISAR)
                    if (i == 0)
                    {


                        //INSERTA CABECERA DE LA 1RA OC
                        eOrdenCompra_Servicio eOrdCom = helper.CreaCabecera(prodProvReq[i], "C");

                        cod_orden_compra = eOrdCom.cod_orden_compra_servicio;
                        flg_solicitud = eOrdCom.flg_solicitud;
                        dsc_anho = eOrdCom.dsc_anho;

                        prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                        prodProvReq[i].flg_solicitud = flg_solicitud;
                        prodProvReq[i].dsc_anho = dsc_anho;

                        //INSERTA DETALLE DE LA 1RA OC...
                        eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = helper.CrearDetalle(prodProvReq[i]);
                    }
                    else
                    {
                        //VALIDA PROVEEDOR PARA GENERAR DISTINTAS OC
                        if (prodProvReq[i].cod_proveedor != prodProvReq[i - 1].cod_proveedor)
                        {
                            //SI ES DIFERENTE SE CREA NUEVA CABECERA
                            eOrdenCompra_Servicio eOrdCom = helper.CreaCabecera(prodProvReq[i], "C");

                            cod_orden_compra = eOrdCom.cod_orden_compra_servicio;
                            flg_solicitud = eOrdCom.flg_solicitud;
                            dsc_anho = eOrdCom.dsc_anho;

                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            //INSERTA DETALLE DE LA OC
                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = helper.CrearDetalle(prodProvReq[i]);
                        }
                        else
                        {
                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_compra;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            //INSERTA DETALLE DE LA OC
                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = helper.CrearDetalle(prodProvReq[i]);
                        }
                    }
                }
                //CAMBIO DE ESTADO DEL RQ
                string respuesta = unit.Requerimiento.GenerarOC_Requerimiento(empresa, sede, requerimientos, Program.Sesion.Usuario.cod_usuario, solicitud, anho);

                SplashScreenManager.CloseForm();

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Órdenes generadas con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show("Error al Crear Órdenes.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BuscarRequerimientos();
        }

        private void btnGenerarOS_ItemClick(object sender, ItemClickEventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Generando Orden de Servicio", "Cargando...");

            string empresa = "";
            string sede = "";
            string solicitud = "";
            Int32 anho = 1;
            string requerimientos = "";

            try
            {
                RequerimientosHelper helper = new RequerimientosHelper(unit);

                foreach (int nRow in gvReqAprobados.GetSelectedRows())
                {
                    eRequerimiento obj = gvReqAprobados.GetRow(nRow) as eRequerimiento;

                    if (obj.flg_solicitud == "SERVICIO")
                    {
                        empresa = obj.cod_empresa;
                        sede = obj.cod_sede_empresa;
                        solicitud = obj.flg_solicitud;
                        anho = obj.dsc_anho;
                        requerimientos = obj.cod_requerimiento + "," + requerimientos;
                    }
                }

                string cod_orden_servicio = "", flg_solicitud = "";
                Int32 dsc_anho = 1;

                List<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle> prodProvReq;

                prodProvReq = unit.Requerimiento.Cargar_Prod_Prov_Requerimientos<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(8, empresa, sede, requerimientos, solicitud, anho);

                for (int i = 0; i < prodProvReq.Count; i++)
                {
                    if (i == 0)
                    {
                        eOrdenCompra_Servicio eOrdCom = helper.CreaCabecera(prodProvReq[i], "S");

                        cod_orden_servicio = eOrdCom.cod_orden_compra_servicio;
                        flg_solicitud = eOrdCom.flg_solicitud;
                        dsc_anho = eOrdCom.dsc_anho;

                        prodProvReq[i].cod_orden_compra_servicio = cod_orden_servicio;
                        prodProvReq[i].flg_solicitud = flg_solicitud;
                        prodProvReq[i].dsc_anho = dsc_anho;

                        eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = helper.CrearDetalle(prodProvReq[i]);
                    }
                    else
                    {
                        if (prodProvReq[i].cod_proveedor != prodProvReq[i - 1].cod_proveedor)
                        {
                            eOrdenCompra_Servicio eOrdCom = helper.CreaCabecera(prodProvReq[i], "S");

                            cod_orden_servicio = eOrdCom.cod_orden_compra_servicio;
                            flg_solicitud = eOrdCom.flg_solicitud;
                            dsc_anho = eOrdCom.dsc_anho;

                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_servicio;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = helper.CrearDetalle(prodProvReq[i]);
                        }
                        else
                        {
                            prodProvReq[i].cod_orden_compra_servicio = cod_orden_servicio;
                            prodProvReq[i].flg_solicitud = flg_solicitud;
                            prodProvReq[i].dsc_anho = dsc_anho;

                            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOrdCom = helper.CrearDetalle(prodProvReq[i]);
                        }
                    }
                }

                string respuesta = unit.Requerimiento.GenerarOC_Requerimiento(empresa, sede, requerimientos, Program.Sesion.Usuario.cod_usuario, solicitud, anho);

                if (respuesta.Contains("OK"))
                {
                    MessageBox.Show("Órdenes generadas con éxito", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Crear Órdenes.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SplashScreenManager.CloseForm();
            BuscarRequerimientos();
        }

        /*private eOrdenCompra_Servicio CreaCabecera(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj, string solicitud)
        {
            eOrdenCompra_Servicio eOC = new eOrdenCompra_Servicio();

            eOC.cod_empresa = obj.cod_empresa;
            eOC.cod_sede_empresa = obj.cod_sede_empresa;
            eOC.cod_orden_compra_servicio = "";
            eOC.num_cotizacion = "N/A";
            eOC.cod_proveedor = obj.cod_proveedor;
            eOC.dsc_ruc = obj.dsc_ruc;
            eOC.flg_solicitud = solicitud;
            eOC.cod_almacen = "";
            eOC.cod_modalidad_pago = "";
            eOC.dsc_direccion_despacho = "";
            eOC.fch_emision = DateTime.Now;
            eOC.fch_despacho = new DateTime(1999, 01, 01);
            eOC.dsc_terminos_condiciones = "";
            eOC.imp_subtotal = 0;
            eOC.imp_igv = 0;
            eOC.imp_total = 0;
            eOC.dsc_imp_total = "";
            eOC.prc_CV = 0;
            eOC.prc_LI = 0;
            eOC.prc_CB = 0;
            eOC.prc_GG = 0;
            eOC.prc_ADM = 0;
            eOC.prc_OPER = 0;
            eOC.prc_GV = 0;
            eOC.dsc_observaciones = "";

            eOC = unit.OrdenCompra_Servicio.Ins_Act_OrdenCompra_Servicio<eOrdenCompra_Servicio>(eOC, Program.Sesion.Usuario.cod_usuario);

            return eOC;
        }

        private eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle CrearDetalle(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj)
        {
            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOC = new eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle();

            eDetOC.cod_empresa = obj.cod_empresa;
            eDetOC.cod_sede_empresa = obj.cod_sede_empresa;
            eDetOC.cod_orden_compra_servicio = obj.cod_orden_compra_servicio;
            eDetOC.flg_solicitud = obj.flg_solicitud;
            eDetOC.dsc_anho = obj.dsc_anho;
            eDetOC.num_item = obj.num_item;
            eDetOC.cod_requerimiento = obj.cod_requerimiento;
            eDetOC.cod_proveedor = obj.cod_proveedor;
            eDetOC.dsc_ruc = obj.dsc_ruc;
            eDetOC.cod_tipo_servicio = obj.cod_tipo_servicio;
            eDetOC.cod_subtipo_servicio = obj.cod_subtipo_servicio;
            eDetOC.cod_producto = obj.cod_producto;//
            eDetOC.dsc_servicio = "";
            eDetOC.cod_unidad_medida = obj.cod_unidad_medida;
            eDetOC.num_cantidad = obj.num_cantidad;
            eDetOC.imp_unitario = obj.imp_unitario;
            eDetOC.imp_total_det = obj.imp_total_det;
            eDetOC.flg_solicitaOC = "SI";// obj.flg_solicitaOC; --> Cambia el flag a Solicitado.

            eDetOC = unit.OrdenCompra_Servicio.Ins_Act_Detalle_OrdenCompra_Servicio<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(eDetOC, Program.Sesion.Usuario.cod_usuario);

            return eDetOC;
        }*/

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"{grupoAcciones.Enabled},  {btnAprobar.Enabled}, { btnDesaprobar.Enabled}, ");
            BuscarRequerimientos();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Busqueda("", "Cliente");
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;

            switch (tipo)
            {
                case "Cliente":
                    frm.entidad = frmBusquedas.MiEntidad.ClienteEmpresa;
                    frm.cod_condicion1 = lkpEmpresa.EditValue.ToString();
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Cliente":
                    codigoCliente = frm.codigo;
                    txtCliente.Text = frm.descripcion;
                    break;
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Busqueda("", "Cliente");
            }
            string dato = unit.Globales.pKeyPress(txtCliente, e);
            if (dato != "")
            {
                Busqueda(dato, "Cliente");
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            unit.Requerimiento.CargaCombosLookUp("Sedes", lkpSede, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString());
        }

        private void lkpSede_EditValueChanged(object sender, EventArgs e)
        {
            unit.Requerimiento.CargaCombosLookUp("Areas", lkpArea, "cod_area", "dsc_area", "", valorDefecto: true, cod_empresa: lkpEmpresa.EditValue.ToString(), cod_sede_empresa: lkpSede.EditValue == null ? "" : lkpSede.EditValue.ToString());
        }

        private void frmListadoRequerimientos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) BuscarRequerimientos();
        }

        private void tcRequerimientos_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(4, Program.Sesion.Usuario.cod_usuario, Program.Sesion.Global.Solucion);
            eVentana oPerfilAdm = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5 || x.cod_perfil == 29 || x.cod_perfil == 31);
            eVentana oPerfilLogOS = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5 || x.cod_perfil == 38 || x.cod_perfil == 40);

            if (tcRequerimientos.SelectedTabPage == tpReqSolicitados)
            {
                btnAnularRequerimiento.Enabled = oPerfilAdm != null ? true : false;
                btnAprobar.Enabled = oPerfilAdm != null ? true : false;
                btnExportarRequerimientoFirmas.Enabled = oPerfilAdm != null ? true : false;
                btnDesaprobar.Enabled = false;
                btnGenerarOC.Enabled = false;
                btnGenerarOS.Enabled = false;
            }
            else if (tcRequerimientos.SelectedTabPage == tpReqAprobados)
            {
                btnAnularRequerimiento.Enabled = false;
                btnAprobar.Enabled = false;
                btnExportarRequerimientoFirmas.Enabled = false;
                btnDesaprobar.Enabled = oPerfilAdm != null ? true : false;
                btnGenerarOC.Enabled = oPerfilLog != null ? true : false;
                btnGenerarOS.Enabled = oPerfilLogOS != null ? true : false;
            }
            else
            {
                btnExportarRequerimientoFirmas.Enabled = false;
                btnAnularRequerimiento.Enabled = false;
                btnAprobar.Enabled = false;
                btnDesaprobar.Enabled = false;
                btnGenerarOC.Enabled = false;
                btnGenerarOS.Enabled = false;
            }
        }

        private void gvReqSolicitados_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2)
            {
                //var ee = e.RowHandle;
                gvReqSolicitados.SelectRow(e.RowHandle);
                MostrarReq("tpReqSolicitados");
            }
        }

        private void gvReqSolicitados_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvReqSolicitados_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvReqSolicitados_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvReqSolicitados_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvReqAprobados_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //if (e.RowHandle >= 0 && e.Clicks == 2)
            //{
            //    var obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;
            //    MostrarReq("tpReqAprobados");
            //}
        }

        private void gvReqAprobados_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (isRunning) return;

            isRunning = true;

            DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.Action == CollectionChangeAction.Add && (string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_estado_requerimiento") == "ORDEN GENERADA")
            {
                if (unit.Requerimiento.ValidarOC_Requerimiento((string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_empresa"), (string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_sede_empresa"), (string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_requerimiento")) != "LIBERADA")
                {
                    View.UnselectRow(e.ControllerRow);
                }
            }

            if (e.Action == CollectionChangeAction.Add && (string)this.gvReqAprobados.GetRowCellValue(this.gvReqAprobados.FocusedRowHandle, "cod_estado_requerimiento") == "ATENDIDO")
            {
                View.UnselectRow(e.ControllerRow);
            }

            if (e.Action == CollectionChangeAction.Refresh && View.SelectedRowsCount > 0)
            {
                View.BeginSelection();

                foreach (int Row in View.GetSelectedRows())
                {
                    if ((string)this.gvReqAprobados.GetRowCellValue(Row, "cod_estado_requerimiento") == "ORDEN GENERADA")
                    {
                        if (unit.Requerimiento.ValidarOC_Requerimiento((string)this.gvReqAprobados.GetRowCellValue(Row, "cod_empresa"), (string)this.gvReqAprobados.GetRowCellValue(Row, "cod_sede_empresa"), (string)this.gvReqAprobados.GetRowCellValue(Row, "cod_requerimiento")) != "LIBERADA")
                        {
                            View.UnselectRow(Row);
                        }
                    }

                    if ((string)this.gvReqAprobados.GetRowCellValue(Row, "cod_estado_requerimiento") == "ATENDIDO")
                    {
                        View.UnselectRow(Row);
                    }
                }

                View.EndSelection();
            }

            isRunning = false;
        }

        private void gvReqAprobados_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvReqAprobados_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvReqAprobados_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void gvReqAprobados_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvReqAtendidos_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarReq("tpReqAtendidos");
        }

        private void gvReqAtendidos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvReqAtendidos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvReqAtendidos_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvReqAtendidos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvReqAnulados_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarReq("tpReqAnulados");
        }

        private void gvReqAnulados_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvReqAnulados_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvReqCompletos_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvReqCompletos_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                unit.Globales.Pintar_EstiloGrilla(sender, e);
                eRequerimiento obj = gvReqCompletos.GetFocusedRow() as eRequerimiento;
                if (obj == null) return;
                if (obj.cod_estado_requerimiento == "ANULADO") e.Appearance.ForeColor = Color.Red;
            }
        }

        private void gvReqAnulados_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }

        private void gvReqAnulados_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void MostrarReq(string tabName)
        {
            eRequerimiento obj = new eRequerimiento();

            List<eVentana> listPerfil = unit.Sistema.ListarPerfilesUsuario<eVentana>(opcion: 4, cod_usuario: Program.Sesion.Usuario.cod_usuario, dsc_solucion: Program.Sesion.Global.Solucion);
            eVentana oPerfilLog = listPerfil.Find(x => x.cod_perfil == 28 || x.cod_perfil == 5 || x.cod_perfil == 26);

            bool mostrarBtnAprobar = false;
            switch (tabName)
            {
                case "tpReqSolicitados":
                    obj = gvReqSolicitados.GetFocusedRow() as eRequerimiento;
                    mostrarBtnAprobar = true;
                    break;
                case "tpReqAprobados":
                    obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;
                    break;
                case "tpReqAtendidos":
                    obj = gvReqAtendidos.GetFocusedRow() as eRequerimiento;
                    break;
                case "tpReqAnulados":
                    obj = gvReqAnulados.GetFocusedRow() as eRequerimiento;
                    break;
            }

            if (obj.flg_solicitud == "COMPRA")
            {
                frmMantRequerimientosCompra frm = new frmMantRequerimientosCompra(ParentFormType.ListadoSolicitudCompra);
                //if (mostrarBtnAprobar) { frm.btnAprobacionRequerimiento.Visibility = BarItemVisibility.Always; }
                if (mostrarBtnAprobar)
                {
                    frm.ParentForm = this;
                    frm.btnAprobar.Enabled = mostrarBtnAprobar;
                }
                frm.empresa = obj.cod_empresa;
                frm.codigoEstadoRequerimiento = obj.cod_estado_requerimiento;
                frm.sede = obj.cod_sede_empresa;
                frm.requerimiento = obj.cod_requerimiento;
                frm.solicitud = obj.flg_solicitud.ToString().Substring(0, 1);
                frm.anho = obj.dsc_anho;
                frm.WindowState = FormWindowState.Maximized;

                if (tabName == "tpReqSolicitados")
                {
                    frm.accion = oPerfilLog != null ? RequerimientoCompra.Editar : RequerimientoCompra.Vista;
                }
                else
                {
                    frm.accion = RequerimientoCompra.Vista;
                }

                frm.ShowDialog();
            }
            else
            {
                frmMantRequerimientosServicio frm = new frmMantRequerimientosServicio();
                frm.empresa = obj.cod_empresa;
                frm.sede = obj.cod_sede_empresa;
                frm.requerimiento = obj.cod_requerimiento;
                frm.solicitud = obj.flg_solicitud.ToString().Substring(0, 1);
                frm.anho = obj.dsc_anho;
                frm.WindowState = FormWindowState.Maximized;

                if (tabName == "tpReqSolicitados")
                {
                    frm.accion = oPerfilLog != null ? RequerimientoServicio.Editar : RequerimientoServicio.Vista;
                }
                else
                {
                    frm.accion = RequerimientoServicio.Vista;
                }

                frm.ShowDialog();
            }

            BuscarRequerimientos();
        }

        private void txtCliente_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCliente.Text)) { _clearCliente.Visible = false; } else { _clearCliente.Visible = true; }
        }

        private void btnStockBajos_ItemClick(object sender, ItemClickEventArgs e)
        {
            //

            AbrirTabProductoBajoStock();
        }

        private void AbrirTabProductoBajoStock()
        {
            string cod_empresa = lkpEmpresa.EditValue.ToString();
            var ListProductos = unit.Logistica.Obtener_ListadosProductos<eProductos>(11, cod_empresa: cod_empresa, flg_activo: "SI");
            var f = new ModalView() { Text = $"Listado de Productos | Indicador Stock Mínimo | {lkpEmpresa.Text}", TitleBackColor = Program.Sesion.Colores.Verde };
            var u = new ucListadoProductoLowStock(cod_empresa, f, unit, ListProductos) { Dock = DockStyle.Fill };
            f.SetSize(u.Size);
            f.Contenedor.Controls.Add(u);
            f.ShowDialog();


        }

        #region Exportar Requerimientos para Firmar
        private void btnExportarRequerimientoFirmas_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Program.Sesion.Usuario.flg_administrador.Equals("NO"))
            {
                HNG.MessageWarning("Esta opción es para los usuarios con el perfil de Administrador.", "Exportar Requerimientos");
                return;
            }

            CrearDocumentoParaExportarPDF();
        }
        private void CrearDocumentoParaExportarPDF()
        {
            eRequerimiento requerimiento = gvReqSolicitados.GetFocusedRow() as eRequerimiento;
            if (requerimiento == null) return;

            //Obtener Listado de productos en requerimientos:
            var productos = unit.Requerimiento.Cargar_Detalle_Requerimiento<eRequerimiento.eRequerimiento_Detalle>(
                opcion: 5,
                empresa: requerimiento.cod_empresa,
                sede: requerimiento.cod_sede_empresa,
                requerimiento: requerimiento.cod_requerimiento,
                flg_solicitud: requerimiento.flg_solicitud.ToString().Substring(0, 1),
                dsc_anho: requerimiento.dsc_anho);
            if (productos == null || productos.Count == 0) return;


            //Obtener plantilla en html.
            string plantillaHtml = HtmlString.GetSignRequerimientoMateriales;
            string rutaLogo = System.Windows.Forms.Application.StartupPath + "\\branding\\"; //@"C:\Imperium-Software\Logo\";
            string empresaColor = "#75C046"; // Color por defecto

            var formatoISO = (Codigo: "NO DATA", Version: "NO DATA", Fecha: "NO DATA");

            switch (requerimiento.cod_empresa)
            {
                case "00001":
                    rutaLogo += "hng-reporte-v1.png"; empresaColor = "#598B7D"; formatoISO = (Codigo: "NO DATA", Version: "NO DATA", Fecha: "NO DATA"); break;
                case "00002":
                    rutaLogo += "facilita-reporte-v1.png"; empresaColor = "#0c3568"; formatoISO = (Codigo: "F - SGI - 32", Version: "04", Fecha: "12/04/2021"); break;
                case "00004":
                    rutaLogo += "k2_reporte_v1.png"; empresaColor = "#259D96"; formatoISO = (Codigo: "NO DATA", Version: "NO DATA", Fecha: "NO DATA");
                    break;
                default: rutaLogo += "Impeerium-reporte-v1.png"; break;
            }

            // Reemplazar las variables de la plantilla HTML con los valores del requerimiento
            plantillaHtml = plantillaHtml.Replace("@logotipo", rutaLogo);
            plantillaHtml = plantillaHtml.Replace("@Codigo_ISO", formatoISO.Codigo);
            plantillaHtml = plantillaHtml.Replace("@Version_ISO", formatoISO.Version);
            plantillaHtml = plantillaHtml.Replace("@Fecha_ISO", formatoISO.Fecha);
            plantillaHtml = plantillaHtml.Replace("@Header_BackColor", empresaColor);
            plantillaHtml = plantillaHtml.Replace("@Num_Requerimiento", requerimiento.cod_requerimiento);
            plantillaHtml = plantillaHtml.Replace("@Fecha_Requerimiento", requerimiento.fch_requerimiento.ToString("yyyy-MM-dd"));
            plantillaHtml = plantillaHtml.Replace("@Cliente_Sede", requerimiento.dsc_sede_cliente);//.dsc_sede_cliente);
            plantillaHtml = plantillaHtml.Replace("@Supervisor_Solicitante", requerimiento.dsc_usuario_aprobacion);
            plantillaHtml = plantillaHtml.Replace("@Operario_Solicitante", requerimiento.dsc_usuario_atendido);

            //Crear filas según la cantidad de productos.
            var filasHtml = string.Join(Environment.NewLine, productos.Select((producto, indice) =>
            $@"<tr>
                  <td>{indice += 1}</td>
                  <td>{producto.dsc_tipo_servicio}</td>
                  <td>{producto.cod_producto}</td>
                  <td>{producto.dsc_producto}</td>
                  <td>{producto.dsc_simbolo}</td>
                  <td>{producto.num_restante}</td>
                  <td>{producto.num_cantidad}</td>
                  <td>{producto.dsc_observaciones}</td>
            </tr>"));

            //Reemplazar filas @rows
            plantillaHtml = plantillaHtml.Replace("@rows", filasHtml);

            var archivoDeDescarga = Program.Sesion.Global.RutaArchivosLocalExportar;
            var documentoExtensionHtml = $"{archivoDeDescarga}\\{requerimiento.cod_empresa}-{requerimiento.cod_requerimiento}.html";
            var documentoExtensionPdf = $"{archivoDeDescarga}\\{requerimiento.cod_empresa}-{requerimiento.cod_requerimiento}.pdf";

            if (System.IO.File.Exists(documentoExtensionHtml))
                System.IO.File.Delete(documentoExtensionHtml);

            if (System.IO.File.Exists(documentoExtensionPdf))
                System.IO.File.Delete(documentoExtensionPdf);

            System.IO.File.WriteAllText(documentoExtensionHtml, plantillaHtml);

            var converter = new HtmlToPdf();
            var doc = converter.ConvertUrl(documentoExtensionHtml);
            doc.Save(documentoExtensionPdf);
            doc.Close();

            if (System.IO.File.Exists(documentoExtensionHtml))
                System.IO.File.Delete(documentoExtensionHtml);

            //Arbir PDF final
            System.Diagnostics.Process.Start(documentoExtensionPdf);
        }
        #endregion

        private void btnVerPdfAdjuntado_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //MessageBox.Show("dos");

        }

        private void btnVerPdfAdjuntado_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("uno");
        }

        private void gvReqAprobados_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2)
            {
                if (e.Column.FieldName != "flg_PDF") { MostrarReq("tpReqAprobados"); }
                else
                {
                    try
                    {
                        if (!(gvReqAprobados.GetFocusedRow() is eRequerimiento obj)) { return; }
                        if (obj.flg_PDF!=null && obj.flg_PDF.Equals("SI"))
                        {
                            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");
                            oneDrive.DownloadFile(new RequerimientoDescargarDTO(
                            idPDF: obj.idPDF,
                            idXML: "",
                            codEmpresa: obj.cod_empresa,
                            isPdf: true
                            ));
                            SplashScreenManager.CloseForm();
                        }
                    }
                    catch (Exception ex)
                    {
                        HNG.MessageError("Hubieron problemas al autenticar las credenciales " + ex.Message, "Ver PDF");
                    }
                }
            }
        }
    }
}