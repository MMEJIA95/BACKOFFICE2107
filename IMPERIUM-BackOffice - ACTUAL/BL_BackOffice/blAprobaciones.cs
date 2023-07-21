using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_BackOffice;
using DA_BackOffice;
using DevExpress.XtraEditors;

namespace BL_BackOffice
{
    public class blAprobaciones
    {
        readonly daSQL sql;
        public blAprobaciones(daSQL sql) { this.sql = sql; }

        public void CargaCombosLookUp(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "",
                                    bool valorDefecto = false, string cod_modulo = "", string cod_usuario = "", string cod_tipo_transaccion = "",
                                    string cod_empresa = "", string cod_cliente = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_DocumentosAprobar";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "EmpresasUsuarios":
                        dictionary.Add("opcion", 2);
                        dictionary.Add("cod_usuario", cod_usuario);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "TipoFecha":
                        dictionary.Add("opcion", 4);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Modulos":
                        dictionary.Add("opcion", 9);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                    case "Validacion":
                        dictionary.Add("opcion", 11);
                        dictionary.Add("cod_modulo", cod_modulo);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                }

                combo.Properties.DataSource = tabla;
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
                if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CargaCombosChecked(string nCombo, CheckedComboBoxEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue, string cod_usuario = "", string modulo = "", string empresa = "")
        {
            combo.Text = "";
            string procedure = "usp_ConsultasVarias_DocumentosAprobar";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            try
            {
                switch (nCombo)
                {
                    case "Estados":
                        dictionary.Add("opcion", 1);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case "TipoDocumento":
                        dictionary.Add("opcion", 3);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case "EmpresasUsuarios":
                        dictionary.Add("opcion", 4);
                        dictionary.Add("cod_usuario", cod_usuario);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case "Modulos":
                        dictionary.Add("opcion", 9);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                    case "Trabajadores":
                        dictionary.Add("opcion", 15);
                        dictionary.Add("cod_modulomultiple", modulo);
                        dictionary.Add("cod_empresamultiple", empresa);
                        sql.CargaCombosChecked(procedure, combo, dictionary, campoValueMember, campoDispleyMember, campoSelectedValue);
                        break;
                }
            }
            catch (Exception generatedExceptionName)
            {
                throw;
            }
        }

        public void CargaCombosLookUp_CECO(string nCombo, LookUpEdit combo, string campoValueMember, string campoDispleyMember, string campoSelectedValue = "",
                                    bool valorDefecto = false, string cod_empresa = "")
        {
            combo.Text = "";
            string procedure = "usp_Consulta_ListarFacturasProveedor";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            DataTable tabla = new DataTable();

            try
            {
                switch (nCombo)
                {
                    case "CecoDisponible":
                        dictionary.Add("opcion", 65);
                        dictionary.Add("cod_empresa", cod_empresa);
                        tabla = sql.ListaDatatable(procedure, dictionary);
                        break;
                }

                combo.Properties.DataSource = tabla;
                combo.Properties.ValueMember = campoValueMember;
                combo.Properties.DisplayMember = campoDispleyMember;
                if (campoSelectedValue == "") { combo.ItemIndex = -1; } else { combo.EditValue = campoSelectedValue; }
                if (tabla.Columns["flg_default"] != null) if (valorDefecto) combo.EditValue = tabla.Select("flg_default = 'SI'").Length == 0 ? null : (tabla.Select("flg_default = 'SI'"))[0].ItemArray[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<T> ListarEmpresas<T>(int opcion, string cod_usuario = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                {"opcion", opcion },
                { "cod_usuario", cod_usuario}
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }

        public List<T> FiltroFactura<T>(int opcion, DateTime fechaemision_inicio, DateTime fechaemision_fin, string empresa, string tipodocumento, decimal imp_minimo, decimal imp_maximo) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "fechaemision_inicio", fechaemision_inicio},
                { "fechaemision_fin", fechaemision_fin},
                 { "cod_empresa", empresa},
                 { "tipo_documento", tipodocumento},
                { "imp_minimo", imp_minimo},
                { "imp_maximo", imp_maximo}
            };
            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }

