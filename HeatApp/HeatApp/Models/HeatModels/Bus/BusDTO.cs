using System;
using System.Collections.Generic;
using System.Text;

namespace HeatApp.Models
{
    public class BusDTO
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public int RouteID { get; set; }
    }
}