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
    /// report controller
    /// </summary>
    public class ReportController : ApiController
    {
        private ICheckingRepository repoChecking;

        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="repoChecking"></param>
        public ReportController(ICheckingRepository repoChecking)
        {
            this.repoChecking = repoChecking;
        }

        /// <summary>
        /// get all amissed, group by create date.
        /// </summary>
        /// <param name="id">vehicle id</param>
        /// <returns></returns>
        public IEnumerable<AmissedReportDTO> Get(string id)
        {
            var data = this.repoChecking.GetAllAmissedByVehicleId(id);
            var grp = data.GroupBy(x => x.CreateDate.Date);

            List<AmissedReportDTO> result = new List<AmissedReportDTO>();
            foreach (var item in grp)
            {
                AmissedReportDTO dto = new AmissedReportDTO();
                dto.CreateDate = item.Key;
                dto.Amisseds = item;
                result.Add(dto);
            }

            return result.OrderByDescending(it => it.CreateDate);
        }
    }
}
