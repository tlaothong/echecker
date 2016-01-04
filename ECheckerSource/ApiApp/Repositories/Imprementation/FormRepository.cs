using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiApp.Models;
using MongoDB.Driver;

namespace ApiApp.Repositories.Imprementation
{
    /// <summary>
    /// repo จัดการเกี่ยวกับแบบฟอร์มการตรวจสภาพรถ
    /// </summary>
    public class FormRepository : IFormRepository
    {

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
    }
}