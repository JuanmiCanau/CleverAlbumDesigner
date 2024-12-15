using CleverAlbumDesigner.Entities;

namespace CleverAlbumDesigner.Managers.Interfaces
{
    public interface IPhotoManager
    {
        Task AddPhotoAsync(Stream fileStream,Guid fileId ,string fileName, string originalName,string contentType, string sessionId);    
        Task DeletePhotoAsync(Guid photoId);
        Task<List<PhotoDto>> GetAllUnassignedPhotosAsync(string sessionId);
    }
}
