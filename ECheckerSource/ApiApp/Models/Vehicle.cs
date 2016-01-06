using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        public string id { get; set; }
        /// <summary>
        /// ทะเบียนรถ
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        ///  จังหวัด
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        ///   เวลาที่สร้าง
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// วันล่าสุดที่ตรวจสภาพ
        /// </summary>
        //[BsonDateTimeOptions(DateOnly = true)]
        public DateTime LatestCheckedDate { get; set; }

        /// <summary>
        /// 11 = รถยน , 13 = มอไซ
        /// </summary>
        public int VehicleTypeId { get; set; }
        /// <summary>
        /// 1 = default รถยน , 2 = default มอไซ
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