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
        /// ดึงข้อมูลการตรวจรถล่าสุด
        /// </summary>
        /// <param name="vehicleId">รหัสรถ</param>
        /// <param name="lastCreateCheckDate">วันที่เชคล่าสุด</param>
        /// <returns></returns>
        Checked GetLastChecked(string vehicleId,DateTime lastCreateCheckDate);
    }
}
