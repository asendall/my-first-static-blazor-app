using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace BlazorApp.Api
{
    public class GetAuthTokensAsyncFunction
    {
        private readonly OAuth2Keys _options;

        public GetAuthTokensAsyncFunction(IOptions<OAuth2Keys> options)
        {
            _options = options.Value;
        }

        [FunctionName("GetAuthTokensAsync")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string code = req.Query["code"];
                var client = new OAuth2Client(_options.ClientId, _options.ClientSecret, _options.RedirectUrl, _options.Environment);

                var tokenResponse = client.GetBearerTokenAsync(code).Result;

                return new OkObjectResult(tokenResponse.AccessToken);
            }
            catch (System.Exception ex)
            {

                return new OkObjectResult(ex.Message); ;
            }


            
        }
    }
}
