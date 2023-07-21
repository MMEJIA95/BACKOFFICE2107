using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Configuration;
using System.Globalization;
using System.Net;
using UI_BackOffice.Clientes_Y_Proveedores.Clientes;
using BE_BackOffice;
using BL_BackOffice;
using UI_BackOffice.Formularios.Clientes_Y_Proveedores.Proveedores;
using DevExpress.Utils;
using System.Xml;
using UI_BackOffice.Formularios.Sistema.Accesos;
using UI_BackOffice.Formularios.Sistema.Sistema;
using UI_BackOffice.Formularios.Sistema.Configuracion_del_Sistema;
using UI_BackOffice.Formularios.Cuentas_Pagar;
using UI_BackOffice.Formularios.Sistema.Configuraciones_Maestras;
using UI_BackOffice.Formularios.Personal;
using UI_BackOffice.Formularios.Creditos;
using System.IO;
using UI_BackOffice.Formularios.Logistica;
using UI_BackOffice.Formularios.Bancos;

namespace UI_BackOffice
{
    public partial class frmPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        public string cod_empresa = "", Entorno = "LOCAL", Servidor = "", BBDD = "", FormatoFecha = "";
        public string formName;

        public frmPrincipal()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            frmDashBoardPrincipal frm = new frmDashBoardPrincipal();
            frm.MdiParent = this;
            frm.Show();
            InhabilitarBotones();
            Inicializar();
            HabilitarBotones();
            btnEliminarExportados.Enabled = true;
            CrearSoluciones();
        }

        #region Espacio de Soluciones
        void CrearSoluciones()
        {
            var modulos = unit.Sistema.ListarSolucion<eSolucionUsuario_Consulta>(
               opcion: 1, cod_usuario: Program.Sesion.Usuario.cod_usuario);
            if (modulos != null && modulos.Count > 0)
            {
                new ConfigSesion().CrearButtonModulos(modulos, rpgSolucion);
            }
        }
        #endregion

        private void Inicializar()
        {
            string IP = ObtenerIP();
            ObtenerUsuario();

            Entorno = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("conexion")].ToString());
            string Servidor = Entorno == "LOCAL" ? unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorLOCAL")].ToString()) : unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("ServidorREMOTO")].ToString());
            string BBDD = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("BBDD")].ToString());
            string Version = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("VersionApp")].ToString());
            string nombrePC = Environment.MachineName;
            lblServidor.Caption = "Conectado a -> " + Servidor + " - " + BBDD;
            lblIPAddress.Caption = "IP : " + IP;
            lblHostName.Caption = "Nombre Equipo : " + nombrePC;
            lblVersion.Caption = "Versión: " + Version;
            lblUsuario.Caption = Program.Sesion.Usuario.dsc_usuario.ToUpper();
            //entorno = Entorno;
            switch (Entorno)
            {
                case "LOCAL": lblEntorno.Caption = "LOCAL"; lblEntorno.ItemAppearance.Normal.BackColor = Color.Green; lblEntorno.ItemAppearance.Normal.ForeColor = Color.White; break;
                case "REMOTO": lblEntorno.Caption = "REMOTO"; lblEntorno.ItemAppearance.Normal.BackColor = Color.DarkKhaki; lblEntorno.ItemAppearance.Normal.ForeColor = Color.Black; break;
            }
            lblEntorno.Caption = Entorno;
            SuperToolTip tool = new SuperToolTip();
            SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();
            args.Contents.Text = Servidor + " -> " + BBDD;
            tool.Setup(args);
            lblServidor.SuperTip = tool;
        }
        private void ObtenerUsuario()
        {
            eUsuario user = unit.Usuario.ObtenerUsuarioLogin<eUsuario>(1, Program.Sesion.Usuario.cod_usuario);
        }
        private string ObtenerIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        private void InhabilitarBotones()
        {
            foreach (var item in ribbon.Items)
            {
                if (item.GetType() == typeof(BarButtonItem))
                {
                    if (((BarButtonItem)item).Name != "btnCambiarContraseña" && ((BarButtonItem)item).Name != "btnHistorialVersiones" &&
                        ((BarButtonItem)item).Name != "btnAcercaDeSistema")
                    {
                        ((BarButtonItem)item).Enabled = false;
                    }
                }
            }
        }
        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, null, Program.Sesion.Global.Solucion);

            if (listPermisos.Count > 0)
            {
                for (int i = 0; i < listPermisos.Count; i++)
                {
                    foreach (var item in ribbon.Items)
                    {
                        if (item.GetType() == typeof(BarButtonItem))
                        {
                            if (((BarButtonItem)item).Name == listPermisos[i].dsc_menu)
                            {
                                ((BarButtonItem)item).Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void btnListadoUsuario_ItemClick(object sender, ItemClickEventArgs e)
        {
            //formName = "ListaUsuarios";
            //if (Application.OpenForms["frmListadoUsuario"] != null)
            //{
            //    Application.OpenForms["frmListadoUsuario"].Activate();
            //}
            //else
            //{
            //    frmListadoUsuario frm = new frmListadoUsuario();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            new HNG.SistemasHNG().Unit.Forms.OpenMdiListaUsuarios(this);
        }

        private void btnOpcionesSistema_ItemClick(object sender, ItemClickEventArgs e)
        {
            //formName = "ListaOpcionesSistema";
            //if (Application.OpenForms["frmOpcionesSistema"] != null)
            //{
            //    Application.OpenForms["frmOpcionesSistema"].Activate();
            //}
            //else
            //{
            //    frmOpcionesSistema frm = new frmOpcionesSistema();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            new HNG.SistemasHNG().Unit.Forms.OpenMdiListaOpcionesSistema(this);
        }

        private void btnAsignacionPermiso_ItemClick(object sender, ItemClickEventArgs e)
        {
            //formName = "ListaPermisos";
            //if (Application.OpenForms["frmAsignacionPermiso"] != null)
            //{
            //    Application.OpenForms["frmAsignacionPermiso"].Activate();
            //}
            //else
            //{
            //    frmAsignacionPermiso frm = new frmAsignacionPermiso();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            new HNG.SistemasHNG().Unit.Forms.OpenMdiListaPermisos(this);
        }

        private void btnFacturaProveedor_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Facturas Proveedor";
            frmListadoFacturaProveedor frm = new frmListadoFacturaProveedor();
            if (Application.OpenForms["frmListadoFacturaProveedor"] != null)
            {
                Application.OpenForms["frmListadoFacturaProveedor"].Activate();
            }
            else
            {
                frm.MdiParent = this;
                frm.Show();
            }
        }
        
        private void btnResumenCuentasxPagar_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Resumen Cuentas por Cobrar";
            if (Application.OpenForms["frmResumenCuentasCobrar"] != null)
            {
                Application.OpenForms["frmResumenCuentasCobrar"].Activate();
            }
            else
            {
                frmResumenCuentasPagar frm = new frmResumenCuentasPagar();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnUndNegocioTipoGastoCostoEmpresa_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (Application.OpenForms["frmMantUnidades_Negocio"] != null)
            //{
            //    Application.OpenForms["frmMantUnidades_Negocio"].Activate();
            //}
            //else
            //{
            //    frmMantUnidades_Negocio frm = new frmMantUnidades_Negocio();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            new HNG.SistemasHNG().Unit.Forms.OpenMdiMantUnidades_Negocio(this);
        }

        private void btnTipoCambio_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (Application.OpenForms["frmMantTipoCambio"] != null)
            //{
            //    Application.OpenForms["frmMantTipoCambio"].Activate();
            //}
            //else
            //{
            //    frmMantTipoCambio frm = new frmMantTipoCambio();
            //    frm.ShowDialog();
            //}
            new HNG.SistemasHNG().Unit.Forms.OpenNormalMantTipoCambio("Tipo de Cambio");
        }

        private void btnProgramacionPagos_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmProgramacionPagos"] != null)
            {
                Application.OpenForms["frmProgramacionPagos"].Activate();
            }
            else
            {
                frmProgramacionPagos frm = new frmProgramacionPagos();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnTipoGastoCosto_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantTipoGastoCosto"] != null)
            {
                Application.OpenForms["frmMantTipoGastoCosto"].Activate();
            }
            else
            {
                frmMantTipoGastoCosto frm = new frmMantTipoGastoCosto();
                //frm.MdiParent = this;
                frm.ShowDialog();
            }
        }

        private void btnResumenCuentasxPagarSemanal_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Resumen Cuentas por Cobrar Semanal";
            if (Application.OpenForms["frmResumenCuentasCobrarSemanal"] != null)
            {
                Application.OpenForms["frmResumenCuentasCobrarSemanal"].Activate();
            }
            else
            {
                frmResumenCuentasPagarSemanal frm = new frmResumenCuentasPagarSemanal();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnResgistroTrabajador_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Trabajador";
            if (Application.OpenForms["frmMantTrabajador"] != null)
            {
                Application.OpenForms["frmMantTrabajador"].Activate();
            }
            else
            {
                frmMantTrabajador frm = new frmMantTrabajador();
                frm.MiAccion = Trabajador.Nuevo;
                frm.ShowDialog();
            }
        }

        private void btnSimuladorCuotasFijas_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmSimuladorCuotasFijas"] != null)
            {
                Application.OpenForms["frmSimuladorCuotasFijas"].Activate();
            }
            else
            {
                frmSimuladorCuotasFijas frm = new frmSimuladorCuotasFijas();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnResumenMovCajaChica_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ResumenMovCajaChica";
            if (Application.OpenForms["frmResumenMovCajaChica"] != null)
            {
                Application.OpenForms["frmResumenMovCajaChica"].Activate();
            }
            else
            {
                frmResumenMovCajaChica frm = new frmResumenMovCajaChica();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnAperturaCajaChica_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "AperturaCajaChica";
            if (Application.OpenForms["frmAperturaCajaChica"] != null)
            {
                Application.OpenForms["frmAperturaCajaChica"].Activate();
            }
            else
            {
                frmAperturaCajaChica frm = new frmAperturaCajaChica();
                frm.ShowDialog();
            }
        }

        private void btnMovCajaChica_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "MovimientoCajaChica";
            if (Application.OpenForms["frmMovimientosCajaChica"] != null)
            {
                Application.OpenForms["frmMovimientosCajaChica"].Activate();
            }
            else
            {
                frmMovimientosCajaChica frm = new frmMovimientosCajaChica();
                frm.ShowDialog();
            }
        }

        private void btnListadoTrabajador_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaTrabajador";
            if (Application.OpenForms["frmListadoTrabajador"] != null)
            {
                Application.OpenForms["frmListadoTrabajador"].Activate();
            }
            else
            {
                frmListadoTrabajador frm = new frmListadoTrabajador();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnResumenEntregasRendir_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ResumenEntregasRendir";
            if (Application.OpenForms["frmResumenEntregasRendir"] != null)
            {
                Application.OpenForms["frmResumenEntregasRendir"].Activate();
            }
            else
            {
                frmResumenEntregasRendir frm = new frmResumenEntregasRendir();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnListadoCreditos_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmListadoCreditos"] != null)
            {
                Application.OpenForms["frmListadoCreditos"].Activate();
            }
            else
            {
                frmListadoCreditos frm = new frmListadoCreditos(this);
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnResumenPresupuestoEjecucion_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmResumenPresupuestoEjecucion"] != null)
            {
                Application.OpenForms["frmResumenPresupuestoEjecucion"].Activate();
            }
            else
            {
                frmResumenPresupuestoEjecucion frm = new frmResumenPresupuestoEjecucion();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnEliminarExportados_ItemClick(object sender, ItemClickEventArgs e)
        {
            //OBTENEMOS LA RUTA DONDE ESTAN LOS ARCHIVOS DESCARGADOS
            var carpeta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString());
            DirectoryInfo source = new DirectoryInfo(carpeta);
            FileInfo[] filesToCopy = source.GetFiles();
            foreach (FileInfo oFile in filesToCopy)
            {
                oFile.Delete();
            }
            MessageBox.Show("Se procedió a eliminar los archivos exportados del sistema", "Eliminar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCrearTipoServicioProducto_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                frmAgregarTipoSubTipo frm = new frmAgregarTipoSubTipo();
                frm.MiAccion = AgregarTipoSubTipo.Tipo;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearSubTipoServicioSubProducto_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //frmAgregarTipoSubTipo frm = new frmAgregarTipoSubTipo();
                frmMantTipoSubTipo frm = new frmMantTipoSubTipo();
                //frm.MiAccion = AgregarTipoSubTipo.SubTipo;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultaProductosSunat_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaProductosSUNAT";
            if (Application.OpenForms["frmListaProductosSunat"] != null)
            {
                Application.OpenForms["frmListaProductosSunat"].Activate();
            }
            else
            {
                frmListaProductosSunat frm = new frmListaProductosSunat();
                //frm.MdiParent = this;
                frm.ShowDialog();
            }
        }

        private void btnCrearProductos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "CrearProductos";
            if (Application.OpenForms["frmMantProductos"] != null)
            {
                Application.OpenForms["frmMantProductos"].Activate();
            }
            else
            {
                frmMantProductos frm = new frmMantProductos();
                //frm.MdiParent = this;
                frm.ShowDialog();
            }
        }

        private void btnListaPreciosProducto_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaPrecios-Producto";
            if (Application.OpenForms["frmListadoProductoPrecios"] != null)
            {
                Application.OpenForms["frmListadoProductoPrecios"].Activate();
            }
            else
            {
                frmListadoProductoPrecios frm = new frmListadoProductoPrecios();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnInventarioAlmacen_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "InventarioAlmacen";
            if (Application.OpenForms["frmListaInventarioAlmacen"] != null)
            {
                Application.OpenForms["frmListaInventarioAlmacen"].Activate();
            }
            else
            {
                frmListaInventarioAlmacen frm = new frmListaInventarioAlmacen();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnListadoRequerimientos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaRequerimientos";
            frmListadoRequerimientos frm = new frmListadoRequerimientos();
            if (Application.OpenForms["frmListadoRequerimientos"] != null)
            {
                Application.OpenForms["frmListadoRequerimientos"].Activate();
            }
            else
            {
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnListadoOrdenesCompra_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaOrdenesCompra";
            frmListadoOrdenesCompra frm = new frmListadoOrdenesCompra();
            if (Application.OpenForms["frmListadoOrdenesCompra"] != null)
            {
                Application.OpenForms["frmListadoOrdenesCompra"].Activate();
            }
            else
            {
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnListadoOrdenesServicio_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Listado de Ordenes de Servicio";
            frmListadoOrdenesServicio frm = new frmListadoOrdenesServicio();
            if (Application.OpenForms["frmListadoOrdenesServicio"] != null)
            {
                Application.OpenForms["frmListadoOrdenesServicio"].Activate();
            }
            else
            {
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnControlHoras_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmControlHoras"] != null)
            {
                Application.OpenForms["frmControlHoras"].Activate();
            }
            else
            {
                frmListaControlHoras frm = new frmListaControlHoras();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnHistorialVersiones_ItemClick(object sender, ItemClickEventArgs e)
        {
            //formName = "HistorialVersiones";
            //if (Application.OpenForms["frmHistorialVersiones"] != null)
            //{
            //    Application.OpenForms["frmHistorialVersiones"].Activate();
            //}
            //else
            //{
            //    frmHistorialVersiones frm = new frmHistorialVersiones();
            //    frm.ShowDialog();
            //}
            new HNG.SistemasHNG().Unit.Forms.OpenNormalHistorialVersiones("Histoial de Versiones");
        }

        private void btnListaMovimientoBancos_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaMovimientoBancos";
            if (Application.OpenForms["frmListadoMovimientoBancos"] != null)
            {
                Application.OpenForms["frmListadoMovimientoBancos"].Activate();
            }
            else
            {
                frmListadoMovimientoBancos frm = new frmListadoMovimientoBancos();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnCtasBancariasEmpresa_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "BancoEmpresa";
            if (Application.OpenForms["frmCtasBancariasEmpresa"] != null)
            {
                Application.OpenForms["frmCtasBancariasEmpresa"].Activate();
            }
            else
            {
                frmCtasBancariasEmpresa frm = new frmCtasBancariasEmpresa();
                frm.ShowDialog();
            }
        }

        private void ribbon_Merge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {
            e.MergeOwner.SelectedPage = e.MergeOwner.MergedPages.GetPageByName(e.MergedChild.SelectedPage.Name);
        }

        private void btnListadoCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaClientes";
            if (Application.OpenForms["frmListadoClientes"] != null)
            {
                Application.OpenForms["frmListadoClientes"].Activate();
            }
            else
            {
                frmListadoClientes frm = new frmListadoClientes();
                frm.MdiParent = this;
                frm.Show();
            }
        }
        
        private void btnRegistroCliente_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Clientes";
            if (Application.OpenForms["frmMantCliente"] != null)
            {
                Application.OpenForms["frmMantCliente"].Activate();
            }
            else
            {
                frmMantCliente frm = new frmMantCliente();
                frm.ShowDialog();
            }
        }

        private void btnListadoSolicitudesCompras_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListadoSolicitudCompra";
            if (Application.OpenForms["frmListadoSolicitudCompra"] != null)
            {
                Application.OpenForms["frmListadoSolicitudCompra"].Activate();
            }
            else
            {
                frmListadoSolicitudCompra frm = new frmListadoSolicitudCompra();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnAprobaciones_ItemClick(object sender, ItemClickEventArgs e) 
        {
            if (Application.OpenForms["frmDocumentosporAprobar"] != null)
            {
                Application.OpenForms["frmDocumentosporAprobar"].Activate();
            }
            else
            {
                frmDocumentosporAprobar frmModif = new frmDocumentosporAprobar();
                frmModif.MdiParent = this;
                frmModif.Show();
            }
        }

        private void btnListadoProveedores_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "ListaProveedores";
            if (Application.OpenForms["frmListadoProveedores"] != null)
            {
                Application.OpenForms["frmListadoProveedores"].Activate();
            }
            else
            {
                frmListadoProveedores frm = new frmListadoProveedores();
                frm.MdiParent = this;
                frm.Show();
            }
        }
        private void btnRegistroProveedores_ItemClick(object sender, ItemClickEventArgs e)
        {
            formName = "Proveedores";
            if (Application.OpenForms["frmMantProveedor"] != null)
            {
                Application.OpenForms["frmMantProveedor"].Activate();
            }
            else
            {
                frmMantProveedor frm = new frmMantProveedor();
                frm.ShowDialog();
            }
        }
        
        private void btnCambiarContraseña_ItemClick(object sender, ItemClickEventArgs e)
        {
            //frmCambiarContraseña frm = new frmCambiarContraseña();
            //frm.Show();
            new HNG.SistemasHNG().Unit.Forms.OpenNormalCambiarContraseña("Cambiar Contraseña");
        }
        
        private void btnAcercaDeSistema_ItemClick(object sender, ItemClickEventArgs e)
        {
            //frmAcercaSistema frm = new frmAcercaSistema();
            //frm.ShowDialog();
            new HNG.SistemasHNG().Unit.Forms.OpenNormalAcercaSistema("Acerca del Sistema");
        }
    }
}