using CleverAlbumDesigner.Data;
using CleverAlbumDesigner.Models;
using CleverAlbumDesigner.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleverAlbumDesigner.Repositories
{
    public class PhotoRepository(AppDbContext context) : IPhotoRepository
    {
        private readonly AppDbContext _context = context;

        
        public async Task AddPhotoAsync(Photo photo)
        {
            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePhotoAsync(Guid id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo != null)
            {
                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePhotosByAlbumIdAsync(Guid albumId)
        {
            var photosToDelete = _context.Photos.Where(photo => photo.AlbumId == albumId);

            _context.Photos.RemoveRange(photosToDelete); 
            await _context.SaveChangesAsync();
        }

        public async Task<Photo?> GetPhotoByIdAsync(Guid id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public async Task<List<Photo>> GetAllUnassignedAsync(string sessionId)
        {
            return await _context.Photos.Where(x => x.AlbumId == null && x.SessionId.Equals(sessionId)).ToListAsync();
        }

        public async Task<List<Photo>> GetPhotosByColorIdsAsync(List<Guid> colorIds, string sessionId)
        {
            return await _context.Photos.Where(x => x.DominantColorId.HasValue && x.AlbumId == null && x.SessionId.Equals(sessionId) && colorIds.Contains(x.DominantColorId.Value)).ToListAsync();
        }
        public async Task<List<Photo>> GetPhotosByAlbumIdAsync(Guid albumId)
        {
            return await _context.Photos.Where(photo => photo.AlbumId == albumId).ToListAsync();
        }

        public async Task UpdatePhotosAsync(List<Photo> photos)
        {
            _context.Photos.UpdateRange(photos);
            await _context.SaveChangesAsync();
        }
    }
}
