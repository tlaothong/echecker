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
    /// Vehicle
    /// </summary>
    public class VehicleController : ApiController
    {
        private IVechicleRepository repoVehicle;
        private ICheckingRepository repoChecking;

        /// <summary>
        /// Vehicle
        /// </summary>
        /// <param name="repoVehicle"></param>
        /// <param name="repoChecking"></param>
        public VehicleController(IVechicleRepository repoVehicle, ICheckingRepository repoChecking)
        {
            this.repoVehicle = repoVehicle;
            this.repoChecking = repoChecking;
        }

        /// <summary>
        /// vehicles by vehicleid.
        /// </summary>
        /// <param name="id">username email</param>
        /// <returns>vehicles</returns>
        ///GET /vehicles/{user-id} 
        public Vehicle Get(string id)
        {
            var qry = repoVehicle.GetVehicle(id);
            return qry;
        }

        /// <summary>
        /// add vehicle
        /// </summary>
        /// <param name="vehicle">The new vehicle.</param>
        ///POST /vehicles/add/
        public void Post(Vehicle vehicle)
        {
            repoVehicle.AddVehicle(vehicle);
        }

        /// <summary>
        /// UpdateVehicle
        /// </summary>
        /// <param name="vehicle">updateInfo</param>
        /// PUT /vehicle/{vehicle-id}   
        public void Put(Vehicle vehicle)
        {
            repoVehicle.UpdateVehicle(vehicle);
        }
    }
}
