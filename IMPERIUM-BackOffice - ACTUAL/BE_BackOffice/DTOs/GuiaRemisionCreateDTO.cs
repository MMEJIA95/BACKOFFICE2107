namespace BE_BackOffice.DTOs
{
    public class GuiaRemisionCreateDTO
    {
        public GuiaRemisionCreateDTO(string cod_guiaremision, string cod_almacen, string cod_empresa, string cod_sede_empresa, string cod_requerimiento)
        {
            this.cod_guiaremision = cod_guiaremision;
            this.cod_almacen = cod_almacen;
            this.cod_empresa = cod_empresa;
            this.cod_sede_empresa = cod_sede_empresa;
            this.cod_requerimiento = cod_requerimiento;
        }

        public string cod_guiaremision { get; private set; }
        public string cod_almacen { get; private set; }
        public string cod_empresa { get; private set; }
        public string cod_sede_empresa { get; private set; }
        public string cod_requerimiento { get; private set; }
    }
}
