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
        private const string tableName = "echecker.Vehicles";


        /// <summary>
        /// เพิ่มรถ
        /// </summary>
        /// <param name="vehicle"></param>
        public void AddVehicle(Vehicle vehicle)
        {
            var now = DateTime.Now;         

            if (vehicle.VehicleTypeId == 11)
            {
                vehicle.FormId = 11;
            }
            else if (vehicle.VehicleTypeId == 13)
            {
                vehicle.FormId = 13;
            }

            vehicle.PayDate = now;
            vehicle.IsPayActive = false;
            vehicle.PBRDate = now;
            vehicle.IsPBRActive = false;
            vehicle.CheckDate = now;
            vehicle.IsCheckActive = false;
            vehicle.DrivingLicenseDate = now;
            vehicle.IsDrivingLicenseActive = false;
            vehicle.TaxDate = now;
            vehicle.IsTaxActive = false;

            vehicle.id = Guid.NewGuid().ToString();
            vehicle.LatestCheckedDate = now;
            vehicle.CreateDate = now;

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

            var result = coltn.Find(x => x.id == vehicleId);
            return result != null ? result.FirstOrDefault() : null;
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
        /// update last checked
        /// </summary>
        /// <param name="vehicleId"> รหัสรถ</param>
        /// <param name="datetime"> last checked datetimec</param>
        public void UpdateLastChecked(string vehicleId, DateTime datetime)
        {
            var update = Builders<Vehicle>.Update
                   .Set(x => x.LatestCheckedDate, datetime);

            var coltn = MongoUtil.GetCollection<Vehicle>(tableName);
            coltn.UpdateOne(v => v.id == vehicleId, update);
        }

        /// <summary>
        /// แก้ไขการแจ้งเตือน
        /// </summary>
        /// <param name="vehicle"></param>
        public void UpdateNotification(Vehicle vehicle)
        {
            var now = DateTime.Now;

            var update = Builders<Vehicle>.Update
              
                  .Set(x => x.PBRDate, vehicle.IsPBRActive ? vehicle.PBRDate : now)
                  .Set(x => x.IsPBRActive, vehicle.IsPBRActive)
                
                  .Set(x => x.CheckDate, vehicle.IsCheckActive ? vehicle.CheckDate : now)
                  .Set(x => x.IsCheckActive, vehicle.IsCheckActive)
                
                  .Set(x => x.DrivingLicenseDate, vehicle.IsDrivingLicenseActive ? vehicle.DrivingLicenseDate : now)
                  .Set(x => x.IsDrivingLicenseActive, vehicle.IsDrivingLicenseActive)
               
                  .Set(x => x.PayDate, vehicle.IsPayActive ? vehicle.PayDate : now)
                  .Set(x => x.IsPayActive, vehicle.IsPayActive)
                 
                  .Set(x => x.TaxDate, vehicle.IsTaxActive ? vehicle.TaxDate : now)
                  .Set(x => x.IsTaxActive, vehicle.IsTaxActive);

            var coltn = MongoUtil.GetCollection<Vehicle>(tableName);
            coltn.UpdateOne(v => v.id == vehicle.id, update);
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
            coltn.UpdateOne(v => v.id == vehicle.id, update);

           
        }
    }
}