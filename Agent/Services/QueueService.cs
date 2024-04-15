using System;
using System.Text.Json;
using System.Threading.Tasks;
using Agent.Services.Interfaces;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;

namespace Agent.Services
{
    public class QueueService : IQueueService
    {
        private readonly ICommonLogger _logger;
        private readonly IOptionsSnapshot<AgentOptions> _agentOptions;

        public QueueService(ICommonLogger logger, IOptionsSnapshot<AgentOptions> agentOptions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _agentOptions = agentOptions ?? throw new ArgumentNullException(nameof(agentOptions));
        }

        public async Task Publish<T>(T message)
        {
            var mqttMessage = BuildMqttMessage(message);

            using var client = await ConnectToMqttServer();
            await client.PublishAsync(mqttMessage);
            await client.DisconnectAsync();
        }

        private MqttApplicationMessage BuildMqttMessage<T>(T message)
        {
            return new MqttApplicationMessageBuilder()
                .WithTopic(_agentOptions.Value.MqttTopic)
                .WithPayload(JsonSerializer.SerializeToUtf8Bytes(message))
                .Build();
        }

        private async Task<IMqttClient> ConnectToMqttServer()
        {
            var mqttClient = new MqttFactory().CreateMqttClient();

            var mqttOptions = BuildMqttClientOptions();

            var connectResult = await mqttClient.ConnectAsync(mqttOptions);
            if (connectResult.ResultCode is not MqttClientConnectResultCode.Success)
            {
                throw new Exception("Failed to connect to MQTT server");
            }

            return mqttClient;
        }

        private MqttClientOptions BuildMqttClientOptions()
        {
            return new MqttClientOptionsBuilder()
                .WithTcpServer("mqtt", _agentOptions.Value.MqttPort)
                .WithCredentials(_agentOptions.Value.MqttUsername, _agentOptions.Value.MqttPassword)
                .WithClientId(Guid.NewGuid().ToString())
                .WithCleanStart()
                .Build();
        }
    }
}
