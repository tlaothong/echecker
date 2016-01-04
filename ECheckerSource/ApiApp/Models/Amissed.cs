using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Amissed
    {
        [BsonId]
        public string id { get; set; }
        public string CheckedId { get; set; }
        public string VehicleId { get; set; }
        public string TopicId { get; set; }
        public string Detail { get; set; }
        public int DamagePercent { get; set; }
        public bool IsCritical { get; set; }
        public string SuggestTopic { get; set; }
        public string SuggestDetail { get; set; }
        public string Comment { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreateDate { get; set; }
    }
}