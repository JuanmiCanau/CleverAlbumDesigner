using CleverAlbumDesigner.Data;
using CleverAlbumDesigner.Models;
using CleverAlbumDesigner.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleverAlbumDesigner.Repositories
{
    public class ColorRepository(AppDbContext context) : IColorRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<List<Color>> GetAllColors()
        {
            return await _context.Colors.ToListAsync();
        }
    }
}
