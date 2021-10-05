using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Intuit.Ipp.Security;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using BlazorApp.Shared.Models;

namespace BlazorApp.Api.Functions
{
    public class CreateCustomerAsyncFunction
    {
        private readonly OAuth2Keys _options;
        private readonly IConfiguration _config;

        public CreateCustomerAsyncFunction(IOptions<OAuth2Keys> options, IConfiguration config)
        {
            _options = options.Value;
            _config = config;
        }

        [FunctionName("CreateCustomerAsync")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] AppCustomer customer, HttpRequest req,
            ILogger log)
        {

            string accessToken = _config["TestApp:QuickBooks:AccessToken"];
            string realmId = _config["TestApp:QuickBooks:RealmId"];
            string qboBaseUrl = _options.QBOBaseUrl;

            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(accessToken);

            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.MinorVersion.Qbo = "23";
            serviceContext.IppConfiguration.BaseUrl.Qbo = qboBaseUrl;

            Customer ObjCustomer = new Customer();
            ObjCustomer.GivenName = "Tabish";
            ObjCustomer.FamilyName = "Rangrej";
            ObjCustomer.ContactName = "Tabish Rangrej";
            ObjCustomer.CompanyName = "Vision";

            EmailAddress ObjEmail = new EmailAddress();
            ObjEmail.Address = "tabishzrangrej.vision@gmail.com";
            ObjCustomer.PrimaryEmailAddr = ObjEmail;

            PhysicalAddress ObjAddress = new PhysicalAddress();
            ObjAddress.PostalCode = "11379";
            ObjAddress.Country = "USA";
            ObjAddress.Line1 = "51 Front Dr";
            ObjAddress.City = "New York";
            ObjCustomer.BillAddr = ObjAddress;

            TelephoneNumber ObjTelephoneNumber = new TelephoneNumber();
            ObjTelephoneNumber.FreeFormNumber = "(123) 456-7890";
            ObjCustomer.PrimaryPhone = ObjTelephoneNumber;


            DataService dataService = new DataService(serviceContext);

            Customer CustomerAdd = dataService.Add(ObjCustomer);

            return new OkObjectResult(customer);
            
        }
    }
}
