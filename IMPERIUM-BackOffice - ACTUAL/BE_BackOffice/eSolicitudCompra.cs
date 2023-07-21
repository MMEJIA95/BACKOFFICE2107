using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_BackOffice
{
    public class eSolicitudCompra
    {

    }
    public class eSolicitudCompra_Vista
    {
        public string cod_producto { get; set; }
        public string dsc_producto { get; set; }
        public decimal num_cantidad { get; set; }
        public decimal num_restante { get; set; }
        public decimal num_cantidad_stock { get; set; }
        public int dsc_diferencia { get; set; }
        public decimal ctd_stock_minimo { get; set; }
        public decimal ctd_stock_reposicion { get; set; }
        public decimal imp_unitario { get; set; }
        public decimal imp_total { get; set; }
    }
    public class eSolicitudCompra_Requerimientos
    {
        public string cod_sede_empresa { get; set; }
        public string cod_requerimiento { get; set; }
        public string cod_producto { get; set; }
        public string dsc_producto { get; set; }
        public int dsc_anho { get; set; }
        public decimal num_cantidad { get; set; }
        public decimal num_restante { get; set; }
        public decimal num_cantidad_disponer { get; set; }
        public string dsc_nombre_solicitante { get; set; }
        public string cod_CECO { get; set; }
        public string dsc_CECO { get; set; }
    }

    public class PQSolicitudCompra : PConsultaBase
    {
        public PQSolicitudCompra()
        {
            _cod_sede_empresa = string.Empty;
            _cod_almacen = string.Empty;
            _cod_requerimiento = string.Empty;
            _cod_producto = string.Empty;
            _fch_busqueda_desde = null;
            _fch_busqueda_hasta = null;
            _dsc_anho = null;
        }

        private string _cod_sede_empresa;
        private string _cod_almacen;
        private string _cod_requerimiento;
        private string _cod_producto;
        private DateTime? _fch_busqueda_desde;
        private DateTime? _fch_busqueda_hasta;
        private int? _dsc_anho;

        public string Cod_sede_empresa { get => _cod_sede_empresa; set => _cod_sede_empresa = value; }
        public string Cod_almacen { get => _cod_almacen; set => _cod_almacen = value; }
        public string Cod_requerimiento { get => _cod_requerimiento; set => _cod_requerimiento = value; }
        public string Cod_producto { get => _cod_producto; set => _cod_producto = value; }
        public DateTime? Fch_busqueda_desde { get => _fch_busqueda_desde; set => _fch_busqueda_desde = value; }
        public DateTime? Fch_busqueda_hasta { get => _fch_busqueda_hasta; set => _fch_busqueda_hasta = value; }
        public int? Dsc_anho { get => _dsc_anho; set => _dsc_anho = value; }
    }
}
