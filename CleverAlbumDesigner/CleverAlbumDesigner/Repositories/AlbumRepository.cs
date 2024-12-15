using CleverAlbumDesigner.Data;
using CleverAlbumDesigner.Models;
using CleverAlbumDesigner.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleverAlbumDesigner.Repositories
{
    public class AlbumRepository(AppDbContext context) : IAlbumRepository
    {
        private readonly AppDbContext _context = context;
        public async Task AddAlbumAsync(Album album)
        {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();       
        }
        public async Task<List<Album>> GetAllAlbumsAsync(string sessionId)
        {
            return await _context.Albums.Where(album => album.SessionId == sessionId).ToListAsync();
        }

        public async Task<Album?> GetAlbumByIdAsync(Guid albumId)
        {
            return await _context.Albums.FindAsync(albumId);
        }

        public async Task DeleteAlbumAsync(Guid albumId)
        {
            var album = await _context.Albums.FindAsync(albumId);
            if (album != null)
            {
                _context.Albums.Remove(album);
                await _context.SaveChangesAsync();
            }
        }
    }
}
