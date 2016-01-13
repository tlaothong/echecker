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
    public interface IFormRepository
    {
        /// <summary>
        /// ดึงข้อมูล ฟอร์ม
        /// </summary>
        /// <param name="fromId"> รหัส ฟอร์ม</param>
        /// <returns></returns>
        IEnumerable<Topic> GetForm(int fromId);

        /// <summary>
        /// สร้าง ฟอร์ม  -- ใช้ชั่วคราวสร้างฟอร์มตั้งต้น
        /// </summary>
        /// <param name="topic"></param>
        void CreateForm(IEnumerable<Topic> topic);
    }
}
