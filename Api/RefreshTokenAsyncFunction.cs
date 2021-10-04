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
using BlazorApp.Shared.Models;
using System;

namespace BlazorApp.Api
{
    public class RefreshTokenAsyncFunction
    {
        private readonly OAuth2Keys _options;
        private readonly IConfiguration _config;

        public RefreshTokenAsyncFunction(IOptions<OAuth2Keys> options, IConfiguration config)
        {
            _options = options.Value;
            _config = config;
        }

        [FunctionName("RefreshTokenAsync")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] AppCustomer customer, HttpRequest req,
            ILogger log)
        {

            string refreshToken = _config["TestApp:QuickBooks:RefreshToken"];

            var client = new OAuth2Client(_options.ClientId, _options.ClientSecret, _options.RedirectUrl, _options.Environment);

            var tokenResponse  = await client.RefreshTokenAsync(refreshToken);

            var connectionString = _config.GetConnectionString("AppConfig");
            var configurationClient = new ConfigurationClient(connectionString);
            await configurationClient.SetConfigurationSettingAsync("TestApp:QuickBooks:AccessToken", tokenResponse.AccessToken);
            await configurationClient.SetConfigurationSettingAsync("TestApp:QuickBooks:RefreshToken", tokenResponse.RefreshToken);
            await configurationClient.SetConfigurationSettingAsync("TestApp:Settings:Sentinel", Guid.NewGuid().ToString());

            return new OkObjectResult(new MyTokenResponse()
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken
            });

        }
    }
}
