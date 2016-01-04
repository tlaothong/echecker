using ApiApp.Models;
using ApiApp.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiApp.Controllers
{
   
    /// <summary>
    /// 
    /// </summary>
    public class VehicleListController : ApiController
    {
        private IVechicleRepository repo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public VehicleListController(IVechicleRepository repo)
        {
            this.repo = repo;
        }
        /// <summary>
        /// List vehicles by email.
        /// </summary>
        /// <param name="id">username email</param>
        /// <returns>vehicles</returns>
        // GET api/vehicles/aaa@aaa.com
        public IEnumerable<Vehicle> Get(string id)
        {
            //var client = new MongoDB.Driver.MongoClient("mongodb://MongoLab-4o:UMOcc359jl3WoTatREpo9qAAEGFL87uwoUWVyfusDUk-@ds056288.mongolab.com:56288/MongoLab-4o");
            //var database = client.GetDatabase("MongoLab-4o");

            //var collection = database.GetCollection<ohaeVehicle>("test.ohaevehicle");

            //var document = collection.Find(x => x.Email == email).ToList();

            //return document;

            //return repo.GetVehicles(id);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Post a new vehicle.
        /// </summary>
        /// <param name="vehicle">The new vehicle.</param>
        // POST api/values
        public void Post(Vehicle vehicle)
        {
            //var client = new MongoDB.Driver.MongoClient("mongodb://MongoLab-4o:UMOcc359jl3WoTatREpo9qAAEGFL87uwoUWVyfusDUk-@ds056288.mongolab.com:56288/MongoLab-4o");
            //var database = client.GetDatabase("MongoLab-4o");

            //var collection = database.GetCollection<ohaeVehicle>("test.ohaevehicle");            

            //collection.InsertOne(vehicle);
        }



        /// <summary>
        /// Update the modified value.
        /// </summary>
        /// <param name="email">The ref id.</param>
        /// <param name="value">The new value to be updated.</param>
        // PUT api/vehicles/aaa@aaa.com
        public void Put(string email, [FromBody]string value)
        {
            //var client = new MongoDB.Driver.MongoClient("mongodb://MongoLab-4o:UMOcc359jl3WoTatREpo9qAAEGFL87uwoUWVyfusDUk-@ds056288.mongolab.com:56288/MongoLab-4o");
            //var database = client.GetDatabase("MongoLab-4o");

            //var collection = database.GetCollection<ohaeVehicle>("test.ohaevehicle");

            //var update = Builders<ohaeVehicle>.Update.Set("Province", value);


            ////var documents = collection.Find(x => x.Email == email).ToList();

            //collection.UpdateMany(x => x.Email == email, update);
        }
    }
}
