using ApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApp.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFormRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromId"></param>
        /// <returns></returns>
        IEnumerable<Topic> GetForm(int fromId);
    }
}
