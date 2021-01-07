using HeatApp.Helpers;
using HeatApp.Interfaces;
using HeatApp.Models.HeatModels;
using HeatApp.ViewModels.Heat;
using HeatApp.Views.HeatViews.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Polyline = Xamarin.Forms.GoogleMaps.Polyline;

namespace HeatApp.Views.HeatViews.Common
{
    public partial class MainMapPage : ContentPage
    {
        Entry searchBar = null;
        IGoogleMapsApiService googleSerivce;
        List<Position> positions = new List<Position>();
        List<Position> busPositions = new List<Position>
        {
            new Position(18.482219, -69.919654),
            new Position(18.482239, -69.919767),
            new Position(18.482255, -69.919890),
            new Position(18.482255, -69.920008),
            new Position(18.482245, -69.920110),
            new Position(18.482250, -69.920212),
            new Position(18.482250, -69.920314),
            new Position(18.482255, -69.920384),
            new Position(18.482255, -69.920507),
            new Position(18.482275, -69.920609),
            new Position(18.482285, -69.920770),
            new Position(18.482412, -69.920765),
            new Position(18.482636, -69.920787),
            new Position(18.482891, -69.920813),
            new Position(18.483120, -69.920803)
        };
        RootPage rootPage;
        Pin buspin;
        MainMapViewModel viewModel;
        public MainMapPage(RootPage rootPage)
        {
            InitializeComponent();
            map.Pins.Clear();
            googleSerivce = DependencyService.Get<IGoogleMapsApiService>();
            viewModel = new MainMapViewModel(Navigation, map);
            BindingContext = viewModel;
            AddMapStyle();
            GetPostionsForRoute();
            this.rootPage = rootPage;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetCurrentLocation();
        }
        private async Task GetCurrentLocation()
        {
            try
            {
                map.Polylines.Clear();

                double latitude = 18.472384;
                double longitude = -69.921611;
                Pin defaultPin = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("user_marker"),
                    Label = "This is my home",
                    Address = "Here",
                    Position = new Position(latitude, longitude)
                };
                buspin = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("small_green_bus"),
                    Label = "This is my home",
                    Address = "Here",
                    Position = new Position(18.472216, -69.920815),
                    Rotation = (float)DegreeBearing(18.471061, -69.922200, 18.471896, -69.921201)
                };

