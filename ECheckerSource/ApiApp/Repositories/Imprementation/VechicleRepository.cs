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
    public class VehicleRepository : IVechicleRepository
    {

        /// <summary>
        /// table name
        /// </summary>
        private string tableName = "echecker.Vehicles";


        /// <summary>
        /// เพิ่มรถ
        /// </summary>
        /// <param name="vehicle"></param>
        public void AddVehicle(Vehicle vehicle)
        {
            var coltn = MongoUtil.GetCollection<Vehicle>(tableName);
            coltn.InsertOne(vehicle);
        }

        /// <summary>
        /// ดึงข้อมูลรถของผู้ใช้
        /// </summary>
        /// <param name="vehicleId">รหัส รถ</param>
        /// <returns></returns>
        public Vehicle GetVehicle(string vehicleId)
        {
            var coltn = MongoUtil.GetCollection<Vehicle>(tableName);
            return coltn.Find(x => x.id == vehicleId).FirstOrDefault();
        }

        /// <summary>
        /// ดึงข้อมูลรถทั้งหมดของผู้ใช้
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetVehicles(string email)
        {
            var coltn = MongoUtil.GetCollection<Vehicle>(tableName);
            return coltn.Find(x => x.Email == email).ToList();
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

            var coltn = MongoUtil.GetCollection<Vehicle>(tableName);
            coltn.UpdateOne( v =>v.id == vehicle.id, update);

        }
    }
}