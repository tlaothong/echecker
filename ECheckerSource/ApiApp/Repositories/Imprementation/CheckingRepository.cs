using ApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace ApiApp.Repositories.Imprementation
{
    /// <summary>
    /// 
    /// </summary>
    class CheckingRepository : ICheckingRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="lastCreateCheckDate"></param>
        /// <returns></returns>
        public Checked GetLastChecked(string vehicleId, DateTime lastCreateCheckDate)
        {
            var coltn = MongoAccess.MongoUtil._database.GetCollection<Checked>("echecker.Checkeds");
            return coltn.Find(x => x.VehicleId == vehicleId && x.CreateDate == lastCreateCheckDate).FirstOrDefault();
        }
    }
}