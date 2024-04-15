using Agent.Services;
using Agent.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agent.DI
{
    public static class AgentDi
    {
        public static IServiceCollection AddAgent(this IServiceCollection services, IConfiguration configuration)
        {
            AddOptions(services, configuration);
            AddTransientServices(services);
            AddScopedServices(services);
            AddSingletonServices(services);

            return services;
        }

        private static void AddOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<AgentOptions>().Bind(configuration.GetRequiredSection(AgentOptions.ConfigSectionName));
        }

        private static void AddTransientServices(IServiceCollection services)
        {
            services.AddTransient<ICsvReader, CsvReader>();
        }

        private static void AddScopedServices(IServiceCollection services)
        {
            services.AddScoped<IQueueService, QueueService>();
        }

        private static void AddSingletonServices(IServiceCollection services)
        {
            services.AddSingleton<ICommonLogger, ConsoleLogger>();
            services.AddHostedService<SensorService>(); // hosted service - singleton
        }
    }
}