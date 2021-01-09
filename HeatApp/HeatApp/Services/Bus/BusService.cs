using System;
using System.Collections.Generic;
using System.Text;
using HeatApp.Interfaces;
using HeatApp.Models;
using System.Threading.Tasks;
using HeatApp.Helpers;
using System.Net.Http;
using Newtonsoft.Json;

namespace HeatApp.Services
{
    public class BusService : BaseService, IBusService
    {

        public BusService()
        {
        }

        public Task<BusDTO> SetPosition()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BusDTO>> GetAll()
        {
            IEnumerable<BusDTO> buses = new List<BusDTO>();
            ApiHttpClient.DefaultRequestHeaders.Clear();
            ApiHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Settings.Token}");
            HttpResponseMessage response = await ApiHttpClient.GetAsync($"{Constants.UrlAPI}api/Vehicles/GetAllActives");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var busResponse = JsonConvert.DeserializeObject<DataResponse<IEnumerable<BusDTO>>>(responseBody);

            if (busResponse != null && busResponse.Success)
                buses = busResponse.Data;

            return buses;
        }

        public async Task<BusInfoDTO> GetByID(int id)
        {
            var busInfo = new BusInfoDTO();
            ApiHttpClient.DefaultRequestHeaders.Clear();
            ApiHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Settings.Token}");
            HttpResponseMessage response = await ApiHttpClient.GetAsync($"{Constants.UrlAPI}api/Vehicles/GetVehicleInfo?id={id}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var busResponse = JsonConvert.DeserializeObject<DataResponse<BusInfoDTO>>(responseBody);

            if (busResponse != null && busResponse.Success)
                busInfo = busResponse.Data;

            return busInfo;
        }
    }
}