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
    public interface ICheckingRepository
    {
        /// <summary>
        /// ดึงข้อมูลการตรวจที่ผิดปกติล่าสุด
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Amissed> GetAmissedByVehicleId(string id);
        /// <summary>
        /// ดึงข้อมูลการตรวจจาก
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentCheckDate"></param>
        /// <returns></returns>
        IEnumerable<Checked> GetCheckedByCurrentCheckDate(string id, DateTime currentCheckDate);
    }
}
