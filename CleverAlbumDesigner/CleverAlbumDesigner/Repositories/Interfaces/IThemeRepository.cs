using CleverAlbumDesigner.Models;

namespace CleverAlbumDesigner.Repositories.Interfaces
{
    public interface IThemeRepository
    {
        Task<List<Theme>> GetAllThemesAsync();
        Task<Theme?> GetThemeByNameAsync(string themeName);        
    }
}
