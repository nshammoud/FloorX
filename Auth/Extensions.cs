using KQF.Floor.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KQF.Floor.Web
{

    public enum WarehouseRecordType
    {
        Output,
        Consumption,
        All
    }

    public class KQFClaimTypes
    {
        public const string WarehouseEmployeeRecord = "KQF.WarehouseEmployeeRecord";
        public const string CredentialsKey = "KQF.CredentialsKey";
    }
    public static class Extensions
    {

        public static string UserName(this System.Security.Claims.ClaimsPrincipal user)
        {
            var claim = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.WindowsAccountName);
            return claim == null ? "" : claim.Value;
        }

        public static string DisplayName(this System.Security.Claims.ClaimsPrincipal user)
        {
            var claim = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            return claim == null ? "" : claim.Value;
        }

        public static string FirstName(this System.Security.Claims.ClaimsPrincipal user)
        {
            var claim = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName);
            return claim == null ? "" : claim.Value;
        }

        public static string LastName(this System.Security.Claims.ClaimsPrincipal user)
        {
            var claim = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Surname);
            return claim == null ? "" : claim.Value;
        }

        public static bool HasRole(this System.Security.Claims.ClaimsPrincipal user, string role, string location)
        {
            var claim = user?.Claims?.FirstOrDefault(x => x.Type.ToLower() == $"access.{location.ToLower()}" && x.Value.ToLower() == role.ToLower());
            return claim != null;
        }

        public static IEnumerable<string> GetAllRoles(this System.Security.Claims.ClaimsPrincipal user,  string location)
        {
            return user?.Claims?.Where(x=> x.Type.ToLower() == $"access.{location.ToLower()}")?.Select(x=> x.Value.ToLower())?.ToList() ?? new List<string>();

        }

        public static ADCredential GetCredentials(this System.Security.Claims.ClaimsPrincipal user)
        {
            try
            {
                var claim = user?.Claims?.FirstOrDefault(x => x.Type == KQFClaimTypes.CredentialsKey);
                return claim == null ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<ADCredential>(claim.Value);
            }
            catch
            {
                return null;
            }
        }

        public static Auth.Models.WarehouseEmployeeRecord[] GetWarehouseRecords(this ClaimsPrincipal user, WarehouseRecordType recordType)
        {
            var claims = user?.Claims?.Where(x => x.Type == KQFClaimTypes.WarehouseEmployeeRecord);
            var records = claims?.Select(x => Newtonsoft.Json.JsonConvert.DeserializeObject<Auth.Models.WarehouseEmployeeRecord>(x.Value)).ToArray() ?? new Auth.Models.WarehouseEmployeeRecord[] { };
            return records.Where(x => x.RecordType == recordType || recordType == WarehouseRecordType.All).ToArray();
        }
    }
}
