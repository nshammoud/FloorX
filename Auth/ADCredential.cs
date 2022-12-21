using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Auth
{
    public class ADCredential
    {

        public string Domain { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }


    }

    //public class ADCredentialRepository
    //{
    //    private readonly List<ADCredentialProvider> _adCredentials;

    //    public ADCredentialRepository()
    //    {
    //        _adCredentials = new List<ADCredentialProvider>();
    //    }

    //    public ADCredentialProvider Get(Guid key)
    //    {
    //        var existing = _adCredentials.FirstOrDefault(x => x.Key == key);
    //        return existing;
    //    }

    //    public ADCredentialProvider Get(string username, string domain)
    //    {
    //        var existing = _adCredentials.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Domain.ToLower() == domain.ToLower());
    //        return existing;
    //    }

    //    public void Logout(Guid key)
    //    {
    //        var existing = Get(key);
    //        if (existing != null)
    //        {
    //            _adCredentials.Remove(existing);
    //            existing.Password = "";
    //        }
    //    }

    //    public ADCredentialProvider Login(string username, string domain, string password)
    //    {
    //        var existing = _adCredentials.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Domain.ToLower() == domain.ToLower());
    //        if (existing != null)
    //        {
    //            existing.Password = password;
    //            return existing;
    //        }
    //        else
    //        {
    //            var creds = new ADCredentialProvider()
    //            {
    //                Key = new Guid(),
    //                Username = username,
    //                Password = password,
    //                Domain = domain
    //            };
    //            _adCredentials.Add(creds);
    //            return creds;
    //        }
    //    }

    //}


}
