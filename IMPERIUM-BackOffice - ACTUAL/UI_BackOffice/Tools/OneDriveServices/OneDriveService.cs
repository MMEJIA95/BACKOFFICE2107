using BE_BackOffice;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using UI_BackOffice.Tools.Interfaces;

namespace UI_BackOffice.Tools.OneDriveServices
{
    public abstract class OneDriveService
    {
        protected async Task<IOneDriveConfiguration> OneDriveStart(
            IOneDriveConfiguration oneDriveConfiguration,
            eEmpresa empresa,
            string pathOrigen,
            string nombreArchivo,
            string nombreArchivoSinExtension,
            eEmpresa.eOnedrive_Empresa empresaOneDriveEnSQL,
            eEmpresa.eOnedrive_Empresa oneDriveCarpetaAnhoEnSQL,
            eEmpresa.eOnedrive_Empresa oneDriveCarpetaMesEnSQL,
            DateTime fechaRegistro)
        //,
        //DateTime fechaDocumentoMover,
        //string idPDF,
        //bool PDF,
        //bool XML,
        //string opcion)
        {
            /*Asignación de Variables*/
            oneDriveConfiguration.AsignarVariables(
                varPathOrigen: pathOrigen,
                varNombreArchivo: nombreArchivo,
                varNombreArchivoSinExtension: nombreArchivoSinExtension);

            //_unitOfWork.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");
            if (empresa.ClientIdOnedrive == null || empresa.ClientIdOnedrive == "")
            { throw new Exception("Debe configurar los datos del Onedrive de la empresa asignada"); }

            /*-----*Asignación de Credenciales*-----*/
            oneDriveConfiguration.AsignarCredenciales(
                clientId: empresa.ClientIdOnedrive,
                tenantId: empresa.TenantOnedrive);

            /*-----*Ejecutamos la configuración del OneDrive*-----*/
            oneDriveConfiguration.Appl();

            var app = oneDriveConfiguration.PublicClientApp;
            var credencialDeAcceso = (emailAddress: empresa.UsuarioOnedrive, password: empresa.ClaveOnedrive);

            var securePassword = new SecureString();
            foreach (char c in credencialDeAcceso.password)
                securePassword.AppendChar(c);

            oneDriveConfiguration.SetAuthResult(
                await app.AcquireTokenByUsernamePassword(
                    oneDriveConfiguration.Scopes,
                    credencialDeAcceso.emailAddress,
                    securePassword).ExecuteAsync()
                );

            oneDriveConfiguration.SetGraphClient(
                new GraphServiceClient(
                    new DelegateAuthenticationProvider((requestMessage) =>
                    {
                        requestMessage
                        .Headers
                        .Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", oneDriveConfiguration.AuthResult.AccessToken);
                        return Task.FromResult(0);
                    })
                    )
                );

            ConfiguracionAccesoDeCarpetas(
                oneDriveConfiguration,
                empresaOneDriveEnSQL,
                oneDriveCarpetaAnhoEnSQL,
                oneDriveCarpetaMesEnSQL,
                fechaRegistro
                );



            //MoverEliminar_ArchivoDeOneDrive(
            //    oneDriveConfiguration: oneDriveConfiguration,
            //    empresaOneDriveEnSQL: empresaOneDriveEnSQL,
            //    oneDriveCarpetaAnho: oneDriveCarpetaAnhoEnSQL,
            //    oneDriveCarpetaMes: oneDriveCarpetaMesEnSQL,
            //    idPDF: idPDF,
            //    fechaDocumento: fechaDocumentoMover,
            //    empresa: empresa,
            //    PDF: PDF,
            //    XML: XML,
            //    opcion: opcion);

            return oneDriveConfiguration;
        }

        protected string IdArchivoAnho;
        protected string IdArchivoMes;
        protected async void ConfiguracionAccesoDeCarpetas(
            IOneDriveConfiguration oneDriveConfiguration,
            eEmpresa.eOnedrive_Empresa empresaOneDriveEnSQL,
            eEmpresa.eOnedrive_Empresa oneDriveCarpetaAnho,
            eEmpresa.eOnedrive_Empresa oneDriveCarpetaMes,
            DateTime fechaRegistro)
        {
            var targetItemFolderId = empresaOneDriveEnSQL.idCarpeta;
            /*-----*Generar Acceso a la Carpeta(AÑO)*-----*/
            if (oneDriveCarpetaAnho == null)// Si no existe el folder, lo crea.
            {
                var driveItem = new Microsoft.Graph.DriveItem
                {
                    Name = fechaRegistro.Year.ToString(),
                    Folder = new Microsoft.Graph.Folder { },
                    AdditionalData = new Dictionary<string, object>()
                            { {"@microsoft.graph.conflictBehavior", "rename"} }
                };
                var driveItemInfo = await oneDriveConfiguration.GraphClient.Me.Drive.Items[targetItemFolderId].Children.Request().AddAsync(driveItem);
                IdArchivoAnho = driveItemInfo.Id;
            }
            else // Si existe el folder, obtenemos el ID
            {
                IdArchivoAnho = oneDriveCarpetaAnho.idCarpetaAnho;
            }
            var targetItemFolderIdAnho = IdArchivoAnho;

            /*-----*Generar Acceso a la Carpeta(MES)*-----*/
            if (oneDriveCarpetaMes == null)
            {
                var driveItem = new Microsoft.Graph.DriveItem
                {
                    Name = $"{fechaRegistro.Month:00}" + ". " + fechaRegistro.ToString("MMMM").ToUpper(),
                    Folder = new Microsoft.Graph.Folder { },
                    AdditionalData = new Dictionary<string, object>() { { "@microsoft.graph.conflictBehavior", "rename" } }
                };

                var driveItemInfo = await oneDriveConfiguration.GraphClient.Me.Drive.Items[targetItemFolderIdAnho].Children.Request().AddAsync(driveItem);
                IdArchivoMes = driveItemInfo.Id;
            }
            else //Si existe folder obtener id
            {
                IdArchivoMes = oneDriveCarpetaMes.idCarpetaMes;
            }
        }

