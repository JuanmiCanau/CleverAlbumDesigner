namespace CleverAlbumDesigner.Repositories.Interfaces
{
    public interface IThemeColorRepository
    {
        Task<List<Guid>> GetColorIdsByThemeIdAsync(Guid themeId);
    }
}
