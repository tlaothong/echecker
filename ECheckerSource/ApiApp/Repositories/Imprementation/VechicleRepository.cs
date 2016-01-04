using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiApp.Models;
using MongoDB.Driver;

namespace ApiApp.Repositories.Imprementation
{
    /// <summary>
    /// 
    /// </summary>
    public class VechicleRepository : IVechicleRepository
    {
        public const string cltName = "echecker.Vehicle";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetVehicles(string id)
        {
            var coltn = MongoAccess.MongoUtil._database.GetCollection<Vehicle>(cltName);
            var result = coltn.Find(x => x.id == id).ToList();
            return result;
        }

        public Vehicle GetVehicleInfoById(string id)
        {
            var coltn = MongoAccess.MongoUtil._database.GetCollection<Vehicle>(cltName);
            var result = coltn.Find(x => x.id == id).FirstOrDefault();
            return result;
        }
    }
}