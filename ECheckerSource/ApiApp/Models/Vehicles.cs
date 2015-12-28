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
    public class Vehicles
    {
        [BsonId]
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int VehicleProgress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int StatusCode { get; set; }
    }
}