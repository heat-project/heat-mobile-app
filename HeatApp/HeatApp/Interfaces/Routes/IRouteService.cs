using HeatApp.Models.HeatModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeatApp.Interfaces.Routes
{
    public interface IRouteService
    {
        Task<List<RouteDTO>> GetFavorites();
        Task<IEnumerable<RouteDTO>> GetAll();
        Task<List<RouteDTO>> GetPreviouslySeen();
        Task<IEnumerable<StopDTO>> GetStops(int routeID = 0);
    }
}