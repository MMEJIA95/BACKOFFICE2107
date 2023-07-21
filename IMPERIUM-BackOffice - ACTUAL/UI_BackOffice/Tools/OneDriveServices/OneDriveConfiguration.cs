using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using UI_BackOffice.Tools.Interfaces;

namespace UI_BackOffice.Tools.OneDriveServices
{
    public class OneDriveConfiguration : IOneDriveConfiguration
    {
        public GraphServiceClient GraphClient { get; private set; }
        public AuthenticationResult AuthResult { get; private set; } = null;
        public string[] Scopes { get; private set; } = new string[] { "Files.ReadWrite.All" };
        public string VarPathOrigen { get; private set; } = string.Empty;
        public string VarNombreArchivo { get; private set; } = string.Empty;
        public string VarNombreArchivoSinExtension { get; private set; } = string.Empty;
        public string ClientId { get; private set; } = string.Empty;
        public string TenantId { get; private set; } = string.Empty;
        public string Instance { get; private set; } = "https://login.microsoftonline.com/";
        public IPublicClientApplication PublicClientApp { get { return _clientApp; } }

        private IPublicClientApplication _clientApp;

        public void Appl()
        {
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
               .WithAuthority($"{Instance}{TenantId}")
               .WithDefaultRedirectUri()
               .Build();
            TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }

        public void AsignarVariables(string varPathOrigen, string varNombreArchivo, string varNombreArchivoSinExtension)
        {
            VarPathOrigen = varPathOrigen;
            VarNombreArchivo = varNombreArchivo;
            VarNombreArchivoSinExtension = varNombreArchivoSinExtension;
        }
        public void AsignarCredenciales(string clientId, string tenantId, string instance = "https://login.microsoftonline.com/")
        {
            ClientId = clientId;
            TenantId = tenantId;
            Instance = instance;
        }

        public void SetAuthResult(AuthenticationResult authentication) => AuthResult = authentication;
        public void SetGraphClient(GraphServiceClient graphServiceClient) => GraphClient = graphServiceClient;
    }
}
