﻿using KQF.Floor.NavServices;
using KQF.Floor.Web.NavServices.Clients;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.NavServices.WarehouseEmployees
{
    public class WarehouseEmployeesClient : NavClientBase<MWSWhseEmployeeItemCatList_Port, WarehouseEmployeesClient>, MWSWhseEmployeeItemCatList_Port
    {

        public WarehouseEmployeesClient(Microsoft.Extensions.Options.IOptions<NavServiceConfig> config,
            System.Security.Claims.ClaimsPrincipal user, ILogger<WarehouseEmployeesClient> logger) :
                base(config, "MWSWhseEmployeeItemCatList_Port", user, logger)
        {

        }


        /* Begin generated code */


        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Read_Result> MWSWhseEmployeeItemCatList_Port.ReadAsync(Read request)
        {
            return base.Channel.ReadAsync(request);
        }

        public System.Threading.Tasks.Task<Read_Result> ReadAsync(string User_ID, string Location_Code, string Item_Category)
        {
            Read inValue = new Read();
            inValue.User_ID = User_ID;
            inValue.Location_Code = Location_Code;
            inValue.Item_Category = Item_Category;
            return ((MWSWhseEmployeeItemCatList_Port)(this)).ReadAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ReadByRecId_Result> MWSWhseEmployeeItemCatList_Port.ReadByRecIdAsync(ReadByRecId request)
        {
            return base.Channel.ReadByRecIdAsync(request);
        }

        public System.Threading.Tasks.Task<ReadByRecId_Result> ReadByRecIdAsync(string recId)
        {
            ReadByRecId inValue = new ReadByRecId();
            inValue.recId = recId;
            return ((MWSWhseEmployeeItemCatList_Port)(this)).ReadByRecIdAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ReadMultiple_Result> MWSWhseEmployeeItemCatList_Port.ReadMultipleAsync(ReadMultiple request)
        {
            return base.Channel.ReadMultipleAsync(request);
        }

        public System.Threading.Tasks.Task<ReadMultiple_Result> ReadMultipleAsync(MWSWhseEmployeeItemCatList_Filter[] filter, string bookmarkKey, int setSize)
        {
            ReadMultiple inValue = new ReadMultiple();
            inValue.filter = filter;
            inValue.bookmarkKey = bookmarkKey;
            inValue.setSize = setSize;
            return ((MWSWhseEmployeeItemCatList_Port)(this)).ReadMultipleAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IsUpdated_Result> MWSWhseEmployeeItemCatList_Port.IsUpdatedAsync(IsUpdated request)
        {
            return base.Channel.IsUpdatedAsync(request);
        }

        public System.Threading.Tasks.Task<IsUpdated_Result> IsUpdatedAsync(string Key)
        {
            IsUpdated inValue = new IsUpdated();
            inValue.Key = Key;
            return ((MWSWhseEmployeeItemCatList_Port)(this)).IsUpdatedAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<GetRecIdFromKey_Result> MWSWhseEmployeeItemCatList_Port.GetRecIdFromKeyAsync(GetRecIdFromKey request)
        {
            return base.Channel.GetRecIdFromKeyAsync(request);
        }

        public System.Threading.Tasks.Task<GetRecIdFromKey_Result> GetRecIdFromKeyAsync(string Key)
        {
            GetRecIdFromKey inValue = new GetRecIdFromKey();
            inValue.Key = Key;
            return ((MWSWhseEmployeeItemCatList_Port)(this)).GetRecIdFromKeyAsync(inValue);
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
        System.Threading.Tasks.Task<Delete_Result> MWSWhseEmployeeItemCatList_Port.DeleteAsync(Delete request)
        {
            return base.Channel.DeleteAsync(request);
        }

        public System.Threading.Tasks.Task<Delete_Result> DeleteAsync(string Key)
        {
            Delete inValue = new Delete();
            inValue.Key = Key;
            return ((MWSWhseEmployeeItemCatList_Port)(this)).DeleteAsync(inValue);
        }
    }


}
