using Hub.Services;
using Hub.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hub.DI
{
    public static class HubDependencyInjection
    {
        public static IServiceCollection AddHub(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<HubOptions>().Bind(configuration.GetRequiredSection(HubOptions.ConfigSectionName));

            services.AddHttpClient<IStoreClient, StoreClient>();
            services.AddHostedService<AgentDataService>();
            services.AddSingleton<IAgentDataService, AgentDataService>();
            services.AddLogging(cfg => cfg.AddConsole());

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}