using BE_BackOffice;
using System.Globalization;
using System.IO;
using System;
using System.Threading.Tasks;
using UI_BackOffice.Tools.Interfaces;
using UI_BackOffice.Tools.OneDriveServices.DTOs;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using DevExpress.Office.Utils;
using IMPERIUM_Sistema.BE_Sistema;
using DevExpress.XtraSplashScreen;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;

namespace UI_BackOffice.Tools.OneDriveServices
{
    public class RequerimientoOneDriveService : OneDriveService, IOneDriveService<RequerimientoAdjuntarDTO, RequerimientoDescargarDTO>
    {
        private readonly UnitOfWork _unitOfWork;
        public RequerimientoOneDriveService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AttachFile(RequerimientoAdjuntarDTO uploadParams)
        {
            /*-----*Obtener listado de Folders*-----*/
            string descripcionCarpeta = "Requerimiento Logistica";
            DateTime fechaRegistro = Convert.ToDateTime(uploadParams.FechaRegistro);

            /*-----*Validar si el periodo se encuentra abierto*-----*/
            eFacturaProveedor validarPeriodoTributario = _unitOfWork.Factura
                    .Obtener_PeriodoTributario<eFacturaProveedor>(50, fechaRegistro.ToString("MM-yyyy"), uploadParams.CodigoEmpresa);

            if (validarPeriodoTributario != null && validarPeriodoTributario.flg_cerrado == "SI")
            {
                eFacturaProveedor periodoTributarioData = _unitOfWork.Factura
                    .Obtener_PeriodoTributario<eFacturaProveedor>(51, "", uploadParams.CodigoEmpresa);

                if (!DateTime.TryParseExact(periodoTributarioData.periodo_tributario, "MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime periodoTributario))
                {
                    throw new Exception("No se pudo convertir la cadena en un objeto DateTime.");
                }
                /*-----*Generar la fecha de registro*-----*/
                fechaRegistro = new DateTime(
                    periodoTributario.Month == 12 ? periodoTributario.Year + 1 : periodoTributario.Year,
                    periodoTributario.Month == 12 ? 1 : periodoTributario.Month, 01);
            }

            /*Seleccionar los archivos a adjuntar*/
            var file = ArchivoHelper.ImportarDocumento(new string[] { "pdf", "xml" });
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((new FileInfo(file.FileName).Length / 1024) >= 4000)
                { throw new Exception("Solo puede subir archivos hasta 5MB de tamaño."); }

                string idArchivoAnho = "", idArchivoMes = "", Extension = Path.GetExtension(file.SafeFileName);
                var idArchivoPDF = ""; var idArchivoXML = "";

                /*Asignación de Variables*/
                var variablesDocumento = (
                    varPathOrigen: file.FileName,
                    varNombreArchivo: "REQUERIMIENTOS_LOGISTICA" + "-" + uploadParams.CodigoRequerimiento + Path.GetExtension(file.SafeFileName),
                    varNombreArchivoSinExtension: Path.GetExtension(file.SafeFileName));

                _unitOfWork.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");
                eEmpresa empresa = _unitOfWork.Factura.ObtenerDatosEmpresa<eEmpresa>(12, uploadParams.CodigoEmpresa);
                if (empresa.ClientIdOnedrive == null || empresa.ClientIdOnedrive == "")
                { throw new Exception("Debe configurar los datos del Onedrive de la empresa asignada"); }

                /*-----*Configurarción del OneDrive*-----*/
                var empresaOneDriveEnSQL = _unitOfWork.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(
                    opcion: 26,
                    cod_empresa: uploadParams.CodigoEmpresa,
                    Anho: Convert.ToDateTime(fechaRegistro).Year,
                    dsc_Carpeta: descripcionCarpeta);

                var oneDriveCarpetaAnhoEnSQL = _unitOfWork.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(
                    opcion: 13,
                    cod_empresa: uploadParams.CodigoEmpresa,
                    Anho: fechaRegistro.Year,
                    dsc_Carpeta: descripcionCarpeta);

                var oneDriveCarpetaMesEnSQL = _unitOfWork.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(
                    opcion: 14, cod_empresa: uploadParams.CodigoEmpresa,
                    Anho: fechaRegistro.Year,
                    Mes: fechaRegistro.Month,
                    dsc_Carpeta: descripcionCarpeta);

                //var objEntrada = _unitOfWork.Logistica.Obtener_DatosLogistica<eAlmacen.eEntrada_Cabecera>(
                //    opcion: 31,
                //    cod_almacen: uploadParams.CodAlmacen,
                //    cod_empresa: uploadParams.CodigoEmpresa,
                //    cod_sede_empresa: uploadParams.CodSedeEmpresa,
                //    cod_entrada: uploadParams.CodEntrada);

                var oneDrive = await OneDriveStart(
                    oneDriveConfiguration: new OneDriveConfiguration(),
                    empresa: empresa,
                    pathOrigen: variablesDocumento.varPathOrigen,
                    nombreArchivo: variablesDocumento.varNombreArchivo,
                    nombreArchivoSinExtension: variablesDocumento.varNombreArchivoSinExtension,
                    empresaOneDriveEnSQL: empresaOneDriveEnSQL,
                    oneDriveCarpetaAnhoEnSQL: oneDriveCarpetaAnhoEnSQL,
                    oneDriveCarpetaMesEnSQL: oneDriveCarpetaMesEnSQL,
                    fechaRegistro: fechaRegistro//,
                                                //fechaDocumentoMover: objEntrada.fch_documento,
                                                //idPDF: objEntrada.idPDF, // verificar si se añade FechaPeriodo
                                                //PDF: true,
                                                //XML: false,
                                                //opcion: "ELIMINAR"
                   );

                //idArchivoAnho = base.IdArchivoAnho;
                //idArchivoMes = base.IdArchivoAnho;

                // _--- NOTA, cuando se carga por segunda ves, reemplaza el documento existente, revisar porque se  ha puesta esta opcion de reemplazar...

                ////////////////////////////////////////////////////////// REEMPLAZAR DOCUMENTO DE ONEDRIVE ////////////////////////////////////////////////////////
                //var obj = _unitOfWork.Logistica.Obtener_DatosLogistica<eRequerimiento>(
                //    opcion: 33,
                //    cod_empresa: uploadParams.CodigoEmpresa,
                //    cod_sede_empresa: uploadParams.CodSedeEmpresa,
                //    cod_requerimiento: uploadParams.CodigoRequerimiento);
                ////////////////////////////// ELIMINAR DOCUMENTO DE ONEDRIVE ////////////////////////////
                //if (obj.idPDF != null && obj.idPDF != "" && Extension.ToLower() == ".pdf")
                //    MoverEliminar_ArchivoDeOneDrive(
                //        oneDriveConfiguration: oneDrive,
                //        empresaOneDriveEnSQL: empresaOneDriveEnSQL,
                //        oneDriveCarpetaAnho: oneDriveCarpetaAnhoEnSQL,
                //        oneDriveCarpetaMes: oneDriveCarpetaMesEnSQL,
                //        idPDF: obj.idPDF,
                //        fechaDocumento: fechaRegistro,//obj.fch_documento,// ver de donde se ha asignado la fch_documento
                //        empresa: empresa,
                //        PDF: true,
                //        XML: false,
                //        opcion: "ELIMINAR");
                ////await MoverEliminar_ArchivoDeOneDrive(obj, new DateTime(), true, false, "ELIMINAR");
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //crea archivo en el OneDrive
                byte[] data = System.IO.File.ReadAllBytes(oneDrive.VarPathOrigen);
                using (Stream stream = new MemoryStream(data))
                {
                    string res = "";
                    int opcion = Extension.ToLower() == ".pdf" ? 1 : Extension.ToLower() == ".xml" ? 2 : 0;
                    if (opcion == 1 || opcion == 2)
                    {
                        var DriveItem = await oneDrive.GraphClient.Me.Drive.Items[IdArchivoMes].ItemWithPath(oneDrive.VarNombreArchivo).Content.Request().PutAsync<Microsoft.Graph.DriveItem>(stream);
                        idArchivoPDF = opcion == 1 ? DriveItem.Id : "";
                        idArchivoXML = opcion == 2 ? DriveItem.Id : "";

                        eFacturaProveedor objFact = new eFacturaProveedor();
                        //objFact.tipo_documento = uploadParams.TipoDocumento;
                        //objFact.serie_documento = uploadParams.SerieDocumento;
                        //objFact.numero_documento = decimal.Parse(uploadParams.NumeroDocumento);
                        objFact.cod_proveedor = "";
                        objFact.idPDF = idArchivoPDF;
                        objFact.idXML = idArchivoXML;
                        //objFact.NombreArchivo = varNombreArchivo;
                        objFact.NombreArchivo = oneDrive.VarNombreArchivoSinExtension;
                        objFact.cod_empresa = uploadParams.CodigoEmpresa;
                        objFact.idCarpetaAnho = IdArchivoAnho;
                        objFact.idCarpetaMes = IdArchivoMes;

                        res = _unitOfWork.Factura.ActualizarInformacionDocumentos(
                            opcion: 6,
                            objFact,
                            idCarpeta: empresaOneDriveEnSQL.idCarpeta,// targetItemFolderId, 
                            Anho: fechaRegistro.Year.ToString(),
                            Mes: $"{fechaRegistro.Month:00}",
                            dsc_Carpeta: descripcionCarpeta,
                            cod_sede_empresa: uploadParams.CodSedeEmpresa,
                            cod_requerimiento: uploadParams.CodigoRequerimiento
                            );
                    }

                    if (res == "OK")
                    {
                        HNG.MessageSuccess("Se registró el documento satisfactoriamente", "Información");

                        //btnVerPDF.Enabled = true;
                        SplashScreenManager.CloseForm();
                    }
                    else
                    {
                        SplashScreenManager.CloseForm();
                        throw new Exception("Hubieron problemas al registrar el documento");
                    }
                }
            }
        }

        public async Task DownloadFile(RequerimientoDescargarDTO downloadParams)
        {
            //_unitOfWork.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");

            eEmpresa empresa = _unitOfWork.Factura.ObtenerDatosEmpresa<eEmpresa>(
                opcion: 12,
                cod_empresa: downloadParams.CodEmpresa);


            var oneDrive = await OneDriveDownload(
                oneDriveConfiguration: new OneDriveConfiguration(),
                empresa: empresa,
                idPDF: downloadParams.IdPDF,
                idXML: downloadParams.IdPDF,
                isPdfNoXml: downloadParams.IsPdf
                );
            //_unitOfWork.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");

            //SplashScreenManager.CloseForm();
        }
    }
}
