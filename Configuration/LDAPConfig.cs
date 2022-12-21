using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Auth
{
    public class LDAPConfig
    {
        public string Server { get; set; }

        public string AccountPath { get; set; }

        public string UserDomainName { get; set; }

        public string ShortDomainName { get; set; }

        public string AccountNameProp { get; set; }

        public string DisplayNameProp { get; set; }

        public string GroupListProp { get; set; }

        public string FirstNameProp { get; set; }

        public string LastNameProp { get; set; }

        public RoleConfig[] Roles { get; set; }

        public bool? DevAuthBypass { get; set; }

    }

    public class RoleConfig
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string ADGroup { get; set; }
    }
}
