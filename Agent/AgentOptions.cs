namespace Agent
{
    public class AgentOptions
    {
        public const string ConfigSectionName = "Agent";
        public int MqttPort {get;set;}
        public string MqttTopic {get;set;} = string.Empty;
        public string MqttUsername {get;set;} = string.Empty;
        public string MqttPassword {get;set;} = string.Empty;
        public int PublishDelay {get;set;}
    }
}