        public string ActualizarEstadoRegistrador(int opcion, DateTime fch_aprobado_reg, string cod_empresa = "", string tipo_documento = "", string serie_documento = "", string numero_documento = "", string cod_usuario_aprobado_reg = "", string cod_caja_multiple = "", string cod_movimiento_multiple = "")
        {

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            {
                dictionary.Add("opcion", opcion);
                dictionary.Add("cod_empresa", cod_empresa);
                dictionary.Add("tipo_documento_multiple", tipo_documento);
                dictionary.Add("serie_documento_multiple", serie_documento);
                dictionary.Add("numero_documento_multiple", numero_documento);
                dictionary.Add("cod_usuario_aprobado_reg", cod_usuario_aprobado_reg);
                dictionary.Add("cod_caja_multiple", cod_caja_multiple);
                dictionary.Add("cod_movimiento_multiple", cod_movimiento_multiple);

                if (fch_aprobado_reg.ToString().Contains("0001")) { dictionary.Add("fch_aprobado_reg", DBNull.Value); } else { dictionary.Add("fch_aprobado_reg", fch_aprobado_reg); }
                string result;
                result = sql.ExecuteScalarWithParams("usp_ConsultasVarias_DocumentosAprobar", dictionary);
                return result;
            }

        }
        public List<T> ListarTrabajadores<T>(int opcion, string cod_empresa = "", string dsc_abreviatura = "", string cod_aprobacion = "", string cod_modulo = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa },
                { "dsc_abreviatura", dsc_abreviatura },
                { "cod_aprobacion", cod_aprobacion },
                { "cod_modulo", cod_modulo }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }

