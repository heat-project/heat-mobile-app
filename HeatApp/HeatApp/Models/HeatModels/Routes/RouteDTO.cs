using System;
using System.Collections.Generic;
using System.Text;

namespace HeatApp.Models.HeatModels
{
    public class RouteDTO
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}