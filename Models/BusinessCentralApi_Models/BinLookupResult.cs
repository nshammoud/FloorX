namespace KQF.Floor.Web.Models.BusinessCentralApi_Models
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50201")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:microsoft-dynamics-nav/xmlports/x50201", IsNullable = false)]
    public partial class BinLookup
    {

        private BinLookupBin[] binField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Bin")]
        public BinLookupBin[] Bin
        {
            get
            {
                return this.binField;
            }
            set
            {
                this.binField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50201")]
    public partial class BinLookupBin
    {

        private string locationCodeField;

        private string binCodeField;

        private BinLookupBinItem itemField;

        /// <remarks/>
        public string LocationCode
        {
            get
            {
                return this.locationCodeField;
            }
            set
            {
                this.locationCodeField = value;
            }
        }

        /// <remarks/>
        public string BinCode
        {
            get
            {
                return this.binCodeField;
            }
            set
            {
                this.binCodeField = value;
            }
        }

        /// <remarks/>
        public BinLookupBinItem Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50201")]
    public partial class BinLookupBinItem
    {

        private ushort itemNoField;

        private object itemDescriptionField;

        private string itemQuantityBaseField;

        private BinLookupBinItemLot lotField;

        /// <remarks/>
        public ushort ItemNo
        {
            get
            {
                return this.itemNoField;
            }
            set
            {
                this.itemNoField = value;
            }
        }

        /// <remarks/>
        public object ItemDescription
        {
            get
            {
                return this.itemDescriptionField;
            }
            set
            {
                this.itemDescriptionField = value;
            }
        }

        /// <remarks/>
        public string ItemQuantityBase
        {
            get
            {
                return this.itemQuantityBaseField;
            }
            set
            {
                this.itemQuantityBaseField = value;
            }
        }

        /// <remarks/>
        public BinLookupBinItemLot Lot
        {
            get
            {
                return this.lotField;
            }
            set
            {
                this.lotField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50201")]
    public partial class BinLookupBinItemLot
    {

        private object lotNoField;

        private string lotQuantityBaseField;

        private object productionDateField;

        private object expirationDateField;

        private string onHoldField;

        /// <remarks/>
        public object LotNo
        {
            get
            {
                return this.lotNoField;
            }
            set
            {
                this.lotNoField = value;
            }
        }

        /// <remarks/>
        public string LotQuantityBase
        {
            get
            {
                return this.lotQuantityBaseField;
            }
            set
            {
                this.lotQuantityBaseField = value;
            }
        }

        /// <remarks/>
        public object ProductionDate
        {
            get
            {
                return this.productionDateField;
            }
            set
            {
                this.productionDateField = value;
            }
        }

        /// <remarks/>
        public object ExpirationDate
        {
            get
            {
                return this.expirationDateField;
            }
            set
            {
                this.expirationDateField = value;
            }
        }

        /// <remarks/>
        public string OnHold
        {
            get
            {
                return this.onHoldField;
            }
            set
            {
                this.onHoldField = value;
            }
        }
    }


}
