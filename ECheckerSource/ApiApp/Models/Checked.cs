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
    public class Checked
    {

        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        public string id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CheckTopics> CheckedTopics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VehicleId { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class CheckTopics
    {
        /// <summary>
        /// id = TopicId
        /// </summary>
        [BsonId]      
        public string id { get; set; }
        /// <summary>
        ///  id = TopicId
        /// </summary>
        public string TopicId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsPass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhotoURL { get; set; }
    }
}