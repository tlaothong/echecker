using ApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using ApiApp.MongoAccess;

namespace ApiApp.Repositories.Imprementation
{
    /// <summary>
    /// repo จัดการเกี่ยวกับการตรวจแบบฟอร์ม และการวิเคราะห์
    /// </summary>
    public class CheckingRepository : ICheckingRepository
    {
        /// <summary>
        /// table name
        /// </summary>
        private const string tableName = "echecker.Checkeds";


        /// <summary>
        /// สร้าง checked รถ
        /// </summary>
        /// <param name="check"> ข้อมูลการตรวจรถ</param>
        public void AddChecked(Checked check)
        {
            var coltn = MongoUtil.GetCollection<Checked>(tableName);
            coltn.InsertOne(check);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public IEnumerable<Amissed> GetAmissedByVehicleId(string vehicleId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ดึงข้อมูลการตรวจรถล่าสุด
        /// </summary>
        /// <param name="vehicleId">รหัสรถ</param>
        /// <param name="lastCreateCheckDate">วันที่เชคล่าสุด</param>
        /// <returns></returns>
        public Checked GetLastChecked(string vehicleId, DateTime lastCreateCheckDate)
        {
            var coltn = MongoUtil.GetCollection<Checked>(tableName);
            return coltn.Find(x => x.VehicleId == vehicleId && x.CreateDate == lastCreateCheckDate).FirstOrDefault();
        }

        /// <summary>
        /// ตรวจรถ
        /// </summary>
        /// <param name="check">ข้อมูล การตรวจรถ</param>
        public void UpdateChecked(Checked check)
        {
            var update = Builders<Checked>.Update
                    .Set(x => x.CheckedTopics, check.CheckedTopics);

            var coltn = MongoUtil.GetCollection<Checked>(tableName);
            coltn.UpdateOne(x => x.id == check.id, update);
        }
    }
}