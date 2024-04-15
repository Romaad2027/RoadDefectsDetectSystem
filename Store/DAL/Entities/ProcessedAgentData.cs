using System;
using System.ComponentModel.DataAnnotations;

namespace Store.DAL.Entities
{
    public class ProcessedAgentData
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RoadState { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}