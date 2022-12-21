using KQF.Floor.Web.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Auth.Services
{
    public interface IAuthenticationService
    {
        Task<User> Login(string userName, string password);
    }
}
