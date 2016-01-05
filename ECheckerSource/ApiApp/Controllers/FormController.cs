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
    public class FormController : ApiController
    {
        private IFormRepository repoRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repoRepository"></param>
        public FormController(IFormRepository repoRepository)
        {
            this.repoRepository = repoRepository;
        }

        /// <summary>
        /// List all values.
        /// </summary>
        /// <returns></returns>
        // GET api/values
        ///GET /forms/{form-id}
        public IEnumerable<Topic> Get(int id)
        {
            return repoRepository.GetForm(id);
        }
    }
}
