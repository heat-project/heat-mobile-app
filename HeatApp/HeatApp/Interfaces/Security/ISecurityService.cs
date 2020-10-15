using HeatApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeatApp.Interfaces
{
    public interface ISecurityService
    {
        Task<UserDTO> Register(UserDTO user);
        Task<TokenDTO> Login(UserDTO user);
    }
}