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
    public class Topic
    {
        [BsonId]
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DamagePercent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCritical { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HowtoTopic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HowToUrl { get; set; }
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
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int FromId { get; set; }

    }
}