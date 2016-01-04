using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiApp.Models;
using MongoDB.Driver;

namespace ApiApp.Repositories.Imprementation
{

    class FormRepository : IFormRepository
    {
        public IEnumerable<Topic> GetForm(int fromId)
        {
            var coltn = MongoAccess.MongoUtil._database.GetCollection<Topic>("echecker.Topics");
            return coltn.Find(x => x.FromId == fromId).ToList();
        }
    }
}