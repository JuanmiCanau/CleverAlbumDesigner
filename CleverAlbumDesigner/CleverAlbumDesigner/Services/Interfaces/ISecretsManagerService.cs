namespace CleverAlbumDesigner.Services.Interfaces
{
    public interface ISecretsManagerService
    {
        Task<string> GetConnectionString();
    }
}
