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
        /// <returns></returns>
        Checked GetLastChecked(string vehicleId);

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Amissed> GetAllAmissed();

        /// <summary>
        /// get all amissed
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        IEnumerable<Amissed> GetAllAmissedByVehicleId(string vehicleId);

        /// <summary>
        /// create amisseds after analyzed
        /// </summary>
        /// <param name="amisseds"></param>
        void CreateAmissed(IEnumerable<Amissed> amisseds);

        /// <summary>
        /// get latest ready status by vehicle id
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        ReadyStatus GetLatestReadyStatus(string vehicleId);

        /// <summary>
        /// for test api
        /// </summary>
        /// <returns></returns>
        IEnumerable<ReadyStatus> GetAllReadyStatus();

        /// <summary>
        /// create ready status
        /// </summary>
        /// <param name="readyStatus"></param>
        void CreateReadyStatus(ReadyStatus readyStatus);        

        /// <summary>
        /// update checked to done
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="latestCheckedDate"></param>
        /*Checked*/void CheckedDone(string vehicleId, DateTime latestCheckedDate);        
    }
}
