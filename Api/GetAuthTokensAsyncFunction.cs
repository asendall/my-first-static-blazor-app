using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Data.AppConfiguration;

namespace BlazorApp.Api
{
    public class GetAuthTokensAsyncFunction
    {
        private readonly OAuth2Keys _options;
        private readonly IConfiguration _config;

        public GetAuthTokensAsyncFunction(IOptions<OAuth2Keys> options, IConfiguration config)
        {
            _options = options.Value;
            _config = config;
        }

        [FunctionName("GetAuthTokensAsync")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAuthTokensAsync/{code}")] HttpRequest req, string code,
            ILogger log)
        {

            //string code = req.Query["code"];
            var client = new OAuth2Client(_options.ClientId, _options.ClientSecret, _options.RedirectUrl, _options.Environment);

            var tokenResponse = await client.GetBearerTokenAsync(code);

            var connectionString = _config.GetConnectionString("AppConfig");
            var configurationClient = new ConfigurationClient(connectionString);
            ConfigurationSetting setting = configurationClient.SetConfigurationSetting("TestApp:Settings:Message", tokenResponse.AccessToken);

            return new OkObjectResult(tokenResponse.AccessToken);
            
        }
    }
}
