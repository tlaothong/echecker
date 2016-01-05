using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiApp.Models;
using MongoDB.Driver;

namespace ApiApp.Repositories
{
    class DemoRepository : IDemoRepository
    {
        public string GetEmail(string userId)
        {
            //var coltn = MongoAccess.MongoUtil.GetCollection("");
            //return string.Format("You've send a {0}.", userId);
            return "";
        }
    }
}
