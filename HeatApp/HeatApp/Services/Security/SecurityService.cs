using HeatApp.Interfaces;
using HeatApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HeatApp.Services
{
    public class SecurityService : BaseService, ISecurityService
    {
        #region Constructors
        public SecurityService() : base() { }
        #endregion

        #region Methods
        public async Task<TokenDTO> Login(UserDTO user)
        {
            HttpResponseMessage response = await ApiHttpClient.PostAsync($"{Constants.UrlAPI}api/authenticate", ToContent(user));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenDTO>(responseBody);
        }
        public async Task<UserDTO> Register(UserDTO user)
        {
            HttpResponseMessage response = await ApiHttpClient.PostAsync($"{Constants.UrlAPI}api/Register", ToContent(user));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDTO>(responseBody);
        }
        #endregion
    }
}