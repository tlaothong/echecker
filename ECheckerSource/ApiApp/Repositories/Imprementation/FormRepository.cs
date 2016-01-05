using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiApp.Models;
using MongoDB.Driver;
using ApiApp.MongoAccess;

namespace ApiApp.Repositories.Imprementation
{
    /// <summary>
    /// 
    /// </summary>
    class FormRepository : IFormRepository
    {
        /// <summary>
        /// table name
        /// </summary>
        private string tableName = "echecker.Topics";


        /// <summary>
        /// ดึงข้อมูล ฟอร์ม
        /// </summary>
        /// <param name="fromId"> รหัส ฟอร์ม</param>
        /// <returns></returns>
        public IEnumerable<Topic> GetForm(int fromId)
        {
            var coltn = MongoUtil.GetCollection<Topic>(tableName);
            return coltn.Find(x => x.FromId == fromId).ToList();
        }
    }
}