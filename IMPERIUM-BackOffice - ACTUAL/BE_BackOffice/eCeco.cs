using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_BackOffice
{
    public class eCeco
    {
        public string cod_empresa { get; set; }
        public int num_item { get; set; }
        public string cod_nivel1 { get; set; }
        public string dsc_nivel1 { get; set; }
        public string cod_nivel2 { get; set; }
        public string dsc_nivel2 { get; set; }
        public string cod_nivel3 { get; set; }
        public string dsc_nivel3 { get; set; }
        public string cod_nivel4 { get; set; }
        public string dsc_nivel4 { get; set; }
        public string dsc_ceco_final { get; set; }
        public string flg_activo { get; set; }

        public string dsc_pref_ceco { get; set; }
        public string dsc_pref_ceco_NUEVO { get; set; }

        public string cod_CECO { get; set; }
        public string dsc_CECO { get; set; }
        public string dsc_CECO_abrev { get; set; }
        public decimal porc_distribucion { get; set; }


        public int num_nivel { get; set; }
        public string dsc_concepto { get; set; }
        public string dsc_nombre_objeto { get; set; }
        public int ctd_digitos { get; set; }
    }

}
