using System.Collections.Concurrent;
using System.Text.Json;
using Hub.Models;
using Hub.Services.Interfaces;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;

namespace Hub.Services
{
    public class AgentDataService : IAgentDataService, IHostedService
    {
        private readonly IOptionsSnapshot<HubOptions> _options;
        private readonly IStoreClient _storeClient;
        private readonly SemaphoreSlim _saveSemaphoreSlim = new(1, 1);

        private readonly ConcurrentBag<ProcessedAgentData> _dataBuffer = new();

        public AgentDataService(IServiceScopeFactory scopeFactory)
        {
            using var scope = scopeFactory.CreateScope();
            _options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<HubOptions>>();
            _storeClient = scope.ServiceProvider.GetRequiredService<IStoreClient>();
        }

        [Obsolete("Obsolete")]
        public Task StartAsync(CancellationToken cancellationToken) => ListenMqttTopic();

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Save(ProcessedAgentData data)
        {
            try
            {
                await _saveSemaphoreSlim.WaitAsync();
                _dataBuffer.Add(data);
                
                if (_dataBuffer.Count < _options.Value.BatchSize)
                {
                    return;
                }
                await _storeClient.BulkAdd(_dataBuffer);

                _dataBuffer.Clear();
            }
            finally
            {
                _saveSemaphoreSlim.Release();
            }
        }

        [Obsolete("Obsolete")]
        private async Task ListenMqttTopic()
        {
            return;
            var mqttClient = await ConnectMqtt();

            await mqttClient.SubscribeAsync(_options.Value.MqttTopic);

            mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                var payload = e.ApplicationMessage.Payload;
                var message = JsonSerializer.Deserialize<ProcessedAgentData>(payload);
                if (message == null)
                {
                    return;
                }

                await Save(message);
                
            };
        }

        private async Task<IMqttClient> ConnectMqtt()
        {
            var mqttClient = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("mqtt", _options.Value.MqttPort)
                .WithCredentials(_options.Value.MqttUsername, _options.Value.MqttPassword)
                .WithClientId(Guid.NewGuid().ToString())
                .WithCleanStart()
                .Build();

            var connectResult = await mqttClient.ConnectAsync(options);
            if (connectResult.ResultCode != MqttClientConnectResultCode.Success)
            {
                throw new Exception("Error while connecting to MQTT server");
            }

            return mqttClient;
        }
    }
}
