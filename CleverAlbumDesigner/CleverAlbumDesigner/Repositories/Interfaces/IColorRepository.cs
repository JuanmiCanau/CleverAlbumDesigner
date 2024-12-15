using CleverAlbumDesigner.Models;

namespace CleverAlbumDesigner.Repositories.Interfaces
{
    public interface IColorRepository
    {
        Task<List<Color>>GetAllColors();
    }
}
