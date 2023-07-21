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
using DevExpress.XtraLayout.Utils;
using Microsoft.Identity.Client;
using System.IO;
using DevExpress.XtraSplashScreen;
using System.Security;
using System.Net.Http.Headers;
using System.Configuration;

namespace UI_BackOffice.Formularios.Personal
{
    internal enum Trabajador
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }
    public partial class frmMantTrabajador : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmListadoTrabajador frmHandler;
        internal Trabajador MiAccion = Trabajador.Nuevo;
        public eTrabajador eTrab = new eTrabajador();
        List<eTrabajador.eInfoLaboral_Trabajador> ListHistInfoLaboral = new List<eTrabajador.eInfoLaboral_Trabajador>();
        List<eTrabajador.eContactoEmergencia_Trabajador> ListContactos = new List<eTrabajador.eContactoEmergencia_Trabajador>();
        List<eTrabajador.eCertificadoEMO_Trabajador> ListInfoEMO = new List<eTrabajador.eCertificadoEMO_Trabajador>();
        List<eTrabajador.eInfoLaboral_Trabajador> ListInfoLaboral = new List<eTrabajador.eInfoLaboral_Trabajador>();
        List<eTrabajador.eInfoFamiliar_Trabajador> ListInfoFamiliar = new List<eTrabajador.eInfoFamiliar_Trabajador>();
        List<eTrabajador.eInfoEconomica_Trabajador> ListInfoEconomica = new List<eTrabajador.eInfoEconomica_Trabajador>();
        List<eTrabajador.eInfoAcademica_Trabajador> ListInfoAcademica = new List<eTrabajador.eInfoAcademica_Trabajador>();
        List<eTrabajador.eInfoProfesional_Trabajador> ListInfoProfesional = new List<eTrabajador.eInfoProfesional_Trabajador>();
        public string ActualizarListado = "NO";
        
        public string cod_trabajador = "", cod_empresa = "";
        int ContactEmergencia = 0, InfoLaboral = 0, InfoBancaria = 0, DatosAdicionales = 0, InfoFamiliar = 0, InfoEconomica = 0, InfoAcademica = 0, ExpProfesional = 0, InfoSalud = 0, InfoVivienda = 0;
        Image ImgPDF = DevExpress.Images.ImageResourceCache.Default.GetImage("images/export/exporttopdf_16x16.png");

        //OneDrive
        private Microsoft.Graph.GraphServiceClient GraphClient { get; set; }
        AuthenticationResult authResult = null;
        string[] scopes = new string[] { "Files.ReadWrite.All" };
        string varPathOrigen = "";
        string varNombreArchivo = "", varNombreArchivoSinExtension = "";

        public frmMantTrabajador()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
        public frmMantTrabajador(frmListadoTrabajador frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }
        private void frmMantTrabajador_Load(object sender, EventArgs e)
        {
            Inicializar();
            HabilitarBotones();
        }
        private void HabilitarBotones()
        {
            List<eVentana> listPermisos = unit.Sistema.ListarMenuxUsuario<eVentana>(Program.Sesion.Usuario.cod_usuario, frmHandler != null ? frmHandler.Name : "", Program.Sesion.Global.Solucion);

            if (listPermisos.Count > 0)
            {
                if (listPermisos[0].flg_escritura == false) BloqueoControles(false, true, false);
            }
        }
        private void Inicializar()
        {
            try
            {
                switch (MiAccion)
                {
                    case Trabajador.Nuevo:
                        Nuevo();
                        break;
                    case Trabajador.Editar:
                        Editar();
                        break;
                    case Trabajador.Vista:
                        Vista();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Nuevo()
        {
            CargarLookUpEdit();
            btnNuevo.Enabled = false;
            acctlMenu.Enabled = false;
            txtCodTrabajador.Text = "";
            glkpTipoDocumento.EditValue = "DI001";
            lkpEstadoCivil.EditValue = "01";
            lkpPais.EditValue = "00001"; lkpDepartamento.EditValue = "00015"; lkpProvincia.EditValue = "00128"; lkpNacionalidad.EditValue = "00001";
            dtFecNacimiento.EditValue = DateTime.Today;
            //dtFecVctoDocumento.EditValue = DateTime.Today;
            lkpSistPensionarioInfoBancaria.EditValue = "AFP";
        }

        private void Editar()
        {
            ContactEmergencia = 0; InfoLaboral = 0; InfoBancaria = 0; DatosAdicionales = 0; InfoFamiliar = 0; InfoEconomica = 0;
            InfoAcademica = 0; ExpProfesional = 0; InfoSalud = 0; InfoVivienda = 0;
            CargarLookUpEdit();
            ObtenerDatos_Trabajador();
            sbtnVerDocumentos.Enabled = true;
            acctlMenu.Enabled = true;
        }

        private void Vista()
        {

        }

        private void BloqueoControles(bool Enabled, bool ReadOnly, bool Editable)
        { 

        }
        private void CargarLookUpEdit()
        {
            try
            {
                CargarCombosGridLookup("TipoDocumento", glkpTipoDocumento, "cod_tipo_documento", "dsc_tipo_documento", "", valorDefecto: true);
                unit.Trabajador.CargaCombosLookUp("Empresa", lkpEmpresa, "cod_empresa", "dsc_empresa", "", valorDefecto: true);
                unit.Trabajador.CargaCombosLookUp("EstadoCivil", lkpEstadoCivil, "cod_estado_civil", "dsc_estado_civil", "", valorDefecto: true);
                unit.Clientes.CargaCombosLookUp("TipoPais", lkpPais, "cod_pais", "dsc_pais", "");
                unit.Clientes.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "");
                unit.Clientes.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "");
                CargarCombosGridLookup("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "");
                unit.Clientes.CargaCombosLookUp("TipoPais", lkpNacionalidad, "cod_pais", "dsc_pais", "");
                List<eProveedor_Empresas> listEmpresasUsuario = unit.Proveedores.ListarEmpresasProveedor<eProveedor_Empresas>(11, "", Program.Sesion.Usuario.cod_usuario);
                lkpEmpresa.EditValue = listEmpresasUsuario[0].cod_empresa;
                lkpDepartamento.EditValue = "00015"; lkpProvincia.EditValue = "00128";
                if (MiAccion == Trabajador.Nuevo)
                {
                    picAnteriorTrabajador.Enabled = false; picSiguienteTrabajador.Enabled = false; btnNuevo.Enabled = false;
                }
                else
                {
                    picAnteriorTrabajador.Enabled = true; picSiguienteTrabajador.Enabled = true; btnNuevo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CargarCombosGridLookup(string nCombo, GridLookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "", string cod_condicion = "", bool valorDefecto = false)
        {
            DataTable tabla = new DataTable();
            tabla = unit.Trabajador.ObtenerListadoGridLookup(nCombo, cod_condicion);

            combo.Properties.DataSource = tabla;
            combo.Properties.ValueMember = campoValueMember;
            combo.Properties.DisplayMember = campoDispleyMember;
            if (campoSelectedValue == "") { combo.EditValue = null; } else { combo.EditValue = campoSelectedValue; }
            if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
        }

        private void ObtenerDatos_Trabajador()
        {
            eTrab = unit.Trabajador.Obtener_Trabajador<eTrabajador>(2, cod_trabajador, cod_empresa);
            lblNombreTrabajador.Text = eTrab.dsc_nombres_completos;
            lblNombreTrabajador.Visibility = LayoutVisibility.Always;
            cod_trabajador = eTrab.cod_trabajador;
            txtCodTrabajador.Text = eTrab.cod_trabajador;
            txtApellPaterno.Text = eTrab.dsc_apellido_paterno;
            txtApellMaterno.Text = eTrab.dsc_apellido_materno;
            txtNombre.Text = eTrab.dsc_nombres;
            dtFecNacimiento.EditValue = eTrab.fch_nacimiento;
            if (eTrab.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacimiento.EditValue = null; }
            lkpEstadoCivil.EditValue = eTrab.cod_estadocivil;
            lkpEmpresa.EditValue = eTrab.cod_empresa;
            glkpTipoDocumento.EditValue = eTrab.cod_tipo_documento;
            txtNroDocumento.Text = eTrab.dsc_documento;
            dtFecVctoDocumento.EditValue = eTrab.fch_vcto_documento;
            txtNroUbigeoDocumento.Text = eTrab.nro_ubigeo_documento;
            lkpNacionalidad.EditValue = eTrab.cod_nacionalidad;
            chkFlgDNI.CheckState = eTrab.flg_DNI == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkFlgCV.CheckState = eTrab.flg_CV == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkFlgAntPoliciales.CheckState = eTrab.flg_AntPolicial == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkFlgAntPenales.CheckState = eTrab.flg_AntPenal == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkFlgVerifDomiciliaria.CheckState = eTrab.flg_VerifDomiciliaria == "SI" ? CheckState.Checked : CheckState.Unchecked;
            btnVerDocIdentidad.Enabled = eTrab.flg_DNI == "SI" ? true : false;
            btnVerCV.Enabled = eTrab.flg_CV == "SI" ? true : false;
            btnVerAntcPoliciales.Enabled = eTrab.flg_AntPolicial == "SI" ? true : false;
            btnVerAntcPenales.Enabled = eTrab.flg_AntPenal == "SI" ? true : false;
            btnVerVerifDomiciliaria.Enabled = eTrab.flg_VerifDomiciliaria == "SI" ? true : false;
            lkpSistPensionarioInfoBancaria.EditValue = eTrab.cod_sist_pension;
            lkpNombreAFPInfoBancaria.EditValue = eTrab.cod_APF;
            txtNroCUSPPInfoBancaria.Text = eTrab.cod_CUSPP;
            txtDireccion.Text = eTrab.dsc_direccion;
            txtReferencia.Text = eTrab.dsc_referencia;
            lkpPais.EditValue = eTrab.cod_pais;
            lkpDepartamento.EditValue = eTrab.cod_departamento;
            lkpProvincia.EditValue = eTrab.cod_provincia;
            glkpDistrito.EditValue = eTrab.cod_distrito;
            txtTelefono.Text = eTrab.dsc_telefono;
            txtCelular.Text = eTrab.dsc_celular;
            txtEmail1.Text = eTrab.dsc_mail_1;
            txtEmail2.Text = eTrab.dsc_mail_2;
            grdbTipoPersonal.SelectedIndex = eTrab.cod_TipoPersonal == "OFICINA" ? 0 : 1;
            if (eTrab.fch_entrega_uniforme.ToString().Contains("1/01/0001")) { dtFecEntregaUnif.EditValue = null; } else { dtFecEntregaUnif.EditValue = eTrab.fch_entrega_uniforme; }
            if (eTrab.fch_renovacion_uniforme.ToString().Contains("1/01/0001")) { dtFecRenovacionUnif.EditValue = null; } else { dtFecRenovacionUnif.EditValue = eTrab.fch_renovacion_uniforme; }
            ObtenerDatos_HistoricoInfoLaboral();
        }

        private void ObtenerDatos_ContactosEmergencia()
        {
            ListContactos = unit.Trabajador.ListarTrabajadores<eTrabajador.eContactoEmergencia_Trabajador>(3, cod_trabajador, cod_empresa);
            bsListaContactos.DataSource = ListContactos; gvListadoContactos.RefreshData();
        }

        private void ObtenerDatos_HistoricoInfoLaboral()
        {
            ListHistInfoLaboral = unit.Trabajador.ListarTrabajadores<eTrabajador.eInfoLaboral_Trabajador>(4, cod_trabajador, cod_empresa);
            bsHistorialInfoLaboral.DataSource = ListHistInfoLaboral; gvInfoLaboral.RefreshData();
        }

        private void ObtenerDatos_InfoLaboral()
        {
            ListInfoLaboral = unit.Trabajador.ListarTrabajadores<eTrabajador.eInfoLaboral_Trabajador>(4, cod_trabajador, cod_empresa);
            bsListaInfoLaboral.DataSource = ListInfoLaboral; gvListadoInfoLaboral.RefreshData();
        }

        private void ObtenerDatos_InfoSalud()
        {
            eTrabajador.eInfoSalud_Trabajador obj = new eTrabajador.eInfoSalud_Trabajador();
            obj = unit.Trabajador.Obtener_Trabajador<eTrabajador.eInfoSalud_Trabajador>(6, cod_trabajador, cod_empresa);
            if (obj == null) return;
            chkflgAlergiasInfoSalud.CheckState = obj.flg_alergias == "SI" ? CheckState.Checked : CheckState.Unchecked;
            mmAlergias.Text = obj.dsc_alergias;
            chkflgOperacionesInfoSalud.CheckState = obj.flg_operaciones == "SI" ? CheckState.Checked : CheckState.Unchecked;
            mmOperaciones.Text = obj.dsc_operaciones;
            chkflgEnfPrexistenteInfoSalud.CheckState = obj.flg_enfprexistente == "SI" ? CheckState.Checked : CheckState.Unchecked;
            mmEnfPrexistente.Text = obj.dsc_enfprexistente;
            chkflgTratamientoInfoSalud.CheckState = obj.flg_tratprexistente == "SI" ? CheckState.Checked : CheckState.Unchecked;
            mmTratamiento.Text = obj.dsc_tratprexistente;
            chkflgEnfActualInfoSalud.CheckState = obj.flg_enfactual == "SI" ? CheckState.Checked : CheckState.Unchecked;
            mmEnfActualidad.Text = obj.dsc_enfactual;
            chkflgTratActualInfoSalud.CheckState = obj.flg_tratactual == "SI" ? CheckState.Checked : CheckState.Unchecked;
            mmTratActual.Text = obj.dsc_tratactual;
            chkflgDiscapacidadInfoSalud.CheckState = obj.flg_discapacidad == "SI" ? CheckState.Checked : CheckState.Unchecked;
            mmDiscapacidad.Text = obj.dsc_discapacidad;
            chkflgOtrosInfoSalud.CheckState = obj.flg_otros == "SI" ? CheckState.Checked : CheckState.Unchecked;
            mmOtros.Text = obj.dsc_otros;
            lkpGrupoSanguineoInfoSalud.EditValue = obj.dsc_gruposanguineo;
            lkpEstadoSaludInfoSalud.EditValue = obj.dsc_estadosalud;
            lkpSeguroSaludInfoSalud.EditValue = obj.dsc_segurosalud;
            txtEspecificarInfoSalud.Text = obj.dsc_tiposegurosalud;
        }

        private void ObtenerDatos_CaracteristicasTallas()
        {
            eTrabajador.eCaractFisicas_Trabajador objC = new eTrabajador.eCaractFisicas_Trabajador();
            objC = unit.Trabajador.Obtener_Trabajador<eTrabajador.eCaractFisicas_Trabajador>(12, cod_trabajador, cod_empresa);
            if (objC == null) return;
            txtEstaturaCaractFisica.EditValue = objC.dsc_estatura;
            txtPesoCaractFisica.EditValue = objC.dsc_peso;
            txtIMCCaractFisica.EditValue = objC.dsc_IMC;
            chkflgLentesCaractFisica.CheckState = objC.flg_lentes == "SI" ? CheckState.Checked : CheckState.Unchecked;
            eTrabajador.eTallaUniforme_Trabajador objT = new eTrabajador.eTallaUniforme_Trabajador();
            objT = unit.Trabajador.Obtener_Trabajador<eTrabajador.eTallaUniforme_Trabajador>(13, cod_trabajador, cod_empresa);
            if (objT == null) return;
            lkpPoloTallaUnif.EditValue = objT.cod_talla_polo;
            lkpCamisaTallaUnif.EditValue = objT.cod_talla_camisa;
            lkpPantalonTallaUnif.EditValue = objT.cod_talla_pantalon;
            lkpCasacaTallaUnif.EditValue = objT.cod_talla_casaca;
            lkpMamelucoTallaUnif.EditValue = objT.cod_talla_mameluco;
            lkpChalecoTallaUnif.EditValue = objT.cod_talla_chaleco;
            txtCalzadoTallaUnif.EditValue = objT.cod_talla_calzado;
            lkpCascoTallaUnif.EditValue = objT.cod_talla_casco;
            lkpFajaTallaUnif.EditValue = objT.cod_talla_faja;
            txtCasilleroTallaUnif.EditValue = objT.dsc_casillero;
            chkflgLentesTallaUnif.CheckState = objT.flg_lentes == "SI" ? CheckState.Checked : CheckState.Unchecked;
        }

        private void ObtenerDatos_HistorialEMO()
        {
            ListInfoEMO.Clear();
            ListInfoEMO = unit.Trabajador.ListarTrabajadores<eTrabajador.eCertificadoEMO_Trabajador>(15, cod_trabajador, cod_empresa);
            bsListadoInfoEMO.DataSource = ListInfoEMO; gvHistoriaEMO.RefreshData();
        }

        private void ObtenerDatos_InfoAcademica()
        {
            ListInfoAcademica.Clear();
            ListInfoAcademica = unit.Trabajador.ListarTrabajadores<eTrabajador.eInfoAcademica_Trabajador>(9, cod_trabajador, cod_empresa);
            bsListaInfoAcademica.DataSource = ListInfoAcademica; gvListadoFormAcademica.RefreshData();
        }

        private void ObtenerDatos_InfoProfesional()
        {
            ListInfoProfesional.Clear();
            ListInfoProfesional = unit.Trabajador.ListarTrabajadores<eTrabajador.eInfoProfesional_Trabajador>(10, cod_trabajador, cod_empresa);
            bsListaInfoProfesional.DataSource = ListInfoProfesional; gvListadoExpProfesional.RefreshData();
        }

        private void ObtenerDatos_InfoFamiliar()
        {
            ListInfoFamiliar.Clear();
            ListInfoFamiliar = unit.Trabajador.ListarTrabajadores<eTrabajador.eInfoFamiliar_Trabajador>(7, cod_trabajador, cod_empresa);
            bsListaInfoFamiliar.DataSource = ListInfoFamiliar; gvListadoInfoFamiliar.RefreshData();
        }

        private void ObtenerDatos_InfoEconomica()
        {
            ListInfoEconomica.Clear();
            ListInfoEconomica = unit.Trabajador.ListarTrabajadores<eTrabajador.eInfoEconomica_Trabajador>(8, cod_trabajador, cod_empresa);
            bsListadoInfoEconomica.DataSource = ListInfoEconomica; gvListadoInfoEconomica.RefreshData();
        }

        private void ObtenerDatos_InfoVivienda()
        {
            eTrabajador.eInfoVivienda_Trabajador obj = new eTrabajador.eInfoVivienda_Trabajador();
            obj = unit.Trabajador.Obtener_Trabajador<eTrabajador.eInfoVivienda_Trabajador>(11, cod_trabajador, cod_empresa);
            if (obj == null) return;
            lkpViviendaInfoVivienda.EditValue = obj.cod_tipovivienda;
            lkpComodidadInfoVivienda.EditValue = obj.cod_tipocomodidad;
            chkflgPuertasInfoVivienda.CheckState = obj.flg_puertas == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgVentanasInfoVivienda.CheckState = obj.flg_ventanas == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgTechoInfoVivienda.CheckState = obj.flg_techo == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgTelefonoInfoVivienda.CheckState = obj.flg_telefono == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkCelularesInfoVivienda.CheckState = obj.flg_celulares == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgInternetComunicacionInfoVivienda.CheckState = obj.flg_internet_comunicacion == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgLuzInfoVivienda.CheckState = obj.flg_luz == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgAguaInfoVivienda.CheckState = obj.flg_agua == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgDesagueInfoVivienda.CheckState = obj.flg_desague == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgGasInfoVivienda.CheckState = obj.flg_gas == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgCableInfoVivienda.CheckState = obj.flg_cable == "SI" ? CheckState.Checked : CheckState.Unchecked;
            chkflgInternetServicioInfoVivienda.CheckState = obj.flg_internet_servicio == "SI" ? CheckState.Checked : CheckState.Unchecked;
            mmViasAccesoInfoVivienda.Text = obj.dsc_viaacceso;
            mmIluminacionInfoVivienda.Text = obj.dsc_iluminacion;
            mmEntornoInfoVivienda.Text = obj.dsc_entorno;
            mmPuestoPolicialInfoVivienda.Text = obj.dsc_puestopolicial;
            txtNombreFamiliarInfoVivienda.Text = obj.dsc_nombre_familiar;
            txtHorasCasaInfoVivienda.Text = obj.dsc_horasencasa;
            lkpParentescoInfoVivienda.EditValue = obj.cod_parentesco;
            txtCelularInfoVivienda.Text = obj.dsc_celular;
            txtEmailInfoVivienda.Text = obj.dsc_mail;
        }

        private void ObtenerDatos_InfoBancaria()
        {
            eTrabajador.eInfoBancaria_Trabajador obj = new eTrabajador.eInfoBancaria_Trabajador();
            obj = unit.Trabajador.Obtener_Trabajador<eTrabajador.eInfoBancaria_Trabajador>(14, cod_trabajador, cod_empresa);
            if (obj == null) return;
            glkpBancoInfoBancaria.EditValue = obj.cod_banco;
            lkpTipoMonedaInfoBancaria.EditValue = obj.cod_moneda;
            lkpTipoCuentaInfoBancaria.EditValue = obj.cod_tipo_cuenta;
            txtNroCuentaBancariaInfoBancaria.Text = obj.dsc_cta_bancaria;
            txtNroCuentaCCIInfoBancaria.Text = obj.dsc_cta_interbancaria;
            glkpBancoCTSInfoBancaria.EditValue = obj.cod_banco_CTS;
            lkpTipoMonedaCTSInfoBancaria.EditValue = obj.cod_moneda_CTS;
            txtNroCuentaBancariaCTSInfoBancaria.Text = obj.dsc_cta_bancaria_CTS;
            txtNroCuentaCCICTSInfoBancaria.Text = obj.dsc_cta_interbancaria_CTS;
            lkpSistPensionarioInfoBancaria.EditValue = obj.cod_sist_pension;
            lkpNombreAFPInfoBancaria.EditValue = obj.cod_APF;
            txtNroCUSPPInfoBancaria.Text = obj.cod_CUSPP;
        }

        private void acctlMenu_SelectedElementChanged(object sender, DevExpress.XtraBars.Navigation.SelectedElementChangedEventArgs e)
        {
            try
            {
                switch (e.Element.Name)
                {
                    case "actelGeneral":
                        navframeTrabajador.SelectedPage = navpageGemeral;
                        break;
                    case "actelContactoEmergencia":
                        navframeTrabajador.SelectedPage = navpageContactoEmergencia;
                        if (ContactEmergencia == 0)
                        {
                            unit.Trabajador.CargaCombosLookUp("Parentesco", lkpParentescoContacto, "cod_parentesco", "dsc_parentesco", "", valorDefecto: true);
                            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumentoContacto, "cod_tipo_documento", "dsc_tipo_documento", "", valorDefecto: true);
                            //dtFecNacimientoContacto.EditValue = DateTime.Today;
                            ObtenerDatos_ContactosEmergencia();
                            gvListadoContactos_FocusedRowChanged(gvListadoContactos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
                            ContactEmergencia = 1; 
                        }
                        break;
                    case "actelInfoLaboral":
                        navframeTrabajador.SelectedPage = navpageInfoLaboral;
                        if (InfoLaboral == 0)
                        {
                            unit.Trabajador.CargaCombosLookUp("TipoContrato", lkpTipoContratoInfoLaboral, "cod_tipoContrato", "dsc_tipoContrato", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("PeriodoPrueba", lkpTiempoPeriodoInfoLaboral, "cod_periodoprueba", "dsc_periodoprueba", "", valorDefecto: true);
                            unit.Factura.CargaCombosLookUp("DistribucionCECO", lkpPrefCECOInfoLaboral, "cod_CECO", "dsc_CECO", "", valorDefecto: true, cod_empresa: cod_empresa);
                            dtFecIngresoInfoLaboral.EditValue = DateTime.Today;
                            dtFecFirmaInfoLaboral.EditValue = DateTime.Today;
                            dtFecVctoInfoLaboral.EditValue = DateTime.Today;
                            if (ListInfoFamiliar.Count == 0)
                            {
                                ListInfoFamiliar = unit.Trabajador.ListarTrabajadores<eTrabajador.eInfoFamiliar_Trabajador>(7, cod_trabajador, cod_empresa);
                            }
                            if (ListInfoFamiliar.Count > 0)
                            {
                                eTrabajador.eInfoFamiliar_Trabajador objF = ListInfoFamiliar.Find(x => x.cod_parentesco == "PR006");
                                txtMontoAsigFamiliarInfoLaboral.ReadOnly = objF != null ? false : true;
                            }
                            ObtenerDatos_InfoLaboral();
                            gvListadoInfoLaboral_FocusedRowChanged(gvListadoInfoLaboral, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
                            InfoLaboral = 1;
                        }
                        break;
                    case "actelInfoBancaria":
                        navframeTrabajador.SelectedPage = navpageInfoBancaria;
                        if (InfoBancaria == 0)
                        {
                            CargarCombosGridLookup("Banco", glkpBancoInfoBancaria, "cod_banco", "dsc_banco", "");
                            unit.Proveedores.CargaCombosLookUp("Moneda", lkpTipoMonedaInfoBancaria, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                            unit.Proveedores.CargaCombosLookUp("TipoCuentaBancaria", lkpTipoCuentaInfoBancaria, "cod_tipo_cuenta", "dsc_tipo_cuenta", "");
                            CargarCombosGridLookup("Banco", glkpBancoCTSInfoBancaria, "cod_banco", "dsc_banco", "");
                            unit.Proveedores.CargaCombosLookUp("Moneda", lkpTipoMonedaCTSInfoBancaria, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("SistPension", lkpSistPensionarioInfoBancaria, "cod_sist_pension", "dsc_sist_pension", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("NombreAFP", lkpNombreAFPInfoBancaria, "cod_APF", "dsc_APF", "", valorDefecto: true);
                            ObtenerDatos_InfoBancaria();
                            InfoBancaria = 1;
                        }
                        break;
                    case "actelDatosAdicionales":
                        navframeTrabajador.SelectedPage = navpageDatosAdicionales;
                        if (DatosAdicionales == 0)
                        {
                            unit.Trabajador.CargaCombosLookUp("TallasUniforme", lkpPoloTallaUnif, "cod_tallauniforme", "dsc_tallauniforme", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TallasUniforme", lkpCasacaTallaUnif, "cod_tallauniforme", "dsc_tallauniforme", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TallasUniforme", lkpCamisaTallaUnif, "cod_tallauniforme", "dsc_tallauniforme", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TallasUniforme", lkpMamelucoTallaUnif, "cod_tallauniforme", "dsc_tallauniforme", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TallasUniforme", lkpPantalonTallaUnif, "cod_tallauniforme", "dsc_tallauniforme", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TallasUniforme", lkpChalecoTallaUnif, "cod_tallauniforme", "dsc_tallauniforme", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TallasUniforme", lkpCascoTallaUnif, "cod_tallauniforme", "dsc_tallauniforme", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TallasUniforme", lkpFajaTallaUnif, "cod_tallauniforme", "dsc_tallauniforme", "", valorDefecto: true);
                            ObtenerDatos_CaracteristicasTallas();
                            DatosAdicionales = 1;
                        }
                        break;
                    case "actelInfoFamiliar":
                        navframeTrabajador.SelectedPage = navpageInfoFamiliar;
                        if (InfoFamiliar == 0)
                        {
                            unit.Trabajador.CargaCombosLookUp("Parentesco", lkpParentescoInfoFamiliar, "cod_parentesco", "dsc_parentesco", "", valorDefecto: true);
                            CargarCombosGridLookup("TipoDocumento", glkpTipoDocumentoInfoFamiliar, "cod_tipo_documento", "dsc_tipo_documento", "", valorDefecto: true);
                            unit.Clientes.CargaCombosLookUp("TipoPais", lkpPaisInfoFamiliar, "cod_pais", "dsc_pais", "");
                            unit.Clientes.CargaCombosLookUp("TipoDepartamento", lkpDepartamentoInfoFamiliar, "cod_departamento", "dsc_departamento", "");
                            unit.Clientes.CargaCombosLookUp("TipoProvincia", lkpProvinciaInfoFamiliar, "cod_provincia", "dsc_provincia", "");
                            CargarCombosGridLookup("TipoDistrito", glkpDistritoInfoFamiliar, "cod_distrito", "dsc_distrito", "");
                            //dtFecNacimientoInfoFamiliar.EditValue = DateTime.Today;
                            ObtenerDatos_InfoFamiliar();
                            gvListadoInfoFamiliar_FocusedRowChanged(gvListadoInfoFamiliar, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
                            InfoFamiliar = 1;
                        }
                        break;
                    case "actelInfoEconomica":
                        navframeTrabajador.SelectedPage = navpageInfoEconomica;
                        if (InfoEconomica == 0)
                        {
                            unit.Trabajador.CargaCombosLookUp("TipoPropiedad", lkpViviendaInfoEconomica, "cod_tipopropiedad", "dsc_tipopropiedad", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TipoVivienda", lkpTipoViviendaInfoEconomica, "cod_tipovivienda", "dsc_tipovivienda", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TipoPropiedad", lkpVehiculoInfoEconomica, "cod_tipopropiedad", "dsc_tipopropiedad", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TipoPropiedad", lkpEmpresaInfoEconomica, "cod_tipopropiedad", "dsc_tipopropiedad", "", valorDefecto: true);
                            unit.Clientes.CargaCombosLookUp("TipoPais", lkpPaisViviendaInfoEconomica, "cod_pais", "dsc_pais", "");
                            unit.Clientes.CargaCombosLookUp("TipoDepartamento", lkpDepartamentoViviendaInfoEconomica, "cod_departamento", "dsc_departamento", "");
                            unit.Clientes.CargaCombosLookUp("TipoProvincia", lkpProvinciaViviendaInfoEconomica, "cod_provincia", "dsc_provincia", "");
                            CargarCombosGridLookup("TipoDistrito", glkpDistritoViviendaInfoEconomica, "cod_distrito", "dsc_distrito", "");
                            unit.Clientes.CargaCombosLookUp("TipoPais", lkpPaisEmpresaInfoEconomica, "cod_pais", "dsc_pais", "");
                            unit.Clientes.CargaCombosLookUp("TipoDepartamento", lkpDepartamentoEmpresaInfoEconomica, "cod_departamento", "dsc_departamento", "");
                            unit.Clientes.CargaCombosLookUp("TipoProvincia", lkpProvinciaEmpresaInfoEconomica, "cod_provincia", "dsc_provincia", "");
                            CargarCombosGridLookup("TipoDistrito", glkpDistritoEmpresaInfoEconomica, "cod_distrito", "dsc_distrito", "");
                            unit.Proveedores.CargaCombosLookUp("Moneda", lkpTipoMonedaViviendaInfoEconomica, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                            unit.Proveedores.CargaCombosLookUp("Moneda", lkpTipoMonedaVehiculoInfoEconomica, "cod_moneda", "dsc_moneda", "", valorDefecto: true);
                            ObtenerDatos_InfoEconomica();
                            InfoEconomica = 1;
                        }
                        break;
                    case "actelInfoAcademica":
                        navframeTrabajador.SelectedPage = navpageInfoAcademica;
                        if (InfoAcademica == 0)
                        {
                            unit.Trabajador.CargaCombosLookUp("NivelAcademico", lkpNivelAcademicoFormAcademic, "cod_nivelacademico", "dsc_nivelacademico", "", valorDefecto: true);
                            dtAnhoInicioFormAcademic.EditValue = DateTime.Today;
                            dtAnhoFinFormAcademic.EditValue = DateTime.Today;
                            ObtenerDatos_InfoAcademica();
                            gvListadoFormAcademica_FocusedRowChanged(gvListadoFormAcademica, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
                            InfoAcademica = 1;
                        }
                        break;
                    case "actelExpProfesional":
                        navframeTrabajador.SelectedPage = navpageExpProfesional;
                        if (ExpProfesional == 0)
                        {
                            dtFecInicioExpProfesional.EditValue = DateTime.Today;
                            dtFecFinExpProfesional.EditValue = DateTime.Today;
                            ObtenerDatos_InfoProfesional();
                            gvListadoExpProfesional_FocusedRowChanged(gvListadoExpProfesional, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
                            ExpProfesional = 1;
                        }
                        break;
                    case "actelInfoSalud":
                        navframeTrabajador.SelectedPage = navpageInfoSalud;
                        if (InfoSalud == 0)
                        {
                            unit.Trabajador.CargaCombosLookUp("GrupoSanguineo", lkpGrupoSanguineoInfoSalud, "cod_gruposanguineo", "dsc_gruposanguineo", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("EstadoSalud", lkpEstadoSaludInfoSalud, "cod_estadosalud", "dsc_estadosalud", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TipoSeguro", lkpSeguroSaludInfoSalud, "cod_tiposegurosalud", "dsc_tiposegurosalud", "", valorDefecto: true);
                            ObtenerDatos_InfoSalud();
                            ObtenerDatos_HistorialEMO();
                            InfoSalud = 1;
                        }
                        break;
                    case "actelInfoVivienda":
                        navframeTrabajador.SelectedPage = navpageInfoVivienda;
                        if (InfoVivienda == 0)
                        {
                            unit.Trabajador.CargaCombosLookUp("TipoPropiedad", lkpViviendaInfoVivienda, "cod_tipopropiedad", "dsc_tipopropiedad", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("TipoComodidad", lkpComodidadInfoVivienda, "cod_comodidad", "dsc_comodidad", "", valorDefecto: true);
                            unit.Trabajador.CargaCombosLookUp("Parentesco", lkpParentescoInfoVivienda, "cod_parentesco", "dsc_parentesco", "", valorDefecto: true);
                            ObtenerDatos_InfoVivienda();
                            InfoVivienda = 1;
                        }
                        break;
                }
                //ObtenerDatosConfiguracion(e.Element.Name.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void acctlMenu_StateChanged(object sender, EventArgs e)
        {
            try
            {
                if (acctlMenu.OptionsMinimizing.State == DevExpress.XtraBars.Navigation.AccordionControlState.Minimized)
                {
                    layoutControlItem1.Size = new Size(45, 559);
                }
                else
                {
                    layoutControlItem1.Size = new Size(182, 596);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void lkpSistPensionario_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpSistPensionarioInfoBancaria.EditValue == null) return;
            if (lkpSistPensionarioInfoBancaria.EditValue.ToString() != "AFP")
            {
                lkpNombreAFPInfoBancaria.EditValue = null; txtNroCUSPPInfoBancaria.Text = "";
                layoutControlItem18.Enabled = false;
                layoutControlItem20.Enabled = false;
            }
            else
            {
                layoutControlItem18.Enabled = true;
                layoutControlItem20.Enabled = true;
            }
        }

        private void glkpTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpTipoDocumento.EditValue != null)
            {
                eProveedor obj = new eProveedor();
                obj = unit.Proveedores.Validar_NroDocumento<eProveedor>(19, "", glkpTipoDocumento.EditValue.ToString());
                txtNroDocumento.Properties.MaxLength = obj.ctd_digitos;
            }
        }

        private void lkpPais_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            lkpDepartamento.Properties.DataSource = null;
            unit.Clientes.CargaCombosLookUp("TipoDepartamento", lkpDepartamento, "cod_departamento", "dsc_departamento", "", cod_condicion: lkpPais.EditValue == null ? "" : lkpPais.EditValue.ToString());
        }

        private void lkpDepartamento_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            lkpProvincia.Properties.DataSource = null;
            unit.Clientes.CargaCombosLookUp("TipoProvincia", lkpProvincia, "cod_provincia", "dsc_provincia", "", cod_condicion: lkpDepartamento.EditValue == null ? "" : lkpDepartamento.EditValue.ToString());
        }

        private void lkpProvincia_EditValueChanged(object sender, EventArgs e)
        {
            glkpDistrito.Properties.DataSource = null;
            //unit.Clientes.CargaCombosLookUp("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvincia.EditValue.ToString());
            CargarCombosGridLookup("TipoDistrito", glkpDistrito, "cod_distrito", "dsc_distrito", "", cod_condicion: lkpProvincia.EditValue == null ? "" : lkpProvincia.EditValue.ToString());
        }

        private void btnNuevoContacto_Click(object sender, EventArgs e)
        {
            txtCodContacto.Text = "0";
            txtApellPaternoContacto.Text = "";
            txtApellMaternoContacto.Text = "";
            txtNombreContacto.Text = "";
            lkpParentescoContacto.EditValue = null;
            glkpTipoDocumentoContacto.EditValue = "DI001";
            txtNroDocumentoContacto.Text = "";
            dtFecNacimientoContacto.EditValue = null;
            txtTelefonoContacto.Text = "";
            txtCelularContacto.Text = "";
            txtApellPaternoContacto.Select();
            sbtnVerDocumentos.Enabled = false;
        }

        private void btnGuardarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                txtApellPaternoContacto.Select();
                if (txtApellPaternoContacto.Text.Trim() == "") { MessageBox.Show("Debe ingresar un apellido paterno.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtApellPaternoContacto.Focus(); return; }
                //if (txtApellMaternoContacto.Text.Trim() == "") { MessageBox.Show("Debe ingresar un apellido materno.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtApellMaternoContacto.Focus(); return; }
                if (txtNombreContacto.Text.Trim() == "") { MessageBox.Show("Debe ingresar un nombre.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNombreContacto.Focus(); return; }
                if (lkpParentescoContacto.EditValue == null) { MessageBox.Show("Debe seleccionar el parentesco.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpParentescoContacto.Focus(); return; }
                //if (glkpTipoDocumentoContacto.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpTipoDocumentoContacto.Focus(); return; }
                //if (txtNroDocumentoContacto.Text.Trim() == "") { MessageBox.Show("Debe ingresar un número de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroDocumentoContacto.Focus(); return; }
                //if (dtFecNacimientoContacto.EditValue == null) { MessageBox.Show("Debe ingresar la fecha de nacimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecNacimientoContacto.Focus(); return; }
                if (txtTelefonoContacto.Text.Trim() == "" && txtCelularContacto.Text.Trim() == "") { MessageBox.Show("Debe ingresar el telefono o celular.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtTelefonoContacto.Focus(); return; }

                eTrabajador.eContactoEmergencia_Trabajador eContact = new eTrabajador.eContactoEmergencia_Trabajador();
                eContact = AsignarValores_Contatos();
                eContact = unit.Trabajador.InsertarActualizar_ContactoTrabajador<eTrabajador.eContactoEmergencia_Trabajador>(eContact);
                if (eContact == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                if (eContact != null)
                {
                    MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ObtenerDatos_ContactosEmergencia();
                    gvListadoContactos_FocusedRowChanged(gvListadoContactos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eTrabajador.eContactoEmergencia_Trabajador AsignarValores_Contatos()
        {
            eTrabajador.eContactoEmergencia_Trabajador obj = new eTrabajador.eContactoEmergencia_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_contactemerg = Convert.ToInt32(txtCodContacto.Text);
            obj.dsc_apellido_paterno = txtApellPaternoContacto.Text.Trim();
            obj.dsc_apellido_materno = txtApellMaternoContacto.Text.Trim();
            obj.dsc_nombres = txtNombreContacto.Text.Trim();
            obj.cod_parentesco = lkpParentescoContacto.EditValue.ToString();
            obj.cod_tipo_documento = glkpTipoDocumentoContacto.EditValue.ToString();
            obj.dsc_documento = txtNroDocumentoContacto.Text;
            obj.fch_nacimiento = Convert.ToDateTime(dtFecNacimientoContacto.EditValue);
            obj.dsc_telefono = txtTelefonoContacto.Text;
            obj.dsc_celular = txtCelularContacto.Text;
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            obj.flg_activo = "SI";

            return obj;
        }

        private void btnNuevaInfoLaboral_Click(object sender, EventArgs e)
        {
            txtCodInfoLaboral.Text = "0";
            dtFecIngresoInfoLaboral.EditValue = DateTime.Today;
            lkpAreaInfoLaboral.EditValue = null;
            lkpCargoInfoLaboral.EditValue = null;
            lkpPrefCECOInfoLaboral.EditValue = null;
            lkpTipoContratoInfoLaboral.EditValue = null;
            dtFecFirmaInfoLaboral.EditValue = DateTime.Today;
            dtFecVctoInfoLaboral.EditValue = DateTime.Today;
            lkpModalidadInfoLaboral.EditValue = null;
            txtMontoSueldoBaseInfoLaboral.EditValue = 0;
            txtMontoAsigFamiliarInfoLaboral.EditValue = 0;
            txtMontoMovilidadInfoLaboral.EditValue = 0;
            txtMontoAlimentacionInfoLaboral.EditValue = 0;
            txtMontoEscolaridadInfoLaboral.EditValue = 0;
            txtMontoBonoInfoLaboral.EditValue = 0;
            lkpSedeEmpresaInfoLaboral.EditValue = null;
            glkpBancoInfoBancaria.EditValue = null;
            lkpTipoMonedaInfoBancaria.EditValue = null;
            lkpTipoCuentaInfoBancaria.EditValue = null;
            txtNroCuentaBancariaInfoBancaria.Text = "";
            txtNroCuentaCCIInfoBancaria.Text = "";
        }

        private void btnGuardarInfoLaboral_Click(object sender, EventArgs e)
        {
            try
            {
                dtFecIngresoInfoLaboral.Select();
                if (dtFecIngresoInfoLaboral.EditValue == null) { MessageBox.Show("Debe ingresar la fecha de ingreso.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecIngresoInfoLaboral.Focus(); return; }
                if (lkpAreaInfoLaboral.EditValue == null) { MessageBox.Show("Debe seleccionar el área.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpAreaInfoLaboral.Focus(); return; }
                if (lkpCargoInfoLaboral.EditValue == null) { MessageBox.Show("Debe seleccionar el cargo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpCargoInfoLaboral.Focus(); return; }
                if (lkpPrefCECOInfoLaboral.EditValue == null) { MessageBox.Show("Debe seleccionar el prefijo CECO.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpPrefCECOInfoLaboral.Focus(); return; }
                if (lkpTipoContratoInfoLaboral.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de contrato.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoContratoInfoLaboral.Focus(); return; }
                //if (lkpModalidadInfoLaboral.EditValue == null) { MessageBox.Show("Debe seleccionar la modalidad.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpModalidadInfoLaboral.Focus(); return; }
                if (dtFecFirmaInfoLaboral.EditValue == null) { MessageBox.Show("Debe ingresar la fecha de firma del contrato.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecFirmaInfoLaboral.Focus(); return; }
                if (dtFecVctoDocumento.EditValue == null) { MessageBox.Show("Debe ingresar la fecha de vencimiento del contrato.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecVctoDocumento.Focus(); return; }
                if (Convert.ToDecimal(txtMontoSueldoBaseInfoLaboral.EditValue) == 0) { MessageBox.Show("Debe ingresar el sueldo base.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtMontoSueldoBaseInfoLaboral.Focus(); return; }
                if (lkpSedeEmpresaInfoLaboral.EditValue == null) { MessageBox.Show("Debe seleccionar una sede de la empresa.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpSedeEmpresaInfoLaboral.Focus(); return; }
                
                eTrabajador.eInfoLaboral_Trabajador eInfoLab = new eTrabajador.eInfoLaboral_Trabajador();
                eInfoLab = AsignarValores_InfoLaboral();
                eInfoLab = unit.Trabajador.InsertarActualizar_InfoLaboralTrabajador<eTrabajador.eInfoLaboral_Trabajador>(eInfoLab);
                if (eInfoLab == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (eInfoLab != null)
                {
                    MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ObtenerDatos_InfoLaboral();
                    gvListadoInfoLaboral_FocusedRowChanged(gvListadoInfoLaboral, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eTrabajador.eInfoLaboral_Trabajador AsignarValores_InfoLaboral()
        {
            eTrabajador.eInfoLaboral_Trabajador obj = new eTrabajador.eInfoLaboral_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_infolab = Convert.ToInt32(txtCodInfoLaboral.Text);
            obj.fch_ingreso = Convert.ToDateTime(dtFecIngresoInfoLaboral.EditValue);
            obj.cod_area = lkpAreaInfoLaboral.EditValue.ToString();
            obj.cod_cargo = lkpCargoInfoLaboral.EditValue.ToString();
            obj.dsc_pref_ceco = lkpPrefCECOInfoLaboral.EditValue.ToString();
            obj.cod_tipo_contrato = lkpTipoContratoInfoLaboral.EditValue.ToString();
            obj.fch_firma = Convert.ToDateTime(dtFecFirmaInfoLaboral.EditValue);
            obj.fch_vencimiento = Convert.ToDateTime(dtFecVctoInfoLaboral.EditValue);
            obj.cod_modalidad = lkpModalidadInfoLaboral.EditValue == null ? null : lkpModalidadInfoLaboral.EditValue.ToString();
            obj.imp_sueldo_base = Convert.ToDecimal(txtMontoSueldoBaseInfoLaboral.Text);
            obj.imp_asig_familiar = Convert.ToDecimal(txtMontoAsigFamiliarInfoLaboral.Text);
            obj.imp_movilidad = Convert.ToDecimal(txtMontoMovilidadInfoLaboral.Text);
            obj.imp_alimentacion = Convert.ToDecimal(txtMontoAlimentacionInfoLaboral.Text);
            obj.imp_escolaridad = Convert.ToDecimal(txtMontoEscolaridadInfoLaboral.Text);
            obj.imp_bono = Convert.ToDecimal(txtMontoBonoInfoLaboral.Text);
            obj.cod_sede_empresa = lkpSedeEmpresaInfoLaboral.EditValue.ToString();
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            obj.flg_activo = "SI";

            return obj;
        }

        private void gvListadoContactos_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0) gvListadoContactos_FocusedRowChanged(gvListadoContactos, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
        }

        private void gvListadoContactos_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoContactos_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoContactos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0) Obtener_ContactoEmergencia();
        }

        private void Obtener_ContactoEmergencia()
        {
            eTrabajador.eContactoEmergencia_Trabajador obj = new eTrabajador.eContactoEmergencia_Trabajador();
            obj = gvListadoContactos.GetFocusedRow() as eTrabajador.eContactoEmergencia_Trabajador;
            if (obj == null) return;
            txtCodContacto.Text = obj.cod_contactemerg.ToString();
            txtApellPaternoContacto.Text = obj.dsc_apellido_paterno;
            txtApellMaternoContacto.Text = obj.dsc_apellido_materno;
            txtNombreContacto.Text = obj.dsc_nombres;
            lkpParentescoContacto.EditValue = obj.cod_parentesco;
            glkpTipoDocumentoContacto.EditValue = obj.cod_tipo_documento;
            txtNroDocumentoContacto.Text = obj.dsc_documento;
            dtFecNacimientoContacto.EditValue = obj.fch_nacimiento;
            if (obj.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacimientoContacto.EditValue = null; }
            txtTelefonoContacto.Text = obj.dsc_telefono;
            txtCelularContacto.Text = obj.dsc_celular;
        }

        private void gvListadoInfoLaboral_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0) gvListadoInfoLaboral_FocusedRowChanged(gvListadoInfoLaboral, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
        }

        private void gvListadoInfoLaboral_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoInfoLaboral_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoInfoLaboral_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0) Obtener_InfoLaboral();
        }

        private void Obtener_InfoLaboral()
        {
            eTrabajador.eInfoLaboral_Trabajador obj = new eTrabajador.eInfoLaboral_Trabajador();
            obj = gvListadoInfoLaboral.GetFocusedRow() as eTrabajador.eInfoLaboral_Trabajador;
            if (obj == null) return;
            txtCodInfoLaboral.Text = obj.cod_infolab.ToString();
            dtFecIngresoInfoLaboral.EditValue = obj.fch_ingreso;
            lkpSedeEmpresaInfoLaboral.EditValue = obj.cod_sede_empresa;
            lkpAreaInfoLaboral.EditValue = obj.cod_area;
            lkpCargoInfoLaboral.EditValue = obj.cod_cargo;
            lkpPrefCECOInfoLaboral.EditValue = obj.dsc_pref_ceco;
            lkpPrefCECOInfoLaboral.ToolTip = lkpPrefCECOInfoLaboral.Text;
            lkpTipoContratoInfoLaboral.EditValue = obj.cod_tipo_contrato;
            dtFecFirmaInfoLaboral.EditValue = obj.fch_firma;
            dtFecVctoInfoLaboral.EditValue = obj.fch_vencimiento;
            lkpModalidadInfoLaboral.EditValue = obj.cod_modalidad;
            txtMontoSueldoBaseInfoLaboral.EditValue = obj.imp_sueldo_base;
            txtMontoAsigFamiliarInfoLaboral.EditValue = obj.imp_asig_familiar;
            txtMontoMovilidadInfoLaboral.EditValue = obj.imp_movilidad;
            txtMontoAlimentacionInfoLaboral.EditValue = obj.imp_alimentacion;
            txtMontoEscolaridadInfoLaboral.EditValue = obj.imp_escolaridad;
            txtMontoBonoInfoLaboral.EditValue = obj.imp_bono;
            //glkpBancoInfoBancaria.EditValue = obj.cod_banco;
            //lkpTipoMonedaInfoBancaria.EditValue = obj.cod_moneda;
            //lkpTipoCuentaInfoBancaria.EditValue = obj.cod_tipo_cuenta;
            //txtNroCuentaBancariaInfoBancaria.Text = obj.dsc_cta_bancaria;
            //txtNroCuentaCCIInfoBancaria.Text = obj.dsc_cta_interbancaria;
        }

        private void gvListadoInfoFamiliar_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoInfoFamiliar_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoInfoFamiliar_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0) gvListadoInfoFamiliar_FocusedRowChanged(gvListadoInfoFamiliar, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
        }

        private void gvListadoInfoFamiliar_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0) Obtener_InfoFamiliar();
        }

        private void Obtener_InfoFamiliar()
        {
            eTrabajador.eInfoFamiliar_Trabajador obj = new eTrabajador.eInfoFamiliar_Trabajador();
            obj = gvListadoInfoFamiliar.GetFocusedRow() as eTrabajador.eInfoFamiliar_Trabajador;
            if (obj == null) return;
            txtCodInfoFamiliar.Text = obj.cod_infofamiliar.ToString();
            lkpParentescoInfoFamiliar.EditValue = obj.cod_parentesco;
            txtApellPaternoInfoFamiliar.Text = obj.dsc_apellido_paterno;
            txtApellMaternoInfoFamiliar.Text = obj.dsc_apellido_materno;
            txtNombreInfoFamiliar.Text = obj.dsc_nombres;
            dtFecNacimientoInfoFamiliar.EditValue = obj.fch_nacimiento;
            if (obj.fch_nacimiento.ToString().Contains("1/01/0001")) { dtFecNacimientoInfoFamiliar.EditValue = null; }
            chkflgVivoInfoFamiliar.CheckState = obj.flg_vivo == "SI" ? CheckState.Checked : CheckState.Unchecked;
            glkpTipoDocumentoInfoFamiliar.EditValue = obj.cod_tipo_documento;
            txtNroDocumentoInfoFamiliar.Text = obj.dsc_documento;
            txtEmailInfoFamiliar.Text = obj.dsc_mail;
            txtTelefonoInfoFamiliar.Text = obj.dsc_telefono;
            txtCelularInfoFamiliar.Text = obj.dsc_celular;
            txtProfesionInfoFamiliar.Text = obj.dsc_profesion;
            txtCentroLaboralInfoFamiliar.Text = obj.dsc_centrolaboral;
            txtGradoInstruccionInfoFamiliar.Text = obj.dsc_gradoinstruccion;
            txtOcupacionInfoFamiliar.Text = obj.dsc_ocupacion;
            txtDiscapacidadInfoFamiliar.Text = obj.dsc_discapacidad;
            txtDireccionInfoFamiliar.Text = obj.dsc_direccion;
            txtReferenciaInfoFamiliar.Text = obj.dsc_referencia;
            lkpPaisInfoFamiliar.EditValue = obj.cod_pais;
            lkpDepartamentoInfoFamiliar.EditValue = obj.cod_departamento;
            lkpProvinciaInfoFamiliar.EditValue = obj.cod_provincia;
            glkpDistritoInfoFamiliar.EditValue = obj.cod_distrito;
            chkflgEnfermedadInfoFamiliar.CheckState = obj.flg_enfermedad == "SI" ? CheckState.Checked : CheckState.Unchecked;
            txtEnfermedadInfoFamiliar.Text = obj.dsc_enfermedad;
            chkflgAdiccionInfoFamiliar.CheckState = obj.flg_adiccion == "SI" ? CheckState.Checked : CheckState.Unchecked;
            txtAdiccionInfoFamiliar.Text = obj.dsc_adiccion;
            chkflgDependeEconomiaInfoFamiliar.CheckState = obj.flg_dependenciaeconomica == "SI" ? CheckState.Checked : CheckState.Unchecked;
        }

        private void gvListadoFormAcademica_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoFormAcademica_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoFormAcademica_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0) gvListadoFormAcademica_FocusedRowChanged(gvListadoFormAcademica, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
        }

        private void gvListadoFormAcademica_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0) Obtener_FormAcademica();
        }

        private void Obtener_FormAcademica()
        {
            eTrabajador.eInfoAcademica_Trabajador obj = new eTrabajador.eInfoAcademica_Trabajador();
            obj = gvListadoFormAcademica.GetFocusedRow() as eTrabajador.eInfoAcademica_Trabajador;
            if (obj == null) { hlinkAdjuntarCertificadoInfoAcademica.Enabled = false; return; }
            txtCodFormAcademic.Text = obj.cod_infoacademica.ToString();
            lkpNivelAcademicoFormAcademic.EditValue = obj.cod_nivelacademico;
            txtCentroEstudiosFormAcademic.Text = obj.dsc_centroestudios;
            txtLugarFormAcademic.Text = obj.dsc_lugar;
            txtCarreraCursoFormAcademic.Text = obj.dsc_carrera_curso;
            txtGradoTituloFormAcademic.Text = obj.dsc_grado;
            dtAnhoInicioFormAcademic.EditValue = new DateTime(obj.anho_inicio, 01, 01);
            dtAnhoFinFormAcademic.EditValue = new DateTime(obj.anho_fin, 01, 01);
            txtPromedioAcademicoFormAcademic.Text = obj.imp_promedio.ToString();
            hlinkAdjuntarCertificadoInfoAcademica.Enabled = true;
        }

        private void gvListadoExpProfesional_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoExpProfesional_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoExpProfesional_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0) gvListadoExpProfesional_FocusedRowChanged(gvListadoExpProfesional, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
        }

        private void gvListadoExpProfesional_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0) Obtener_ExpProfesional();
        }

        private void Obtener_ExpProfesional()
        {
            eTrabajador.eInfoProfesional_Trabajador obj = new eTrabajador.eInfoProfesional_Trabajador();
            obj = gvListadoExpProfesional.GetFocusedRow() as eTrabajador.eInfoProfesional_Trabajador;
            if (obj == null) { hlinkAdjuntarCertificadoInfoProfesional.Enabled = false; return; }
            txtCodExpProfesional.Text = obj.cod_infoprofesional.ToString();
            txtJefeInmediatoExpProfesional.Text = obj.dsc_nombre_jefe;
            txtRazonSocialExpProfesional.Text = obj.dsc_razon_social;
            txtCargoJefeExpProfesional.Text = obj.dsc_cargo_jefe;
            txtCargoExpProfesional.Text = obj.dsc_cargo;
            txtMotivoSalidaExpProfesional.Text = obj.dsc_motivo_salida;
            dtFecInicioExpProfesional.EditValue = obj.fch_inicio;
            if (obj.fch_inicio.ToString().Contains("1/01/0001")) { dtFecInicioExpProfesional.EditValue = null; }
            dtFecFinExpProfesional.EditValue = obj.fch_fin;
            if (obj.fch_fin.ToString().Contains("1/01/0001")) { dtFecFinExpProfesional.EditValue = null; }
            txtCelularExpProfesional.Text = obj.dsc_celular;
            mmComentariosExpProfesional.Text = obj.dsc_comentarios;
            hlinkAdjuntarCertificadoInfoProfesional.Enabled = true;
        }

        private void btnGuardarTallaUnif_Click(object sender, EventArgs e)
        {
            try
            {
                txtEstaturaCaractFisica.Select();
                if (Convert.ToDecimal(txtEstaturaCaractFisica.EditValue) == 0) { MessageBox.Show("Debe ingresar la estatura.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEstaturaCaractFisica.Focus(); return; }
                if (Convert.ToDecimal(txtPesoCaractFisica.EditValue) == 0) { MessageBox.Show("Debe ingresar el peso.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPesoCaractFisica.Focus(); return; }
                eTrabajador.eCaractFisicas_Trabajador eCaractF = new eTrabajador.eCaractFisicas_Trabajador();
                eCaractF = AsignarValores_CaractFisicas();
                eCaractF = unit.Trabajador.InsertarActualizar_CaractFisicasTrabajador<eTrabajador.eCaractFisicas_Trabajador>(eCaractF);
                if (eCaractF == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                eTrabajador.eTallaUniforme_Trabajador eTallaU = new eTrabajador.eTallaUniforme_Trabajador();
                eTallaU = AsignarValores_TallaUniforme();
                eTallaU = unit.Trabajador.InsertarActualizar_TallaUniformesTrabajador<eTrabajador.eTallaUniforme_Trabajador>(eTallaU);
                if (eTallaU == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                if (eCaractF != null && eTallaU != null)
                {
                    MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ObtenerDatos_InfoLaboral();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eTrabajador.eCaractFisicas_Trabajador AsignarValores_CaractFisicas()
        {
            eTrabajador.eCaractFisicas_Trabajador obj = new eTrabajador.eCaractFisicas_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.dsc_estatura = Convert.ToDecimal(txtEstaturaCaractFisica.Text);
            obj.dsc_peso = Convert.ToDecimal(txtPesoCaractFisica.Text);
            obj.dsc_IMC = Convert.ToDecimal(txtIMCCaractFisica.Text);
            obj.flg_lentes = chkflgLentesCaractFisica.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private eTrabajador.eTallaUniforme_Trabajador AsignarValores_TallaUniforme()
        {
            eTrabajador.eTallaUniforme_Trabajador obj = new eTrabajador.eTallaUniforme_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_talla_polo = lkpPoloTallaUnif.EditValue == null ? null : lkpPoloTallaUnif.EditValue.ToString();
            obj.cod_talla_camisa = lkpCamisaTallaUnif.EditValue == null ? null : lkpCamisaTallaUnif.EditValue.ToString();
            obj.cod_talla_pantalon = lkpPantalonTallaUnif.EditValue == null ? null : lkpPantalonTallaUnif.EditValue.ToString();
            obj.cod_talla_casaca = lkpCasacaTallaUnif.EditValue == null ? null : lkpCasacaTallaUnif.EditValue.ToString();
            obj.cod_talla_mameluco = lkpMamelucoTallaUnif.EditValue == null ? null : lkpMamelucoTallaUnif.EditValue.ToString();
            obj.cod_talla_chaleco = lkpChalecoTallaUnif.EditValue == null ? null : lkpChalecoTallaUnif.EditValue.ToString();
            obj.cod_talla_calzado = txtCalzadoTallaUnif.EditValue.ToString();
            obj.cod_talla_casco = lkpCasacaTallaUnif.EditValue == null ? null : lkpCasacaTallaUnif.EditValue.ToString();
            obj.cod_talla_faja = lkpMamelucoTallaUnif.EditValue == null ? null : lkpMamelucoTallaUnif.EditValue.ToString();
            obj.dsc_casillero = Convert.ToInt32(txtCasilleroTallaUnif.Text);
            obj.flg_lentes = chkflgLentesTallaUnif.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private void btnNuevaInfoEconomica_Click(object sender, EventArgs e)
        {
            lkpViviendaInfoEconomica.EditValue = null;
            lkpTipoViviendaInfoEconomica.EditValue = null;
            txtValorRentaInfoEconomica.EditValue = 0;
            lkpTipoMonedaViviendaInfoEconomica.EditValue = "SOL";
            txtDireccionViviendaInfoEconomica.Text = "";
            txtReferenciaViviendaInfoEconomica.Text = "";
            lkpPaisViviendaInfoEconomica.EditValue = "00001"; 
            lkpDepartamentoViviendaInfoEconomica.EditValue = "00015"; 
            lkpProvinciaViviendaInfoEconomica.EditValue = "00128";
            glkpDistritoViviendaInfoEconomica.EditValue = null;
            lkpVehiculoInfoEconomica.EditValue = null;
            txtMarcaInfoEconomica.Text = "";
            txtModeloInfoEconomica.Text = "";
            txtPlacaVehiculoInfoEconomica.Text = "";
            txtValorComercialInfoEconomica.EditValue = 0;
            lkpTipoMonedaVehiculoInfoEconomica.EditValue = "SOL";
            lkpEmpresaInfoEconomica.EditValue = null;
            txtParticipacionInfoEconomica.Text = "";
            txtRUCEmpresaInfoEconomica.Text = "";
            txtGiroEmpresaInfoEconomica.Text = "";
            txtDireccionEmpresaInfoEconomica.Text = "";
            txtReferenciaEmpresaInfoEconomica.Text = "";
            lkpPaisEmpresaInfoEconomica.EditValue = "00001";
            lkpDepartamentoEmpresaInfoEconomica.EditValue = "00015";
            lkpProvinciaEmpresaInfoEconomica.EditValue = "00128";
            glkpDistritoEmpresaInfoEconomica.EditValue = null;
        }

        private void btnGuardarInfoEconomica_Click(object sender, EventArgs e)
        {
            try
            {
                txtIngresoMensualInfoEconomica.Select();
                eTrabajador.eInfoEconomica_Trabajador eInfoEco = new eTrabajador.eInfoEconomica_Trabajador();
                eInfoEco = AsignarValores_InfoEconomica();
                eInfoEco = unit.Trabajador.InsertarActualizar_InfoEconomicaTrabajador<eTrabajador.eInfoEconomica_Trabajador>(eInfoEco);
                 if (eInfoEco == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                if (eInfoEco != null)
                {
                    MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ObtenerDatos_InfoEconomica();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eTrabajador.eInfoEconomica_Trabajador AsignarValores_InfoEconomica()
        {
            eTrabajador.eInfoEconomica_Trabajador obj = new eTrabajador.eInfoEconomica_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_infoeconomica = Convert.ToInt32(txtCodInfoEconomica.Text);
            obj.imp_ingresomensual = Convert.ToDecimal(txtIngresoMensualInfoEconomica.EditValue);
            obj.imp_gastomensual = Convert.ToDecimal(txtGastoMensualInfoEconomica.EditValue);
            obj.imp_totalactivos = Convert.ToDecimal(txtValorActivoInfoEconomica.EditValue);
            obj.imp_totaldeudas = Convert.ToDecimal(txtValorDeudaInfoEconomica.EditValue);
            obj.cod_vivienda = lkpViviendaInfoEconomica.EditValue == null ? null : lkpViviendaInfoEconomica.EditValue.ToString();
            obj.cod_tipovivienda = lkpTipoViviendaInfoEconomica.EditValue == null ? null : lkpTipoViviendaInfoEconomica.EditValue.ToString();
            obj.imp_valorvivienda = txtValorRentaInfoEconomica.EditValue.ToString();
            obj.cod_monedavivienda = lkpTipoMonedaViviendaInfoEconomica.EditValue.ToString();
            obj.dsc_direccion_vivienda = txtDireccionViviendaInfoEconomica.Text;
            obj.dsc_referencia_vivienda = txtReferenciaViviendaInfoEconomica.Text;
            obj.cod_pais_vivienda = lkpPaisViviendaInfoEconomica.EditValue == null ? null : lkpPaisViviendaInfoEconomica.EditValue.ToString();
            obj.cod_departamento_vivienda = lkpDepartamentoViviendaInfoEconomica.EditValue == null ? null : lkpDepartamentoViviendaInfoEconomica.EditValue.ToString();
            obj.cod_provincia_vivienda = lkpProvinciaViviendaInfoEconomica.EditValue == null ? null : lkpProvinciaViviendaInfoEconomica.EditValue.ToString();
            obj.cod_distrito_vivienda = glkpDistritoViviendaInfoEconomica.EditValue == null ? null : glkpDistritoViviendaInfoEconomica.EditValue.ToString();
            obj.cod_tipovehiculo = lkpVehiculoInfoEconomica.EditValue == null ? null : lkpVehiculoInfoEconomica.EditValue.ToString();
            obj.dsc_marcavehiculo = txtMarcaInfoEconomica.Text;
            obj.dsc_modelovehiculo = txtModeloInfoEconomica.Text;
            obj.dsc_placavehiculo = txtPlacaVehiculoInfoEconomica.Text;
            obj.imp_valorvehiculo = txtValorComercialInfoEconomica.EditValue.ToString();
            obj.cod_monedavehiculo = lkpTipoMonedaVehiculoInfoEconomica.EditValue.ToString();
            obj.cod_tipoempresa = lkpEmpresaInfoEconomica.EditValue == null ? null : lkpEmpresaInfoEconomica.EditValue.ToString();
            obj.dsc_participacion_empresa = txtParticipacionInfoEconomica.Text;
            obj.dsc_RUC_empresa = txtRUCEmpresaInfoEconomica.Text;
            obj.dsc_giro_empresa = txtGiroEmpresaInfoEconomica.Text;
            obj.dsc_direccion_empresa = txtDireccionEmpresaInfoEconomica.Text;
            obj.dsc_referencia_empresa = txtReferenciaEmpresaInfoEconomica.Text;
            obj.cod_pais_empresa = lkpPaisEmpresaInfoEconomica.EditValue == null ? null : lkpPaisEmpresaInfoEconomica.EditValue.ToString();
            obj.cod_departamento_empresa = lkpDepartamentoEmpresaInfoEconomica.EditValue == null ? null : lkpDepartamentoEmpresaInfoEconomica.EditValue.ToString();
            obj.cod_provincia_empresa = lkpProvinciaEmpresaInfoEconomica.EditValue == null ? null : lkpProvinciaEmpresaInfoEconomica.EditValue.ToString();
            obj.cod_distrito_empresa = glkpDistritoEmpresaInfoEconomica.EditValue == null ? null : glkpDistritoEmpresaInfoEconomica.EditValue.ToString();
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private async void chkFlgDNI_Click(object sender, EventArgs e)
        {
            await AdjuntarDocumentosVarios(1, "Doc. Identidad");
        }

        private async void chkFlgCV_Click(object sender, EventArgs e)
        {
            await AdjuntarDocumentosVarios(2, "CV");
        }

        private async void chkFlgAntPoliciales_Click(object sender, EventArgs e)
        {
            await AdjuntarDocumentosVarios(3, "Antc. Policial");
        }

        private async void chkFlgAntPenales_Click(object sender, EventArgs e)
        {
            await AdjuntarDocumentosVarios(4, "Antc. Penal");
        }

        private async void chkFlgVerifDomiciliaria_Click(object sender, EventArgs e)
        {
            await AdjuntarDocumentosVarios(5, "Verif. Domiciliaria");
        }

        private async void hlinkAdjuntarCertificadoInfoAcademica_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCodFormAcademic.Text) != 0)
            {
                await AdjuntarDocumentosVarios(6, "Cert. Academico", txtCarreraCursoFormAcademic.Text);
                ObtenerDatos_InfoAcademica();
                gvListadoFormAcademica_FocusedRowChanged(gvListadoFormAcademica, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            }
        }

        private async void hlinkAdjuntarCertificadoInfoProfesional_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCodExpProfesional.Text) != 0)
            {
                await AdjuntarDocumentosVarios(7, "Exp. Laboral", txtRazonSocialExpProfesional.Text + " " + txtCargoExpProfesional.Text);
                ObtenerDatos_InfoProfesional();
                gvListadoExpProfesional_FocusedRowChanged(gvListadoExpProfesional, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            }
        }

        private async void btnAdjuntarEMOInfoSalud_Click(object sender, EventArgs e)
        {
            await AdjuntarDocumentosVarios(8, "EMO");
            ObtenerDatos_HistorialEMO();
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

        private async Task AdjuntarDocumentosVarios(int opcionDoc, string nombreDoc, string nombreDocAdicional = "")
        {
            try
            {
                DateTime FechaRegistro = DateTime.Today;
                string nombreCarpeta = "";

                eTrabajador resultado = unit.Trabajador.Obtener_Trabajador<eTrabajador>(2, cod_trabajador, cod_empresa);
                if (resultado == null) { MessageBox.Show("Antes de adjuntar los docuentos debe crear al trabajador.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                OpenFileDialog myFileDialog = new OpenFileDialog();
                myFileDialog.Filter = "Archivos (*.pdf)|; *.pdf";
                myFileDialog.FilterIndex = 1;
                myFileDialog.InitialDirectory = "C:\\";
                myFileDialog.Title = "Abrir archivo";
                myFileDialog.CheckFileExists = false;
                myFileDialog.Multiselect = false;

                DialogResult result = myFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string IdCarpetaTrabajador = "", Extension = "";
                    var idArchivoPDF = "";
                    var TamañoDoc = new FileInfo(myFileDialog.FileName).Length / 1024;
                    if (TamañoDoc < 4000)
                    {
                        varPathOrigen = myFileDialog.FileName;
                        //varNombreArchivo = nombreDoc + "-" + FechaRegistro.ToString("yyyyMMdd") + "."+ Path.GetExtension(myFileDialog.SafeFileName);
                        varNombreArchivo = nombreDoc + "-" + FechaRegistro.Year.ToString() + "." + FechaRegistro.ToString("MM") + "." + FechaRegistro.ToString("dd") + (nombreDocAdicional != "" ? " " + nombreDocAdicional : "") + Path.GetExtension(myFileDialog.SafeFileName);
                        //varNombreArchivoSinExtension = nombreDoc + "-" + FechaRegistro.ToString("yyyyMMdd");
                        Extension = Path.GetExtension(myFileDialog.SafeFileName);
                    }
                    else
                    {
                        MessageBox.Show("Solo puede subir archivos hasta 4MB de tamaño", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");
                    eEmpresa eEmp = unit.Trabajador.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
                    if (eEmp.ClientIdOnedrive == null || eEmp.ClientIdOnedrive == "")
                    { MessageBox.Show("Debe configurar los datos del Onedrive de la empresa asignada", "Onedrive", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    ClientId = eEmp.ClientIdOnedrive;
                    TenantId = eEmp.TenantOnedrive;
                    Appl();
                    var app = PublicClientApp;
                    string correo = eEmp.UsuarioOnedrivePersonal;
                    string password = eEmp.ClaveOnedrivePersonal;

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

                    eEmpresa.eOnedrive_Empresa eDatos = new eEmpresa.eOnedrive_Empresa();
                    eDatos = unit.Trabajador.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, cod_empresa, dsc_Carpeta: "Personal");
                    var targetItemFolderId = eDatos.idCarpeta;

                    nombreCarpeta = resultado.dsc_documento + " - " + resultado.dsc_nombres_completos;
                    eTrabajador objCarpeta = unit.Trabajador.ObtenerDatosOneDrive<eTrabajador>(14, cod_trabajador: cod_trabajador);
                    if (objCarpeta.idCarpeta_Trabajador == null || objCarpeta.idCarpeta_Trabajador == "") //Si no existe folder lo crea
                    {
                        var driveItem = new Microsoft.Graph.DriveItem
                        {
                            Name = nombreCarpeta,
                            Folder = new Microsoft.Graph.Folder
                            {
                            },
                            AdditionalData = new Dictionary<string, object>()
                            {
                            {"@microsoft.graph.conflictBehavior", "rename"}
                            }
                        };

                        var driveItemInfo = await GraphClient.Me.Drive.Items[targetItemFolderId].Children.Request().AddAsync(driveItem);
                        IdCarpetaTrabajador = driveItemInfo.Id;
                    }
                    else //Si existe folder obtener id
                    {
                        IdCarpetaTrabajador = objCarpeta.idCarpeta_Trabajador;
                    }

                    //crea archivo en el OneDrive
                    byte[] data = System.IO.File.ReadAllBytes(varPathOrigen);
                    using (Stream stream = new MemoryStream(data))
                    {
                        string res = "";
                        var DriveItem = await GraphClient.Me.Drive.Items[IdCarpetaTrabajador].ItemWithPath(varNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                        idArchivoPDF = DriveItem.Id;
                        
                        eTrabajador objTrab = new eTrabajador();
                        objTrab.cod_trabajador = cod_trabajador;
                        objTrab.cod_empresa = cod_empresa;
                        objTrab.idCarpeta_Trabajador = IdCarpetaTrabajador;
                        objTrab.id_DNI = opcionDoc == 1 ? idArchivoPDF : null;
                        objTrab.id_CV = opcionDoc == 2 ? idArchivoPDF : null;
                        objTrab.id_AntPolicial = opcionDoc == 3 ? idArchivoPDF : null;
                        objTrab.id_AntPenal = opcionDoc == 4 ? idArchivoPDF : null;
                        objTrab.id_VerifDomiciliaria = opcionDoc == 5 ? idArchivoPDF : null;

                        chkFlgDNI.CheckState = opcionDoc == 1 ? CheckState.Checked : chkFlgDNI.CheckState;
                        chkFlgCV.CheckState = opcionDoc == 2 ? CheckState.Checked : chkFlgCV.CheckState;
                        chkFlgAntPoliciales.CheckState = opcionDoc == 3 ? CheckState.Checked : chkFlgAntPoliciales.CheckState;
                        chkFlgAntPenales.CheckState = opcionDoc == 4 ? CheckState.Checked : chkFlgAntPenales.CheckState;
                        chkFlgVerifDomiciliaria.CheckState = opcionDoc == 5 ? CheckState.Checked : chkFlgVerifDomiciliaria.CheckState;

                        btnVerDocIdentidad.Enabled = opcionDoc == 1 ? true : btnVerDocIdentidad.Enabled;
                        btnVerCV.Enabled = opcionDoc == 2 ? true : btnVerCV.Enabled;
                        btnVerAntcPoliciales.Enabled = opcionDoc == 3 ? true : btnVerAntcPoliciales.Enabled;
                        btnVerAntcPenales.Enabled = opcionDoc == 4 ? true : btnVerAntcPenales.Enabled;
                        btnVerVerifDomiciliaria.Enabled = opcionDoc == 5 ? true : btnVerVerifDomiciliaria.Enabled;

                        if (opcionDoc <= 5) res = unit.Trabajador.ActualizarInformacionDocumentos("SI", opcionDoc, objTrab);

                        if (opcionDoc == 6)
                        {
                            eTrabajador.eInfoAcademica_Trabajador eAcad = new eTrabajador.eInfoAcademica_Trabajador();
                            eAcad.cod_trabajador = cod_trabajador; eAcad.cod_empresa = cod_empresa;
                            eAcad.cod_infoacademica = Convert.ToInt32(txtCodFormAcademic.Text);
                            eAcad.id_certificado = idArchivoPDF;
                            eAcad = unit.Trabajador.InsertarActualizar_InfoAcademicaTrabajador<eTrabajador.eInfoAcademica_Trabajador>(eAcad, "SI");
                            res = eAcad != null ? "OK" : "ERROR";
                        }
                        if (opcionDoc == 7)
                        {
                            eTrabajador.eInfoProfesional_Trabajador eProf = new eTrabajador.eInfoProfesional_Trabajador();
                            eProf.cod_trabajador = cod_trabajador; eProf.cod_empresa = cod_empresa;
                            eProf.cod_infoprofesional = Convert.ToInt32(txtCodExpProfesional.Text);
                            eProf.id_certificado = idArchivoPDF;
                            eProf = unit.Trabajador.InsertarActualizar_InfoProfesionalTrabajador<eTrabajador.eInfoProfesional_Trabajador>(eProf, "SI");
                            res = eProf != null ? "OK" : "ERROR";
                        }
                        if (opcionDoc == 8)
                        {
                            eTrabajador.eCertificadoEMO_Trabajador eEMO = new eTrabajador.eCertificadoEMO_Trabajador();
                            eEMO.cod_trabajador = cod_trabajador; eEMO.cod_empresa = cod_empresa; eEMO.cod_EMO = 0; eEMO.fch_registro = FechaRegistro;
                            eEMO.dsc_descripcion = "Cert. EMO del " + FechaRegistro.Year.ToString() + "." + FechaRegistro.ToString("MM") + "." + FechaRegistro.ToString("dd");
                            eEMO.dsc_anho = FechaRegistro.Year; eEMO.flg_certificado = "SI"; eEMO.id_certificado = idArchivoPDF;
                            eEMO = unit.Trabajador.InsertarActualizar_CertificadoEMOTrabajador<eTrabajador.eCertificadoEMO_Trabajador>(eEMO);
                            res = eEMO != null ? "OK" : "ERROR";
                        }

                        if (res == "OK")
                        {
                            MessageBox.Show("Se registró el documento satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Hubieron problemas al registrar el documento", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    SplashScreenManager.CloseForm();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtFecNacimientoInfoFamiliar_EditValueChanged(object sender, EventArgs e)
        {
            DateTime nacimiento = Convert.ToDateTime(dtFecNacimientoInfoFamiliar.EditValue); 
            int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            txtEdadInfoFamiliar.EditValue = edad;
        }

        private void btnVerDocIdentidad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VerDocumentos(1, "Doc. Identidad");
        }

        private void btnVerCV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VerDocumentos(2, "CV");
        }

        private void btnVerAntcPoliciales_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VerDocumentos(3, "Antc. Policial");
        }

        private void btnVerAntcPenales_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VerDocumentos(4, "Antc. Penal");
        }

        private void btnVerVerifDomiciliaria_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VerDocumentos(5, "Verif. Domiciliaria");
        }

        private void txtEstaturaCaractFisica_EditValueChanged(object sender, EventArgs e)
        {
            decimal peso = 0, estatura = 0;
            estatura = Convert.ToDecimal(Math.Pow(Convert.ToDouble(txtEstaturaCaractFisica.EditValue), 2));
            peso = Convert.ToDecimal(txtPesoCaractFisica.EditValue);
            if (estatura > 0) txtIMCCaractFisica.EditValue = Math.Round((peso / estatura), 2);
        }

        private void btnGuardarInfoSalud_Click(object sender, EventArgs e)
        {
            eTrabajador.eInfoSalud_Trabajador obj = AsignarValores_InfoSalud();
            obj = unit.Trabajador.InsertarActualizar_InfoSaludTrabajador<eTrabajador.eInfoSalud_Trabajador>(obj);
            if (obj == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (obj != null)
            {
                MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerDatos_InfoSalud();
            }
        }

        private eTrabajador.eInfoSalud_Trabajador AsignarValores_InfoSalud()
        {
            eTrabajador.eInfoSalud_Trabajador obj = new eTrabajador.eInfoSalud_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.flg_alergias = chkflgAlergiasInfoSalud.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_alergias = mmAlergias.Text;
            obj.flg_operaciones = chkflgOperacionesInfoSalud.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_operaciones = mmOperaciones.Text;
            obj.flg_enfprexistente = chkflgEnfPrexistenteInfoSalud.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_enfprexistente = mmEnfPrexistente.Text;
            obj.flg_tratprexistente = chkflgTratamientoInfoSalud.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_tratprexistente = mmTratamiento.Text;
            obj.flg_enfactual = chkflgEnfActualInfoSalud.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_enfactual = mmEnfActualidad.Text;
            obj.flg_tratactual = chkflgTratActualInfoSalud.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_tratactual = mmTratActual.Text;
            obj.flg_discapacidad  = chkflgDiscapacidadInfoSalud.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_discapacidad = mmDiscapacidad.Text;
            obj.flg_otros = chkflgOtrosInfoSalud.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_otros = mmOtros.Text;
            obj.dsc_gruposanguineo = lkpGrupoSanguineoInfoSalud.EditValue == null ? null : lkpGrupoSanguineoInfoSalud.EditValue.ToString();
            obj.dsc_estadosalud = lkpEstadoSaludInfoSalud.EditValue == null ? null : lkpEstadoSaludInfoSalud.EditValue.ToString();
            obj.dsc_segurosalud = lkpSeguroSaludInfoSalud.EditValue == null ? null : lkpSeguroSaludInfoSalud.EditValue.ToString();
            obj.dsc_tiposegurosalud = txtEspecificarInfoSalud.Text;
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private void btnGuardarInfoFamiliar_Click(object sender, EventArgs e)
        {
            txtApellPaternoInfoFamiliar.Select();
            if (lkpParentescoInfoFamiliar.EditValue == null) { MessageBox.Show("Debe seleccionar el parentesco.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpParentescoInfoFamiliar.Focus(); return; }
            if (dtFecNacimientoInfoFamiliar.EditValue == null) { MessageBox.Show("Debe ingresar la fecha de nacimiento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecNacimientoInfoFamiliar.Focus(); return; }
            if (txtApellPaternoInfoFamiliar.Text.Trim() == "") { MessageBox.Show("Debe ingresar el apellido paterno.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtApellPaternoInfoFamiliar.Focus(); return; }
            if (txtApellMaternoInfoFamiliar.Text.Trim() == "") { MessageBox.Show("Debe ingresar el apellido materno.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtApellMaternoInfoFamiliar.Focus(); return; }
            if (txtNombreInfoFamiliar.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNombreInfoFamiliar.Focus(); return; }
            //if (glkpTipoDocumentoInfoFamiliar.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpTipoDocumentoInfoFamiliar.Focus(); return; }
            //if (txtNroDocumentoInfoFamiliar.Text.Trim() == "") { MessageBox.Show("Debe ingresar el número de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroDocumentoInfoFamiliar.Focus(); return; }

            eTrabajador.eInfoFamiliar_Trabajador obj = AsignarValores_InfoFamiliar();
            obj = unit.Trabajador.InsertarActualizar_InfoFamiliarTrabajador<eTrabajador.eInfoFamiliar_Trabajador>(obj);
            if (obj == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (obj != null)
            {
                MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerDatos_InfoFamiliar();
                gvListadoInfoFamiliar_FocusedRowChanged(gvListadoInfoFamiliar, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            }
        }

        private eTrabajador.eInfoFamiliar_Trabajador AsignarValores_InfoFamiliar()
        {
            eTrabajador.eInfoFamiliar_Trabajador obj = new eTrabajador.eInfoFamiliar_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_infofamiliar = Convert.ToInt32(txtCodInfoFamiliar.Text);
            obj.cod_parentesco = lkpParentescoInfoFamiliar.EditValue.ToString();
            obj.dsc_apellido_paterno = txtApellPaternoInfoFamiliar.Text.Trim();
            obj.dsc_apellido_materno = txtApellMaternoInfoFamiliar.Text.Trim();
            obj.dsc_nombres = txtNombreInfoFamiliar.Text.Trim();
            obj.fch_nacimiento = dtFecNacimientoInfoFamiliar.EditValue == null ? new DateTime() : Convert.ToDateTime(dtFecNacimientoInfoFamiliar.EditValue);
            obj.flg_vivo = chkflgVivoInfoFamiliar.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.cod_tipo_documento = glkpTipoDocumentoInfoFamiliar.EditValue == null ? null : glkpTipoDocumentoInfoFamiliar.EditValue.ToString();
            obj.dsc_documento = txtNroDocumentoInfoFamiliar.Text;
            obj.dsc_mail = txtEmailInfoFamiliar.Text;
            obj.dsc_telefono = txtTelefonoInfoFamiliar.Text;
            obj.dsc_celular = txtCelularInfoFamiliar.Text;
            obj.dsc_profesion = txtProfesionInfoFamiliar.Text;
            obj.dsc_centrolaboral = txtCentroLaboralInfoFamiliar.Text;
            obj.dsc_gradoinstruccion = txtGradoInstruccionInfoFamiliar.Text;
            obj.dsc_ocupacion = txtOcupacionInfoFamiliar.Text;
            obj.dsc_discapacidad = txtDiscapacidadInfoFamiliar.Text;
            obj.dsc_direccion = txtDireccionInfoFamiliar.Text;
            obj.dsc_referencia = txtReferenciaInfoFamiliar.Text;
            obj.cod_pais = lkpPaisInfoFamiliar.EditValue == null ? null : lkpPaisInfoFamiliar.EditValue.ToString();
            obj.cod_departamento = lkpDepartamentoInfoFamiliar.EditValue == null ? null : lkpDepartamentoInfoFamiliar.EditValue.ToString();
            obj.cod_provincia = lkpProvinciaInfoFamiliar.EditValue == null ? null : lkpProvinciaInfoFamiliar.EditValue.ToString();
            obj.cod_distrito = glkpDistritoInfoFamiliar.EditValue == null ? null : glkpDistritoInfoFamiliar.EditValue.ToString();
            obj.flg_enfermedad = chkflgEnfermedadInfoFamiliar.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_enfermedad = txtEnfermedadInfoFamiliar.Text;
            obj.flg_adiccion = chkflgAdiccionInfoFamiliar.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_adiccion = txtAdiccionInfoFamiliar.Text;
            obj.flg_dependenciaeconomica = chkflgDependeEconomiaInfoFamiliar.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private void btnNuevaInfoFamiliar_Click(object sender, EventArgs e)
        {
            txtCodInfoFamiliar.Text = "0";
            lkpParentescoInfoFamiliar.Text = null;
            txtApellPaternoInfoFamiliar.Text = "";
            txtApellMaternoInfoFamiliar.Text = "";
            txtNombreInfoFamiliar.Text = "";
            dtFecNacimientoInfoFamiliar.EditValue = null;
            chkflgVivoInfoFamiliar.CheckState = CheckState.Checked;
            glkpTipoDocumentoInfoFamiliar.EditValue = null;
            txtNroDocumentoInfoFamiliar.Text = "";
            txtEmailInfoFamiliar.Text = "";
            txtTelefonoInfoFamiliar.Text = "";
            txtCelularInfoFamiliar.Text = "";
            txtProfesionInfoFamiliar.Text = "";
            txtCentroLaboralInfoFamiliar.Text = "";
            txtGradoInstruccionInfoFamiliar.Text = "";
            txtOcupacionInfoFamiliar.Text = "";
            txtDiscapacidadInfoFamiliar.Text = "";
            txtDireccionInfoFamiliar.Text = "";
            txtReferenciaInfoFamiliar.Text = "";
            lkpPaisInfoFamiliar.EditValue = "00001"; 
            lkpDepartamentoInfoFamiliar.EditValue = "00015";
            lkpProvinciaInfoFamiliar.EditValue = "00128";
            glkpDistritoInfoFamiliar.EditValue = null;
            chkflgEnfermedadInfoFamiliar.CheckState = CheckState.Unchecked;
            txtEnfermedadInfoFamiliar.Text = "";
            chkflgAdiccionInfoFamiliar.CheckState = CheckState.Unchecked;
            txtAdiccionInfoFamiliar.Text = "";
            chkflgDependeEconomiaInfoFamiliar.CheckState = CheckState.Unchecked;
        }

        private void btnGuardarFormAcademic_Click(object sender, EventArgs e)
        {
            if (lkpNivelAcademicoFormAcademic.EditValue == null) { MessageBox.Show("Debe seleccionar el nivel académico.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpNivelAcademicoFormAcademic.Focus(); return; }
            if (txtCentroEstudiosFormAcademic.Text.Trim() == "") { MessageBox.Show("Debe ingresar el centro de estudios.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCentroEstudiosFormAcademic.Focus(); return; }
            if (txtCarreraCursoFormAcademic.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre de la carrera o curso.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCarreraCursoFormAcademic.Focus(); return; }
            if (dtAnhoInicioFormAcademic.EditValue == null) { MessageBox.Show("Debe seleccionar el año de inicio.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtAnhoInicioFormAcademic.Focus(); return; }
            if (dtAnhoFinFormAcademic.EditValue == null) { MessageBox.Show("Debe seleccionar el año de fin.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtAnhoFinFormAcademic.Focus(); return; }

            eTrabajador.eInfoAcademica_Trabajador obj = AsignarValores_InfoAcademica();
            obj = unit.Trabajador.InsertarActualizar_InfoAcademicaTrabajador<eTrabajador.eInfoAcademica_Trabajador>(obj);
            if (obj == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (obj != null)
            {
                MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerDatos_InfoAcademica();
                gvListadoFormAcademica_FocusedRowChanged(gvListadoFormAcademica, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            }
        }

        private eTrabajador.eInfoAcademica_Trabajador AsignarValores_InfoAcademica()
        {
            eTrabajador.eInfoAcademica_Trabajador obj = new eTrabajador.eInfoAcademica_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_infoacademica = Convert.ToInt32(txtCodFormAcademic.Text);
            obj.cod_nivelacademico = lkpNivelAcademicoFormAcademic.EditValue == null ? null : lkpNivelAcademicoFormAcademic.EditValue.ToString();
            obj.dsc_centroestudios = txtCentroEstudiosFormAcademic.Text;
            obj.dsc_lugar = txtLugarFormAcademic.Text;
            obj.dsc_carrera_curso = txtCarreraCursoFormAcademic.Text;
            obj.dsc_grado = txtGradoTituloFormAcademic.Text;
            obj.anho_inicio = Convert.ToDateTime(dtAnhoInicioFormAcademic.EditValue).Year;
            obj.anho_fin = Convert.ToDateTime(dtAnhoFinFormAcademic.EditValue).Year;
            obj.imp_promedio = Convert.ToDecimal(txtPromedioAcademicoFormAcademic.Text);
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private void btnNuevaFormAcademic_Click(object sender, EventArgs e)
        {
            txtCodFormAcademic.Text = "0";
            hlinkAdjuntarCertificadoInfoAcademica.Enabled = false;
            lkpNivelAcademicoFormAcademic.EditValue = null;
            txtCentroEstudiosFormAcademic.Text = "";
            txtLugarFormAcademic.Text = "";
            txtCarreraCursoFormAcademic.Text = "";
            txtGradoTituloFormAcademic.Text = "";
            dtAnhoInicioFormAcademic.EditValue = DateTime.Today;
            dtAnhoFinFormAcademic.EditValue = DateTime.Today;
            txtPromedioAcademicoFormAcademic.Text = "";
        }

        private void btnGuardarExpProfesional_Click(object sender, EventArgs e)
        {
            if (txtRazonSocialExpProfesional.Text.Trim() == "") { MessageBox.Show("Debe ingresar la razón social.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtRazonSocialExpProfesional.Focus(); return; }
            if (txtCargoExpProfesional.Text.Trim() == "") { MessageBox.Show("Debe ingresar el cargo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCargoExpProfesional.Focus(); return; }
            if (txtJefeInmediatoExpProfesional.Text.Trim() == "") { MessageBox.Show("Debe ingresar el nombre del jefe.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtJefeInmediatoExpProfesional.Focus(); return; }
            if (txtCargoJefeExpProfesional.Text.Trim() == "") { MessageBox.Show("Debe ingresar el cargo del jefe.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtCargoJefeExpProfesional.Focus(); return; }
            if (txtMotivoSalidaExpProfesional.Text.Trim() == "") { MessageBox.Show("Debe ingresar el motivo de salida.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtMotivoSalidaExpProfesional.Focus(); return; }
            if (dtFecInicioExpProfesional.EditValue == null) { MessageBox.Show("Debe seleccionar la fecha de inicio.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecInicioExpProfesional.Focus(); return; }
            if (dtFecFinExpProfesional.EditValue == null) { MessageBox.Show("Debe seleccionar la fecha de inicio.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecFinExpProfesional.Focus(); return; }

            eTrabajador.eInfoProfesional_Trabajador obj = AsignarValores_InfoProfesional();
            obj = unit.Trabajador.InsertarActualizar_InfoProfesionalTrabajador<eTrabajador.eInfoProfesional_Trabajador>(obj, "NO");
            if (obj == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (obj != null)
            {
                MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerDatos_InfoProfesional();
                gvListadoExpProfesional_FocusedRowChanged(gvListadoExpProfesional, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            }
        }

        private eTrabajador.eInfoProfesional_Trabajador AsignarValores_InfoProfesional()
        {
            eTrabajador.eInfoProfesional_Trabajador obj = new eTrabajador.eInfoProfesional_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_infoprofesional = Convert.ToInt32(txtCodExpProfesional.Text);
            obj.dsc_nombre_jefe = txtJefeInmediatoExpProfesional.Text;
            obj.dsc_razon_social = txtRazonSocialExpProfesional.Text;
            obj.dsc_cargo_jefe = txtCargoJefeExpProfesional.Text;
            obj.dsc_cargo = txtCargoExpProfesional.Text;
            obj.dsc_motivo_salida = txtMotivoSalidaExpProfesional.Text;
            obj.fch_inicio = Convert.ToDateTime(dtFecInicioExpProfesional.EditValue);
            obj.fch_fin = Convert.ToDateTime(dtFecFinExpProfesional.EditValue);
            obj.dsc_celular = txtCelularExpProfesional.Text;
            obj.dsc_comentarios = mmComentariosExpProfesional.Text;
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private void btnNuevaExpProfesional_Click(object sender, EventArgs e)
        {
            txtCodExpProfesional.Text = "0";
            hlinkAdjuntarCertificadoInfoProfesional.Enabled = false;
            txtJefeInmediatoExpProfesional.Text = "";
            txtRazonSocialExpProfesional.Text = "";
            txtCargoJefeExpProfesional.Text = "";
            txtCargoExpProfesional.Text = "";
            txtMotivoSalidaExpProfesional.Text = "";
            dtFecInicioExpProfesional.EditValue = DateTime.Today;
            dtFecFinExpProfesional.EditValue = DateTime.Today;
            txtCelularExpProfesional.Text = "";
            mmComentariosExpProfesional.Text = "";
        }

        private void btnGuardarInfoVivienda_Click(object sender, EventArgs e)
        {
            if (lkpViviendaInfoVivienda.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de vivienda.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpViviendaInfoVivienda.Focus(); return; }
            if (lkpComodidadInfoVivienda.EditValue == null) { MessageBox.Show("Debe seleccionar el tipo de comodidad.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpComodidadInfoVivienda.Focus(); return; }
            
            eTrabajador.eInfoVivienda_Trabajador obj = AsignarValores_InfoVivienda();
            obj = unit.Trabajador.InsertarActualizar_InfoViviendaTrabajador<eTrabajador.eInfoVivienda_Trabajador>(obj);
            if (obj == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (obj != null)
            {
                MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerDatos_InfoVivienda();
            }
        }

        private eTrabajador.eInfoVivienda_Trabajador AsignarValores_InfoVivienda()
        {
            eTrabajador.eInfoVivienda_Trabajador obj = new eTrabajador.eInfoVivienda_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_tipovivienda = lkpViviendaInfoVivienda.EditValue == null ? null : lkpViviendaInfoVivienda.EditValue.ToString();
            obj.cod_tipocomodidad = lkpComodidadInfoVivienda.EditValue == null ? null : lkpComodidadInfoVivienda.EditValue.ToString();
            obj.flg_puertas = chkflgPuertasInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_ventanas = chkflgVentanasInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_techo = chkflgTechoInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_telefono = chkflgTelefonoInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_celulares = chkCelularesInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_internet_comunicacion = chkflgInternetComunicacionInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_luz = chkflgLuzInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_agua = chkflgAguaInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_desague = chkflgDesagueInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_gas = chkflgGasInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_cable = chkflgCableInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_internet_servicio = chkflgInternetServicioInfoVivienda.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_viaacceso  = mmViasAccesoInfoVivienda.Text;
            obj.dsc_iluminacion = mmIluminacionInfoVivienda.Text;
            obj.dsc_entorno = mmEntornoInfoVivienda.Text;
            obj.dsc_puestopolicial = mmPuestoPolicialInfoVivienda.Text;
            obj.dsc_nombre_familiar = txtNombreFamiliarInfoVivienda.Text;
            obj.dsc_horasencasa = txtHorasCasaInfoVivienda.Text;
            obj.cod_parentesco = lkpParentescoInfoVivienda.EditValue == null ? null : lkpParentescoInfoVivienda.EditValue.ToString();
            obj.dsc_celular = txtCelularInfoVivienda.Text;
            obj.dsc_mail = txtEmailInfoVivienda.Text;
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private void btnEliminarContacto_Click(object sender, EventArgs e)
        {
            string result = "";
            result = unit.Trabajador.EliminarInactivar_DatosTrabajador(1, cod_trabajador, Program.Sesion.Usuario.cod_usuario, cod_contactemerg: Convert.ToInt32(txtCodContacto.Text));
            if (result != "OK") { MessageBox.Show("Error al eliminar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (result == "OK") MessageBox.Show("Se procedió a eliminar el registro de manera satisfactoria.", "Eliminar registro", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            ObtenerDatos_ContactosEmergencia();
        }

        private void btnEliminarInfoLaboral_Click(object sender, EventArgs e)
        {
            string result = "";
            result = unit.Trabajador.EliminarInactivar_DatosTrabajador(2, cod_trabajador, Program.Sesion.Usuario.cod_usuario, cod_infolab: Convert.ToInt32(txtCodInfoLaboral.Text));
            if (result != "OK") { MessageBox.Show("Error al eliminar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (result == "OK") MessageBox.Show("Se procedió a eliminar el registro de manera satisfactoria.", "Eliminar registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ObtenerDatos_InfoLaboral();
        }

        private void btnEliminarInfoFamiliar_Click(object sender, EventArgs e)
        {
            string result = "";
            result = unit.Trabajador.EliminarInactivar_DatosTrabajador(3, cod_trabajador, Program.Sesion.Usuario.cod_usuario, cod_infofamiliar: Convert.ToInt32(txtCodInfoFamiliar.Text));
            if (result != "OK") { MessageBox.Show("Error al eliminar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (result == "OK") MessageBox.Show("Se procedió a eliminar el registro de manera satisfactoria.", "Eliminar registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ObtenerDatos_InfoFamiliar();
        }

        private void btnEliminarFormAcademic_Click(object sender, EventArgs e)
        {
            string result = "";
            result = unit.Trabajador.EliminarInactivar_DatosTrabajador(4, cod_trabajador, Program.Sesion.Usuario.cod_usuario, cod_infoacademica: Convert.ToInt32(txtCodFormAcademic.Text));
            if (result != "OK") { MessageBox.Show("Error al eliminar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (result == "OK") MessageBox.Show("Se procedió a eliminar el registro de manera satisfactoria.", "Eliminar registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ObtenerDatos_InfoAcademica();
        }

        private void btnEliminarExpProfesional_Click(object sender, EventArgs e)
        {
            string result = "";
            result = unit.Trabajador.EliminarInactivar_DatosTrabajador(5, cod_trabajador, Program.Sesion.Usuario.cod_usuario, cod_infoprofesional: Convert.ToInt32(txtCodExpProfesional.Text));
            if (result != "OK") { MessageBox.Show("Error al eliminar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (result == "OK") MessageBox.Show("Se procedió a eliminar el registro de manera satisfactoria.", "Eliminar registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ObtenerDatos_InfoProfesional();
        }

        private void btnEliminarInfoEconomica_Click(object sender, EventArgs e)
        {
            string result = "";
            result = unit.Trabajador.EliminarInactivar_DatosTrabajador(6, cod_trabajador, Program.Sesion.Usuario.cod_usuario, cod_infoeconomica: Convert.ToInt32(txtCodInfoEconomica.Text));
            if (result != "OK") { MessageBox.Show("Error al eliminar registro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (result == "OK") MessageBox.Show("Se procedió a eliminar el registro de manera satisfactoria.", "Eliminar registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ObtenerDatos_InfoEconomica();
        }

        private void btnGuardarInfoBancaria_Click(object sender, EventArgs e)
        {
            if (lkpSistPensionarioInfoBancaria.EditValue.ToString() == "AFP")
            {
                if (lkpNombreAFPInfoBancaria.EditValue == null) { MessageBox.Show("Debe seleccionar el nombre de la AFP.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpNombreAFPInfoBancaria.Focus(); return; }
            }
            if (glkpBancoInfoBancaria.EditValue == null) { MessageBox.Show("Debe seleccionar un banco.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpBancoInfoBancaria.Focus(); return; }
            if (lkpTipoMonedaInfoBancaria.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de moneda.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoMonedaInfoBancaria.Focus(); return; }
            if (lkpTipoCuentaInfoBancaria.EditValue == null) { MessageBox.Show("Debe seleccionar un tipo de cuenta.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpTipoCuentaInfoBancaria.Focus(); return; }
            if (txtNroCuentaBancariaInfoBancaria.Text.Trim() == "") { MessageBox.Show("Debe ingresar una cuenta bancaria.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroCuentaBancariaInfoBancaria.Focus(); return; }
            if (txtNroCuentaCCIInfoBancaria.Text.Trim() == "") { MessageBox.Show("Debe ingresar una cuenta interbancaria.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroCuentaCCIInfoBancaria.Focus(); return; }
            eTrabajador.eInfoBancaria_Trabajador eBanc = new eTrabajador.eInfoBancaria_Trabajador();
            eBanc = AsignarValores_InfoBancaria();
            eBanc = unit.Trabajador.InsertarActualizar_InfoBancariaTrabajador<eTrabajador.eInfoBancaria_Trabajador>(eBanc);
            if (eBanc == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (eBanc != null)
            {
                MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ObtenerDatos_InfoBancaria();
            }
        }

        private eTrabajador.eInfoBancaria_Trabajador AsignarValores_InfoBancaria()
        {
            eTrabajador.eInfoBancaria_Trabajador obj = new eTrabajador.eInfoBancaria_Trabajador();
            obj.cod_trabajador = cod_trabajador;
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_banco = glkpBancoInfoBancaria.EditValue == null ? null : glkpBancoInfoBancaria.EditValue.ToString();
            obj.cod_moneda = lkpTipoMonedaInfoBancaria.EditValue == null ? null : lkpTipoMonedaInfoBancaria.EditValue.ToString();
            obj.cod_tipo_cuenta = lkpTipoCuentaInfoBancaria.EditValue == null ? null : lkpTipoCuentaInfoBancaria.EditValue.ToString();
            obj.dsc_cta_bancaria = txtNroCuentaBancariaInfoBancaria.Text;
            obj.dsc_cta_interbancaria = txtNroCuentaCCIInfoBancaria.Text;
            obj.cod_banco_CTS = glkpBancoCTSInfoBancaria.EditValue == null ? null : glkpBancoCTSInfoBancaria.EditValue.ToString();
            obj.cod_moneda_CTS = lkpTipoMonedaCTSInfoBancaria.EditValue == null ? null : lkpTipoMonedaCTSInfoBancaria.EditValue.ToString();
            obj.dsc_cta_bancaria_CTS = txtNroCuentaBancariaCTSInfoBancaria.Text;
            obj.dsc_cta_interbancaria_CTS = txtNroCuentaCCICTSInfoBancaria.Text;
            obj.cod_sist_pension = lkpSistPensionarioInfoBancaria.EditValue == null ? null : lkpSistPensionarioInfoBancaria.EditValue.ToString();
            obj.cod_APF = lkpNombreAFPInfoBancaria.EditValue == null ? null : lkpNombreAFPInfoBancaria.EditValue.ToString();
            obj.cod_CUSPP = lkpNombreAFPInfoBancaria.EditValue == null ? null : txtNroCUSPPInfoBancaria.Text;
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;

            return obj;
        }

        private void picAnteriorTrabajador_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListadoTrabajador.RowCount - 1;
                int nRow = frmHandler.gvListadoTrabajador.FocusedRowHandle;
                frmHandler.gvListadoTrabajador.FocusedRowHandle = nRow == 0 ? tRow : nRow - 1;

                btnNuevo_ItemClick(btnNuevo, new DevExpress.XtraBars.ItemClickEventArgs(null, null));
                eTrabajador obj = frmHandler.gvListadoTrabajador.GetFocusedRow() as eTrabajador;
                cod_trabajador = obj.cod_trabajador;
                MiAccion = Trabajador.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picSiguienteTrabajador_Click(object sender, EventArgs e)
        {
            try
            {
                int tRow = frmHandler.gvListadoTrabajador.RowCount - 1;
                int nRow = frmHandler.gvListadoTrabajador.FocusedRowHandle;
                frmHandler.gvListadoTrabajador.FocusedRowHandle = nRow == tRow ? 0 : nRow + 1;

                btnNuevo_ItemClick(btnNuevo, new DevExpress.XtraBars.ItemClickEventArgs(null, null));
                eTrabajador obj = frmHandler.gvListadoTrabajador.GetFocusedRow() as eTrabajador;
                cod_trabajador = obj.cod_trabajador;
                MiAccion = Trabajador.Editar;
                Editar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkflgPeriodoPruebaInfoLaboral_CheckedChanged(object sender, EventArgs e)
        {
            lkpTiempoPeriodoInfoLaboral.ReadOnly = chkflgPeriodoPruebaInfoLaboral.CheckState == CheckState.Checked ? false : true;
        }

        private void gvListadoFormAcademica_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eTrabajador.eInfoAcademica_Trabajador obj = gvListadoFormAcademica.GetRow(e.RowHandle) as eTrabajador.eInfoAcademica_Trabajador;
                    if (e.Column.FieldName == "flg_certificado" && obj.flg_certificado == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_certificado" && obj.flg_certificado == "NO") e.DisplayText = "";
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvListadoExpProfesional_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eTrabajador.eInfoProfesional_Trabajador obj = gvListadoExpProfesional.GetRow(e.RowHandle) as eTrabajador.eInfoProfesional_Trabajador;
                    if (e.Column.FieldName == "flg_certificado" && obj.flg_certificado == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_certificado" && obj.flg_certificado == "NO") e.DisplayText = "";
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void gvListadoFormAcademica_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eTrabajador.eInfoAcademica_Trabajador obj = new eTrabajador.eInfoAcademica_Trabajador();
                if (e.Clicks == 2 && e.Column.FieldName == "flg_certificado")
                {
                    obj = gvListadoFormAcademica.GetFocusedRow() as eTrabajador.eInfoAcademica_Trabajador;
                    if (obj == null) { return; }

                    if (obj.flg_certificado == "NO")
                    {
                        MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //else
                    //{
                    eEmpresa eEmp = unit.Trabajador.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
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
                        //eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                        string correo = eEmp.UsuarioOnedrivePersonal;
                        string password = eEmp.ClaveOnedrivePersonal;

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

                        string IdOneDriveDoc = obj.id_certificado;
                        string Extension = ".pdf";

                        var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                        string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + ("Cert. Academico_" + txtNroDocumento.Text + Extension);
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
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                        return;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void gvListadoExpProfesional_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eTrabajador.eInfoProfesional_Trabajador obj = new eTrabajador.eInfoProfesional_Trabajador();
                if (e.Clicks == 2 && e.Column.FieldName == "flg_certificado")
                {
                    obj = gvListadoExpProfesional.GetFocusedRow() as eTrabajador.eInfoProfesional_Trabajador;
                    if (obj == null) { return; }

                    if (obj.flg_certificado == "NO")
                    {
                        MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //else
                    //{
                    eEmpresa eEmp = unit.Trabajador.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
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
                        //eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                        string correo = eEmp.UsuarioOnedrivePersonal;
                        string password = eEmp.ClaveOnedrivePersonal;

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

                        string IdOneDriveDoc = obj.id_certificado;
                        string Extension = ".pdf";

                        var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                        string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + ("Cert. Profesional_" + txtNroDocumento.Text + Extension);
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
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                        return;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvListadoInfoEconomica_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoInfoEconomica_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoInfoEconomica_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0) gvListadoInfoEconomica_FocusedRowChanged(gvListadoInfoEconomica, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, e.RowHandle));
        }

        private void gvListadoInfoEconomica_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0) Obtener_InfoEconomica();
        }

        private void Obtener_InfoEconomica()
        {
            eTrabajador.eInfoEconomica_Trabajador obj = new eTrabajador.eInfoEconomica_Trabajador();
            obj = gvListadoInfoEconomica.GetFocusedRow() as eTrabajador.eInfoEconomica_Trabajador;
            if (obj == null) return;
            txtCodInfoEconomica.Text = obj.cod_infoeconomica.ToString(); ;
            txtIngresoMensualInfoEconomica.EditValue = obj.imp_ingresomensual;
            txtGastoMensualInfoEconomica.EditValue = obj.imp_gastomensual;
            txtValorActivoInfoEconomica.EditValue = obj.imp_totalactivos;
            txtValorDeudaInfoEconomica.EditValue = obj.imp_totaldeudas;
            lkpViviendaInfoEconomica.EditValue = obj.cod_vivienda;
            lkpTipoViviendaInfoEconomica.EditValue = obj.cod_tipovivienda;
            txtValorRentaInfoEconomica.EditValue = obj.imp_valorvivienda;
            lkpTipoMonedaViviendaInfoEconomica.EditValue = obj.cod_monedavivienda;
            txtDireccionViviendaInfoEconomica.Text = obj.dsc_direccion_vivienda;
            txtReferenciaViviendaInfoEconomica.Text = obj.dsc_referencia_vivienda;
            lkpPaisViviendaInfoEconomica.EditValue = obj.cod_pais_vivienda;
            lkpDepartamentoViviendaInfoEconomica.EditValue = obj.cod_departamento_vivienda;
            lkpProvinciaViviendaInfoEconomica.EditValue = obj.cod_provincia_vivienda;
            glkpDistritoViviendaInfoEconomica.EditValue = obj.cod_distrito_vivienda;
            lkpVehiculoInfoEconomica.EditValue = obj.cod_tipovehiculo;
            txtMarcaInfoEconomica.Text = obj.dsc_marcavehiculo;
            txtModeloInfoEconomica.Text = obj.dsc_modelovehiculo;
            txtPlacaVehiculoInfoEconomica.Text = obj.dsc_placavehiculo;
            txtValorComercialInfoEconomica.EditValue = obj.imp_valorvehiculo;
            lkpTipoMonedaVehiculoInfoEconomica.EditValue = obj.cod_monedavehiculo;
            lkpEmpresaInfoEconomica.EditValue = obj.cod_tipoempresa;
            txtParticipacionInfoEconomica.Text = obj.dsc_participacion_empresa;
            txtRUCEmpresaInfoEconomica.Text = obj.dsc_RUC_empresa;
            txtGiroEmpresaInfoEconomica.Text = obj.dsc_giro_empresa;
            txtDireccionEmpresaInfoEconomica.Text = obj.dsc_direccion_empresa;
            txtReferenciaEmpresaInfoEconomica.Text = obj.dsc_referencia_empresa;
            lkpPaisEmpresaInfoEconomica.EditValue = obj.cod_pais_empresa;
            lkpDepartamentoEmpresaInfoEconomica.EditValue = obj.cod_departamento_empresa;
            lkpProvinciaEmpresaInfoEconomica.EditValue = obj.cod_provincia_empresa;
            glkpDistritoEmpresaInfoEconomica.EditValue = obj.cod_distrito_empresa;
        }

        private void gvHistoriaEMO_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvHistoriaEMO_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvHistoriaEMO_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    eTrabajador.eCertificadoEMO_Trabajador obj = gvHistoriaEMO.GetRow(e.RowHandle) as eTrabajador.eCertificadoEMO_Trabajador;
                    if (e.Column.FieldName == "flg_certificado" && obj.flg_certificado == "SI")
                    {
                        e.Handled = true; e.Graphics.DrawImage(ImgPDF, new Rectangle(e.Bounds.X + (e.Bounds.Width / 2) - 8, e.Bounds.Y + (e.Bounds.Height / 2) - 8, 16, 16));
                    }
                    if (e.Column.FieldName == "flg_certificado" && obj.flg_certificado == "NO") e.DisplayText = "";
                    e.DefaultDraw();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void gvHistoriaEMO_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                eTrabajador.eCertificadoEMO_Trabajador obj = new eTrabajador.eCertificadoEMO_Trabajador();
                if (e.Clicks == 2 && e.Column.FieldName == "flg_certificado")
                {
                    obj = gvHistoriaEMO.GetFocusedRow() as eTrabajador.eCertificadoEMO_Trabajador;
                    if (obj == null) { return; }

                    if (obj.flg_certificado == "NO")
                    {
                        MessageBox.Show("No se cargado ningún PDF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //else
                    //{
                    eEmpresa eEmp = unit.Trabajador.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
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
                        //eEmpresa eEmp = unit.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);
                        string correo = eEmp.UsuarioOnedrivePersonal;
                        string password = eEmp.ClaveOnedrivePersonal;

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

                        string IdOneDriveDoc = obj.id_certificado;
                        string Extension = ".pdf";

                        var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                        string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + ("EMO_" + obj.fch_registro.Year.ToString() + "." + obj.fch_registro.ToString("MM") + "." + obj.fch_registro.ToString("dd") + Extension);
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
                        MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                        return;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkflgEnfermedadInfoFamiliar_CheckStateChanged(object sender, EventArgs e)
        {
            layoutControlItem102.Enabled = chkflgEnfermedadInfoFamiliar.CheckState == CheckState.Checked ? true : false;
        }

        private void lkpPrefCECOInfoLaboral_EditValueChanged(object sender, EventArgs e)
        {
            lkpPrefCECOInfoLaboral.ToolTip = lkpPrefCECOInfoLaboral.EditValue == null ? "" : lkpPrefCECOInfoLaboral.Text;
        }

        private void chkflgAdiccionInfoFamiliar_CheckStateChanged(object sender, EventArgs e)
        {
            layoutControlItem104.Enabled = chkflgAdiccionInfoFamiliar.CheckState == CheckState.Checked ? true : false;
        }

        private void txtPesoCaractFisica_EditValueChanged(object sender, EventArgs e)
        {
            decimal peso = 0, estatura = 0;
            estatura = Convert.ToDecimal(Math.Pow(Convert.ToDouble(txtEstaturaCaractFisica.EditValue), 2));
            peso = Convert.ToDecimal(txtPesoCaractFisica.EditValue);
            if (estatura > 0) txtIMCCaractFisica.EditValue = Math.Round((peso / estatura), 2);
        }

        private async void VerDocumentos(int opcionDoc, string nombreDoc)
        {
            eTrabajador resultado = unit.Trabajador.Obtener_Trabajador<eTrabajador>(2, cod_trabajador, cod_empresa);
            if (resultado == null) return;
            switch (opcionDoc)
            {
                case 1: if (resultado.id_DNI == null || resultado.id_DNI == "") return; break;
                case 2: if (resultado.id_CV == null || resultado.id_CV == "") return; break;
                case 3: if (resultado.id_AntPolicial == null || resultado.id_AntPolicial == "") return; break;
                case 4: if (resultado.id_AntPenal == null || resultado.id_AntPenal == "") return; break;
                case 5: if (resultado.id_VerifDomiciliaria == null || resultado.id_VerifDomiciliaria == "") return; break;
            }

            eEmpresa eEmp = unit.Trabajador.ObtenerDatosOneDrive<eEmpresa>(12, cod_empresa);
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
                string correo = eEmp.UsuarioOnedrivePersonal;
                string password = eEmp.ClaveOnedrivePersonal;

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

                string IdPDF = opcionDoc == 1 ? resultado.id_DNI : opcionDoc == 2 ? resultado.id_CV : opcionDoc == 3 ? resultado.id_AntPolicial : opcionDoc == 4 ? resultado.id_AntPenal : opcionDoc == 5 ? resultado.id_VerifDomiciliaria : "";
                string IdOneDriveDoc = IdPDF;

                var fileContent = await GraphClient.Me.Drive.Items[IdOneDriveDoc].Content.Request().GetAsync();
                string ruta = unit.Encripta.Desencrypta(ConfigurationManager.AppSettings[unit.Encripta.Encrypta("RutaArchivosLocalExportar")].ToString()) + @"\" + nombreDoc + "-" + resultado.dsc_documento + ".pdf";
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
                MessageBox.Show("Hubieron problemas al autenticar las credenciales", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //lblResultado.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                return;
            }
        }


        private void gvInfoLaboral_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvInfoLaboral_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void glkpTipoDocumentoContacto_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpTipoDocumentoContacto.EditValue != null)
            {
                eProveedor obj = new eProveedor();
                obj = unit.Proveedores.Validar_NroDocumento<eProveedor>(19, "", glkpTipoDocumentoContacto.EditValue.ToString());
                txtNroDocumentoContacto.Properties.MaxLength = obj.ctd_digitos;
            }
        }

        private void lkpEmpresa_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpEmpresa.EditValue != null)
            {
                unit.Trabajador.CargaCombosLookUp("SedesEmpresa", lkpSedeEmpresaInfoLaboral, "cod_sede_empresa", "dsc_sede_empresa", "", valorDefecto: true, lkpEmpresa.EditValue.ToString());
                lkpAreaInfoLaboral.Properties.DataSource = null; lkpCargoInfoLaboral.Properties.DataSource = null;
                lkpSedeEmpresaInfoLaboral.EditValue = null; lkpAreaInfoLaboral.EditValue = null; lkpCargoInfoLaboral.EditValue = null;
            }
        }

        private void lkpSedeEmpresaInfoLaboral_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpSedeEmpresaInfoLaboral.EditValue != null)
            {
                unit.Trabajador.CargaCombosLookUp("AreaEmpresa", lkpAreaInfoLaboral, "cod_area", "dsc_area", "", valorDefecto: true, lkpEmpresa.EditValue.ToString(), lkpSedeEmpresaInfoLaboral.EditValue.ToString());
                lkpCargoInfoLaboral.Properties.DataSource = null; lkpAreaInfoLaboral.EditValue = null; lkpCargoInfoLaboral.EditValue = null;
            }
        }

        private void lkpAreaInfoLaboral_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpAreaInfoLaboral.EditValue != null)
            {
                unit.Trabajador.CargaCombosLookUp("CargoEmpresa", lkpCargoInfoLaboral, "cod_cargo", "dsc_cargo", "", valorDefecto: true, lkpEmpresa.EditValue.ToString(), lkpSedeEmpresaInfoLaboral.EditValue.ToString(), lkpAreaInfoLaboral.EditValue.ToString());
                lkpCargoInfoLaboral.EditValue = null;
            }
        }

        private void lkpTipoContratoInfoLaboral_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoContratoInfoLaboral.EditValue != null) unit.Trabajador.CargaCombosLookUp("Modalidad", lkpModalidadInfoLaboral, "cod_ModContrato", "dsc_ModContrato", "", valorDefecto: true, cod_tipoContrato: lkpTipoContratoInfoLaboral.EditValue.ToString());
        }

        private void frmMantTrabajador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MiAccion == Trabajador.Editar) this.Close();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MiAccion = Trabajador.Nuevo;
                btnNuevo.Enabled = false;
                acctlMenu.Enabled = false;
                cod_trabajador = "";
                txtCodTrabajador.Text = "";
                txtApellPaterno.Text = "";
                txtApellMaterno.Text = "";
                txtNombre.Text = "";
                dtFecNacimiento.EditValue = null;
                lkpEstadoCivil.EditValue = "01";
                lkpEmpresa.EditValue = null;
                glkpTipoDocumento.EditValue = "DI001";
                txtNroDocumento.Text = "";
                dtFecVctoDocumento.EditValue = null;
                txtNroUbigeoDocumento.Text = "";
                chkFlgDNI.CheckState = CheckState.Unchecked;
                chkFlgCV.CheckState = CheckState.Unchecked;
                chkFlgVerifDomiciliaria.CheckState = CheckState.Unchecked;
                chkFlgAntPoliciales.CheckState = CheckState.Unchecked;
                chkFlgAntPenales.CheckState = CheckState.Unchecked;
                lkpSistPensionarioInfoBancaria.EditValue = null;
                lkpNombreAFPInfoBancaria.EditValue = null;
                txtNroCUSPPInfoBancaria.Text = "";
                txtDireccion.Text = "";
                txtReferencia.Text = "";
                lkpPais.EditValue = "00001"; lkpDepartamento.EditValue = "00015"; lkpProvincia.EditValue = "00128"; lkpNacionalidad.EditValue = "00001";
                glkpDistrito.EditValue = null;
                txtTelefono.Text = "";
                txtCelular.Text = "";
                txtEmail1.Text = "";
                txtEmail2.Text = "";
                ListHistInfoLaboral.Clear(); ListContactos.Clear(); ListInfoLaboral.Clear();
                ListInfoFamiliar.Clear(); ListInfoAcademica.Clear(); ListInfoProfesional.Clear();
                gvInfoLaboral.RefreshData(); gvListadoContactos.RefreshData(); gvListadoInfoLaboral.RefreshData();
                gvListadoInfoFamiliar.RefreshData(); gvListadoFormAcademica.RefreshData(); gvListadoExpProfesional.RefreshData();
                btnNuevoContacto_Click(null, new EventArgs());
                btnNuevaInfoLaboral_Click(null, new EventArgs());
                btnNuevaInfoFamiliar_Click(null, new EventArgs());
                btnNuevaFormAcademic_Click(null, new EventArgs());
                btnNuevaInfoEconomica_Click(null, new EventArgs());
                btnNuevaExpProfesional_Click(null, new EventArgs());
                chkflgAlergiasInfoSalud.CheckState = CheckState.Unchecked;
                mmAlergias.Text = "";
                chkflgOperacionesInfoSalud.CheckState = CheckState.Unchecked;
                mmOperaciones.Text = "";
                chkflgEnfPrexistenteInfoSalud.CheckState = CheckState.Unchecked;
                mmEnfPrexistente.Text = "";
                chkflgTratamientoInfoSalud.CheckState = CheckState.Unchecked;
                mmTratamiento.Text = "";
                chkflgEnfActualInfoSalud.CheckState = CheckState.Unchecked;
                mmEnfActualidad.Text = "";
                chkflgTratActualInfoSalud.CheckState = CheckState.Unchecked;
                mmTratActual.Text = "";
                chkflgDiscapacidadInfoSalud.CheckState = CheckState.Unchecked;
                mmDiscapacidad.Text = "";
                chkflgOtrosInfoSalud.CheckState = CheckState.Unchecked;
                mmOtros.Text = "";
                lkpGrupoSanguineoInfoSalud.EditValue = null;
                lkpEstadoSaludInfoSalud.EditValue = null;
                lkpSeguroSaludInfoSalud.EditValue = null;
                txtEspecificarInfoSalud.Text = "";
                glkpBancoInfoBancaria.EditValue = null;
                lkpTipoMonedaInfoBancaria.EditValue = null;
                lkpTipoCuentaInfoBancaria.EditValue = null;
                txtNroCuentaBancariaInfoBancaria.Text = "";
                txtNroCuentaCCIInfoBancaria.Text = "";
                glkpBancoCTSInfoBancaria.EditValue = null;
                lkpTipoMonedaCTSInfoBancaria.EditValue = null;
                txtNroCuentaBancariaCTSInfoBancaria.Text = "";
                txtNroCuentaCCICTSInfoBancaria.Text = "";
                lkpSistPensionarioInfoBancaria.EditValue = null;
                lkpNombreAFPInfoBancaria.EditValue = null;
                txtNroCUSPPInfoBancaria.Text = "";
                txtEstaturaCaractFisica.EditValue = 0;
                txtPesoCaractFisica.EditValue = 0;
                txtIMCCaractFisica.EditValue = 0;
                chkflgLentesCaractFisica.CheckState = CheckState.Unchecked;
                lkpPoloTallaUnif.EditValue = null;
                lkpCamisaTallaUnif.EditValue = null;
                lkpPantalonTallaUnif.EditValue = null;
                lkpCasacaTallaUnif.EditValue = null;
                lkpMamelucoTallaUnif.EditValue = null;
                lkpChalecoTallaUnif.EditValue = null;
                txtCalzadoTallaUnif.EditValue = null;
                txtIngresoMensualInfoEconomica.EditValue = 0;
                txtGastoMensualInfoEconomica.EditValue = 0;
                txtValorActivoInfoEconomica.EditValue = 0;
                txtValorDeudaInfoEconomica.EditValue = 0;
                lkpViviendaInfoEconomica.EditValue = null;
                lkpTipoViviendaInfoEconomica.EditValue = null;
                txtValorRentaInfoEconomica.EditValue = 0;
                txtDireccionViviendaInfoEconomica.Text = "";
                txtReferenciaViviendaInfoEconomica.Text = "";
                lkpPaisViviendaInfoEconomica.EditValue = null;
                lkpDepartamentoViviendaInfoEconomica.EditValue = null;
                lkpProvinciaViviendaInfoEconomica.EditValue = null;
                glkpDistritoViviendaInfoEconomica.EditValue = null;
                lkpVehiculoInfoEconomica.EditValue = null;
                txtMarcaInfoEconomica.Text = "";
                txtModeloInfoEconomica.Text = "";
                txtPlacaVehiculoInfoEconomica.Text = "";
                txtValorComercialInfoEconomica.EditValue = 0;
                lkpEmpresaInfoEconomica.EditValue = null;
                txtParticipacionInfoEconomica.Text = "";
                txtRUCEmpresaInfoEconomica.Text = "";
                txtGiroEmpresaInfoEconomica.Text = "";
                txtDireccionEmpresaInfoEconomica.Text = "";
                txtReferenciaEmpresaInfoEconomica.Text = "";
                lkpPaisEmpresaInfoEconomica.EditValue = null;
                lkpDepartamentoEmpresaInfoEconomica.EditValue = null;
                lkpProvinciaEmpresaInfoEconomica.EditValue = null;
                glkpDistritoEmpresaInfoEconomica.EditValue = null;
                lkpViviendaInfoVivienda.EditValue = null;
                lkpComodidadInfoVivienda.EditValue = null;
                chkflgPuertasInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgVentanasInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgTechoInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgTelefonoInfoVivienda.CheckState = CheckState.Unchecked;
                chkCelularesInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgInternetComunicacionInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgLuzInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgAguaInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgDesagueInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgGasInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgCableInfoVivienda.CheckState = CheckState.Unchecked;
                chkflgInternetServicioInfoVivienda.CheckState = CheckState.Unchecked;
                mmViasAccesoInfoVivienda.Text = "";
                mmIluminacionInfoVivienda.Text = "";
                mmEntornoInfoVivienda.Text = "";
                mmPuestoPolicialInfoVivienda.Text = "";
                txtNombreFamiliarInfoVivienda.Text = "";
                txtHorasCasaInfoVivienda.Text = "";
                lkpParentescoInfoVivienda.EditValue = null;
                txtCelularInfoVivienda.Text = "";
                txtEmailInfoVivienda.Text = "";

                navframeTrabajador.SelectedPage = navpageGemeral;
                txtApellPaterno.Select();
                if (MiAccion != Trabajador.Editar)
                {
                    picAnteriorTrabajador.Enabled = false; picSiguienteTrabajador.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                txtApellPaterno.Select();
                if (txtApellPaterno.Text.Trim() == "") { MessageBox.Show("Debe ingresar un apellido paterno.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtApellPaterno.Focus(); return; }
                if (txtApellMaterno.Text.Trim() == "") { MessageBox.Show("Debe ingresar un apellido materno.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtApellMaterno.Focus(); return; }
                if (txtNombre.Text.Trim() == "") { MessageBox.Show("Debe ingresar un nombre.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNombre.Focus(); return; }
                if (dtFecNacimiento.EditValue == null) { MessageBox.Show("Debe ingresar la fecha de nacimiento", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecNacimiento.Focus(); return; }
                if (txtNroDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un número de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroDocumento.Focus(); return; }
                if (dtFecVctoDocumento.EditValue == null) { MessageBox.Show("Debe ingresar la fecha de vencimiento del documento", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtFecVctoDocumento.Focus(); return; }
                //if (txtNroUbigeoDocumento.Text.Trim() == "") { MessageBox.Show("Debe ingresar un número de ubigeo de documento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNroUbigeoDocumento.Focus(); return; }
                if (txtDireccion.Text.Trim() == "") { MessageBox.Show("Debe ingresar una dirección.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDireccion.Focus(); return; }
                if (lkpPais.EditValue == null) { MessageBox.Show("Debe seleccionar un país.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpPais.Focus(); return; }
                if (lkpDepartamento.EditValue == null) { MessageBox.Show("Debe seleccionar un departamento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpDepartamento.Focus(); return; }
                if (lkpProvincia.EditValue == null) { MessageBox.Show("Debe seleccionar una provincia.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpProvincia.Focus(); return; }
                if (glkpDistrito.EditValue == null) { MessageBox.Show("Debe seleccionar un distrito.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); glkpDistrito.Focus(); return; }
                if (lkpNacionalidad.EditValue == null) { MessageBox.Show("Debe seleccionar la nacionalidad.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); lkpNacionalidad.Focus(); return; }

                eTrab = AsignarValores_Trabajador();
                eTrab = unit.Trabajador.InsertarActualizar_Trabajador<eTrabajador>(eTrab);
                if (eTrab == null) { MessageBox.Show("Error al guardar los datos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                cod_trabajador = eTrab.cod_trabajador; cod_empresa = eTrab.cod_empresa;

                if (eTrab != null)
                {
                    ActualizarListado = "SI";
                    if (frmHandler != null)
                    {
                        int nRow = frmHandler.gvListadoTrabajador.FocusedRowHandle;
                        frmHandler.frmListadoTrabajador_KeyDown(frmHandler, new KeyEventArgs(Keys.F5));
                        frmHandler.gvListadoTrabajador.FocusedRowHandle = nRow;
                        //frmHandler.CargarOpcionesMenu();
                    }

                    MiAccion = Trabajador.Editar;
                    MessageBox.Show("Se registraron los datos de manera satisfactoria.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Inicializar();
                    //frmHandler.frmListadoTrabajador_KeyDown(null, new KeyEventArgs(Keys.F5));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private eTrabajador AsignarValores_Trabajador()
        {
            eTrabajador obj = new eTrabajador();
            obj.cod_trabajador = txtCodTrabajador.Text;
            obj.dsc_apellido_paterno = txtApellPaterno.Text.Trim();
            obj.dsc_apellido_materno = txtApellMaterno.Text.Trim();
            obj.dsc_nombres = txtNombre.Text.Trim();
            obj.fch_nacimiento = Convert.ToDateTime(dtFecNacimiento.EditValue);
            obj.cod_estadocivil = lkpEstadoCivil.EditValue.ToString();
            obj.cod_empresa = lkpEmpresa.EditValue.ToString();
            obj.cod_tipo_documento = glkpTipoDocumento.EditValue.ToString();
            obj.dsc_documento = txtNroDocumento.Text;
            obj.fch_vcto_documento = Convert.ToDateTime(dtFecVctoDocumento.EditValue);
            obj.nro_ubigeo_documento = txtNroUbigeoDocumento.Text;
            obj.cod_nacionalidad = lkpNacionalidad.EditValue.ToString();
            obj.flg_DNI = chkFlgDNI.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_CV = chkFlgCV.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_VerifDomiciliaria = chkFlgVerifDomiciliaria.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_AntPolicial = chkFlgAntPoliciales.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.flg_AntPenal = chkFlgAntPenales.CheckState == CheckState.Checked ? "SI" : "NO";
            obj.dsc_direccion = txtDireccion.Text;
            obj.dsc_referencia = txtReferencia.Text;
            obj.cod_pais = lkpPais.EditValue.ToString();
            obj.cod_departamento = lkpDepartamento.EditValue.ToString();
            obj.cod_provincia = lkpProvincia.EditValue.ToString();
            obj.cod_distrito = glkpDistrito.EditValue.ToString();
            obj.dsc_telefono = txtTelefono.Text;
            obj.dsc_celular = txtCelular.Text;
            obj.dsc_mail_1 = txtEmail1.Text;
            obj.dsc_mail_2 = txtEmail2.Text;
            obj.cod_TipoPersonal = grdbTipoPersonal.SelectedIndex == 0 ? "OFICINA" : "DESTACADO";
            obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            obj.flg_activo = "SI";
            obj.fch_entrega_uniforme = Convert.ToDateTime(dtFecEntregaUnif.EditValue);
            obj.fch_renovacion_uniforme = Convert.ToDateTime(dtFecRenovacionUnif.EditValue);

            return obj;
        }

    }
}