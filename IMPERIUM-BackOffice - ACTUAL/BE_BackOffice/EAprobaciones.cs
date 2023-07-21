using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_BackOffice
{
    public class EAprobaciones
    {
        public string cod_empresa { get; set; }
        public string dsc_empresa { get; set; }
        public string cod_tipo_comprobante { get; set; }
        public string dsc_tipo_comprobante { get; set; }
        public string tipo_documento { get; set; }
        public string dsc_razon_social { get; set; }
        public string cod_modulofiltro { get; set; }
        public string dsc_modulofiltro { get; set; }
        public DateTime fch_registro { get; set; }
        public DateTime fch_documento { get; set; }
        public string cod_moneda { get; set; }
        public decimal imp_total { get; set; }

        public decimal imp_saldo { get; set; }

        public DateTime fch_vencimiento { get; set; }

        public string documento { get; set; }
        public string cod_proveedor { get; set; }
        public string serie_documento { get; set; }
        public decimal numero_documento { get; set; }
        public bool seleccionado { get; set; }
        public string value { get; set; }
        public string descripcion { get; set; }
        public string dsc_modulo { get; set; }
        public decimal imp_maximo { get; set; }
        public decimal imp_minimo { get; set; }
        public int cod_modulo { get; set; }
        public int cant_trabajadores { get; set; }
        public string flg_activo { get; set; }
        public int cod_aprobacion { get; set; }
        public int cant_maxima { get; set; }
        public string prefijo { get; set; }
        public string flg_aprobador { get; set; }
        public string dsc_abreviatura { get; set; }
        public int contador { get; set; }
        public DateTime fch_aprobado_reg { get; set; }
        public string cod_movimiento { get; set; }
        public DateTime fch_creacion { get; set; }
        public decimal imp_entregado { get; set; }
        public string dsc_observacion { get; set; }
        public int ctd_comprobantes { get; set; }
        public decimal imp_monto { get; set; }
        public string dsc_ruc { get; set; }
        public DateTime fch_pago_programado { get; set; }
        public string cod_estado_pago { get; set; }
        public DateTime fch_pago_ejecutado { get; set; }
        public string flg_inventario { get; set; }
        public string flg_activo_fijo { get; set; }
        public class EcuentasPagar : EAprobaciones
        {
            public string dsc_usuario { get; set; }
            public string cod_usuario { get; set; }
            public string cod_cargo { get; set; }
            public string dsc_cargo { get; set; }
            public string cod_trabajador { get; set; }
            public int cod_ceco { get; set; }
            public string dsc_ceco { get; set; }
            public string codigo_ceco { get; set; }
            public string dsc_nombres_completos { get; set; }
            public decimal imp_totaldesde { get; set; }
            public int cod_aprobador { get; set; }
        }
        public class  eTrabajador:EAprobaciones
        {
            public string dsc_usuario { get; set; }
            public string cod_usuario { get; set; }
        }
        public class Ecajachica : EAprobaciones
        {
            public string cod_usuario { get; set; }
            public string dsc_tipo { get; set; }
            public string cod_caja { get; set; }
            public string dsc_entregado { get; set; }
            public string dsc_ajuste { get; set; }
            public string dsc_documento_entregado_a { get; set; }
            public string cod_trabajador { get; set; }
            public string cod_sede_empresa { get; set; }
            public string dsc_documento { get; set; }
            public int num_Anho { get; set; }

        }
        public class EEntregasRendir : EAprobaciones
        {
            public string cod_movimiento { get; set; }
            public string cod_tipo { get; set; }
            public string dsc_tipo { get; set; }
            public string dsc_documento { get; set; }
            public string dsc_entregado { get; set; }
            public string cod_estado_aprobado { get; set; }
            public string cod_entregarendir { get; set; }
            public string cod_sede_empresa { get; set; }
            public int num_Anho { get; set; }
        }
        public class eFaturaProveedor_ProgramacionPagos : EAprobaciones
        {
            public bool Sel { get; set; }
            public int num_linea { get; set; }
            public DateTime fch_pago { get; set; }
            public decimal imp_pago { get; set; }
            public string cod_pagar_a { get; set; }
            public string dsc_pagar_a { get; set; }
            public string cod_estado { get; set; }
            public string dsc_estado { get; set; }
            public DateTime fch_ejecucion { get; set; }
            public string cod_usuario_ejecucion { get; set; }
            public string dsc_usuario_ejecucion { get; set; }
            public DateTime fch_cambio { get; set; }
            public string cod_tipo_prog { get; set; }
            public string dsc_tipo_prog { get; set; }
            public string cod_formapago { get; set; }
            public string dsc_formapago { get; set; }
            public string cod_destinatario { get; set; }
            public string dsc_destinatario { get; set; }
            public Int32 num_linea_banco { get; set; }
            public string cod_banco_empresa { get; set; }
            public string dsc_banco_empresa { get; set; }
            public string dsc_cta_bancaria_empresa { get; set; }
            public string cod_bloque_pago { get; set; }
            public DateTime fch_bloque_pago { get; set; }
            public string cod_usuario_bloque_pago { get; set; }
            public Int32 num_linea_banco_prov { get; set; }
            public string cod_banco_prov { get; set; }
            public string dsc_banco_prov { get; set; }
            public string cod_tipo_cuenta_prov { get; set; }
            public string cod_tipo_documento_prov { get; set; }
            public string num_documento_prov { get; set; }
            public string dsc_cta_bancaria_prov { get; set; }
            public string dsc_cta_interbancaria_prov { get; set; }
            public string dsc_glosa_principal { get; set; }
            public string cod_moneda_prog { get; set; }
            public int cant_lineasprog { get; set; }
            public decimal sum_totalprog { get; set; }
            public string cod_usuario_registro { get; set; }
            
        }
    }
}
