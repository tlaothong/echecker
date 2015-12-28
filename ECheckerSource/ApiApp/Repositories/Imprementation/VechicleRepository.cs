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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Vehicles> GetVehicles(string id)
        {
            var coltn = MongoAccess.MongoUtil._database.GetCollection<Vehicles>("echecker.Vehicles"); ;
            return coltn.Find(x => x.id == id).ToList();
        }
    }
}