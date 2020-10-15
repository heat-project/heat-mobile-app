using HeatApp.Views.HeatViews.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace HeatApp.Views.HeatViews.Common
{
    public partial class MainMapPage : ContentPage
    {
        RootPage rootPage;
        List<Position> positions = new List<Position>();
        public MainMapPage(RootPage rootPage)
        {
            InitializeComponent();
            GetPostionsForRoute();
            this.rootPage = rootPage;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetCurrentLocation();
        }
        private async void GetCurrentLocation()
        {
            try
            {
                map.Polylines.Clear();

                var latitude = 18.472384;
                var longitude = -69.921611;
                var defaultPin = new Pin { Type = PinType.Generic, Label = "This is my home", Address = "Here", Position = new Position(latitude, longitude) };
                var buspin = new Pin
                {
                    Icon = BitmapDescriptorFactory.FromBundle("small_green_bus"),
                    Label = "This is my home",
                    Address = "Here",
                    Position = new Position(18.472216, -69.920815),
                    Rotation = -43
                };


                map.Pins.Add(defaultPin);
                map.Pins.Add(buspin);
                var newBoundsArea = CameraUpdateFactory.NewPositionZoom(new Position(latitude, longitude), 18);
                await map.MoveCamera(newBoundsArea);

            }
            catch (Exception ex)
            {

            }
        }
        private void SfButton_Clicked(object sender, EventArgs e)
        {
            this.rootPage.IsPresented = !this.rootPage.IsPresented;
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

            }
            map.Polylines.Add(polyline);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.34f)));

            var pin = new Xamarin.Forms.GoogleMaps.Pin
            {
                Type = PinType.SearchResult,
                Position = new Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
                Label = "Pin",
                Address = "Pin"
            };
            map.Pins.Add(pin);
        }
        private void GetPostionsForRoute()
        {
            positions.Add(new Position(18.484200, -69.939930));
            positions.Add(new Position(18.484210, -69.939830));
            positions.Add(new Position(18.484220, -69.939700));
            positions.Add(new Position(18.484230, -69.939560));
            positions.Add(new Position(18.484230, -69.939430));
            positions.Add(new Position(18.484220, -69.939370));
            positions.Add(new Position(18.484220, -69.939300));
            positions.Add(new Position(18.484210, -69.939170));
            positions.Add(new Position(18.484190, -69.939050));
            positions.Add(new Position(18.484170, -69.938910));
            positions.Add(new Position(18.484140, -69.938800));
            positions.Add(new Position(18.483820, -69.937530));
            positions.Add(new Position(18.483700, -69.937060));
            positions.Add(new Position(18.483520, -69.936400));
            positions.Add(new Position(18.483480, -69.936250));
            positions.Add(new Position(18.483280, -69.935520));
            positions.Add(new Position(18.483090, -69.934790));
            positions.Add(new Position(18.483060, -69.934670));
            positions.Add(new Position(18.482990, -69.934420));
            positions.Add(new Position(18.482980, -69.934360));
            positions.Add(new Position(18.482960, -69.934310));
            positions.Add(new Position(18.482940, -69.934240));
            positions.Add(new Position(18.482920, -69.934120));
            positions.Add(new Position(18.482890, -69.933980));
            positions.Add(new Position(18.482870, -69.933850));
            positions.Add(new Position(18.482860, -69.933740));
            positions.Add(new Position(18.482840, -69.933480));
            positions.Add(new Position(18.482810, -69.933020));
            positions.Add(new Position(18.482790, -69.932520));
            positions.Add(new Position(18.482770, -69.932100));
            positions.Add(new Position(18.482740, -69.931400));
            positions.Add(new Position(18.482740, -69.931400));
            positions.Add(new Position(18.482770, -69.931360));
            positions.Add(new Position(18.482790, -69.931340));
            positions.Add(new Position(18.482830, -69.931310));
            positions.Add(new Position(18.482850, -69.931300));
            positions.Add(new Position(18.482880, -69.931300));
            positions.Add(new Position(18.482890, -69.931300));
            positions.Add(new Position(18.482900, -69.931300));
            positions.Add(new Position(18.482920, -69.931310));
            positions.Add(new Position(18.482930, -69.931320));
            positions.Add(new Position(18.482950, -69.931340));
            positions.Add(new Position(18.482970, -69.931370));
            positions.Add(new Position(18.482990, -69.931420));
            positions.Add(new Position(18.483000, -69.931660));
            positions.Add(new Position(18.483020, -69.932060));
            positions.Add(new Position(18.483020, -69.932180));
            positions.Add(new Position(18.483050, -69.932740));
            positions.Add(new Position(18.483060, -69.932960));
            positions.Add(new Position(18.483060, -69.933040));
            positions.Add(new Position(18.483070, -69.933150));
            positions.Add(new Position(18.483080, -69.933340));
            positions.Add(new Position(18.483090, -69.933720));
            positions.Add(new Position(18.483120, -69.933910));
            positions.Add(new Position(18.483140, -69.934060));
            positions.Add(new Position(18.483180, -69.934260));
            positions.Add(new Position(18.483360, -69.934910));
            positions.Add(new Position(18.483400, -69.935080));
            positions.Add(new Position(18.483400, -69.935090));
            positions.Add(new Position(18.483460, -69.935300));
            positions.Add(new Position(18.483470, -69.935350));
            positions.Add(new Position(18.483640, -69.935970));
            positions.Add(new Position(18.483640, -69.935970));
            positions.Add(new Position(18.483890, -69.936000));
            positions.Add(new Position(18.483890, -69.936000));
            positions.Add(new Position(18.483890, -69.935880));
            positions.Add(new Position(18.483900, -69.935870));
            positions.Add(new Position(18.483900, -69.935860));
            positions.Add(new Position(18.483910, -69.935860));
            positions.Add(new Position(18.483920, -69.935860));
            positions.Add(new Position(18.483940, -69.935860));
            positions.Add(new Position(18.483980, -69.935860));
            positions.Add(new Position(18.484030, -69.935840));
            positions.Add(new Position(18.484050, -69.935820));
            positions.Add(new Position(18.484100, -69.935780));
            positions.Add(new Position(18.484130, -69.935760));
            positions.Add(new Position(18.484160, -69.935750));
            positions.Add(new Position(18.484210, -69.935740));
            positions.Add(new Position(18.484240, -69.935750));
            positions.Add(new Position(18.484250, -69.935760));
            positions.Add(new Position(18.484270, -69.935790));
            positions.Add(new Position(18.484290, -69.935830));
            positions.Add(new Position(18.484300, -69.935880));
            positions.Add(new Position(18.484310, -69.935880));
            positions.Add(new Position(18.484320, -69.935890));
            positions.Add(new Position(18.484330, -69.935890));
            positions.Add(new Position(18.484340, -69.935900));
            positions.Add(new Position(18.484350, -69.935910));
            positions.Add(new Position(18.484360, -69.935910));
            positions.Add(new Position(18.484360, -69.935920));
            positions.Add(new Position(18.484370, -69.935930));
            positions.Add(new Position(18.484380, -69.935940));
            positions.Add(new Position(18.484380, -69.935950));
            positions.Add(new Position(18.484390, -69.935960));
            positions.Add(new Position(18.484390, -69.935970));
            positions.Add(new Position(18.484390, -69.935980));
            positions.Add(new Position(18.484400, -69.935990));
            positions.Add(new Position(18.484400, -69.936000));
            positions.Add(new Position(18.484400, -69.936010));
            positions.Add(new Position(18.484400, -69.936020));
        }
        private void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            headerSearch.IsVisible = true;
            lstRoutes.IsVisible = false;
            btnMenu.IsVisible = false;
            stkEntry.IsVisible = false;
            this.myLocation.IsVisible = true;
            DrawRoute();
        }
        // Do NOT mark async method.
        // Because Xamarin.Forms.GoogleMaps wait synchronously for this callback returns.
        void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            e.Handled = true;

            Navigation.PushModalAsync(new NavigationPage(new BusProfilePage()));
        }

        private void myLocation_Clicked(object sender, EventArgs e)
        {
            GetCurrentLocation();
            this.myLocation.IsVisible = false;
            headerSearch.IsVisible = false;
            lstRoutes.IsVisible = true;
            btnMenu.IsVisible = true;
            stkEntry.IsVisible = true;
        }
    }
}