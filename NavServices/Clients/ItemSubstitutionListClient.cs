﻿using KQF.Floor.NavServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.NavServices.ItemSubstitution
{
    public class ItemSubstitutionListClient : NavServices.Clients.NavClientBase<MWSSubstitudeItemList_Port, ItemSubstitutionListClient>, MWSSubstitudeItemList_Port
    {
        public ItemSubstitutionListClient(Microsoft.Extensions.Options.IOptions<NavServiceConfig> config, System.Security.Claims.ClaimsPrincipal user, ILogger<ItemSubstitutionListClient> logger) :
         base(config, "MWSSubstitudeItemList_Port", user, logger)
        {

        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Read_Result> MWSSubstitudeItemList_Port.ReadAsync(Read request)
        {
            return base.Channel.ReadAsync(request);
        }

        public System.Threading.Tasks.Task<Read_Result> ReadAsync(string No, string Substitute_No)
        {
            Read inValue = new Read();
            inValue.No = No;
            inValue.Substitute_No = Substitute_No;
            return ((MWSSubstitudeItemList_Port)(this)).ReadAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ReadByRecId_Result> MWSSubstitudeItemList_Port.ReadByRecIdAsync(ReadByRecId request)
        {
            return base.Channel.ReadByRecIdAsync(request);
        }

        public System.Threading.Tasks.Task<ReadByRecId_Result> ReadByRecIdAsync(string recId)
        {
            ReadByRecId inValue = new ReadByRecId();
            inValue.recId = recId;
            return ((MWSSubstitudeItemList_Port)(this)).ReadByRecIdAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ReadMultiple_Result> MWSSubstitudeItemList_Port.ReadMultipleAsync(ReadMultiple request)
        {
            return base.Channel.ReadMultipleAsync(request);
        }

        public System.Threading.Tasks.Task<ReadMultiple_Result> ReadMultipleAsync(MWSSubstitudeItemList_Filter[] filter, string bookmarkKey, int setSize)
        {
            ReadMultiple inValue = new ReadMultiple();
            inValue.filter = filter;
            inValue.bookmarkKey = bookmarkKey;
            inValue.setSize = setSize;
            return ((MWSSubstitudeItemList_Port)(this)).ReadMultipleAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IsUpdated_Result> MWSSubstitudeItemList_Port.IsUpdatedAsync(IsUpdated request)
        {
            return base.Channel.IsUpdatedAsync(request);
        }

        public System.Threading.Tasks.Task<IsUpdated_Result> IsUpdatedAsync(string Key)
        {
            IsUpdated inValue = new IsUpdated();
            inValue.Key = Key;
            return ((MWSSubstitudeItemList_Port)(this)).IsUpdatedAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<GetRecIdFromKey_Result> MWSSubstitudeItemList_Port.GetRecIdFromKeyAsync(GetRecIdFromKey request)
        {
            return base.Channel.GetRecIdFromKeyAsync(request);
        }

        public System.Threading.Tasks.Task<GetRecIdFromKey_Result> GetRecIdFromKeyAsync(string Key)
        {
            GetRecIdFromKey inValue = new GetRecIdFromKey();
            inValue.Key = Key;
            return ((MWSSubstitudeItemList_Port)(this)).GetRecIdFromKeyAsync(inValue);
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
        System.Threading.Tasks.Task<Delete_Result> MWSSubstitudeItemList_Port.DeleteAsync(Delete request)
        {
            return base.Channel.DeleteAsync(request);
        }

        public System.Threading.Tasks.Task<Delete_Result> DeleteAsync(string Key)
        {
            Delete inValue = new Delete();
            inValue.Key = Key;
            return ((MWSSubstitudeItemList_Port)(this)).DeleteAsync(inValue);
        }

    }
}
