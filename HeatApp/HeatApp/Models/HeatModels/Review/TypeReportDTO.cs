using System;
using Newtonsoft.Json;

namespace HeatApp.Models
{
    public class TypeReportDTO
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
