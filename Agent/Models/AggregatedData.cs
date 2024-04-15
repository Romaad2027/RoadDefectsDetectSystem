using System;

namespace Agent.Models
{
    public class AggregatedData
    {
        public Accelerometer? Accelerometer { get; set; }
        public Gps? Gps { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
    }
}