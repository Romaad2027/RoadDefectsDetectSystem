namespace Hub
{
    public class HubOptions
    {
        public const string ConfigSectionName = "Hub";

        public string StoreBulkAddUrl { get; set; } = string.Empty;
        public string MqttBroker { get; set; } = string.Empty;
        public int MqttPort {get;set;}
        public string MqttTopic {get;set;} = string.Empty;
        public string MqttUsername {get;set;} = string.Empty;
        public string MqttPassword {get;set;} = string.Empty;
        public int BatchSize { get; set; }
    }
}