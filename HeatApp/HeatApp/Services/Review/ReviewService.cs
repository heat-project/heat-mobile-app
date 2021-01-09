using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HeatApp.Helpers;
using HeatApp.Interfaces;
using HeatApp.Models;
using HeatApp.Models.HeatModels.Bus;
using Newtonsoft.Json;

namespace HeatApp.Services.Review
{
    public class ReviewService : BaseService, IReviewService
    {
        public async Task<IEnumerable<ReviewDTO>> Get(int busID)
        {
            IEnumerable<ReviewDTO> reviews = new List<ReviewDTO>();
            ApiHttpClient.DefaultRequestHeaders.Clear();
            ApiHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Settings.Token}");
            HttpResponseMessage response = await ApiHttpClient.GetAsync($"{Constants.UrlAPI}api/Review/Get?vid={busID}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var routeResponse = JsonConvert.DeserializeObject<DataResponse<IEnumerable<ReviewDTO>>>(responseBody);

            if (routeResponse != null && routeResponse.Success)
                reviews = routeResponse.Data;

            return reviews;
        }

        public async Task<IEnumerable<TypeReportDTO>> GetTypeReport()
        {
            IEnumerable<TypeReportDTO> reviews = new List<TypeReportDTO>();
            ApiHttpClient.DefaultRequestHeaders.Clear();
            ApiHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Settings.Token}");
            HttpResponseMessage response = await ApiHttpClient.GetAsync($"{Constants.UrlAPI}api/Review/GetTypesReport");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var routeResponse = JsonConvert.DeserializeObject<DataResponse<IEnumerable<TypeReportDTO>>>(responseBody);

            if (routeResponse != null && routeResponse.Success)
                reviews = routeResponse.Data;

            return reviews;
        }

        public async Task<bool> Report(ReportDTO report)
        {
            bool isSaved = false;
            ApiHttpClient.DefaultRequestHeaders.Clear();
            ApiHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Settings.Token}");
            HttpResponseMessage response = await ApiHttpClient.PostAsync($"{Constants.UrlAPI}api/Review/Report", ToContent(report));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var routeResponse = JsonConvert.DeserializeObject<DataResponse<bool>>(responseBody);

            if (routeResponse != null && routeResponse.Success)
                isSaved = routeResponse.Data;

            return isSaved;
        }

        public async Task<bool> Save(ReviewDTO report)
        {
            bool isSaved = false;
            ApiHttpClient.DefaultRequestHeaders.Clear();
            ApiHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Settings.Token}");
            HttpResponseMessage response = await ApiHttpClient.PostAsync($"{Constants.UrlAPI}api/Review/Save", ToContent(report));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var routeResponse = JsonConvert.DeserializeObject<DataResponse<bool>>(responseBody);

            if (routeResponse != null && routeResponse.Success)
                isSaved = routeResponse.Data;

            return isSaved;
        }
    }
}
