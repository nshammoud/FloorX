using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Auth.ClaimsHandling
{
    public class LocationClaimRoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; set; }

        public LocationClaimRoleRequirement(string role)
        {
            Role = role;
        }
    }
}
