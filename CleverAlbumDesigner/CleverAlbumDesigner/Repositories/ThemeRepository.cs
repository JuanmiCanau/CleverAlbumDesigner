using CleverAlbumDesigner.Data;
using CleverAlbumDesigner.Models;
using CleverAlbumDesigner.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleverAlbumDesigner.Repositories
{
    public class ThemeRepository(AppDbContext context) : IThemeRepository
    {
        private readonly AppDbContext _context = context;
         
        public async Task<List<Theme>> GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }

        public async Task<Theme?> GetThemeByNameAsync(string themeName)
        {
            return await _context.Themes.FirstOrDefaultAsync(t => t.Name.Equals(themeName));
        }
    }
}
