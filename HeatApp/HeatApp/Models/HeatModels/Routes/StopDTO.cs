using System;
using System.Collections.Generic;
using System.Text;
using HeatApp.Models;
using Newtonsoft.Json;

namespace HeatApp.Models.HeatModels
{
    public class StopDTO
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("street")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

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
                    return double.Parse(Location.Split(',')[1]);
                else
                    return 0;
            }
        }

        [JsonProperty("routeid")]
        public int RouteID { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("route")]
        public string Route { get; set; }

        [JsonProperty("routecode")]
        public string RouteCode { get; set; }
    }
}