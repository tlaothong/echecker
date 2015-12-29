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
        /// <param name="id">email</param>
        /// <returns></returns>
        IEnumerable<Models.Vehicle> GetVehicles(string id);

        /// <summary>
        /// ดึงข้อมูลรถของผู้ใช้
        /// </summary>
        /// <param name="id">รหัส รถ</param>
        /// <returns></returns>
        Models.Vehicle GetVehicle(string id);

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

    }
}
