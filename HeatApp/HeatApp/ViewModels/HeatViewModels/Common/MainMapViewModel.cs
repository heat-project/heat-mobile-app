using HeatApp.Helpers;
using HeatApp.Interfaces;
using HeatApp.Interfaces.Map;
using HeatApp.Interfaces.Routes;
using HeatApp.Models;
using HeatApp.Models.Dashboard;
using HeatApp.Models.HeatModels;
using HeatApp.Views.HeatViews.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace HeatApp.ViewModels.Heat
{
    public class MainMapViewModel : BaseViewModel
    {
        #region Constructors
        public MainMapViewModel(INavigation navigation, Map map) : base(navigation)
        {
            this.map = map;
            SetServices();
            FillMap();
        }
        #endregion

        #region Services
        private IRouteService RouteService;
        private IGoogleMapsApiService GoogleMapsService;
        private IMapService MapService;
        #endregion

        #region Commands
        private ICommand getStopInfoCommand;
        public ICommand GetStopInfoCommand => (getStopInfoCommand ?? new Command<StopDTO>(async (stop) => await GetStopInfo(stop)));


        private ICommand goToStopCommand;
        public ICommand GoToStopCommand => (goToStopCommand ?? new Command<StopDTO>(async (stop) => await GoToStop(stop)));
        #endregion

        #region Propertties
        private Map map;

        private string from = string.Empty;
        public string From
        {
            get => from;
            set => SetProperty(ref from, value);
        }

        private string to = string.Empty;
        public string To
        {
            get => to;
            set => SetProperty(ref to, value);
        }

        private double currentLatitude = 0;
        public double CurrentLatitude
        {
            get => currentLatitude;
            set => SetProperty(ref currentLatitude, value);
        }

        private double currentLongitude = 0;
        public double CurrentLongitude
        {
            get => currentLongitude;
            set => SetProperty(ref currentLongitude, value);
        }

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

        private ObservableCollection<RouteDTO> cardItems = new ObservableCollection<RouteDTO>();
        public ObservableCollection<RouteDTO> CardItems
        {
            get => cardItems;
            set => SetProperty(ref cardItems, value);
        }

        private bool showHeaderSearch = false;
        public bool ShowHeaderSearch
        {
            get => showHeaderSearch;
            set => SetProperty(ref showHeaderSearch, value);
        }

        private bool showRoutes = false;
        public bool ShowRoutes
        {
            get => showRoutes;
            set => SetProperty(ref showRoutes, value);
        }

        private bool showMenu = false;
        public bool ShowMenu
        {
            get => showMenu;
            set => SetProperty(ref showMenu, value);
        }

        private bool showLocation = false;
        public bool ShowLocation
        {
            get => showLocation;
            set => SetProperty(ref showLocation, value);
        }

        private bool showFollowRoute = false;
        public bool ShowFollowRoute
        {
            get => showFollowRoute;
            set => SetProperty(ref showFollowRoute, value);
        }

        private bool showStopInfo = false;
        public bool ShowStopInfo
        {
            get => showStopInfo;
            set => SetProperty(ref showStopInfo, value);
        }

        private StopDTO stop = new StopDTO();
        public StopDTO Stop
        {
            get => stop;
            set => SetProperty(ref stop, value);
        }
        #endregion

        #region Methods
        private async Task GetStopInfo(StopDTO stop)
        {
            this.Stop = stop;
            this.ShowStopInfo = true;
            this.ShowRoutes = this.ShowMenu = false;
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(stop.Latitude, stop.Longitude), Distance.FromMiles(0.34f)));
            var newBoundsArea = CameraUpdateFactory.NewPositionZoom(new Position(stop.Latitude, stop.Longitude), 22);
            await map.MoveCamera(newBoundsArea);
        }

        private async Task GetRoute()
        {
            try
            {
                CardItems = new ObservableCollection<RouteDTO>(await RouteService.GetAll());
            }
            catch (HttpRequestException)
            {
                App.Current.MainPage = new NavigationPage(new InitialPage());
            }
            catch (Exception ex)
            {

            }
        }

        private async Task GetStops(Map map)
        {
            try
            {
                Stops = new ObservableCollection<StopDTO>(await RouteService.GetStops());
                foreach (var item in Stops)
                {
                    var pin = new Pin
                    {
                        Icon = BitmapDescriptorFactory.FromBundle("bus_station"),
                        Label = item.Street,
                        Address = item.Description,
                        Position = new Position(item.Latitude, item.Longitude),
                        BindingContext = item
                    };
                    map.Pins.Add(pin);
                }
            }
            catch (HttpRequestException)
            {
                App.Current.MainPage = new NavigationPage(new InitialPage());
            }
            catch (Exception) { }
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

        private async void FillMap()
        {
            await GetRoute();
            await GetStops(map);
            var location = await MapService.GetCurrentLocation();
            CurrentLatitude = location.Latitude;
            CurrentLongitude = location.Longitude;
        }

        private async Task GoToStop(StopDTO stop)
        {

            if (IsBusy) return;

            try
            {
                StartBusy();
                map.Pins.Clear();

                map.Pins.Add(UserPin(new Position(CurrentLatitude, CurrentLongitude)));
                map.Pins.Add(BusStationPin(new Position(stop.Latitude, stop.Longitude)));

                var polyline = new Xamarin.Forms.GoogleMaps.Polyline();

                var googleDirections = await GoogleMapsService.GetDirections(CurrentLatitude.ToString(),
                                                                             CurrentLongitude.ToString(),
                                                                             stop.Latitude.ToString(),
                                                                             stop.Longitude.ToString(),
                                                                             mode: "walking");

                if (googleDirections.Routes != null && googleDirections.Routes.Any())
                {
                    From = googleDirections.Routes.FirstOrDefault().Legs.FirstOrDefault().StartAddress;
                    To = stop.Description;
                    foreach (var item in Enumerable.ToList(PolylineHelper.Decode(googleDirections.Routes.First().OverviewPolyline.Points)))
                    {
                        polyline.Positions.Add(item);
                    }

                    MapService.DrawRoute(polyline);
                    await map.MoveCamera(CameraUpdateFactory.NewPositionZoom(new Position(CurrentLatitude, CurrentLongitude), 18));
                    ShowStopInfo = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                EndBusy();
            }
        }
        #endregion

        #region AuxiliarMethods
        private Pin BusStationPin(Position position)
        {
            return new Pin
            {
                Icon = BitmapDescriptorFactory.FromBundle("bus_station"),
                Position = position,
                Label = stop.Street,
                Address = stop.Description,
                BindingContext = stop
            };
        }
        private Pin UserPin(Position position)
        {
            return new Pin
            {
                Icon = BitmapDescriptorFactory.FromBundle("user_marker"),
                Position = position,
                Label = "Origen"
            }; ;
        }
        private void SetServices()
        {
            RouteService = DependencyService.Get<IRouteService>();
            GoogleMapsService = DependencyService.Get<IGoogleMapsApiService>();
            MapService = DependencyService.Get<IMapService>();
            MapService.Map = map;
        }

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