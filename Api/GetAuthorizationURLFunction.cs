using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Collections.Generic;

namespace BlazorApp.Api
{
    public static class GetAuthorizationURLFunction
    {

        [FunctionName("GetAuthorizationURL")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var client = new OAuth2Client("ABueAkoxSzDJc0wy5MP37Hun9ySxPoHGyE5Kv3i5Ti5dpskaHF", "MuaCKhAh9Vt35Oz6nASjvWB3UWZBYinqG4ax0oyL", "https://localhost:44351/callback", "sandbox");
            var scopes = new List<OidcScopes>
            {
                OidcScopes.Accounting
            };
            var authorizeUrl = client.GetAuthorizationURL(scopes);

            return new OkObjectResult(authorizeUrl);
        }
    }
}
