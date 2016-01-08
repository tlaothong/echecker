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
    [RoutePrefix("api/vehicle")]
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
        [HttpGet]
        [Route("{id}")]
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
        [HttpPost]
        [Route("add/")]
        public void Post(Vehicle vehicle)
        {
            vehicle.id = Guid.NewGuid().ToString();
            vehicle.LatestCheckedDate = DateTime.Now;
            vehicle.CreateDate = DateTime.Now;

            if (vehicle.VehicleTypeId ==11)
            {
                //vehicle.FormId = 11;
                vehicle.FormId = 1;
            }
            else if (vehicle.VehicleTypeId == 13)
            {
                //vehicle.FormId = 13;
                vehicle.FormId = 1;
            }

            vehicle.PayDate = DateTime.Today;
            vehicle.IsPayActive = false;
            vehicle.PBRDate = DateTime.Today;
            vehicle.IsPBRActive = false;
            vehicle.CheckDate = DateTime.Today;
            vehicle.IsCheckActive = false;
            vehicle.DrivingLicenseDate = DateTime.Today;
            vehicle.IsDrivingLicenseActive = false;
            vehicle.TaxDate = DateTime.Today;
            vehicle.IsTaxActive = false;

            try
            {
                repoVehicle.AddVehicle(vehicle);
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }

        /// <summary>
        /// UpdateVehicle Info
        /// </summary>
        /// <param name="id">รหัสรถ</param>
        /// <param name="vehicle">updateInfo</param>
        /// PUT /vehicle/{vehicle-id}   
        [HttpPut]
        [Route("{id}")]
        public void UpdateInfo(string id, Vehicle vehicle)
        {
            repoVehicle.UpdateVehicle(vehicle);
        }
        /// <summary>
        /// UpdateVehicle Noti
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehicle">notiInfo</param>
        [HttpPut]
        [Route("{id}/noti/")]
        public void UpdateNoti(string id, Vehicle vehicle)
        {
            repoVehicle.UpdateNotification(vehicle);
        }
    }
}
