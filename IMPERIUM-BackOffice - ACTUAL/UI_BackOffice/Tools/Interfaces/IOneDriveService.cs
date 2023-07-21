using System.Threading.Tasks;

namespace UI_BackOffice.Tools.Interfaces
{
    public interface IOneDriveService<TUploadParams, TDownloadParams>
        where TUploadParams : class
        where TDownloadParams : class
    {
        Task AttachFile(TUploadParams uploadParams);
        Task DownloadFile(TDownloadParams downloadParams);
    }
}

