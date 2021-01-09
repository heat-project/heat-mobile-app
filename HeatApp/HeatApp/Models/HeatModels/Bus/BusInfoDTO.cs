using System;
using System.Collections.Generic;
using HeatApp.Models.HeatModels.Bus;
using Newtonsoft.Json;

namespace HeatApp.Models
{
    public class BusInfoDTO
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("plate")]
        public string Plate { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("routeid")]
        public int RouteID { get; set; }

        [JsonProperty("route")]
        public string Route { get; set; }

        [JsonProperty("typeid")]
        public int TypeID { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("conductorid")]
        public int ConductorID { get; set; }

        [JsonProperty("conductor")]
        public string Conductor { get; set; }

        [JsonProperty("photoconductor")]
        public string PhotoConductor { get; set; }

        [JsonProperty("dateentryconductor")]
        public string DateEntryConductor { get; set; }

        [JsonProperty("rating")]
        public decimal Rating { get; set; }

        [JsonProperty("atributtes")]
        public IEnumerable<BusInfoDetailDTO> Atributtes { get; set; }

        [JsonProperty("reviews")]
        public IEnumerable<ReviewDTO> Reviews { get; set; }

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
    }
}