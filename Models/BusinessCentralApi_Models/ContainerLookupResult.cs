
namespace KQF.Floor.Web.Models.BusinessCentralApi_Models
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50202")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:microsoft-dynamics-nav/xmlports/x50202", IsNullable = false)]
    public partial class PackageLookup
    {

        private PackageLookupPackage[] packageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Package")]
        public PackageLookupPackage[] Package
        {
            get
            {
                return this.packageField;
            }
            set
            {
                this.packageField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50202")]
    public partial class PackageLookupPackage
    {

        private string packageNoField;

        private string locationCodeField;

        private string binCodeField;

        private PackageLookupPackageItemCategory itemCategoryField;

        /// <remarks/>
        public string PackageNo
        {
            get
            {
                return this.packageNoField;
            }
            set
            {
                this.packageNoField = value;
            }
        }

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
        public PackageLookupPackageItemCategory ItemCategory
        {
            get
            {
                return this.itemCategoryField;
            }
            set
            {
                this.itemCategoryField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50202")]
    public partial class PackageLookupPackageItemCategory
    {

        private string iCC_CodeField;

        private string iCC_DescriptionField;

        private PackageLookupPackageItemCategoryPackageDetail packageDetailField;

        /// <remarks/>
        public string ICC_Code
        {
            get
            {
                return this.iCC_CodeField;
            }
            set
            {
                this.iCC_CodeField = value;
            }
        }

        /// <remarks/>
        public string ICC_Description
        {
            get
            {
                return this.iCC_DescriptionField;
            }
            set
            {
                this.iCC_DescriptionField = value;
            }
        }

        /// <remarks/>
        public PackageLookupPackageItemCategoryPackageDetail PackageDetail
        {
            get
            {
                return this.packageDetailField;
            }
            set
            {
                this.packageDetailField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50202")]
    public partial class PackageLookupPackageItemCategoryPackageDetail
    {

        private string itemNoField;

        private string descriptionField;

        private string regradedField;

        private PackageLookupPackageItemCategoryPackageDetailLotInformation lotInformationField;

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
        public string Regraded
        {
            get
            {
                return this.regradedField;
            }
            set
            {
                this.regradedField = value;
            }
        }

        /// <remarks/>
        public PackageLookupPackageItemCategoryPackageDetailLotInformation LotInformation
        {
            get
            {
                return this.lotInformationField;
            }
            set
            {
                this.lotInformationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:microsoft-dynamics-nav/xmlports/x50202")]
    public partial class PackageLookupPackageItemCategoryPackageDetailLotInformation
    {

        private string lotNoField;

        private string expirationDateField;

        private string productionDateField;

        private decimal quantityField;

        private string unitOfMeasureBaseField;

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
        public decimal Quantity
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
        public string UnitOfMeasureBase
        {
            get
            {
                return this.unitOfMeasureBaseField;
            }
            set
            {
                this.unitOfMeasureBaseField = value;
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


