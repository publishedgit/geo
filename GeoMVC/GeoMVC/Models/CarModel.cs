using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoMVC.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Made { get; set; }
        public string Type { get; set; }
        public int Vintage { get; set; }
        public DateTime ProductionTime { get; set; }
        public string Condition { get; set; }
        public int NumberOfOwners { get; set; }
        public int LocationId { get; set; }

    }
}