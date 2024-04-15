namespace Hub.Models
{
    public class ProcessedAgentData
    {
        public AgentData AgentData { get; set; } = null!;
        public string RoadState { get; set; } = string.Empty;
    }
}