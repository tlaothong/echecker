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
        /// รหัส topic id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// รหัสประเภทรถที่ใช้ topic 
        /// </summary>
        public int VehicleTypeId { get; set; }
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
        public string How2Topic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string How2Url { get; set; }
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
        public string FormId { get; set; }
    }
}