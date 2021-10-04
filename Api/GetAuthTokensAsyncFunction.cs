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
using BlazorApp.Shared;
using System;
using BlazorApp.Shared.Models;

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] AuthorizationServerResponse authorizationServerResponse, HttpRequest req, 
            ILogger log)
        {

            //string code = req.Query["code"];
            var client = new OAuth2Client(_options.ClientId, _options.ClientSecret, _options.RedirectUrl, _options.Environment);
            
            var tokenResponse = await client.GetBearerTokenAsync(authorizationServerResponse.Code);

            var connectionString = _config.GetConnectionString("AppConfig");
            var configurationClient = new ConfigurationClient(connectionString);
            await configurationClient.SetConfigurationSettingAsync("TestApp:QuickBooks:AccessToken", tokenResponse.AccessToken);
            await configurationClient.SetConfigurationSettingAsync("TestApp:QuickBooks:RefreshToken", tokenResponse.RefreshToken);
            await configurationClient.SetConfigurationSettingAsync("TestApp:QuickBooks:RealmId", authorizationServerResponse.RealmId);
            await configurationClient.SetConfigurationSettingAsync("TestApp:Settings:Sentinel", Guid.NewGuid().ToString());

            return new OkObjectResult(new MyTokenResponse()
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken
            });
            
        }
    }
}
