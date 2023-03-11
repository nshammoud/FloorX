using KQF.Floor.Web.Controllers;
using KQF.Floor.Web.Models.BusinessCentralApi_Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Auth
{
    public class LocationProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly NavServices.LocationList.LocationListClient _locationListClient;
        private readonly List<KQF.Floor.Web.Models.BusinessCentralApi_Models.Location> locationListClient_;
        public readonly HttpContext _httpContext;

        //<< NSH 
        //public LocationProvider(IHttpContextAccessor contextAccessor, NavServices.LocationList.LocationListClient locationListClient)
        //{
        //    _contextAccessor = contextAccessor;
        //    _locationListClient = locationListClient;
        //}
        // NSH >>

        //<< NSH
        public LocationProvider(IHttpContextAccessor contextAccessor, Web.Models.BusinessCentralApi_Models.Location locationListClientX, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = contextAccessor;
            _httpContext = httpContextAccessor.HttpContext;
            var test = _httpContext.Session.GetString("LocationsList_" + "a2db7307-078b-ed11-aad8-000d3a21edc2");
            if(test != null)
            {
                var loc = JsonConvert.DeserializeObject<GenericResponse<Location>>(test);
            }
        }
        // NSH >>

        public void ClearCache()
        {
            var key = "seconds-day-offset-" + CurrentLocation;
            _contextAccessor.HttpContext.Session.Remove(key);
        }

        public string[] Locations
        {
            get
            {
                var test = _httpContext.Session.GetString("LocationsList_" + "a2db7307-078b-ed11-aad8-000d3a21edc2");
                if (test != null)
                {
                    var loc = JsonConvert.DeserializeObject<GenericResponse<Location>>(test);
                }
                var locations = _contextAccessor.HttpContext.Session.GetString("Locations");
                if (locations == null || locations == string.Empty)
                {
                    _contextAccessor.HttpContext.Session.SetString("Locations", "[]");
                    var userLocations = _contextAccessor.HttpContext.User.Claims.Where(x => x.Type == "location").Select(x => x.Value).Distinct().ToArray();
                    if (userLocations.Length > 0)
                    {
                        locations = Newtonsoft.Json.JsonConvert.SerializeObject(userLocations);
                        _contextAccessor.HttpContext.Session.SetString("Locations", locations);
                    }
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(locations ?? "[]");
            }
            set
            {
                var locations = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                _contextAccessor.HttpContext.Session.SetString("Locations", locations);
            }
        }

        public string CurrentLocation
        {
            get
            {
                var location = _contextAccessor.HttpContext.Session.GetString("CurrentLocation");
                if (location == null || location == string.Empty)
                {
                    var userLocations = _contextAccessor.HttpContext.User.Claims.Where(x => x.Type == "location").Select(x => x.Value).ToArray();
                    if (userLocations.Length > 0)
                    {
                        location = userLocations.First();
                        _contextAccessor.HttpContext.Session.SetString("CurrentLocation", location);
                    }
                }
                return location;
            }
            set
            {
                var userLocation = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "location" && x.Value.ToLower() == value.ToLower());
                if (userLocation != default)
                {
                    _contextAccessor.HttpContext.Session.SetString("CurrentLocation", userLocation.Value);
                }

            }
        }

        public string CurrentUserName
        {
            get
            {
               return _contextAccessor.HttpContext.User.UserName();
            }
        }

        public int LocationDayOffsetInSeconds { get
            {
                var key = "seconds-day-offset-" + CurrentLocation;
                var val = _contextAccessor.HttpContext.Session.GetInt32(key);

                if (!val.HasValue)
                {

                    var results = _locationListClient.ReadAsync(CurrentLocation).GetAwaiter().GetResult();

                    if (results?.MWSLocationList != null)
                    {
                        
                        var split = results.MWSLocationList.End_of_Day_Offset.Split("-PYMDTHMS".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (split.Length > 5)
                        {
                            //Int32.TryParse(split[0], out var year);
                            //Int32.TryParse(split[1], out var month);
                            //Int32.TryParse(split[2], out var day);
                            Int32.TryParse(split[3], out var hour);
                            Int32.TryParse(split[4], out var minute);
                            Int32.TryParse(split[5], out var second);

                            //only looking at the time, ignore date
                            //convert to seconds, then back to hours
                            int totalSeconds = second + (minute * 60) + (hour * 60) * 60;
                            _contextAccessor.HttpContext.Session.SetInt32(key, totalSeconds);
                            return totalSeconds;
                        }
                    }
                }

                return val ?? 0;
            } }
    }
}
