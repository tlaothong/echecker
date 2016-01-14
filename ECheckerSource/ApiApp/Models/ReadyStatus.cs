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
    public class ReadyStatus
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId] 
        public string id { get; set; }
        /// <summary>
        /// รหัส รถ
        /// </summary>
        public string VehicleId { get; set; }
        /// <summary>
        /// สถานะความพร้อม
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// ชื่อที่ สร้าง
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}