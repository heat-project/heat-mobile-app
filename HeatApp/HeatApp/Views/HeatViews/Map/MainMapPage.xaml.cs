using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace HeatApp.Views.HeatViews.Common
{
    public partial class MainMapPage : ContentPage
    {
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
        public MainMapPage(RootPage rootPage)
        {
            InitializeComponent();
            map.Pins.Clear();
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

                var latitude = 18.472384;
                var longitude = -69.921611;
                var defaultPin = new Pin
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
                var newBoundsArea = CameraUpdateFactory.NewPositionZoom(new Position(buspin.Position.Latitude, buspin.Position.Longitude), 18);
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
        private void DrawRoute()
        {
            map.Polylines.Clear();
            var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            polyline.StrokeColor = Color.FromHex("#F4A32C");
            polyline.StrokeWidth = 6;

            foreach (var p in positions)
            {
                polyline.Positions.Add(p);

                var pinC = new Pin
                {
                    Type = PinType.SearchResult,
                    Position = new Position(p.Latitude, p.Longitude),
                    Label = "Pin",
                    Address = "Pin"
                };
                map.Pins.Add(pinC);
            }
            map.Polylines.Add(polyline);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[1].Latitude, polyline.Positions[1].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.34f)));
        }
        private void GetPostionsForRoute()
        {
            positions.Add(new Position(18.484200, -69.939930));
            positions.Add(new Position(18.482790, -69.932520));
            positions.Add(new Position(18.483360, -69.934910));
            positions.Add(new Position(18.484400, -69.936020));
        }
        private void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            headerSearch.IsVisible = true;
            lstRoutes.IsVisible = false;
            btnMenu.IsVisible = false;
            stkEntry.IsVisible = false;
            myLocation.IsVisible = true;
            DrawRoute();
        }
        // Do NOT mark async method.
        // Because Xamarin.Forms.GoogleMaps wait synchronously for this callback returns.
        async void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            e.Handled = true;

            //Navigation.PushModalAsync(new NavigationPage(new BusProfilePage()));
            await MoveBus();
        }
        private async void myLocation_Clicked(object sender, EventArgs e)
        {
            await GetCurrentLocation();
            myLocation.IsVisible = false;
            headerSearch.IsVisible = false;
            lstRoutes.IsVisible = true;
            btnMenu.IsVisible = true;
            stkEntry.IsVisible = true;
        }
        private async Task MoveBus()
        {
            foreach (var position in busPositions)
            {
                var lastPor = buspin.Position;
                var animation = new Animation(a => buspin.Position = new Position(position.Latitude, position.Longitude), 1, 2);
                animation.Commit(this, "PinAnimation", 300, 10000, Easing.SinInOut, null, () => false);
                buspin.Rotation = (float)DegreeBearing(lastPor.Latitude, lastPor.Longitude, buspin.Position.Latitude, buspin.Position.Longitude);
                await map.MoveCamera(CameraUpdateFactory.NewPosition(buspin.Position));
                await Task.Delay(100);
            }
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

    }
}