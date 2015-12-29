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
    [RoutePrefix("api/vehicle")]
    public class VehicleController : ApiController
    {
        private IVechicleRepository repoVehicle;
        private ICheckingRepository repoChecking;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repoVehicle"></param>
        /// <param name="repoChecking"></param>
        public VehicleController(IVechicleRepository repoVehicle, ICheckingRepository repoChecking)
        {
            this.repoVehicle = repoVehicle;
            this.repoChecking = repoChecking;
        }

        /// <summary>
        /// List vehicles by email.
        /// </summary>
        /// <param name="id">username email</param>
        /// <returns>vehicles</returns>
        ///GET /vehicles/{user-id}
        [HttpGet]
        [Route("vehicles/{id}")]
        public IEnumerable<VehiclesDTO> Vehicles(string id)
        {
            //return null;

            List<VehiclesDTO> _vehicleList = new List<VehiclesDTO>();
            var vehicle = repoVehicle.GetVehicles(id);
            if (vehicle.Count() > 0)
            {
                foreach (var item in vehicle)
                {
                    var vehicleDto = new VehiclesDTO
                    {
                        id = item.id,
                        PlateNumber = item.PlateNumber,
                        Province = item.Province,
                        Email = item.Email,
                        CreateDate = item.CreateDate,
                        LatestCheckedDate = item.LatestCheckedDate,
                        VehicleTypeId = item.VehicleTypeId,
                    };

                    var qry = repoChecking.GetLastChecked(item.id, item.LatestCheckedDate);

                    if (qry != null)
                    {
                       

                        if (qry.IsDone)
                        {
                            vehicleDto.StatusCode = 2;
                        }
                        else
                        {
                            //HACK * fix มี 20 รายการ ทำเป็น เปอเซน *5
                            vehicleDto.VehicleProgress = qry.CheckedTopics.Where(x => x.IsPass).Count() * 5;

                            if (vehicleDto.VehicleProgress == 100)
                                vehicleDto.StatusCode = 1;
                            else
                                vehicleDto.StatusCode = 0;
                        }                       
                    }
                    _vehicleList.Add(vehicleDto);
                }
            }

            return _vehicleList;

        }

        /// <summary>
        /// vehicles by vehicleid.
        /// </summary>
        /// <param name="id">username email</param>
        /// <returns>vehicles</returns>
        ///GET /vehicles/{user-id}
        [HttpGet]
        [Route("vehicle/{id}")]
        public Vehicle Vehicle(string id)
        {
            var qry = repoVehicle.GetVehicle(id);
            return  qry;
        }

        /// <summary>
        /// add vehicle
        /// </summary>
        /// <param name="vehicle">The new vehicle.</param>
        ///POST /vehicles/add/
        //[HttpPost]
        //[Route("add/{vehicle}")]
        public void Post(Vehicle vehicle)
        {
            repoVehicle.AddVehicle(vehicle);
        }

        /// <summary>
        /// UpdateVehicle
        /// </summary>
        /// <param name="vehicle"></param>
        /// PUT /vehicle/{vehicle-id}
        //[HttpPut]
        //[Route("{vehicle}")]
        public void Put(Vehicle vehicle)
        {
            repoVehicle.UpdateVehicle(vehicle);
        }
    }
}
