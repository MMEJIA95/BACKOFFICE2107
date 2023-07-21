using Microsoft.Identity.Client;

namespace UI_BackOffice.Tools.Interfaces
{
    public interface IOneDriveConfiguration
    {
        Microsoft.Graph.GraphServiceClient GraphClient { get; }
        AuthenticationResult AuthResult { get; }
        string[] Scopes { get; }
        string VarPathOrigen { get; }
        string VarNombreArchivo { get; }
        string VarNombreArchivoSinExtension { get; }
        string ClientId { get; }
        string TenantId { get; }
        string Instance { get; }
        IPublicClientApplication PublicClientApp { get; }
        void Appl();
        void AsignarVariables(string varPathOrigen, string varNombreArchivo, string varNombreArchivoSinExtension);
        void AsignarCredenciales(string clientId, string tenantId, string instance = "https://login.microsoftonline.com/");
        void SetAuthResult(AuthenticationResult authentication);
        void SetGraphClient(Microsoft.Graph.GraphServiceClient graphServiceClient);
    }
}
