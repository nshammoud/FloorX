using KQF.Floor.Web.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;

namespace KQF.Floor.Web.Configuration
{
    public class FeatureFlagConfiguration
    {        public FeatureFlag[] Flags { get; set; }
    }

    public class FeatureFlag
    {
        public string Name { get; set; } = "";
        public string Location { get; set; } = string.Empty;
        public bool Enabled { get; set; } = false;
        public string[] PreviewRoles { get; set; }
    }

    public class FeatureFlagProvider
    {
        private readonly LocationProvider _locationProvider;
        private readonly FeatureFlagConfiguration _config;
        private readonly ILogger _logger;

        public FeatureFlagProvider(LocationProvider locationProvider, IOptions<FeatureFlagConfiguration> config, ILogger<FeatureFlagProvider> logger)
        {
            _config = config.Value;
            _locationProvider = locationProvider;
            _logger = logger;
        }

        public bool FlagEnabled(string flagName, System.Security.Claims.ClaimsPrincipal user)
        {
            var flag = _config.Flags.FirstOrDefault(x => x.Name.ToLower() == flagName.ToLower() && x.Location.ToLower() == _locationProvider.CurrentLocation.ToLower());
            if (flag == null)
                return false;

            if (flag.Enabled)
                return true;

            var hasRole = flag.PreviewRoles.Any(x => user.HasRole(x, flag.Location));
            return hasRole;
        }

        public string[] GetAllEnabledFlags(System.Security.Claims.ClaimsPrincipal user)
        {
            var allRoles = user.GetAllRoles(_locationProvider.CurrentLocation);
            var allFlags = _config.Flags
                .Where(x => x.Location.ToLower() == (_locationProvider.CurrentLocation?.ToLower() ?? ""))
                .Where(x => x.Enabled || allRoles.Intersect(x.PreviewRoles.Select(r => r.ToLower())).Any())
                .Select(x => x.Name.ToLower())
                .ToArray();
            return allFlags;
        }
    }
}
