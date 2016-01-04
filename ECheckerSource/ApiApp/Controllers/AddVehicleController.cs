using ApiApp.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiApp.Controllers
{
    [RoutePrefix("api")]
    /// <summary>
    /// 
    /// </summary>
    public class AddVehicleController : ApiController
    {
        /// <summary>
        /// List all values.
        /// </summary>
        /// <returns></returns>
        // GET api/form/
        public void Post()
        {
            var collection = MongoAccess.MongoUtil._database.GetCollection<Vehicle>("echecker.Vehicles");
            var documents = new List<Vehicle>
            {
                    new Vehicle
                    {
                        id = Guid.NewGuid().ToString(),
                        PlateNumber = Guid.NewGuid().ToString().Substring(0, 4),
                        Email = "xxx@xxx.com",
                        Province = "qqqq",
                        CreateDate = DateTime.Now.AddDays(-1),
                        LatestCheckedDate = DateTime.Now.AddDays(1).Date,
                    },
                        new Vehicle
                    {
                        id = Guid.NewGuid().ToString(),
                        PlateNumber = Guid.NewGuid().ToString().Substring(0, 4),
                        Email = "sdfs@xcv.com",
                        Province = "wewer",
                        CreateDate = DateTime.Now.AddDays(-2),
                        LatestCheckedDate = DateTime.Now.AddDays(-2).Date,
                    },
                    new Vehicle
                    {
                        id = Guid.NewGuid().ToString(),
                        PlateNumber = Guid.NewGuid().ToString().Substring(0, 4),
                        Email = "sdfh@fghfgh.com",
                        Province = "qqwerwqq",
                        CreateDate = DateTime.Now.AddDays(-2),
                        LatestCheckedDate = DateTime.Now.AddDays(1).Date,
                    },
                    new Vehicle
                    {
                        id = Guid.NewGuid().ToString(),
                        PlateNumber = Guid.NewGuid().ToString().Substring(0, 4),
                        Email = "xxcv@dws.com",
                        Province = "erww",
                        CreateDate = DateTime.Now.AddDays(-1),
                        LatestCheckedDate = DateTime.Now.AddDays(1).Date,
                    },
            };
            collection.InsertMany(documents);
            //return collection.Find(x => x.id == id).ToList();
        }

        /// <summary>
        /// Get a specific value.
        /// </summary>
        /// <param name="id">The ref id.</param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet]
        [Route("test/{date}")]
        public IEnumerable<Vehicle> Get(DateTime date)
        {
            var collection = MongoAccess.MongoUtil._database.GetCollection<Vehicle>("echecker.Vehicles");
            //var result = collection.Find(x =>  x.LatestCheckedDate == date).ToList();
            //var x = collection.Find(zx => true).FirstOrDefault();
            //var dd = x.LatestCheckedDate.ToString("yyyyMMdd");

            //(x.LatestCheckedDate.Day == date.Day
            //&& x.LatestCheckedDate.Month == date.Month
            //&& x.LatestCheckedDate.Year == date.Year)).ToList();
            return null;
        }

        [HttpGet]
        [Route("test/")]
        public IEnumerable<Vehicle> GetAll()
        {
            var collection = MongoAccess.MongoUtil._database.GetCollection<Vehicle>("echecker.Vehicles");
            var result = collection.Find(x => true).ToList();

            //collection.DeleteMany(x => true);
            return result;
        }
    }
}
