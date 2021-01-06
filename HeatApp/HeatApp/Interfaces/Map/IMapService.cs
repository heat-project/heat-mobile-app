using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HeatApp.Interfaces.Map
{
    public interface IMapService
    {
        Task<Location> GetCurrentLocation();
    }
}
