using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using CleverAlbumDesigner.Services.Interfaces;
using Newtonsoft.Json;

namespace CleverAlbumDesigner.Services
{
    public class SecretsManagerService(AmazonSecretsManagerClient client, string secretName) : ISecretsManagerService
    {
        private readonly AmazonSecretsManagerClient _client = client;
        private readonly string _secretName = secretName;

        public async Task<string> GetConnectionString()
        {
            var request = new GetSecretValueRequest { SecretId = _secretName };
            try
            {
                var response = await _client.GetSecretValueAsync(request);               

                var secretValue = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString);
                
                if (secretValue != null && secretValue.TryGetValue("ConnectionString", out string? value))              
                    return value;                
                else               
                    throw new Exception("ConnectionString key not found in the secret");                
            }
            catch (Exception e)
            {
                throw new Exception("We couldn't recover the connection string secret", e);
            }
        }

    }
}
