using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HeatApp.Models.HeatModels
{
    public class RouteDTO
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("codigo")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("ubicacion")]
        public string Location { get; set; }

        [JsonProperty("tiempoestimadosegundos")]
        public int Estimate { get; set; }

        [JsonProperty("conductorid")]
        public int ConductorID { get; set; }

        [JsonProperty("conductor")]
        public string Conductor { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}