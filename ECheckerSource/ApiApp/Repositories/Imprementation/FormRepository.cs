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
            return collection.Find(x => x.VehicleTypeId == id).ToList();
        }

        /// <summary>
        /// ดึงข้อมูล ฟอร์ม
        /// </summary>
        /// <param name="fromId"> รหัส ฟอร์ม</param>
        /// <returns></returns>
        public IEnumerable<Topic> GetForm(int fromId)
        {
            var coltn = MongoUtil.GetCollection<Topic>(tableName);
            return coltn.Find(x => x.FormId == fromId).ToList();
        }
  

        /// <summary>
        /// สร้าง ฟอร์ม  -- ใช้ชั่วคราวสร้างฟอร์มตั้งต้น
        /// </summary>
        /// <param name="topic"></param>
        public void CreateForm(IEnumerable<Topic> topic)
        {
            var coltn = MongoUtil.GetCollection<Topic>(tableName);

            coltn.InsertMany(topic);
        }
    }
}