using CleverAlbumDesigner.Data;
using CleverAlbumDesigner.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleverAlbumDesigner.Repositories
{
    public class ThemeColorRepository(AppDbContext context) : IThemeColorRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Guid>> GetColorIdsByThemeIdAsync(Guid themeId)
        {
            return await _context.ThemeColor
                .Where(tc => tc.ThemeId == themeId)
                .Select(tc => tc.ColorId)
                .ToListAsync();
        }
    }
}
