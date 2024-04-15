using Hub.Models;
using Hub.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Hub.Services
{
    public class StoreClient : IStoreClient
    {
        private readonly IOptionsSnapshot<HubOptions> _options;
        private readonly HttpClient _httpClient;

        public StoreClient(IOptionsSnapshot<HubOptions> options, HttpClient httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        public async Task BulkAdd(IEnumerable<ProcessedAgentData> datas)
        {
            var models = datas.Select(x => new StoreAgentDataRequestModel
            {
                RoadState = x.RoadState,
                UserId = x.AgentData.UserId,
                X = x.AgentData.Accelerometer?.X ?? 0,
                Y = x.AgentData.Accelerometer?.Y ?? 0,
                Z = x.AgentData.Accelerometer?.Z ?? 0,
                Longitude = x.AgentData.Gps?.Longitude ?? 0,
                Latitude = x.AgentData.Gps?.Latitude ?? 0,
                TimeStamp = x.AgentData.Timestamp
            });

            var request = new {Models = models};
            await _httpClient.PostAsJsonAsync(_options.Value.StoreBulkAddUrl, request);
        }
    }
}