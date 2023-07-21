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
using UI_BackOffice.Tools.FiltrosAprobaciones;
using UI_BackOffice;
using BE_BackOffice;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraTreeList;
using IMPERIUM_Sistema.BE_Sistema;
using DevExpress.CodeParser;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{

    public partial class frmDocumentosporAprobar : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;
        List<EAprobaciones> listapendientes = new List<EAprobaciones>();
        List<EAprobaciones> listaaprobados = new List<EAprobaciones>();
        List<EAprobaciones.Ecajachica> listarcajachica = new List<EAprobaciones.Ecajachica>();
        List<EAprobaciones.Ecajachica> listarcajachicaaprobados = new List<EAprobaciones.Ecajachica>();
        List<EAprobaciones.Ecajachica> listarcajachicadetalles = new List<EAprobaciones.Ecajachica>();
        List<EAprobaciones.EEntregasRendir> listarentregasrendir = new List<EAprobaciones.EEntregasRendir>();
        List<EAprobaciones.EEntregasRendir> listarentregasrendiraprobados = new List<EAprobaciones.EEntregasRendir>();

        private string modulo = "", empresas = "", tipodocumentos = "";
        private int contadorcuentaapagarpendiente = 0, contadorcuentaapagaraprobados=0,contadorcajachicapendientes=0,
                    contadorcajachicaaprobados=0,opcion,contadordecuentasrendirpendientes=0, contadordecuentasrendiraprobados=0;
        DateTime fechainicios, fechafines;
        private bool funcionEjecutada = false;
        public string cod_empresa = "", cod_sede_empresa = "", movimiento = "", dsc_modulofiltro= "", cod_modulofiltro="";
        private System.Windows.Forms.Timer timer1;
        eParametrosGenerales objBloq = new eParametrosGenerales();
        //


        public frmDocumentosporAprobar()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            TileBarItem item = new TileBarItem();
            FiltroCuetasPagar control = new FiltroCuetasPagar();
            control.Dock = DockStyle.Fill;
            control.Inicializar();
            mostrardataCajachica(true, false, 0, false,false);
            Panel_Contenedor.Controls.Clear();
            Panel_Contenedor.Controls.Add(control);
            fch_desde.Text = Convert.ToString(control.dtFechaInicio.DateTime.ToShortDateString());
            fch_hasta.Text = Convert.ToString(control.dtFechaFin.DateTime.ToShortDateString());
            FiltroCajachica controlcajachica = new FiltroCajachica();
            //Tbar_Cuentapagar_ItemClick(Tbar_Cuentapagar, new DevExpress.XtraEditors.TileItemEventArgs());
            //Tbar_Cajachica_ItemClick(Tbar_Cuentapagar, new DevExpress.XtraEditors.TileItemEventArgs());
            //Tbar_CuentaRendir_item(Tbar_Cuentapagar, new DevExpress.XtraEditors.TileItemEventArgs());

            //busqueda_cuentaspagar(Convert.ToDateTime(control.dtFechaInicio.EditValue),
            //                      Convert.ToDateTime(control.dtFechaFin.EditValue),
            //                      control.lkpEmpresa.EditValue == null ? "" : control.lkpEmpresa.EditValue.ToString(),
            //                      control.chkcbTipoDocumento.EditValue == null ? "" : control.chkcbTipoDocumento.EditValue.ToString());
            modulo = "CuentaPagar";
        }
        int ctd_empresas = 0;
        //private void CargarFiltroTreeList()
        //{

        //    var ListEmp = Program.Sesion.EmpresaList;
        //    ctd_empresas = ListEmp.Count;

        //    var emp_sedeList = new List<eFltEmpresaSede>();
        //    foreach (var obj in ListEmp)
        //    {
        //        List<eEmpresa.eSedeEmpresa> ListSedes = unit.Clientes.ListarOpcionesMenu<eEmpresa.eSedeEmpresa>(6, obj.cod_empresa);
        //        foreach (eEmpresa.eSedeEmpresa objSede in ListSedes)
        //        {
        //            emp_sedeList.Add(new eFltEmpresaSede()
        //            {
        //                cod_empresa = obj.cod_empresa,
        //                dsc_empresa = obj.dsc_empresa
        //            });
        //        }
        //    }

        //    if (emp_sedeList != null && emp_sedeList.Count > 0)
        //    {
        //        var lst = emp_sedeList;
        //        var tree = new Tools.TreeListHelper(treeList_empresa);
        //        tree.TreeViewParaUnNodo<eFltEmpresaSede>(emp_sedeList,
        //              ColumnaCod_Padre: "cod_empresa",
        //              ColumnaDsc_Padre: "dsc_empresa"
        //            );
        //        refreshTreeView_empresa();

        //    }
        //}
        private void CargarFiltroTreeList_modulos()
        {

            List<EAprobaciones> ListModulo = unit.Aprobaciones.ListarTrabajadores<EAprobaciones>(1);

            var emp_moduloList = new List<eFltModulo>();

            foreach (var obj in ListModulo)
            {
                    emp_moduloList.Add(new eFltModulo()
                    {
                        cod_modulofiltro = obj.cod_modulofiltro,
                        dsc_modulofiltro = obj.dsc_modulofiltro
                    });

            }

            if (emp_moduloList != null && emp_moduloList.Count > 0)
            {
                var lst = emp_moduloList;
                var tree = new Tools.TreeListHelper(treeList_modulos);
                tree.TreeViewParaUnNodo<eFltModulo>(emp_moduloList,
                      ColumnaCod_Padre: "cod_modulofiltro",
                      ColumnaDsc_Padre: "dsc_modulofiltro"
                    );
                refreshTreeView_modulo();
                treeList_modulos.AfterCheckNode += treeList_modulos_AfterCheckNode;
            }
           
        }
      
        //private void refreshTreeView_empresa()
        //{
        //    treeList_empresa.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
        //    treeList_empresa.CollapseAll();
        //    treeList_empresa.Nodes[0].ExpandAll();
        //    treeList_empresa.Refresh();

            //}
        private void refreshTreeView_modulo()
        {
            treeList_modulos.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Radio;
            treeList_modulos.CollapseAll();
            treeList_modulos.Nodes[0].ExpandAll();
            treeList_modulos.Refresh();

        }
        FiltroCuetasPagar control = new FiltroCuetasPagar();
        private void Tbar_Cuentapagar_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            funcionEjecutada = true;
            bsDocPendientes.Clear();
            bsDocAprobados.Clear();
            TileBarItem item = new TileBarItem();
            control.Dock = DockStyle.Fill;
            unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");

            control.btnBusqueda.Click += (s, a) =>
            busqueda_cuentaspagar(Convert.ToDateTime(control.dtFechaInicio.EditValue),
                                  Convert.ToDateTime(control.dtFechaFin.EditValue),
                                  control.lkpEmpresa.EditValue == null ? "" : control.lkpEmpresa.EditValue.ToString(),
                                  control.chkcbTipoDocumento.EditValue == null ? "" : control.chkcbTipoDocumento.EditValue.ToString());
            control.Inicializar();
            busqueda_cuentaspagar(Convert.ToDateTime(control.dtFechaInicio.EditValue),
                              Convert.ToDateTime(control.dtFechaFin.EditValue),
                              control.lkpEmpresa.EditValue == null ? "" : control.lkpEmpresa.EditValue.ToString(),
                              control.chkcbTipoDocumento.EditValue == null ? "" : control.chkcbTipoDocumento.EditValue.ToString());
            control.lkpEmpresa.EditValue = empresas;control.chkcbTipoDocumento.EditValue = tipodocumentos;control.dtFechaInicio.EditValue = fechainicios;control.dtFechaFin.EditValue = fechafines;
            
            Panel_Contenedor.Controls.Clear();
            Panel_Contenedor.Controls.Add(control);
            modulo = "CuentaPagar";

            SplashScreenManager.CloseForm();
        }
  
        private void busqueda_cuentaspagar(DateTime fechainicio, DateTime fechafin,string empresa,string tipodocumento)
        {
            EAprobaciones obj1 = new EAprobaciones();
            obj1 = unit.Aprobaciones.Obtener_datos<EAprobaciones.Ecajachica>(27, cod_usuario: Program.Sesion.Usuario.cod_usuario,cod_empresa: empresa);
            if (obj1 == null) { HNG.MessageWarning("NO TIENE NINGUN PERMISO PARA ESTE MODULO", "SOLICITAR PERMISO"); return; }

            try
            {
                listapendientes = unit.Aprobaciones.FiltroFactura<EAprobaciones>(5, fechainicio, fechafin,empresa,tipodocumento,obj1.imp_minimo,obj1.imp_maximo);
                bsDocPendientes.DataSource = listapendientes;
                contadorcuentaapagarpendiente = listapendientes.Count();
                listaaprobados = unit.Aprobaciones.FiltroFactura<EAprobaciones>(7, fechainicio, fechafin, empresa, tipodocumento, obj1.imp_minimo, obj1.imp_maximo);
                bsDocAprobados.DataSource = listaaprobados;
                contadorcuentaapagaraprobados = listaaprobados.Count();
                empresas = empresa; tipodocumentos = tipodocumento;fechainicios = fechainicio;fechafines = fechafin;
                

            }
            catch (Exception ex)
            {  
                HNG.MessageError(ex.ToString(), "");
            }
            tabs();


        }
        private void busqueda_cajachica(string cod_caja, string empresa, string cod_sede_empresa)
        {
            EAprobaciones obj1 = new EAprobaciones();
            obj1 = unit.Aprobaciones.Obtener_datos<EAprobaciones.Ecajachica>(35, cod_usuario: Program.Sesion.Usuario.cod_usuario, cod_empresa: cod_empresa);
            if (obj1 == null) { HNG.MessageWarning("NO TIENE NINGUN PERMISO PARA ESTE MODULO", "SOLICITAR PERMISO"); return; }

            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");

                listarcajachica = unit.Aprobaciones.ListarCajaChica<EAprobaciones.Ecajachica>(18, "", "", cod_caja);
                bsDocPendientes.DataSource = listarcajachica;
                contadorcajachicapendientes = listarcajachica.Count();
                listarcajachicaaprobados = unit.Aprobaciones.ListarCajaChica<EAprobaciones.Ecajachica>(23, "", "", cod_caja);
                bsDocAprobados.DataSource = listarcajachicaaprobados;
                contadorcajachicaaprobados = listarcajachicaaprobados.Count();
                
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
            tabs();


        }
        private void busqueda_entregasrendir(DateTime fechainicio, DateTime fechafin, string empresa, string sedeempresa)
        {
            EAprobaciones obj1 = new EAprobaciones();
            obj1 = unit.Aprobaciones.Obtener_datos<EAprobaciones.Ecajachica>(31, cod_usuario: Program.Sesion.Usuario.cod_usuario, cod_empresa: empresa);
            if (obj1 == null) { HNG.MessageWarning("NO TIENE NINGUN PERMISO PARA ESTE MODULO", "SOLICITAR PERMISO"); return; }
            try
            {
                unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");

                listarentregasrendir = unit.Aprobaciones.ListarEntregaRendir<EAprobaciones.EEntregasRendir>(24, fechainicio, fechafin, empresa, sedeempresa,obj1.imp_minimo,obj1.imp_maximo);
                bsDocPendientes.DataSource = listarentregasrendir;
                contadordecuentasrendirpendientes = listarentregasrendir.Count();
               
                listarentregasrendiraprobados = unit.Aprobaciones.ListarEntregaRendir<EAprobaciones.EEntregasRendir>(26, fechainicio, fechafin, empresa, sedeempresa);
                bsDocAprobados.DataSource = listarentregasrendiraprobados;
                contadordecuentasrendiraprobados = listarentregasrendiraprobados.Count();

                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                HNG.MessageError(ex.ToString(), "");
            }
            tabs();


        }
        private void Tbar_Cajachica_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            xTabAprobados.SelectedTabPage.Select();
            int contador = 0;
            int contadorcaja = 0;
            funcionEjecutada = true;
            bsDocPendientes.Clear();
            bsDocAprobados.Clear();
            FiltroCajachica controlcajachica = new FiltroCajachica();
            TileBarItem item = new TileBarItem();
            mostrardataCajachica(false, true, 0, true,false);
            controlcajachica.Dock = DockStyle.Fill;
            controlcajachica.btnGuardarcaja.Click += (s, a) =>
            busqueda_cajachica(controlcajachica.lkpTipoCaja.EditValue == null ? "" : controlcajachica.lkpTipoCaja.EditValue.ToString(),controlcajachica.lkpEmpresa.EditValue == null ? "" : controlcajachica.lkpEmpresa.EditValue.ToString(), controlcajachica.lkpSedeEmpresa.EditValue == null ? "" : controlcajachica.lkpSedeEmpresa.EditValue.ToString());
            controlcajachica.Inicializar();
            busqueda_cajachica(controlcajachica.lkpTipoCaja.EditValue == null ? "" : controlcajachica.lkpTipoCaja.EditValue.ToString(), controlcajachica.lkpEmpresa.EditValue == null ? "" : controlcajachica.lkpEmpresa.EditValue.ToString(), controlcajachica.lkpSedeEmpresa.EditValue == null ? "" : controlcajachica.lkpSedeEmpresa.EditValue.ToString());
            cod_empresa = controlcajachica.lkpEmpresa.EditValue.ToString();
            cod_sede_empresa = controlcajachica.lkpSedeEmpresa.EditValue.ToString();
            Panel_Contenedor.Controls.Clear();
            Panel_Contenedor.Controls.Add(controlcajachica);
            modulo = "CajaChica";


        }

        private void Tbar_CuentaRendir_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            bsDocPendientes.Clear();
            bsDocAprobados.Clear();
            FiltroEntregasaRendir controlentrehasrendir = new FiltroEntregasaRendir();
            mostrardataCajachica(false, false, 0, true, true);
            controlentrehasrendir.Dock = DockStyle.Fill;
            controlentrehasrendir.btnBuscar.Click += (s, a) =>
            busqueda_entregasrendir(Convert.ToDateTime(controlentrehasrendir.dtFechaInicio.EditValue), Convert.ToDateTime(controlentrehasrendir.dtFechaFin.EditValue) ,
                                    controlentrehasrendir.lkpEmpresa.EditValue == null ? "" : controlentrehasrendir.lkpEmpresa.EditValue.ToString(),
                                    controlentrehasrendir.lkpSedeEmpresa.EditValue == null ? "" : controlentrehasrendir.lkpSedeEmpresa.EditValue.ToString());
            controlentrehasrendir.Inicializar();
            busqueda_entregasrendir(Convert.ToDateTime(controlentrehasrendir.dtFechaInicio.EditValue), Convert.ToDateTime(controlentrehasrendir.dtFechaFin.EditValue),
                                              controlentrehasrendir.lkpEmpresa.EditValue == null ? "" : controlentrehasrendir.lkpEmpresa.EditValue.ToString(),
                                              controlentrehasrendir.lkpSedeEmpresa.EditValue == null ? "" : controlentrehasrendir.lkpSedeEmpresa.EditValue.ToString());
            Panel_Contenedor.Controls.Clear();
            Panel_Contenedor.Controls.Add(controlentrehasrendir);
            funcionEjecutada = true;
            modulo = "EntregaRendir";
        }

        private void gvCuentaPagar_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) { }
        }

        private void gvCuentaPagar_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void xTabAprobados_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabs();
        }

       
        private void tabs()
        {
            int contador = 0;
            int contadorcaja = 0;
            int contadorrendir = 0;
            int opcion = xTabAprobados.SelectedTabPageIndex;
            
            switch (opcion)
            {
                case 0:
                    contador = contadorcuentaapagarpendiente;
                    contadorcaja = contadorcajachicapendientes;
                    contadorrendir = contadordecuentasrendirpendientes;
                    //Tbar_Cuentapagar.Elements[2].Text = contador.ToString();
                    //Tbar_Cajachica.Elements[2].Text = contadorcaja.ToString();
                    //Tbar_CuentaRendir.Elements[2].Text = contadorrendir.ToString();
                    btnSeleccionMultiple.Enabled = true;
                    btnAprobar.Enabled = true;
                    if(modulo== "CuentaPagar") { mostrardataCajachica(true, false, opcion = 0,false,false);}
                    else if(modulo == "CajaChica") { mostrardataCajachica(false, true, opcion = 0, true,false); }
                    else if (modulo == "EntregaRendir") { mostrardataCajachica(false, false, opcion = 0, true,true); }
                    break;
                case 1:
                    contador = contadorcuentaapagaraprobados;
                    contadorcaja = contadorcajachicaaprobados;
                    contadorrendir = contadordecuentasrendiraprobados;
                    //Tbar_Cuentapagar.Elements[2].Text = contador.ToString();
                    //Tbar_Cajachica.Elements[2].Text = contadorcaja.ToString();
                    //Tbar_CuentaRendir.Elements[2].Text = contadorrendir.ToString();
                    btnSeleccionMultiple.Enabled = false;
                    btnAprobar.Enabled = false;
                    if (modulo == "CuentaPagar") { mostrardataCajachica(true, false, opcion = 1, false, false); gvDocAprobados.Columns[18].Visible = true; }
                    else if (modulo == "CajaChica") { mostrardataCajachica(false, true, opcion = 1, true, false); }
                    else if (modulo == "EntregaRendir") { mostrardataCajachica(false, false, opcion = 1, true, true); }
                    break;
            }

           
        }

        private void gvDocAprobados_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void treeList_modulos_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (e.Node != null && e.Node.CheckState == CheckState.Checked)
            {
                // Obtener el valor del radio seleccionado
                var valorSeleccionado = e.Node["Descripcion"];
                var codigoSeleccionado = e.Node["Codigo"];
                if (valorSeleccionado != null)
                {
                    // Actualizar el campo de texto con el valor seleccionado
                    lblModulo.Text = valorSeleccionado.ToString();
                }

                int codigo = Convert.ToInt32(codigoSeleccionado);
                switch(codigo)
                {
                    case 1:

                        funcionEjecutada = true;
                        bsDocPendientes.Clear();
                        bsDocAprobados.Clear();
                        TileBarItem item = new TileBarItem();
                        control.Dock = DockStyle.Fill;
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo documentos", "Cargando...");

                        control.btnBusqueda.Click += (s, a) =>
                        busqueda_cuentaspagar(Convert.ToDateTime(control.dtFechaInicio.EditValue),
                                              Convert.ToDateTime(control.dtFechaFin.EditValue),
                                              control.lkpEmpresa.EditValue == null ? "" : control.lkpEmpresa.EditValue.ToString(),
                                              control.chkcbTipoDocumento.EditValue == null ? "" : control.chkcbTipoDocumento.EditValue.ToString());
                        control.Inicializar();
                        
                        control.lkpEmpresa.EditValue = empresas; control.chkcbTipoDocumento.EditValue = tipodocumentos; control.dtFechaInicio.EditValue = fechainicios; control.dtFechaFin.EditValue = fechafines;

                        Panel_Contenedor.Controls.Clear();
                        Panel_Contenedor.Controls.Add(control);
                        modulo = "CuentaPagar";

                        SplashScreenManager.CloseForm();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
            }
        }

        private void btnAprobacionecxp_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantenimientoAprobacionesCP"] != null)
            {
                Application.OpenForms["frmMantenimientoAprobacionesCP"].Activate();
            }
            else
            {
                frmMantenimientoAprobacionesCP frm = new frmMantenimientoAprobacionesCP();
                frm.ShowDialog();
                //if (frm.ActualizarListado == "SI") frmListadoTrabajador_KeyDown(null, new KeyEventArgs(Keys.F5));
            }
        }

        private void gvDocPendientes_RowCellClick_1(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                EAprobaciones obj = new EAprobaciones();
                if (e.Clicks == 1 && e.Column.FieldName == "documento")
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    obj = gvDocPendientes.GetFocusedRow() as EAprobaciones;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.RUC = obj.dsc_ruc;
                    frmModif.tipo_documento = obj.tipo_documento;
                    frmModif.serie_documento = obj.serie_documento;
                    frmModif.numero_documento = obj.numero_documento;
                    frmModif.cod_proveedor = obj.cod_proveedor;

                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }
                EAprobaciones.Ecajachica obj2 = new EAprobaciones.Ecajachica();
                if (e.Clicks == 1 && e.Column.FieldName == "cod_movimiento")
                {
                    obj2 = gvDocPendientes.GetFocusedRow() as EAprobaciones.Ecajachica;
                    if (obj2.cod_movimiento == "APERTURA") { return; }
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    if (obj2 == null) { return; }

                    frmDetalleCajaChicaAprobaciones frmModif = new frmDetalleCajaChicaAprobaciones();
                    frmModif.ObtenerLista_CajaRendida(obj2.cod_caja, obj2.cod_movimiento);
                    frmModif.cod_empresa = cod_empresa;
                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDocumentosporAprobar_Load(object sender, EventArgs e)
        {
            // CargarFiltroTreeList();
            CargarFiltroTreeList_modulos();
        }

        private void gcDocPendientes_Click(object sender, EventArgs e)
        {

        }

        private void gvDocAprobados_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                EAprobaciones obj = new EAprobaciones();
                if (e.Clicks == 1 && e.Column.FieldName == "documento")
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    obj = gvDocAprobados.GetFocusedRow() as EAprobaciones;
                    if (obj == null) { return; }

                    frmMantFacturaProveedor frmModif = new frmMantFacturaProveedor();
                    frmModif.MiAccion = Factura.Vista;
                    frmModif.RUC = obj.dsc_ruc;
                    frmModif.tipo_documento = obj.tipo_documento;
                    frmModif.serie_documento = obj.serie_documento;
                    frmModif.numero_documento = obj.numero_documento;
                    frmModif.cod_proveedor = obj.cod_proveedor;

                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }
                EAprobaciones.Ecajachica obj2 = new EAprobaciones.Ecajachica();
                if (e.Clicks == 1 && e.Column.FieldName == "cod_movimiento")
                {
                    if (obj2.cod_movimiento == "APERTURA") { return; }
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    obj2 = gvDocAprobados.GetFocusedRow() as EAprobaciones.Ecajachica;
                    if (obj2 == null) { return; }

                    frmDetalleCajaChicaAprobaciones frmModif = new frmDetalleCajaChicaAprobaciones();
                    frmModif.ObtenerLista_CajaRendida(obj2.cod_caja,obj2.cod_movimiento);
                    SplashScreenManager.CloseForm();
                    frmModif.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tileBar1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void tileBar1_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            if (!funcionEjecutada)
            {
                // Ejecutar la función deseada aquí
                funcionEjecutada = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Ejecutar la función deseada aquí
            funcionEjecutada = true;
        }

        private void btnSeleccionMultiple_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvDocPendientes.OptionsSelection.MultiSelect == true)
            {
                gvDocPendientes.OptionsSelection.MultiSelect = false;
                return;
            }
            if (gvDocPendientes.OptionsSelection.MultiSelect == false)
            {
                gvDocPendientes.OptionsSelection.MultiSelect = true;
                return;
            }
        }

        private void btnAprobar_ItemClick(object sender, ItemClickEventArgs e)
        {
            string empresa = "", tipodoc = "",seriedoc="", movimientos = "",numerodoc="", proveedor="", caja="",result="",sedempresa="",entregarendir="";
            int contador = 0;
            int opcion = xTabAprobados.SelectedTabPageIndex;
            DateTime fechahoy = DateTime.Today;

            switch (opcion)
            {
                case 0:
                    if (modulo == "CuentaPagar")
                    {
                        //foreach (int nRow in gvDocPendientes.GetSelectedRows())
                        //{

                        //    EAprobaciones obj = gvDocPendientes.GetRow(nRow) as EAprobaciones;
                        //    empresa = obj.cod_empresa;
                        //    tipodoc = obj.tipo_documento + "," + tipodoc;
                        //    seriedoc = obj.serie_documento + "," + seriedoc;
                        //    numerodoc = obj.numero_documento + "," + numerodoc;
                        //    proveedor = obj.cod_proveedor + "," + proveedor;
                        //}
                        //result = unit.Aprobaciones.ActualizarEstadoRegistrador(8, fch_aprobado_reg:fechahoy, cod_empresa:empresa, tipo_documento:tipodoc, serie_documento: seriedoc, numero_documento: numerodoc, cod_usuario_aprobado_reg:Program.Sesion.Usuario.cod_usuario);
                        //HNG.MessageSuccess("Documentos Aprobados", "Aprobación Exitosa");
                        //Tbar_Cuentapagar_ItemClick(Tbar_Cuentapagar, new DevExpress.XtraEditors.TileItemEventArgs());
                        gvDocPendientes.RefreshData();
                        if (gvDocPendientes.SelectedRowsCount == 0) { MessageBox.Show("Debe seleccionar un documento.", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                        unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Aprobando documentos", "Cargando...");
                        List<EAprobaciones> lista = new List<EAprobaciones>();
                        foreach (int nRow in gvDocPendientes.GetSelectedRows())
                        {
                            EAprobaciones obj = gvDocPendientes.GetRow(nRow) as EAprobaciones;
                            lista.Add(obj);
                            objBloq.valor_1 = obj.cod_empresa;
                            objBloq = unit.Factura.Obtener_BloqueoCECOxEmpresa<eParametrosGenerales>(64, objBloq);

                            if (obj.flg_inventario == "NO" && obj.flg_activo_fijo == "NO")
                            {
                                List<eFacturaProveedor.eFacturaProveedor_Distribucion> mylistLineasDetFactura = unit.Factura.Obtener_LineasDetalleFactura<eFacturaProveedor.eFacturaProveedor_Distribucion>(4, obj.tipo_documento, obj.serie_documento, obj.numero_documento, obj.cod_proveedor,
                                                                                                                //objBloq.valor_2 == "NO" ? "NO" : objBloq.valor_2 == "SI" && Convert.ToDateTime(obj.fch_documento).Year < 2023 ? "NO" : "SI");
                                                                                                                objBloq.valor_2 == "NO" && Convert.ToDateTime(obj.fch_documento).Year < 2023 ? "NO" : "SI");
                                if (mylistLineasDetFactura.Count == 0)
                                {
                                    SplashScreenManager.CloseForm();
                                    MessageBox.Show("Debe asignar un centro de costo para APROBAR el documento.", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //Tbar_Cuentapagar_ItemClick(Tbar_Cuentapagar, new DevExpress.XtraEditors.TileItemEventArgs()); return;
                                    return;
                                }
                            }

                            empresa = obj.cod_empresa;
                            tipodoc = obj.tipo_documento;
                            seriedoc = obj.serie_documento;
                            numerodoc = Convert.ToString(obj.numero_documento);
                            proveedor = obj.cod_proveedor;

                            result = unit.Aprobaciones.ActualizarEstadoRegistrador(8, fch_aprobado_reg:fechahoy, cod_empresa:empresa, tipo_documento:tipodoc, serie_documento: seriedoc, numero_documento: numerodoc, cod_usuario_aprobado_reg:Program.Sesion.Usuario.cod_usuario);
                            if (result != "OK") { MessageBox.Show("Error al aprobar documento", "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error); SplashScreenManager.CloseForm(); return; }

                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objProg = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                            if (obj.imp_saldo == 0) continue;
                            objProg.tipo_documento = obj.tipo_documento; objProg.serie_documento = obj.serie_documento; objProg.numero_documento = obj.numero_documento; objProg.cod_proveedor = obj.cod_proveedor;
                            objProg.num_linea = 0; objProg.fch_pago = obj.fch_pago_programado; objProg.dsc_observacion = null;
                            objProg.cod_estado = obj.tipo_documento == "TC006" ? "EJE" : obj.cod_estado_pago == "PAG" ? "EJE" : "PRO"; objProg.cod_pagar_a = "PROV";
                            objProg.fch_ejecucion = obj.cod_estado_pago == "PAG" ? obj.fch_pago_ejecutado : new DateTime(); objProg.cod_usuario_ejecucion = null; objProg.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                            objProg.cod_tipo_prog = obj.tipo_documento == "TC006" ? "NOTACRED" : "REGULAR"; objProg.cod_formapago = obj.tipo_documento == "TC006" ? "NOTACRED" : "TRANF";
                            objProg.imp_pago = obj.imp_saldo; objProg.cod_empresa = obj.cod_empresa;

                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                            eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(objProg);
                            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                       // lista = lista.FindAll(x => x.cod_formapago != "ONLINE" && x.CantCuentas == "NO");
                        if (lista.Count > 0)
                        {
                             MessageBox.Show("Hay proveedores que no tienen CTAS BANCARIAS PARA PAGO CON TRANSFERENCIA." + Environment.NewLine + "Debe realizar los siguientes pasos:"
                            + Environment.NewLine + "1. Ingresar a la ventana del proveedor." + Environment.NewLine + "2. Ingresar la cuenta bancaria correspondiente." + Environment.NewLine + "3. Asginar una cuenta por defecto dando click en el botón POR DEFECTO."
                        , "Aprobar documentos", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        }

                        SplashScreenManager.CloseForm();
                        HNG.MessageSuccess("Documentos Aprobados", "Aprobación Exitosa");
                        //Tbar_Cuentapagar_ItemClick(Tbar_Cuentapagar, new DevExpress.XtraEditors.TileItemEventArgs()); return;

                    }

                    if (modulo == "CajaChica")
                    {
                        EAprobaciones.Ecajachica s = new EAprobaciones.Ecajachica();
                        s = gvDocPendientes.GetFocusedRow() as EAprobaciones.Ecajachica;
                        listarcajachicadetalles = unit.Aprobaciones.ListarCajaChica<EAprobaciones.Ecajachica>(32, "", "", s.cod_caja);
                        //listarcajachicadetalles = unit.Aprobaciones.ListarCajaChica<EAprobaciones.Ecajachica>(44, "", "", s.cod_caja);
                        
                        foreach (var item in listarcajachicadetalles)
                        {
                            movimiento = item.cod_movimiento + "," + movimiento;
                        }

                        foreach (int nRow in gvDocPendientes.GetSelectedRows())
                        {
                            EAprobaciones.Ecajachica obj = gvDocPendientes.GetRow(nRow) as EAprobaciones.Ecajachica;
                            caja = obj.cod_caja + "," + caja;
                            movimientos = obj.cod_movimiento + "," + movimiento;
                        }

                        s.cod_aprobacion = 0;
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objPR = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        objPR.tipo_documento = s.cod_caja; objPR.serie_documento = s.cod_movimiento; objPR.numero_documento = s.num_Anho; objPR.cod_proveedor = s.cod_proveedor;
                        objPR.num_linea = 0; objPR.fch_pago = fechahoy; objPR.dsc_observacion = null; objPR.cod_estado = "PRO"; objPR.cod_pagar_a = "PROV";
                        objPR.fch_ejecucion = new DateTime(); objPR.cod_usuario_ejecucion = null; objPR.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                        objPR.cod_tipo_prog = "CAJACHICA"; objPR.cod_moneda_prog = "SOL"; objPR.cod_formapago = "TRANF"; objPR.imp_pago = s.imp_entregado;
                        objPR.cod_empresa = s.cod_empresa; objPR.cod_sede_empresa = s.cod_sede_empresa;
                        eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                        eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(objPR);
                        if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        result = unit.Aprobaciones.ActualizarEstadoRegistrador(22, fch_aprobado_reg:fechahoy, cod_caja_multiple: caja, cod_movimiento_multiple: movimientos,cod_usuario_aprobado_reg: Program.Sesion.Usuario.cod_usuario);
                        string result2 = unit.Aprobaciones.ActualizarEstadoRegistrador(44, fch_aprobado_reg: fechahoy, cod_caja_multiple: caja, cod_movimiento_multiple: movimientos, cod_usuario_aprobado_reg: Program.Sesion.Usuario.cod_usuario);


                        gvDocPendientes.RefreshData();
                        
                        HNG.MessageSuccess("Documentos Aprobados", "Aprobación Exitosa");
                        busqueda_cajachica(caja, empresa, cod_sede_empresa);

                    }
                    if (modulo == "EntregaRendir")
                    {
                        foreach (int nRow in gvDocPendientes.GetSelectedRows())
                        {

                            EAprobaciones.EEntregasRendir obj = gvDocPendientes.GetRow(nRow) as EAprobaciones.EEntregasRendir;
                            empresa = obj.cod_empresa;
                            sedempresa = obj.cod_sede_empresa + "," + sedempresa;
                            entregarendir = obj.cod_entregarendir + "," + entregarendir;

                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos objPR = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                            objPR.tipo_documento = obj.cod_entregarendir; objPR.serie_documento = obj.cod_tipo; objPR.numero_documento = obj.num_Anho; objPR.cod_proveedor = obj.cod_proveedor;
                            objPR.num_linea = 0; objPR.fch_pago = fechahoy; objPR.dsc_observacion = null; objPR.cod_estado = "PRO"; objPR.cod_pagar_a = "PROV";
                            objPR.fch_ejecucion = new DateTime(); objPR.cod_usuario_ejecucion = null; objPR.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                            objPR.cod_tipo_prog = "ENTREGARENDIR"; objPR.cod_moneda_prog = "SOL"; objPR.cod_formapago = "TRANF"; objPR.imp_pago = obj.imp_entregado;
                            objPR.cod_empresa = obj.cod_empresa; objPR.cod_sede_empresa = obj.cod_sede_empresa;
                            eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
                            eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(objPR);
                            if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        result = unit.Aprobaciones.ActualizarAprobacionEntregaRendir(25, fch_aprobado_reg: fechahoy,cod_entregarendir_multiple: entregarendir,cod_empresamultiple:empresa,cod_sede_empresa:sedempresa, cod_usuario_aprobado_reg: Program.Sesion.Usuario.cod_usuario);
                        

                        gvDocPendientes.RefreshData();

                        HNG.MessageSuccess("Documentos Aprobados", "Aprobación Exitosa");
                        //Tbar_CuentaRendir_ItemClick(Tbar_CuentaRendir, new DevExpress.XtraEditors.TileItemEventArgs());

                    }
                    break;
                case 1:
                    

                    break;
            }

        }

        private void btnRegistroAprobadores_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmRegistroTrabajadoresAprobaciones"] != null)
            {
                Application.OpenForms["frmRegistroTrabajadoresAprobaciones"].Activate();
            }
            else
            {
                frmRegistroTrabajadoresAprobaciones frm = new frmRegistroTrabajadoresAprobaciones();
                frm.ShowDialog();
                //if (frm.ActualizarListado == "SI") frmListadoTrabajador_KeyDown(null, new KeyEventArgs(Keys.F5));
            }
        }

        private void btnMantenimientoAprobaciones_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmMantenimientoAprobaciones"] != null)
            {
                Application.OpenForms["frmMantenimientoAprobaciones"].Activate();
            }
            else
            {
                frmMantenimientoAprobaciones frm = new frmMantenimientoAprobaciones();

                frm.ShowDialog();
                //if (frm.ActualizarListado == "SI") frmListadoTrabajador_KeyDown(null, new KeyEventArgs(Keys.F5));
            }
        }

        private void btnHistorialAprobaciones_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Application.OpenForms["frmHistorialAprobaciones"] != null)
            {
                Application.OpenForms["frmHistorialAprobaciones"].Activate();
            }
            else
            {
                frmHistorialAprobaciones frm = new frmHistorialAprobaciones();
                frm.ShowDialog();
                //if (frm.ActualizarListado == "SI") frmListadoTrabajador_KeyDown(null, new KeyEventArgs(Keys.F5));
            }
        }

        private void mostrardataCajachica(bool cuentapagar,bool cajachica,int opcion,bool entregarendir,bool entregarendirsolo)
        {
            EAprobaciones.Ecajachica columnascaja = new EAprobaciones.Ecajachica();

            switch (opcion)
            {
                case 0:

                    gvDocPendientes.Columns["dsc_tipo_comprobante"].Visible = cuentapagar;
                    gvDocPendientes.Columns["documento"].Visible = cuentapagar;
                    gvDocPendientes.Columns["dsc_razon_social"].Visible = cuentapagar;
                    gvDocPendientes.Columns["fch_documento"].Visible = cuentapagar;
                    gvDocPendientes.Columns["fch_registro"].Visible = cuentapagar;
                    gvDocPendientes.Columns["cod_moneda"].Visible = cuentapagar;
                    gvDocPendientes.Columns["imp_total"].Visible = cuentapagar;
                    gvDocPendientes.Columns["imp_saldo"].Visible = cuentapagar;
                    gvDocPendientes.Columns["fch_vencimiento"].Visible = cuentapagar;
                    gvDocPendientes.Columns["cod_movimiento"].Visible = cajachica;
                    gvDocPendientes.Columns["fch_creacion"].Visible = entregarendir;
                    gvDocPendientes.Columns["dsc_tipo"].Visible = cajachica;
                    gvDocPendientes.Columns["imp_entregado"].Visible = entregarendir;
                    gvDocPendientes.Columns["dsc_entregado"].Visible = entregarendir;
                    gvDocPendientes.Columns["dsc_observacion"].Visible = cajachica;
                    gvDocPendientes.Columns["ctd_comprobantes"].Visible = entregarendir;
                    gvDocPendientes.Columns["imp_entregado"].Visible = entregarendir;
                    gvDocPendientes.Columns["dsc_entregado"].Visible = entregarendirsolo;
                    gvDocPendientes.Columns["fch_pago_programado"].Visible = cuentapagar;
                    gvDocPendientes.Columns["imp_entregado"].Visible = entregarendirsolo;
                    gvDocPendientes.Columns["cod_entregarendir"].Visible = entregarendirsolo;

                    if (modulo != "EntregaRendir")
                    {
                        gvDocPendientes.Columns["dsc_tipo_comprobante"].VisibleIndex = !cuentapagar ? -1 : 0;
                        gvDocPendientes.Columns["documento"].VisibleIndex = !cuentapagar ? -1 : 1;
                        gvDocPendientes.Columns["dsc_razon_social"].VisibleIndex = !cuentapagar ? -1 : 2;
                        gvDocPendientes.Columns["fch_documento"].VisibleIndex = !cuentapagar ? -1 : 3;
                        gvDocPendientes.Columns["fch_registro"].VisibleIndex = !cuentapagar ? -1 : 4;
                        gvDocPendientes.Columns["cod_moneda"].VisibleIndex = !cuentapagar ? -1 : 5;
                        gvDocPendientes.Columns["fch_vencimiento"].VisibleIndex = !cuentapagar ? -1 : 6;
                        gvDocPendientes.Columns["cod_movimiento"].VisibleIndex = !cajachica ? -1 : 7;
                        gvDocPendientes.Columns["dsc_entregado"].VisibleIndex = !cajachica ? -1 : 8;
                        gvDocPendientes.Columns["dsc_tipo"].VisibleIndex = !cajachica ? -1 : 9;
                        gvDocPendientes.Columns["dsc_observacion"].VisibleIndex = !cajachica ? -1 : 10;
                        gvDocPendientes.Columns["ctd_comprobantes"].VisibleIndex = !entregarendir ? -1 : 11;
                        gvDocPendientes.Columns["fch_creacion"].VisibleIndex = !entregarendir ? -1 : 12;
                        gvDocPendientes.Columns["imp_entregado"].VisibleIndex = !entregarendir ? -1 : 13;
                        gvDocPendientes.Columns["fch_pago_programado"].VisibleIndex = !cuentapagar ? -1 : 14; 
                        gvDocPendientes.Columns["imp_total"].VisibleIndex = !cuentapagar ? -1 : 15;
                        gvDocPendientes.Columns["imp_saldo"].VisibleIndex = !cuentapagar ? -1 : 16;
                        gvDocPendientes.Columns["cod_entregarendir"].VisibleIndex= !entregarendirsolo? -1 :17;
                    }
                    else
                    {
                        gvDocPendientes.Columns["cod_entregarendir"].VisibleIndex = 0;
                        gvDocPendientes.Columns["dsc_entregado"].VisibleIndex = 1;
                        gvDocPendientes.Columns["fch_creacion"].VisibleIndex = 2;
                        gvDocPendientes.Columns["dsc_observacion"].VisibleIndex = 3;
                        gvDocPendientes.Columns["ctd_comprobantes"].VisibleIndex = 4;
                        gvDocPendientes.Columns["imp_entregado"].VisibleIndex = 5;
                        //gvDocPendientes.Columns["imp_monto"].VisibleIndex = 6;

                    }
                    break;
                case 1:

                    gvDocAprobados.Columns["dsc_tipo_comprobante"].Visible = cuentapagar;
                    gvDocAprobados.Columns["documento"].Visible = cuentapagar;
                    gvDocAprobados.Columns["dsc_razon_social"].Visible = cuentapagar;
                    gvDocAprobados.Columns["fch_documento"].Visible = cuentapagar;
                    gvDocAprobados.Columns["fch_registro"].Visible = cuentapagar;
                    gvDocAprobados.Columns["cod_moneda"].Visible = cuentapagar;
                    gvDocAprobados.Columns["imp_total"].Visible = cuentapagar;
                    gvDocAprobados.Columns["imp_saldo"].Visible = cuentapagar;
                    gvDocAprobados.Columns["fch_vencimiento"].Visible = cuentapagar;
                    gvDocAprobados.Columns["cod_movimiento"].Visible = cajachica;
                    gvDocAprobados.Columns["fch_creacion"].Visible = entregarendir;
                    gvDocAprobados.Columns["dsc_tipo"].Visible = cajachica;
                    gvDocAprobados.Columns["dsc_observacion"].Visible = cajachica;
                    gvDocAprobados.Columns["ctd_comprobantes"].Visible = entregarendir;
                    gvDocAprobados.Columns["imp_monto"].Visible = entregarendir;
                    gvDocAprobados.Columns["fch_aprobado_reg"].Visible = cajachica;
                    gvDocAprobados.Columns["dsc_entregado"].Visible = entregarendirsolo;
                    gvDocAprobados.Columns["cod_entregarendir"].Visible = entregarendirsolo;
                    gvDocAprobados.Columns["fch_pago_programado"].Visible = cuentapagar;
                    gvDocAprobados.Columns["imp_entregado"].Visible = entregarendirsolo;

                    if (modulo != "EntregaRendir")
                    {
                        gvDocAprobados.Columns["dsc_tipo_comprobante"].VisibleIndex = !cuentapagar ? -1 : 0;
                        gvDocAprobados.Columns["documento"].VisibleIndex = !cuentapagar ? -1 : 1;
                        gvDocAprobados.Columns["dsc_razon_social"].VisibleIndex = !cuentapagar ? -1 : 2;
                        gvDocAprobados.Columns["fch_documento"].VisibleIndex = !cuentapagar ? -1 : 3;
                        gvDocAprobados.Columns["fch_registro"].VisibleIndex = !cuentapagar ? -1 : 4;
                        gvDocAprobados.Columns["fch_vencimiento"].VisibleIndex = !cuentapagar ? -1 : 5;
                        gvDocAprobados.Columns["fch_pago_programado"].VisibleIndex = !cuentapagar ? -1 : 6;
                        gvDocAprobados.Columns["cod_moneda"].VisibleIndex = !cuentapagar ? -1 : 7;
                        gvDocAprobados.Columns["cod_movimiento"].VisibleIndex = !cajachica ? -1 : 8;
                        gvDocAprobados.Columns["dsc_entregado"].VisibleIndex = !entregarendir ? -1 : 9;
                        gvDocAprobados.Columns["dsc_tipo"].VisibleIndex = !cajachica ? -1 : 10;
                        gvDocAprobados.Columns["dsc_observacion"].VisibleIndex = !cajachica ? -1 : 11;
                        gvDocAprobados.Columns["ctd_comprobantes"].VisibleIndex = !entregarendir ? -1 : 12;
                        gvDocAprobados.Columns["fch_creacion"].VisibleIndex = !entregarendir ? -1 : 13;
                        gvDocAprobados.Columns["imp_monto"].VisibleIndex = !cajachica ? -1 : 14;
                        gvDocAprobados.Columns["imp_total"].VisibleIndex = !cuentapagar ? -1 : 15;
                        gvDocAprobados.Columns["imp_saldo"].VisibleIndex = !cuentapagar ? -1 : 16;
                        gvDocAprobados.Columns["fch_aprobado_reg"].VisibleIndex = !cajachica ? -1 : 17;
                        gvDocAprobados.Columns["fch_aprobado_reg"].VisibleIndex = !cuentapagar ? -1 : 18;
                        gvDocAprobados.Columns["fch_aprobado_reg"].VisibleIndex = !entregarendirsolo ? -1 : 19;
                    }
                    else
                    {
                        gvDocAprobados.Columns["cod_entregarendir"].VisibleIndex = 0;
                        gvDocAprobados.Columns["dsc_entregado"].VisibleIndex = 1;
                        gvDocAprobados.Columns["fch_creacion"].VisibleIndex = 2;
                        gvDocAprobados.Columns["dsc_observacion"].VisibleIndex = 3;
                        gvDocAprobados.Columns["ctd_comprobantes"].VisibleIndex = 4;
                        gvDocAprobados.Columns["imp_entregado"].VisibleIndex = 5; //imp_monto
                        gvDocAprobados.Columns["imp_monto"].VisibleIndex = 6;
                        gvDocAprobados.Columns["fch_aprobado_reg"].VisibleIndex = 7;
                    }
                    break;
            }
            
           
        }

       
    }
}