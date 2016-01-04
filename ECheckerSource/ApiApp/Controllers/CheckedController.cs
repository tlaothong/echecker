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
    [RoutePrefix("api/checked")]
    /// <summary>
    /// 
    /// </summary>
    public class CheckedController : ApiController
    {
        public ICheckingRepository _CheckingRepo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkingRepo"></param>
        public CheckedController(ICheckingRepository checkingRepo)
        {
            this._CheckingRepo = checkingRepo;
        }

        [HttpGet]
        [Route("{vehicleid}/amissed")]
        /// <summary>
        /// Get a specific value.
        /// </summary>
        /// <param name="id">The ref id.</param>
        /// <returns></returns>
        // GET /checked/{vehicle-id}/amissed
        public IEnumerable<Amissed> Get(string vehicleid)
        {
            var amissedList = this._CheckingRepo.GetAmissedByVehicleId(vehicleid);
            return amissedList.GroupBy(x => x.CreateDate.Date).OrderByDescending(x => x.Key).FirstOrDefault();
        }

        [HttpGet]
        [Route("{vehicleid}/readystatus")]
        /// <summary>
        /// Get a specific value.
        /// </summary>
        /// <param name="vehicleid">The ref id.</param>
        /// <returns></returns>
        // GET /checked/{vehicle-id}/readystatus
        public string ReadyStatus(string vehicleid)
        {
            //check critical first!!
            var amissedList = this._CheckingRepo.GetAmissedByVehicleId(vehicleid);
            if (amissedList.Any(x => x.IsCritical == true))
            {
                return "ไม่พร้อมใช้งาน";
            }
            //sum calculate damage
            else
            {
                if (amissedList.Sum(x => x.DamagePercent) > 60)
                {
                    return "พร้อมใช้งาน";
                }
                else
                {
                    return "ไม่พร้อมใช้งาน";
                }
            }
        }
    }
}
