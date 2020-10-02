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
        public MainMapPage(RootPage rootPage)
        {
            InitializeComponent();
            //Position position = new Position(36.9628066, -122.0194722);
            //Pin pin = new Pin
            //{
            //    Label = "Casa",
            //    Address = "Esta es mi casa",
            //    Type = PinType.Place,
            //    Position = position
            //};
            //map.Pins.Add(pin);
            this.rootPage = rootPage;
            rootPage.IsPresentedChanged += SfButton_Clicked;
            // MapTypes
            //var mapTypeValues = new List<MapType>();
            //foreach (var mapType in Enum.GetValues(typeof(MapType)))
            //{
            //    mapTypeValues.Add((MapType)mapType);
            //    pickerMapType.Items.Add(Enum.GetName(typeof(MapType), mapType));
            //}

            //pickerMapType.SelectedIndexChanged += (sender, e) =>
            //{
            //    map.MapType = (Xamarin.Forms.GoogleMaps.MapType)mapTypeValues[pickerMapType.SelectedIndex];
            //};
            //pickerMapType.SelectedIndex = 0;

            //// MyLocationEnabled
            //switchMyLocationEnabled.Toggled += (sender, e) =>
            //{
            //    map.MyLocationEnabled = e.Value;
            //};
            //switchMyLocationEnabled.IsToggled = map.MyLocationEnabled;

            //// IsTrafficEnabled
            //switchIsTrafficEnabled.Toggled += (sender, e) =>
            //{
            //    map.IsTrafficEnabled = e.Value;
            //};
            //switchIsTrafficEnabled.IsToggled = map.IsTrafficEnabled;

            //// IndoorEnabled
            //switchIndoorEnabled.Toggled += (sender, e) =>
            //{
            //    map.IsIndoorEnabled = e.Value;
            //};
            //switchIndoorEnabled.IsToggled = map.IsIndoorEnabled;

            //// CompassEnabled
            //switchCompassEnabled.Toggled += (sender, e) =>
            //{
            //    map.UiSettings.CompassEnabled = e.Value;
            //};
            //switchCompassEnabled.IsToggled = map.UiSettings.CompassEnabled;

            //// RotateGesturesEnabled
            //switchRotateGesturesEnabled.Toggled += (sender, e) =>
            //{
            //    map.UiSettings.RotateGesturesEnabled = e.Value;
            //};
            //switchRotateGesturesEnabled.IsToggled = map.UiSettings.RotateGesturesEnabled;

            //// MyLocationButtonEnabled
            //switchMyLocationButtonEnabled.Toggled += (sender, e) =>
            //{
            //    map.UiSettings.MyLocationButtonEnabled = e.Value;
            //};
            //switchMyLocationButtonEnabled.IsToggled = map.UiSettings.MyLocationButtonEnabled;

            //// IndoorLevelPickerEnabled
            //switchIndoorLevelPickerEnabled.Toggled += (sender, e) =>
            //{
            //    map.UiSettings.IndoorLevelPickerEnabled = e.Value;
            //};
            //switchIndoorLevelPickerEnabled.IsToggled = map.UiSettings.IndoorLevelPickerEnabled;

            //// ScrollGesturesEnabled
            //switchScrollGesturesEnabled.Toggled += (sender, e) =>
            //{
            //    map.UiSettings.ScrollGesturesEnabled = e.Value;
            //};
            //switchScrollGesturesEnabled.IsToggled = map.UiSettings.ScrollGesturesEnabled;

            //// TiltGesturesEnabled
            //switchTiltGesturesEnabled.Toggled += (sender, e) =>
            //{
            //    map.UiSettings.TiltGesturesEnabled = e.Value;
            //};
            //switchTiltGesturesEnabled.IsToggled = map.UiSettings.TiltGesturesEnabled;

            //// ZoomControlsEnabled
            //switchZoomControlsEnabled.Toggled += (sender, e) =>
            //{
            //    map.UiSettings.ZoomControlsEnabled = e.Value;
            //};
            //switchZoomControlsEnabled.IsToggled = map.UiSettings.ZoomControlsEnabled;

            //// ZoomGesturesEnabled
            //switchZoomGesturesEnabled.Toggled += (sender, e) =>
            //{
            //    map.UiSettings.ZoomGesturesEnabled = e.Value;
            //};
            //switchZoomGesturesEnabled.IsToggled = map.UiSettings.ZoomGesturesEnabled;

            //// Map Clicked
            //map.MapClicked += (sender, e) =>
            //{
            //    var lat = e.Point.Latitude.ToString("0.000");
            //    var lng = e.Point.Longitude.ToString("0.000");
            //    this.DisplayAlert("MapClicked", $"{lat}/{lng}", "CLOSE");
            //};

            //// Map Long clicked
            //map.MapLongClicked += (sender, e) =>
            //{
            //    var lat = e.Point.Latitude.ToString("0.000");
            //    var lng = e.Point.Longitude.ToString("0.000");
            //    this.DisplayAlert("MapLongClicked", $"{lat}/{lng}", "CLOSE");
            //};

            //// Map MyLocationButton clicked
            //map.MyLocationButtonClicked += (sender, args) =>
            //{
            //    args.Handled = switchHandleMyLocationButton.IsToggled;
            //    if (switchHandleMyLocationButton.IsToggled)
            //    {
            //        GetCurrentLocation();
            //    }

            //};

            //map.CameraChanged += (sender, args) =>
            //{
            //    var p = args.Position;
            //    labelStatus.Text = $"Lat={p.Target.Latitude:0.00}, Long={p.Target.Longitude:0.00}, Zoom={p.Zoom:0.00}, Bearing={p.Bearing:0.00}, Tilt={p.Tilt:0.00}";
            //};

            //// Geocode
            //buttonGeocode.Clicked += async (sender, e) =>
            //{
            //    var geocoder = new Xamarin.Forms.GoogleMaps.Geocoder();
            //    var positions = await geocoder.GetPositionsForAddressAsync(entryAddress.Text);
            //    if (positions.Count() > 0)
            //    {
            //        //    var pos = positions.FirstOrDefault();
            //        //    map.MoveToRegion(MapSpan.FromCenterAndRadius((new Position(), Distance.FromMeters(5000)));
            //        //    var reg = map.VisibleRegion;
            //        //    var format = "0.00";
            //        //    labelStatus.Text = $"Center = {reg.Center.Latitude.ToString(format)}, {reg.Center.Longitude.ToString(format)}";
            //    }
            //    else
            //    {
            //        await this.DisplayAlert("Not found", "Geocoder returns no results", "Close");
            //    }
            //};

            //// Snapshot
            //buttonTakeSnapshot.Clicked += async (sender, e) =>
            //{
            //    var stream = await map.TakeSnapshot();
            //    imageSnapshot.Source = ImageSource.FromStream(() => stream);
            //};
            //GetCurrentLocation();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetCurrentLocation();
        }
        private void GetCurrentLocation()
        {
            try
            {
                //var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                //var location = await Geolocation.GetLocationAsync(request);
                ////var location = await Geolocation.GetLastKnownLocationAsync();
                var latitude = 18.472384;
                var longitude = -69.921611;
                var defaultPin = new Pin { Type = PinType.Generic, Label = "This is my home", Address = "Here", Position = new Position(latitude, longitude) };
                var buspin = new Pin { Icon=BitmapDescriptorFactory.FromBundle("small_green_bus"), Label = "This is my home", Address = "Here", Position = new Position(18.472990, -69.920876) };
                map.Pins.Add(defaultPin);
                map.Pins.Add(buspin);
            }
            catch (Exception ex)
            {

            }
        }
        private void SfButton_Clicked(object sender, EventArgs e)
        {
            this.rootPage.IsPresented = !this.rootPage.IsPresented;
        }
    }
}