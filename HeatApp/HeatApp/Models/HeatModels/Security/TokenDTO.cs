using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeatApp.Models
{
    public class TokenDTO
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}