using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using BlazorApp.Shared;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Collections.Generic;

namespace BlazorApp.Api
{
    public static class WeatherForecastFunction
    {
        private static string GetSummary(int temp)
        {
            var summary = "Mild";

            if (temp >= 32)
            {
                summary = "Hot";
            }
            else if (temp <= 16 && temp > 0)
            {
                summary = "Cold";
            }
            else if (temp <= 0)
            {
                summary = "Freezing";
            }

            return summary;
        }

        [FunctionName("WeatherForecast")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var randomNumber = new Random();
            var temp = 0;

            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = temp = randomNumber.Next(-20, 55),
                Summary = GetSummary(temp)
            }).ToArray();

            var client = new OAuth2Client("ABueAkoxSzDJc0wy5MP37Hun9ySxPoHGyE5Kv3i5Ti5dpskaHF", "MuaCKhAh9Vt35Oz6nASjvWB3UWZBYinqG4ax0oyL", "https://localhost:44351/", "sandbox");
            var scopes = new List<OidcScopes>
            {
                OidcScopes.Accounting
            };
            var authorizeUrl = client.GetAuthorizationURL(scopes);

            return new OkObjectResult(result);
        }
    }
}
