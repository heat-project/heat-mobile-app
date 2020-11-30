using System;
using System.Collections.Generic;
using System.Text;
using HeatApp.Models;
using System;
using System.Threading.Tasks;

namespace HeatApp.Interfaces
{
    public interface IBusService
    {
        Task<List<BusDTO>> GetAll();
    }
}