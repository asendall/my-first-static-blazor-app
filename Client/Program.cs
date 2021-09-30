using System;
using System.Net.Http;
using System.Threading.Tasks;
using AzureStaticWebApps.Blazor.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

//https://anthonychu.ca/post/blazor-auth-azure-static-web-apps/

namespace BlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var baseAddress = builder.Configuration["BaseAddress"] ?? builder.HostEnvironment.BaseAddress;
                builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) })
                    .AddStaticWebAppsAuthentication();



            await builder.Build().RunAsync();
        }
    }
}