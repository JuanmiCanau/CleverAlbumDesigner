using CleverAlbumDesigner.Models;

namespace CleverAlbumDesigner.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        Task AddAlbumAsync(Album album);
        Task<List<Album>> GetAllAlbumsAsync(string sessionId);
        Task DeleteAlbumAsync(Guid albumId);
        Task<Album?> GetAlbumByIdAsync(Guid albumId);
    }
}
