using KQF.Floor.NavServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.NavServices.ProductionMgmt
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class ProductionMgmtClient : NavServices.Clients.NavClientBase<MWSProductionMgmt_Port, ProductionMgmtClient>, MWSProductionMgmt_Port
    {



        public ProductionMgmtClient(Microsoft.Extensions.Options.IOptions<NavServiceConfig> config, System.Security.Claims.ClaimsPrincipal user, ILogger<ProductionMgmtClient> logger) :
              base(config, "MWSProductionMgmt_Port", user, logger)
        {

        }


        public System.Threading.Tasks.Task<PostOutput_Result> PostOutputAsync(PostOutput request)
        {
            return base.Channel.PostOutputAsync(request);
        }

        public System.Threading.Tasks.Task<GetTemplateAndBatch_Result> GetTemplateAndBatchAsync(GetTemplateAndBatch request)
        {
            return base.Channel.GetTemplateAndBatchAsync(request);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<GetOutputJnlTemplateName_Result> MWSProductionMgmt_Port.GetOutputJnlTemplateNameAsync(GetOutputJnlTemplateName request)
        {
            return base.Channel.GetOutputJnlTemplateNameAsync(request);
        }

        public System.Threading.Tasks.Task<GetOutputJnlTemplateName_Result> GetOutputJnlTemplateNameAsync(string pcodUserID)
        {
            GetOutputJnlTemplateName inValue = new GetOutputJnlTemplateName();
            inValue.pcodUserID = pcodUserID;
            return ((MWSProductionMgmt_Port)(this)).GetOutputJnlTemplateNameAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<PostConsumption_Result> MWSProductionMgmt_Port.PostConsumptionAsync(PostConsumption request)
        {
            return base.Channel.PostConsumptionAsync(request);
        }

        public System.Threading.Tasks.Task<PostConsumption_Result> PostConsumptionAsync(
                    string pcodUserID,
                    string pcodProdOrderNo,
                    int pintProdOrderLineNo,
                    int pintProdOrderCompLineNo,
                    int pintBatchNo,
                    string pcodItemNo,
                    string pcodVariantCode,
                    string pcodContainerNo,
                    string pcodLotNo,
                    string pcodSerialNo,
                    decimal pdecCatchWeight,
                    string pcodLocationCode,
                    string pcodBinCode,
                    decimal pdecQty,
                    string pcodUOM,
                    int pintAppliesFromEntry,
                    int pintAppliesToEntry,
                    string pMixerID,
                    string pRunID,
                    System.DateTime pPostingDate)
        {
            PostConsumption inValue = new PostConsumption();
            inValue.pcodUserID = pcodUserID;
            inValue.pcodProdOrderNo = pcodProdOrderNo;
            inValue.pintProdOrderLineNo = pintProdOrderLineNo;
            inValue.pintProdOrderCompLineNo = pintProdOrderCompLineNo;
            inValue.pintBatchNo = pintBatchNo;
            inValue.pcodItemNo = pcodItemNo;
            inValue.pcodVariantCode = pcodVariantCode;
            inValue.pcodContainerNo = pcodContainerNo;
            inValue.pcodLotNo = pcodLotNo;
            inValue.pcodSerialNo = pcodSerialNo;
            inValue.pdecCatchWeight = pdecCatchWeight;
            inValue.pcodLocationCode = pcodLocationCode;
            inValue.pcodBinCode = pcodBinCode;
            inValue.pdecQty = pdecQty;
            inValue.pcodUOM = pcodUOM;
            inValue.pintAppliesFromEntry = pintAppliesFromEntry;
            inValue.pintAppliesToEntry = pintAppliesToEntry;
            inValue.pMixerID = pMixerID;
            inValue.pRunID = pRunID;
            inValue.pPostingDate = pPostingDate;
            return ((MWSProductionMgmt_Port)(this)).PostConsumptionAsync(inValue);
        }

        public System.Threading.Tasks.Task<AddSubComponent_Result> AddSubComponentAsync(AddSubComponent request)
        {
            return base.Channel.AddSubComponentAsync(request);
        }

        public System.Threading.Tasks.Task<NicGetContainerContent_Result> NicGetContainerContentAsync(NicGetContainerContent request)
        {
            return base.Channel.NicGetContainerContentAsync(request);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<NicGetConsumedItemsQtySum_Result> MWSProductionMgmt_Port.NicGetConsumedItemsQtySumAsync(NicGetConsumedItemsQtySum request)
        {
            return base.Channel.NicGetConsumedItemsQtySumAsync(request);
        }

        public System.Threading.Tasks.Task<NicGetConsumedItemsQtySum_Result> NicGetConsumedItemsQtySumAsync(string itemNo, string lotNo, string pLocationCode)
        {
            NicGetConsumedItemsQtySum inValue = new NicGetConsumedItemsQtySum();
            inValue.itemNo = itemNo;
            inValue.lotNo = lotNo;
            inValue.pLocationCode = pLocationCode;
            return ((MWSProductionMgmt_Port)(this)).NicGetConsumedItemsQtySumAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<NicGetItemsQtySum_Result> MWSProductionMgmt_Port.NicGetItemsQtySumAsync(NicGetItemsQtySum request)
        {
            return base.Channel.NicGetItemsQtySumAsync(request);
        }

        public System.Threading.Tasks.Task<NicGetItemsQtySum_Result> NicGetItemsQtySumAsync(string itemNo, string lotNo)
        {
            NicGetItemsQtySum inValue = new NicGetItemsQtySum();
            inValue.itemNo = itemNo;
            inValue.lotNo = lotNo;
            return ((MWSProductionMgmt_Port)(this)).NicGetItemsQtySumAsync(inValue);
        }

        public System.Threading.Tasks.Task<GetContainerContentList_Result> GetContainerContentListAsync(GetContainerContentList request)
        {
            return base.Channel.GetContainerContentListAsync(request);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<GetHoldItemQtybyLotNo_Result> MWSProductionMgmt_Port.GetHoldItemQtybyLotNoAsync(GetHoldItemQtybyLotNo request)
        {
            return base.Channel.GetHoldItemQtybyLotNoAsync(request);
        }

        public System.Threading.Tasks.Task<GetHoldItemQtybyLotNo_Result> GetHoldItemQtybyLotNoAsync(string lotNo, string itemNo, string locationCode, string contNoa46)
        {
            GetHoldItemQtybyLotNo inValue = new GetHoldItemQtybyLotNo();
            inValue.lotNo = lotNo;
            inValue.itemNo = itemNo;
            inValue.locationCode = locationCode;
            inValue.contNoa46 = contNoa46;
            return ((MWSProductionMgmt_Port)(this)).GetHoldItemQtybyLotNoAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<GetContainerizedItemQty_Result> MWSProductionMgmt_Port.GetContainerizedItemQtyAsync(GetContainerizedItemQty request)
        {
            return base.Channel.GetContainerizedItemQtyAsync(request);
        }

        public System.Threading.Tasks.Task<GetContainerizedItemQty_Result> GetContainerizedItemQtyAsync(string itemNo, string lotNo)
        {
            GetContainerizedItemQty inValue = new GetContainerizedItemQty();
            inValue.itemNo = itemNo;
            inValue.lotNo = lotNo;
            return ((MWSProductionMgmt_Port)(this)).GetContainerizedItemQtyAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IsConsumptionBin_Result> MWSProductionMgmt_Port.IsConsumptionBinAsync(IsConsumptionBin request)
        {
            return base.Channel.IsConsumptionBinAsync(request);
        }

        public System.Threading.Tasks.Task<IsConsumptionBin_Result> IsConsumptionBinAsync(string pLocationCode, string pBinCode)
        {
            IsConsumptionBin inValue = new IsConsumptionBin();
            inValue.pLocationCode = pLocationCode;
            inValue.pBinCode = pBinCode;
            return ((MWSProductionMgmt_Port)(this)).IsConsumptionBinAsync(inValue);
        }

        public System.Threading.Tasks.Task<PostBulkConsumption_Result> PostBulkConsumptionAsync(PostBulkConsumption request)
        {
            return base.Channel.PostBulkConsumptionAsync(request);
        }

        public System.Threading.Tasks.Task<NicConvertItemUOM_Result> NicConvertItemUOMAsync(NicConvertItemUOM request)
        {
            return base.Channel.NicConvertItemUOMAsync(request);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IsCrossRefItemExist_Result> MWSProductionMgmt_Port.IsCrossRefItemExistAsync(IsCrossRefItemExist request)
        {
            return base.Channel.IsCrossRefItemExistAsync(request);
        }

        public System.Threading.Tasks.Task<IsCrossRefItemExist_Result> IsCrossRefItemExistAsync(string sourceItem, string crossRefItem)
        {
            IsCrossRefItemExist inValue = new IsCrossRefItemExist();
            inValue.sourceItem = sourceItem;
            inValue.crossRefItem = crossRefItem;
            return ((MWSProductionMgmt_Port)(this)).IsCrossRefItemExistAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<UpdateComponentMovedQtyWhenPost_Result> MWSProductionMgmt_Port.UpdateComponentMovedQtyWhenPostAsync(UpdateComponentMovedQtyWhenPost request)
        {
            return base.Channel.UpdateComponentMovedQtyWhenPostAsync(request);
        }

        public System.Threading.Tasks.Task<UpdateComponentMovedQtyWhenPost_Result> UpdateComponentMovedQtyWhenPostAsync(string pProdOrderNo, string pItemNo, decimal pMovedQty)
        {
            UpdateComponentMovedQtyWhenPost inValue = new UpdateComponentMovedQtyWhenPost();
            inValue.pProdOrderNo = pProdOrderNo;
            inValue.pItemNo = pItemNo;
            inValue.pMovedQty = pMovedQty;
            return ((MWSProductionMgmt_Port)(this)).UpdateComponentMovedQtyWhenPostAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<GetUnAllocatedMovedQtyInFBC_Result> MWSProductionMgmt_Port.GetUnAllocatedMovedQtyInFBCAsync(GetUnAllocatedMovedQtyInFBC request)
        {
            return base.Channel.GetUnAllocatedMovedQtyInFBCAsync(request);
        }

        public System.Threading.Tasks.Task<GetUnAllocatedMovedQtyInFBC_Result> GetUnAllocatedMovedQtyInFBCAsync(string pItemNo, string pProdOrderNo, string pLocationCode, string returnedValueMovedUOM)
        {
            GetUnAllocatedMovedQtyInFBC inValue = new GetUnAllocatedMovedQtyInFBC();
            inValue.pItemNo = pItemNo;
            inValue.pProdOrderNo = pProdOrderNo;
            inValue.pLocationCode = pLocationCode;
            inValue.returnedValueMovedUOM = returnedValueMovedUOM;
            return ((MWSProductionMgmt_Port)(this)).GetUnAllocatedMovedQtyInFBCAsync(inValue);
        }

        public System.Threading.Tasks.Task<GetItemLotList_Result> GetItemLotListAsync(GetItemLotList request)
        {
            return base.Channel.GetItemLotListAsync(request);
        }

        public System.Threading.Tasks.Task<MergeCART_Result> MergeCARTAsync(MergeCART request)
        {
            return base.Channel.MergeCARTAsync(request);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MoveCNT_Result> MWSProductionMgmt_Port.MoveCNTAsync(MoveCNT request)
        {
            return base.Channel.MoveCNTAsync(request);
        }

        public System.Threading.Tasks.Task<MoveCNT_Result> MoveCNTAsync(string pContainerNo, string pDestinationBinCode)
        {
            MoveCNT inValue = new MoveCNT();
            inValue.pContainerNo = pContainerNo;
            inValue.pDestinationBinCode = pDestinationBinCode;
            return ((MWSProductionMgmt_Port)(this)).MoveCNTAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SplitCNT_Result> MWSProductionMgmt_Port.SplitCNTAsync(SplitCNT request)
        {
            return base.Channel.SplitCNTAsync(request);
        }

        public System.Threading.Tasks.Task<SplitCNT_Result> SplitCNTAsync(string pCartNo, string pNewCartNo, int pNumberOfROD, decimal pGrossWeight)
        {
            SplitCNT inValue = new SplitCNT();
            inValue.pCartNo = pCartNo;
            inValue.pNewCartNo = pNewCartNo;
            inValue.pNumberOfROD = pNumberOfROD;
            inValue.pGrossWeight = pGrossWeight;
            return ((MWSProductionMgmt_Port)(this)).SplitCNTAsync(inValue);
        }
    }
}

