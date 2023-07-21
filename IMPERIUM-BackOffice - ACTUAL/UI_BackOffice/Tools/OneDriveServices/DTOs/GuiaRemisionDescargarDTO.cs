namespace UI_BackOffice.Tools.OneDriveServices.DTOs
{
    public class GuiaRemisionDescargarDTO
    {
        public GuiaRemisionDescargarDTO(string codAlmacen, string codEmpresa, string codSedeEmpresa, string codEntrada)
        {
            CodAlmacen = codAlmacen;
            CodEmpresa = codEmpresa;
            CodSedeEmpresa = codSedeEmpresa;
            CodEntrada = codEntrada;
        }

        public string CodAlmacen { get; private set; }
        public string CodEmpresa { get; private set; }
        public string CodSedeEmpresa { get; private set; }
        public string CodEntrada { get; private set; }
    }
}
