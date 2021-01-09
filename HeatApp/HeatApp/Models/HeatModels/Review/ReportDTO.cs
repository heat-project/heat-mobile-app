using System;
using Newtonsoft.Json;

namespace HeatApp.Models
{
    public class ReportDTO
    {
        [JsonProperty("reviewid")]
        public int ReviewID { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("typeid")]
        public int TypeID { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
