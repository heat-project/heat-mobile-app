using HeatApp.Interfaces.Routes;
using HeatApp.Models.HeatModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeatApp.Services
{
    public class RouteService : BaseService, IRouteService
    {
        #region Methods
        public async Task<List<RouteDTO>> GetAll()
        {
            return new List<RouteDTO>
            {
                new RouteDTO
                {
                    Code = "C12B",
                    From = "Av.Los Próceres",
                    To = "Av. Correa y Cidrón"
                },new RouteDTO
                {
                    Code = "C11C10",
                    From = "Av. W. Churchill",
                    To = "Av. Abraham Lincoln"
                },
                new RouteDTO
                {
                    Code = "C11S",
                    From = "Av. Correa y Cidrón",
                    To = "Av. Independencia"
                }
            };
        }
        public async Task<List<RouteDTO>> GetFavorites()
        {
            return new List<RouteDTO>
            {
                new RouteDTO
                {
                    Code = "C12B",
                    From = "Av.Los Próceres",
                    To = "Av. Correa y Cidrón"
                }
            };
        }
        public async Task<List<RouteDTO>> GetPreviouslySeen()
        {
            return new List<RouteDTO>
            {
                new RouteDTO
                {
                    Code = "C12B",
                    From = "Av.Los Próceres",
                    To = "Av. Correa y Cidrón"
                },
                new RouteDTO
                {
                    Code = "C11S",
                    From = "Av. Correa y Cidrón",
                    To = "Av. Independencia"
                }
            };
        }
        public async Task<List<StopDTO>> GetStops(int routeID)
        {
            return new List<StopDTO>
            {
                new StopDTO
                {
                    ID = 1,
                    Title = $"Parada Prueba {1}",
                    Latitude = 18.482580,
                    Longitude = -69.922857
                },
                new StopDTO
                {
                    ID = 2,
                    Title = $"Parada Prueba {2}",
                    Latitude = 18.482702,
                    Longitude = -69.925316
                },
                new StopDTO
                {
                    ID = 3,
                    Title = $"Parada Prueba {3}",
                    Latitude = 18.482875,
                    Longitude = -69.927645
                },
                new StopDTO
                {
                    ID = 4,
                    Title = $"Parada Prueba {4}",
                    Latitude = 18.482977,
                    Longitude = -69.929750
                },
                new StopDTO
                {
                    ID = 5,
                    Title = $"Parada Prueba {5}",
                    Latitude = 18.482356,
                    Longitude = -69.931285
                },
                new StopDTO
                {
                    ID = 6,
                    Title = $"Parada Prueba {6}",
                    Latitude = 18.481288,
                    Longitude = -69.931317
                }
            };
        }
        #endregion
    }
}