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
using UI_BackOffice.Formularios.Cuentas_Pagar;
using UI_BackOffice.Formularios.Shared;
using Microsoft.Identity.Client;
using DevExpress.XtraSplashScreen;
using System.IO;
using System.Security;
using System.Net.Http.Headers;
using System.Configuration;

namespace UI_BackOffice.Formularios.Logistica
{
    internal enum IngresoAlmacen
    {
        Nuevo = 1,
        Editar = 2,
        Vista = 3
    }
    public partial class frmRegistrarEntradaAlmacen : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        internal IngresoAlmacen MiAccion = IngresoAlmacen.Nuevo;
        List<eAlmacen.eProductos_Almacen> listaProd = new List<eAlmacen.eProductos_Almacen>();
        string fmt_nro_doc = "";
        Int16 num_ctd_serie, num_ctd_doc;
        public decimal numero_documento = 0;
        public string cod_empresa = "", cod_sede_empresa = "", cod_almacen = "", cod_entrada = "", cod_orden_compra_servicio = "", flg_solicitud = "", dsc_anho = "0";
        public string tipo_documento = "", serie_documento = "", TD_sunat = "";
        public bool ActualizarListado = false;

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "", varNombreArchivoSinExtension = "";

        public frmRegistrarEntradaAlmacen()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmRegistrarEntrada_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void Inicializar()
        {
            CargarCombosGridLookup("TipoComprobante", glkpTipoDocumento, "cod_tipo_comprobante", "dsc_tipo_comprobante", "", valorDefecto: false);
            unit.Logistica.CargaCombosLookUp("TipoMovimiento", lkpTipoMovimiento, "cod_tipo_movimiento", "dsc_tipo_movimiento", "", valorDefecto: true, dsc_variable: "ENTRADA", cod_empresa: cod_empresa);
            unit.Logistica.CargaCombosLookUp("Almacen", lkpAlmacen, "cod_almacen", "dsc_almacen", "", valorDefecto: true, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa);

            switch (MiAccion)
            {
                case IngresoAlmacen.Nuevo:
                    dtFechaDocumento.EditValue = DateTime.Today;
                    dtFechaTipoCambio.EditValue = DateTime.Today;
                    lkpAlmacen.EditValue = cod_almacen; lkpTipoMovimiento.EditValue = "003";
                    if (cod_orden_compra_servicio != "")
                    {
                        eOrdenCompra_Servicio eOrden = unit.OrdenCompra_Servicio.Cargar_OrdenCompra_Servicio<eOrdenCompra_Servicio>(2, cod_empresa, cod_sede_empresa, cod_orden_compra_servicio, "C", Convert.ToInt32(dsc_anho));
                        txtNroOC.Text = cod_orden_compra_servicio;
                        txtProveedorOC.Tag = eOrden.cod_proveedor;
                        txtProveedorOC.Text = eOrden.dsc_proveedor;
                        dtFechaOC.EditValue = eOrden.fch_emision;
                        dtFechaTipoCambio.EditValue = eOrden.fch_emision;
                        listaProd = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(17, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa, cod_orden_compra_servicio: cod_orden_compra_servicio);
                        bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
                    }

                    break;
                case IngresoAlmacen.Editar:
                    ObtenerDatos_EntradaAlmacen();
                    BloqueoControles(false, true, false);
                    if (glkpTipoDocumento.EditValue == null) { picBuscarDocumentos.Enabled = true; btnGuardar.Enabled = true; }
                    break;
                case IngresoAlmacen.Vista:
                    ObtenerDatos_EntradaAlmacen();
                    BloqueoControles(false, true, false);
                    //gvListadoProductos.Columns["num_cantidad_stock"].Visible = false;
                    //gvListadoProductos.Columns["num_cantidad_stock_nuevo"].Visible = false;
                    //btnAdjuntarArchivo.Enabled = true;
                    //btnVerPDF.Enabled = true;
                    break;
            }
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_condicion = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = unit.Factura.ObtenerListadoGridLookup(nCombo, cod_condicion);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        {
            btnGuardar.Enabled = Enabled;
            txtCodigo.ReadOnly = ReadOnly;
            lkpAlmacen.ReadOnly = ReadOnly;
            lkpTipoMovimiento.ReadOnly = ReadOnly;
            dtFechaDocumento.ReadOnly = ReadOnly;
            txtGlosa.ReadOnly = ReadOnly;
            txtNroOC.ReadOnly = ReadOnly;
            txtProveedorOC.ReadOnly = ReadOnly;
            picBuscarProveedor.Enabled = Enabled;
            dtFechaOC.ReadOnly = ReadOnly;
            dtFechaTipoCambio.ReadOnly = ReadOnly;
            txtTipoCambio.ReadOnly = ReadOnly;
            picBuscarDocumentos.Enabled = Enabled;
            glkpTipoDocumento.ReadOnly = ReadOnly;
            txtSerieDocumento.ReadOnly = ReadOnly;
            txtNumeroDocumento.ReadOnly = ReadOnly;
            txtRucProveedor.ReadOnly = ReadOnly;
            txtProveedor.ReadOnly = ReadOnly;
            chkAtenderTodo.Enabled = Enabled;
            gvListadoProductos.OptionsBehavior.Editable = Editable;
        }

        private void ObtenerDatos_EntradaAlmacen()
        {
            eAlmacen.eEntrada_Cabecera obj = new eAlmacen.eEntrada_Cabecera();
            obj = unit.Logistica.Obtener_DatosLogistica<eAlmacen.eEntrada_Cabecera>(20, cod_almacen, cod_empresa, cod_sede_empresa, cod_entrada);
            txtCodigo.Text = obj.cod_entrada;
            lkpAlmacen.EditValue = obj.cod_almacen;
            lkpTipoMovimiento.EditValue = obj.cod_tipo_movimiento;
            dtFechaDocumento.EditValue = obj.fch_documento;
            txtGlosa.Text = obj.dsc_glosa;
            txtNroOC.Text = obj.cod_orden_compra_servicio;
            txtProveedorOC.Tag = obj.cod_proveedor;
            txtProveedorOC.Text = obj.dsc_proveedor;
            dtFechaOC.EditValue = obj.fch_documentoOC;
            dtFechaTipoCambio.EditValue = obj.fch_tipocambio;
            txtTipoCambio.EditValue = obj.imp_tipocambio;
            glkpTipoDocumento.EditValue = obj.tipo_documento == null || obj.tipo_documento.Trim() == "" ? null : obj.tipo_documento;
            if (glkpTipoDocumento.EditValue != null)
            {
                eTipoComprobante objTC = new eTipoComprobante();
                objTC = unit.Factura.BuscarTipoComprobante<eTipoComprobante>(27, glkpTipoDocumento.EditValue.ToString().Trim());
                num_ctd_serie = objTC.num_ctd_serie; num_ctd_doc = objTC.num_ctd_doc;
                fmt_nro_doc = new string('0', num_ctd_doc);
            }
            txtSerieDocumento.Text = obj.serie_documento;
            txtNumeroDocumento.Text = obj.numero_documento == 0 ? "" : String.Format("{0:" + fmt_nro_doc + "}", obj.numero_documento);  //$"{eFact.numero_documento:00000000}";
            txtRucProveedor.Text = obj.dsc_ruc;
            txtProveedor.Tag = obj.cod_proveedor;
            txtProveedor.Text = obj.dsc_proveedor;
            btnAdjuntarArchivo.Enabled = obj.flg_PDF == "SI" ? false : true;
            btnVerPDF.Enabled = obj.flg_PDF == "SI" ? true : false;

            listaProd = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(21, cod_almacen, cod_empresa, cod_sede_empresa, cod_entrada: cod_entrada);
            bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
        }

        private void dtFechaTipoCambio_EditValueChanged(object sender, EventArgs e)
        {
            if (MiAccion == IngresoAlmacen.Nuevo) TraerTipoCambio();
        }

        private void TraerTipoCambio()
        {
            eTipoCambio objj = unit.Factura.BuscarTipoCambio<eTipoCambio>(9, Convert.ToDateTime(dtFechaTipoCambio.EditValue));
            if (objj != null)
            {
                txtTipoCambio.Text = objj.imp_cambio_venta.ToString();
            }
            else
            {
                MessageBox.Show("No existe tipo de cambio registrado para la fecha seleccionada", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTipoCambio.Text = "0.00";

            }
        }

        private void picBuscarProveedor_Click(object sender, EventArgs e)
        {
            Busqueda("", "OrdenesCompra");
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }
                if (lkpTipoMovimiento.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de movimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoMovimiento.Focus(); return; }
                if (txtNroOC.Text.Trim() == "") { MessageBox.Show("Debe seleccionar la orden de compra.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroOC.Focus(); return; }
                //if (glkpTipoDocumento.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpTipoDocumento.Focus(); return; }
                //if (txtSerieDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar una serie de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtSerieDocumento.Focus(); return; }
                //if (txtNumeroDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un numero de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNumeroDocumento.Focus(); return; }
                if (txtProveedorOC.Text.Trim() == "") { MessageBox.Show("Debe seleccionar proveedor.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtProveedor.Focus(); return; }
                if (txtGlosa.Text.Trim() == "") { MessageBox.Show("Debe ingresar una glosa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtGlosa.Focus(); return; }

                eAlmacen.eEntrada_Cabecera eEntr = AsignarValores_Cabecera();
                eEntr = unit.Logistica.Insertar_Actualizar_EntradaCabecera<eAlmacen.eEntrada_Cabecera>(eEntr);

                if (eEntr != null)
                {
                    txtCodigo.Text = eEntr.cod_entrada;
                    if (gvListadoProductos.RowCount > 0)
                    {
                        for (int nRow = 0; nRow < gvListadoProductos.RowCount; nRow++)
                        {
                            eAlmacen.eProductos_Almacen eProd = gvListadoProductos.GetRow(nRow) as eAlmacen.eProductos_Almacen;
                            if (eProd.num_cantidad_recibido == 0) continue;
                            eAlmacen.eEntrada_Detalle eDet = new eAlmacen.eEntrada_Detalle();
                            eDet.cod_entrada = eEntr.cod_entrada;
                            eDet.cod_almacen = cod_almacen;
                            eDet.cod_empresa = cod_empresa;
                            eDet.cod_sede_empresa = cod_sede_empresa;
                            eDet.cod_tipo_servicio = eProd.cod_tipo_servicio;
                            eDet.cod_subtipo_servicio = eProd.cod_subtipo_servicio;
                            eDet.cod_producto = eProd.cod_producto;
                            eDet.cod_unidad_medida = eProd.cod_unidad_medida;
                            eDet.num_cantidad = eProd.num_cantidad;
                            eDet.num_cantidad_recibido = eProd.num_cantidad_recibido;
                            eDet.num_cantidad_x_recibir = eProd.num_cantidad_x_recibir;
                            eDet.num_item_costo = eProd.num_item_costo;
                            eDet.imp_costo = eProd.imp_costo;
                            eDet.imp_total = eProd.imp_total;
                            eDet.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                            
                            eDet = unit.Logistica.Insertar_Actualizar_EntradaDetalle<eAlmacen.eEntrada_Detalle>(eDet);
                            if (eDet == null) MessageBox.Show("Error al registrar producto", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    /////////Validamos OC para pasarlo a ATENTDIDO/////////////////////////////////
                    eOrdenCompra_Servicio eOC = new eOrdenCompra_Servicio(); string respuesta = "";
                    eOC = unit.Logistica.Obtener_DatosLogistica<eOrdenCompra_Servicio>(30, cod_almacen, cod_empresa, cod_sede_empresa, cod_orden_compra_servicio: txtNroOC.Text.Trim());
                    
                    //var f = Convert.ToInt32();
                    if (int.TryParse(dsc_anho, out int anho)) { }                    
                    if (eOC != null && eOC.ctd_Atencion == 2)
                    {
                        respuesta = unit.OrdenCompra_Servicio.Atender_Orden(cod_empresa, cod_sede_empresa, txtNroOC.Text.Trim(), "C", dsc_anho: anho, Program.Sesion.Usuario.cod_usuario);
                    }
                    
                    ActualizarListado = true;
                    MessageBox.Show("Se ingresaron los productos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MiAccion == IngresoAlmacen.Nuevo)
                    {
                        MiAccion = IngresoAlmacen.Editar;
                        BloqueoControles(false, true, false);
                        //gvListadoProductos.Columns["num_cantidad_stock"].Visible = false;
                        //gvListadoProductos.Columns["num_cantidad_stock_nuevo"].Visible = false;
                        btnAdjuntarArchivo.Enabled = true; //btnVerPDF.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Error al registrar ingreso", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnAdjuntarArchivo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await AdjuntarArchivo();
        }
        static void Appl()
        {
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{TenantId}")
                .WithDefaultRedirectUri()
                .Build();
            TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }
        private static string ClientId = "";
        private static string TenantId = "";
        private static string Instance = "https://login.microsoftonline.com/";
        public static IPublicClientApplication _clientApp;
        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }

        private async Task AdjuntarArchivo()
        {
            try
            {
                //colocar mensaje que se necesita el tipo del documento.
               // if (string.IsNullOrEmpty(tipo_documento)) { HNG.MessageWarning("Se necesita el tipo de documento para cargar el archivo.", "Adjuntar Documento"); return; }


                //ObtenerListadeFolders
                string dsc_Carpeta = "Guia Remision";
                DateTime FechaRegistro = Convert.ToDateTime(dtFechaDocumento.EditValue);
                //VALIDAR SI EL PERIODO SE ENCUENTRA ABIERTO
                eFacturaProveedor objTrib = unit.Factura.Obtener_PeriodoTributario<eFacturaProveedor>(50, FechaRegistro.ToString("MM-yyyy"), cod_empresa);
                if (objTrib != null && objTrib.flg_cerrado == "SI")
                {
                    eFacturaProveedor objTrib2 = unit.Factura.Obtener_PeriodoTributario<eFacturaProveedor>(51, "", cod_empresa);
                    int n_Mes = 0, n_Anho = 0;
                    n_Mes = Convert.ToInt32(objTrib2.periodo_tributario.Substring(0, 2));
                    n_Anho = Convert.ToInt32(objTrib2.periodo_tributario.Substring(3, 4));
                    n_Anho = n_Mes == 12 ? n_Anho + 1 : n_Anho;
                    n_Mes = n_Mes == 12 ? 1 : n_Mes + 1;
                    FechaRegistro = new DateTime(n_Anho, n_Mes, 01);
                }

                int Anho = FechaRegistro.Year; int Mes = FechaRegistro.Month; string NombreMes = FechaRegistro.ToString("MMMM");
                OpenFileDialog myFileDialog = new OpenFileDialog();
                //myFileDialog.Filter = "Archivos (*.pdf;*.docx;*.xlsx;*.pptx;*.xml)|; *.pdf;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.xml";
                myFileDialog.Filter = "Archivos (*.pdf;*.xml)|; *.pdf;*.xml";
                myFileDialog.FilterIndex = 1;
                myFileDialog.InitialDirectory = "C:\\";
                myFileDialog.Title = "Abrir archivo";
                myFileDialog.CheckFileExists = false;
                myFileDialog.Multiselect = false;

                DialogResult result = myFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string IdArchivoAnho = "", IdArchivoMes = "", Extension = "";
                    var idArchivoPDF = ""; var idArchivoXML = "";
                    var TamañoDoc = new FileInfo(myFileDialog.FileName).Length / 1024;
                    if (TamañoDoc < 4000)
                    {
                        varPathOrigen = myFileDialog.FileName;
                        //////varNombreArchivo = Path.GetFileNameWithoutExtension(myFileDialog.SafeFileName) + Path.GetExtension(myFileDialog.SafeFileName);
                        //////tipo_documento = tipo_documento ?? glkpTipoDocumento.EditValue.ToString(); // analizar...
                        //List<eFacturaProveedor> list = unit.Factura.CombosEnGridControl<eFacturaProveedor>("TipoDocumento");
                        //TD_sunat = list.Find(x => x.tipo_documento == tipo_documento).cod_sunat;
                        //////varNombreArchivo = RUC + "-" + TD_sunat + "-" + serie_documento + "-" + $"{numero_documento:00000000}" + Path.GetExtension(myFileDialog.SafeFileName);
                        //varNombreArchivo = "GUIA_REMISION" + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento) + Path.GetExtension(myFileDialog.SafeFileName);
                        //varNombreArchivoSinExtension = "GUIA_REMISION" + "-" + TD_sunat + "-" + serie_documento + "-" + String.Format("{0:" + fmt_nro_doc + "}", numero_documento);
                        varNombreArchivo = "GUIA_REMISION" + "-" + txtNroOC.Text + Path.GetExtension(myFileDialog.SafeFileName);
                        varNombreArchivoSinExtension = "GUIA_REMISION" + "-" + txtNroOC.Text;
                        Extension = Path.GetExtension(myFileDialog.SafeFileName);
                    }
                    else
                    {
                        MessageBox.Show("Solo puede subir archivos hasta 5MB de tamaño", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");
                    eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, cod_empresa);
                    if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                    { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    ClientId = eEmp.ClientIdOnedrive;
                    TenantId = eEmp.TenantOnedrive;
                    Appl();
                    var app = PublicClientApp;
                    ////var app = App.PublicClientApp;
                    string correo = eEmp.UsuarioOnedrive;
                    string password = eEmp.ClaveOnedrive;

                    var securePassword = new SecureString();
                    foreach (char c in password)
                        securePassword.AppendChar(c);

                    authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                    GraphClient = new Microsoft.Graph.GraphServiceClient(
                      new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                      {
                          requestMessage
                              .Headers
                              .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                          return Task.FromResult(0);
                      }));

                    //var targetItemFolderId = eEmp.idCarpetaFacturasOnedrive;
                    eEmpresa.eOnedrive_Empresa eDatos = new eEmpresa.eOnedrive_Empresa();
                    eDatos = unit.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(26, cod_empresa, Convert.ToDateTime(dtFechaDocumento.EditValue).Year, dsc_Carpeta: dsc_Carpeta);
                    var targetItemFolderId = eDatos.idCarpeta;

                    //eFacturaProveedor IdCarpetaAnho = unit.Factura.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                    eEmpresa.eOnedrive_Empresa IdCarpetaAnho = unit.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, cod_empresa, FechaRegistro.Year, dsc_Carpeta: dsc_Carpeta);
                    if (IdCarpetaAnho == null) //Si no existe folder lo crea
                    {
                        var driveItem = new Microsoft.Graph.DriveItem
                        {
                            Name = Anho.ToString(),
                            Folder = new Microsoft.Graph.Folder
                            {
                            },
                            AdditionalData = new Dictionary<string, object>()
                            {
                            {"@microsoft.graph.conflictBehavior", "rename"}
                            }
                        };

                        var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderId].Children.Request().AddAsync(driveItem);
                        IdArchivoAnho = driveItemInfo.Id;
                    }
                    else //Si existe folder obtener id
                    {
                        IdArchivoAnho = IdCarpetaAnho.idCarpetaAnho;
                    }
                    var targetItemFolderIdAnho = IdArchivoAnho;

                    //eFacturaProveedor IdCarpetaMes = unit.Factura.ObtenerDatosOneDrive<eFacturaProveedor>(14, lkpEmpresaProveedor.EditValue.ToString(), Mes: Convert.ToDateTime(dtFechaRegistro.EditValue).Month);
                    eEmpresa.eOnedrive_Empresa IdCarpetaMes = unit.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, cod_empresa, FechaRegistro.Year, FechaRegistro.Month, dsc_Carpeta);
                    if (IdCarpetaMes == null)
                    {
                        var driveItem = new Microsoft.Graph.DriveItem
                        {
                            //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
                            Name = $"{Mes:00}" + ". " + NombreMes.ToUpper(),
                            Folder = new Microsoft.Graph.Folder
                            {
                            },
                            AdditionalData = new Dictionary<string, object>()
                        {
                        {"@microsoft.graph.conflictBehavior", "rename"}
                        }
                        };

                        var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderIdAnho].Children.Request().AddAsync(driveItem);
                        IdArchivoMes = driveItemInfo.Id;
                    }
                    else //Si existe folder obtener id
                    {
                        IdArchivoMes = IdCarpetaMes.idCarpetaMes;
                    }

                    //////////////////////////////////////////////////////// REEMPLAZAR DOCUMENTO DE ONEDRIVE ////////////////////////////////////////////////////////
                    eAlmacen.eEntrada_Cabecera obj = new eAlmacen.eEntrada_Cabecera();
                    obj = unit.Logistica.Obtener_DatosLogistica<eAlmacen.eEntrada_Cabecera>(31, cod_almacen, cod_empresa, cod_sede_empresa, cod_entrada);
                    //////////////////////////// ELIMINAR DOCUMENTO DE ONEDRIVE ////////////////////////////
                    if (obj.idPDF != null && obj.idPDF != "" && Extension.ToLower() == ".pdf") await Mover_Eliminar_ArchivoOneDrive(obj, new DateTime(), true, false, "ELIMINAR");
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //crea archivo en el OneDrive
                    byte[] data = System.IO.File.ReadAllBytes(varPathOrigen);
                    using (Stream stream = new MemoryStream(data))
                    {
                        string res = "";
                        int opcion = Extension.ToLower() == ".pdf" ? 1 : Extension.ToLower() == ".xml" ? 2 : 0;
                        if (opcion == 1 || opcion == 2)
                        {
                            var DriveItem = await GraphClient.Me.Drive.Items[IdArchivoMes].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                            idArchivoPDF = opcion == 1 ? DriveItem.Id : "";
                            idArchivoXML = opcion == 2 ? DriveItem.Id : "";

                            eFacturaProveedor objFact = new eFacturaProveedor();
                            objFact.tipo_documento = tipo_documento;
                            objFact.serie_documento = serie_documento;
                            objFact.numero_documento = numero_documento;
                            objFact.cod_proveedor = "";
                            objFact.idPDF = idArchivoPDF;
                            objFact.idXML = idArchivoXML;
                            //objFact.NombreArchivo = varNombreArchivo;
                            objFact.NombreArchivo = varNombreArchivoSinExtension;
                            objFact.cod_empresa = cod_empresa;
                            objFact.idCarpetaAnho = IdArchivoAnho;
                            objFact.idCarpetaMes = IdArchivoMes;

                            res = unit.Factura.ActualizarInformacionDocumentos(5, objFact, targetItemFolderId, Anho.ToString(), $"{Mes:00}", dsc_Carpeta, cod_entrada, cod_almacen, cod_sede_empresa);
                        }

                        if (res == "OK")
                        {
                            MessageBox.Show("Se registró el documento satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnVerPDF.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Hubieron problemas al registrar el documento", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    SplashScreenManager.CloseForm();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private async Task Mover_Eliminar_ArchivoOneDrive(eAlmacen.eEntrada_Cabecera obj, DateTime FechaPeriodo, bool PDF, bool XML, string opcion)
        {
            try
            {
                string dsc_Carpeta = "Guia Remision";
                int Anho = obj.fch_documento.Year; int Mes = obj.fch_documento.Month; string NombreMes = obj.fch_documento.Month.ToString("MMMM");
                string IdArchivoAnho = "", IdArchivoMes = "";
                //varNombreArchivo = obj.NombreArchivo;

                eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                ClientId = eEmp.ClientIdOnedrive;
                TenantId = eEmp.TenantOnedrive;
                Appl();
                var app = PublicClientApp;
                ////var app = App.PublicClientApp;
                string correo = eEmp.UsuarioOnedrive;
                string password = eEmp.ClaveOnedrive;

                var securePassword = new SecureString();
                foreach (char c in password)
                    securePassword.AppendChar(c);

                authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                GraphClient = new Microsoft.Graph.GraphServiceClient(
                  new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                  {
                      requestMessage
                          .Headers
                          .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                      return Task.FromResult(0);
                  }));

                //var targetItemFolderId = eEmp.idCarpetaFacturasOnedrive;
                eEmpresa.eOnedrive_Empresa eDatos = new eEmpresa.eOnedrive_Empresa();
                eDatos = unit.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(26, obj.cod_empresa, obj.fch_documento.Year, dsc_Carpeta: dsc_Carpeta);
                var targetItemFolderId = opcion != "ELIMINAR" ? eDatos.idCarpeta : "";

                //eFacturaProveedor IdCarpetaAnho = unit.Factura.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
                eEmpresa.eOnedrive_Empresa IdCarpetaAnho = unit.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, obj.cod_empresa, obj.fch_documento.Year, dsc_Carpeta: dsc_Carpeta);
                if (IdCarpetaAnho == null && opcion != "ELIMINAR") //Si no existe folder lo crea
                {
                    var driveItem = new Microsoft.Graph.DriveItem
                    {
                        Name = Anho.ToString(),
                        Folder = new Microsoft.Graph.Folder
                        {
                        },
                        AdditionalData = new Dictionary<string, object>()
                                {
                                {"@microsoft.graph.conflictBehavior", "rename"}
                                }
                    };

                    var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderId].Children.Request().AddAsync(driveItem);
                    IdArchivoAnho = driveItemInfo.Id;
                }
                else //Si existe folder obtener id
                {
                    IdArchivoAnho = opcion != "ELIMINAR" ? IdCarpetaAnho.idCarpetaAnho : "";
                }
                var targetItemFolderIdAnho = IdArchivoAnho;

                //eFacturaProveedor IdCarpetaMes = unit.Factura.ObtenerDatosOneDrive<eFacturaProveedor>(14, lkpEmpresaProveedor.EditValue.ToString(), Mes: Convert.ToDateTime(dtFechaRegistro.EditValue).Month);
                eEmpresa.eOnedrive_Empresa IdCarpetaMes = unit.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(14, obj.cod_empresa, obj.fch_documento.Year, obj.fch_documento.Month, dsc_Carpeta);
                if (IdCarpetaMes == null && opcion != "ELIMINAR")
                {
                    var driveItem = new Microsoft.Graph.DriveItem
                    {
                        //Name = Mes.ToString() + ". " + NombreMes.ToUpper(),
                        Name = $"{Mes:00}" + ". " + NombreMes.ToUpper(),
                        Folder = new Microsoft.Graph.Folder
                        {
                        },
                        AdditionalData = new Dictionary<string, object>()
                                    {
                                    {"@microsoft.graph.conflictBehavior", "rename"}
                                    }
                    };

                    var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderIdAnho].Children.Request().AddAsync(driveItem);
                    IdArchivoMes = driveItemInfo.Id;
                }
                else //Si existe folder obtener id
                {
                    IdArchivoMes = opcion != "ELIMINAR" ? IdCarpetaMes.idCarpetaMes : "";
                }

                for (int x = 0; x < 2; x++)
                {
                    if (x == 0 && !PDF) continue;
                    if (x == 1 && !XML) continue;
                    //MOVER ARCHIVO A OTRA CARPETA DEL ONEDRIVE
                    var DriveItem = new Microsoft.Graph.DriveItem
                    {
                        ParentReference = new Microsoft.Graph.ItemReference
                        {
                            Id = IdArchivoMes
                        },
                        //Name = varNombreArchivo + (x == 0 ? ".pdf" : ".xml") //Se comenta para que siga MANTENIENDO EL NOMBRE ASIGNADO
                    };

                    //if (opcion == "MOVER") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : obj.idXML].Request().UpdateAsync(DriveItem);
                    if (opcion == "ELIMINAR") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : ""].Request().DeleteAsync();
                    //if (opcion == "ELIMINAR") await GraphClient.Directory.DeletedItems[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async void btnVerPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eAlmacen.eEntrada_Cabecera obj = unit.Logistica.Obtener_DatosLogistica<eAlmacen.eEntrada_Cabecera>(20, cod_almacen, cod_empresa, cod_sede_empresa, cod_entrada);

            if (obj.idPDF == null || obj.idPDF == "")
            {
                MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, cod_empresa);
                if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                //var app = App.PublicClientApp;
                ClientId = eEmp.ClientIdOnedrive;
                TenantId = eEmp.TenantOnedrive;
                Appl();
                var app = PublicClientApp;

                try
                {
                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Abriendo documento", "Cargando...");
                    //eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, lkpEmpresaProveedor.EditValue.ToString());
                    string correo = eEmp.UsuarioOnedrive;
                    string password = eEmp.ClaveOnedrive;

                    var securePassword = new SecureString();
                    foreach (char c in password)
                        securePassword.AppendChar(c);

                    authResult = await app.AcquireTokenByUsernamePassword(scopes, correo, securePassword).ExecuteAsync();

                    GraphClient = new Microsoft.Graph.GraphServiceClient(
                    new Microsoft.Graph.DelegateAuthenticationProvider((requestMessage) =>
                    {
                        requestMessage
                            .Headers
                            .Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
                        return Task.FromResult(0);
                    }));

                    string IdPDF = obj.idPDF;
                    string IdOneDriveDoc = IdPDF;

                    var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                    string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + obj.NombreArchivo + ".pdf";
                    if (!System.IO.File.Exists(ruta))
                    {
                        using (var fileStream = new FileStream(ruta, FileMode.Create, System.IO.FileAccess.Write))
                            fileContent.CopyTo(fileStream);
                    }

                    if (!System.IO.Directory.Exists(unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()))) System.IO.Directory.CreateDirectory(unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()));
                    System.Diagnostics.Process.Start(ruta);
                    SplashScreenManager.CloseForm();
                }
                catch (Exception ex)
                {
                    SplashScreenManager.CloseForm();
                    MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                    return;
                }
            }
        }

        private void glkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            //  verificar si  la variable  tipo_documento: va  a obtener el id de este control
            if (glkpTipoDocumento.EditValue !=null)
            { 
                tipo_documento = glkpTipoDocumento.EditValue.ToString(); 
            }
        }

        private eAlmacen.eEntrada_Cabecera AsignarValores_Cabecera()
        {
            eAlmacen.eEntrada_Cabecera obj = new eAlmacen.eEntrada_Cabecera();
            obj.cod_entrada = txtCodigo.Text;
            obj.cod_almacen = cod_almacen;
            obj.cod_tipo_movimiento = lkpTipoMovimiento.EditValue.ToString();
            obj.dsc_glosa = txtGlosa.Text;
            obj.cod_empresa = cod_empresa;
            obj.cod_sede_empresa = cod_sede_empresa;
            obj.cod_orden_compra_servicio = txtNroOC.Text.Trim();
            obj.fch_documento = Convert.ToDateTime(dtFechaDocumento.EditValue);
            obj.fch_tipocambio = Convert.ToDateTime(dtFechaTipoCambio.EditValue);
            obj.imp_tipocambio = Convert.ToDecimal(txtTipoCambio.EditValue);
            obj.tipo_documento = glkpTipoDocumento.EditValue == null ? null : glkpTipoDocumento.EditValue.ToString();
            obj.serie_documento = txtSerieDocumento.Text;
            obj.numero_documento = txtNumeroDocumento.Text == "" ? 0 : Convert.ToDecimal(txtNumeroDocumento.Text);
            obj.cod_proveedor = txtProveedorOC.Tag.ToString();
            obj.flg_activo = "SI";
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        public void Busqueda(string dato, string tipo)
        {
            if (lkpAlmacen.EditValue == null) { MessageBox.Show("Debe seleccionar el almacen.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAlmacen.Focus(); return; }

            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;
            
            switch (tipo)
            {
                case "OrdenesCompra":
                    frm.entidad = frmBusquedas.MiEntidad.OrdenesCompra;
                    frm.cod_almacen = lkpAlmacen.EditValue.ToString();
                    frm.cod_empresa = cod_empresa;
                    frm.cod_sede_empresa = cod_sede_empresa;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "OrdenesCompra":
                    txtNroOC.Text = frm.codigo;
                    txtProveedorOC.Tag = frm.cod_condicion1;
                    txtProveedorOC.Text = frm.descripcion;
                    dtFechaOC.EditValue = frm.fch_generica;
                    /*-----*Asignar AÑO desde la ORDEN que se selecciona*-----*/
                    dsc_anho = frm.dsc_anho.ToString();

                    //listaProd = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(5, cod_orden_compra: frm.codigo);
                    listaProd = unit.Logistica.Obtener_ListaLogistica<eAlmacen.eProductos_Almacen>(17, cod_empresa: cod_empresa, cod_sede_empresa: cod_sede_empresa, cod_orden_compra_servicio: frm.codigo);
                    bsListadoProductos.DataSource = listaProd; gvListadoProductos.RefreshData();
                    break;
            }
        }

        private void frmRegistrarEntradaAlmacen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion != IngresoAlmacen.Nuevo) this.Close();
        }

        private void picBuscarDocumentos_Click(object sender, EventArgs e)
        {
            if (txtProveedorOC.Tag == null || txtProveedorOC.Tag.ToString().Trim() == "") { MessageBox.Show("Debe seleccionar una Orden de Compra.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroOC.Focus(); return; }

            frmFacturasDetalle frm = new frmFacturasDetalle();
            frm.BusquedaAutomatica = false;
            frm.cod_empresa = cod_empresa;
            frm.cod_proveedor = txtProveedorOC.Tag.ToString();
            frm.cod_moneda = "SOL";
            frm.BusquedaLogistica = true;
            frm.ShowDialog();
            if (frm.listDocumentosNC.Count > 0)
            {
                eFacturaProveedor eFact = new eFacturaProveedor();
                eFact = unit.Factura.ObtenerFacturaProveedor<eFacturaProveedor>(2, frm.listDocumentosNC[0].tipo_documento, frm.listDocumentosNC[0].serie_documento, frm.listDocumentosNC[0].numero_documento, frm.listDocumentosNC[0].cod_proveedor);

                eTipoComprobante obj = new eTipoComprobante();
                obj = unit.Factura.BuscarTipoComprobante<eTipoComprobante>(27, eFact.tipo_documento);
                num_ctd_serie = obj.num_ctd_serie; num_ctd_doc = obj.num_ctd_doc;
                fmt_nro_doc = new string('0', num_ctd_doc);

                glkpTipoDocumento.EditValue = eFact.tipo_documento;
                txtSerieDocumento.Text = eFact.serie_documento;
                txtNumeroDocumento.Text = String.Format("{0:" + fmt_nro_doc + "}", eFact.numero_documento);  //$"{eFact.numero_documento:00000000}";
                txtRucProveedor.Text = eFact.dsc_ruc;
                txtProveedor.Tag = eFact.cod_proveedor;
                txtProveedor.Text = eFact.dsc_proveedor;

                //Analizar:  obtner el tipo de documento
                tipo_documento = eFact.tipo_documento;
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

        private void gvListadoProductos_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetFocusedRow() as eAlmacen.eProductos_Almacen;
                if (objProd != null)
                {
                    if (e.Column.FieldName == "num_cantidad_recibido")
                    {
                        if (objProd.num_cantidad_recibido > objProd.num_cantidad_x_recibir_interno)
                        {
                            MessageBox.Show("No puede ingresar una cantidad mayor a la que falta recibir", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            objProd.num_cantidad_recibido = objProd.num_cantidad_x_recibir_interno;
                            objProd.num_cantidad_x_recibir = 0;
                            gvListadoProductos.RefreshData();
                            return;
                        }

                        objProd.num_cantidad_x_recibir = objProd.num_cantidad_x_recibir_interno - objProd.num_cantidad_recibido;
                        objProd.imp_total = objProd.imp_costo * objProd.num_cantidad_recibido;
                    }
                    gvListadoProductos.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkAtenderTodo_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                for (int nRow = 0; nRow < gvListadoProductos.RowCount; nRow++)
                {
                    eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetRow(nRow) as eAlmacen.eProductos_Almacen;
                    if (objProd == null) continue;
                    objProd.num_cantidad_recibido = chkAtenderTodo.CheckState == CheckState.Checked ? objProd.num_cantidad_x_recibir_interno : 0;
                    objProd.num_cantidad_x_recibir = objProd.num_cantidad_x_recibir_interno - objProd.num_cantidad_recibido;
                    objProd.imp_total = objProd.imp_costo * objProd.num_cantidad_recibido;
                }
                gvListadoProductos.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rbtnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MiAccion == IngresoAlmacen.Nuevo)
            {
                eAlmacen.eProductos_Almacen objP = gvListadoProductos.GetFocusedRow() as eAlmacen.eProductos_Almacen;
                listaProd.Remove(objP);
                int n_Orden = 1;
                foreach (eAlmacen.eProductos_Almacen obj in listaProd)
                {
                    obj.n_Orden = n_Orden;
                    n_Orden += 1;
                }
                bsListadoProductos.DataSource = listaProd;
                gvListadoProductos.RefreshData();
            }
        }

        private void gvListadoProductos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eAlmacen.eProductos_Almacen objProd = gvListadoProductos.GetRow(e.RowHandle) as eAlmacen.eProductos_Almacen;
                    if (e.Column.FieldName == "num_cantidad_recibido") e.Appearance.ForeColor = Color.Blue;
                    if (e.Column.FieldName == "num_cantidad_x_recibir" && objProd.num_cantidad_x_recibir > 0) e.Appearance.ForeColor = Color.Red;
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}