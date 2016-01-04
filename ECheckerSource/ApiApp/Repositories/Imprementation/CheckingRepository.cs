using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiApp.Models;
using MongoDB.Driver;

namespace ApiApp.Repositories.Imprementation
{
    /// <summary>
    /// repo จัดการเกี่ยวกับการตรวจแบบฟอร์ม และการวิเคราะห์
    /// </summary>
    public class CheckingRepository : ICheckingRepository
    {
        /// <summary>
        /// collection naem
        /// </summary>
        public const string cltNameAmissed = "echecker.Amisseds";
        /// <summary>
        /// collection naem
        /// </summary>
        public const string cltNameChecked = "echecker.Checkeds";

        /// <summary>
        /// Get all amissed by vehicle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Amissed> GetAmissedByVehicleId(string id)
        {
            var collection = MongoAccess.MongoUtil._database.GetCollection<Amissed>(cltNameAmissed);
            var result = collection.Find(x => x.VehicleId == id ).ToList();
            return result;
        }

        public IEnumerable<Checked> GetCheckedByCurrentCheckDate(string id, DateTime currentCheckDate)
        {
            throw new NotImplementedException();
        }
    }
}