        protected async void MoverEliminar_ArchivoDeOneDrive(
            IOneDriveConfiguration oneDriveConfiguration,
            eEmpresa.eOnedrive_Empresa empresaOneDriveEnSQL,
            eEmpresa.eOnedrive_Empresa oneDriveCarpetaAnho,
            eEmpresa.eOnedrive_Empresa oneDriveCarpetaMes,
            string idPDF,
            DateTime fechaDocumento,
            eEmpresa empresa,
            bool PDF,
            bool XML,
            string opcion)
        {
            //string dsc_Carpeta = "Guia Remision";
            //int Anho = obj.fch_documento.Year; int Mes = obj.fch_documento.Month; string NombreMes = obj.fch_documento.Month.ToString("MMMM");
            //string IdArchivoAnho = "", IdArchivoMes = ""; ....  habilitar?????

            //eEmpresa eEmp = unitOfWork.Factura.ObtenerDatosEmpresa<eEmpresa>(12, obj.cod_empresa);

            //if (empresa.ClientIdOnedrive == null || empresa.ClientIdOnedrive == "")
            //{ throw new Exception("Debe configurar los datos del Onedrive de la empresa asignada"); }

            //oneDriveConfiguration.AsignarCredenciales(
            //    clientId: empresa.ClientIdOnedrive,
            //    tenantId: empresa.TenantOnedrive);

            //oneDriveConfiguration.Appl();

            //var app = oneDriveConfiguration.PublicClientApp;
            //var credencialDeAcceso = (emailAddress: empresa.UsuarioOnedrive, password: empresa.ClaveOnedrive);

            //var securePassword = new SecureString();
            //foreach (char c in credencialDeAcceso.password)
            //    securePassword.AppendChar(c);

            //oneDriveConfiguration.SetAuthResult(
            //   await app.AcquireTokenByUsernamePassword(
            //       oneDriveConfiguration.Scopes,
            //       credencialDeAcceso.emailAddress,
            //       securePassword).ExecuteAsync()
            //   );

            //oneDriveConfiguration.SetGraphClient(
            //    new GraphServiceClient(
            //        new DelegateAuthenticationProvider((requestMessage) =>
            //        {
            //            requestMessage
            //            .Headers
            //            .Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", oneDriveConfiguration.AuthResult.AccessToken);
            //            return Task.FromResult(0);
            //        })
            //        )
            //    );

            var targetItemFolderId = opcion != "ELIMINAR" ? empresaOneDriveEnSQL.idCarpeta : "";

            //eFacturaProveedor IdCarpetaAnho = unit.Factura.ObtenerDatosOneDrive<eFacturaProveedor>(13, lkpEmpresaProveedor.EditValue.ToString(), Convert.ToDateTime(dtFechaRegistro.EditValue).Year);
            // eEmpresa.eOnedrive_Empresa IdCarpetaAnho = unitOfWork.Factura.ObtenerDatosOneDrive<eEmpresa.eOnedrive_Empresa>(13, obj.cod_empresa, obj.fch_documento.Year, dsc_Carpeta: dsc_Carpeta);
            if (oneDriveCarpetaAnho == null && opcion != "ELIMINAR") //Si no existe folder lo crea
            {
                var driveItem = new Microsoft.Graph.DriveItem
                {
                    Name = fechaDocumento.Year.ToString(),
                    Folder = new Microsoft.Graph.Folder { },
                    AdditionalData = new Dictionary<string, object>()
                    { {"@microsoft.graph.conflictBehavior", "rename"} }
                };

                var driveItemInfo = await oneDriveConfiguration.GraphClient.Me.Drive.Items[targetItemFolderId].Children.Request().AddAsync(driveItem);
                IdArchivoAnho = driveItemInfo.Id;
            }
            else //Si existe folder obtener id
            {
                IdArchivoAnho = opcion != "ELIMINAR" ? oneDriveCarpetaAnho.idCarpetaAnho : "";
            }
            var targetItemFolderIdAnho = IdArchivoAnho;

            if (oneDriveCarpetaMes == null && opcion != "ELIMINAR")
            {
                var driveItem = new Microsoft.Graph.DriveItem
                {
                    Name = $"{fechaDocumento.Month:00}" + ". " + fechaDocumento.Month.ToString("MMMM").ToUpper(),
                    Folder = new Microsoft.Graph.Folder { },
                    AdditionalData = new Dictionary<string, object>()
                    { {"@microsoft.graph.conflictBehavior", "rename"} }
                };

                var driveItemInfo = await oneDriveConfiguration.GraphClient.Me.Drive.Items[targetItemFolderIdAnho].Children.Request().AddAsync(driveItem);
                IdArchivoMes = driveItemInfo.Id;
            }
            else //Si existe folder obtener id
            {
                IdArchivoMes = opcion != "ELIMINAR" ? oneDriveCarpetaMes.idCarpetaMes : "";
            }

            for (int x = 0; x < 2; x++)
            {
                if (x == 0 && !PDF) continue;
                if (x == 1 && !XML) continue;
                //MOVER ARCHIVO A OTRA CARPETA DEL ONEDRIVE
                var DriveItem = new Microsoft.Graph.DriveItem
                {
                    ParentReference = new Microsoft.Graph.ItemReference
                    {
                        Id = IdArchivoMes
                    },
                    //Name = varNombreArchivo + (x == 0 ? ".pdf" : ".xml") //Se comenta para que siga MANTENIENDO EL NOMBRE ASIGNADO
                };

                //if (opcion == "MOVER") await GraphClient.Me.Drive.Items[x == 0 ? obj.idPDF : obj.idXML].Request().UpdateAsync(DriveItem);
                if (opcion == "ELIMINAR")
                {
                    var objItems = oneDriveConfiguration.GraphClient.Me.Drive.Items;
                    var idDocument = x == 0 ? idPDF : "";
                    if (objItems[idDocument] != null)
                    {
                        try
                        {
                            await objItems[idDocument].Request().DeleteAsync();
                        }
                        catch (Exception) { }

                        //var itemssss = objItems[idDocument].Request();
                        //var dd = itemssss.Select(s => s.Id);
                        ////var sssss = objItems[idDocument].Request().GetAsync().Result;
                        //var item = await objItems[idDocument].Request().GetAsync();
                        //var abc = await objItems[idDocument].Request().GetAsync();

                        //if (itemssss != null)
                        //{
                        //    var ss = "";
                        //}

                    }
                    //var _driveItem =
                    //await oneDriveConfiguration.GraphClient.Me.Drive.Items[];//.Request();.DeleteAsync();
                }
                //if (opcion == "ELIMINAR") await GraphClient.Directory.DeletedItems[x == 0 ? obj.idPDF : obj.idXML].Request().DeleteAsync();
            }
        }
        protected async Task<IOneDriveConfiguration> OneDriveDownload(
            IOneDriveConfiguration oneDriveConfiguration,
            eEmpresa empresa,
            string idPDF="",
            string idXML = "",
            bool isPdfNoXml=true)
        {
            //_unitOfWork.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Por favor espere...", "Cargando...");
            if (empresa.ClientIdOnedrive == null || empresa.ClientIdOnedrive == "")
            { throw new Exception("Debe configurar los datos del Onedrive de la empresa asignada"); }

            /*-----*Asignación de Credenciales*-----*/
            oneDriveConfiguration.AsignarCredenciales(
                clientId: empresa.ClientIdOnedrive,
                tenantId: empresa.TenantOnedrive);

            /*-----*Ejecutamos la configuración del OneDrive*-----*/
            oneDriveConfiguration.Appl();

            var app = oneDriveConfiguration.PublicClientApp;
            var credencialDeAcceso = (emailAddress: empresa.UsuarioOnedrive, password: empresa.ClaveOnedrive);

            var securePassword = new SecureString();
            foreach (char c in credencialDeAcceso.password)
                securePassword.AppendChar(c);

            oneDriveConfiguration.SetAuthResult(
                await app.AcquireTokenByUsernamePassword(
                    oneDriveConfiguration.Scopes,
                    credencialDeAcceso.emailAddress,
                    securePassword).ExecuteAsync()
                );

            oneDriveConfiguration.SetGraphClient(
                new GraphServiceClient(
                    new DelegateAuthenticationProvider((requestMessage) =>
                    {
                        requestMessage
                        .Headers
                        .Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", oneDriveConfiguration.AuthResult.AccessToken);
                        return Task.FromResult(0);
                    })
                    )
                );
            string IdOneDriveDoc = isPdfNoXml ? idPDF : idXML;
            string Extension = isPdfNoXml ? ".pdf" : ".xml";

            try
            {
                var file = await oneDriveConfiguration.GraphClient.Me.Drive.Items[IdOneDriveDoc].CreateLink("view").Request().PostAsync();
                string fileUrl = file.Link.WebUrl;

                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = fileUrl,
                    UseShellExecute = true
                });
            }
            catch (Microsoft.Graph.ServiceException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    HNG.MessageError("No se ha encontrado el arhivo adjunto", "Archivo no existe");
                }
                else
                {
                    HNG.MessageError(ex.Message, "Error en descarga");
                }
            }

            return oneDriveConfiguration;
        }
    }
}
