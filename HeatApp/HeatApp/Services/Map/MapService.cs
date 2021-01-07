using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HeatApp.Interfaces.Map;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace HeatApp.Services
{
    public class MapService : IMapService
    {

        CancellationTokenSource cts;

        public Xamarin.Forms.GoogleMaps.Map Map { get; set; }

        public async Task<Location> GetCurrentLocation()
        {
            var location = new Location();
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                location = await Geolocation.GetLocationAsync(request, cts.Token);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            return location;
        }

        public void DrawRoute(Polyline polyline)
        {
            Map.Polylines.Clear();
            polyline.StrokeColor = Color.FromHex("#F4A32C");
            polyline.StrokeWidth = 3;

            Map.Polylines.Add(polyline);
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Distance.FromMiles(0.34f)));
        }
    }
}