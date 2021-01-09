using System;
using Newtonsoft.Json;

namespace HeatApp.Models.HeatModels.Bus
{
    public class BusInfoDetailDTO
    {
        [JsonProperty("vehicleid")]
        public int BusID { get; set; }

        [JsonProperty("atributteid")]
        public int AtributteID { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}