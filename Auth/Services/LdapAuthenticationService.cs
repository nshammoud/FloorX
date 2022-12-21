using KQF.Floor.NavServices;
using KQF.Floor.Web.Auth.Models;
using KQF.Floor.Web.NavServices.WarehouseEmployeesConsumption;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Auth.Services
{
    public class LdapAuthenticationService : IAuthenticationService
    {


        private readonly LDAPConfig config;
        private readonly ILogger<LdapAuthenticationService> _logger;
        private readonly ILogger<NavServices.WarehouseEmployees.WarehouseEmployeesClient> _whelogger;
        private readonly ILogger<WarehouseEmployeesConsumptionClient> _wheclogger;
        private readonly IOptions<NavServiceConfig> _navConfig;


        public LdapAuthenticationService(
            IOptions<LDAPConfig> config,
            IOptions<NavServiceConfig> navConfig,
            ILogger<NavServices.WarehouseEmployees.WarehouseEmployeesClient> whelogger,
            ILogger<WarehouseEmployeesConsumptionClient> wheclogger,
        ILogger<LdapAuthenticationService> logger)
        {
            this.config = config.Value;
            _logger = logger;
            _navConfig = navConfig;
            _whelogger = whelogger;
            _wheclogger = wheclogger;

        }
        public async Task<User> DevLogin(string userName, string password)
        {
            if (this.config.DevAuthBypass ?? false)
            {

                var creds = new ADCredential()
                {
                    //Storing AD version as casing does not matter in the search above,
                    //but does appear to matter to the authentication checks
                    Username =  userName,
                    Domain = config.ShortDomainName,
                    Password = password

                };
                _logger.LogInformation($"Calling nav svcs");
                //create a temporary claims principal
                var tempUser = new System.Security.Claims.ClaimsPrincipal(
                    new System.Security.Claims.ClaimsIdentity(new List<System.Security.Claims.Claim>{
                                    new System.Security.Claims.Claim(KQFClaimTypes.CredentialsKey, Newtonsoft.Json.JsonConvert.SerializeObject(creds))
                    }));


                var whClient = new NavServices.WarehouseEmployees.WarehouseEmployeesClient(_navConfig, tempUser, _whelogger); //cant inject this need credentials first

                var navRecords = await whClient.ReadMultipleAsync(new NavServices.WarehouseEmployees.MWSWhseEmployeeItemCatList_Filter[]
                    {
                                    new NavServices.WarehouseEmployees.MWSWhseEmployeeItemCatList_Filter()
                                    {
                                        Criteria = $"={config.ShortDomainName}\\{creds.Username}",
                                        Field = NavServices.WarehouseEmployees.MWSWhseEmployeeItemCatList_Fields.User_ID
                                    }
                    }, "", 1000);

                var whEmployeesList = new List<WarehouseEmployeeRecord>();
                foreach (var r in navRecords.ReadMultiple_Result1)
                {
                    whEmployeesList.Add(new WarehouseEmployeeRecord()
                    {
                        Location = r.Location_Code,
                        ItemCategory = r.Item_Category,
                        OutputScreenVersion = r.Output_Screen_Ver.ToString(),
                        RecordType = WarehouseRecordType.Output
                    });
                }

                var whConsumptionClient = new NavServices.WarehouseEmployeesConsumption.WarehouseEmployeesConsumptionClient(_navConfig, tempUser, _wheclogger); //cant inject this need credentials first

                var consumptionRecords = await whConsumptionClient.ReadMultipleAsync(new NavServices.WarehouseEmployeesConsumption.MWSWhseEmployeeConsItemCatList_Filter[]
                    {
                                    new NavServices.WarehouseEmployeesConsumption.MWSWhseEmployeeConsItemCatList_Filter()
                                    {
                                        Criteria = $"={config.ShortDomainName}\\{creds.Username}",
                                        Field = NavServices.WarehouseEmployeesConsumption.MWSWhseEmployeeConsItemCatList_Fields.User_ID
                                    }
                    }, "", 1000);

                foreach (var r in consumptionRecords.ReadMultiple_Result1)
                {
                    whEmployeesList.Add(new WarehouseEmployeeRecord()
                    {
                        Location = r.Location_Code,
                        ItemCategory = r.Item_Category,
                        OutputScreenVersion = string.Empty,
                        RecordType = WarehouseRecordType.Consumption
                    });
                }

                _logger.LogInformation($"Login successeful");
                return new User
                {
                    DisplayName = "DEV" + userName,
                    UserName = userName,
                    FirstName = "DEV" + userName,
                    LastName = "DEV" + userName,
                    LocationAccess = new List<LocationRole>()
                    {
                        new LocationRole(){ Location = "Quincy", Role="Administrator"},
                        new LocationRole(){ Location = "Leonard", Role="Administrator"}
                    },
                    WarehouseEmployeeRecords = whEmployeesList,
                    Credentials = creds
                };
            }
            return null;
        }

        public async Task<User> Login(string userName, string password)
        {
            if (this.config.DevAuthBypass ?? false)
            {
                return await DevLogin(userName, password);
            }

                _logger.LogInformation("Logging in user " + userName);
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(config.Server + config.AccountPath, userName + config.UserDomainName, password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = String.Format("({0}={1})", config.AccountNameProp, userName);
                        searcher.PropertiesToLoad.Add(config.DisplayNameProp);
                        searcher.PropertiesToLoad.Add(config.AccountNameProp);
                        searcher.PropertiesToLoad.Add(config.GroupListProp);
                        searcher.PropertiesToLoad.Add(config.FirstNameProp);
                        searcher.PropertiesToLoad.Add(config.LastNameProp);


                        var result = searcher.FindOne();
                        _logger.LogInformation($"LDAP result for {userName} was " + (result == null ? " false " : " true"));
                        if (result != null)
                        {

                            var displayName = result.Properties[config.DisplayNameProp];
                            var samAccountName = result.Properties[config.AccountNameProp];
                            var firstName = result.Properties[config.FirstNameProp];
                            var lastName = result.Properties[config.LastNameProp];
                            var groups = result.Properties[config.GroupListProp];

                            var groupList = new List<LocationRole>();
                            foreach (var g in groups)
                            {
                                var str = g.ToString().Split(",", StringSplitOptions.RemoveEmptyEntries)
                                    //TODO Filter on OU=Groups maybe?
                                    .Where(x => x.StartsWith("CN="))
                                    .Select(x => x.Replace("CN=", ""))
                                    .FirstOrDefault()
                                    .ToLower();

                                var roles = config.Roles.Where(x => x.ADGroup.ToLower() == str).ToList();
                                
                                if (roles != null)
                                {
                                   roles.ForEach(role =>
                                   {
                                       groupList.Add(new LocationRole() { Location = role.Location, Role = role.Name });
                                   });                                    
                                }
                            }

                            var creds = new ADCredential()
                            {
                                //Storing AD version as casing does not matter in the search above,
                                //but does appear to matter to the authentication checks
                                Username = samAccountName.Count == 1 ? samAccountName[0].ToString() : userName,
                                Domain = config.ShortDomainName,
                                Password = password

                            };
                            _logger.LogInformation($"Calling nav svcs");
                            //create a temporary claims principal
                            var tempUser = new System.Security.Claims.ClaimsPrincipal(
                                new System.Security.Claims.ClaimsIdentity(new List<System.Security.Claims.Claim>{
                                    new System.Security.Claims.Claim(KQFClaimTypes.CredentialsKey, Newtonsoft.Json.JsonConvert.SerializeObject(creds))
                                }));


                            var whClient = new NavServices.WarehouseEmployees.WarehouseEmployeesClient(_navConfig, tempUser, _whelogger); //cant inject this need credentials first

                            var navRecords = await whClient.ReadMultipleAsync(new NavServices.WarehouseEmployees.MWSWhseEmployeeItemCatList_Filter[]
                                {
                                    new NavServices.WarehouseEmployees.MWSWhseEmployeeItemCatList_Filter()
                                    {
                                        Criteria = $"={config.ShortDomainName}\\{creds.Username}",
                                        Field = NavServices.WarehouseEmployees.MWSWhseEmployeeItemCatList_Fields.User_ID
                                    }
                                }, "", 1000);

                            var whEmployeesList = new List<WarehouseEmployeeRecord>();
                            foreach (var r in navRecords.ReadMultiple_Result1)
                            {
                                whEmployeesList.Add(new WarehouseEmployeeRecord()
                                {
                                    Location = r.Location_Code,
                                    ItemCategory = r.Item_Category,
                                    OutputScreenVersion = r.Output_Screen_Ver.ToString(),
                                    RecordType = WarehouseRecordType.Output
                                });
                            }

                            var whConsumptionClient = new NavServices.WarehouseEmployeesConsumption.WarehouseEmployeesConsumptionClient(_navConfig, tempUser, _wheclogger); //cant inject this need credentials first

                            var consumptionRecords = await whConsumptionClient.ReadMultipleAsync(new NavServices.WarehouseEmployeesConsumption.MWSWhseEmployeeConsItemCatList_Filter[]
                                {
                                    new NavServices.WarehouseEmployeesConsumption.MWSWhseEmployeeConsItemCatList_Filter()
                                    {
                                        Criteria = $"={config.ShortDomainName}\\{creds.Username}",
                                        Field = NavServices.WarehouseEmployeesConsumption.MWSWhseEmployeeConsItemCatList_Fields.User_ID
                                    }
                                }, "", 1000);

                            foreach (var r in consumptionRecords.ReadMultiple_Result1)
                            {
                                whEmployeesList.Add(new WarehouseEmployeeRecord()
                                {
                                    Location = r.Location_Code,
                                    ItemCategory = r.Item_Category,
                                    OutputScreenVersion = string.Empty,
                                    RecordType = WarehouseRecordType.Consumption
                                });
                            }

                            _logger.LogInformation($"Login successeful");
                            return new User
                            {
                                DisplayName = displayName == null || displayName.Count <= 0 ? null : displayName[0].ToString(),
                                UserName = samAccountName == null || samAccountName.Count <= 0 ? null : samAccountName[0].ToString(),
                                FirstName = firstName == null || firstName.Count <= 0 ? null : firstName[0].ToString(),
                                LastName = lastName == null || lastName.Count <= 0 ? null : lastName[0].ToString(),
                                LocationAccess = groupList,
                                WarehouseEmployeeRecords = whEmployeesList,
                                Credentials = creds
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // if we get an error, it means we have a login failure.
                // Log specific exception
                _logger.LogError(ex, $"Login Failed for user {userName} {ex.Message}");
                throw ex;
            }
            return null;
        }
    }
}
