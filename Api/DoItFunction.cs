using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BlazorApp.Api
{
    public class DoItFunction
    {
        private readonly IConfiguration _config;

        public DoItFunction(IConfiguration config)
        {
            _config = config;
        }

        [FunctionName("DoIt")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            string keyName = "TestApp:Settings:Message";
            string message = _config[keyName];

            return message != null
                ? (ActionResult)new OkObjectResult(message)
                : new BadRequestObjectResult($"Please create a key-value with the key '{keyName}' in App Configuration.");

        }
    }
}
