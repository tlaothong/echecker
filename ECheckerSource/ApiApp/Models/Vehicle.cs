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
    public class Vehicle
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
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
        public DateTime LatestCheckedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int VehicleTypeId { get; set; }
    }
}