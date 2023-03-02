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

        private BinLookupBin binField;

        /// <remarks/>
        public BinLookupBin Bin
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

        private BinLookupBinItem[] itemField;

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
        [System.Xml.Serialization.XmlElementAttribute("Item")]
        public BinLookupBinItem[] Item
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

        private string itemNoField;

        private string itemDescriptionField;

        private string itemQuantityBaseField;

        private BinLookupBinItemLot[] lotField;

        /// <remarks/>
        public string ItemNo
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
        public string ItemDescription
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
        [System.Xml.Serialization.XmlElementAttribute("Lot")]
        public BinLookupBinItemLot[] Lot
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

        private string lotNoField;

        private string lotQuantityBaseField;

        private string productionDateField;

        private string expirationDateField;

        private string onHoldField;

        /// <remarks/>
        public string LotNo
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
        public string ProductionDate
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
        public string ExpirationDate
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
