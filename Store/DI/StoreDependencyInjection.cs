using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Store.DAL;
using Store.Services;
using Store.Services.Interfaces;

namespace Store.DI
{
    public static class StoreDependencyInjection
    {
        public static IServiceCollection AddStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<StoreOptions>().Bind(configuration.GetRequiredSection(StoreOptions.ConfigSectionName));

            services.AddDbContext<DataContext>((provider, builder) =>
            {
                var options = provider.GetRequiredService<IOptionsSnapshot<StoreOptions>>();
                builder.UseNpgsql(options.Value.PostgresConnectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProcessedAgentDataService, ProcessedAgentDataService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}