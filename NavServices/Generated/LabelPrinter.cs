﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.NavServices.LabelPrinter
{
    //------------------------------------------------------------------------------
    // <auto-generated>
    //     This code was generated by a tool.
    //
    //     Changes to this file may cause incorrect behavior and will be lost if
    //     the code is regenerated.
    // </auto-generated>
    //------------------------------------------------------------------------------



    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", ConfigurationName = "MWSASNLablePrinter_Port")]
    public interface MWSASNLablePrinter_Port
    {

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:Read", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Read_Result> ReadAsync(Read request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:ReadByRecId", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<ReadByRecId_Result> ReadByRecIdAsync(ReadByRecId request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:ReadMultiple", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<ReadMultiple_Result> ReadMultipleAsync(ReadMultiple request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:IsUpdated", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<IsUpdated_Result> IsUpdatedAsync(IsUpdated request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:GetRecIdFromKey", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<GetRecIdFromKey_Result> GetRecIdFromKeyAsync(GetRecIdFromKey request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:Create", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Create_Result> CreateAsync(Create request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:CreateMultiple", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<CreateMultiple_Result> CreateMultipleAsync(CreateMultiple request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:Update", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Update_Result> UpdateAsync(Update request);

        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:UpdateMultiple", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<UpdateMultiple_Result> UpdateMultipleAsync(UpdateMultiple request);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter:Delete", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
        System.Threading.Tasks.Task<Delete_Result> DeleteAsync(Delete request);
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter")]
    public partial class MWSASNLablePrinter
    {

        private string keyField;

        private string aSN_Label_PrinterField;

        private string descriptionField;

        private string location_CodeField;

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
        public string ASN_Label_Printer
        {
            get
            {
                return this.aSN_Label_PrinterField;
            }
            set
            {
                this.aSN_Label_PrinterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Location_Code
        {
            get
            {
                return this.location_CodeField;
            }
            set
            {
                this.location_CodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter")]
    public partial class MWSASNLablePrinter_Filter
    {

        private MWSASNLablePrinter_Fields fieldField;

        private string criteriaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public MWSASNLablePrinter_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter")]
    public enum MWSASNLablePrinter_Fields
    {

        /// <remarks/>
        ASN_Label_Printer,

        /// <remarks/>
        Description,

        /// <remarks/>
        Location_Code,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Read", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class Read
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        public string ASN_Label_Printer;

        public Read()
        {
        }

        public Read(string ASN_Label_Printer)
        {
            this.ASN_Label_Printer = ASN_Label_Printer;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Read_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class Read_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        public MWSASNLablePrinter MWSASNLablePrinter;

        public Read_Result()
        {
        }

        public Read_Result(MWSASNLablePrinter MWSASNLablePrinter)
        {
            this.MWSASNLablePrinter = MWSASNLablePrinter;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadByRecId", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class ReadByRecId
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadByRecId_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class ReadByRecId_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        public MWSASNLablePrinter MWSASNLablePrinter;

        public ReadByRecId_Result()
        {
        }

        public ReadByRecId_Result(MWSASNLablePrinter MWSASNLablePrinter)
        {
            this.MWSASNLablePrinter = MWSASNLablePrinter;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadMultiple", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class ReadMultiple
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("filter")]
        public MWSASNLablePrinter_Filter[] filter;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 1)]
        public string bookmarkKey;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 2)]
        public int setSize;

        public ReadMultiple()
        {
        }

        public ReadMultiple(MWSASNLablePrinter_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.filter = filter;
            this.bookmarkKey = bookmarkKey;
            this.setSize = setSize;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "ReadMultiple_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class ReadMultiple_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "ReadMultiple_Result", Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSASNLablePrinter[] ReadMultiple_Result1;

        public ReadMultiple_Result()
        {
        }

        public ReadMultiple_Result(MWSASNLablePrinter[] ReadMultiple_Result1)
        {
            this.ReadMultiple_Result1 = ReadMultiple_Result1;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "IsUpdated", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class IsUpdated
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "IsUpdated_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class IsUpdated_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "IsUpdated_Result", Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "GetRecIdFromKey", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class GetRecIdFromKey
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "GetRecIdFromKey_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class GetRecIdFromKey_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetRecIdFromKey_Result", Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Create", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class Create
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        public MWSASNLablePrinter MWSASNLablePrinter;

        public Create()
        {
        }

        public Create(MWSASNLablePrinter MWSASNLablePrinter)
        {
            this.MWSASNLablePrinter = MWSASNLablePrinter;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Create_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class Create_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        public MWSASNLablePrinter MWSASNLablePrinter;

        public Create_Result()
        {
        }

        public Create_Result(MWSASNLablePrinter MWSASNLablePrinter)
        {
            this.MWSASNLablePrinter = MWSASNLablePrinter;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "CreateMultiple", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class CreateMultiple
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSASNLablePrinter[] MWSASNLablePrinter_List;

        public CreateMultiple()
        {
        }

        public CreateMultiple(MWSASNLablePrinter[] MWSASNLablePrinter_List)
        {
            this.MWSASNLablePrinter_List = MWSASNLablePrinter_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "CreateMultiple_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class CreateMultiple_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSASNLablePrinter[] MWSASNLablePrinter_List;

        public CreateMultiple_Result()
        {
        }

        public CreateMultiple_Result(MWSASNLablePrinter[] MWSASNLablePrinter_List)
        {
            this.MWSASNLablePrinter_List = MWSASNLablePrinter_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Update", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class Update
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        public MWSASNLablePrinter MWSASNLablePrinter;

        public Update()
        {
        }

        public Update(MWSASNLablePrinter MWSASNLablePrinter)
        {
            this.MWSASNLablePrinter = MWSASNLablePrinter;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Update_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class Update_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        public MWSASNLablePrinter MWSASNLablePrinter;

        public Update_Result()
        {
        }

        public Update_Result(MWSASNLablePrinter MWSASNLablePrinter)
        {
            this.MWSASNLablePrinter = MWSASNLablePrinter;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "UpdateMultiple", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class UpdateMultiple
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSASNLablePrinter[] MWSASNLablePrinter_List;

        public UpdateMultiple()
        {
        }

        public UpdateMultiple(MWSASNLablePrinter[] MWSASNLablePrinter_List)
        {
            this.MWSASNLablePrinter_List = MWSASNLablePrinter_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "UpdateMultiple_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class UpdateMultiple_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public MWSASNLablePrinter[] MWSASNLablePrinter_List;

        public UpdateMultiple_Result()
        {
        }

        public UpdateMultiple_Result(MWSASNLablePrinter[] MWSASNLablePrinter_List)
        {
            this.MWSASNLablePrinter_List = MWSASNLablePrinter_List;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Delete", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class Delete
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
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
    [System.ServiceModel.MessageContractAttribute(WrapperName = "Delete_Result", WrapperNamespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", IsWrapped = true)]
    public partial class Delete_Result
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "Delete_Result", Namespace = "urn:microsoft-dynamics-schemas/page/mwsasnlableprinter", Order = 0)]
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
    public interface MWSASNLablePrinter_PortChannel : MWSASNLablePrinter_Port, System.ServiceModel.IClientChannel
    {
    }
}
