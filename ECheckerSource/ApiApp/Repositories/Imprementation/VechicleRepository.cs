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
    /// 
    /// </summary>
    class VechicleRepository : IVechicleRepository
    {

        private string tableVehicle = "echecker.Vehicles";
        /// <summary>
        /// เพิ่มรถ
        /// </summary>
        /// <param name="vehicle"></param>
        public void AddVehicle(Vehicle vehicle)
        {
            var coltn = MongoUtil.GetCollection<Vehicle>(tableVehicle);
            coltn.InsertOne(vehicle);
        }

        /// <summary>
        /// ดึงข้อมูลรถของผู้ใช้
        /// </summary>
        /// <param name="id">รหัส รถ</param>
        /// <returns></returns>
        public Vehicle GetVehicle(string id)
        {
            var coltn = MongoUtil.GetCollection<Vehicle>(tableVehicle);
            return coltn.Find(x => x.id == id).FirstOrDefault();
        }

        /// <summary>
        /// ดึง
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetVehicles(string id)
        {
            var coltn = MongoUtil.GetCollection<Vehicle>(tableVehicle);
            return coltn.Find(x => x.Email == id).ToList();
        }

        /// <summary>
        /// แก้ไขรถ ปล.แก้ได้เฉพาะ เลขทะเบียน กับ จังหวัด
        /// </summary>
        /// <param name="vehicle"></param>
        public void UpdateVehicle(Vehicle vehicle)
        {   
            var update = Builders<Vehicle>.Update
                    .Set(x => x.PlateNumber, vehicle.PlateNumber)
                    .Set(x => x.Province, vehicle.Province);

            var coltn = MongoUtil.GetCollection<Vehicle>(tableVehicle);
            coltn.UpdateOne( v =>v.id == vehicle.id, update);

        }
    }
}