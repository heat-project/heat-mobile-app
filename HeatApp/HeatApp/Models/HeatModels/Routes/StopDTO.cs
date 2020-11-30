using System;
using System.Collections.Generic;
using System.Text;
using HeatApp.Models;

namespace HeatApp.Models.HeatModels
{
    public class StopDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RouteID { get; set; }
    }
}