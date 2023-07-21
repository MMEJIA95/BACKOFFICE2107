namespace UI_BackOffice.Tools.OneDriveServices.DTOs
{
    public  class GuiaRemisionAdjuntarDTO
    {
        public GuiaRemisionAdjuntarDTO(
            string fechaRegistro, 
            string codigoEmpresa, 
            string codigoRequerimiento, 
            string codAlmacen, 
            string codSedeEmpresa, 
            string codEntrada, 
            string tipoDocumento, 
            string serieDocumento, 
            string numeroDocumento)
        {
            FechaRegistro = fechaRegistro;
            CodigoEmpresa = codigoEmpresa;
            CodigoRequerimiento = codigoRequerimiento;
            CodAlmacen = codAlmacen;
            CodSedeEmpresa = codSedeEmpresa;
            CodEntrada = codEntrada;
            TipoDocumento = tipoDocumento;
            SerieDocumento = serieDocumento;
            NumeroDocumento = numeroDocumento;
        }

        public string FechaRegistro { get; private set; }
        public string CodigoEmpresa { get; private set; }
        public string CodigoRequerimiento { get; private set; }
        public string CodAlmacen { get; private set; }
        public string CodSedeEmpresa { get; private set; }
        public string CodEntrada { get; private set; }
        public string TipoDocumento { get; private set; }
        public string SerieDocumento { get; private set; }
        public string NumeroDocumento { get; private set; }
    }
}
