using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HeatApp.Services
{
    public class BaseService
    {
        #region Constructors
        public BaseService()
        {
            ApiHttpClient = ApiHttpClient ?? new HttpClient
            {
                BaseAddress = new Uri(Constants.UrlAPI)
            };
        }
        #endregion

        #region Propertties
        public HttpClient ApiHttpClient { get; }
        #endregion

        #region Methods
        public StringContent ToContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
        #endregion
    }
}