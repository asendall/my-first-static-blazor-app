using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BlazorApp.Api.Startup))]

namespace BlazorApp.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<OAuth2Keys>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("OAuth2Keys").Bind(settings);
                });
        }
    }
}
