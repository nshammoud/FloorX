using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.NavServices.ItemLotList
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", ConfigurationName = "WSItemLotList_Port")]
    public interface WSItemLotList_Port
    {

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:Read", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Read_Result> ReadAsync(Read request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:ReadByRecId", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<ReadByRecId_Result> ReadByRecIdAsync(ReadByRecId request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:ReadMultiple", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<ReadMultiple_Result> ReadMultipleAsync(ReadMultiple request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:IsUpdated", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<IsUpdated_Result> IsUpdatedAsync(IsUpdated request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:GetRecIdFromKey", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<GetRecIdFromKey_Result> GetRecIdFromKeyAsync(GetRecIdFromKey request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:Create", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Create_Result> CreateAsync(Create request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:CreateMultiple", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<CreateMultiple_Result> CreateMultipleAsync(CreateMultiple request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:Update", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Update_Result> UpdateAsync(Update request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:UpdateMultiple", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<UpdateMultiple_Result> UpdateMultipleAsync(UpdateMultiple request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/wsitemlotlist:Delete", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Delete_Result> DeleteAsync(Delete request);
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist")]
    public partial class WSItemLotList
    {

        private string keyField;

        private string item_NoField;

        private string locationField;

        private string lot_NoField;

        private decimal available_Loose_ItemField;

        private bool available_Loose_ItemFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Item_No
        {
            get
            {
                return this.item_NoField;
            }
            set
            {
                this.item_NoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Lot_No
        {
            get
            {
                return this.lot_NoField;
            }
            set
            {
                this.lot_NoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public decimal Available_Loose_Item
        {
            get
            {
                return this.available_Loose_ItemField;
            }
            set
            {
                this.available_Loose_ItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Available_Loose_ItemSpecified
        {
            get
            {
                return this.available_Loose_ItemFieldSpecified;
            }
            set
            {
                this.available_Loose_ItemFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist")]
    public partial class WSItemLotList_Filter
    {

        private WSItemLotList_Fields fieldField;

        private string criteriaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public WSItemLotList_Fields Field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Criteria
        {
            get
            {
                return this.criteriaField;
            }
            set
            {
                this.criteriaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist")]
    public enum WSItemLotList_Fields
    {

        /// <remarks/>
        Item_No,

        /// <remarks/>
        Location,

        /// <remarks/>
        Lot_No,

        /// <remarks/>
        Available_Loose_Item,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Read", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class Read
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public string Item_No;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 1)]
        public string Location;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 2)]
        public string Lot_No;

        public Read()
        {
        }

        public Read(string Item_No, string Location, string Lot_No)
        {
            this.Item_No = Item_No;
            this.Location = Location;
            this.Lot_No = Lot_No;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Read_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class Read_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public WSItemLotList WSItemLotList;

        public Read_Result()
        {
        }

        public Read_Result(WSItemLotList WSItemLotList)
        {
            this.WSItemLotList = WSItemLotList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadByRecId", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class ReadByRecId
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public string recId;

        public ReadByRecId()
        {
        }

        public ReadByRecId(string recId)
        {
            this.recId = recId;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadByRecId_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class ReadByRecId_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public WSItemLotList WSItemLotList;

        public ReadByRecId_Result()
        {
        }

        public ReadByRecId_Result(WSItemLotList WSItemLotList)
        {
            this.WSItemLotList = WSItemLotList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadMultiple", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class ReadMultiple
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("filter")]
        public WSItemLotList_Filter[] filter;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 1)]
        public string bookmarkKey;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 2)]
        public int setSize;

        public ReadMultiple()
        {
        }

        public ReadMultiple(WSItemLotList_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.filter = filter;
            this.bookmarkKey = bookmarkKey;
            this.setSize = setSize;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadMultiple_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class ReadMultiple_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "ReadMultiple_Result", Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public WSItemLotList[] ReadMultiple_Result1;

        public ReadMultiple_Result()
        {
        }

        public ReadMultiple_Result(WSItemLotList[] ReadMultiple_Result1)
        {
            this.ReadMultiple_Result1 = ReadMultiple_Result1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "IsUpdated", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class IsUpdated
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public string Key;

        public IsUpdated()
        {
        }

        public IsUpdated(string Key)
        {
            this.Key = Key;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "IsUpdated_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class IsUpdated_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "IsUpdated_Result", Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public bool IsUpdated_Result1;

        public IsUpdated_Result()
        {
        }

        public IsUpdated_Result(bool IsUpdated_Result1)
        {
            this.IsUpdated_Result1 = IsUpdated_Result1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "GetRecIdFromKey", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class GetRecIdFromKey
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public string Key;

        public GetRecIdFromKey()
        {
        }

        public GetRecIdFromKey(string Key)
        {
            this.Key = Key;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "GetRecIdFromKey_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class GetRecIdFromKey_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetRecIdFromKey_Result", Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public string GetRecIdFromKey_Result1;

        public GetRecIdFromKey_Result()
        {
        }

        public GetRecIdFromKey_Result(string GetRecIdFromKey_Result1)
        {
            this.GetRecIdFromKey_Result1 = GetRecIdFromKey_Result1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Create", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class Create
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public WSItemLotList WSItemLotList;

        public Create()
        {
        }

        public Create(WSItemLotList WSItemLotList)
        {
            this.WSItemLotList = WSItemLotList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Create_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class Create_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public WSItemLotList WSItemLotList;

        public Create_Result()
        {
        }

        public Create_Result(WSItemLotList WSItemLotList)
        {
            this.WSItemLotList = WSItemLotList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "CreateMultiple", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class CreateMultiple
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public WSItemLotList[] WSItemLotList_List;

        public CreateMultiple()
        {
        }

        public CreateMultiple(WSItemLotList[] WSItemLotList_List)
        {
            this.WSItemLotList_List = WSItemLotList_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "CreateMultiple_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class CreateMultiple_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public WSItemLotList[] WSItemLotList_List;

        public CreateMultiple_Result()
        {
        }

        public CreateMultiple_Result(WSItemLotList[] WSItemLotList_List)
        {
            this.WSItemLotList_List = WSItemLotList_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Update", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class Update
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public WSItemLotList WSItemLotList;

        public Update()
        {
        }

        public Update(WSItemLotList WSItemLotList)
        {
            this.WSItemLotList = WSItemLotList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Update_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class Update_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public WSItemLotList WSItemLotList;

        public Update_Result()
        {
        }

        public Update_Result(WSItemLotList WSItemLotList)
        {
            this.WSItemLotList = WSItemLotList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "UpdateMultiple", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class UpdateMultiple
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public WSItemLotList[] WSItemLotList_List;

        public UpdateMultiple()
        {
        }

        public UpdateMultiple(WSItemLotList[] WSItemLotList_List)
        {
            this.WSItemLotList_List = WSItemLotList_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "UpdateMultiple_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class UpdateMultiple_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public WSItemLotList[] WSItemLotList_List;

        public UpdateMultiple_Result()
        {
        }

        public UpdateMultiple_Result(WSItemLotList[] WSItemLotList_List)
        {
            this.WSItemLotList_List = WSItemLotList_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Delete", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class Delete
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public string Key;

        public Delete()
        {
        }

        public Delete(string Key)
        {
            this.Key = Key;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Delete_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", IsWrapped = true)]
    public partial class Delete_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "Delete_Result", Namespace = "urn:microsoft-dynamics-schemas/page/wsitemlotlist", Order = 0)]
        public bool Delete_Result1;

        public Delete_Result()
        {
        }

        public Delete_Result(bool Delete_Result1)
        {
            this.Delete_Result1 = Delete_Result1;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface WSItemLotList_PortChannel : WSItemLotList_Port, System.ServiceModel.IClientChannel
    {
    }

}
