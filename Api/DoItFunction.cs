using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Api
{
    public class DoItFunction
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationRefresher _configurationRefresher;

        public DoItFunction(IConfiguration config, IConfigurationRefresherProvider refresherProvider)
        {
            _config = config;
            _configurationRefresher = refresherProvider.Refreshers.First(); ;
        }

        [FunctionName("DoIt")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            await _configurationRefresher.TryRefreshAsync();

            string keyName = "TestApp:QuickBooks:RefreshToken";
            string message = _config[keyName];

            return message != null
                ? (ActionResult)new OkObjectResult(message)
                : new BadRequestObjectResult($"Please create a key-value with the key '{keyName}' in App Configuration.");

        }
    }
}
