﻿using ApiApp.Models;
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
    /// 
    /// </summary>
    public class FromsController : ApiController
    {
        private IFormRepository repo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public FromsController(IFormRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// List all values.
        /// </summary>
        /// <returns></returns>
        // GET api/values
        ///GET /forms/{form-id}
        public IEnumerable<Topic> Get(int id)
        {
            return repo.GetForm(id);
        }

        /// <summary>
        /// Get a specific value.
        /// </summary>
        /// <param name="id">The ref id.</param>
        /// <returns></returns>
        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        /// <summary>
        /// Post a new value.
        /// </summary>
        /// <param name="value">The new value.</param>
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Update the modified value.
        /// </summary>
        /// <param name="id">The ref id.</param>
        /// <param name="value">The new value to be updated.</param>
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Delete a specific value.
        /// </summary>
        /// <param name="id">The ref id.</param>
        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
