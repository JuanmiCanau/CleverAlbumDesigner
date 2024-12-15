namespace CleverAlbumDesigner.Services.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(string fileName, Stream fileStream, string contentType);
        string GeneratePreSignedUrl(string fileName, int expirationInMinutes = 30);
        Task DeleteFileAsync(string fileName);
        Task DeleteFilesAsync(List<string> fileNames);
        Task<Stream> GetFileStreamAsync(string key);
    }
}
