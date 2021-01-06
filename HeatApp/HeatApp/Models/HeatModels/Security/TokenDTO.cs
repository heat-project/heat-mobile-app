using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeatApp.Models
{
    public class TokenDTO
    {
        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Sex")]
        public string Sex { get; set; }

    }
}