using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BE_BackOffice;
using DevExpress.XtraNavBar;
using DevExpress.XtraSplashScreen;
using DevExpress.Images;
using System.Configuration;
using DevExpress.XtraReports.UI;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using UI_BackOffice.Tools;
using static UI_BackOffice.Tools.Hng_Htmltools;
using DevExpress.Utils.About;
using DevExpress.XtraBars.Objects;
using System.Linq;
using DevExpress.Office.Internal;
using System.IO;
using Microsoft.Identity.Client;
using System.Windows.Controls;
using DevExpress.PivotGrid.ServerMode.OperationGraph;
using System.Diagnostics;

namespace UI_BackOffice.Formularios.Logistica
{
    internal enum InventarioAlmacen
    {
        Nuevo = 0,
        Editar = 1
    }
    public partial class frmListaInventarioAlmacen : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        internal InventarioAlmacen MiAccion = InventarioAlmacen.Nuevo;
        List<eProductos.eProductosProveedor> lista = new List<eProductos.eProductosProveedor>();
        List<eAlmacen.eEntrada_Cabecera> listaEntradas = new List<eAlmacen.eEntrada_Cabecera>();
        List<eAlmacen.eSalida_Cabecera> listaSalidas = new List<eAlmacen.eSalida_Cabecera>();
        List<eAlmacen.eGuiaRemision_Cabecera> listaGuias = new List<eAlmacen.eGuiaRemision_Cabecera>();
        List<eRequerimiento> listReqAprobados = new List<eRequerimiento>();
        List<eOrdenCompra_Servicio> listOrdenesEnviadas = new List<eOrdenCompra_Servicio>();

        Brush ConCriterios = Brushes.Green;
        Brush ParcialCriterios = Brushes.Orange;
        Brush SinCriterios = Brushes.Red;
        int markWidth = 16;
        string cod_empresa = "";
        bool Buscar = false;

        public frmListaInventarioAlmacen()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListaInventarioAlmacen_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarOpcionesMenu();
            CargarListado("TODOS", "");

            lblTitulo.ForeColor = Program.Sesion.Colores.Verde;
            lblTitulo.Text = navBarControl1.SelectedLink.Group.Caption + ": " + navBarControl1.SelectedLink.Item.Caption;
            picTitulo.Image = navBarControl1.SelectedLink.Group.ImageOptions.LargeImage;
            navBarControl1.Groups[0].SelectedLinkIndex = 0;
            Buscar = true;
            navBarControl1.SelectedLink = navBarControl1.Groups[0].ItemLinks[0];
            NavBarGroup navGrupo = navBarControl1.SelectedLink.Group as NavBarGroup;
            CargarListado(navGrupo.Caption, navGrupo.SelectedLink.Item.Tag.ToString());
            //Fecha
            DateTime date = DateTime.Now;
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtFechaInicio.EditValue = new DateTime(date.Year, 1, 1);
            dtFechaFin.EditValue = oUltimoDiaDelMes;

            //  unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            Buscador();
            //  SplashScreenManager.CloseForm();

            HabilitarBotones();

