using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var settings = builder
                .ConfigurationBuilder
                .AddUserSecrets(Assembly.GetExecutingAssembly(), false)
                .Build();
            var connection = settings.GetConnectionString("AppConfig");
            builder.ConfigurationBuilder.AddAzureAppConfiguration(connection);
        }
    }
}
