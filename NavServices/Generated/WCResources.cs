using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.NavServices.WCResources
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", ConfigurationName = "MWSWCresourceList_Port")]
    public interface MWSWCresourceList_Port
    {

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:Read", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Read_Result> ReadAsync(Read request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:ReadByRecId", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<ReadByRecId_Result> ReadByRecIdAsync(ReadByRecId request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:ReadMultiple", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<ReadMultiple_Result> ReadMultipleAsync(ReadMultiple request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:IsUpdated", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<IsUpdated_Result> IsUpdatedAsync(IsUpdated request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:GetRecIdFromKey", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<GetRecIdFromKey_Result> GetRecIdFromKeyAsync(GetRecIdFromKey request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:Create", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Create_Result> CreateAsync(Create request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:CreateMultiple", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<CreateMultiple_Result> CreateMultipleAsync(CreateMultiple request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:Update", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Update_Result> UpdateAsync(Update request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:UpdateMultiple", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<UpdateMultiple_Result> UpdateMultipleAsync(UpdateMultiple request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist:Delete", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Delete_Result> DeleteAsync(Delete request);
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist")]
    public partial class MWSWCresourceList
    {

        private string keyField;

        private string work_CenterField;

        private string wC_ResourceField;

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
        public string Work_Center
        {
            get
            {
                return this.work_CenterField;
            }
            set
            {
                this.work_CenterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string WC_Resource
        {
            get
            {
                return this.wC_ResourceField;
            }
            set
            {
                this.wC_ResourceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist")]
    public partial class MWSWCresourceList_Filter
    {

        private MWSWCresourceList_Fields fieldField;

        private string criteriaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public MWSWCresourceList_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist")]
    public enum MWSWCresourceList_Fields
    {

        /// <remarks/>
        Work_Center,

        /// <remarks/>
        WC_Resource,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Read", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class Read
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        public string Work_Center;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 1)]
        public string WC_Resource;

        public Read()
        {
        }

        public Read(string Work_Center, string WC_Resource)
        {
            this.Work_Center = Work_Center;
            this.WC_Resource = WC_Resource;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Read_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class Read_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        public MWSWCresourceList MWSWCresourceList;

        public Read_Result()
        {
        }

        public Read_Result(MWSWCresourceList MWSWCresourceList)
        {
            this.MWSWCresourceList = MWSWCresourceList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadByRecId", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class ReadByRecId
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadByRecId_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class ReadByRecId_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        public MWSWCresourceList MWSWCresourceList;

        public ReadByRecId_Result()
        {
        }

        public ReadByRecId_Result(MWSWCresourceList MWSWCresourceList)
        {
            this.MWSWCresourceList = MWSWCresourceList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadMultiple", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class ReadMultiple
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("filter")]
        public MWSWCresourceList_Filter[] filter;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 1)]
        public string bookmarkKey;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 2)]
        public int setSize;

        public ReadMultiple()
        {
        }

        public ReadMultiple(MWSWCresourceList_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.filter = filter;
            this.bookmarkKey = bookmarkKey;
            this.setSize = setSize;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadMultiple_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class ReadMultiple_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "ReadMultiple_Result", Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSWCresourceList[] ReadMultiple_Result1;

        public ReadMultiple_Result()
        {
        }

        public ReadMultiple_Result(MWSWCresourceList[] ReadMultiple_Result1)
        {
            this.ReadMultiple_Result1 = ReadMultiple_Result1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "IsUpdated", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class IsUpdated
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "IsUpdated_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class IsUpdated_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "IsUpdated_Result", Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "GetRecIdFromKey", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class GetRecIdFromKey
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "GetRecIdFromKey_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class GetRecIdFromKey_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetRecIdFromKey_Result", Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Create", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class Create
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        public MWSWCresourceList MWSWCresourceList;

        public Create()
        {
        }

        public Create(MWSWCresourceList MWSWCresourceList)
        {
            this.MWSWCresourceList = MWSWCresourceList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Create_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class Create_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        public MWSWCresourceList MWSWCresourceList;

        public Create_Result()
        {
        }

        public Create_Result(MWSWCresourceList MWSWCresourceList)
        {
            this.MWSWCresourceList = MWSWCresourceList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "CreateMultiple", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class CreateMultiple
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSWCresourceList[] MWSWCresourceList_List;

        public CreateMultiple()
        {
        }

        public CreateMultiple(MWSWCresourceList[] MWSWCresourceList_List)
        {
            this.MWSWCresourceList_List = MWSWCresourceList_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "CreateMultiple_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class CreateMultiple_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSWCresourceList[] MWSWCresourceList_List;

        public CreateMultiple_Result()
        {
        }

        public CreateMultiple_Result(MWSWCresourceList[] MWSWCresourceList_List)
        {
            this.MWSWCresourceList_List = MWSWCresourceList_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Update", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class Update
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        public MWSWCresourceList MWSWCresourceList;

        public Update()
        {
        }

        public Update(MWSWCresourceList MWSWCresourceList)
        {
            this.MWSWCresourceList = MWSWCresourceList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Update_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class Update_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        public MWSWCresourceList MWSWCresourceList;

        public Update_Result()
        {
        }

        public Update_Result(MWSWCresourceList MWSWCresourceList)
        {
            this.MWSWCresourceList = MWSWCresourceList;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "UpdateMultiple", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class UpdateMultiple
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSWCresourceList[] MWSWCresourceList_List;

        public UpdateMultiple()
        {
        }

        public UpdateMultiple(MWSWCresourceList[] MWSWCresourceList_List)
        {
            this.MWSWCresourceList_List = MWSWCresourceList_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "UpdateMultiple_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class UpdateMultiple_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSWCresourceList[] MWSWCresourceList_List;

        public UpdateMultiple_Result()
        {
        }

        public UpdateMultiple_Result(MWSWCresourceList[] MWSWCresourceList_List)
        {
            this.MWSWCresourceList_List = MWSWCresourceList_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Delete", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class Delete
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Delete_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", IsWrapped = true)]
    public partial class Delete_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "Delete_Result", Namespace = "urn:microsoft-dynamics-schemas/page/mwswcresourcelist", Order = 0)]
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
    public interface MWSWCresourceList_PortChannel : MWSWCresourceList_Port, System.ServiceModel.IClientChannel
    {
    }
}
