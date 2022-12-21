using KQF.Floor.Web.Auth;
using KQF.Floor.Web.NavServices.LabelPrinter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Services
{
    public class UserSettingsService
    {
        private readonly NavServices.LabelPrinter.LabelPrinterClient _labelPrinterClient;
        private readonly NavServices.WarehouseEmployeesPage.WarehouseEmployeePageClient _warehouseEmployeesClient;
        private readonly LDAPConfig _ldapConfig;
        private readonly ILogger<UserSettingsService> _logger;
        private readonly LocationProvider _locationProvider;

        public UserSettingsService(ILogger<UserSettingsService> logger, LabelPrinterClient labelPrinterClient, LocationProvider locationProvider, 
            IOptions<LDAPConfig> ldapConfig,
            NavServices.WarehouseEmployeesPage.WarehouseEmployeePageClient warehouseEmployeesClient)
        {
            _logger = logger;
            _labelPrinterClient = labelPrinterClient;
            _locationProvider = locationProvider;
            _warehouseEmployeesClient = warehouseEmployeesClient;
            _ldapConfig = ldapConfig.Value;
        }

        public async Task<string> GetCurrentPrinter()
        {
            var locRecord = await GetWHENavRecordsForCurrentLocation();
            return locRecord?.ASN_Label_Printer ?? "";
        }

        public async Task<string[]> GetPrinters()
        {
            var results = await _labelPrinterClient.ReadMultipleAsync(new MWSASNLablePrinter_Filter[] { 
                new MWSASNLablePrinter_Filter()
                {
                    Field = MWSASNLablePrinter_Fields.Location_Code,
                    Criteria = $"={_locationProvider.CurrentLocation}"
                }
            }, "", 1000);
            return results.ReadMultiple_Result1.Select(x => $"{x.ASN_Label_Printer}").ToArray();
        }

        public async Task<bool> SetCurrentPrinter(string printerName)
        {
            var locRecord = await GetWHENavRecordsForCurrentLocation();
            if(locRecord == null)
            {
                return false;
            }
            else
            {
                locRecord.ASN_Label_Printer = printerName;
                var result = await _warehouseEmployeesClient.UpdateAsync(new NavServices.WarehouseEmployeesPage.Update()
                {
                    MWWarehouseEmployees = locRecord
                });

                return result?.MWWarehouseEmployees?.ASN_Label_Printer == printerName;
            }

            return false;
        }


        private async Task<NavServices.WarehouseEmployeesPage.MWWarehouseEmployees> GetWHENavRecordsForCurrentLocation()
        {
            var navRecords = await _warehouseEmployeesClient.ReadMultipleAsync(new NavServices.WarehouseEmployeesPage.MWWarehouseEmployees_Filter[]
            {
                new NavServices.WarehouseEmployeesPage.MWWarehouseEmployees_Filter()
                {
                    Criteria = $"={_ldapConfig.ShortDomainName}\\{_locationProvider.CurrentUserName}",
                    Field = NavServices.WarehouseEmployeesPage.MWWarehouseEmployees_Fields.User_ID
                }
            }, "", 1000);

            var locRecord = navRecords.ReadMultiple_Result1.FirstOrDefault(x => x.Location_Code.ToUpper() == _locationProvider.CurrentLocation.ToUpper());
            return locRecord;
        }
    }
}
