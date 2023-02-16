using Newtonsoft.Json;

namespace KQF.Floor.Web.Models.BusinessCentralApi_Models
{
    public class GenericResponse<T> where T :  class
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string Odatacontext { get; set; }

        [JsonProperty(PropertyName = "value")]
        public T[] ReturnList { get; set; }
    }


    public class GenericResponse2<T> where T : class
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string Odatacontext { get; set; }

        [JsonProperty(PropertyName = "value")]
        public T ReturnList { get; set; }
    }
}
