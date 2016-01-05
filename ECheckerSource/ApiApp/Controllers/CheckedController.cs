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
            var amissedList = this.repoChecking.GetAmissedByVehicleId(id);
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
            var check = repoChecking.GetLastChecked(id);

            if (check != null)
            {
                return check;
            }
            else
            {
                var qry = repoVehicle.GetVehicle(id);

                if (qry != null)
                {
                    var now = DateTime.Now;
                    var newChecked = GetNewChecked(id, qry.FormId);
                    newChecked.CreateDate = now;

                    repoChecking.AddChecked(newChecked);
                    repoVehicle.UpdateLastChecked(id, now);

                    return repoChecking.GetLastChecked(id);
                }
                else
                {
                    return null;
                }
            }
          
        }

        /// <summary>
        /// create Checked
        /// </summary>
        /// <param name="id">VehicleId</param>
        [HttpPost]
        [Route("{id}")]
        public void Post(string id)
        {
            var qry = repoVehicle.GetVehicle(id);       

            if (qry != null)
            {
                var now = DateTime.Now;
                var newChecked = GetNewChecked(id,qry.FormId);
                newChecked.CreateDate = now;

                repoChecking.AddChecked(newChecked);
                repoVehicle.UpdateLastChecked(id, now);
            }

            ////HACK : mock 
            //var xx = GetMock();
            //var now = DateTime.Now;
            //xx.CreateDate = now;
            //repoChecking.AddChecked(xx);
            //repoVehicle.UpdateLastChecked(xx.VehicleId, now);
        }


        /// <summary>
        /// update checked
        /// </summary>
        /// <param name="check"></param>
        [HttpPut]
        public void Put(Checked check)
        {
            repoChecking.UpdateChecked(check);

            //HACK Test
            //var cc = GetMock();

            //repoChecking.UpdateChecked(cc);


        }


        Checked GetNewChecked(string vehicleId, int formId)
        {
            var now = DateTime.Now;
            var form = repoForm.GetForm(formId);

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
                CreateDate = now,
                VehicleId = vehicleId,
                CheckedTopics = _checkTopic
            };

            return check;
        }
        

        Checked GetMock()
        {
            return new Checked
            {
                id = Guid.NewGuid().ToString(),
                VehicleId = "C37EBF61 - 20E4 - 4612 - 9160 - A94A7281F2E4",
                CreateDate = DateTime.Now,
                IsDone = false,
                CheckedTopics = new List<CheckTopics>
                {
                    new CheckTopics {  Comment = "ddddddd", id = Guid.NewGuid().ToString() , IsPass = null, PhotoURL = string.Empty},
                    new CheckTopics {  Comment = "seewr", id = Guid.NewGuid().ToString() , IsPass = null, PhotoURL = string.Empty},
                    new CheckTopics {  Comment = "wrtwret", id = Guid.NewGuid().ToString() , IsPass = null, PhotoURL = string.Empty},
                    new CheckTopics {  Comment = "45wrt6", id = Guid.NewGuid().ToString() , IsPass = null, PhotoURL = string.Empty},
                    new CheckTopics {  Comment = "wert", id = Guid.NewGuid().ToString() , IsPass = null, PhotoURL = string.Empty},
                },
            };
        }

    }

 
}
