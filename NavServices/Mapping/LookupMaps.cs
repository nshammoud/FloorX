using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KQF.Floor.Web.Models;
using KQF.Floor.Web.NavServices.Lookup;

namespace KQF.Floor.Web.NavServices.Mapping
{
    public class LookupMaps : AutoMapper.Profile
    {
        public LookupMaps()
        {
            CreateMap<Bin, LookupBin>()
                .ForMember(x => x.BinCode, o => o.MapFrom(s => s.BinCode))
                .ForMember(x => x.Items, o => o.MapFrom(s => s.Item == null ? new Item[] { } : s.Item))
                .ForMember(x => x.LocationCode, o => o.MapFrom(s => s.LocationCode))
                .ForMember(x => x.ResultType, o => o.MapFrom(s => "Bin"))
                .ForMember(x => x.Title, o => o.MapFrom(s => s.BinCode))
                .ForMember(x => x.Description, o => o.MapFrom(s => ""))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Container, LookupContainer>()
                .ForMember(x => x.BinCode, o => o.MapFrom(s => s.BinCode))
                .ForMember(x => x.ItemCategories, o => o.MapFrom(s => s.ItemCategory == null ? new ItemCategory[] { } : s.ItemCategory))
                .ForMember(x => x.LocationCode, o => o.MapFrom(s => s.LocationCode))
                .ForMember(x => x.ContainerNo, o => o.MapFrom(s => s.ContainerNo))
                .ForMember(x => x.ResultType, o => o.MapFrom(s => "Container"))
                .ForMember(x => x.Title, o => o.MapFrom(s => s.ContainerNo))
                .ForMember(x => x.Description, o => o.MapFrom(s => ""))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ItemCategory, LookupItemCategory>()
                .ForMember(x => x.Code, o => o.MapFrom(s => s.ICC_Code))
                .ForMember(x => x.ContainerDetails, o => o.MapFrom(s => s.ContainerDetail == null ? new ContainerDetail[] { } : s.ContainerDetail))
                .ForMember(x => x.Description, o => o.MapFrom(s => s.ICC_Description))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<ContainerDetail, LookupContainerDetail>()
                .ForMember(x => x.ItemNo, o => o.MapFrom(s => s.ItemNo))
                .ForMember(x => x.LotInformation, o => o.MapFrom(s => s.LotInformation == null ? new LotInformation[] { } : s.LotInformation))
                .ForMember(x => x.Description, o => o.MapFrom(s => s.Description))
                .ForMember(x => x.Regraded, o => o.MapFrom(s => s.Regraded))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<LotInformation, LookupLotInformation>()
                .ForMember(x => x.ExpirationDate, o => o.MapFrom(s => s.ExpirationDate))
                .ForMember(x => x.LotNo, o => o.MapFrom(s => s.LotNo))
                .ForMember(x => x.Sources, o => o.MapFrom(s => s.Source == null ? new Source[] { } : s.Source))
                .ForMember(x => x.ProductionDate, o => o.MapFrom(s => s.ProductionDate))
                .ForMember(x => x.Quantity, o => o.MapFrom(s => s.Quantity))
                .ForMember(x => x.UnitOfMeasureBase, o => o.MapFrom(s => s.UnitOfMeasureBase))
                .ForMember(x => x.OnHold, o => o.MapFrom(s => s.OnHold))

                .ForMember(x => x.OnHold, o => o.MapFrom(s => s.OnHold))
                .ForAllOtherMembers(x => x.Ignore());


            CreateMap<Source, LookupLotSource>()
                .ForMember(x => x.EntryNo, o => o.MapFrom(s => s.EntryNo))
                .ForMember(x => x.DocumentNo, o => o.MapFrom(s => s.DocumentNo))
                .ForMember(x => x.SourceItem, o => o.MapFrom(s => s.SourceItem))
                .ForMember(x => x.SourceLotNo, o => o.MapFrom(s => s.SourceLotNo))
                .ForMember(x => x.SourceType, o => o.MapFrom(s => s.Type))
                .ForMember(x => x.VendorName, o => o.MapFrom(s => s.VendorName))
                .ForMember(x => x.VendorNo, o => o.MapFrom(s => s.VendorNo))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Item, LookupItem>()
                .ForMember(x => x.ItemNo, o => o.MapFrom(s => s.ItemNo))
                .ForMember(x => x.ItemDescription, o => o.MapFrom(s => s.ItemDescription))
                .ForMember(x => x.Lots, o => o.MapFrom(s => s.Lot == null ? new Lot[] { } : s.Lot))

                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Lot, LookupLot>()
                .ForMember(x => x.LotNo, o => o.MapFrom(s => s.LotNo))
                .ForMember(x => x.QuantityBase, o => o.MapFrom(s => s.LotQuantityBase))
                 .ForMember(x => x.ProductionDate, o => o.MapFrom(s => s.ProductionDate))
                .ForMember(x => x.ExpirationDate, o => o.MapFrom(s => s.ExpirationDate))
                .ForMember(x => x.OnHold, o => o.MapFrom(s => s.OnHold))
                .ForMember(x => x.Containers, o => o.MapFrom(s => s.Container))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Container1, LookupLotContainer>()
                .ForMember(x => x.ContainerNo, o => o.MapFrom(s => s.ContainerNo))
                .ForMember(x => x.ContainerQuantityBase, o => o.MapFrom(s => s.ContainerQuantityBase))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Item1, LookupItemResult>()
                .ForMember(x => x.ItemNo, o => o.MapFrom(s => s.ItemNo))
                .ForMember(x => x.ItemDescription, o => o.MapFrom(s => s.Description))
                .ForMember(x => x.UoM, o => o.MapFrom(s => s.UOM))
                .ForMember(x => x.WarehouseEntries, o => o.MapFrom(s => s.WarehouseEntry == null ? new WarehouseEntry[] { } : s.WarehouseEntry))
                .ForMember(x => x.ResultType, o => o.MapFrom(s => "Item"))
                .ForMember(x => x.Title, o => o.MapFrom(s => s.ItemNo))
                .ForMember(x => x.Description, o => o.MapFrom(s => s.Description))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<WarehouseEntry, LookupWarehouseEntry>()
                .ForMember(x => x.Location, o => o.MapFrom(s => s.Location))
                .ForMember(x => x.Bin, o => o.MapFrom(s => s.Bin))
                .ForMember(x => x.LotNo, o => o.MapFrom(s => s.LotNo))
                .ForMember(x => x.ContainerNo, o => o.MapFrom(s => s.ContainerNo))
                .ForMember(x => x.Quantity, o => o.MapFrom(s => s.Quantity))
                .ForMember(x => x.ExpDate, o => o.MapFrom(s => s.ExpDate))
                .ForMember(x => x.OnHold, o => o.MapFrom(s => s.OnHold))
                .ForMember(x => x.ProdDate, o => o.MapFrom(s => s.ProdDate == null ? new string[] { } : s.ProdDate))
                .ForAllOtherMembers(x => x.Ignore());

        }
    }
}
