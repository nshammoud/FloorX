using Newtonsoft.Json;

namespace KQF.Floor.Web.Models.BusinessCentralApi_Models
{
    public class Location
    {
        [JsonProperty(PropertyName = "@odata.etag")]
        public string OdataeTag { get; set; }

        [JsonProperty(PropertyName = "systemId")]
        public string SystemId { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "useAsInTransit")]
        public bool UseAsInTransit { get; set; }

        [JsonProperty(PropertyName = "containerPutAway")]
        public bool ContainerPutAway { get; set; }

        [JsonProperty(PropertyName = "containerDestReservPeriod")]
        public int ContainerDestReservPeriod { get; set; }

        [JsonProperty(PropertyName = "containerDestReserving")]
        public bool ContainerDestReserving { get; set; }

        [JsonProperty(PropertyName = "printSSCCwhileRegistPick")]
        public bool PrintSSCCwhileRegistPick { get; set; }

        [JsonProperty(PropertyName = "enableContainerCART")]
        public bool EnableContainerCART { get; set; }

        [JsonProperty(PropertyName = "endOfDayOffset")]
        public string EndOfDayOffset { get; set; }
    }
}
