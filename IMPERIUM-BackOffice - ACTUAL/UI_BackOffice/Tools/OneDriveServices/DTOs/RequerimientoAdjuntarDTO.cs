using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_BackOffice.Tools.OneDriveServices.DTOs
{
    public class RequerimientoAdjuntarDTO
    {
        public RequerimientoAdjuntarDTO(string fechaRegistro, string codigoEmpresa, string codigoRequerimiento, string codSedeEmpresa)
        {
            FechaRegistro = fechaRegistro;
            CodigoEmpresa = codigoEmpresa;
            CodigoRequerimiento = codigoRequerimiento;
            CodSedeEmpresa = codSedeEmpresa;
        }

        public string FechaRegistro { get; private set; }
        public string CodigoEmpresa { get; private set; }
        public string CodigoRequerimiento { get; private set; }
        public string CodSedeEmpresa { get; private set; }

        //public string CodAlmacen { get; private set; }
        //public string CodEntrada { get; private set; }
        //public string TipoDocumento { get; private set; }
        //public string SerieDocumento { get; private set; }
        //public string NumeroDocumento { get; private set; }
    }
}
