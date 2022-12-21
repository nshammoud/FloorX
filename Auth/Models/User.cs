using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Auth.Models
{
    public class User
    {
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        //Key = location code, Value = role
        public List<LocationRole> LocationAccess { get; set; }

        public List<WarehouseEmployeeRecord> WarehouseEmployeeRecords { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public ADCredential Credentials { get; set; }

    }

    public class LocationRole
    {
        public string Location { get; set; }

        public string Role { get; set; }

        public override string ToString()
        {
            return $"{Location}.{Role}";
        }
    }

    public class WarehouseEmployeeRecord
    {
        public string Location { get; set; }

        public string ItemCategory { get; set; }

        public string OutputScreenVersion { get; set; }

        public WarehouseRecordType RecordType { get; set; }
    }
}
