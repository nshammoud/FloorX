using System.Collections.Generic;

namespace KQF.Floor.Web.Models
{
    public class LookupModel
    {
        public string SearchTerm { get; set; }
        public List<LookupResult> Results { get; set; }

        public Models.BusinessCentralApi_Models.BinLookup LookupResults { get; set; }
    }

    public abstract class LookupResult
    {
        public string ResultType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LocationCode { get; set; }
    }
    
    public class LookupBin : LookupResult
    {
        public string BinCode { get; set; }
        public LookupItem[] Items { get; set; }
    }

    public class LookupContainer : LookupResult
    {
        public string ContainerNo { get; set; }
        public string BinCode { get; set; }
        public LookupItemCategory[] ItemCategories { get; set; }

    }

    public class LookupItemCategory
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public LookupContainerDetail[] ContainerDetails { get; set; }
    }

    public class LookupContainerDetail
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string Regraded { get; set; }
        public LookupLotInformation[] LotInformation { get; set; }
    }

    public class LookupLotInformation
    {
        public string LotNo { get; set; }
        public string ExpirationDate { get; set; }
        public string ProductionDate { get; set; }
        public string Quantity { get; set; }
        public string UnitOfMeasureBase { get; set; }
        public LookupLotSource[] Sources { get; set; }
        public string OnHold { get; set; }
    }

    public class LookupLotSource
    {
        public int EntryNo { get; set; }
        public string SourceItem { get; set; }
        public string SourceLotNo { get; set; }
        public string SourceType { get; set; }
        public string VendorNo { get; set; }
        public string VendorName { get; set; }
        public string DocumentNo { get; set; }
    }

    public class LookupItemResult : LookupResult
    {
        public string ItemNo { get; set; }
        public string ItemDescription { get; set; }
        public string UoM { get; set; }
        public LookupWarehouseEntry[] WarehouseEntries { get; set; }
    }

    public class LookupWarehouseEntry
    {
        public string Location { get; set; }
        public string Bin { get; set; }
        public string LotNo { get; set; }
        public string ContainerNo { get; set; }
        public string Quantity { get; set; }
        public string ExpDate { get; set; }
        public string[] ProdDate { get; set; }
        public string OnHold { get; set; }
    }
    
    public class LookupItem
    {
        public string ItemNo { get; set; }
        public string ItemDescription { get; set; }
        public LookupLot[] Lots { get; set; }
    }

    public class LookupLot
    {
        public string LotNo { get; set; }
        public string QuantityBase { get; set; }
        public string ProductionDate { get; set; }
        public string ExpirationDate { get; set; }
        public string OnHold { get; set; }
        public LookupLotContainer[] Containers { get; set; }
    }

    public class LookupLotContainer
    {
        public string ContainerNo { get; set; }
        public string ContainerQuantityBase { get; set; }
    }
}
