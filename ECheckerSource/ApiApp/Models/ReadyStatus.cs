using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadyStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// รหัส รถ
        /// </summary>
        public int VehicleId { get; set; }
        /// <summary>
        /// สถานะความพร้อม
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// ชื่อที่ สร้าง
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}