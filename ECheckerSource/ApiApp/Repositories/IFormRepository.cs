using ApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApp.Repositories
{
    /// <summary>
    /// manage form base
    /// </summary>
    public interface IFormRepository
    {
        /// <summary>
        /// ดึงข้อมูล topic 
        /// </summary>
        /// <param name="id">form id</param>
        /// <returns>รายการตรวจ</returns>
        IEnumerable<Topic> GetTopicByVehicleId(int id);
    }
}
