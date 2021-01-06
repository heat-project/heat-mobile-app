using System;
using System.Threading;
using System.Threading.Tasks;
using HeatApp.Interfaces.Map;
using Xamarin.Essentials;

namespace HeatApp.Services
{
    public class MapService : IMapService
    {

        CancellationTokenSource cts;

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
    }
}
