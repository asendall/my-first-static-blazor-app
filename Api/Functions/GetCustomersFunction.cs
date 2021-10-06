using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using BlazorApp.Api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Api.Functions
{
    public class GetCustomersFunction
    {
        private readonly AdminContext _adminContext;

        public GetCustomersFunction(AdminContext adminContext)
        {
            _adminContext = adminContext;
        }

        [FunctionName("GetCustomers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            var result = await _adminContext.Customers.ToArrayAsync();

            return new OkObjectResult(result);
        }
    }
}
