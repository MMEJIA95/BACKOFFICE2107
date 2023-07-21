using BE_BackOffice;
using DA_BackOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_BackOffice
{
    public class blSolicitudCompra
    {
        private readonly daSQL sql;
        public blSolicitudCompra(daSQL sql) { this.sql = sql; }

        public List<T> ConsultaVarias<T>(PQSolicitudCompra @params) where T : class, new()
        {
            if (@params is null)
            {
                throw new ArgumentNullException(nameof(@params));
            }

            //List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", @params.Opcion},
                { "cod_empresa", @params.Cod_empresa},
                { "cod_sede_empresa", @params.Cod_sede_empresa},
                { "cod_almacen", @params.Cod_almacen},
                { "cod_requerimiento", @params.Cod_requerimiento},
                { "cod_producto", @params.Cod_producto},
                { "fch_busqueda_desde", @params.Fch_busqueda_desde},
                { "fch_busqueda_hasta", @params.Fch_busqueda_hasta},
                { "dsc_anho", @params.Dsc_anho},
            };

            return sql.ListaconSP<T>("Usp_BCF_ConsultaVarias_SolicitudCompra", oDictionary);
            //return myList;
        }
        public eUsuario_Aprobacion ObtenerAprobacion(string cod_aprobacion)
        {
            if (cod_aprobacion is null) { throw new ArgumentNullException(nameof(cod_aprobacion)); }

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            { { "cod_aprobacion", cod_aprobacion} };

            var result = sql.ListaconSP<eUsuario_Aprobacion>("Usp_BCF_Consultar_UsuarioAprobacion", oDictionary);
            return result.Count > 0 ? result.First() : new eUsuario_Aprobacion();
        }
        public eSqlMessage RestablecerRequerimientoGenerada(string cod_empresa, string cod_sede_empresa, 
            string cod_requerimiento, string cod_producto, string cod_orden_compra_servicio="", string elim_oc="")
        {
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", cod_empresa},
                { "cod_sede_empresa", cod_sede_empresa},
                { "cod_requerimiento", cod_requerimiento},
                { "cod_producto", cod_producto},
                { "cod_orden_compra_servicio", cod_orden_compra_servicio},
                { "elim_oc", elim_oc},
            };

            var result = sql.ConsultarEntidad<eSqlMessage>("Usp_BCF_Restablecer_SolicitudOCGenerada", oDictionary);
            return result;
        }
    }
}
