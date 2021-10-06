using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using BlazorApp.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BlazorApp.Api.Functions
{
    public class GetEventsFunction
    {
        private readonly AdminContext _adminContext;

        public GetEventsFunction(AdminContext adminContext)
        {
            _adminContext = adminContext;
        }

        [FunctionName("GetEvents")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            var result = await _adminContext.Inbox.ToArrayAsync();

            return new JsonResult(result,new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.All });
        }
    }
}
