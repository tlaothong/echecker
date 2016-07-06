using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiApp.Models;
using MongoDB.Driver;
using ApiApp.MongoAccess;

namespace ApiApp.Repositories.Imprementation
{
    /// <summary>
    /// repo จัดการเกี่ยวกับแบบฟอร์มการตรวจสภาพรถ
    /// </summary>
    public class FormRepository : IFormRepository
    {
        /// <summary>
        /// table name
        /// </summary>
        private const string tableName = "echecker.Topics";

        /// <summary>
        /// ดึงข้อมูล topic 
        /// </summary>
        /// <param name="id">form id</param>
        /// <returns>รายการตรวจ</returns>
        public IEnumerable<Topic> GetTopicByVehicleId(int id)
        {
            var collection = MongoAccess.MongoUtil._database.GetCollection<Topic>("echecker.Topics");
            var result = collection.Find(x => x.VehicleTypeId == id).ToList();
            return result != null ? result : new List<Topic>();
        }

        /// <summary>
        /// ดึงข้อมูล ฟอร์ม
        /// </summary>
        /// <param name="fromId"> รหัส ฟอร์ม</param>
        /// <returns></returns>
        public IEnumerable<Topic> GetForm(int fromId)
        {
            var coltn = MongoUtil.GetCollection<Topic>(tableName);
            var result = coltn.Find(x => x.FormId == fromId).ToList();

            if (result == null || result.Count() <= 0)
            {
                throw new Exception("No data from GetForm repo");
            }
            return result;
        }
  

        /// <summary>
        /// สร้าง ฟอร์ม  -- ใช้ชั่วคราวสร้างฟอร์มตั้งต้น
        /// </summary>
        /// <param name="topic"></param>
        public void CreateForm(IEnumerable<Topic> topic)
        {
            if (topic == null || topic.Count() <= 0)
            {
                throw new ArgumentNullException("null input from CreateForm repo");
              
            }
            else
            {
                var coltn = MongoUtil.GetCollection<Topic>(tableName);
                coltn.InsertMany(topic);
            }
        }
    }
}