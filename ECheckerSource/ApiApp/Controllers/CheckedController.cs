using ApiApp.Models;
using ApiApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        public object ReadyStatus(string id)
        {
            var result = this.repoChecking.GetLatestReadyStatus(id);
            if (result == null)
            {
                result = new Models.ReadyStatus();
                result.Status = "พร้อมใช้งาน";
            }

            return new { ReadyStatus = result.Status };
        }

        /// <summary>
        /// Update latest checked to done and generate ready status and amissed checked.
        /// </summary>
        /// <param name="id">vehicle id</param>
        // PUT /checked/{vehicle-id}/done
        [HttpPut]
        [Route("{id}/done")]
        public void PutDone(string id)
        {
            //verify all checked topics.IsPass are not null
            var myLatestChecked = this.repoChecking.GetLastChecked(id);
            if (myLatestChecked != null && myLatestChecked.CheckedTopics.All(x => x.IsPass != null))
            {
                try
                {
                    //generate amissed
                    var vehicle = this.repoVehicle.GetVehicle(id);
                    var form = this.repoForm.GetForm(vehicle.FormId);

                    List<Amissed> amisseds = new List<Amissed>();
                    var failCheckedTopic = myLatestChecked.CheckedTopics.Where(x => x.IsPass == false);
                    foreach (var item in failCheckedTopic)
                    {
                        //linked form item to check topic
                        var checkedTopic = myLatestChecked.CheckedTopics.Where(x => x.id == item.id).FirstOrDefault();

                        Amissed data = new Amissed();

                        //linked to topic
                        data.id = Guid.NewGuid().ToString();
                        data.CheckedId = myLatestChecked.id;
                        data.VehicleId = id;
                        data.TopicId = item.id;
                        data.Detail = form.Where(x => x.id == item.id).FirstOrDefault().Detail;
                        data.SuggestTopic = form.Where(x => x.id == item.id).FirstOrDefault().SuggestTopic;
                        data.SuggestDetail = form.Where(x => x.id == item.id).FirstOrDefault().SuggestDetail;
                        data.DamagePercent = form.Where(x => x.id == item.id).FirstOrDefault().DamagePercent;
                        data.IsCritical = form.Where(x => x.id == item.id).FirstOrDefault().IsCritical;

                        //linked to checked topic
                        data.Comment = checkedTopic.Comment;
                        data.PhotoUrl = checkedTopic.PhotoURL;
                        data.CreateDate = DateTime.Now;

                        amisseds.Add(data);
                    }

                    //compute status
                    //check critical first!!
                    ReadyStatus status = new Models.ReadyStatus { VehicleId = id, CreateDateTime = DateTime.Now };
                    //var amissedList = this.repoChecking.GetAmissedByVehicleId(id);

                    //sum calculate damage
                    int avg = 0;
                    avg = 100 - amisseds.Sum(x => x.DamagePercent);

                    if (amisseds.All(x => x.IsCritical == false) && avg >= 60)
                    {
                        status.Status = string.Format("{0}% ใช้งานได้ปกติ", avg);
                    }
                    else
                    {
                        status.Status = string.Format("{0}% ไม่ควรใช้งาน", avg);
                    }

                    //update checked[] to done
                    /*Checked myChecked = */
                    this.repoChecking.CheckedDone(id, vehicle.LatestCheckedDate);

                    //call repo to create
                    this.repoChecking.CreateReadyStatus(status);

                    //create amisseds
                    if (amisseds.Count() > 0)
                    {
                        this.repoChecking.CreateAmissed(amisseds);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("found some checked topic that is not checking yet.");
            }
        }

        /// <summary>
        /// GetLastChecked
        /// </summary>
        /// <param name="id"> vehicle id</param>
        /// <returns></returns>
        /// GET /checked/{vehicle-id}
        [HttpGet]
        [Route("{id}")]
        public Checked GetCheckedByVehicleId(string id)
        {
            var check = repoChecking.GetLastChecked(id);

            //ถ้า ไม่มี checked หรือ checked ล่าสุดตรวจไปแล้ว ให้สร้าง checked ใหม่
            if (check == null || check.IsDone)
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
            else
            {
                return check;
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

                try
                {
                    repoChecking.AddChecked(newChecked);
                    repoVehicle.UpdateLastChecked(id, now);
                }
                catch (Exception e)
                {

                    throw e;
                }

            }

            ////HACK : mock 
            //var xx = GetMock();
            //var now = DateTime.Now;
            //xx.CreateDate = now;
            //repoChecking.AddChecked(xx);
            //repoVehicle.UpdateLastChecked(xx.VehicleId, now);
        }

        /// <summary>
        /// upload photo and Return Photo URL
        /// </summary>
        /// <param name="id">รหัสรถ</param>
        /// <param name="topicid"> รหัส Topic</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/{topicid}/photo")]
        public async System.Threading.Tasks.Task<object> PostPhoto(string id, string topicid)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/CheckedImg");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder(); // Holds the response body

                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the form data.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        sb.Append(string.Format("{0}: {1}\n", key, val));
                    }
                }

                var localfileURL = string.Empty;
                var serverfileURL = string.Empty;
                // This illustrates how to get the file names for uploaded files.
                foreach (var file in provider.FileData)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file.LocalFileName);
                    sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));

                    var fileName = Guid.NewGuid().ToString() + ".jpg";

                    localfileURL = System.Web.HttpContext.Current.Server.MapPath("~/CheckedImg/Img/" + fileName);


                    fileInfo.MoveTo(localfileURL);

                    //Fix URL
                    serverfileURL = new StringBuilder().Append("http://echecker-vanlek.azurewebsites.net").Append("/CheckedImg/Img/").Append(fileName).ToString();
                }


                return new { PhotoUrl = serverfileURL };
                //return new HttpResponseMessage()
                //{
                //    Content = new StringContent(sb.ToString())
                //};
            }
            catch (System.Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
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

            try
            {
                repoChecking.UpdateChecked(check);
            }
            catch (Exception e)
            {

                throw e;
            }


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

        // for create New CheckedInfo
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
                //SuggestTopic = "suggest 301",
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
                //SuggestTopic = "suggest 302",
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
                //SuggestTopic = "suggest 303",
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
