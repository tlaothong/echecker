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
    public class User
    {
        [BsonId]
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
    }
}