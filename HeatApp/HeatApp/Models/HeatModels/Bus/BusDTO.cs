using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HeatApp.Models
{
    public class BusDTO
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        public string Description { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        public double Latitude
        {
            get
            {
                if (!string.IsNullOrEmpty(Location) && !string.IsNullOrEmpty(Location))
                    return double.Parse(Location.Split(',')[0]);
                else
                    return 0;
            }
        }

        public double Longitude
        {
            get
            {
                if (!string.IsNullOrEmpty(Location) && !string.IsNullOrEmpty(Location) && Location.Split(',').Length > 1)
                    return double.Parse(Location.Split(',')[1]) * -1;
                else
                    return 0;
            }
        }

        public int RouteID { get; set; }
    }
}