        public T InsertarActualizarAprobaciones<T>(EAprobaciones eUsu) where T : class, new()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "imp_maximo",     eUsu.imp_maximo },
                { "imp_minimo",     eUsu.imp_minimo },
                { "descripcion",    eUsu.descripcion },
                { "flg_activo",     eUsu.flg_activo },
                { "cod_modulo",     eUsu.cod_modulo },
                { "cant_trabajadores", eUsu.cant_trabajadores }

            };

            T obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_MantenimientoAprobaciones", dictionary);
            return obj;
        }

        public T ObtenerCodModulo<T>(int opcion, string cod_modulo = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "abreviatura", cod_modulo}
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_DocumentosAprobar", dictionary);
            return obj;
        }

        public T ActualizarPermisoUsuario<T>(int opcion, EAprobaciones.eTrabajador apro) where T : class, new()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", apro.cod_empresa },
                { "cod_aprobacion", apro.cod_aprobacion },
                { "cod_usuario", apro.cod_usuario }
            };

            T obj = sql.ConsultarEntidad<T>("usp_Insertar_Eliminar_UsuarioAprobaciones", dictionary);
            return obj;

        }

        public T Obtenermaximo<T>(int opcion, string cod_empresa, string cod_aprobacion) where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_empresa", cod_empresa},
                { "cod_aprobacion", cod_aprobacion}
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Eliminar_UsuarioAprobaciones", dictionary);
            return obj;
        }
        public List<T> ListarFacturasHistorial<T>(int opcion, DateTime fechaemision_inicio, DateTime fechaemision_fin, string cod_usuario_aprobadomultiple = "", string cod_empresa = "", string cod_modulo = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "fechaemision_inicio", fechaemision_inicio},
                { "fechaemision_fin", fechaemision_fin},
                 { "cod_usuario_aprobadomultiple", cod_usuario_aprobadomultiple},
                 { "cod_empresa", cod_empresa},
                { "cod_modulo", cod_modulo}
            };
            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }

        public List<T> ListarCajaChica<T>(int opcion, string cod_empresa = "", string cod_responsable = "", string cod_caja = "", string cod_sede_empresa = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa },
                { "cod_responsable", cod_responsable },
                { "cod_caja", cod_caja },
                { "cod_sede_empresa", cod_sede_empresa }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }
        public List<T> ListarEntregaRendir<T>(int opcion, DateTime FechaInicio, DateTime FechaFin, string cod_empresa = "", string cod_sede_empresa = "", decimal imp_minimo = 0, decimal imp_maximo = 0) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa },
                { "cod_sede_empresa", cod_sede_empresa },
                { "FechaInicio", FechaInicio },
                { "FechaFin", FechaFin },
                { "imp_minimo", imp_minimo},
                { "imp_maximo", imp_maximo}
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }
        public string ActualizarAprobacionEntregaRendir(int opcion, DateTime fch_aprobado_reg, string cod_empresamultiple = "", string cod_entregarendir_multiple = "", string cod_sede_empresa = "", string cod_usuario_aprobado_reg = "")
        {

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            {
                dictionary.Add("opcion", opcion);
                dictionary.Add("cod_entregarendir_multiple", cod_entregarendir_multiple);
                dictionary.Add("cod_empresamultiple", cod_empresamultiple);
                dictionary.Add("cod_sede_empresa", cod_sede_empresa);
                dictionary.Add("cod_usuario_aprobado_reg", cod_usuario_aprobado_reg);


                if (fch_aprobado_reg.ToString().Contains("0001")) { dictionary.Add("fch_aprobado_reg", DBNull.Value); } else { dictionary.Add("fch_aprobado_reg", fch_aprobado_reg); }
                string result;
                result = sql.ExecuteScalarWithParams("usp_ConsultasVarias_DocumentosAprobar", dictionary);
                return result;
            }

        }
        public string EliminarMantenimiento(int cod_modulo = 0, string cod_aprobacion = "")
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_modulo", cod_modulo },
                { "cod_aprobacion", cod_aprobacion },

            };

            result = sql.ExecuteScalarWithParams("usp_Eliminar_MantenimientoAprobaciones", dictionary);
            return result;
        }
        public T Obtener_datos<T>(int opcion, string cod_usuario = "", string cod_empresa = "", int cod_modulo = 0, string cod_aprobacion = "") where T : class, new()
        {
            T obj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_usuario", cod_usuario },
                { "cod_empresa", cod_empresa },
                 { "cod_modulo", cod_modulo },
                { "cod_aprobacion", cod_aprobacion }
            };

            obj = sql.ConsultarEntidad<T>("usp_ConsultasVarias_DocumentosAprobar", dictionary);
            return obj;
        }
        public List<T> FiltroFacturaMenu<T>(int opcion, string cod_empresa_multiple = "", string tipo_documento = "", string FechaInicio = "", string FechaFin = "", string cod_tipo_documento = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_empresa_multiple", cod_empresa_multiple},
                { "tipo_documento", tipo_documento},
                { "FechaInicio", FechaInicio},
                { "FechaFin", FechaFin},
            };
            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }

        public List<T> ListarCajachicaAuditoria<T>(int opcion, string cod_empresa = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }
        public List<T> ListarEntregaRendirAuditoria<T>(int opcion, DateTime FechaInicio, DateTime FechaFin, string cod_empresa = "", string cod_movimiento_rendido = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa },
                { "fch_inicio", FechaInicio },
                { "fch_fin", FechaFin },
                {"cod_movimiento_rendido", cod_movimiento_rendido }

            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }

        public List<T> listarcajachica<T>(int opcion, DateTime fch_inicio, DateTime fch_fin, string cod_empresa = "", string cod_movimiento_rendido = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_empresa", cod_empresa},
                { "fch_inicio", fch_inicio},
                { "fch_fin", fch_fin},
                {"cod_movimiento_rendido", cod_movimiento_rendido }
            };
            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }
        public List<T> FiltroFacturaAuditoria<T>(int opcion, string FechaInicio, string FechaFin, string cod_empresa_multiple = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion},
                { "cod_empresa_multiple", cod_empresa_multiple},
                { "fch_inicio", FechaInicio},
                { "FechaFin", FechaFin},
            };
            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }

        public string EliminarProgramacionpagos(int opcion, string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor, decimal imp_saldo = 0, int num_linea = 0)
        {
            string result;
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "tipo_documento", tipo_documento },
                { "serie_documento", serie_documento },
                { "numero_documento", numero_documento },
                { "cod_proveedor", cod_proveedor },
                { "imp_saldo", imp_saldo },
                { "num_linea", num_linea }

            };

            result = sql.ExecuteScalarWithParams("usp_bcf_Actualizar_RestablecerContable", dictionary);
            return result;
        }

        public List<T> Ultimaprogramacion<T>(int opcion, string tipo_documento, string serie_documento, decimal numero_documento, string cod_proveedor) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>() {
                { "opcion", opcion },
                { "tipo_documento", tipo_documento },
                { "serie_documento", serie_documento },
                { "numero_documento", numero_documento },
                { "cod_proveedor", cod_proveedor }

            };
            myList = sql.ListaconSP<T>("usp_bcf_Actualizar_RestablecerContable", oDictionary);
            return myList;
        }
        public List<T> ListarAprobadoresCP<T>(int opcion, string cod_empresa = "",int cod_aprobador = 0) where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion },
                { "cod_empresa", cod_empresa },
                { "cod_aprobador", cod_aprobador }
            };

            myList = sql.ListaconSP<T>("usp_ConsultasVarias_DocumentosAprobar", oDictionary);
            return myList;
        }
        public T Insertar_Actualizar_Aprobacioncp<T>(EAprobaciones.EcuentasPagar obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_aprobador", obj.cod_aprobador },{ "cod_trabajador",obj.cod_trabajador },
                { "cod_empresa", obj.cod_empresa }, { "imp_totaldesde", obj.imp_totaldesde }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_UsuarioAprobacionCxP", dictionary);
            return objj;
        }
        public T Insertar_Actualizar_Aprobacioncp_detalle<T>(EAprobaciones.EcuentasPagar obj) where T : class, new()
        {
            T objj = new T();
            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                { "cod_aprobacion", obj.cod_aprobacion },{ "cod_aprobador", obj.cod_aprobador }, { "dsc_ceco", obj.dsc_ceco },
                { "codigo_ceco", obj.codigo_ceco },{ "cod_empresa", obj.cod_empresa },{ "cod_ceco", obj.cod_ceco }
            };

            objj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_UsuarioAprobacionCxP_detalle", dictionary);
            return objj;
        }
    }
}
