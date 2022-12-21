using KQF.Floor.NavServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.NavServices.Components
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class ComponentsClient : NavServices.Clients.NavClientBase<MWSProdComponentList_Port, ComponentsClient>, MWSProdComponentList_Port
    {

        public ComponentsClient(Microsoft.Extensions.Options.IOptions<NavServiceConfig> config, System.Security.Claims.ClaimsPrincipal user, ILogger<ComponentsClient> logger) :
             base(config, "MWSProdComponentList_Port", user, logger)
        {

        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Read_Result> MWSProdComponentList_Port.ReadAsync(Read request)
        {
            return base.Channel.ReadAsync(request);
        }

        public System.Threading.Tasks.Task<Read_Result> ReadAsync(string Prod_Order_No)
        {
            Read inValue = new Read();
            inValue.Prod_Order_No = Prod_Order_No;
            return ((MWSProdComponentList_Port)(this)).ReadAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ReadByRecId_Result> MWSProdComponentList_Port.ReadByRecIdAsync(ReadByRecId request)
        {
            return base.Channel.ReadByRecIdAsync(request);
        }

        public System.Threading.Tasks.Task<ReadByRecId_Result> ReadByRecIdAsync(string recId)
        {
            ReadByRecId inValue = new ReadByRecId();
            inValue.recId = recId;
            return ((MWSProdComponentList_Port)(this)).ReadByRecIdAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ReadMultiple_Result> MWSProdComponentList_Port.ReadMultipleAsync(ReadMultiple request)
        {
            return base.Channel.ReadMultipleAsync(request);
        }

        public System.Threading.Tasks.Task<ReadMultiple_Result> ReadMultipleAsync(MWSProdComponentList_Filter[] filter, string bookmarkKey, int setSize)
        {
            ReadMultiple inValue = new ReadMultiple();
            inValue.filter = filter;
            inValue.bookmarkKey = bookmarkKey;
            inValue.setSize = setSize;
            return ((MWSProdComponentList_Port)(this)).ReadMultipleAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IsUpdated_Result> MWSProdComponentList_Port.IsUpdatedAsync(IsUpdated request)
        {
            return base.Channel.IsUpdatedAsync(request);
        }

        public System.Threading.Tasks.Task<IsUpdated_Result> IsUpdatedAsync(string Key)
        {
            IsUpdated inValue = new IsUpdated();
            inValue.Key = Key;
            return ((MWSProdComponentList_Port)(this)).IsUpdatedAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<GetRecIdFromKey_Result> MWSProdComponentList_Port.GetRecIdFromKeyAsync(GetRecIdFromKey request)
        {
            return base.Channel.GetRecIdFromKeyAsync(request);
        }

        public System.Threading.Tasks.Task<GetRecIdFromKey_Result> GetRecIdFromKeyAsync(string Key)
        {
            GetRecIdFromKey inValue = new GetRecIdFromKey();
            inValue.Key = Key;
            return ((MWSProdComponentList_Port)(this)).GetRecIdFromKeyAsync(inValue);
        }

        public System.Threading.Tasks.Task<Create_Result> CreateAsync(Create request)
        {
            return base.Channel.CreateAsync(request);
        }

        public System.Threading.Tasks.Task<CreateMultiple_Result> CreateMultipleAsync(CreateMultiple request)
        {
            return base.Channel.CreateMultipleAsync(request);
        }

        public System.Threading.Tasks.Task<Update_Result> UpdateAsync(Update request)
        {
            return base.Channel.UpdateAsync(request);
        }

        public System.Threading.Tasks.Task<UpdateMultiple_Result> UpdateMultipleAsync(UpdateMultiple request)
        {
            return base.Channel.UpdateMultipleAsync(request);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Delete_Result> MWSProdComponentList_Port.DeleteAsync(Delete request)
        {
            return base.Channel.DeleteAsync(request);
        }

        public System.Threading.Tasks.Task<Delete_Result> DeleteAsync(string Key)
        {
            Delete inValue = new Delete();
            inValue.Key = Key;
            return ((MWSProdComponentList_Port)(this)).DeleteAsync(inValue);
        }
    }
}
