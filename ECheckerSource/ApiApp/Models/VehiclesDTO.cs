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
        public DateTime LatestCheckedDate { get; set; }
    }
}