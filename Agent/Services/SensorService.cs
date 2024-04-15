using Agent.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Agent.Services
{
    public class SensorService : ISensorService, IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private bool _isRunning;

        public SensorService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _isRunning = true;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await PublishSensorsData();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task PublishSensorsData()
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();

            using var csvReader = scope.ServiceProvider.GetRequiredService<ICsvReader>(); 
            var agentOptions = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<AgentOptions>>();
            var queueService = scope.ServiceProvider.GetRequiredService<IQueueService>();

            while (_isRunning)
            {
                await Task.Delay(agentOptions.Value.PublishDelay);

                var data = await csvReader.Read();
                if (data is null)
                {
                    _isRunning = false;
                    break;
                }

                await queueService.Publish(data);
            }
        }
    }
}