            switch (MiAccion)
            {
                case InventarioAlmacen.Nuevo:
                    break;
                case InventarioAlmacen.Editar:
                    break;
            }
        }

        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, this.Name, Program.Sesion.Global.Solucion);
            if (listPermisos.Count > 0)
            {
                grupoAcciones.Enabled = listPermisos[0].flg_escritura;
            }
        }

        internal void CargarOpcionesMenu()
        {
            List<eProveedor_Empresas> listEmpresas = unit.Proveedores.ListarOpcionesMenu<eProveedor_Empresas>(12);
            System.Drawing.Image imgEmpresaLarge = ImageResourceCache.Default.GetImage("images/navigation/home_32x32.png");
            System.Drawing.Image imgEmpresaSmall = ImageResourceCache.Default.GetImage("images/navigation/home_16x16.png");

            NavBarGroup NavEmpresa = navBarControl1.Groups.Add();
            NavEmpresa.Name = "Por Empresa";
            NavEmpresa.Caption = "Por Empresa"; NavEmpresa.Expanded = true; NavEmpresa.SelectedLinkIndex = 0;
            NavEmpresa.GroupCaptionUseImage = NavBarImage.Large; NavEmpresa.GroupStyle = NavBarGroupStyle.SmallIconsText;
            NavEmpresa.ImageOptions.LargeImage = imgEmpresaLarge; NavEmpresa.ImageOptions.SmallImage = imgEmpresaSmall;

            List<eProveedor_Empresas> listEmpresasUsuario = unit.Proveedores.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", Program.Sesion.Usuario.cod_usuario);
            if (listEmpresasUsuario.Count == 0) { MessageBox.Show("Debe tener una empresa asignada para visualizar los datos", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            List<eProveedor_Empresas> listadoEmp = new List<eProveedor_Empresas>();
            eProveedor_Empresas objEmp = new eProveedor_Empresas();
            //objEmp = listEmpresas.Find(x => x.cod_empresa == "ALL");
            //listadoEmp.Add(objEmp);
            if (listEmpresas.Count > 0)
            {
                foreach (eProveedor_Empresas obj2 in listEmpresasUsuario)
                {
                    objEmp = listEmpresas.Find(x => x.cod_empresa == obj2.cod_empresa);
                    if (objEmp != null) listadoEmp.Add(objEmp);
                }
            }

            foreach (eProveedor_Empresas obj in listadoEmp)
            {
                NavBarItem NavDetalle = navBarControl1.Items.Add();
                NavDetalle.Tag = obj.cod_empresa; NavDetalle.Name = obj.cod_empresa;
                NavDetalle.Caption = obj.dsc_empresa; NavDetalle.LinkClicked += NavDetalle_LinkClicked;

                NavEmpresa.ItemLinks.Add(NavDetalle);
            }
        }

        private void NavDetalle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            lblTitulo.Text = e.Link.Group.Caption + ": " + e.Link.Caption; picTitulo.Image = e.Link.Group.ImageOptions.LargeImage;
            //CargarListado(e.Link.Group.Caption, e.Link.Item.Tag.ToString());
        }

        public void CargarListado(string NombreGrupo, string Codigo)
        {
            try
            {
                switch (NombreGrupo)
                {
                    case "Por Empresa": cod_empresa = Codigo; break;
                }

                unit.Trabajador.CargaCombosLookUp("SedesEmpresa", lkpSedeEmpresa, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, cod_empresa);
                lkpSedeEmpresa.EditValue = null; lkpAlmacen.EditValue = null;
                List<eTrabajador.eInfoLaboral_Trabajador> lista = unit.Trabajador.ListarOpcionesTrabajador<eTrabajador.eInfoLaboral_Trabajador>(6, cod_empresa);
                if (lista.Count >= 1) lkpSedeEmpresa.EditValue = lista[0].cod_sede_empresa;

                //  btnBuscar_Click(btnBuscar, new EventArgs());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void navBarControl1_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            e.Group.SelectedLinkIndex = 0;
            //navBarControl1_SelectedLinkChanged(navBarControl1, new DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs(e.Group, e.Group.SelectedLink));
        }

        void ActiveGroupChanged(string caption, System.Drawing.Image imagen)
        {
            lblTitulo.Text = caption; picTitulo.Image = imagen;
        }

        private void navBarControl1_SelectedLinkChanged(object sender, DevExpress.XtraNavBar.ViewInfo.NavBarSelectedLinkChangedEventArgs e)
        {
            //e.Group.SelectedLinkIndex = 0;
            if (!Buscar) e.Group.SelectedLinkIndex = 0;
            if (e.Group.SelectedLink != null && Buscar)
            {
                ActiveGroupChanged(e.Group.Caption + ": " + e.Group.SelectedLink.Item.Caption, e.Group.ImageOptions.LargeImage);
                //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                CargarListado(e.Group.Caption, e.Group.SelectedLink.Item.Tag.ToString());
                //SplashScreenManager.CloseForm();


            }
        }

        private void btnCrearAlmacen_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMantAlmacen frm = new frmMantAlmacen();
            frm.cod_empresa = cod_empresa;
            frm.ShowDialog();
            if (frm.ActualizarListado) lkpSedeEmpresa_EditValueChanged(lkpSedeEmpresa, new EventArgs());
        }

        private void btnRegistrarEntrada_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                eOrdenCompra_Servicio obj = null;
                if (xtcInventarioAlmacen.SelectedTabPage == xtabOrdenesCompra) obj = gvOrdEnviadas.GetFocusedRow() as eOrdenCompra_Servicio;
                if (obj != null && obj.ctd_Atencion == 2)
                {
                    MessageBox.Show("La OC ya se encuentra ATENDIDA", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); lkpAlmacen.Focus(); return;
                }

                frmRegistrarEntradaAlmacen frm = new frmRegistrarEntradaAlmacen();
                frm.MiAccion = IngresoAlmacen.Nuevo;
                frm.cod_empresa = cod_empresa;
                frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                frm.cod_orden_compra_servicio = obj == null ? "" : obj.cod_orden_compra_servicio;
                frm.flg_solicitud = obj == null ? "" : obj.flg_solicitud;
                frm.dsc_anho = obj == null ? "" : obj.dsc_anho.ToString();
                frm.ShowDialog();
                if (frm.ActualizarListado)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    Buscador();
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRegistrarSalida_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                eRequerimiento obj = null;
                if (xtcInventarioAlmacen.SelectedTabPage == xtabRequerimientos) obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;

                frmRegistrarSalidaAlmacen frm = new frmRegistrarSalidaAlmacen();
                frm.cod_empresa = cod_empresa;
                frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                frm.cod_requerimiento = obj == null ? "" : obj.cod_requerimiento;
                frm.flg_solicitud = obj == null ? "" : obj.flg_solicitud;
                frm.dsc_anho = obj == null ? "" : obj.dsc_anho.ToString();
                frm.ShowDialog();
                if (frm.ActualizarListado)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    Buscador();
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRegistrarGuiaRemision_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                eRequerimiento obj = null;
                if (xtcInventarioAlmacen.SelectedTabPage == xtabRequerimientos) obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;

                frmRegistrarGuiaRemisionAlmacen frm = new frmRegistrarGuiaRemisionAlmacen();
                frm.cod_empresa = cod_empresa;
                frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                frm.cod_empresa = cod_empresa;
                frm.cod_requerimiento = obj == null ? "" : obj.cod_requerimiento;
                frm.flg_solicitud = obj == null ? "" : obj.flg_solicitud;
                frm.dsc_anho = obj == null ? "" : obj.dsc_anho.ToString();
                frm.ShowDialog();
                if (frm.ActualizarListado)
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                    Buscador();
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lkpSedeEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpSedeEmpresa.EditValue != null)
            {
                unit.Logistica.CargaCombosLookUp("Almacen", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: cod_empresa, cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
                List<eAlmacen> lista = unit.Logistica.Obtener_ListaVariasLogistica<eAlmacen>(13, cod_empresa: cod_empresa, cod_sede_empresa: lkpSedeEmpresa.EditValue.ToString());
                if (lista.Count >= 1) lkpAlmacen.EditValue = lista[0].cod_almacen;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
            Buscador();
            SplashScreenManager.CloseForm();
        }
        private void Buscador()
        {
            try
            {
                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }

                lista = unit.Logistica.Obtener_ListaLogistica<eProductos.eProductosProveedor>(15, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                      FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                      FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsListadoProductos.DataSource = lista; gvListadoProductos.RefreshData();

                listaEntradas = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eEntrada_Cabecera>(19, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                      FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                      FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsListadoEntradas.DataSource = listaEntradas; gvListadoEntradas.RefreshData();

                listaSalidas = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eSalida_Cabecera>(22, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                      FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                      FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsListadoSalidas.DataSource = listaSalidas; gvListadoSalidas.RefreshData();

                listaGuias = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eGuiaRemision_Cabecera>(24, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                      FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                      FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                bsListadoSalidasGuiaRemision.DataSource = listaGuias; gvListadoSalidasGuiaRemision.RefreshData();

                CargarRequerimientos();
                //listReqAprobados = unit.Requerimiento.ListarRequerimiento<eRequerimiento>(opcion: 3,
                //    cod_empresa: cod_empresa, cod_sede_empresa: lkpSedeEmpresa.EditValue == null ? "" : lkpSedeEmpresa.EditValue.ToString(),
                //    cod_cliente: "", cod_area: "", cod_tipo_fecha: "01", fch_inicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                //    fch_fin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                //    cod_estado_requerimiento: "");
                //bsListadoReqAprobados.DataSource = listReqAprobados;

                //listOrdenesEnviadas = unit.OrdenCompra_Servicio.ListarOrdenesCompra<eOrdenCompra>(7, cod_empresa, lkpSedeEmpresa.EditValue == null ? "" : lkpSedeEmpresa.EditValue.ToString(),
                //                                                            "", "01", Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                //                                                            Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                listOrdenesEnviadas = unit.Logistica.Obtener_ListaLogistica<eOrdenCompra_Servicio>(30, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(),
                                                                                FechaInicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                                FechaFin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));

                bsListadoOrdEnviadas.DataSource = listOrdenesEnviadas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CargarRequerimientos(string estado = "")
        {
            listReqAprobados = unit.Requerimiento.ListarRequerimiento<eRequerimiento>(opcion: 3,
                    cod_empresa: cod_empresa, cod_sede_empresa: lkpSedeEmpresa.EditValue == null ? "" : lkpSedeEmpresa.EditValue.ToString(),
                    cod_cliente: "", cod_area: "", cod_tipo_fecha: "01", fch_inicio: Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                    fch_fin: Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"),
                    cod_estado_requerimiento: estado);
            bsListadoReqAprobados.DataSource = listReqAprobados;
        }

        private void gvListadoEntradas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eAlmacen.eEntrada_Cabecera obj = gvListadoEntradas.GetFocusedRow() as eAlmacen.eEntrada_Cabecera;

                    frmRegistrarEntradaAlmacen frm = new frmRegistrarEntradaAlmacen();
                    frm.MiAccion = IngresoAlmacen.Editar;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                    frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                    frm.cod_entrada = obj.cod_entrada;
                    frm.dsc_anho = "";
                    frm.ShowDialog();
                    if (frm.ActualizarListado)
                    {
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                        Buscador();
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoSalidas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eAlmacen.eSalida_Cabecera obj = gvListadoSalidas.GetFocusedRow() as eAlmacen.eSalida_Cabecera;

                    frmRegistrarSalidaAlmacen frm = new frmRegistrarSalidaAlmacen();
                    frm.MiAccion = SalidaAlmacen.Vista;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                    frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                    frm.cod_salida = obj.cod_salida;
                    frm.ShowDialog();
                    if (frm.ActualizarListado)
                    {
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                        Buscador();
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoSalidasGuiaRemision_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.Clicks == 2)
                {
                    eAlmacen.eGuiaRemision_Cabecera obj = gvListadoSalidasGuiaRemision.GetFocusedRow() as eAlmacen.eGuiaRemision_Cabecera;

                    frmRegistrarGuiaRemisionAlmacen frm = new frmRegistrarGuiaRemisionAlmacen();
                    frm.MiAccion = GuiaRemision.Vista;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = lkpSedeEmpresa.EditValue.ToString();
                    frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                    frm.cod_guiaremision = obj.cod_guiaremision;
                    frm.ShowDialog();
                    if (frm.ActualizarListado)
                    {
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                        Buscador();
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmListaInventarioAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo listado", "Cargando...");
                Buscador();
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
                string archivo = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + "\\InventarioAlmacen" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "") + ".xlsx";
                if (!System.IO.Directory.Exists(carpeta)) System.IO.Directory.CreateDirectory(carpeta);
                if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoProductos) gvListadoProductos.ExportToXlsx(archivo);
                if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoEntradas) gvListadoEntradas.ExportToXlsx(archivo);
                if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoSalidas) gvListadoSalidas.ExportToXlsx(archivo);
                if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoSalidasGuiaRemision) gvListadoSalidasGuiaRemision.ExportToXlsx(archivo);
                gvListadoProductos.ExportToXlsx(archivo);
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

        private void gvListadoProductos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProductos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoEntradas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoEntradas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoSalidas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void btnGenerarNotaIngreso_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //REPORTE  HTML

                // string html = Properties.Resources.nota_ingreso.ToString();

                //Header
                //html = html.Replace("@razon_social", Program.Sesion.Empresa.RazonSocial);
                //html = html.Replace("@ruc", Program.Sesion.Empresa.RUC);
                //html = html.Replace("@cliente_name", Cabeceras.Cliente.Trim());
                //html = html.Replace("@area", Cabeceras.Area.Trim());
                //html = html.Replace("@celular_cliente", Cabeceras.Telefono.Trim());
                //html = html.Replace("@num-ticket", Cabeceras.Boleta.Trim());
                //html = html.Replace("@date_ticket", string.Concat("Fecha: ", Cabeceras.FechaBoleta.Trim()));
                //html = html.Replace("@gloga", Cabeceras.Glosa.Trim());

                //Rows
                //string rows = string.Empty;
                //eAlmacen.eEntrada_Cabecera cab = gvListadoEntradas.GetFocusedRow() as eAlmacen.eEntrada_Cabecera;

                //var rep = unit.Logistica.Reporte_NotaEntrada_Detalle<eAlmacen.eEntrada_Detalle>(cod_entrada: cab.cod_entrada, 
                //    cod_almacen:cab.cod_almacen, cod_empresa: cab.cod_empresa, cod_sede_empresa: cab.cod_sede_empresa);
                //int index = 0;
                //foreach (eAlmacen.eEntrada_Detalle doc in rep)
                //{
                //    index++;
                //    rows += "<tr>";
                //    rows += $"<td style='text-align: center; height: 28px;'>{index}</td>";
                //    rows += $"<td style='text-align: center;'>{doc.cod_producto}</td> ";
                //    rows += $"<td style='text-align: left; padding-left: 10px'>{doc.dsc_descripcion}</td> ";
                //    rows += $"<td style='text-align: right;'>{doc.num_cantidad}</td> ";
                //    rows += $"<td style='text-align: right;'>{doc.num_cantidad_recibido}</td> ";
                //    rows += $"<td style='text-align: right;'>{doc.imp_costo}</td> ";
                //    rows += $"<td style='text-align: right; padding-right: 10px;'>{doc.imp_total}</td> ";
                //    rows += "</tr> ";
                //}
                //html = html.Replace("@rows", rows);

                //Footer
                //html = html.Replace("@sub_total", Totals.SubTotal.Trim());
                //html = html.Replace("@igv", Totals.Igv.Trim());
                //html = html.Replace("@total", Totals.Totalizado.Trim());
                //html = html.Replace("@celular_empresa", Program.Sesion.Empresa.Telefono);
                //html = html.Replace("@responsable", Program.Sesion.Empresa.Representante);
                //html = html.Replace("@email_empresa", Program.Sesion.Empresa.Email);

                //Save
                //  var _ = String.Concat(FacturaID, " (", Cabeceras.FechaBoleta.Trim(), ")", " ", Cabeceras.Razon.Trim());
                //  string filePath = "C:\\TS\\sasasassakkkkk.pdf";//new WordRepHelper().GetFilePath(_);
                // Delete file
                //new WordRepHelper().DeleteFile(filePath);

                //using (FileStream stream = new FileStream(filePath, FileMode.Create))
                //{
                //    //Creamos un nuevo documento y lo definimos como PDF
                //    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                //    iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, stream);
                //    pdfDoc.Open();
                //    pdfDoc.Add(new Phrase(""));

                //    ////Agregamos la imagen del banner al documento
                //    //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.down12, System.Drawing.Imaging.ImageFormat.Png);
                //    //img.ScaleToFit(60, 60);
                //    //img.Alignment = iTextSharp.text.Image.UNDERLYING;

                //    ////img.SetAbsolutePosition(10,100);
                //    //img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 60);
                //    //pdfDoc.Add(img);


                //    //pdfDoc.Add(new Phrase("Hola Mundo"));
                //    using (StringReader sr = new StringReader(html))
                //    {
                //        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                //    }

                //    pdfDoc.Close();
                //    stream.Close();

                //    //if (openfile) Process.Start(filePath);
                //}



                //return;
                //////////

                //unit.Encripta.DesencryptaFile();
                unit.Encripta.DesencryptaConnectionStrings();

                if (xtcInventarioAlmacen.SelectedTabPage != xtabListadoEntradas) return;
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eAlmacen.eEntrada_Cabecera eProv = gvListadoEntradas.GetFocusedRow() as eAlmacen.eEntrada_Cabecera;
                if (eProv == null) { MessageBox.Show("Debe seleccionar un registro de ingreso.", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                rptNotaIngreso report = new rptNotaIngreso();
                //= "";

                ReportPrintTool printTool = new ReportPrintTool(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_entrada"].Value = eProv.cod_entrada;
                report.Parameters["cod_almacen"].Value = eProv.cod_almacen;
                report.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                report.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;
                report.Parameters["cod_proveedor"].Value = eProv.cod_proveedor;

                if (eProv.cod_empresa == "00001") { report.xpb_logo.Image = Properties.Resources.Logo_HNG1; report.lblref.BackColor = Color.FromArgb(63, 63, 65); report.tblcuadro.BackColor = Color.FromArgb(63, 63, 65); report.lblglosa.BackColor = Color.FromArgb(63, 63, 65); }
                if (eProv.cod_empresa == "00002") { report.xpb_logo.Image = Properties.Resources.logo_facilita; report.lblref.BackColor = Color.FromArgb(12, 63, 104); report.tblcuadro.BackColor = Color.FromArgb(12, 63, 104); report.lblglosa.BackColor = Color.FromArgb(12, 63, 104); }
                if (eProv.cod_empresa == "00003") { report.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                if (eProv.cod_empresa == "00004") { report.xpb_logo.Image = Properties.Resources.logo_k2; report.lblref.BackColor = Color.FromArgb(0, 157, 150); report.tblcuadro.BackColor = Color.FromArgb(0, 157, 150); report.lblglosa.BackColor = Color.FromArgb(0, 157, 150); }
                if (eProv.cod_empresa == "00005") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00006") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00007") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00008") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00009") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00010") { report.xpb_logo.Image = Properties.Resources.add_32x32; }

                printTool.ShowPreviewDialog();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { unit.Encripta.EncryptaConnectionString(); }
        }

        private void btnGenerarNotaSalida_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //unit.Encripta.DesencryptaFile();
                unit.Encripta.DesencryptaConnectionStrings();

                if (xtcInventarioAlmacen.SelectedTabPage != xtabListadoSalidas) return;
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eAlmacen.eSalida_Cabecera eProv = gvListadoSalidas.GetFocusedRow() as eAlmacen.eSalida_Cabecera;
                if (eProv == null) { MessageBox.Show("Debe seleccionar un registro de saldia.", "Nota de Salida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                rptNotaSalida report = new rptNotaSalida();
                ReportPrintTool printTool = new ReportPrintTool(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_salida"].Value = eProv.cod_salida;
                report.Parameters["cod_almacen"].Value = eProv.cod_almacen;
                report.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                report.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;
                //report.Parameters["cod_proveedor"].Value = eProv.cod_proveedor;

                if (eProv.cod_empresa == "00001") { report.xpb_logo.Image = Properties.Resources.Logo_HNG1; report.lblref.BackColor = Color.FromArgb(63, 63, 65); report.tblcuadro.BackColor = Color.FromArgb(63, 63, 65); report.lblglosa.BackColor = Color.FromArgb(63, 63, 65); }
                if (eProv.cod_empresa == "00002") { report.xpb_logo.Image = Properties.Resources.logo_facilita; report.lblref.BackColor = Color.FromArgb(12, 63, 104); report.tblcuadro.BackColor = Color.FromArgb(12, 63, 104); report.lblglosa.BackColor = Color.FromArgb(12, 63, 104); }
                if (eProv.cod_empresa == "00003") { report.xpb_logo.Image = Properties.Resources.Logo_HNG1; }
                if (eProv.cod_empresa == "00004") { report.xpb_logo.Image = Properties.Resources.logo_k2; report.lblref.BackColor = Color.FromArgb(0, 157, 150); report.tblcuadro.BackColor = Color.FromArgb(0, 157, 150); report.lblglosa.BackColor = Color.FromArgb(0, 157, 150); }
                if (eProv.cod_empresa == "00005") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00006") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00007") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00008") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00009") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00010") { report.xpb_logo.Image = Properties.Resources.add_32x32; }

                printTool.ShowPreviewDialog();
                SplashScreenManager.CloseForm();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { unit.Encripta.EncryptaConnectionString(); }
        }

        private void btnGenerarGuiaRemision_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //unit.Encripta.DesencryptaFile();
                unit.Encripta.DesencryptaConnectionStrings();

                if (xtcInventarioAlmacen.SelectedTabPage != xtabListadoSalidasGuiaRemision) return;
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo reporte", "Cargando...");
                eAlmacen.eGuiaRemision_Cabecera eProv = gvListadoSalidasGuiaRemision.GetFocusedRow() as eAlmacen.eGuiaRemision_Cabecera;
                if (eProv == null) { MessageBox.Show("Debe seleccionar un registro de salida con guía de remisión.", "Guía de Remisión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                rptGuiaRemision report = new rptGuiaRemision();
                ReportPrintTool printTool = new ReportPrintTool(report);
                report.RequestParameters = false;
                printTool.AutoShowParametersPanel = false;
                report.Parameters["cod_guiaremision"].Value = eProv.cod_guiaremision;
                report.Parameters["cod_empresa"].Value = eProv.cod_empresa;
                report.Parameters["cod_sede_empresa"].Value = eProv.cod_sede_empresa;

                if (eProv.cod_empresa == "00001")
                {
                    report.xpb_logo.Image = Properties.Resources.Logo_HNG;
                    report.lbldomicilio.BackColor = Color.FromArgb(63, 63, 65); report.lblllegada.BackColor = Color.FromArgb(63, 63, 65); report.lbldestinatario.BackColor = Color.FromArgb(63, 63, 65); report.lbltransporte.BackColor = Color.FromArgb(63, 63, 65);
                    report.tblcuadro.BackColor = Color.FromArgb(63, 63, 65); report.lbltransportista.BackColor = Color.FromArgb(63, 63, 65); report.lbltipo.BackColor = Color.FromArgb(63, 63, 65); report.lblremitente.BackColor = Color.FromArgb(63, 63, 65);
                }
                if (eProv.cod_empresa == "00002")
                {
                    report.xpb_logo.Image = Properties.Resources.logo_facilita;
                    report.lbldomicilio.BackColor = Color.FromArgb(12, 63, 104); report.lblllegada.BackColor = Color.FromArgb(12, 63, 104); report.lbldestinatario.BackColor = Color.FromArgb(12, 63, 104); report.lbltransporte.BackColor = Color.FromArgb(12, 63, 104);
                    report.tblcuadro.BackColor = Color.FromArgb(12, 63, 104); report.lbltransportista.BackColor = Color.FromArgb(12, 63, 104); report.lbltipo.BackColor = Color.FromArgb(12, 63, 104); report.lblremitente.BackColor = Color.FromArgb(12, 63, 104);
                }
                if (eProv.cod_empresa == "00003") { report.xpb_logo.Image = Properties.Resources.Logo_HNG; }
                if (eProv.cod_empresa == "00004")
                {
                    report.xpb_logo.Image = Properties.Resources.logo_k2;
                    report.lbldomicilio.BackColor = Color.FromArgb(0, 157, 150); report.lblllegada.BackColor = Color.FromArgb(0, 157, 150); report.lbldestinatario.BackColor = Color.FromArgb(0, 157, 150); report.lbltransporte.BackColor = Color.FromArgb(0, 157, 150);
                    report.tblcuadro.BackColor = Color.FromArgb(0, 157, 150); report.lbltransportista.BackColor = Color.FromArgb(0, 157, 150); report.lbltipo.BackColor = Color.FromArgb(0, 157, 150); report.lblremitente.BackColor = Color.FromArgb(0, 157, 150);
                }
                if (eProv.cod_empresa == "00005") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00006") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00007") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00008") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00009") { report.xpb_logo.Image = Properties.Resources.add_32x32; }
                if (eProv.cod_empresa == "00010") { report.xpb_logo.Image = Properties.Resources.add_32x32; }

                printTool.ShowPreviewDialog();
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { unit.Encripta.EncryptaConnectionString(); }
        }
        private void gvOrdEnviadas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eOrdenCompra_Servicio obj = gvOrdEnviadas.GetRow(e.RowHandle) as eOrdenCompra_Servicio;
                    if (e.Column.FieldName == "ctd_Atencion") e.Appearance.ForeColor = Color.Transparent;
                    if (e.Column.FieldName.ToLower().Equals("fch_envio"))
                    {
                        if (obj != null)
                        {
                            if (obj.fch_envio.ToShortDateString().Contains("0001"))
                            { e.DisplayText = ""; }
                        }
                    }

                    e.DefaultDraw();
                    if (e.Column.FieldName == "ctd_Atencion")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        int cellValue = Convert.ToInt32(e.CellValue);
                        b = cellValue == 0 ? SinCriterios : cellValue == 1 ? ParcialCriterios : ConCriterios;
                        e.Graphics.FillEllipse(b, new System.Drawing.Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void gvListadoEntradas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eAlmacen.eEntrada_Cabecera obj = gvListadoEntradas.GetRow(e.RowHandle) as eAlmacen.eEntrada_Cabecera;
                    if (e.Column.FieldName == "ctd_DocVinculado") e.Appearance.ForeColor = Color.Transparent;

                    e.DefaultDraw();
                    if (e.Column.FieldName == "ctd_DocVinculado")
                    {
                        Brush b; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string cellValue = e.CellValue.ToString();
                        b = cellValue == "NO" ? SinCriterios : ConCriterios;
                        e.Graphics.FillEllipse(b, new System.Drawing.Rectangle(e.Bounds.X + 6, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportarLibro12_1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            frmRangoFecha frm = new frmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;
            ExportarLibro12_1(frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));
        }


        private void ExportarLibro12_1(string FechaInicio, string FechaFin)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");
            //string ListSeparator = "";

            //string entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
            //string server = unit.Encripta.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
            //string bd = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
            //string user = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("UserID")].ToString());
            //string pass = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("Password")].ToString());
            //string AppName = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("AppName")].ToString());

            //string cnxl = "ODBC;DRIVER=SQL Server;SERVER=" + server + ";UID=" + user + ";PWD=" + pass + ";APP=SGI_Excel;DATABASE=" + bd + "";
            //string procedure = "";

            //ListSeparator = ConfigurationManager.AppSettings["ListSeparator"];
            //Excel.Application objExcel = new Excel.Application();
            //objExcel.Workbooks.Add();
            ////objExcel.Visible = true;
            //var workbook = objExcel.ActiveWorkbook;
            //var sheet = workbook.Sheets["Hoja1"];

            Hng_Htmltools html = new Hng_Htmltools();
            ToolTable table = new ToolTable(10);
            string template = html.GetTemplate();
            string rows = string.Empty;
            string cols = string.Empty;

            try
            {


                //objExcel.Sheets.Add();
                //var worksheet = workbook.ActiveSheet;
                //worksheet.Name = "Libro 12.1";
                //objExcel.ActiveWindow.DisplayGridlines = false;
                //objExcel.Range["A1:ZZ10000"].Font.Name = "Calibri"; objExcel.Range["A2:ZZ10000"].Font.Size = 10;
                //objExcel.Range["A1:A7"].Font.Bold = true; objExcel.Range["A9:P11"].Font.Bold = true;
                //objExcel.Range["A1:ZZ10000"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //objExcel.Range["A9:P11"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //objExcel.Range["A1"].Font.Size = 16;
                //objExcel.Range["A1"].Font.Color = System.Drawing.ColorTranslator.FromHtml("#E8194F");

                /*-----*Cabecera*-----*/
                table.ColumnStyle();
                table.C1 = html.HeaderValue("Formato 12.1 - Registro de Inventario Permanente en unidades fisicas - Detalle del Inventario permanente en unidades fisicas");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("Periodo :");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("RUC :");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("Razón Social :");
                table.C2 = html.HeaderValue(navBarControl1.SelectedLink.Item.Caption.ToUpper());
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("Establecimiento :");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                //objExcel.Range["A1"].Value = "Formato 12.1 - Registro de Inventario Permanente en unidades fisicas - Detalle del Inventario permanente en unidades fisicas";
                //objExcel.Range["A2"].Value = "Periodo :";
                //objExcel.Range["A3"].Value = "RUC :";
                //objExcel.Range["A4"].Value = "Razón Social :";
                //objExcel.Range["B4"].Value = navBarControl1.SelectedLink.Item.Caption.ToUpper();
                //objExcel.Range["A5"].Value = "Establecimiento :";
                //  objExcel.Range["A7"].Value = "Método de Valuación :"; objExcel.Range["B7"].Value = "PROMEDIO PONDERADO DIARIO";

                table.C1 = html.HeaderValue("DOCUMENTO TRASLADO, COMPROBANTE PAGO");
                table.C8 = html.HeaderValue("ENTRADAS");
                table.C9 = html.HeaderValue("SALIDAS");
                table.C10 = html.HeaderValue("SALDO FINAL");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("DOCUMENTO INTERNO O SIMILAR");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("FECHA");
                table.C2 = html.HeaderValue("TIPO");
                table.C3 = html.HeaderValue("SERIE");
                table.C4 = html.HeaderValue("NUMERO");
                table.C5 = html.HeaderValue("TIPO DE OPERACION");
                table.C6 = html.HeaderValue("ALMACEN");
                cols += html.GetConcatRows(table);
                table.RowStyle();

                template = template.Replace("@cols", cols);
                //objExcel.Range["A9"].Value = "DOCUMENTO TRASLADO, COMPROBANTE PAGO";
                //objExcel.Range["A10"].Value = "DOCUMENTO INTERNO O SIMILAR";
                //objExcel.Range["A11"].Value = "FECHA"; objExcel.Range["B11"].Value = "TIPO"; objExcel.Range["C11"].Value = "SERIE";
                //objExcel.Range["D11"].Value = "NUMERO"; objExcel.Range["E11"].Value = "TIPO DE OPERACION"; objExcel.Range["F11"].Value = "ALMACEN";
                //worksheet.Range["A9:G9"].MergeCells = true; worksheet.Range["A10:G10"].MergeCells = true; worksheet.Range["F11:G11"].MergeCells = true;
                //objExcel.Range["H9"].Value = "ENTRADAS";
                //worksheet.Range["H9:H11"].MergeCells = true;
                //objExcel.Range["I9"].Value = "SALIDAS";
                //worksheet.Range["I9:I11"].MergeCells = true;
                //objExcel.Range["J9"].Value = "SALDO FINAL";
                //worksheet.Range["J9:J11"].MergeCells = true;

                //objExcel.Range["A9:J11"].Font.Color = Color.White;
                //objExcel.Range["A9:J11"].Select();
                //objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                //objExcel.Selection.Interior.Color = Program.Sesion.Colores.Verde;


                //objExcel.Range["A:A"].ColumnWidth = 17; objExcel.Range["B:B"].ColumnWidth = 7; objExcel.Range["C:C"].ColumnWidth = 7;
                //objExcel.Range["D:D"].ColumnWidth = 15; objExcel.Range["E:E"].ColumnWidth = 23; objExcel.Range["F:F"].ColumnWidth = 6;
                //objExcel.Range["G:G"].ColumnWidth = 25; objExcel.Range["H:I"].ColumnWidth = 11; objExcel.Range["J:J"].ColumnWidth = 15;
                //objExcel.Range["K:L"].ColumnWidth = 11; objExcel.Range["M:M"].ColumnWidth = 15; objExcel.Range["N:O"].ColumnWidth = 11;
                //objExcel.Range["P:P"].ColumnWidth = 15;
                //objExcel.Range["B:G"].NumberFormat = "@";

                int fila = 10;// verificar en que linea comienza las filas...

                List<eAlmacen.eReporteInventario> eLista = new List<eAlmacen.eReporteInventario>();
                //eLista = unit.Logistica.Reporte_InventariounidadesFisicas<eAlmacen.eReporteInventario>(lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio, FechaFin);
                eLista = unit.Logistica.Obtener_ReporteLogistica_InventarioPermanenteValorizado<eAlmacen.eReporteInventario>(lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio, FechaFin);
                List<eAlmacen.eReporteInventario> eListaAnt = new List<eAlmacen.eReporteInventario>(); //SALDO ANTERIOR
                eListaAnt = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eReporteInventario>(28, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio: FechaInicio);
                decimal ctd_entrada = 0, ctd_salida = 0, ctd_entradaT = 0, ctd_salidaT = 0, ctd_finalT = 0, ctd_finalS = 0;
                decimal total_entrada = 0, total_salida = 0, total_final = 0, total_entradaT = 0, total_salidaT = 0, total_finalT = 0;
                if (eLista.Count > 0)
                {
                    string producto = eLista[0].cod_producto;
                    foreach (eAlmacen.eReporteInventario eObj in eLista)
                    {
                        eAlmacen.eReporteInventario eObjAnt = new eAlmacen.eReporteInventario();
                        if (fila == 10)
                        {
                            table.C1 = html.RowValue("CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT, bold: true);
                            table.C5 = html.HeaderValue("DESCRIPCION: " + eObj.dsc_producto);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            //objExcel.Range["A" + fila].Value = "CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT; objExcel.Range["A" + fila].Font.Bold = true;
                            //objExcel.Range["E" + fila].Value = "DESCRIPCION: " + eObj.dsc_producto; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            table.C1 = html.RowValue("U. MED.: " + eObj.dsc_simbolo, bold: true);
                            table.C5 = html.HeaderValue("TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            //objExcel.Range["A" + fila].Value = "U. MED.: " + eObj.dsc_simbolo; objExcel.Range["A" + fila].Font.Bold = true;
                            //objExcel.Range["E" + fila].Value = "TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            table.C3 = html.RowValue("SALDO ANTERIOR:", bold: true, red: true);
                            table.C10 = html.RowValue(eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            //objExcel.Range["C" + fila].Value = "SALDO ANTERIOR:";
                            //objExcel.Rows[fila].Font.Bold = true;
                            //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            eObjAnt = eListaAnt.Find(x => x.cod_tipo_servicio == eObj.cod_tipo_servicio && x.cod_subtipo_servicio == eObj.cod_subtipo_servicio && x.cod_producto == eObj.cod_producto);
                            //objExcel.Range["J" + fila].Value = eObjAnt == null ? 0 : eObjAnt.cantidad_final;
                            ctd_finalS = ctd_finalS + (eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                        }
                        if (eObj.cod_producto != producto)
                        {
                            fila = fila + 1;
                            //objExcel.Range["B" + fila].Value = "Total Movimiento :";
                            //objExcel.Rows[fila].Font.Bold = true;
                            //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#0070C0");
                            //objExcel.Range["H" + fila].Value = ctd_entrada;
                            //objExcel.Range["I" + fila].Value = ctd_salida;

                            table.C2 = html.RowValue("Total Movimiento :", bold: true, blue: true);
                            table.C8 = html.RowValue(ctd_entrada);
                            table.C9 = html.RowValue(ctd_salida);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            ctd_entrada = 0; ctd_salida = 0; total_entrada = 0; total_salida = 0; total_final = 0;
                            fila = fila + 2;
                            //objExcel.Range["A" + fila].Value = "CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT; objExcel.Range["A" + fila].Font.Bold = true;
                            //objExcel.Range["E" + fila].Value = "DESCRIPCION: " + eObj.dsc_producto; objExcel.Range["E" + fila].Font.Bold = true;

                            table.C1 = html.RowValue("CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT, bold: true);
                            table.C5 = html.RowValue("DESCRIPCION: " + eObj.dsc_producto);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                            fila = fila + 1;


                            table.C1 = html.RowValue("U. MED.: " + eObj.dsc_simbolo, bold: true);
                            table.C5 = html.RowValue("TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                            //objExcel.Range["A" + fila].Value = "U. MED.: " + eObj.dsc_simbolo; objExcel.Range["A" + fila].Font.Bold = true;
                            //objExcel.Range["E" + fila].Value = "TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            table.C3 = html.RowValue("SALDO ANTERIOR:", bold: true, red: true);
                            table.C10 = html.RowValue(eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            //objExcel.Range["C" + fila].Value = "SALDO ANTERIOR:";
                            //objExcel.Rows[fila].Font.Bold = true;
                            //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            eObjAnt = eListaAnt.Find(x => x.cod_tipo_servicio == eObj.cod_tipo_servicio && x.cod_subtipo_servicio == eObj.cod_subtipo_servicio && x.cod_producto == eObj.cod_producto);
                            //objExcel.Range["J" + fila].Value = eObjAnt == null ? 0 : eObjAnt.cantidad_final;
                            ctd_finalS = ctd_finalS + (eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                        }

                        //rows += html.GetConcatRows(table);
                        //table.RowStyle();

                        fila = fila + 1;

                        table.C1 = html.RowValue(Convert.ToDateTime(eObj.fch_documento).ToString("dd/MM/yyyy"));
                        table.C2 = html.RowValue(eObj.tipo);
                        table.C3 = html.RowValue(eObj.serie);
                        table.C4 = html.RowValue(eObj.numero);
                        table.C5 = html.RowValue(eObj.dsc_tipo_movimiento);
                        table.C6 = html.RowValue(eObj.cod_almacen);
                        table.C7 = html.RowValue(eObj.dsc_almacen);


                        //objExcel.Range["A" + fila].Value = eObj.fch_documento;
                        //objExcel.Range["B" + fila].Value = eObj.tipo;
                        //objExcel.Range["C" + fila].Value = eObj.serie;
                        //objExcel.Range["D" + fila].Value = eObj.numero;
                        //objExcel.Range["E" + fila].Value = eObj.dsc_tipo_movimiento;
                        //objExcel.Range["F" + fila].Value = eObj.cod_almacen;
                        //objExcel.Range["G" + fila].Value = eObj.dsc_almacen;
                        if (eObj.cantidad_entrada > 0) table.C8 = html.RowValue(eObj.cantidad_entrada); //objExcel.Range["H" + fila].Value = eObj.cantidad_entrada;
                        if (eObj.cantidad_salida > 0) table.C9 = html.RowValue(eObj.cantidad_salida); //objExcel.Range["I" + fila].Value = eObj.cantidad_salida;
                        table.C10 = html.RowValue(eObj.cantidad_final);
                        rows += html.GetConcatRows(table);
                        table.RowStyle();

                        //objExcel.Range["J" + fila].Value = eObj.cantidad_final;
                        ctd_entrada = ctd_entrada + eObj.cantidad_entrada;
                        ctd_salida = ctd_salida + eObj.cantidad_salida;
                        ctd_entradaT = ctd_entradaT + eObj.cantidad_entrada;
                        ctd_salidaT = ctd_salidaT + eObj.cantidad_salida;
                        ctd_finalT = ctd_finalT + eObj.cantidad_final;
                        total_entrada = total_entrada + eObj.total_entrada;
                        total_salida = total_salida + eObj.total_salida;
                        total_final = total_final + eObj.total_final;
                        total_entradaT = total_entradaT + eObj.total_entrada;
                        total_salidaT = total_salidaT + eObj.total_salida;
                        total_finalT = total_finalT + eObj.total_final;
                        producto = eObj.cod_producto;
                    }
                }
                fila = fila + 1;

                table.C2 = html.RowValue("Total Movimiento :", bold: true, blue: true);
                table.C8 = html.RowValue(ctd_entrada);
                table.C9 = html.RowValue(ctd_salida);
                rows += html.GetConcatRows(table);
                table.RowStyle();

                rows += html.GetConcatRows(table);
                table.RowStyle();

                //objExcel.Range["B" + fila].Value = "Total Movimiento :";
                //objExcel.Rows[fila].Font.Bold = true;
                //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#0070C0");
                //objExcel.Range["H" + fila].Value = ctd_entrada;
                //objExcel.Range["I" + fila].Value = ctd_salida;
                fila = fila + 3;
                table.C2 = html.RowValue("SALDO INICIAL:", bold: true, red: true);
                table.C10 = html.RowValue(ctd_finalS);
                rows += html.GetConcatRows(table);
                table.RowStyle();

                //objExcel.Range["B" + fila].Value = "SALDO INICIAL:"; objExcel.Range["B" + fila + ":P" + fila].Font.Bold = true;
                //objExcel.Rows[fila].Font.Bold = true;
                //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                //objExcel.Range["J" + fila].Value = ctd_finalS;
                fila = fila + 1;

                table.C2 = html.RowValue("TOTALES:", bold: true);
                table.C8 = html.RowValue(ctd_entradaT);
                table.C10 = html.RowValue(total_entradaT);
                rows += html.GetConcatRows(table);
                table.RowStyle();


                template = template.Replace("@rows", rows);
                //string path = $"{Program.Sesion.Global.RutaArchivosLocalExportar}\\";
                //string excelPath = $"{path}{"Libro_12_1"}{".xlsx"}";
                string htmlContent = template;
                html.ParseHtmlToExcel(htmlContent, "Libro_12_1", Tools.Hng_Htmltools.HtmlExportFormat.Libro_12_1);

                //objExcel.Range["B" + fila].Value = "TOTALES:"; objExcel.Range["B" + fila + ":P" + fila].Font.Bold = true;
                //objExcel.Range["H" + fila].Value = ctd_entradaT; objExcel.Range["J" + fila].Value = total_entradaT;

                //objExcel.Range["H13:P" + fila].NumberFormat = "#,##0.0000";
                //objExcel.Rows[13].Delete();

                //sheet.Delete();
                //objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                //objExcel.Visible = true;
                //objExcel = null /* TODO Change to default(_) if this is not a reference type */;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                try
                {
                    MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SplashScreenManager.CloseForm();
                    System.Threading.Thread.CurrentThread.Abort();
                }
                catch (Exception) { }

                //objExcel.ActiveWorkbook.Saved = true;
                //objExcel.ActiveWorkbook.Close();
                //objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                //objExcel.Quit();
            }
        }


        private void ExportDataSetToExcel(DataSet ds, string strPath)
        {
            int inHeaderLength = 3, inColumn = 0, inRow = 0;
            System.Reflection.Missing Default = System.Reflection.Missing.Value;
            strPath += @"\Excel" + DateTime.Now.ToString().Replace(':', '-') + ".xlx";
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelworkbook = excelApp.Workbooks.Add(1);
            foreach (DataTable dt in ds.Tables)
            {
                Excel.Worksheet excelworkSheet = excelworkbook.Sheets.Add(Default, excelworkbook.Sheets[excelworkbook.Sheets.Count], 1, Default);
                excelworkSheet.Name = dt.TableName;

                for (int i = 0; i < dt.Rows.Count; i++)
                    excelworkSheet.Cells[inHeaderLength + 1, i + 1] = dt.Columns[i].ColumnName.ToUpper();
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    for (int n = 0; n < dt.Columns.Count; n++)
                    {
                        inColumn = n + 1;
                        inRow = inHeaderLength + 2 + m;
                        excelworkSheet.Cells[inRow, inColumn] = dt.Rows[m].ItemArray[n].ToString();
                        if (m % 2 == 0)
                            excelworkSheet.get_Range("A" + inRow.ToString(), "G" + inRow.ToString()).Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

                    }
                }

                Excel.Range cellRang = excelworkSheet.get_Range("A1", "P3");
                cellRang.Merge(false);
                cellRang.Interior.Color = System.Drawing.Color.White;
                cellRang.Font.Color = System.Drawing.Color.Gray;
                cellRang.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                cellRang.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                cellRang.Font.Size = 12;
                excelworkSheet.Cells[1, 1] = "Formato 12.1 - Registro de Inventario Permanente en unidades fisicas - Detalle del Inventario permanente en unidades fisicas";

                cellRang = excelworkSheet.get_Range("A4", "G4");
                cellRang.Font.Bold = true;
                cellRang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                cellRang.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#ED7D31");
                excelworkSheet.get_Range("F4").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                excelworkSheet.get_Range("F5").EntireColumn.NumberFormat = "0.00";
                excelworkSheet.Columns.AutoFit();
            }
            excelApp.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Worksheet lastWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkbook.Worksheets[1];
            lastWorkSheet.Delete();
            excelApp.DisplayAlerts = true;

            (excelworkbook.Sheets[1] as Excel._Worksheet).Activate();
            //excelworkbook.SaveAs(strPath, Default, Default, Default, false, Default, officeExcel.XlSaveAsAccessMode.xlNoChange, Default, Default, Default, Default, Default);
            excelworkbook.Close();
            excelApp.Quit();
            MessageBox.Show("EXCEL GENERADO " + strPath);
        }

        private void btnExportarLibro13_1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            frmRangoFecha frm = new frmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;
            ExportarLibro13_1(frm.fechaInicio, frm.fechaFin);
        }

        private void ExportarLibro13_1(DateTime FechaInicio, DateTime FechaFin)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");
            //string ListSeparator = "";

            //string entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
            //string server = unit.Encripta.Desencrypta(entorno == "LOCAL" ? ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString() : ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
            //string bd = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
            //string user = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("UserID")].ToString());
            //string pass = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("Password")].ToString());
            //string AppName = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("AppName")].ToString());

            //string cnxl = "ODBC;DRIVER=SQL Server;SERVER=" + server + ";UID=" + user + ";PWD=" + pass + ";APP=SGI_Excel;DATABASE=" + bd + "";
            ////string procedure = "";

            //ListSeparator = ConfigurationManager.AppSettings["ListSeparator"];
            //Excel.Application objExcel = new Excel.Application();
            //objExcel.Workbooks.Add();
            ////objExcel.Visible = true;
            //var workbook = objExcel.ActiveWorkbook;
            //var sheet = workbook.Sheets["Hoja1"];
            Hng_Htmltools html = new Hng_Htmltools();
            ToolTable table = new ToolTable(16);
            string template = html.GetTemplate();
            string rows = string.Empty;
            string cols = string.Empty;


            try
            {
                table.ColumnStyle();


                //objExcel.Sheets.Add();
                //var worksheet = workbook.ActiveSheet;
                //worksheet.Name = "Libro 13.1";
                //objExcel.ActiveWindow.DisplayGridlines = false;
                //objExcel.Range["A1:ZZ10000"].Font.Name = "Calibri"; objExcel.Range["A2:ZZ10000"].Font.Size = 10;
                //objExcel.Range["A1:A7"].Font.Bold = true; objExcel.Range["A9:P11"].Font.Bold = true;
                //objExcel.Range["A1:ZZ10000"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //objExcel.Range["A9:P11"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //objExcel.Range["A1"].Font.Size = 16;
                //objExcel.Range["A1"].Font.Color = System.Drawing.ColorTranslator.FromHtml("#E8194F");

                eEmpresa eEmp = unit.Logistica.Obtener_DatosLogistica<eEmpresa>(29, "", cod_empresa);
                //objExcel.Range["A1"].Value = "Formato 13.1 - Registro de Inventario Permanente Valorizado - Detalle del Inventario Valorizado";
                table.C1 = html.HeaderValue("Formato 13.1 - Registro de Inventario Permanente Valorizado - Detalle del Inventario Valorizado");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("Periodo :");
                table.C2 = html.HeaderValue(FechaInicio.ToString("dd/MM/yyyy") + " al " + FechaFin.ToString("dd/MM/yyyy"));
                cols += html.GetConcatRows(table);
                table.ColumnStyle();
                //objExcel.Range["A2"].Value = "Periodo :"; objExcel.Range["B2"].Value = FechaInicio.ToString("dd/MM/yyyy") + " al " + FechaFin.ToString("dd/MM/yyyy");
                table.C1 = html.HeaderValue("RUC :");
                table.C2 = html.HeaderValue(eEmp.dsc_ruc);
                cols += html.GetConcatRows(table);
                table.ColumnStyle();
                //objExcel.Range["A3"].Value = "RUC :"; objExcel.Range["B3"].NumberFormat = "@"; objExcel.Range["B3"].Value = eEmp.dsc_ruc;
                table.C1 = html.HeaderValue("Razón Social :");
                table.C2 = html.HeaderValue(eEmp.dsc_empresa.ToUpper());
                cols += html.GetConcatRows(table);
                table.ColumnStyle();
                //objExcel.Range["A4"].Value = "Razón Social :"; objExcel.Range["B4"].Value = eEmp.dsc_empresa.ToUpper();

                table.C1 = html.HeaderValue("Expresado en :");
                table.C2 = html.HeaderValue("SOLES");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();
                //objExcel.Range["A5"].Value = "Expresado en :"; objExcel.Range["B5"].Value = "SOLES"; objExcel.Range["A6"].Value = "Establecimiento :";

                table.C1 = html.HeaderValue("Establecimiento :");
                table.C2 = html.HeaderValue("");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();
                // objExcel.Range["A7"].Value = "Método de Valuación :"; objExcel.Range["B7"].Value = "PROMEDIO PONDERADO DIARIO";
                table.C1 = html.HeaderValue("Método de Valuación :");
                table.C2 = html.HeaderValue("PROMEDIO PONDERADO DIARIO");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("DOCUMENTO TRASLADO, COMPROBANTE PAGO");
                table.C8 = html.HeaderValue("ENTRADAS");
                table.C11 = html.HeaderValue("SALIDAS");
                table.C14 = html.HeaderValue("SALDO FINAL");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("DOCUMENTO INTERNO O SIMILAR");
                table.C8 = html.HeaderValue("CANTIDAD");
                table.C9 = html.HeaderValue("COSTO UNITARIO");
                table.C10 = html.HeaderValue("COSTO TOTAL");
                table.C11 = html.HeaderValue("CANTIDAD");
                table.C12 = html.HeaderValue("COSTO UNITARIO");
                table.C13 = html.HeaderValue("COSTO TOTAL");
                table.C14 = html.HeaderValue("CANTIDAD");
                table.C15 = html.HeaderValue("COSTO UNITARIO");
                table.C16 = html.HeaderValue("COSTO TOTAL");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                //objExcel.Range["A9"].Value = "DOCUMENTO TRASLADO, COMPROBANTE PAGO";
                //objExcel.Range["A10"].Value = "DOCUMENTO INTERNO O SIMILAR";
                //objExcel.Range["A11"].Value = "FECHA"; objExcel.Range["B11"].Value = "TIPO"; objExcel.Range["C11"].Value = "SERIE";
                //objExcel.Range["D11"].Value = "NUMERO"; objExcel.Range["E11"].Value = "TIPO DE OPERACION"; objExcel.Range["F11"].Value = "ALMACEN";
                //worksheet.Range["A9:G9"].MergeCells = true; worksheet.Range["A10:G10"].MergeCells = true; worksheet.Range["F11:G11"].MergeCells = true;
                //objExcel.Range["H9"].Value = "ENTRADAS"; objExcel.Range["H10"].Value = "CANTIDAD"; objExcel.Range["I10"].Value = "COSTO UNITARIO";
                //objExcel.Range["J10"].Value = "COSTO TOTAL";
                //worksheet.Range["H9:J9"].MergeCells = true; worksheet.Range["H10:H11"].MergeCells = true;
                //worksheet.Range["I10:I11"].MergeCells = true; worksheet.Range["J10:J11"].MergeCells = true;
                //objExcel.Range["K9"].Value = "SALIDAS"; objExcel.Range["K10"].Value = "CANTIDAD"; objExcel.Range["L10"].Value = "COSTO UNITARIO";
                //objExcel.Range["M10"].Value = "COSTO TOTAL";
                //worksheet.Range["K9:M9"].MergeCells = true; worksheet.Range["K10:K11"].MergeCells = true;
                //worksheet.Range["L10:L11"].MergeCells = true; worksheet.Range["M10:M11"].MergeCells = true;
                //objExcel.Range["N9"].Value = "SALDO FINAL"; objExcel.Range["N10"].Value = "CANTIDAD"; objExcel.Range["O10"].Value = "COSTO UNITARIO";
                //objExcel.Range["P10"].Value = "COSTO TOTAL";
                //worksheet.Range["N9:P9"].MergeCells = true; worksheet.Range["N10:N11"].MergeCells = true;
                //worksheet.Range["O10:O11"].MergeCells = true; worksheet.Range["P10:P11"].MergeCells = true;
                //worksheet.Range["I10:I11"].WrapText = true; worksheet.Range["L10:L11"].WrapText = true; worksheet.Range["O10:O11"].WrapText = true;

                table.C1 = html.HeaderValue("FECHA");
                table.C2 = html.HeaderValue("TIPO");
                table.C3 = html.HeaderValue("SERIE");
                table.C4 = html.HeaderValue("NUMERO");
                table.C5 = html.HeaderValue("TIPO DE OPERACIÓN");
                table.C6 = html.HeaderValue("ALMACÉN");

                cols += html.GetConcatRows(table);
                table.RowStyle();

                template = template.Replace("@cols", cols);

                //objExcel.Range["A9:P11"].Font.Color = Color.White;
                //objExcel.Range["A9:P11"].Select();
                //objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);
                //objExcel.Selection.Interior.Color = Program.Sesion.Colores.Verde;

                //objExcel.Range["A:A"].ColumnWidth = 17; objExcel.Range["B:B"].ColumnWidth = 7; objExcel.Range["C:C"].ColumnWidth = 7;
                //objExcel.Range["D:D"].ColumnWidth = 15; objExcel.Range["E:E"].ColumnWidth = 23; objExcel.Range["F:F"].ColumnWidth = 6;
                //objExcel.Range["G:G"].ColumnWidth = 25; objExcel.Range["H:I"].ColumnWidth = 11; objExcel.Range["J:J"].ColumnWidth = 15;
                //objExcel.Range["K:L"].ColumnWidth = 11; objExcel.Range["M:M"].ColumnWidth = 15; objExcel.Range["N:O"].ColumnWidth = 11;
                //objExcel.Range["P:P"].ColumnWidth = 15; objExcel.Range["B:G"].NumberFormat = "@";

                int fila = 13;
                //procedure = "usp_Reporte_Logistica_InventarioPermanenteValorizado";
                ////procedure = "usp_Reporte_Logistica_InventarioPermanenteValorizado @cod_almacen = '" + lkpAlmacen.EditValue.ToString() +
                ////                                    "', @cod_empresa = '" + cod_empresa +
                ////                                    "', @cod_sede_empresa = '" + lkpSedeEmpresa.EditValue.ToString() +
                ////                                    "', @FechaInicio = '" + FechaInicio + //Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd") +
                ////                                    "', @FechaFin = '" + FechaFin + "'";
                //unit.Logistica.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + fila, true);
                //fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                //    System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

                List<eAlmacen.eReporteInventario> eLista = new List<eAlmacen.eReporteInventario>(); //LISTA PRODUCTO
                eLista = unit.Logistica.Obtener_ReporteLogistica_InventarioPermanenteValorizado<eAlmacen.eReporteInventario>(lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio.ToString("yyyyMMdd"), FechaFin.ToString("yyyyMMdd"));
                List<eAlmacen.eReporteInventario> eListaAnt = new List<eAlmacen.eReporteInventario>(); //SALDO ANTERIOR
                eListaAnt = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eReporteInventario>(28, lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), FechaInicio: FechaInicio.ToString("yyyyMMdd"));
                decimal ctd_entrada = 0, ctd_salida = 0, ctd_entradaT = 0, ctd_salidaT = 0, ctd_finalT = 0, ctd_finalS = 0;
                decimal total_entrada = 0, total_salida = 0, total_final = 0, total_entradaT = 0, total_salidaT = 0, total_finalT = 0, total_finalS = 0;
                if (eLista.Count > 0)
                {
                    string producto = eLista[0].cod_producto;
                    foreach (eAlmacen.eReporteInventario eObj in eLista)
                    {
                        eAlmacen.eReporteInventario eObjAnt = new eAlmacen.eReporteInventario();
                        if (fila == 13)
                        {
                            table.C1 = html.RowValue("CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT, bold: true);
                            table.C5 = html.RowValue("DESCRIPCION: " + eObj.dsc_producto);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            //objExcel.Range["A" + fila].Value = "CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT; objExcel.Range["A" + fila].Font.Bold = true;
                            //objExcel.Range["E" + fila].Value = "DESCRIPCION: " + eObj.dsc_producto; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            table.C1 = html.RowValue("U. MED.: " + eObj.dsc_simbolo, bold: true);
                            table.C5 = html.RowValue("TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                            //objExcel.Range["A" + fila].Value = "U. MED.: " + eObj.dsc_simbolo; objExcel.Range["A" + fila].Font.Bold = true;
                            //objExcel.Range["E" + fila].Value = "TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            table.C3 = html.RowValue("SALDO ANTERIOR:", bold: true, red: true);
                            table.C14 = html.RowValue(eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                            table.C15 = html.RowValue(eObjAnt == null ? 0 : eObjAnt.costo_ponderado);
                            table.C16 = html.RowValue(eObjAnt == null ? 0 : eObjAnt.total_final);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            //objExcel.Range["C" + fila].Value = "SALDO ANTERIOR:";
                            //objExcel.Rows[fila].Font.Bold = true;
                            //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            eObjAnt = eListaAnt.Find(x => x.cod_tipo_servicio == eObj.cod_tipo_servicio && x.cod_subtipo_servicio == eObj.cod_subtipo_servicio && x.cod_producto == eObj.cod_producto);
                            //objExcel.Range["N" + fila].Value = eObjAnt == null ? 0 : eObjAnt.cantidad_final;
                            //objExcel.Range["O" + fila].Value = eObjAnt == null ? 0 : eObjAnt.costo_ponderado;
                            //objExcel.Range["P" + fila].Value = eObjAnt == null ? 0 : eObjAnt.total_final;
                            ctd_finalS = ctd_finalS + (eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                            total_finalS = total_finalS + (eObjAnt == null ? 0 : eObjAnt.total_final);
                        }
                        if (eObj.cod_producto != producto)
                        {
                            fila = fila + 1;
                            table.C2 = html.RowValue("Total Movimiento :", bold: true, blue: true);
                            table.C8 = html.RowValue(ctd_entrada);
                            table.C10 = html.RowValue(total_entrada);
                            table.C11 = html.RowValue(ctd_salida);
                            table.C13 = html.RowValue(total_salida);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            //objExcel.Range["B" + fila].Value = "Total Movimiento :";
                            //objExcel.Rows[fila].Font.Bold = true;
                            //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#0070C0");
                            //objExcel.Range["H" + fila].Value = ctd_entrada; objExcel.Range["J" + fila].Value = total_entrada;
                            //objExcel.Range["K" + fila].Value = ctd_salida; objExcel.Range["M" + fila].Value = total_salida;

                            ctd_entrada = 0; ctd_salida = 0; total_entrada = 0; total_salida = 0; total_final = 0;
                            fila = fila + 2;
                            table.C1 = html.RowValue("CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT, bold: true);
                            table.C5 = html.RowValue("DESCRIPCION: " + eObj.dsc_producto);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();

                            //objExcel.Range["A" + fila].Value = "CODIGO EXISTENCIA: " + eObj.cod_producto_SUNAT; objExcel.Range["A" + fila].Font.Bold = true;
                            //objExcel.Range["E" + fila].Value = "DESCRIPCION: " + eObj.dsc_producto; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            table.C1 = html.RowValue("U. MED.: " + eObj.dsc_simbolo, bold: true);
                            table.C5 = html.RowValue("TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                            //objExcel.Range["A" + fila].Value = "U. MED.: " + eObj.dsc_simbolo; objExcel.Range["A" + fila].Font.Bold = true;
                            //objExcel.Range["E" + fila].Value = "TIPO EXISTENCIA: " + eObj.dsc_tipo_servicio; objExcel.Range["E" + fila].Font.Bold = true;
                            fila = fila + 1;
                            table.C3 = html.RowValue("SALDO ANTERIOR:", bold: true, red: true);
                            table.C14 = html.RowValue(eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                            table.C15 = html.RowValue(eObjAnt == null ? 0 : eObjAnt.costo_ponderado);
                            table.C16 = html.RowValue(eObjAnt == null ? 0 : eObjAnt.total_final);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                            //objExcel.Range["C" + fila].Value = "SALDO ANTERIOR:";
                            //objExcel.Rows[fila].Font.Bold = true;
                            //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            eObjAnt = eListaAnt.Find(x => x.cod_tipo_servicio == eObj.cod_tipo_servicio && x.cod_subtipo_servicio == eObj.cod_subtipo_servicio && x.cod_producto == eObj.cod_producto);
                            //objExcel.Range["N" + fila].Value = eObjAnt == null ? 0 : eObjAnt.cantidad_final;
                            //objExcel.Range["O" + fila].Value = eObjAnt == null ? 0 : eObjAnt.costo_ponderado;
                            //objExcel.Range["P" + fila].Value = eObjAnt == null ? 0 : eObjAnt.total_final;
                            ctd_finalS = ctd_finalS + (eObjAnt == null ? 0 : eObjAnt.cantidad_final);
                            total_finalS = total_finalS + (eObjAnt == null ? 0 : eObjAnt.total_final);
                        }

                        fila = fila + 1;
                        table.C1 = html.RowValue(eObj.fch_documento);
                        table.C2 = html.RowValue(eObj.tipo_documento);
                        table.C3 = html.RowValue(eObj.serie_documento);
                        table.C4 = html.RowValue(eObj.numero_documento);
                        table.C5 = html.RowValue(eObj.dsc_tipo_movimiento);
                        table.C6 = html.RowValue(eObj.cod_almacen);
                        table.C7 = html.RowValue(eObj.dsc_almacen);

                        //objExcel.Range["A" + fila].Value = eObj.fch_documento;
                        //objExcel.Range["B" + fila].Value = eObj.tipo;
                        //objExcel.Range["C" + fila].Value = eObj.serie;
                        //objExcel.Range["D" + fila].Value = eObj.numero;
                        //objExcel.Range["E" + fila].Value = eObj.dsc_tipo_movimiento;
                        //objExcel.Range["F" + fila].Value = eObj.cod_almacen;
                        //objExcel.Range["G" + fila].Value = eObj.dsc_almacen;
                        if (eObj.cantidad_entrada > 0) table.C8 = html.RowValue(eObj.cantidad_entrada);// objExcel.Range["H" + fila].Value = eObj.cantidad_entrada;
                        if (eObj.costo_entrada > 0) table.C9 = html.RowValue(eObj.costo_entrada);//objExcel.Range["I" + fila].Value = eObj.costo_entrada;
                        if (eObj.total_entrada > 0) table.C10 = html.RowValue(eObj.total_entrada);//objExcel.Range["J" + fila].Value = eObj.total_entrada;
                        if (eObj.cantidad_salida > 0) table.C11 = html.RowValue(eObj.cantidad_salida);// objExcel.Range["K" + fila].Value = eObj.cantidad_salida;
                        if (eObj.costo_salida > 0) table.C12 = html.RowValue(eObj.costo_salida);// objExcel.Range["L" + fila].Value = eObj.costo_salida;
                        if (eObj.total_salida > 0) table.C13 = html.RowValue(eObj.total_salida);// objExcel.Range["M" + fila].Value = eObj.total_salida;
                        //objExcel.Range["N" + fila].Value = eObj.cantidad_final;
                        //objExcel.Range["O" + fila].Value = eObj.costo_ponderado;
                        //objExcel.Range["P" + fila].Value = eObj.total_final;

                        table.C14 = html.RowValue(eObj.cantidad_final);
                        table.C15 = html.RowValue(eObj.costo_ponderado);
                        table.C16 = html.RowValue(eObj.total_final);
                        rows += html.GetConcatRows(table);
                        table.RowStyle();

                        ctd_entrada = ctd_entrada + eObj.cantidad_entrada;
                        ctd_salida = ctd_salida + eObj.cantidad_salida;
                        ctd_entradaT = ctd_entradaT + eObj.cantidad_entrada;
                        ctd_salidaT = ctd_salidaT + eObj.cantidad_salida;
                        ctd_finalT = ctd_finalT + eObj.cantidad_final;
                        total_entrada = total_entrada + eObj.total_entrada;
                        total_salida = total_salida + eObj.total_salida;
                        total_final = total_final + eObj.total_final;
                        total_entradaT = total_entradaT + eObj.total_entrada;
                        total_salidaT = total_salidaT + eObj.total_salida;
                        total_finalT = total_finalT + eObj.total_final;
                        producto = eObj.cod_producto;
                    }
                }
                fila = fila + 1;
                table.C2 = html.RowValue("Total Movimiento :", bold: true, blue: true);
                table.C8 = html.RowValue(ctd_entrada);
                table.C10 = html.RowValue(total_entrada);
                table.C11 = html.RowValue(ctd_salida);
                table.C13 = html.RowValue(total_salida);
                rows += html.GetConcatRows(table);
                table.RowStyle();
                rows += html.GetConcatRows(table);
                table.RowStyle();

                //objExcel.Range["B" + fila].Value = "Total Movimiento :";
                //objExcel.Rows[fila].Font.Bold = true;
                //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#0070C0");
                //objExcel.Range["H" + fila].Value = ctd_entrada; objExcel.Range["J" + fila].Value = total_entrada;
                //objExcel.Range["K" + fila].Value = ctd_salida; objExcel.Range["M" + fila].Value = total_salida;
                fila = fila + 3;
                table.C2 = html.RowValue("SALDO INICIAL:", bold: true, red: true);
                table.C14 = html.RowValue(ctd_finalS);
                table.C16 = html.RowValue(total_finalS);
                rows += html.GetConcatRows(table);
                table.RowStyle();

                //objExcel.Range["B" + fila].Value = "SALDO INICIAL:";
                //objExcel.Rows[fila].Font.Bold = true;
                //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                //objExcel.Range["N" + fila].Value = ctd_finalS; objExcel.Range["P" + fila].Value = total_finalS;
                fila = fila + 1;
                table.C2 = html.RowValue("TOTALES:", bold: true);
                table.C8 = html.RowValue(ctd_entradaT);
                table.C10 = html.RowValue(total_entradaT);
                table.C11 = html.RowValue(ctd_salidaT);
                table.C13 = html.RowValue(total_salidaT);
                table.C14 = html.RowValue(ctd_finalT);
                table.C16 = html.RowValue(total_finalT);
                rows += html.GetConcatRows(table);
                table.RowStyle();


                template = template.Replace("@rows", rows);
                //string path = $"{Program.Sesion.Global.RutaArchivosLocalExportar}\\";
                //string excelPath = $"{path}{"Libro_13_1"}{".xlsx"}";
                string htmlContent = template;
                html.ParseHtmlToExcel(htmlContent, "Libro_13_1", Tools.Hng_Htmltools.HtmlExportFormat.Libro_13_1);


                //objExcel.Range["B" + fila].Value = "TOTALES:"; objExcel.Range["B" + fila + ":P" + fila].Font.Bold = true;
                //objExcel.Range["H" + fila].Value = ctd_entradaT; objExcel.Range["J" + fila].Value = total_entradaT;
                //objExcel.Range["K" + fila].Value = ctd_salidaT; objExcel.Range["M" + fila].Value = total_salidaT;
                //objExcel.Range["N" + fila].Value = ctd_finalT; objExcel.Range["P" + fila].Value = total_finalT;
                //objExcel.Range["H13:P" + fila].NumberFormat = "#,##0.0000";
                //objExcel.Rows[13].Delete();

                //sheet.Delete();
                //objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                //objExcel.Visible = true;
                //objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                //objExcel.ActiveWorkbook.Saved = true;
                //objExcel.ActiveWorkbook.Close();
                //objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                //objExcel.Quit();
                try
                {
                    SplashScreenManager.CloseForm();
                    MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    System.Threading.Thread.CurrentThread.Abort();
                }
                catch (Exception) { }
            }
        }

        private void btnExportarKardexValorizado_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            frmRangoFecha frm = new frmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;

            DataTable dtGeneral = unit.Logistica.ReporteKardex(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));
            DataTable dtSaldo = unit.Logistica.ReporteKardex_Saldo(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"));

            dtGeneral.Merge(dtSaldo);

            DataView dvKardex = dtGeneral.DefaultView;
            dvKardex.Sort = "cod_producto, Fecha, fch_registro ASC";
            DataTable dtKardex = dvKardex.ToTable();

            GenerarReporteKardex(dtKardex, frm.fechaInicio, frm.fechaFin);
        }

        private void GenerarReporteKardex(DataTable dtKardex, DateTime fechaInicio, DateTime fechaFin)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");



            //Excel.Application objExcel = new Excel.Application();
            //objExcel.Workbooks.Add();

            //var workbook = objExcel.ActiveWorkbook;
            //var sheet = workbook.Sheets["Hoja1"];
            //objExcel.Visible = true;

            try
            {
                Hng_Htmltools html = new Hng_Htmltools();
                ToolTable table = new ToolTable(15);
                string template = html.GetTemplate();
                string cols = string.Empty;
                string rows = string.Empty;

                //objExcel.Sheets.Add();
                //var worksheet = workbook.ActiveSheet;
                //worksheet.Name = "Kardex";

                //objExcel.ActiveWindow.DisplayGridlines = false;
                //objExcel.Range["B:G"].NumberFormat = "@";
                //objExcel.Range["G:G"].ColumnWidth = 34;
                //objExcel.Range["B:B"].ColumnWidth = 6;
                //objExcel.Range["C:C"].ColumnWidth = 6;
                //objExcel.Range["D:D"].ColumnWidth = 8;
                //objExcel.Range["E:E"].ColumnWidth = 8;
                //objExcel.Range["A4:O5"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //objExcel.Range["A4:O5"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                string mesInicio = mesEnLetras(fechaInicio);
                string mesFin = mesEnLetras(fechaFin);


                table.ColumnStyle();
                table.C1 = html.HeaderValue(navBarControl1.SelectedLink.Item.Caption.ToString());
                table.C7 = html.HeaderValue("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + ", Hora: " + DateTime.Now.ToString("hh:mm:ss"));
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("ALPROM23");
                table.C4 = html.HeaderValue("MOVIMIENTO DE EXISTENCIAS POR ARTICULO - DE " + mesInicio + " DEL " + fechaInicio.Year.ToString() + " A " + mesFin + " DEL " + fechaFin.Year.ToString());
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C4 = html.HeaderValue("MONEDA :");
                table.C5 = html.HeaderValue("MONEDA NACIONAL");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();


                //objExcel.Cells[1, 1] = navBarControl1.SelectedLink.Item.Caption.ToString();
                //objExcel.Cells[2, 1] = "ALPROM23";



                //objExcel.Cells[2, 4] = "MOVIMIENTO DE EXISTENCIAS POR ARTICULO - DE " + mesInicio + " DEL " + fechaInicio.Year.ToString() + " A " + mesFin + " DEL " + fechaFin.Year.ToString();
                //objExcel.Cells[3, 6] = "MONEDA :";
                //objExcel.Cells[3, 7] = "MONEDA NACIONAL";

                table.C1 = html.HeaderValue("Fecha");
                table.C2 = html.HeaderValue("Tipo Doc.");
                table.C3 = html.HeaderValue("Tipo Mov.");
                table.C4 = html.HeaderValue("Almacen");
                table.C5 = html.HeaderValue("Nro. Doc.");
                table.C6 = html.HeaderValue("Doc. Ref");
                table.C7 = html.HeaderValue("Proveedor/Cliente");
                table.C8 = html.HeaderValue("Precio Unitario");
                table.C9 = html.HeaderValue("*****E N T R A D A******");
                table.C11 = html.HeaderValue("******S A L I D A*******");
                table.C13 = html.HeaderValue("*******S A L D O********");
                table.C15 = html.HeaderValue("Glosa");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                //worksheet.Range["A4:A5"].MergeCells = true; objExcel.Cells[4, 1] = "Fecha";
                //worksheet.Range["B4:B5"].MergeCells = true; objExcel.Cells[4, 2] = "Tipo Doc."; worksheet.Range["B4:B5"].WrapText = true;
                //worksheet.Range["C4:C5"].MergeCells = true; objExcel.Cells[4, 3] = "Tipo Mov."; worksheet.Range["C4:C5"].WrapText = true;
                //worksheet.Range["D4:D5"].MergeCells = true; objExcel.Cells[4, 4] = "Almacen";
                //worksheet.Range["E4:E5"].MergeCells = true; objExcel.Cells[4, 5] = "Nro. Doc.";
                //worksheet.Range["F4:F5"].MergeCells = true; objExcel.Cells[4, 6] = "Doc. Ref";
                //worksheet.Range["G4:G5"].MergeCells = true; objExcel.Cells[4, 7] = "Proveedor/Cliente";
                //worksheet.Range["H4:H5"].MergeCells = true; objExcel.Cells[4, 8] = "Precio Unitario"; worksheet.Range["H4:H5"].WrapText = true;
                //worksheet.Range["I4:J4"].MergeCells = true; objExcel.Cells[4, 9] = "*****E N T R A D A******";
                ////J
                //worksheet.Range["K4:L4"].MergeCells = true; objExcel.Cells[4, 11] = "******S A L I D A*******";
                //worksheet.Range["M4:N4"].MergeCells = true; objExcel.Cells[4, 13] = "*******S A L D O********";
                ////N
                //worksheet.Range["O4:O5"].MergeCells = true; objExcel.Cells[4, 15] = "Glosa";

                table.C9 = html.HeaderValue("Cantidad");
                table.C10 = html.HeaderValue("P.T.Doc.");
                table.C11 = html.HeaderValue("Cantidad");
                table.C12 = html.HeaderValue("P.T.Doc.");
                table.C13 = html.HeaderValue("Cantidad");
                table.C14 = html.HeaderValue("P.T.Doc.");
                cols += html.GetConcatRows(table);
                table.RowStyle();

                template = template.Replace("@cols", cols);
                //objExcel.Cells[5, 9] = "Cantidad";
                //objExcel.Cells[5, 10] = "P.T.Doc.";
                //objExcel.Cells[5, 11] = "Cantidad";
                //objExcel.Cells[5, 12] = "P.T.Doc.";
                //objExcel.Cells[5, 13] = "Cantidad";
                //objExcel.Cells[5, 14] = "Importe";

                //objExcel.Cells[1, 12] = DateTime.Now.ToString("dd/MM/yyyy");
                //objExcel.Cells[2, 12] = DateTime.Now.ToString("hh:mm:ss t");

                int fila = 6;
                int cantidad = 0, ctd_entradas = 0, ctd_salidas = 0, ctd_saldos = 0, ctd_entradas_tot = 0, ctd_salidas_tot = 0;
                decimal importe = 0, imp_ponderado = 0, costo_salida = 0, total_entradas = 0, total_salidas = 0, total_saldos = 0, total_entradas_tot = 0, total_salidas_tot = 0;
                Boolean cambiar = true;

                for (int i = 0; i < dtKardex.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        //table.C0 = html.RowValue("Bold");
                        table.C1 = html.RowValue(dtKardex.Rows[i][9].ToString(), bold: true);
                        table.C2 = html.RowValue(dtKardex.Rows[i][10]);
                        rows += html.GetConcatRows(table);
                        table.RowStyle();

                        //objExcel.Range["A" + fila].NumberFormat = "@";
                        //objExcel.Cells[fila, 1] = dtKardex.Rows[i][9].ToString();
                        //objExcel.Cells[fila, 2] = dtKardex.Rows[i][10];

                        //objExcel.Range["A" + fila + ":B" + fila].Select();
                        //objExcel.Selection.Font.Bold = true;

                        fila = fila + 1;
                        table.C2 = html.RowValue("SALDO ANTERIOR");

                        //objExcel.Cells[fila, 2] = "SALDO ANTERIOR";
                        //objExcel.Rows[fila].Font.Bold = true;
                        //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                        table.C13 = html.RowValue(cantidad);
                        table.C14 = html.RowValue(importe);
                        //objExcel.Cells[fila, 13] = cantidad;
                        //objExcel.Cells[fila, 14] = importe;

                        if (dtKardex.Rows[i][8].ToString() == "Saldo")
                        {
                            ctd_saldos = (ctd_saldos + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                            total_saldos = (total_saldos + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                            cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                            importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                            imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;


                            //objExcel.Cells[fila, 13] = cantidad;
                            //objExcel.Cells[fila, 14] = importe;
                            //objExcel.Rows[fila].Font.Bold = true;
                            //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");

                            table.C13 = html.RowValue(cantidad);
                            table.C14 = html.RowValue(importe);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                        }
                        else
                        {
                            //table.C0 = html.RowValue("Bold-Red");
                            table.C1 = html.RowValue("Saldo Inicial:", bold: true, red: true);
                            table.C2 = html.RowValue("");
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                            //objExcel.Cells[fila, 1] = "Saldo Inicial:";
                            //objExcel.Rows[fila].Font.Bold = true;
                            //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            //objExcel.Cells[fila, 2] = "";


                            fila = fila + 1;
                            table.C1 = html.RowValue(Convert.ToDateTime(dtKardex.Rows[i][2]).ToString("dd/MM/yyyy"));
                            table.C2 = html.RowValue(dtKardex.Rows[i][3]);
                            table.C3 = html.RowValue(dtKardex.Rows[i][4]);
                            table.C4 = html.RowValue(dtKardex.Rows[i][5]);
                            table.C5 = html.RowValue(dtKardex.Rows[i][6]);
                            table.C6 = html.RowValue(dtKardex.Rows[i][7]);
                            table.C7 = html.RowValue(dtKardex.Rows[i][8]);
                            table.C8 = html.RowValue(dtKardex.Rows[i][11]);
                            table.C9 = html.RowValue(dtKardex.Rows[i][12]);
                            table.C10 = html.RowValue(dtKardex.Rows[i][13]);
                            table.C11 = html.RowValue(dtKardex.Rows[i][14]);
                            table.C12 = html.RowValue(dtKardex.Rows[i][15]);

                            //objExcel.Cells[fila, 1] = dtKardex.Rows[i][2];
                            //objExcel.Cells[fila, 2] = dtKardex.Rows[i][3];
                            //objExcel.Cells[fila, 3] = dtKardex.Rows[i][4];
                            //objExcel.Cells[fila, 4] = dtKardex.Rows[i][5];
                            //objExcel.Cells[fila, 5] = dtKardex.Rows[i][6];
                            //objExcel.Cells[fila, 6] = dtKardex.Rows[i][7];
                            //objExcel.Cells[fila, 7] = dtKardex.Rows[i][8];
                            //objExcel.Cells[fila, 8] = dtKardex.Rows[i][11];
                            //objExcel.Cells[fila, 9] = dtKardex.Rows[i][12];
                            //objExcel.Cells[fila, 10] = dtKardex.Rows[i][13];
                            //objExcel.Cells[fila, 11] = dtKardex.Rows[i][14];
                            //objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];

                            cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                            if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                            if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                            {
                                table.C8 = html.RowValue("");
                                //objExcel.Cells[fila, 8] = "";
                                costo_salida = imp_ponderado;
                                dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                //objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];
                                table.C12 = html.RowValue(dtKardex.Rows[i][15]);
                                importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                            }
                            imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                            ctd_entradas = ctd_entradas + Convert.ToInt32(dtKardex.Rows[i][12]); ctd_entradas_tot = ctd_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][12]);
                            total_entradas = total_entradas + Convert.ToInt32(dtKardex.Rows[i][13]); total_entradas_tot = total_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][13]);
                            ctd_salidas = ctd_salidas + Convert.ToInt32(dtKardex.Rows[i][14]); ctd_salidas_tot = ctd_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][14]);
                            total_salidas = total_salidas + Convert.ToInt32(dtKardex.Rows[i][15]); total_salidas_tot = total_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][15]);

                            //objExcel.Cells[fila, 13] = cantidad;
                            //objExcel.Cells[fila, 14] = importe;
                            //objExcel.Cells[fila, 15] = dtKardex.Rows[i][18];
                            table.C13 = html.RowValue(cantidad);
                            table.C14 = html.RowValue(importe);
                            table.C15 = html.RowValue(dtKardex.Rows[i][18]);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                        }
                    }
                    else
                    {
                        if (dtKardex.Rows[i][9].ToString() == dtKardex.Rows[i - 1][9].ToString())
                        {
                            if (dtKardex.Rows[i][8].ToString() == "Saldo")
                            {
                                ctd_saldos = (ctd_saldos + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                total_saldos = (total_saldos + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                table.C13 = html.RowValue(cantidad);
                                table.C14 = html.RowValue(importe);
                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                                //objExcel.Cells[fila, 13] = cantidad;
                                //objExcel.Cells[fila, 14] = importe;
                                //objExcel.Rows[fila].Font.Bold = true;
                                //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            }
                            else
                            {
                                if (cambiar)
                                {
                                    if (dtKardex.Rows[i - 1][8].ToString() == "Saldo")
                                    {
                                        fila = fila + 1;
                                        rows += html.GetConcatRows(table);
                                        table.RowStyle();
                                    }

                                    //table.C0 = html.RowValue("Bold-Red");
                                    table.C1 = html.RowValue("Saldo Inicial:", bold: true, red: true);
                                    table.C2 = html.RowValue("");//

                                    //objExcel.Cells[fila - 1, 1] = "Saldo Inicial:";
                                    //objExcel.Rows[fila - 1].Font.Bold = true;
                                    //objExcel.Rows[fila - 1].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                                    //objExcel.Cells[fila - 1, 2] = "";

                                    if (dtKardex.Rows[i - 1][8].ToString() == "Saldo") { fila = fila - 1; }
                                }

                                fila = fila + 1;
                                //table.C0 = html.RowValue("");
                                table.C1 = html.RowValue(Convert.ToDateTime(dtKardex.Rows[i][2]).ToString("dd/MM/yyyy"));
                                table.C2 = html.RowValue(dtKardex.Rows[i][3]);
                                table.C3 = html.RowValue(dtKardex.Rows[i][4]);
                                table.C4 = html.RowValue(dtKardex.Rows[i][5]);
                                table.C5 = html.RowValue(dtKardex.Rows[i][6]);
                                table.C6 = html.RowValue(dtKardex.Rows[i][7]);
                                table.C7 = html.RowValue(dtKardex.Rows[i][8]);
                                table.C8 = html.RowValue(dtKardex.Rows[i][11]);
                                table.C9 = html.RowValue(dtKardex.Rows[i][12]);
                                table.C10 = html.RowValue(dtKardex.Rows[i][13]);
                                table.C11 = html.RowValue(dtKardex.Rows[i][14]);
                                table.C12 = html.RowValue(dtKardex.Rows[i][15]);

                                //objExcel.Cells[fila, 1] = dtKardex.Rows[i][2];
                                //objExcel.Cells[fila, 2] = dtKardex.Rows[i][3];
                                //objExcel.Cells[fila, 3] = dtKardex.Rows[i][4];
                                //objExcel.Cells[fila, 4] = dtKardex.Rows[i][5];
                                //objExcel.Cells[fila, 5] = dtKardex.Rows[i][6];
                                //objExcel.Cells[fila, 6] = dtKardex.Rows[i][7];
                                //objExcel.Cells[fila, 7] = dtKardex.Rows[i][8];
                                //objExcel.Cells[fila, 8] = dtKardex.Rows[i][11];
                                //objExcel.Cells[fila, 9] = dtKardex.Rows[i][12];
                                //objExcel.Cells[fila, 10] = dtKardex.Rows[i][13];
                                //objExcel.Cells[fila, 11] = dtKardex.Rows[i][14];
                                //objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                                {
                                    table.C8 = html.RowValue("");
                                    //objExcel.Cells[fila, 8] = "";
                                    costo_salida = imp_ponderado;
                                    dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                    table.C12 = html.RowValue(dtKardex.Rows[i][15]);
                                    //objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];

                                    importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                ctd_entradas = ctd_entradas + Convert.ToInt32(dtKardex.Rows[i][12]); ctd_entradas_tot = ctd_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][12]);
                                total_entradas = total_entradas + Convert.ToInt32(dtKardex.Rows[i][13]); total_entradas_tot = total_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][13]);
                                ctd_salidas = ctd_salidas + Convert.ToInt32(dtKardex.Rows[i][14]); ctd_salidas_tot = ctd_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][14]);
                                total_salidas = total_salidas + Convert.ToInt32(dtKardex.Rows[i][15]); total_salidas_tot = total_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][15]);

                                table.C13 = html.RowValue(cantidad);
                                table.C14 = html.RowValue(importe);
                                table.C15 = html.RowValue(dtKardex.Rows[i][18]);
                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                                //objExcel.Cells[fila, 13] = cantidad;
                                //objExcel.Cells[fila, 14] = importe;
                                //objExcel.Cells[fila, 15] = dtKardex.Rows[i][18];

                                cambiar = false;
                            }
                        }
                        else
                        {
                            if (dtKardex.Rows[i - 1][8].ToString() == "Saldo")
                            {
                                //table.C0 = html.RowValue("");
                                table.C1 = html.RowValue("");
                                table.C2 = html.RowValue("SALDO ANTERIOR");
                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                                //objExcel.Cells[fila, 1] = "";
                                //objExcel.Cells[fila, 2] = "SALDO ANTERIOR";
                                //objExcel.Rows[fila].Font.Bold = true;
                                //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            }
                            else
                            {
                                fila = fila + 1;

                                //table.C0 = html.RowValue("");
                                table.C2 = html.RowValue("TOTAL DE MOVIMIENTO :");
                                //objExcel.Cells[fila, 2] = "TOTAL DE MOVIMIENTO :";
                                table.C9 = html.RowValue(ctd_entradas);
                                table.C10 = html.RowValue(total_entradas);
                                table.C11 = html.RowValue(ctd_salidas);
                                table.C12 = html.RowValue(total_salidas);
                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                                //objExcel.Cells[fila, 9] = ctd_entradas;
                                //objExcel.Cells[fila, 10] = total_entradas;
                                //objExcel.Cells[fila, 11] = ctd_salidas;
                                //objExcel.Cells[fila, 12] = total_salidas;
                            }

                            fila = fila + 1;

                            cantidad = 0; importe = 0;
                            imp_ponderado = 0; costo_salida = 0;
                            ctd_entradas = 0; total_entradas = 0;
                            ctd_salidas = 0; total_salidas = 0;

                            //table.C0 = html.RowValue("Bold");
                            table.C1 = html.RowValue(dtKardex.Rows[i][9].ToString(), bold: true);
                            table.C2 = html.RowValue(dtKardex.Rows[i][10]);
                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                            //objExcel.Range["A" + fila].NumberFormat = "@";
                            //objExcel.Cells[fila, 1] = dtKardex.Rows[i][9].ToString();
                            //objExcel.Cells[fila, 2] = dtKardex.Rows[i][10];

                            //objExcel.Range["A" + fila + ":B" + fila].Select();
                            //objExcel.Selection.Font.Bold = true;

                            fila = fila + 1;

                            //table.C0 = html.RowValue("");
                            table.C2 = html.RowValue("SALDO ANTERIOR");
                            table.C13 = html.RowValue(cantidad);
                            table.C14 = html.RowValue(importe);
                            //objExcel.Cells[fila, 2] = "SALDO ANTERIOR";
                            //objExcel.Cells[fila, 13] = cantidad;
                            //objExcel.Cells[fila, 14] = importe;
                            //objExcel.Rows[fila].Font.Bold = true;
                            //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                            cambiar = true;

                            if (dtKardex.Rows[i][8].ToString() == "Saldo")
                            {
                                ctd_saldos = (ctd_saldos + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                total_saldos = (total_saldos + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());

                                if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                                {
                                    table.C8 = html.RowValue("");
                                    //objExcel.Cells[fila, 8] = "";
                                    costo_salida = imp_ponderado;
                                    dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                    table.C12 = html.RowValue(dtKardex.Rows[i][15]);
                                    //objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];
                                    importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                table.C13 = html.RowValue(cantidad);
                                table.C14 = html.RowValue(importe);
                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                                //objExcel.Cells[fila, 13] = cantidad;
                                //objExcel.Cells[fila, 14] = importe;
                                //objExcel.Rows[fila].Font.Bold = true;
                                //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");


                            }
                            else
                            {
                                //table.C0 = html.RowValue("Bold-Red");
                                table.C1 = html.RowValue("Saldo Inicial:", bold: true, red: true);
                                table.C2 = html.RowValue("");
                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                                //objExcel.Cells[fila, 1] = "Saldo Inicial:";
                                //objExcel.Rows[fila].Font.Bold = true;
                                //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                                //objExcel.Cells[fila, 2] = "";

                                fila = fila + 1;

                                //table.C0 = html.RowValue("");
                                table.C1 = html.RowValue(Convert.ToDateTime(dtKardex.Rows[i][2]).ToString("dd/MM/yyyy"));
                                table.C2 = html.RowValue(dtKardex.Rows[i][3]);
                                table.C3 = html.RowValue(dtKardex.Rows[i][4]);
                                table.C4 = html.RowValue(dtKardex.Rows[i][5]);
                                table.C5 = html.RowValue(dtKardex.Rows[i][6]);
                                table.C6 = html.RowValue(dtKardex.Rows[i][7]);
                                table.C7 = html.RowValue(dtKardex.Rows[i][8]);
                                table.C8 = html.RowValue(dtKardex.Rows[i][11]);
                                table.C9 = html.RowValue(dtKardex.Rows[i][12]);
                                table.C10 = html.RowValue(dtKardex.Rows[i][13]);
                                table.C11 = html.RowValue(dtKardex.Rows[i][14]);
                                table.C12 = html.RowValue(dtKardex.Rows[i][15]);

                                //objExcel.Cells[fila, 1] = dtKardex.Rows[i][2];
                                //objExcel.Cells[fila, 2] = dtKardex.Rows[i][3];
                                //objExcel.Cells[fila, 3] = dtKardex.Rows[i][4];
                                //objExcel.Cells[fila, 4] = dtKardex.Rows[i][5];
                                //objExcel.Cells[fila, 5] = dtKardex.Rows[i][6];
                                //objExcel.Cells[fila, 6] = dtKardex.Rows[i][7];
                                //objExcel.Cells[fila, 7] = dtKardex.Rows[i][8];
                                //objExcel.Cells[fila, 8] = dtKardex.Rows[i][11];
                                //objExcel.Cells[fila, 9] = dtKardex.Rows[i][12];
                                //objExcel.Cells[fila, 10] = dtKardex.Rows[i][13];
                                //objExcel.Cells[fila, 11] = dtKardex.Rows[i][14];
                                //objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];

                                cantidad = (cantidad + int.Parse(dtKardex.Rows[i][12].ToString())) - int.Parse(dtKardex.Rows[i][14].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) == 0) importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                if (Convert.ToInt32(dtKardex.Rows[i][14]) > 0)
                                {
                                    table.C8 = html.RowValue("");
                                    //objExcel.Cells[fila, 8] = "";
                                    costo_salida = imp_ponderado;
                                    dtKardex.Rows[i][15] = costo_salida * Convert.ToInt32(dtKardex.Rows[i][14]);
                                    //objExcel.Cells[fila, 12] = dtKardex.Rows[i][15];
                                    table.C12 = html.RowValue(dtKardex.Rows[i][15]);
                                    importe = (importe + decimal.Parse(dtKardex.Rows[i][13].ToString())) - decimal.Parse(dtKardex.Rows[i][15].ToString());
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                ctd_entradas = ctd_entradas + Convert.ToInt32(dtKardex.Rows[i][12]); ctd_entradas_tot = ctd_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][12]);
                                total_entradas = total_entradas + Convert.ToInt32(dtKardex.Rows[i][13]); total_entradas_tot = total_entradas_tot + Convert.ToInt32(dtKardex.Rows[i][13]);
                                ctd_salidas = ctd_salidas + Convert.ToInt32(dtKardex.Rows[i][14]); ctd_salidas_tot = ctd_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][14]);
                                total_salidas = total_salidas + Convert.ToInt32(dtKardex.Rows[i][15]); total_salidas_tot = total_salidas_tot + Convert.ToInt32(dtKardex.Rows[i][15]);

                                //objExcel.Cells[fila, 13] = cantidad;
                                //objExcel.Cells[fila, 14] = importe;
                                //objExcel.Cells[fila, 15] = dtKardex.Rows[i][18];

                                table.C13 = html.RowValue(cantidad);
                                table.C14 = html.RowValue(importe);
                                table.C15 = html.RowValue(dtKardex.Rows[i][18]);
                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                            }
                        }
                    }
                }

                if (dtKardex.DataSet != null && dtKardex.Rows[dtKardex.Rows.Count - 1][8].ToString() == "Saldo")
                {
                    //table.C0 = html.RowValue("");
                    table.C1 = html.RowValue("");
                    table.C2 = html.RowValue("SALDO ANTERIOR");
                    rows += html.GetConcatRows(table);
                    table.RowStyle();

                    //objExcel.Cells[fila, 1] = "";
                    //objExcel.Cells[fila, 2] = "SALDO ANTERIOR";
                    //objExcel.Rows[fila].Font.Bold = true;
                    //objExcel.Rows[fila].Font.Color = System.Drawing.ColorTranslator.FromHtml("#C00000");
                }
                else
                {
                    fila = fila + 1;

                    //table.C0 = html.RowValue("");
                    table.C2 = html.RowValue("TOTAL DE MOVIMIENTO :");
                    table.C9 = html.RowValue(ctd_entradas);
                    table.C10 = html.RowValue(total_entradas);
                    table.C11 = html.RowValue(ctd_salidas);
                    table.C12 = html.RowValue(total_salidas);
                    rows += html.GetConcatRows(table);
                    table.RowStyle();

                    //objExcel.Cells[fila, 2] = "TOTAL DE MOVIMIENTO :";
                    //objExcel.Cells[fila, 9] = ctd_entradas;
                    //objExcel.Cells[fila, 10] = total_entradas;
                    //objExcel.Cells[fila, 11] = ctd_salidas;
                    //objExcel.Cells[fila, 12] = total_salidas;
                }

                rows += html.GetConcatRows(table);
                table.RowStyle();

                fila = fila + 2;
                //objExcel.Cells[fila, 2] = "SALDO INICIAL";
                //objExcel.Cells[fila, 13] = ctd_saldos;
                //objExcel.Cells[fila, 14] = total_saldos;
                //table.C0 = html.RowValue("");
                table.C2 = html.RowValue("SALDO INICIAL");
                table.C13 = html.RowValue(ctd_saldos);
                table.C14 = html.RowValue(total_saldos);
                rows += html.GetConcatRows(table);
                table.RowStyle();

                fila = fila + 1;
                //objExcel.Cells[fila, 2] = "TOTALES :";
                //objExcel.Cells[fila, 9] = ctd_entradas_tot;
                //objExcel.Cells[fila, 10] = total_entradas_tot;
                //objExcel.Cells[fila, 11] = ctd_salidas_tot;
                //objExcel.Cells[fila, 12] = total_salidas_tot;

                //table.C0 = html.RowValue("");
                table.C2 = html.RowValue("TOTALES :");
                table.C9 = html.RowValue(ctd_entradas_tot);
                table.C10 = html.RowValue(total_entradas_tot);
                table.C11 = html.RowValue(ctd_salidas_tot);
                table.C12 = html.RowValue(total_salidas_tot);
                rows += html.GetConcatRows(table);
                table.RowStyle();


                fila = fila + 1;
                //objExcel.Cells[fila, 2] = "STOCK FINAL A : " + fechaFin.ToString("dd/MM/yyyy");
                //objExcel.Cells[fila, 13] = (ctd_entradas_tot - ctd_salidas_tot) + ctd_saldos;
                //objExcel.Cells[fila, 14] = (total_entradas_tot - total_salidas_tot) + total_saldos;

                //table.C0 = html.RowValue("");
                table.C2 = html.RowValue("STOCK FINAL A : " + fechaFin.ToString("dd/MM/yyyy"));
                table.C13 = html.RowValue((ctd_entradas_tot - ctd_salidas_tot) + ctd_saldos);
                table.C14 = html.RowValue((total_entradas_tot - total_salidas_tot) + total_saldos);
                rows += html.GetConcatRows(table);
                table.RowStyle();


                //html = html.Replace("@rows", rows);
                template = template.Replace("@rows", rows);

                //string path = $"{Program.Sesion.Global.RutaArchivosLocalExportar}\\";

                //System.IO.File.WriteAllText(filePath, html);
                //string excelPath = $"{path}{"kardex_valorizado"}{".xlsx"}";
                string htmlContent = template;//


                html.ParseHtmlToExcel(htmlContent, "kardex_valorizado", Tools.Hng_Htmltools.HtmlExportFormat.KardexValorizado);


                //objExcel.Range["A1:O5"].Select();
                //objExcel.Selection.Font.Bold = true;

                //objExcel.Range["B" + (fila - 2) + ":N" + fila].Select();
                //objExcel.Selection.Font.Bold = true;

                //objExcel.Range["A4:H5"].Select();
                //objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C0FFC0");

                //objExcel.Range["I4:J5"].Select();
                //objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C0FFFF");

                //objExcel.Range["K4:L5"].Select();
                //objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFFFC0");

                //objExcel.Range["M4:N5"].Select();
                //objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#C0FFC0");

                //objExcel.Range["A4:O5"].Select();
                //objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);

                //objExcel.Range["A1"].Select();

                //objExcel.Range["H6:N" + fila].NumberFormat = "#,##0.0000";

                //sheet.Delete();
                //objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                //objExcel.Visible = true;
                //objExcel = null;



                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Generar Reporte. " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string mesEnLetras(DateTime fecha)
        {
            string mes = "";

            switch (fecha.Month)
            {
                case 1: mes = "Enero"; break;
                case 2: mes = "FEBRERO"; break;
                case 3: mes = "MARZO"; break;
                case 4: mes = "ABRIL"; break;
                case 5: mes = "MAYO"; break;
                case 6: mes = "JUNIO"; break;
                case 7: mes = "JULIO"; break;
                case 8: mes = "AGOSTO"; break;
                case 9: mes = "SETIEMBRE"; break;
                case 10: mes = "OCTUBRE"; break;
                case 11: mes = "NOVIEMBRE"; break;
                case 12: mes = "DICIEMBRE"; break;
            }

            return mes;
        }

        private void gvOrdEnviadas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvOrdEnviadas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvReqAprobados_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvReqAprobados_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void btnAtenderRequerimiento_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (xtcInventarioAlmacen.SelectedTabPage == xtabRequerimientos)
            {
                if (MessageBox.Show("¿Esta seguro de atender los requerimientos?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    eRequerimiento obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;
                    string result = unit.Requerimiento.Atender_Requerimiento(cod_empresa, lkpSedeEmpresa.EditValue.ToString(), obj.cod_requerimiento, obj.flg_solicitud, obj.dsc_anho, Program.Sesion.Usuario.cod_usuario);
                    if (result != "OK") { MessageBox.Show("Error al atender requerimientos", "Requerimientos Aprobados", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    listReqAprobados = unit.Requerimiento.ListarRequerimiento<eRequerimiento>(3, cod_empresa, lkpSedeEmpresa.EditValue == null ? "" : lkpSedeEmpresa.EditValue.ToString(),
                                                                            "", "", "01", Convert.ToDateTime(dtFechaInicio.EditValue).ToString("yyyyMMdd"),
                                                                            Convert.ToDateTime(dtFechaFin.EditValue).ToString("yyyyMMdd"));
                    bsListadoReqAprobados.DataSource = listReqAprobados;
                    MessageBox.Show("Se atendieron los requerimientos de manera satisfactoria", "Requerimientos No Aprobados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void gvOrdEnviadas_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarOC();
        }

        private void MostrarOC()
        {
            eOrdenCompra_Servicio obj = new eOrdenCompra_Servicio();
            obj = gvOrdEnviadas.GetFocusedRow() as eOrdenCompra_Servicio;
            frmMantOrdenCompra frm = new frmMantOrdenCompra();
            frm.accion = OrdenCompra.Vista;
            frm.empresa = obj.cod_empresa;
            frm.sede = obj.cod_sede_empresa;
            frm.ordenCompraServicio = obj.cod_orden_compra_servicio;
            frm.solicitud = obj.flg_solicitud;
            frm.anho = obj.dsc_anho;
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();
        }

        private void gvReqAprobados_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Clicks == 2) MostrarReq();
        }

        private void MostrarReq()
        {
            eRequerimiento obj = new eRequerimiento();
            frmMantRequerimientosCompra frm = new frmMantRequerimientosCompra(ParentFormType.ListaInventarioAlmacen);
            obj = gvReqAprobados.GetFocusedRow() as eRequerimiento;
            frm.accion = RequerimientoCompra.Vista;
            frm.empresa = obj.cod_empresa;
            frm.sede = obj.cod_sede_empresa;
            frm.requerimiento = obj.cod_requerimiento;
            frm.solicitud = obj.flg_solicitud;
            frm.anho = obj.dsc_anho;
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();
        }

        private void xtcInventarioAlmacen_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            AdministrarSemaforoRequerimiento(xtcInventarioAlmacen.SelectedTabPage == xtabRequerimientos ? true : false);

            if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoEntradas)
            {
                btnGenerarNotaIngreso.Enabled = true;
                btnGenerarNotaSalida.Enabled = false;
                btnGenerarGuiaRemision.Enabled = false;
            }
            else if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoSalidas)
            {
                btnGenerarNotaSalida.Enabled = true;
                btnGenerarNotaIngreso.Enabled = false;
                btnGenerarGuiaRemision.Enabled = false;
            }
            else if (xtcInventarioAlmacen.SelectedTabPage == xtabListadoSalidasGuiaRemision)
            {
                btnGenerarGuiaRemision.Enabled = true;
                btnGenerarNotaIngreso.Enabled = false;
                btnGenerarNotaSalida.Enabled = false;
            }
            else
            {
                btnGenerarGuiaRemision.Enabled = false;
                btnGenerarNotaIngreso.Enabled = false;
                btnGenerarNotaSalida.Enabled = false;
            }
        }

        private void btnKardexValorizadoDetallado_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            frmRangoFecha frm = new frmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;

            DataTable dtGeneral = unit.Logistica.ReporteKardex(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));
            DataTable dtSaldo = unit.Logistica.ReporteKardex_Saldo(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"));

            dtGeneral.Merge(dtSaldo);


            DataView dvKardex = dtGeneral.DefaultView;
            dvKardex.Sort = "cod_producto, Fecha, fch_registro ASC";
            DataTable dtKardex = dvKardex.ToTable();

            GenerarReporteKardexDetallado(dtKardex, frm.fechaInicio, frm.fechaFin);
        }

        private class Kardex
        {
            public string cod_empresa { get; set; }
            public string cod_sede_empresa { get; set; }
            public DateTime Fecha { get; set; }
            public string TipoDoc { get; set; }
            public string TipoMov { get; set; }
            public string cod_almacen { get; set; }
            public string numero { get; set; }
            public string documento { get; set; }
            public string dsc_proveedor_cliente { get; set; }
            public string cod_producto { get; set; }
            public string Producto { get; set; }
            public decimal PrecioUnitario { get; set; }
            public int cantidad_entrada { get; set; }
            public decimal total_entrada { get; set; }
            public int cantidad_salida { get; set; }
            public decimal total_salida { get; set; }
            public int cantidad_final { get; set; }
            public decimal total_final { get; set; }
            public string Glosa { get; set; }
            public string dsc_tipo_servicio { get; set; }
            public string dsc_subtipo_servicio { get; set; }
            public DateTime fch_registro { get; set; }
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public const string sNullable = "Nullable`1";
        private static T GetItem<T>(DataRow row)
        {
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn col in row.Table.Columns)
            {
                PropertyInfo prop = obj.GetType().GetProperty(col.ColumnName);
                if (prop != null && prop.CanWrite && !object.ReferenceEquals(row[col], DBNull.Value))
                {
                    prop.SetValue(obj, row[col], null);
                }
            }
            return obj;

        }





        private void GenerarReporteKardexDetallado(DataTable dtKardex, DateTime fechaInicio, DateTime fechaFin)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");
            try
            {
                Hng_Htmltools html = new Hng_Htmltools();
                ToolTable table = new ToolTable(19);

                string mesInicio = mesEnLetras(fechaInicio);
                string mesFin = mesEnLetras(fechaFin);

                // string html = Properties.Resources.kardex_valorizado.ToString();
                string template = html.GetTemplate();
                string rows = string.Empty;
                string cols = string.Empty;

                int cantidad = 0, ctd_entradas = 0, ctd_salidas = 0, ctd_saldos = 0, ctd_entradas_tot = 0, ctd_salidas_tot = 0;
                decimal importe = 0, imp_ponderado = 0, costo_salida = 0, total_entradas = 0, total_salidas = 0, total_saldos = 0, total_entradas_tot = 0, total_salidas_tot = 0;

                /*-----*Cabecera*-----*/
                table.ColumnStyle();
                table.C1 = html.HeaderValue("K2 SEGURIDAD Y RESGUARDO S.A.C");
                table.C11 = html.HeaderValue("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + ", Hora: " + DateTime.Now.ToString("hh:mm:ss"));
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("ALPROM23");
                table.C4 = html.HeaderValue($"MOVIMIENTO DE EXISTENCIAS POR ARTICULO - DE {mesInicio} DEL {fechaInicio.Year} A {mesFin} DEL {fechaFin.Year}");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C6 = html.HeaderValue("MONEDA : MONEDA NACIONAL");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("Codigo");
                table.C2 = html.HeaderValue("Tipo");
                table.C3 = html.HeaderValue("Sub Tipo");
                table.C4 = html.HeaderValue("Producto");
                table.C5 = html.HeaderValue("Fecha");
                table.C6 = html.HeaderValue("Tipo Doc.");
                table.C7 = html.HeaderValue("Tipo Mov.");
                table.C8 = html.HeaderValue("Almacén");
                table.C9 = html.HeaderValue("Nro. Doc.");
                table.C10 = html.HeaderValue("Doc. Ref.");
                table.C11 = html.HeaderValue("Proveedor/Cliente");
                table.C12 = html.HeaderValue("Precio Unitario");
                table.C13 = html.HeaderValue("***ENTRADA***");
                table.C15 = html.HeaderValue("***SALIDA***");
                table.C17 = html.HeaderValue("***SALDO***");
                table.C19 = html.HeaderValue("Glosa");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C13 = html.HeaderValue("Cantidad");
                table.C14 = html.HeaderValue("P.T.Doc.");
                table.C15 = html.HeaderValue("Cantidad");
                table.C16 = html.HeaderValue("P.T.Doc.");
                table.C17 = html.HeaderValue("Cantidad");
                table.C18 = html.HeaderValue("P.T.Doc.");
                cols += html.GetConcatRows(table);
                table.RowStyle();

                //html = html.Replace("@cols", cols);
                template = template.Replace("@cols", cols);

                List<Kardex> kardex = ConvertDataTable<Kardex>(dtKardex);
                int index = 0;
                foreach (var item in kardex)
                {
                    if (index == 0)
                    {
                        table.C1 = html.RowValue(item.cod_producto);
                        table.C2 = html.RowValue(item.dsc_tipo_servicio);
                        table.C3 = html.RowValue(item.dsc_subtipo_servicio);
                        table.C4 = html.RowValue(item.Producto);
                        table.C5 = html.RowValue("SALDO ANTERIOR");
                        table.C17 = html.RowValue(cantidad);
                        table.C18 = html.RowValue(importe);

                        if (item.dsc_proveedor_cliente == "Saldo")
                        {
                            ctd_saldos = (ctd_saldos + item.cantidad_entrada) - item.cantidad_salida;
                            total_saldos = (total_saldos + item.total_entrada) - item.total_salida;

                            cantidad = (cantidad + item.cantidad_entrada) - item.cantidad_salida;
                            importe = (importe + item.total_entrada) - item.total_salida;
                            imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                            table.C17 = html.RowValue(cantidad);
                            table.C18 = html.RowValue(importe);

                        }
                        else
                        {
                            table.C5 = html.RowValue(item.Fecha.ToString("dd/MM/yyyy"));
                            table.C6 = html.RowValue(item.TipoDoc);
                            table.C7 = html.RowValue(item.TipoMov);
                            table.C8 = html.RowValue(item.cod_almacen);
                            table.C9 = html.RowValue(item.numero);
                            table.C10 = html.RowValue(item.documento);
                            table.C11 = html.RowValue(item.dsc_proveedor_cliente);
                            table.C12 = html.RowValue(item.PrecioUnitario);
                            table.C13 = html.RowValue(item.cantidad_entrada);
                            table.C14 = html.RowValue(item.total_entrada);
                            table.C15 = html.RowValue(item.cantidad_salida);
                            table.C16 = html.RowValue(item.total_salida);

                            cantidad = (cantidad + item.cantidad_entrada) - item.cantidad_salida;
                            if (item.cantidad_salida == 0) importe = (importe + item.total_entrada) - item.total_salida;

                            if (item.cantidad_salida > 0)
                            {
                                table.C8 = html.RowValue("");
                                costo_salida = imp_ponderado;
                                item.total_salida = costo_salida * item.cantidad_salida;
                                table.C16 = html.RowValue(item.total_salida.ToString("#,##0.0000"));
                                importe = (importe + item.total_entrada) - item.total_salida;
                            }
                            imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                            ctd_entradas = ctd_entradas + item.cantidad_entrada; ctd_entradas_tot = ctd_entradas_tot + item.cantidad_entrada;
                            total_entradas = total_entradas + item.total_entrada; total_entradas_tot = total_entradas_tot + item.total_entrada;
                            ctd_salidas = ctd_salidas + item.cantidad_salida; ctd_salidas_tot = ctd_salidas_tot + item.cantidad_salida;
                            total_salidas = total_salidas + item.total_salida; total_salidas_tot = total_salidas_tot + item.total_salida;

                            table.C17 = html.RowValue(cantidad);
                            table.C18 = html.RowValue(importe);
                            table.C19 = html.RowValue(item.Glosa);

                            rows += html.GetConcatRows(table);
                            table.RowStyle();
                        }
                    }
                    else
                    {
                        var itemAnterior = kardex[index - 1].cod_producto;
                        if (item.cod_producto == itemAnterior)
                        {
                            if (item.dsc_proveedor_cliente == "Saldo")
                            {
                                ctd_saldos = (ctd_saldos + item.cantidad_entrada) - item.cantidad_salida;
                                total_saldos = (total_saldos + item.total_entrada) - item.total_salida;

                                cantidad = (cantidad + item.cantidad_entrada) - item.cantidad_salida;
                                importe = (importe + item.total_entrada) - item.total_salida;
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                table.C17 = html.RowValue(cantidad);
                                table.C18 = html.RowValue(importe);

                                ////verificar
                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                            }
                            else
                            {
                                if (kardex[index - 1].dsc_proveedor_cliente == "Saldo")
                                {
                                    table.C5 = html.RowValue("Saldo Inicial");
                                }

                                //fila = fila + 1;

                                table.C1 = html.RowValue(item.cod_producto);
                                table.C2 = html.RowValue(item.dsc_tipo_servicio);
                                table.C3 = html.RowValue(item.dsc_subtipo_servicio);
                                table.C4 = html.RowValue(item.Producto);
                                table.C5 = html.RowValue(item.Fecha.ToString("dd/MM/yyyy"));
                                table.C6 = html.RowValue(item.TipoDoc);
                                table.C7 = html.RowValue(item.TipoMov);
                                table.C8 = html.RowValue(item.cod_almacen);
                                table.C9 = html.RowValue(item.numero);
                                table.C10 = html.RowValue(item.documento);
                                table.C11 = html.RowValue(item.dsc_proveedor_cliente);
                                table.C12 = html.RowValue(item.PrecioUnitario);
                                table.C13 = html.RowValue(item.cantidad_entrada);
                                table.C14 = html.RowValue(item.total_entrada);
                                table.C15 = html.RowValue(item.cantidad_salida);
                                table.C16 = html.RowValue(item.total_salida);

                                cantidad = (cantidad + item.cantidad_entrada) - item.cantidad_salida;
                                if (item.cantidad_salida == 0) importe = (importe + item.total_entrada) - item.total_salida;

                                if (item.cantidad_salida > 0)
                                {
                                    table.C8 = html.RowValue("");
                                    costo_salida = imp_ponderado;
                                    item.total_salida = costo_salida * item.cantidad_salida;
                                    table.C16 = html.RowValue(item.total_salida);
                                    importe = (importe + item.total_entrada) - item.total_salida;
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                ctd_entradas = ctd_entradas + item.cantidad_entrada; ctd_entradas_tot = ctd_entradas_tot + item.cantidad_entrada;
                                total_entradas = total_entradas + item.total_entrada; total_entradas_tot = total_entradas_tot + item.total_entrada;
                                ctd_salidas = ctd_salidas + item.cantidad_salida; ctd_salidas_tot = ctd_salidas_tot + item.cantidad_salida;
                                total_salidas = total_salidas + item.total_salida; total_salidas_tot = total_salidas_tot + item.total_salida;

                                table.C17 = html.RowValue(cantidad);
                                table.C18 = html.RowValue(importe);
                                table.C19 = html.RowValue(item.Glosa);

                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                            }
                        }
                        else
                        {
                            if (kardex[index - 1].dsc_proveedor_cliente == "Saldo")
                            {
                                table.C5 = html.RowValue("SALDO ANTERIOR");
                            }
                            else
                            {
                                //fila = fila + 1;
                                table.C1 = html.RowValue(kardex[index - 1].cod_producto);
                                table.C2 = html.RowValue(kardex[index - 1].dsc_tipo_servicio);
                                table.C3 = html.RowValue(kardex[index - 1].dsc_subtipo_servicio);
                                table.C4 = html.RowValue(kardex[index - 1].Producto);
                                table.C5 = html.RowValue("TOTAL DE MOVIMIENTOS :");

                                table.C13 = html.RowValue(ctd_entradas);
                                table.C14 = html.RowValue(total_entradas);
                                table.C15 = html.RowValue(ctd_salidas);
                                table.C16 = html.RowValue(total_salidas);

                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                            }

                            //fila = fila + 1;

                            cantidad = 0; importe = 0;
                            imp_ponderado = 0; costo_salida = 0;
                            ctd_entradas = 0; total_entradas = 0;
                            ctd_salidas = 0; total_salidas = 0;

                            table.C1 = html.RowValue(item.cod_producto);
                            table.C2 = html.RowValue(item.dsc_tipo_servicio);
                            table.C3 = html.RowValue(item.dsc_subtipo_servicio);
                            table.C4 = html.RowValue(item.Producto);
                            table.C5 = html.RowValue("SALDO ANTERIOR");
                            table.C13 = html.RowValue(cantidad);
                            table.C14 = html.RowValue(importe);

                            if (item.dsc_proveedor_cliente == "Saldo")
                            {
                                ctd_saldos = (ctd_saldos + item.cantidad_entrada) - item.cantidad_salida;
                                total_saldos = (total_saldos + item.total_entrada) - item.total_salida;

                                cantidad = (cantidad + item.cantidad_entrada) - item.cantidad_salida;
                                if (item.cantidad_salida == 0) importe = (importe + item.total_entrada) - item.total_salida;

                                if (item.cantidad_salida > 0)
                                {
                                    table.C8 = html.RowValue("");
                                    costo_salida = imp_ponderado;
                                    item.total_salida = costo_salida * item.cantidad_salida;
                                    table.C16 = html.RowValue(item.total_salida);
                                    importe = (importe + item.total_entrada) - item.total_salida;
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                table.C17 = html.RowValue(cantidad);
                                table.C18 = html.RowValue(importe);

                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                            }
                            else
                            {
                                table.C5 = html.RowValue(item.Fecha.ToString("dd/MM/yyyy"));
                                table.C6 = html.RowValue(item.TipoDoc);
                                table.C7 = html.RowValue(item.TipoMov);
                                table.C8 = html.RowValue(item.cod_almacen);
                                table.C9 = html.RowValue(item.numero);
                                table.C10 = html.RowValue(item.documento);
                                table.C11 = html.RowValue(item.dsc_proveedor_cliente);
                                table.C12 = html.RowValue(item.PrecioUnitario);
                                table.C13 = html.RowValue(item.cantidad_entrada);
                                table.C14 = html.RowValue(item.total_entrada);
                                table.C15 = html.RowValue(item.cantidad_salida);
                                table.C16 = html.RowValue(item.total_salida);


                                cantidad = (cantidad + item.cantidad_entrada) - item.cantidad_salida;
                                if (item.cantidad_salida == 0) importe = (importe + item.total_entrada) - item.total_salida;
                                if (item.cantidad_salida > 0)
                                {
                                    table.C8 = html.RowValue("");
                                    costo_salida = imp_ponderado;
                                    item.total_salida = costo_salida * item.cantidad_salida;
                                    table.C16 = html.RowValue(item.total_salida);
                                    importe = (importe + item.total_entrada) - item.total_salida;
                                }
                                imp_ponderado = cantidad == 0 ? 0 : importe / cantidad;

                                ctd_entradas = ctd_entradas + item.cantidad_entrada; ctd_entradas_tot = ctd_entradas_tot + item.cantidad_entrada;
                                total_entradas = total_entradas + item.total_entrada; total_entradas_tot = total_entradas_tot + item.total_entrada;
                                ctd_salidas = ctd_salidas + item.cantidad_salida; ctd_salidas_tot = ctd_salidas_tot + item.cantidad_salida;
                                total_salidas = total_salidas + item.total_salida; total_salidas_tot = total_salidas_tot + item.total_salida;

                                table.C17 = html.RowValue(cantidad);
                                table.C18 = html.RowValue(importe);
                                table.C19 = html.RowValue(item.Glosa);
                                rows += html.GetConcatRows(table);
                                table.RowStyle();
                            }
                        }
                    }
                    index++;
                }

                if (kardex != null && kardex.Count > 0)
                {
                    if (kardex[kardex.Count - 1].dsc_proveedor_cliente == "Saldo")
                    {
                        table.C5 = html.RowValue("SALDO ANTERIOR");
                    }
                    else
                    {
                        //fila = fila + 1;

                        table.C1 = html.RowValue(kardex[kardex.Count - 1].cod_producto);
                        table.C2 = html.RowValue(kardex[kardex.Count - 1].dsc_tipo_servicio);
                        table.C3 = html.RowValue(kardex[kardex.Count - 1].dsc_subtipo_servicio);
                        table.C4 = html.RowValue(kardex[kardex.Count - 1].Producto);

                        table.C5 = html.RowValue("TOTAL DE MOVIMIENTO :");
                        table.C13 = html.RowValue(ctd_entradas);
                        table.C14 = html.RowValue(total_entradas.ToString("#,##0.0000"));
                        table.C15 = html.RowValue(ctd_salidas);
                        table.C16 = html.RowValue(total_salidas.ToString("#,##0.0000"));
                    }

                    rows += html.GetConcatRows(table);
                    table.RowStyle();
                }


                /*-----*Footer Sumatorias*-----*/
                rows += html.GetConcatRows(table);
                table.RowStyle();

                table.C2 = html.RowValue("SALDO INICIAL :");
                table.C17 = html.RowValue(ctd_saldos);
                table.C18 = html.RowValue(total_saldos);
                rows += html.GetConcatRows(table);
                table.RowStyle();

                table.C2 = html.RowValue("TOTALES :");
                table.C13 = html.RowValue(ctd_entradas_tot);
                table.C14 = html.RowValue(total_entradas_tot);
                table.C15 = html.RowValue(ctd_salidas_tot);
                table.C16 = html.RowValue(total_salidas_tot);
                rows += html.GetConcatRows(table);
                table.RowStyle();

                table.C2 = html.RowValue("STOCK FINAL A: " + fechaFin.ToString("dd/MM/yyyy"));
                table.C17 = html.RowValue((ctd_entradas_tot - ctd_salidas_tot) + ctd_saldos);
                table.C18 = html.RowValue((total_entradas_tot - total_salidas_tot) + total_saldos);
                rows += html.GetConcatRows(table);
                table.RowStyle();


                //html = html.Replace("@rows", rows);
                template = template.Replace("@rows", rows);

                //string path = $"{Program.Sesion.Global.RutaArchivosLocalExportar}\\";
                //string filePath = $"{path}{"kardex"}{".html"}";

                //System.IO.File.WriteAllText(filePath, html);
                //string excelPath = $"{path}{"kardex_valorizado_detallado"}{".xlsx"}";
                string htmlContent = template;//


                html.ParseHtmlToExcel(htmlContent, "kardex_valorizado_detallado", Tools.Hng_Htmltools.HtmlExportFormat.KardexValorizado_Detalle);

                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Generar Reporte. " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRepOCxProv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            frmRangoFecha frm = new frmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;

            DataTable dtOrdenes = unit.Logistica.ReporteOrdenesCompra(8, navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));

            if (dtOrdenes.Rows.Count > 0)
            { GenerarReporteOrdenCompra(dtOrdenes, frm.fechaInicio, frm.fechaFin); }
        }

        private void GenerarReporteOrdenCompra(DataTable dtOrdenes, DateTime fechaInicio, DateTime fechaFin)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");

            Excel.Application objExcel = new Excel.Application();
            objExcel.Workbooks.Add();

            var workbook = objExcel.ActiveWorkbook;
            var sheet = workbook.Sheets["Hoja1"];
            objExcel.Visible = true;

            try
            {
                objExcel.Sheets.Add();
                var worksheet = workbook.ActiveSheet;
                worksheet.Name = "Reporte OC";

                //objExcel.ActiveWindow.DisplayGridlines = false;
                objExcel.Range["H:H"].NumberFormat = "@";
                objExcel.Range["A:A"].ColumnWidth = 18;
                objExcel.Range["B:B"].ColumnWidth = 50;
                objExcel.Range["C:I"].ColumnWidth = 18;
                objExcel.Range["J:J"].ColumnWidth = 60;
                objExcel.Range["K:AG"].ColumnWidth = 18;

                objExcel.Range["A1:A1"].Select();
                objExcel.Selection.Font.Bold = true;
                objExcel.Cells[1, 1] = navBarControl1.SelectedLink.Item.Caption.ToString();
                objExcel.Cells[2, 1] = "COMOVI11";

                string mesInicio = mesEnLetras(fechaInicio);
                string mesFin = mesEnLetras(fechaFin);

                objExcel.Range["B3:B5"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["B3:B5"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                worksheet.Range["B3:H3"].MergeCells = true; objExcel.Cells[3, 2] = "REPORTE DE ORDENES DE COMPRA POR PROVEEDOR";
                worksheet.Range["B4:H4"].MergeCells = true; objExcel.Cells[4, 2] = "TIPO DE MONEDA: MN - MONEDA NACIONAL";
                worksheet.Range["B5:H5"].MergeCells = true; objExcel.Cells[5, 2] = "DEL PROVEEDOR: " + dtOrdenes.Rows[0][0].ToString() + " AL PROVEEDOR: " + dtOrdenes.Rows[dtOrdenes.Rows.Count - 1][0].ToString();

                objExcel.Range["B3:B5"].Select();
                objExcel.Selection.Font.Bold = true;

                objExcel.Cells[8, 1] = "COD.PROVEE"; objExcel.Cells[8, 2] = "PROVEEDOR"; objExcel.Cells[8, 3] = "NRO.ORDEN"; objExcel.Cells[8, 4] = "M.O";
                objExcel.Cells[8, 5] = "FEC.ORDEN"; objExcel.Cells[8, 6] = "FORMA DE PAGO"; objExcel.Cells[8, 7] = "FEC.ENTREGA"; objExcel.Cells[8, 8] = "COD.ARTICULO";
                objExcel.Cells[8, 9] = "COD.REFERENCIA"; objExcel.Cells[8, 10] = "ARTICULO"; objExcel.Cells[8, 11] = "CANT.ORDENADA"; objExcel.Cells[8, 12] = "PR.UNIT. s/IGV";
                objExcel.Cells[8, 13] = "PR.TOTAL s/IGV"; objExcel.Cells[8, 14] = "PR.UNIT c/IGV"; objExcel.Cells[8, 15] = "PR.TOTAL c/IGV"; objExcel.Cells[8, 16] = "U.M.";
                objExcel.Cells[8, 17] = "CANT.RECIBIDA"; objExcel.Cells[8, 18] = "CANT.PENDIENTE"; objExcel.Cells[8, 19] = "SITUACION"; objExcel.Cells[8, 20] = "TD NUMDOC";
                objExcel.Cells[8, 21] = "P.ARANCELARIA"; objExcel.Cells[8, 22] = "COD.C.COSTOS"; objExcel.Cells[8, 23] = "CENTRO DE COSTOS"; objExcel.Cells[8, 24] = "COD.GRUPO";
                objExcel.Cells[8, 25] = "GRUPO"; objExcel.Cells[8, 26] = "COD.FAMILIA"; objExcel.Cells[8, 27] = "FAMILIA"; objExcel.Cells[8, 28] = "COD.MODELO";
                objExcel.Cells[8, 29] = "MODELO"; objExcel.Cells[8, 30] = "COD.MARCA"; objExcel.Cells[8, 31] = "MARCA"; objExcel.Cells[8, 32] = "COD.LINEA";
                objExcel.Cells[8, 33] = "LINEA";

                objExcel.Range["A8:AG8"].Select();
                objExcel.Selection.Borders.Color = Color.FromArgb(0, 0, 0);

                objExcel.Range["AG1:AG2"].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                objExcel.Cells[1, 33] = DateTime.Now.ToString("dd/MM/yyyy");
                objExcel.Cells[2, 33] = DateTime.Now.ToString("hh:mm:ss");

                int fila = 9;

                for (int i = 0; i < dtOrdenes.Rows.Count; i++)
                {
                    for (int x = 0; x < 33; x++)
                    {
                        objExcel.Cells[fila, x + 1] = dtOrdenes.Rows[i][x];
                    }

                    fila = fila + 1;
                }

                objExcel.Range["L6:O" + fila].NumberFormat = "#,##0.0000";

                objExcel.Range["A1:A1"].Select();

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null;

                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Generar Reporte. " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SplashScreenManager.CloseForm();
            }
        }

        private void gvListadoSalidas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoSalidasGuiaRemision_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoSalidasGuiaRemision_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        #region Inicio :: filtros de estados para requerimientos
        private void AdministrarSemaforoRequerimiento(bool value)
        {
            var visibility = value ? BarItemVisibility.Always : BarItemVisibility.Never;
            bsiRequerimiento.Visibility = visibility;
            bbiAprobado.Visibility = visibility;
            bbiOGenerada.Visibility = visibility;
            //bbiAtendido.Visibility = visibility;
            //bbiSolicitado.Visibility = visibility;
            bbiTodos.Visibility = visibility;
        }
        private void bbiAprobado_ItemClick(object sender, ItemClickEventArgs e)
        {
            CargarRequerimientos("APR");
        }

        private void bbiOGenerada_ItemClick(object sender, ItemClickEventArgs e)
        {
            CargarRequerimientos("OCG");
        }

        private void bbiAtendido_ItemClick(object sender, ItemClickEventArgs e)
        {
            //  CargarRequerimientos("ATE");
        }

        private void bbiSolicitado_ItemClick(object sender, ItemClickEventArgs e)
        {
            // CargarRequerimientos("SOL");
        }

        private void bbiTodos_ItemClick(object sender, ItemClickEventArgs e)
        {
            CargarRequerimientos("");
        }

        private class Semaforo
        {
            public Brush Green { get { return Brushes.MediumSeaGreen; } }
            public Brush Yellow { get { return Brushes.Yellow; } }
            public Brush Red { get { return Brushes.Red; } }
            public Brush White { get { return Brushes.WhiteSmoke; } }
            public Brush Blue { get { return Brushes.MediumBlue; } }
        }
        private void gvReqAprobados_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (!(gvReqAprobados.GetRow(e.RowHandle) is eRequerimiento obj)) return;

                    e.DefaultDraw();
                    if (e.Column.FieldName.Equals("cod_estado_requerimiento"))
                    {
                        Semaforo semaforo = new Semaforo();
                        Brush brush; e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        string value = e.CellValue.ToString();

                        brush = value == "APROBADO" ? semaforo.Green :
                            value == "ORDEN GENERADA" ? semaforo.Yellow :
                            value == "ATENDIDO" ? semaforo.White :
                            value == "SOLICITADO" ? semaforo.Red : semaforo.Blue;
                        e.Graphics.FillEllipse(brush, new System.Drawing.Rectangle(e.Bounds.X + 2, e.Bounds.Y + 1, markWidth, markWidth));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion Fin :: filtros de estados para requerimientos

        private void btnExportarAlprom49_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            frmRangoFecha frm = new frmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;

            //DataTable dtResumen = unit.Logistica.Obtener_ReporteLogistica_InventarioResumido(almacen: lkpAlmacen.EditValue.ToString(),
            // empresa: cod_empresa, sede: lkpSedeEmpresa.EditValue.ToString(), fechaInicio: frm.fechaInicio.ToString("yyyyMMdd"), fechaFin: frm.fechaFin.ToString("yyyyMMdd"));

            //List<eAlmacen.eReporteInventarioResumen> resumen = ConvertDataTable<eAlmacen.eReporteInventarioResumen>(dtResumen);

            //ExportarLibro13_1_Alprom49(resumen, frm.fechaInicio, frm.fechaFin);

            DataTable dtGeneral = unit.Logistica.ReporteKardex(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));
            DataTable dtSaldo = unit.Logistica.ReporteKardex_Saldo(navBarControl1.SelectedLink.Item.Name.ToString(), lkpSedeEmpresa.EditValue.ToString(), lkpAlmacen.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"));

            dtGeneral.Merge(dtSaldo);

            DataView dvKardex = dtGeneral.DefaultView;
            dvKardex.Sort = "cod_producto, Fecha, fch_registro ASC";
            DataTable dtKardex = dvKardex.ToTable();

            ExportarLibro13_1_Alprom49(dtKardex, frm.fechaInicio, frm.fechaFin);
        }

        struct Cantidad
        {
            public Cantidad(int cero)
            {
                AcumuladoEntradaSaldo = cero;
                AcumuladoSalidaSaldo = cero;
                AcumuladoEntradaActual = cero;
                AcumuladoSalidaActual = cero;
                SaldoFinal = cero;
                ActualFinal = cero;
            }
            public int AcumuladoEntradaSaldo;
            public int AcumuladoSalidaSaldo;
            public int AcumuladoEntradaActual;
            public int AcumuladoSalidaActual;
            public int SaldoFinal;
            public int ActualFinal;
        }
        struct Importe
        {
            public Importe(decimal cero)
            {
                AcumuladoEntradaSaldo = cero;
                AcumuladoSalidaSaldo = cero;
                AcumuladoEntradaActual = cero;
                AcumuladoSalidaActual = cero;
                SaldoFinal = cero;
                ActualFinal = cero;
            }
            public decimal AcumuladoEntradaSaldo;
            public decimal AcumuladoSalidaSaldo;
            public decimal AcumuladoEntradaActual;
            public decimal AcumuladoSalidaActual;
            public decimal SaldoFinal;
            public decimal ActualFinal;
        }
        struct Total
        {
            public Total(int cero, decimal zero)
            {
                CantidadAnterior = cero;
                CantidadEntradaActual = cero;
                CantidadSalidaActual = cero;
                CantidadSaldoFinal = cero;

                ImporteAnterior = zero;
                ImporteEntradaActual = zero;
                ImporteSalidaActual = zero;
                ImporteSaldoFinal = zero;
            }
            public int CantidadAnterior;
            public int CantidadEntradaActual;
            public int CantidadSalidaActual;
            public int CantidadSaldoFinal;

            public decimal ImporteAnterior;
            public decimal ImporteEntradaActual;
            public decimal ImporteSalidaActual;
            public decimal ImporteSaldoFinal;
        }

        private void ExportarLibro13_1_Alprom49(DataTable dtKardex /*List<eAlmacen.eReporteInventarioResumen> resumen*/,
            DateTime fechaInicio, DateTime fechaFin)
        {
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Reporte", "Cargando...");

            Hng_Htmltools html = new Hng_Htmltools();
            ToolTable table = new ToolTable(12);
            string template = html.GetTemplate();
            string rows = string.Empty;
            string cols = string.Empty;

            try
            {
                string mesInicio = mesEnLetras(fechaInicio);
                string mesFin = mesEnLetras(fechaFin);

                /*-----*Cabecera*-----*/
                table.ColumnStyle();
                table.C1 = html.HeaderValue(navBarControl1.SelectedLink.Item.Caption.ToString());
                table.C3 = html.HeaderValue("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + ", Hora: " + DateTime.Now.ToString("hh:mm:ss"));
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("ALPROM49");
                table.C4 = html.HeaderValue("FORMATO 13.1 REGISTRO DE INVENTARIO VALORIZADO POR ARTICULO - DE " + mesInicio + " DEL " + fechaInicio.Year.ToString() + " A " + mesFin + " DEL " + fechaFin.Year.ToString());
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C6 = html.HeaderValue("MONEDA :");
                table.C7 = html.HeaderValue("MONEDA NACIONAL");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C1 = html.HeaderValue("CÓDIGO");
                table.C2 = html.HeaderValue("DESCRIPCIÓN");
                table.C3 = html.HeaderValue("CANTIDADES");
                table.C7 = html.HeaderValue("VALORIZACIÓN");
                table.C12 = html.HeaderValue("ALM");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();


                table.C3 = html.HeaderValue("SALDO");
                table.C7 = html.HeaderValue("SALDO");
                table.C11 = html.HeaderValue("COSTO");
                cols += html.GetConcatRows(table);
                table.ColumnStyle();

                table.C3 = html.HeaderValue("ANTERIOR");
                table.C4 = html.HeaderValue("INGRESO");
                table.C5 = html.HeaderValue("SALIDA");
                table.C6 = html.HeaderValue("FINAL");
                table.C7 = html.HeaderValue("ANTERIOR");
                table.C8 = html.HeaderValue("INGRESO");
                table.C9 = html.HeaderValue("SALIDA");
                table.C10 = html.HeaderValue("FINAL");
                table.C11 = html.HeaderValue("PRE.UNI");
                cols += html.GetConcatRows(table);
                table.RowStyle();

                template = template.Replace("@cols", cols);


                List<Kardex> kardex = ConvertDataTable<Kardex>(dtKardex);

                var groupKardex = kardex.GroupBy((G) => G.cod_producto);

                Cantidad cantidad;
                Importe importe;
                Total total = new Total(0, 0);
                //int iCantidadTotalAnterior = 0, iCantidadEntradaActualTotal = 0, iCantidadSalidaActualTotal = 0, iCantidadSaldoTotal = 0;
                //decimal dImporteTotalAnterior = 0, dImporteEntradaActualTotal = 0, dImporteSalidaActualTotal = 0, dImporteSaldoTotal = 0;

                foreach (var group in groupKardex)
                {
                    cantidad = new Cantidad(0);
                    importe = new Importe(0);

                    string _codProducto = string.Empty, _dscProducto = string.Empty, _codAlmacen = string.Empty;

                    decimal dImpUnitario = 0, dAcumuladoSaldo = 0, dAcumuladoActual = 0;
                    int iAcumuladoSaldo = 0, iAcumuladoActual = 0, index = -1;

                    decimal dImporte = 0, dImportePonderado = 0; int iCantidad = 0;
                    foreach (var _kardex in group)
                    {
                        if (_kardex.cod_producto.Equals("0000000705"))
                        {
                            var hola = "";
                        }

                        if (_kardex.cod_producto.Equals("0000000ABC"))
                        {
                            var hola = "";
                        }
                        if (_kardex.cod_producto.Equals("0000000ABC"))
                        {
                            var hola = "";
                        }
                        if (_kardex.cod_producto.Equals("0000001ABC"))
                        {
                            var hola = "";
                        }
                        if (_kardex.cod_producto.Equals("0000000ABC"))
                        {
                            var hola = "";
                        }


                        index++;
                        if (_codProducto == string.Empty)
                        {
                            _codProducto = _kardex.cod_producto;
                            _dscProducto = _kardex.Producto;
                            _codAlmacen = _kardex.cod_almacen;
                        }
                        if (_kardex.dsc_proveedor_cliente != null && _kardex.dsc_proveedor_cliente.ToLower() == "saldo")
                        {
                            if (index == 0) { dImpUnitario = _kardex.PrecioUnitario; }

                            dImporte += (_kardex.total_entrada - _kardex.total_salida);
                            iCantidad += (_kardex.cantidad_entrada - _kardex.cantidad_salida);
                            dImportePonderado = iCantidad == 0 ? 0 : (dImporte / iCantidad);

                            //Acumular cantidades anteriores
                            cantidad.AcumuladoEntradaSaldo += _kardex.cantidad_entrada;
                            cantidad.AcumuladoSalidaSaldo += _kardex.cantidad_salida;
                            //Acumular importes anteriores
                            importe.AcumuladoEntradaSaldo += (_kardex.cantidad_entrada * _kardex.PrecioUnitario); //dImpUnitario
                            importe.AcumuladoSalidaSaldo += (_kardex.cantidad_salida * _kardex.PrecioUnitario); //dImpUnitario
                            //Obtener Acumulados en el indice.
                            dAcumuladoSaldo = (importe.AcumuladoEntradaSaldo - importe.AcumuladoSalidaSaldo);
                            iAcumuladoSaldo = (cantidad.AcumuladoEntradaSaldo - cantidad.AcumuladoSalidaSaldo);
                            //Obtener el ponderado para recalcular.
                            decimal ponderado = dAcumuladoSaldo / (iAcumuladoSaldo == 0 ? 1 : iAcumuladoSaldo);
                            dImpUnitario = ponderado > 0 ? ponderado : dImpUnitario;
                        }
                        else
                        {
                            iCantidad += (_kardex.cantidad_entrada - _kardex.cantidad_salida);
                            if (_kardex.cantidad_salida == 0) { dImporte += (_kardex.total_entrada - _kardex.total_salida); }
                            //if (index == 0 || dImpUnitario == 0) { dImpUnitario = _kardex.PrecioUnitario; }
                            //if(dImpUnitario<=0) { dImpUnitario = _kardex.PrecioUnitario; }
                            decimal costoSalida = 0;
                            if (_kardex.cantidad_salida > 0)
                            {
                                costoSalida = dImportePonderado;
                                _kardex.total_salida = (costoSalida * _kardex.cantidad_salida);
                                dImporte += (_kardex.total_entrada - _kardex.total_salida);
                            }
                            dImportePonderado = iCantidad == 0 ? 0 : (dImporte / iCantidad);


                            cantidad.AcumuladoEntradaActual += _kardex.cantidad_entrada;
                            importe.AcumuladoEntradaActual += _kardex.total_entrada;
                            //(_kardex.cantidad_entrada * dImpUnitario);// _kardex.PrecioUnitario); //dImpUnitario
                            //(_kardex.cantidad_salida * dImpUnitario);//_kardex.PrecioUnitario); //dImpUnitario

                            cantidad.AcumuladoSalidaActual += _kardex.cantidad_salida;
                            importe.AcumuladoSalidaActual += _kardex.total_salida;

                            dAcumuladoActual = (importe.AcumuladoEntradaSaldo + importe.AcumuladoEntradaActual) - (importe.AcumuladoSalidaSaldo + importe.AcumuladoSalidaActual);
                            iAcumuladoActual = (cantidad.AcumuladoEntradaSaldo + cantidad.AcumuladoEntradaActual) - (cantidad.AcumuladoSalidaActual + cantidad.AcumuladoSalidaSaldo);

                            //decimal ponderado = iAcumuladoActual == 0 ? 0 : (dAcumuladoActual / iAcumuladoActual);//dAcumuladoActual / (iAcumuladoActual == 0 ? 1 : iAcumuladoActual);

                            //var iSaldo = (iAcumuladoSaldo + _kardex.cantidad_entrada - _kardex.cantidad_salida);
                            //var dSaldo =(dAcumuladoSaldo+_kardex.total_entrada-_kardex.total_salida);
                            //var aaa = _kardex.cantidad_final;
                            //var bbb = _kardex.total_final;
                            //var ddddddd = (dSaldo / (iSaldo==0?1:iSaldo));
                            //var iiiiii = (bbb / (aaa==0?1:aaa));

                            // dImpUnitario = (dSaldo / (iSaldo == 0 ? 1 : iSaldo));



                            //var f = (_kardex.cantidad_salida * dImpUnitario);

                            //var salida = 


                            //dImpUnitario = ponderado > 0 ? ponderado : dImpUnitario;
                        }
                    }

                    cantidad.SaldoFinal = (cantidad.AcumuladoEntradaSaldo - cantidad.AcumuladoSalidaSaldo);
                    importe.SaldoFinal = (importe.AcumuladoEntradaSaldo - importe.AcumuladoSalidaSaldo);

                    cantidad.ActualFinal = (cantidad.SaldoFinal + cantidad.AcumuladoEntradaActual) - cantidad.AcumuladoSalidaActual;
                    importe.ActualFinal = (importe.SaldoFinal + importe.AcumuladoEntradaActual) - importe.AcumuladoSalidaActual;

                    table.C1 = html.RowValue(_codProducto);
                    table.C2 = html.RowValue(_dscProducto);
                    table.C3 = html.RowValue(cantidad.SaldoFinal);
                    table.C4 = html.RowValue(cantidad.AcumuladoEntradaActual);
                    table.C5 = html.RowValue(cantidad.AcumuladoSalidaActual);
                    table.C6 = html.RowValue(cantidad.ActualFinal);
                    table.C7 = html.RowValue(importe.SaldoFinal);
                    table.C8 = html.RowValue(importe.AcumuladoEntradaActual);
                    table.C9 = html.RowValue(importe.AcumuladoSalidaActual);
                    table.C10 = html.RowValue(importe.ActualFinal);
                    table.C11 = html.RowValue(dImpUnitario);
                    table.C12 = html.RowValue(_codAlmacen);

                    //iCantidadTotalAnterior += cantidad.SaldoFinal;
                    //iCantidadEntradaActualTotal += cantidad.AcumuladoEntradaActual;
                    //iCantidadSalidaActualTotal += cantidad.AcumuladoSalidaActual;
                    //iCantidadSaldoTotal += cantidad.ActualFinal;

                    //dImporteTotalAnterior += importe.SaldoFinal;
                    //dImporteEntradaActualTotal += importe.AcumuladoEntradaActual;
                    //dImporteSalidaActualTotal += importe.AcumuladoSalidaActual;
                    //dImporteSaldoTotal += importe.ActualFinal;

                    total.CantidadAnterior += cantidad.SaldoFinal;
                    total.CantidadEntradaActual += cantidad.AcumuladoEntradaActual;
                    total.CantidadSalidaActual += cantidad.AcumuladoSalidaActual;
                    total.CantidadSaldoFinal += cantidad.ActualFinal;

                    total.ImporteAnterior += importe.SaldoFinal;
                    total.ImporteEntradaActual += importe.AcumuladoEntradaActual;
                    total.ImporteSalidaActual += importe.AcumuladoSalidaActual;
                    total.ImporteSaldoFinal += importe.ActualFinal;

                    rows += html.GetConcatRows(table);
                    table.RowStyle();
                }

                rows += html.GetConcatRows(table);
                table.RowStyle();

                table.C2 = html.RowValue("SALDO INICIAL");
                table.C3 = html.RowValue(total.CantidadAnterior);// iCantidadTotalAnterior);// cant_anterior_tot);// ctd_saldos);
                table.C7 = html.RowValue(total.ImporteAnterior);//dImporteTotalAnterior);//tot_anterior_tot);// total_saldos);
                rows += html.GetConcatRows(table);
                table.RowStyle();

                table.C2 = html.RowValue("TOTAL GENERAL :");
                table.C4 = html.RowValue(total.CantidadEntradaActual);//iCantidadEntradaActualTotal);//ctd_entradas_tot);
                table.C5 = html.RowValue(total.CantidadSalidaActual);//iCantidadSalidaActualTotal);//ctd_salidas_tot);
                table.C8 = html.RowValue(total.ImporteEntradaActual);//dImporteEntradaActualTotal);//total_entradas_tot);
                table.C9 = html.RowValue(total.ImporteSalidaActual);//dImporteSalidaActualTotal);//total_salidas_tot);
                rows += html.GetConcatRows(table);
                table.RowStyle();

                table.C2 = html.RowValue("SALDO TOTAL : ");
                table.C6 = html.RowValue(total.CantidadSaldoFinal);//iCantidadSaldoTotal);//(ctd_entradas_tot - ctd_salidas_tot));
                table.C10 = html.RowValue(total.ImporteSaldoFinal);//dImporteSaldoTotal);// dImporteSaldoTotal);//(total_entradas_tot - total_salidas_tot));
                rows += html.GetConcatRows(table);
                table.RowStyle();

                template = template.Replace("@rows", rows);

                string htmlContent = template;
                html.ParseHtmlToExcel(htmlContent, "Inventario_detalles", Tools.Hng_Htmltools.HtmlExportFormat.Inventario);

                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                HNG.MessageError("Error al Generar Reporte. " + ex.Message, "Generar Reporte");
            }
        }

        private void frmListaInventarioAlmacen_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btnExportarTXT13_1_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {


                //////////LIMPIA LA CARPETA DONDE SE EXPORTA EL TXT DE PAGOS
                if (!Directory.Exists("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados")) Directory.CreateDirectory("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados");
                DirectoryInfo source = new DirectoryInfo("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados");
                FileInfo[] filesToCopy = source.GetFiles();
                foreach (FileInfo oFile in filesToCopy)
                {
                    oFile.Delete();
                }
                foreach (string oCarpeta in Directory.GetDirectories("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados"))
                {
                    Directory.Delete(oCarpeta, true);
                }

                if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                frmRangoFecha frm = new frmRangoFecha();
                frm.ShowDialog();
                if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;

                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando TXT", "Cargando...");
                string msgError = "";
                List<eAlmacen.eReporteInventario> eLista = new List<eAlmacen.eReporteInventario>(); //LISTA PRODUCTO
                eLista = unit.Logistica.Obtener_ReporteLogistica_InventarioPermanenteValorizado<eAlmacen.eReporteInventario>(lkpAlmacen.EditValue.ToString(), cod_empresa, lkpSedeEmpresa.EditValue.ToString(), frm.fechaInicio.ToString("yyyyMMdd"), frm.fechaFin.ToString("yyyyMMdd"));

                if (eLista.Count > 0)
                {
                    /*cod_bloque_pago = "";*/ /*nuevo_bloque_pago = "SI";*/
                    //////////GENERA EL TXT DE PAGOS
                    StreamWriter sw = new StreamWriter("C:\\IMPERIUM-Software\\Recursos\\ArchivosExportados\\TXT_Libro13_1.txt");
                    foreach (eAlmacen.eReporteInventario obj in eLista)
                    {
                        if (msgError != "") { SplashScreenManager.CloseForm(); HNG.MessageError(msgError, "ERROR"); return; }
                        if (obj == null) continue;
                        if (obj.tipo_documento == "" | obj.tipo_documento == null) continue;
                        string campo_1, campo_2, campo_3, campo_4, campo_5, campo_6, campo_7, campo_8, campo_9, campo_10, campo_11, campo_12, campo_13, campo_14, campo_15,
                            campo_16, campo_17, campo_18, campo_19, campo_20, campo_21, campo_22, campo_23, campo_24, campo_25, campo_26, campo_27;

                        campo_1 = obj.dsc_periodo; 
                        campo_2 = obj.dsc_CUO;
                        campo_3 = obj.dsc_correlativo; 
                        campo_4 = obj.cod_establecimiento; 
                        campo_5 = obj.cod_catalogo;
                        campo_6 = obj.dsc_tipo_existencia;
                        campo_7 = obj.cod_existencia.Length >= 24 ? obj.cod_existencia.Substring(0, 24) : obj.cod_existencia;
                        campo_8 = obj.cod_tipo_existencia;
                        campo_9 = obj.cod_existencia_acuerdo.Length >= 128 ? obj.cod_existencia_acuerdo.Substring(0, 128) : obj.cod_existencia_acuerdo;
                        campo_10 = Convert.ToDateTime(obj.fch_documento).ToString("dd/MM/yyyy");
                        campo_11 = obj.tipo_documento_SUNAT;
                        campo_12 = obj.serie_documento;
                        campo_13 = obj.numero_documento;
                        campo_14 = obj.cod_tipo_operacion;
                        campo_15 = obj.dsc_producto.Length >= 80 ? obj.dsc_producto.Substring(0, 80) : obj.dsc_producto;
                        campo_16 = obj.cod_und_medida_SUNAT;
                        campo_17 = obj.cod_valuacion_existencia;
                        campo_18 = obj.cantidad_entrada.ToString();
                        campo_19 = obj.costo_entrada.ToString();
                        campo_20 = obj.total_entrada.ToString();
                        campo_21 = obj.cantidad_salida.ToString();
                        campo_22 = obj.costo_salida.ToString();
                        campo_23 = obj.total_salida.ToString();
                        campo_24 = obj.cantidad_final.ToString();
                        campo_25 = obj.costo_ponderado.ToString();
                        campo_26 = obj.total_final.ToString();
                        campo_27 = obj.cod_estado_operacion;

                        sw.WriteLine(campo_1 + "|" + campo_2 + "|" + campo_3 + "|" + campo_4 + "|" + campo_5 + "|" + campo_6 + "|" + campo_7 + "|" + campo_8 + "|" +
                                    campo_9 + "|" + campo_10 + "|" + campo_11 + "|" + campo_12 + "|" + campo_13 + "|" + campo_14 + "|" + campo_15 + "|" + campo_16 + "|" +
                                    campo_17 + "|" + campo_18 + "|" + campo_19 + "|" + campo_20 + "|" + campo_21 + "|" + campo_22 + "|" + campo_23 + "|" + campo_24 + "|" +
                                    campo_25 + "|" + campo_26 + "|" + campo_27 + "|");
                    }
                    sw.Close();
                    SplashScreenManager.CloseForm();
                    Process.Start(@"C:\IMPERIUM-Software\Recursos\ArchivosExportados\TXT_Libro13_1.txt");
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                HNG.MessageError("Error al Generar Reporte. " + ex.Message, "Generar Reporte");
            }
        }

        private void btnAsientoConsumo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lkpSedeEmpresa.EditValue == null) { MessageBox.Show("Debe seleccionar la sede.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresa.Focus(); return; }
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
            frmRangoFecha frm = new frmRangoFecha();
            frm.ShowDialog();
            if (frm.fechaInicio.ToString().Contains("1/01/0001")) return;

            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Exportando Asiento", "Cargando...");
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
                worksheet.Name = "Importacion_SISPAG";
                objExcel.ActiveWindow.DisplayGridlines = false;

                int fila = 0;
                fila = fila + 1;
                procedure = "usp_Reporte_Logistica_AsientoDeConsumo @cod_almacen = '" + lkpAlmacen.EditValue.ToString() +
                                                "', @cod_empresa = '" + cod_empresa +
                                                "', @cod_sede_empresa = '" + lkpSedeEmpresa.EditValue.ToString() +
                                                "', @FechaInicio = '" + frm.fechaInicio.ToString("yyyyMMdd") +
                                                "', @FechaFin = '" + frm.fechaFin.ToString("yyyyMMdd") + "'";
                unit.Factura.pDatosAExcel(cnxl, objExcel, procedure, "Consulta", "A" + fila, true);
                if (fila > 1) objExcel.Rows[fila].Delete();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

                objExcel.Range["A:A"].Delete();
                objExcel.Range["A1"].Select();
                fila = objExcel.Cells.Find("*", System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                worksheet.Rows(2).Insert();
                worksheet.Rows(2).Insert();
                fila = fila + 2;
                //int fila = nInLastRow;

                objExcel.Range["A1:AO1"].Select();
                objExcel.Selection.Borders.Color = System.Drawing.Color.FromArgb(0, 0, 0);
                objExcel.Selection.Font.Bold = true;
                objExcel.Selection.Font.Color = System.Drawing.Color.Black;
                objExcel.Selection.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FFC000");
                objExcel.Range["A1:AO" + fila].Font.Name = "Century Gothic";
                objExcel.Range["A1:AO" + fila].Font.Size = 10;

                objExcel.Range["A1:AO" + fila].Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AO" + fila].Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AO" + fila].Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AO" + (fila + 1)].Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AO" + fila].Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AO" + fila].Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                objExcel.Range["A1:AO1"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                objExcel.Range["A1:AO1"].Borders.Color = System.Drawing.Color.FromArgb(0, 0, 0);

                objExcel.Range["A1"].RowHeight = 70;
                objExcel.Range["A1:AO" + fila].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                objExcel.Range["A1:AO" + fila].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                objExcel.Range["A1:AO" + fila].WrapText = true;
                //objExcel.Range["A:AR"].ColumnWidth = 18;


                //objExcel.Range["C4"].Value = "Tipo de Solicitud";
                //objExcel.Range["D4"].Value = "Area";

                objExcel.Range["A1:AO1"].AutoFilter(1, Type.Missing, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                objExcel.Range["A1"].Select();

                sheet.Delete();
                objExcel.WindowState = Excel.XlWindowState.xlMaximized;
                objExcel.Visible = true;
                objExcel = null/* TODO Change to default(_) if this is not a reference type */;
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                HNG.MessageError("Error al Generar Reporte. " + ex.Message, "Generar Reporte");
            }
        }
    }
}