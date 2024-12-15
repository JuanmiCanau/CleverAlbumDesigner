using CleverAlbumDesigner.Models;

namespace CleverAlbumDesigner.Repositories.Interfaces
{
    public interface IPhotoRepository
    {
        Task<Photo?> GetPhotoByIdAsync(Guid id);
        Task AddPhotoAsync(Photo photo);
        Task DeletePhotoAsync(Guid id);
        Task<List<Photo>> GetAllUnassignedAsync(string sessionId);
        Task<List<Photo>> GetPhotosByColorIdsAsync(List<Guid> colorIds, string sessionId);
        Task UpdatePhotosAsync(List<Photo> photos);
        Task<List<Photo>> GetPhotosByAlbumIdAsync(Guid albumId);
        Task DeletePhotosByAlbumIdAsync(Guid albumId);
    }
}
