using System;
using Newtonsoft.Json;

namespace HeatApp.Models.HeatModels.Bus
{
    public class ReviewDTO
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("vehicleid")]
        public int BusID { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("rating")]
        public double? Rating { get; set; }
    }
}