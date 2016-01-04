using ApiApp.Models;
using ApiApp.Repositories;
using ApiApp.Repositories.Imprementation;
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
    [RoutePrefix("api/forms")]
    public class FormController : ApiController
    {
        /// <summary>
        /// interface of IFormRepository for easier to test spec
        /// </summary>
        public IFormRepository _repo { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        public FormController(IFormRepository repo)
        {
            this._repo = repo;
        }
        
        [Route("{id}")]
        /// <summary>
        /// List all values.
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        // GET api/form/                   
        public IEnumerable<Topic> Get(int id)
        {
            return this._repo.GetTopicByVehicleId(id);
        }
    }
}
