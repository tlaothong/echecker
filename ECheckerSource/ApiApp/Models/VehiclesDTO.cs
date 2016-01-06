using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class VehiclesDTO
    {
        /// <summary>
        /// 
        /// </summary> 
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int VehicleProgress { get; set; }
        /// <summary>
        ///  0 = ตรวจยังไม่เสร็จ ,1=รอส่งวิเคราะห์ ,2=วิเคราะห์แล้ว
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int VehicleTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LatestCheckedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// Notification วันที่วันที่แจ้งเตือน พรบ หมด
        /// </summary>
        public DateTime PBRDate { get; set; }
        /// <summary>
        /// Notification ตั้ง่คาการแจ้งเตือน พรบ
        /// </summary>
        public bool IsPBRActive { get; set; }
        /// <summary>
        /// Notification วันที่วันที่แจ้งเตือน ใบขับขี่
        /// </summary>
        public DateTime DrivingLicenseDate { get; set; }
        /// <summary>
        ///  Notification ตั้ง่คาการแจ้งเตือน ใบขับขี่
        /// </summary>
        public bool IsDrivingLicenseActive { get; set; }
        /// <summary>
        ///  Notification วันที่วันที่แจ้งเตือน ตรวจสภาพรถ
        /// </summary>
        public DateTime CheckDate { get; set; }
        /// <summary>
        ///  Notification ตั้ง่คาการแจ้งเตือน ตรวจสภาพรถ
        /// </summary>
        public bool IsCheckActive { get; set; }
        /// <summary>
        ///  Notification วันที่วันที่แจ้งเตือน ต่อภาษีรถ
        /// </summary>
        public DateTime TaxDate { get; set; }
        /// <summary>
        ///  Notification ตั้ง่คาการแจ้งเตือน ต่อภาษีรถ
        /// </summary>
        public bool IsTaxActive { get; set; }
        /// <summary>
        ///  Notification วันที่วันที่แจ้งเตือน จ่ายงวดรถ
        /// </summary>
        public DateTime PayDate { get; set; }
        /// <summary>
        ///  Notification ตั้ง่คาการแจ้งเตือน จ่ายงวดรถ
        /// </summary>
        public bool IsPayActive { get; set; }
    }
}