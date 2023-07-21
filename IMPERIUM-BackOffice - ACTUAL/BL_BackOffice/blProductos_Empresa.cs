﻿using DA_BackOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_BackOffice;

namespace BL_BackOffice
{
    public class blProductos_Empresa
    {
        readonly daSQL sql;
        public blProductos_Empresa(daSQL sql) { this.sql = sql; }

        public List<T> Cargar_Empresas<T>(int opcion, string cod_usuario = "", string cod_producto = "") where T : class, new()
        {
            List<T> myList = new List<T>();
            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "opcion", opcion},
                { "cod_usuario", cod_usuario},
                { "cod_producto", cod_producto}
            };
            myList = sql.ListaconSP<T>("usp_Listar_Productos_Empresa", oDictionary);
            return myList;
        }

        public T Ins_Act_Prod_Empresa<T>(eProductos_Empresa eProdEmp) where T : class, new()
        {
            T obj = new T();

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", eProdEmp.cod_empresa},
                { "cod_tipo_servicio", eProdEmp.cod_tipo_servicio},
                { "cod_subtipo_servicio", eProdEmp.cod_subtipo_servicio},
                { "cod_producto", eProdEmp.cod_producto},
                { "cod_cta_contable", eProdEmp.cod_cta_contable},
                { "flg_con_proveedor", eProdEmp.flg_con_proveedor},
                { "ctd_stock_minimo", eProdEmp.ctd_stock_minimo},
            };

            obj = sql.ConsultarEntidad<T>("usp_Insertar_Actualizar_Productos_Empresa", oDictionary);
            return obj;
        }

        public string Ina_Productos_Empresa(string empresa = "", string cod_tipo_servicio = "", string cod_subtipo_servicio = "", string cod_producto = "")
        {
            string respuesta = "";

            Dictionary<string, object> oDictionary = new Dictionary<string, object>()
            {
                { "cod_empresa", empresa},
                { "cod_tipo_servicio", cod_tipo_servicio},
                { "cod_subtipo_servicio", cod_subtipo_servicio},
                { "cod_producto", cod_producto}
            };

            respuesta = sql.ExecuteSPRetornoValor("usp_Inactivar_Productos_Empresa", oDictionary);
            return respuesta;
        }
    }
}
