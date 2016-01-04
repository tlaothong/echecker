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
        /// สร้าง checked รถ
        /// </summary>
        /// <param name="check"> ข้อมูลการตรวจรถ</param>
        public void AddChecked(Checked check)
        {
            var coltn = MongoAccess.MongoUtil._database.GetCollection<Checked>("echecker.Checkeds");
            coltn.InsertOne(check);
        }

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

        /// <summary>
        /// ตรวจรถ
        /// </summary>
        /// <param name="check">ข้อมูล การตรวจรถ</param>
        public void UpdateChecked(Checked check)
        {
            var filter = Builders<Checked>.Filter.Eq("id", check.id);
            var update = Builders<Checked>.Update
                    .Set(x => x.CheckedTopics, check.CheckedTopics);   

            var coltn = MongoAccess.MongoUtil._database.GetCollection<Checked>("echecker.Checkeds");
            coltn.UpdateOne(filter, update);
        }
    }
}