﻿using ApiApp.Models;
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
        Checked GetLastChecked(string vehicleId, DateTime lastCreateCheckDate);

        /// <summary>
        /// ตรวจรถ
        /// </summary>
        /// <param name="check">ข้อมูล การตรวจรถ</param>
        void UpdateChecked(Checked check);

        /// <summary>
        /// สร้าง checked รถ
        /// </summary>
        /// <param name="check"> ข้อมูลการตรวจรถ</param>
        void AddChecked(Checked check);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        IEnumerable<Amissed> GetAmissedByVehicleId(string vehicleId);
    }
}
