using HeatApp.Helpers;
using HeatApp.Interfaces;
using HeatApp.Interfaces.Map;
using HeatApp.Interfaces.Routes;
using HeatApp.Models;
using HeatApp.Models.Dashboard;
using HeatApp.Models.HeatModels;
using HeatApp.Views.HeatViews.Bus;
using HeatApp.Views.HeatViews.Common;
using Microsoft.AspNetCore.SignalR.Client;
using Plugin.Geolocator.Abstractions;
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
using Position = Xamarin.Forms.GoogleMaps.Position;

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
            SetConnectionHub();
        }
        #endregion

        #region Services
        private IRouteService RouteService;
        private IGoogleMapsApiService GoogleMapsService;
        private IMapService MapService;
        private IBusService BusService;
        #endregion

        #region Commands
        private ICommand getStopInfoCommand;
        public ICommand GetStopInfoCommand => (getStopInfoCommand ?? new Command<StopDTO>(async (stop) => await GetStopInfo(stop)));

        private ICommand goToStopCommand;
        public ICommand GoToStopCommand => (goToStopCommand ?? new Command<StopDTO>(async (stop) => await GoToStop(stop)));

        private ICommand showRouteCommand;
        public ICommand ShowRouteCommand => (showRouteCommand ?? new Command<object>(async (obj) => await ShowRoute(obj)));

        private ICommand showMyLocationCommand;
        public ICommand ShowMyLocationCommand => (showMyLocationCommand ?? new Command(async () => await GoToMyLocation()));

        private ICommand goToNearstStopCommand;
        public ICommand GoToNearstStopCommand => (goToNearstStopCommand ?? new Command(async () => await GoToNearstStop()));

        private ICommand goToBusProfileCommand;
        public ICommand GoToBusProfileCommand => (goToBusProfileCommand ?? new Command<BusDTO>(async (bus) => await GoToBusProfile(bus)));
        #endregion

        #region Propertties
        private TrackerHelper TrackerHelper;

        private Map map;

        private int selectedRouteID;
        public int SelectedRouteID
        {
            get => selectedRouteID;
            set => SetProperty(ref selectedRouteID, value);
        }

        private Pin user;
        public Pin UserPin
        {
            get => user;
            set => SetProperty(ref user, value);
        }

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

        private bool showRoutes = true;
        public bool ShowRoutes
        {
            get => showRoutes;
            set => SetProperty(ref showRoutes, value);
        }

        private bool showMenu = true;
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
        private async Task GoToBusProfile(BusDTO bus)
        {
            if (IsBusy) return;

            try
            {
                StartBusy();
                await navigation.PushModalAsync(new NavigationPage(new BusProfilePage(bus, UpdateBusPosition)));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                EndBusy();
            }
        }

        private async Task GoToMyLocation()
        {
            if (IsBusy) return;
            try
            {
                StartBusy();    
                ShowLocation = false;
                ShowFollowRoute = false;
                ShowHeaderSearch = false;
                ShowRoutes = true;
                ShowMenu = true;
                MapService.Map.Polylines.Clear();
                await map.MoveCamera(CameraUpdateFactory.NewPositionZoom(UserPin.Position, 18));
                await GetStops(map);
                await GetAllBuses();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                EndBusy();
            }
        }

        private async Task ShowRoute(object obj)
        {
            if (IsBusy) return;
            var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            var previousPos = new Position();
            int count = 0;

            ShowHeaderSearch = true;
            ShowLocation = true;
            ShowFollowRoute = true;
            ShowRoutes = false;
            ShowMenu = false;

            try
            {
                var ItemData = (obj as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as RouteDTO;


                StartBusy();
                SetLoadingMessage(Constants.Messages.SearchingStops);

                MapService.Map.Polylines.Clear();
                MapService.Map.Pins.Clear();
                MapService.Map.Pins.Add(UserPin);
                SelectedRouteID = ItemData.ID;
                foreach (var item in Stops.Where(p => p.RouteID == ItemData.ID))
                {
                    MapService.Map.Pins.Add(BusStationPin(new Position(item.Latitude, item.Longitude), item));

                    if (count == 0)
                        polyline.Positions.Add(new Position(item.Latitude, item.Longitude));
                    else
                    {
                        var googleDirections = await GoogleMapsService.GetDirections(previousPos.Latitude.ToString(),
                                                                      previousPos.Longitude.ToString(),
                                                                      item.Latitude.ToString(),
                                                                      item.Longitude.ToString());

                        if (googleDirections.Routes != null && googleDirections.Routes.Any())
                        {
                            foreach (var poli in Enumerable.ToList(PolylineHelper.Decode(googleDirections.Routes.First().OverviewPolyline.Points)))
                            {
                                polyline.Positions.Add(poli);
                            }

                        }
                    }
                    previousPos = new Position(item.Latitude, item.Longitude);
                    count++;
                }

                MapService.DrawRoute(polyline);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.34f)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                EndBusy();
            }
        }

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
            try
            {
                StartBusy();
                await GetRoute();
                await GetStops(map);
                var location = await LocationHelper.GetCurrentPosition();
                LocationHelper.PositionChanged += PositionChanged;
                await LocationHelper.StartListening();
                CurrentLatitude = location.Latitude;
                CurrentLongitude = location.Longitude;
                SetUserPin(new Position(CurrentLatitude, CurrentLongitude));
                map.Pins.Add(UserPin);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(UserPin.Position, Distance.FromMiles(0.30f)));
                await GetAllBuses();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                EndBusy();
            }
        }

        private async Task GoToStop(StopDTO stop, bool skipBusy = false)
        {

            if (IsBusy && !skipBusy) return;

            try
            {
                StartBusy();
                map.Pins.Clear();

                SetUserPin(new Position(CurrentLatitude, CurrentLongitude));
                map.Pins.Add(UserPin);
                map.Pins.Add(BusStationPin(new Position(stop.Latitude, stop.Longitude), stop));

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

        private async Task GoToNearstStop()
        {

            if (IsBusy) return;
            try
            {
                StartBusy();
                var stop = new StopDTO();
                var nearestStop = new Position();
                double auxDistance = 0;
                foreach (var item in Stops.Where(p => p.RouteID == SelectedRouteID))
                {
                    var thisDistance = GeolocatorUtils.CalculateDistance(UserPin.Position.Latitude, UserPin.Position.Longitude, item.Latitude, item.Longitude, GeolocatorUtils.DistanceUnits.Kilometers);
                    if (nearestStop.Longitude == 0 && nearestStop.Latitude == 0)
                    {
                        nearestStop = new Position(item.Latitude, item.Longitude);
                        auxDistance = thisDistance;
                        stop = item;
                    }
                    else
                    {
                        if (thisDistance < auxDistance)
                        {
                            nearestStop = new Position(item.Latitude, item.Longitude);
                            auxDistance = thisDistance;
                            stop = item;
                        }
                    }
                }

                await GoToStop(stop, true);
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
        private async Task GetAllBuses()
        {
            try
            {
                Buses = new ObservableCollection<BusDTO>(await BusService.GetAll());

                if (Buses.Any())
                {
                    foreach (var item in Buses)
                    {
                        MapService.Map.Pins.Add(BusPin(item));
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private async void SetConnectionHub()
        {
            TrackerHelper = new TrackerHelper();
            await TrackerHelper.Connect();
            TrackerHelper.Connection.On<string, string>("SetVehiclePosition", (id, location) =>
             {
                 var bus = new BusDTO
                 {
                     ID = int.Parse(id),
                     Location = location
                 };
                 UpdateBusPosition(bus);
             });
        }

        private Pin BusStationPin(Position position, StopDTO stop)
        {
            return new Pin
            {
                Icon = BitmapDescriptorFactory.FromBundle("bus_station"),
                Position = position,
                Label = stop.Street ?? stop.Description,
                Address = stop.Description,
                BindingContext = stop
            };
        }

        private Pin BusPin(BusDTO bus)
        {
            return new Pin
            {
                Icon = BitmapDescriptorFactory.FromBundle("small_green_bus"),
                Position = new Position(bus.Latitude, bus.Longitude),
                Label = bus.Description,
                Address = bus.Description,
                BindingContext = bus
            };
        }

        private void SetUserPin(Position position)
        {
            if (UserPin == null)

                UserPin = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("user_marker"),
                    Position = position,
                    Label = "Origen"
                };
            else
                UserPin.Position = position;
        }

        private void SetServices()
        {
            RouteService = DependencyService.Get<IRouteService>();
            BusService = DependencyService.Get<IBusService>();
            GoogleMapsService = DependencyService.Get<IGoogleMapsApiService>();
            MapService = DependencyService.Get<IMapService>();
            MapService.Map = map;
        }

        private double DegreeBearing(double lat1, double lon1, double lat2, double lon2)
        {
            var dLon = ToRad(lon2 - lon1);
            var dPhi = Math.Log(
                Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ToBearing(Math.Atan2(dLon, dPhi));
        }

        private static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        private static double ToBearing(double radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ToDegrees(radians) + 360) % 360;
        }

        private void UpdateBusPosition(BusDTO bus)
        {
            var busPins = MapService.Map.Pins.Where(p => p.BindingContext is BusDTO);

            foreach (var item in busPins)
            {
                var busContext = item.BindingContext as BusDTO;

                if (busContext.ID == bus.ID)
                {
                    var buspin = item;
                    Position lastPor = item.Position;
                    buspin.Position = new Position(bus.Latitude, bus.Longitude);
                    buspin.Rotation = (float)DegreeBearing(lastPor.Latitude, lastPor.Longitude, bus.Latitude, bus.Longitude);
                    break;
                }
            }
        }
        #endregion

        #region Events
        private void PositionChanged(object sender, PositionEventArgs e)
        {
            //If updating the UI, ensure you invoke on main thread
            var position = e.Position;

            CurrentLatitude = position.Latitude;
            CurrentLongitude = position.Longitude;
            SetUserPin(new Position(CurrentLatitude, CurrentLongitude));
        }
        #endregion
    }
}