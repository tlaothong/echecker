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
        private const string tableNameAmissed = "echecker.Amisseds";
        private const string tableNameReadyStatus = "echecker.ReadyStatuss";

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
        /// get latest amissed by vehicle id
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns>Amisseds</returns>
        public IEnumerable<Amissed> GetAmissedByVehicleId(string vehicleId)
        {
            var coltn = MongoUtil.GetCollection<Amissed>(tableNameAmissed);
            var data = coltn.Find(x => x.VehicleId == vehicleId).ToList()
                .GroupBy(x => x.CreateDate.Date).OrderByDescending(y => y.Key)
                .FirstOrDefault();

            return data != null ? data.Select(x => x) : new List<Amissed>();
        }

        /// <summary>
        /// for test api
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Amissed> GetAllAmissed()
        {
            var result = MongoUtil.GetCollection<Amissed>(tableNameAmissed).Find(x => true).ToList();
            return result != null ? result : new List<Amissed>();
        }

        /// <summary>
        /// get all amissed
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public IEnumerable<Amissed> GetAllAmissedByVehicleId(string vehicleId)
        {
            var coltn = MongoUtil.GetCollection<Amissed>(tableNameAmissed);
            var result = coltn.Find(x => x.VehicleId == vehicleId).ToList();
            return result != null ? result : new List<Amissed>();
        }

        /// <summary>
        /// ดึงข้อมูลการตรวจรถล่าสุด
        /// </summary>
        /// <param name="vehicleId">รหัสรถ</param>   
        /// <returns></returns>
        public Checked GetLastChecked(string vehicleId)
        {
            var coltn = MongoUtil.GetCollection<Checked>(tableName);
            var result = coltn.Find(x => x.VehicleId == vehicleId).SortByDescending(x => x.CreateDate);
            return result != null ? result.FirstOrDefault() : null;
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

            var last = GetLastChecked(check.VehicleId);
            coltn.UpdateOne(p => p.id == last.id, update);
        }

        /// <summary>
        /// create amisseds after analyzed
        /// </summary>
        /// <param name="amisseds"></param>
        public void CreateAmissed(IEnumerable<Amissed> amisseds)
        {
            var coltn = MongoUtil.GetCollection<Amissed>(tableNameAmissed);
            coltn.InsertMany(amisseds);
        }

        /// <summary>
        /// get latest ready status by vehicle id
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public ReadyStatus GetLatestReadyStatus(string vehicleId)
        {
            var coltn = MongoUtil.GetCollection<ReadyStatus>(tableNameReadyStatus);
            var result = coltn.Find(x => x.VehicleId == vehicleId).ToList().OrderByDescending(y => y.CreateDateTime);
            return result != null ? result.FirstOrDefault() : null;
        }

        /// <summary>
        /// create ready status
        /// </summary>
        /// <param name="readyStatus"></param>
        public void CreateReadyStatus(ReadyStatus readyStatus)
        {
            var coltn = MongoUtil.GetCollection<ReadyStatus>(tableNameReadyStatus);
            readyStatus.id = Guid.NewGuid().ToString();
            coltn.InsertOne(readyStatus);
        }

        /// <summary>
        /// for test api
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReadyStatus> GetAllReadyStatus()
        {
            var coltn = MongoUtil.GetCollection<ReadyStatus>(tableNameReadyStatus);
            return coltn.Find(x => true).ToList();
        }

        /// <summary>
        /// update checked to done
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="latestCheckedDate"></param>
        public Checked CheckedDone(string vehicleId, DateTime latestCheckedDate)
        {
            var coltn = MongoUtil.GetCollection<Checked>(tableName);
            var updater = Builders<Checked>.Update
                .Set(x => x.IsDone, true);

            var filter = Builders<Checked>.Filter
                .Where((x => x.VehicleId == vehicleId && x.CreateDate == latestCheckedDate));

            coltn.UpdateOne(filter, updater);
            return coltn.Find(filter).FirstOrDefault();
        }
    }
}