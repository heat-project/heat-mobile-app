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
        Task<List<RouteDTO>> GetAll();
        Task<List<RouteDTO>> GetPreviouslySeen();
        Task<List<StopDTO>> GetStops(int routeID);
    }
}