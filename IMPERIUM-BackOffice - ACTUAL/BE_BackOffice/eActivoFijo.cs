using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_BackOffice
{
    public class eActivoFijo
    {
		public string cod_activo_fijo { get; set; }
		public string dsc_descripcion { get; set; }
		public int cod_marca { get; set; }
		public string dsc_marca { get; set; }
		public string dsc_modelo { get; set; }
		public string dsc_placa { get; set; }
		public string cod_color { get; set; }
		public string dsc_color { get; set; }
		public string dsc_serie { get; set; }
		public string dsc_concepto_estado { get; set; }
		public string cod_parametro_estado { get; set; }
		public string dsc_parametro_estado { get; set; }
		public int dsc_anho_fabricacion { get; set; }
		public string dsc_concepto_grupo { get; set; }
		public string cod_parametro_grupo { get; set; }
		public string dsc_parametro_grupo { get; set; }
		public string dsc_concepto_dependencia { get; set; }
		public string cod_parametro_dependencia { get; set; }
		public string dsc_parametro_dependencia { get; set; }
		public string dsc_concepto_clase { get; set; }
		public string cod_parametro_clase { get; set; }
		public string dsc_parametro_clase { get; set; }
		public string dsc_concepto_situacion_activo { get; set; }
		public string cod_parametro_situacion_activo { get; set; }
		public string dsc_parametro_situacion_activo { get; set; }
		public string dsc_concepto_concepto { get; set; }
		public string cod_parametro_concepto { get; set; }
		public string dsc_parametro_concepto { get; set; }
		public string dsc_concepto_catalogo { get; set; }
		public string cod_parametro_catalogo { get; set; }
		public string dsc_parametro_catalogo { get; set; }
		public string dsc_concepto_forma_compra { get; set; }
		public string cod_parametro_forma_compra { get; set; }
		public string dsc_parametro_forma_compra { get; set; }
		public string dsc_agrupamiento { get; set; }
		public string dsc_material { get; set; }
		public string cod_internacional { get; set; }
		public string cod_OSCE { get; set; }
		public string dsc_comprobante { get; set; }
		public string cod_empresa { get; set; }
		public string dsc_empresa { get; set; }
		public string cod_sede_empresa { get; set; }
		public string dsc_sede_empresa { get; set; }
		public string cod_area { get; set; }
		public string dsc_area { get; set; }
		public string dsc_nro_oficina { get; set; }
		public string dsc_piso { get; set; }
		public string cod_usuario_responsable { get; set; }
		public string dsc_usuario_responsable { get; set; }
		public string cod_departamento { get; set; }
		public string cod_provincia { get; set; }
		public string fch_cambio_ubicacion { get; set; }
		public string cod_anexo { get; set; }
		public string tipo_documento { get; set; }
		public string serie_documento { get; set; }
		public decimal numero_documento { get; set; }
		public string fch_garantia { get; set; }
		public string dsc_subdiario { get; set; }
		public string dsc_nro_compra { get; set; }
		public string dsc_cuenta { get; set; }
		public string dsc_cuenta_gasto { get; set; }
		public string fch_inicio_depreciacion { get; set; }
		public string fch_ultimo_depreciacion { get; set; }
		public string fch_ultimo_historico_depreciacion { get; set; }
		public decimal imp_valor_adqui_US { get; set; }
		public decimal imp_valor_adqui_PE { get; set; }
		public string fch_situacion { get; set; }
		public decimal imp_tasa_depreciacion { get; set; }
		public int dsc_meses_depreciacion { get; set; }
		public int dsc_cantidad { get; set; }
		public decimal imp_ValorHistorico_US { get; set; }
		public decimal imp_DepAcumulada_US { get; set; }
		public decimal imp_ValorHistorico_MN { get; set; }
		public decimal imp_DepAcumulada_MN { get; set; }
		//public string dsc_clase { get; set; }
		//public string cod_agrupacion { get; set; }
		public string cod_usuario_registro { get; set; }
		public string dsc_usuario_registro { get; set; }
		public string dsc_usuario_cambio { get; set; }
		public DateTime fch_registro { get; set; }
		public string cod_usuario_cambio { get; set; }
		public DateTime fch_cambio { get; set; }
		public string flg_activo { get; set; }

		public class eParametro : eActivoFijo
		{
			public string cod_parametro { get; set; }
			public string dsc_parametro { get; set; }
		}

	}
}
