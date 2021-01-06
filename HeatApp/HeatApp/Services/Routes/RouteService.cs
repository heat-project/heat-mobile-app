using HeatApp.Helpers;
using HeatApp.Interfaces.Routes;
using HeatApp.Models.HeatModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HeatApp.Services
{
    public class RouteService : BaseService, IRouteService
    {
        #region Methods
        public async Task<IEnumerable<RouteDTO>> GetAll()
        {
            IEnumerable<RouteDTO> routes = new List<RouteDTO>();
            ApiHttpClient.DefaultRequestHeaders.Clear();
            ApiHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Settings.Token}");
            HttpResponseMessage response = await ApiHttpClient.GetAsync($"{Constants.UrlAPI}api/Route/GetAllInfo");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var routeResponse = JsonConvert.DeserializeObject<DataResponse<IEnumerable<RouteDTO>>>(responseBody);

            if (routeResponse != null && routeResponse.Success)
                routes = routeResponse.Data;

            return routes;
        }

        public async Task<IEnumerable<StopDTO>> GetStops(int routeID = 0)
        {

            IEnumerable<StopDTO> stops = new List<StopDTO>();
            ApiHttpClient.DefaultRequestHeaders.Clear();
            ApiHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Settings.Token}");
            HttpResponseMessage response = await ApiHttpClient.GetAsync($"{Constants.UrlAPI}api/Stop/GetByRouteID?rid={routeID}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var routeResponse = JsonConvert.DeserializeObject<DataResponse<IEnumerable<StopDTO>>>(responseBody);

            if (routeResponse != null && routeResponse.Success)
                stops = routeResponse.Data;

            return stops;
        }
        public async Task<List<RouteDTO>> GetFavorites()
        {
            return new List<RouteDTO>
            {
                new RouteDTO
                {
                    Code = "C12B",
                    From = "Av.Los Próceres",
                    To = "Av. Correa y Cidrón"
                }
            };
        }
        public async Task<List<RouteDTO>> GetPreviouslySeen()
        {
            return new List<RouteDTO>
            {
                new RouteDTO
                {
                    Code = "C12B",
                    From = "Av.Los Próceres",
                    To = "Av. Correa y Cidrón"
                },
                new RouteDTO
                {
                    Code = "C11S",
                    From = "Av. Correa y Cidrón",
                    To = "Av. Independencia"
                }
            };
        }
        #endregion
    }
}