                map.Pins.Add(defaultPin);
                map.Pins.Add(buspin);
                CameraUpdate newBoundsArea = CameraUpdateFactory.NewPositionZoom(new Position(buspin.Position.Latitude, buspin.Position.Longitude), 18);
                await map.MoveCamera(newBoundsArea);

            }
            catch (Exception)
            {

            }
        }
        private void SfButton_Clicked(object sender, EventArgs e)
        {
            rootPage.IsPresented = !rootPage.IsPresented;
            //GetCurrentLocation();
        }
        private void DrawRoute(List<Position> positions = null)
        {
            map.Polylines.Clear();
            Polyline polyline = new Polyline();
            polyline.StrokeColor = Color.FromHex("#F4A32C");
            polyline.StrokeWidth = 6;

            foreach (var p in positions)
            {
                polyline.Positions.Add(p);
            }

            var positionD = new Position(positions.LastOrDefault().Latitude, positions.LastOrDefault().Longitude);
            var positionO = new Position(positions.FirstOrDefault().Latitude, positions.FirstOrDefault().Longitude);
            polyline.Positions.Add(positionD);
            Pin pinC = new Pin
            {
                Icon = BitmapDescriptorFactory.FromBundle("bus_station"),
                Position = positionD,
                Label = "Parada Destino"
            };
            map.Pins.Add(pinC);
            Pin pinD = new Pin
            {
                Icon = BitmapDescriptorFactory.FromBundle("user_marker"),
                Position = positionO,
                Label = "Origen"
            };
            map.Pins.Add(pinD);

            map.Polylines.Add(polyline);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.50f)));
        }
        private async void DrawRoute(int routeID = 0)
        {
            map.Polylines.Clear();
            Polyline polyline = new Polyline();
            polyline.StrokeColor = Color.FromHex("#F4A32C");
            polyline.StrokeWidth = 3;

            int count = 0;
            Position previousPos = new Position();
            viewModel.SetLoadingMessage(Constants.Messages.SearchingStops);
            viewModel.StartBusy();
            foreach (var stop in viewModel.Stops.Where(p => p.RouteID == routeID).OrderBy(a => a.Order))
            {

                var position = new Position(stop.Latitude, stop.Longitude);
                var pinC = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("bus_station"),
                    Position = position,
                    Label = stop.Street,
                    Address = stop.Description,
                    BindingContext = stop
                };
                map.Pins.Add(pinC);

                if (count != 0)
                {
                    var googleDirections = await googleSerivce.GetDirections(previousPos.Latitude.ToString(), previousPos.Longitude.ToString(), position.Latitude.ToString(), position.Longitude.ToString());

                    if (googleDirections.Routes != null && googleDirections.Routes.Any())
                        foreach (var item in Enumerable.ToList(PolylineHelper.Decode(googleDirections.Routes.First().OverviewPolyline.Points)))
                        {
                            polyline.Positions.Add(item);
                        }
                }
                else
                {
                    polyline.Positions.Add(position);
                }

                previousPos = new Position(position.Latitude, position.Longitude);
                count++;
            }
            viewModel.EndBusy();

            map.Polylines.Add(polyline);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.34f)));
        }

        private void AddMapStyle()
        {
            Assembly assembly = typeof(MainPage).GetTypeInfo().Assembly;
            System.IO.Stream stream = assembly.GetManifestResourceStream($"HeatApp.MapStyle.json");
            string styleFile;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            {
                styleFile = reader.ReadToEnd();
            }

            map.MapStyle = MapStyle.FromJson(styleFile);
        }

        private void GetPostionsForRoute()
        {
            positions.Add(new Position(18.484200, -69.939930));
            positions.Add(new Position(18.482790, -69.932520));
            positions.Add(new Position(18.483360, -69.934910));
            positions.Add(new Position(18.484400, -69.936020));
        }

        private async Task ShowNearStopRoute()
        {
            double latitude = 18.472384;
            double longitude = -69.921611;
            double latitude2 = viewModel.Stops.LastOrDefault().Latitude;
            double longitude2 = viewModel.Stops.LastOrDefault().Longitude;

            DrawRoute(await viewModel.GetRouteToStop(new Position(latitude, longitude), new Position(latitude2, longitude2)));
        }
        private void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            headerSearch.IsVisible = true;
            lstRoutes.IsVisible = false;
            btnMenu.IsVisible = false;
            stkEntry.IsVisible = false;
            myLocation.IsVisible = true;
            followRoute.IsVisible = true;
            map.Pins.Clear();
            // int routeID = (lstRoutes.SelectedItem != null) ? (lstRoutes.SelectedItem as RouteDTO).ID : 0;
            DrawRoute(1);
        }
        // Do NOT mark async method.
        // Because Xamarin.Forms.GoogleMaps wait synchronously for this callback returns.
        async void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            e.Handled = true;

            if (e.Pin.BindingContext is StopDTO)
            {
                if (viewModel.GetStopInfoCommand.CanExecute(e.Pin.BindingContext as StopDTO))
                    viewModel.GetStopInfoCommand.Execute(e.Pin.BindingContext as StopDTO);
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new BusProfilePage(MoveBus)));
            }
        }
        private async void myLocation_Clicked(object sender, EventArgs e)
        {
            await GetCurrentLocation();
            myLocation.IsVisible = false;
            followRoute.IsVisible = false;
            headerSearch.IsVisible = false;
            lstRoutes.IsVisible = true;
            btnMenu.IsVisible = true;
            stkEntry.IsVisible = true;
        }
        private async void MoveBus()
        {
            map.Pins.Clear();
            map.Pins.Add(buspin);
            myLocation.IsVisible = true;
            foreach (Position position in busPositions)
            {
                Position lastPor = buspin.Position;
                Animation animation = new Animation(a => buspin.Position = new Position(position.Latitude, position.Longitude), 1, 2);
                animation.Commit(this, "PinAnimation", 300, 10000, Easing.SinInOut, null, () => false);
                buspin.Rotation = (float)DegreeBearing(lastPor.Latitude, lastPor.Longitude, buspin.Position.Latitude, buspin.Position.Longitude);
                await map.MoveCamera(CameraUpdateFactory.NewPosition(buspin.Position));
                await Task.Delay(300);
            }
        }
        double DegreeBearing(double lat1, double lon1, double lat2, double lon2)
        {
            double dLon = ToRad(lon2 - lon1);
            double dPhi = Math.Log(
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
        private async void followRoute_Clicked(object sender, EventArgs e)
        {
            map.Pins.Clear();
            await ShowNearStopRoute();
        }

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as Entry);
            if (lstRoutes.DataSource != null)
            {
                this.lstRoutes.DataSource.Filter = FilterContacts;
                this.lstRoutes.DataSource.RefreshFilter();
            }
        }

        private bool FilterContacts(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

            var contacts = obj as RouteDTO;
            if (contacts.Description.ToLower().Contains(searchBar.Text.ToLower())
                 || contacts.Description.ToLower().Contains(searchBar.Text.ToLower()))
                return true;
            else if ((contacts.Code?.ToLower().Contains(searchBar.Text.ToLower())).GetValueOrDefault(false)
                 || (contacts.Code?.ToLower().Contains(searchBar.Text.ToLower())).GetValueOrDefault(false))
                return true;
            else
                return false;
        }
    }
}