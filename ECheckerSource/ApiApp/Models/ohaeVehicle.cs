using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApp.Models
{
    public class ohaeVehicle
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string Province { get; set; }
        public DateTime CreateDate { get; set; }
        public string Email { get; set; }
        public int VehicleProgress { get; set; }
        public int StatusCode { get; set; }
    }
}
