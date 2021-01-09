using System;
using System.Collections.Generic;
using System.Text;
using HeatApp.Models;
using System;
using System.Threading.Tasks;
using HeatApp.Helpers;

namespace HeatApp.Interfaces
{
    public interface IBusService
    {
        Task<IEnumerable<BusDTO>> GetAll();
        Task<BusDTO> SetPosition();
        Task<BusInfoDTO> GetByID(int id);
    }
}