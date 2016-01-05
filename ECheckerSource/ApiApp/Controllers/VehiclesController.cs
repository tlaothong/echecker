using ApiApp.Models;
using ApiApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiApp.Controllers
{
    /// <summary>
    /// Vehicles
    /// </summary>
    public class VehiclesController : ApiController
    {
        private IVechicleRepository repoVehicle;
        private ICheckingRepository repoChecking;

        /// <summary>
        /// Vehicles
        /// </summary>
        /// <param name="repoVehicle"></param>
        /// <param name="repoChecking"></param>
        public VehiclesController(IVechicleRepository repoVehicle, ICheckingRepository repoChecking)
        {
            this.repoChecking = repoChecking;
            this.repoVehicle = repoVehicle;
        }

        /// <summary>
        /// List vehicles by email.
        /// </summary>
        /// <param name="id">email</param>
        /// <returns>vehicles</returns>
        ///GET /vehicles/{user-id}
        public IEnumerable<VehiclesDTO> Get(string id)
        {     
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
                        FormId = item.FormId,
                    };

                    var qry = repoChecking.GetLastChecked(item.id);

                    if (qry != null)
                    {
                        if (qry.IsDone)
                        {
                            vehicleDto.StatusCode = 2;
                        }
                        else
                        {
                            //HACK * fix มี 20 รายการ ทำเป็น เปอเซน *5
                            vehicleDto.VehicleProgress = qry.CheckedTopics.Where(x => x.IsPass.HasValue).Count() * 5;

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
    }
}
