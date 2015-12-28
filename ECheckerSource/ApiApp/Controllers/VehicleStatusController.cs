using ApiApp.Models;
//using MongoDB.Bson;
//using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiApp.Controllers
{
    /// <summary>
    /// The default controller comes with the template.
    /// </summary>
    public class VehicleStatusController : ApiController
    {
        //MongoClient _mongo = new MongoClient("mongodb://MongoLab-4o:UMOcc359jl3WoTatREpo9qAAEGFL87uwoUWVyfusDUk-@ds056288.mongolab.com:56288/MongoLab-4o");


        /// <summary>
        /// List all values.
        /// </summary>
        /// <returns></returns>
        // GET api/values
        public string Get(int vid)
        {
            //var _db = _mongo.GetDatabase("MongoLab-4o");

            //var collection = _db.GetCollection<ReadyStatus>("ReadyStatus6");

            ////var filter = Builders<ReadyStatus>.Filter.Eq("VehicleId", "1");
            ////var result = collection.Find(filter).FirstOrDefault();

            ////return result.Status;

            //var qry = collection.Find(x => x.VehicleId == vid).ToList().OrderByDescending(x => x.CreateDateTime).FirstOrDefault();

            //return qry.Status;

            return "";
        }

        /// <summary>
        /// Post a new value.
        /// </summary>
        /// <param name="value">The new value.</param>
        // POST api/values
        public void Post([FromBody]string value)
        {
            //var _db = _mongo.GetDatabase("MongoLab-4o");
            //var collection = _db.GetCollection<ReadyStatus>("ReadyStatus");


            //var testReadyStatus = new ReadyStatus
            //{
            //    Id = 1,
            //    VehicleId = 1,
            //    Status = " 100 % พร้อมใช้งาน ",
            //    CreateDateTime = DateTime.Today,
            //};

            //collection.InsertOne(testReadyStatus);


        }

        /// <summary>
        /// Update the modified value.
        /// </summary>
        /// <param name="id">The ref id.</param>
        /// <param name="value">The new value to be updated.</param>
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

            //var _db = _mongo.GetDatabase("MongoLab-4o");
            //var collection = _db.GetCollection<ReadyStatus>("ReadyStatus6");


            //var update = Builders<ReadyStatus>.Update
            //    .Set("Status", value);          
            //collection.UpdateOne(x => x.Id == id, update);
        }

        /// <summary>
        /// Delete a specific value.
        /// </summary>
        /// <param name="id">The ref id.</param>
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
