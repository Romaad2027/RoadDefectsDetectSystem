using System;

namespace Hub.Models
{
    public class StoreAgentDataRequestModel
    {
        public string RoadState { get; set; }
        public int UserId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}