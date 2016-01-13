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

        /// <summary>
        /// get latest amissed list
        /// </summary>
        /// <param name="id">vehicle id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/amissed")]
        public IEnumerable<Amissed> GetLatestAmisseds(string id)
        {
            var amissedList = this.repoChecking.GetAmissedByVehicleId(id);
            return amissedList;
        }

        /// <summary>
        /// for test api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("amissed")]
        public IEnumerable<Amissed> GetAmissed()
        {
            var amissedList = this.repoChecking.GetAllAmissed();
            return amissedList;
        }

        /// <summary>
        /// for test api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("readystatus/getall")]
        /*public*/
        IEnumerable<ReadyStatus> GetAllReadStatus()
        {
            return this.repoChecking.GetAllReadyStatus();
        }


        /// <summary>
        /// get latest status (ready/not ready).
        /// </summary>
        /// <param name="id">Vehicle ID.</param>
        /// <returns></returns>
        // GET /checked/{vehicle-id}/readystatus
        [HttpGet]
        [Route("{id}/readystatus")]
        public string ReadyStatus(string id)
        {
            var result = this.repoChecking.GetLatestReadyStatus(id);
            return result == null ? "ไม่พร้อมใช้งาน" : result.Status;
        }

        /// <summary>
        /// Update the modified value.
        /// </summary>
        /// <param name="id">vehicle id</param>
        /// <param name="value">The new value to be updated.</param>
        // PUT /checked/{vehicle-id}/done
        [HttpPut]
        [Route("{id}/done")]
        public void PutDone(string id)
        {
            //TODO: verify all checked topics.IsPass are not null
            var myLatestChecked = this.repoChecking.GetLastChecked(id);
            if (myLatestChecked != null && myLatestChecked.CheckedTopics.All(x => x.IsPass != null))
            { }
            else
            {
                throw new Exception("found some checked topic that is not checking yet.");
            }

            try
            {
                //TODO: compute status
                //check critical first!!
                ReadyStatus status = new Models.ReadyStatus { VehicleId = id };
                var amissedList = this.repoChecking.GetAmissedByVehicleId(id);
                if (amissedList.Any(x => x.IsCritical == true))
                {
                    status.Status = "ไม่พร้อมใช้งาน";
                }
                //sum calculate damage
                else
                {
                    int avg = 0;
                    avg = amissedList.Sum(x => x.DamagePercent);
                    if (avg < 60)
                    {
                        status.Status = string.Format("{0}% พร้อมใช้งาน", avg);
                    }
                    else
                    {
                        status.Status = string.Format("{0}% ไม่พร้อมใช้งาน", avg);
                    }
                }
                //call repo to create
                this.repoChecking.CreateReadyStatus(status);

                //TODO: update checked[] to done
                var vehicle = this.repoVehicle.GetVehicle(id);
                Checked myChecked = this.repoChecking.CheckedDone(id, vehicle.LatestCheckedDate);

                //TODO: generate amissed
                var form = this.repoForm.GetForm(vehicle.FormId);

                List<Amissed> amisseds = new List<Amissed>();
                foreach (var item in form)
                {
                    //linked form item to check topic
                    var checkedTopic = myChecked.CheckedTopics.Where(x => x.TopicId == item.id).FirstOrDefault();

                    Amissed data = new Amissed();

                    //linked to topic
                    data.id = Guid.NewGuid().ToString();
                    data.CheckedId = myChecked.id;
                    data.VehicleId = id;
                    data.TopicId = item.id;
                    data.Detail = item.Detail;
                    //data.SuggestTopic = item.SuggestTopic;
                    data.SuggestDetail = item.SuggestDetail;
                    data.DamagePercent = item.DamagePercent;
                    data.IsCritical = item.IsCritical;

                    //linked to checked topic
                    data.Comment = checkedTopic.Comment;
                    data.PhotoUrl = checkedTopic.PhotoURL;
                    data.CreateDate = DateTime.Now;

                    amisseds.Add(data);
                }

                //create amisseds
                this.repoChecking.CreateAmissed(amisseds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                var newChecked = GetNewChecked(id, qry.FormId);
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
        /// <param name="id">VehicleId</param>
        /// <param name="check">CheckInfo</param>
        [HttpPut]
        [Route("{id}")]
        public void Put(string id, Checked check)
        {
            //var data = repoChecking.GetLastChecked("02bf2517-c90f-4963-bc4c-7d942fdc20d5");

            //foreach (var item in data.CheckedTopics)
            //{
            //    item.IsPass = true;
            //}

            //repoChecking.UpdateChecked(data);

            repoChecking.UpdateChecked(check);

            //HACK Test
            //var cc = GetMock();

            //repoChecking.UpdateChecked(cc);


        }

        /// <summary>
        /// for test api
        /// </summary>
        [HttpPost]
        [Route("createReady")]
        /*public*/
        void createready()
        {
            ReadyStatus rd = new Models.ReadyStatus
            {
                id = Guid.NewGuid().ToString(),
                VehicleId = "69C90FD9-5F74-405B-BC24-5C54D3C14252",
                Status = "พร้อมใช้งาน",
                CreateDateTime = DateTime.Now,
            };
            this.repoChecking.CreateReadyStatus(rd);
        }

        /// <summary>
        /// for test api
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        [Route("post")]
        /*public*/
        void CreateAmissed(string id)
        {
            this.repoChecking.CreateAmissed(mockAmissdes());
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
                        // คือตัวนี้กัน id = ToppicId ใส่ให้เทสได้้ก่อนเฉยๆ
                        TopicId = item.id,
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

        List<Amissed> mockAmissdes()
        {
            Guid checkedId = Guid.NewGuid();
            Guid checkedId2 = Guid.NewGuid();
            Guid topicId = Guid.NewGuid();
            return new List<Amissed>
            {
                new Amissed {
                id = Guid.NewGuid().ToString(),
                CheckedId = checkedId.ToString(),
                VehicleId = "69C90FD9-5F74-405B-BC24-5C54D3C14252",
                TopicId = topicId.ToString(),
                Detail = "amissed 301",
                DamagePercent = 15,
                IsCritical = false,
                SuggestTopic = "suggest 301",
                SuggestDetail = "suggestdetail 301",
                Comment = "comment 301",
                PhotoUrl = "",
                CreateDate = DateTime.Parse("1/5/2016"),
                },
                new Amissed {
                id = Guid.NewGuid().ToString(),
                CheckedId = checkedId.ToString(),
                VehicleId = "69C90FD9-5F74-405B-BC24-5C54D3C14252",
                TopicId = topicId.ToString(),
                Detail = "amissed 302",
                DamagePercent = 15,
                IsCritical = true,
                SuggestTopic = "suggest 302",
                SuggestDetail = "suggestdetail 302",
                Comment = "comment 302",
                PhotoUrl = "",
                CreateDate = DateTime.Parse("1/5/2016"),
                },
                new Amissed {
                id = Guid.NewGuid().ToString(),
                CheckedId = checkedId.ToString(),
                VehicleId = "69C90FD9-5F74-405B-BC24-5C54D3C14252",
                TopicId = topicId.ToString(),
                Detail = "amissed 303",
                DamagePercent = 15,
                IsCritical = false,
                SuggestTopic = "suggest 303",
                SuggestDetail = "suggestdetail 303",
                Comment = "comment 303",
                PhotoUrl = "",
                CreateDate = DateTime.Parse("1/5/2016"),
                },

                //new Amissed {
                //id = Guid.NewGuid().ToString(),
                //CheckedId = checkedId2.ToString(),
                //VehicleId = "69C90FD9-5F74-405B-BC24-5C54D3C14252",
                //TopicId = topicId.ToString(),
                //Detail = "amissed 201",
                //DamagePercent = 15,
                //IsCritical = false,
                //SuggestTopic = "suggest 201",
                //SuggestDetail = "suggestdetail 201",
                //Comment = "comment 201",
                //PhotoUrl = "",
                //CreateDate = DateTime.Parse("2016/1/3"),
                //},
                //new Amissed {
                //id = Guid.NewGuid().ToString(),
                //CheckedId = checkedId2.ToString(),
                //VehicleId = "69C90FD9-5F74-405B-BC24-5C54D3C14252",
                //TopicId = topicId.ToString(),
                //Detail = "amissed 202",
                //DamagePercent = 15,
                //IsCritical = true,
                //SuggestTopic = "suggest 202",
                //SuggestDetail = "suggestdetail 202",
                //Comment = "comment 202",
                //PhotoUrl = "",
                //CreateDate = DateTime.Parse("2016/1/3"),
                //},
                //new Amissed {
                //id = Guid.NewGuid().ToString(),
                //CheckedId = checkedId2.ToString(),
                //VehicleId = "69C90FD9-5F74-405B-BC24-5C54D3C14252",
                //TopicId = topicId.ToString(),
                //Detail = "amissed 203",
                //DamagePercent = 15,
                //IsCritical = false,
                //SuggestTopic = "suggest 203",
                //SuggestDetail = "suggestdetail 203",
                //Comment = "comment 203",
                //PhotoUrl = "",
                //CreateDate = DateTime.Parse("2016/1/3"),
                //},
            };
        }

        Checked GetMock()
        {
            return new Checked
            {
                id = Guid.NewGuid().ToString(),
                VehicleId = "69C90FD9-5F74-405B-BC24-5C54D3C14252",
                CreateDate = DateTime.Now,
                IsDone = false,
                CheckedTopics = new List<CheckTopics>
                {
                    new CheckTopics {  Comment = "ddddddd", id = Guid.NewGuid().ToString() , IsPass = null, PhotoURL = string.Empty},
                    new CheckTopics {  Comment = "seewr", id = Guid.NewGuid().ToString() , IsPass = true, PhotoURL = string.Empty},
                    new CheckTopics {  Comment = "wrtwret", id = Guid.NewGuid().ToString() , IsPass = false, PhotoURL = string.Empty},
                    new CheckTopics {  Comment = "45wrt6", id = Guid.NewGuid().ToString() , IsPass = null, PhotoURL = string.Empty},
                    new CheckTopics {  Comment = "wert", id = Guid.NewGuid().ToString() , IsPass = null, PhotoURL = string.Empty},
                },
            };
        }
    }
}
