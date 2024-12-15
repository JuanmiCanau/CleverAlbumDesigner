using CleverAlbumDesigner.Entities;
using CleverAlbumDesigner.Models;

namespace CleverAlbumDesigner.Managers.Interfaces
{
    public interface IAlbumManager
    {
        Task<Album?> GenerateAlbumAsync(string themeName, string? suffix, string sessionId);
        Task<List<Album>> GetAllAlbumsAsync(string sessionId);
        Task<List<PhotoDto>> GetAlbumPhotosAsync(Guid albumId);
        Task DeleteAlbumAsync(Guid albumId);
        Task<Entities.ZipFile> GenerateAlbumZipAsync(Guid albumId);
    }
}
