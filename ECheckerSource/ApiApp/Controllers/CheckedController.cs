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
    /// api การตรวจรถ
    /// </summary>
    [RoutePrefix("api/checked")]
    public class CheckedController : ApiController
    {
        public ICheckingRepository checkingRepo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkingRepo"></param>
        public CheckedController(ICheckingRepository checkingRepo)
        {
            this.checkingRepo = checkingRepo;
        }

        [HttpGet]
        [Route("{id}/amissed")]
        /// <summary>
        /// Get a specific value.
        /// </summary>
        /// <param name="id">The ref id.</param>
        /// <returns></returns>
        // GET /checked/{vehicle-id}/amissed
        public IEnumerable<Amissed> Get(string id)
        {
            var amissedList = this.checkingRepo.GetAmissedByVehicleId(id);
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
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{vehicleid}/done")]
        /// <summary>
        /// Update the modified value.
        /// </summary>
        /// <param name="id">The ref id.</param>
        /// <param name="value">The new value to be updated.</param>
        // PUT /checked/{vehicle-id}/done
        public void PutDone(Checked currentChecked)
        {
            //TODO: compute status
            //check critical first!!
            //var amissedList = this._CheckingRepo.GetAmissedByVehicleId(vehicleid);
            //if (amissedList.Any(x => x.IsCritical == true))
            //{
            //    return "ไม่พร้อมใช้งาน";
            //}
            ////sum calculate damage
            //else
            //{
            //    if (amissedList.Sum(x => x.DamagePercent) > 60)
            //    {
            //        return "พร้อมใช้งาน";
            //    }
            //    else
            //    {
            //        return "ไม่พร้อมใช้งาน";
            //    }
            //}
            
            //TODO: update checked[] to done
        }

        private IVechicleRepository repoVehicle;
        private ICheckingRepository repoChecking;
        private IFormRepository repoForm;

        /// <summary>
        /// api การตรวจรถ
        /// </summary>
        /// <param name="repoChecking"> repo การตรวจรถ</param>
        /// <param name="repoVehicle">repo ข้อมูลรถ</param>
        /// <param name="repoForm">repo ข้อมูลform</param>
        public CheckedController(ICheckingRepository repoChecking, IVechicleRepository repoVehicle, IFormRepository repoForm)
        {
            this.repoVehicle = repoVehicle;
            this.repoChecking = repoChecking;
            this.repoForm = repoForm;
        }

        /// <summary>
        /// GetLastChecked
        /// </summary>
        /// <param name="id"> รหัส รถ</param>
        /// <returns></returns>
        /// GET /checked/{vehicle-id}
        [HttpGet]
        [Route("{id}")]
        public Checked GetCheckedByVehicleId(string id)
        {
            var qry = repoVehicle.GetVehicle(id);

            return repoChecking.GetLastChecked(id, qry.LatestCheckedDate);
        }

        /// <summary>
        /// create Checked
        /// </summary>
        /// <param name="id">VehicleId</param>
        public void Post(string id)
        {
            var qry = repoVehicle.GetVehicle(id);

            if (qry != null)
            {
                var form = repoForm.GetForm(qry.FormId);

                List<CheckTopics> _checkTopic = new List<CheckTopics>();
                if (form.Count() > 0)
                {
                    foreach (var item in form)
                    {
                        var checkTopic = new CheckTopics
                        {
                            id = item.id,
                            IsPass = null,
                            Comment = string.Empty,
                            PhotoURL = string.Empty,
                        };

                        _checkTopic.Add(checkTopic);
                    }
                }

                Checked check = new Checked
                {
                    id = Guid.NewGuid().ToString(),
                    IsDone = false,
                    CreateDate = DateTime.Today,
                    VehicleId = id,
                    CheckedTopics = _checkTopic
                };

                repoChecking.AddChecked(check);
            }
        }

        [HttpPut]
        [Route("{id}")]
        /// <summary>
        /// update checked
        /// </summary>
        /// <param name="check"></param>
        public void Put(Checked check)
        {
            repoChecking.UpdateChecked(check);
        }
      
    }
}
