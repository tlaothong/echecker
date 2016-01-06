using ApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApp.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVechicleRepository
    {
        /// <summary>
        /// ดึงข้อมูลรถทั้งหมดของผู้ใช้
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        IEnumerable<Models.Vehicle> GetVehicles(string email);

        /// <summary>
        /// ดึงข้อมูลรถของผู้ใช้
        /// </summary>
        /// <param name="vehicleId">รหัส รถ</param>
        /// <returns></returns>
        Models.Vehicle GetVehicle(string vehicleId);

        /// <summary>
        /// เพิ่มรถ
        /// </summary>
        /// <param name="vehicle"></param>
        void AddVehicle(Models.Vehicle vehicle);
        
        /// <summary>
        /// แก้ไขรถ ปล.แก้ได้เฉพาะ เลขทะเบียน กับ จังหวัด
        /// </summary>
        /// <param name="vehicle"></param>
        void UpdateVehicle(Models.Vehicle vehicle);

        /// <summary>
        /// แก้ไขการแจ้งเตือน
        /// </summary>
        /// <param name="vehicle"></param>
        void UpdateNotification(Models.Vehicle vehicle);

        /// <summary>
        /// update last checked
        /// </summary>
        /// <param name="vehicleId"> รหัสรถ</param>
        /// <param name="datetime"> last checked datetimec</param>
        void UpdateLastChecked(string vehicleId, DateTime datetime);

    }
}
