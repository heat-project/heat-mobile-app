using HeatApp.Helpers;
using HeatApp.Interfaces;
using HeatApp.Interfaces.Routes;
using HeatApp.Models;
using HeatApp.Models.Dashboard;
using HeatApp.Models.HeatModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace HeatApp.ViewModels.Heat
{
    public class MainMapViewModel : BaseViewModel
    {
        #region Constructors
        public MainMapViewModel(INavigation navigation, Map map) : base(navigation)
        {
            SetServices();
            GetRoute();
            GetStops(map);
        }
        #endregion

        #region Services
        private IRouteService RouteService;
        private IGoogleMapsApiService GoogleMapsService;
        #endregion

        #region Propertties
        private ObservableCollection<StopDTO> stops = new ObservableCollection<StopDTO>();
        public ObservableCollection<StopDTO> Stops
        {
            get => stops;
            set => SetProperty(ref stops, value);
        }
        private ObservableCollection<BusDTO> buses = new ObservableCollection<BusDTO>();
        public ObservableCollection<BusDTO> Buses
        {
            get => buses;
            set => SetProperty(ref buses, value);
        }
        private ObservableCollection<HealthCare> cardItems = new ObservableCollection<HealthCare>();
        public ObservableCollection<HealthCare> CardItems
        {
            get => cardItems;
            set => SetProperty(ref cardItems, value);
        }
        #endregion

        #region Methods
        private void SetServices()
        {
            RouteService = DependencyService.Get<IRouteService>();
            GoogleMapsService = DependencyService.Get<IGoogleMapsApiService>();
        }
        private void GetRoute()
        {
            CardItems = new ObservableCollection<HealthCare>()
            {
                new HealthCare()
                {
                    Category = "HEART RATE",
                    CategoryValue = "87 bmp",
                    BackgroundGradientStart = "#F4A32C",
                },
                new HealthCare()
                {
                    Category = "CALORIES BURNED",
                    CategoryValue = "948 cal",
                    BackgroundGradientStart = "#ED694A"
                },
                new HealthCare()
                {
                    Category = "SLEEP TIME",
                    CategoryValue = "7.3 hrs",
                    BackgroundGradientStart = "#F4D44E"
                },
                new HealthCare()
                {
                    Category = "WATER CONSUMED",
                    CategoryValue = "38.6 ltr",
                    BackgroundGradientStart = "#006992"
                }
            };
        }
        private async void GetStops(Map map)
        {
            Stops = new ObservableCollection<StopDTO>(await RouteService.GetStops(1));
            foreach (var item in Stops)
            {
                var pin = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("bus_station"),
                    Label = item.Title,
                    Address = item.Description,
                    Position = new Position(item.Latitude, item.Longitude)
                };
                map.Pins.Add(pin);
            }
        }
        public async Task<List<Position>> GetRouteToStop(Position origin, Position destination)
        {
            var googleDirection = await GoogleMapsService.GetDirections($"{origin.Latitude}", $"{origin.Longitude}", $"{destination.Latitude}", $"{destination.Longitude}");
            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.FirstOrDefault().OverviewPolyline.Points)));
                return positions;
            }

            return new List<Position>();
        }
        #endregion

        #region AuxiliarMethods

        double DegreeBearing(
            double lat1, double lon1,
            double lat2, double lon2)
        {
            var dLon = ToRad(lon2 - lon1);
            var dPhi = Math.Log(
                Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ToBearing(Math.Atan2(dLon, dPhi));
        }
        public static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
        public static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }
        public static double ToBearing(double radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ToDegrees(radians) + 360) % 360;
        }
        #endregion
    }
}