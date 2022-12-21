using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Auth.ClaimsHandling
{
    public class LocationClaimsHandler : AuthorizationHandler<LocationClaimRoleRequirement>
    {
        private readonly LocationProvider _location;

        public LocationClaimsHandler(LocationProvider location) { _location = location; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LocationClaimRoleRequirement requirement)
        {
            var currentLocation = _location.CurrentLocation;
            if (string.IsNullOrEmpty(currentLocation))
            {
                context.Fail();
                return Task.CompletedTask;
            }


            if (context.User.HasClaim(c => c.Type.ToLower() == $"access.{currentLocation.ToLower()}" &&
                                      c.Value.ToLower() == requirement.Role.ToLower()))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
