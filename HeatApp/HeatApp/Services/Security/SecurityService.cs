using HeatApp.Helpers;
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
            var tokenDTO = JsonConvert.DeserializeObject<TokenDTO>(responseBody);

            if (tokenDTO != null && !string.IsNullOrEmpty(tokenDTO.Token) && !string.IsNullOrWhiteSpace(tokenDTO.Token))
            {
                Settings.Token = tokenDTO.Token;
                Settings.Email = tokenDTO.Email;
                Settings.Phone = tokenDTO.Phone;
                Settings.FullName = tokenDTO.FullName;
                Settings.Sex = tokenDTO.Sex;
            }

            return tokenDTO;
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