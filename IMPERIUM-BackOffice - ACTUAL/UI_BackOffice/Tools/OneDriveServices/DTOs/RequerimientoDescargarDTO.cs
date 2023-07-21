using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_BackOffice.Tools.OneDriveServices.DTOs
{
    public class RequerimientoDescargarDTO
    {
        public RequerimientoDescargarDTO(string idPDF, string idXML, string codEmpresa, bool isPdf)
        {
            IdPDF = idPDF;
            IdXML = idXML;
            CodEmpresa = codEmpresa;
            IsPdf = isPdf;
        }

        public string IdPDF { get; private set; }
        public string IdXML { get; private set; }
        public string CodEmpresa { get; private set; }
        public bool IsPdf { get; private set; }
    }
}
