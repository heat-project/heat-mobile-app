using System;
using System.Collections.Generic;
using System.Text;
using HeatApp.Interfaces;
using HeatApp.Models;
using System.Threading.Tasks;

namespace HeatApp.Services
{
    public class BusService : IBusService
    {
        public async Task<List<BusDTO>> GetAll()
        {
            return new List<BusDTO>
            {
                new BusDTO
                {
                    ID = 1,
                    Description = $"Autobus Prueba {1}",
                    Latitude = (long)18.482519,
                    Longitude = (long)-69.924915
                },
                new BusDTO
                {
                    ID = 2,
                    Description = $"Autobus Prueba {2}",
                    Latitude = (long)18.482662,
                    Longitude = (long)-69.927709
                },
                new BusDTO
                {
                    ID = 3,
                    Description = $"Autobus Prueba {3}",
                    Latitude = (long)18.480525,
                    Longitude = (long)-69.928461
                },
                new BusDTO
                {
                    ID = 4,
                    Description = $"Autobus Prueba {4}",
                    Latitude = (long)18.475702,
                    Longitude = (long)-69.923736
                }
            };
        }
    }
}