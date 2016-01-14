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
        /// <summary>
        /// 
        /// </summary>
        [BsonId]        
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CheckedId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VehicleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TopicId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DamagePercent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCritical { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SuggestTopic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SuggestDetail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// create date 
        /// </summary>
        //[BsonDateTimeOptions(DateOnly = true)] ***(only date do not put time into this property)***
        public DateTime CreateDate { get; set; }
    }
}