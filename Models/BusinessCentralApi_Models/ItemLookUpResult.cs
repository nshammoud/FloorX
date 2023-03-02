namespace KQF.Floor.Web.Models.BusinessCentralApi_Models
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50203")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:microsoft-dynamics-nav/xmlports/x50203", IsNullable = false)]
    public partial class ItemLookup
    {

        private ItemLookupItem itemField;

        /// <remarks/>
        public ItemLookupItem Item
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50203")]
    public partial class ItemLookupItem
    {

        private string itemNoField;

        private string descriptionField;

        private string uOMField;

        private ItemLookupItemWarehouseEntry[] warehouseEntryField;

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
        public string UOM
        {
            get
            {
                return this.uOMField;
            }
            set
            {
                this.uOMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("WarehouseEntry")]
        public ItemLookupItemWarehouseEntry[] WarehouseEntry
        {
            get
            {
                return this.warehouseEntryField;
            }
            set
            {
                this.warehouseEntryField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50203")]
    public partial class ItemLookupItemWarehouseEntry
    {

        private string locationField;

        private string binField;

        private string lotNoField;

        private string containerNoField;

        private string quantityField;

        private string expDateField;

        private string prodDateField;

        private string onHoldField;

        /// <remarks/>
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
        public string Bin
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
        public string ContainerNo
        {
            get
            {
                return this.containerNoField;
            }
            set
            {
                this.containerNoField = value;
            }
        }

        /// <remarks/>
        public string Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        public string ExpDate
        {
            get
            {
                return this.expDateField;
            }
            set
            {
                this.expDateField = value;
            }
        }

        /// <remarks/>
        public string ProdDate
        {
            get
            {
                return this.prodDateField;
            }
            set
            {
                this.prodDateField = value;
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
