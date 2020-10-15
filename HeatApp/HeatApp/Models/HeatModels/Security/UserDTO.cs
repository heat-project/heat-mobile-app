using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeatApp.Models
{
    public class UserDTO
    {
        [JsonProperty("Username")]
        public string UserName { get; set; }
        [JsonProperty("Nombre")]
        public string Name { get; set; }
        [JsonProperty("Apellido")]
        public string LastName { get; set; }
        [JsonProperty("Sexo")]
        public string Sex { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("Telefono")]
        public string Phone { get; set; }
        [JsonProperty("CorreoElectronico")]
        public string Email { get; set; }
        [JsonIgnore]
        public string FullName { get => $"{Name} {LastName}"; }
    }
}