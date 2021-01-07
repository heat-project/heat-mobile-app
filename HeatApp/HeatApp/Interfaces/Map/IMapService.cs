using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace HeatApp.Interfaces.Map
{
    public interface IMapService
    {
        Xamarin.Forms.GoogleMaps.Map Map { get; set; }

        Task<Location> GetCurrentLocation();

        void DrawRoute(Polyline polyline);
    }
}
