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
        #endregion
    }
}