using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace BlazorApp.Api.Functions
{
    public class GetAuthorizationURLFunction
    {
        private readonly OAuth2Keys _options;

        public GetAuthorizationURLFunction(IOptions<OAuth2Keys> options)
        {
            _options = options.Value;
        }

        [FunctionName("GetAuthorizationURL")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var client = new OAuth2Client(_options.ClientId, _options.ClientSecret, _options.RedirectUrl, _options.Environment);
            var scopes = new List<OidcScopes>
            {
                OidcScopes.Accounting
            };
            var authorizeUrl = client.GetAuthorizationURL(scopes);

            return new OkObjectResult(authorizeUrl);
        }
    }
}
