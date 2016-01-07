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
    public class AmissedReportDTO
    {
        public DateTime CreateDate { get; set; }
        public IEnumerable<Amissed> Amisseds { get; set; }
    }
}