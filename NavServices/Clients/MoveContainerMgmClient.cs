using KQF.Floor.NavServices;
using Microsoft.Extensions.Logging;

namespace KQF.Floor.Web.NavServices.MoveContainerMgm
{
    public class MoveContainerMgmClient : NavServices.Clients.NavClientBase<MoveContainerMgm_Port, MoveContainerMgmClient>, MoveContainerMgm_Port
    {
        public MoveContainerMgmClient(Microsoft.Extensions.Options.IOptions<NavServiceConfig> config, System.Security.Claims.ClaimsPrincipal user, ILogger<MoveContainerMgmClient> logger) :
         base(config, "MoveContainerMgm_Port", user, logger)
        {

        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MoveContainer_Result> MoveContainerMgm_Port.MoveContainerAsync(MoveContainer request)
        {
            return base.Channel.MoveContainerAsync(request);
        }

        public System.Threading.Tasks.Task<MoveContainer_Result> MoveContainerAsync(string pContainerNo, string pCurrentBinCode, string pLocationCode, string pNewBinCode)
        {
            MoveContainer inValue = new MoveContainer();
            inValue.pContainerNo = pContainerNo;
            inValue.pCurrentBinCode = pCurrentBinCode;
            inValue.pLocationCode = pLocationCode;
            inValue.pNewBinCode = pNewBinCode;
            return ((MoveContainerMgm_Port)(this)).MoveContainerAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MoveBetweenBins_Result> MoveContainerMgm_Port.MoveBetweenBinsAsync(MoveBetweenBins request)
        {
            return base.Channel.MoveBetweenBinsAsync(request);
        }

        public System.Threading.Tasks.Task<MoveBetweenBins_Result> MoveBetweenBinsAsync(string pLocationCode, string pItemNo, string pLotNo, string pSourceBin, decimal pQty, string pDestinationBin)
        {
            MoveBetweenBins inValue = new MoveBetweenBins();
            inValue.pLocationCode = pLocationCode;
            inValue.pItemNo = pItemNo;
            inValue.pLotNo = pLotNo;
            inValue.pSourceBin = pSourceBin;
            inValue.pQty = pQty;
            inValue.pDestinationBin = pDestinationBin;
            return ((MoveContainerMgm_Port)(this)).MoveBetweenBinsAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MoveItemsToContainer_Result> MoveContainerMgm_Port.MoveItemsToContainerAsync(MoveItemsToContainer request)
        {
            return base.Channel.MoveItemsToContainerAsync(request);
        }

        public System.Threading.Tasks.Task<MoveItemsToContainer_Result> MoveItemsToContainerAsync(string pLocationCode, string pItemNo, string pLotNo, string pSourceBin, decimal pQty, string pContainerNo)
        {
            MoveItemsToContainer inValue = new MoveItemsToContainer();
            inValue.pLocationCode = pLocationCode;
            inValue.pItemNo = pItemNo;
            inValue.pLotNo = pLotNo;
            inValue.pSourceBin = pSourceBin;
            inValue.pQty = pQty;
            inValue.pContainerNo = pContainerNo;
            return ((MoveContainerMgm_Port)(this)).MoveItemsToContainerAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MoveItemsBetweenContainers_Result> MoveContainerMgm_Port.MoveItemsBetweenContainersAsync(MoveItemsBetweenContainers request)
        {
            return base.Channel.MoveItemsBetweenContainersAsync(request);
        }

        public System.Threading.Tasks.Task<MoveItemsBetweenContainers_Result> MoveItemsBetweenContainersAsync(string pLocationCode, string pItemNo, string pLotNo, decimal pQty, string pDestinationBin, string pSourceContainerNo, string pDestContainerNo, bool pMovePartial)
        {
            MoveItemsBetweenContainers inValue = new MoveItemsBetweenContainers();
            inValue.pLocationCode = pLocationCode;
            inValue.pItemNo = pItemNo;
            inValue.pLotNo = pLotNo;
            inValue.pQty = pQty;
            inValue.pDestinationBin = pDestinationBin;
            inValue.pSourceContainerNo = pSourceContainerNo;
            inValue.pDestContainerNo = pDestContainerNo;
            inValue.pMovePartial = pMovePartial;
            return ((MoveContainerMgm_Port)(this)).MoveItemsBetweenContainersAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RemoveContentOfContainer_Result> MoveContainerMgm_Port.RemoveContentOfContainerAsync(RemoveContentOfContainer request)
        {
            return base.Channel.RemoveContentOfContainerAsync(request);
        }

        public System.Threading.Tasks.Task<RemoveContentOfContainer_Result> RemoveContentOfContainerAsync(string pLocationCode, string pItemNo, string pLotNo, string pSourceBin, decimal pQty, string pDestinationBin, string pSourceContainerNo, string pDestContainerNo, bool pMovePartial)
        {
            RemoveContentOfContainer inValue = new RemoveContentOfContainer();
            inValue.pLocationCode = pLocationCode;
            inValue.pItemNo = pItemNo;
            inValue.pLotNo = pLotNo;
            inValue.pSourceBin = pSourceBin;
            inValue.pQty = pQty;
            inValue.pDestinationBin = pDestinationBin;
            inValue.pSourceContainerNo = pSourceContainerNo;
            inValue.pDestContainerNo = pDestContainerNo;
            inValue.pMovePartial = pMovePartial;
            return ((MoveContainerMgm_Port)(this)).RemoveContentOfContainerAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<MoveContainerToBin_Result> MoveContainerMgm_Port.MoveContainerToBinAsync(MoveContainerToBin request)
        {
            return base.Channel.MoveContainerToBinAsync(request);
        }

        public System.Threading.Tasks.Task<MoveContainerToBin_Result> MoveContainerToBinAsync(string pLocationCode, string pDestinationBin, string pSourceContainerNo)
        {
            MoveContainerToBin inValue = new MoveContainerToBin();
            inValue.pLocationCode = pLocationCode;
            inValue.pDestinationBin = pDestinationBin;
            inValue.pSourceContainerNo = pSourceContainerNo;
            return ((MoveContainerMgm_Port)(this)).MoveContainerToBinAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<GetTemplateAndBatch_Result> MoveContainerMgm_Port.GetTemplateAndBatchAsync(GetTemplateAndBatch request)
        {
            return base.Channel.GetTemplateAndBatchAsync(request);
        }

        public System.Threading.Tasks.Task<GetTemplateAndBatch_Result> GetTemplateAndBatchAsync(string pcodUserID, string pcodTemplate, string pcodBatch)
        {
            GetTemplateAndBatch inValue = new GetTemplateAndBatch();
            inValue.Body = new GetTemplateAndBatchBody();
            inValue.Body.pcodUserID = pcodUserID;
            inValue.Body.pcodTemplate = pcodTemplate;
            inValue.Body.pcodBatch = pcodBatch;
            return ((MoveContainerMgm_Port)(this)).GetTemplateAndBatchAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<GetOutputJnlTemplateName_Result> MoveContainerMgm_Port.GetOutputJnlTemplateNameAsync(GetOutputJnlTemplateName request)
        {
            return base.Channel.GetOutputJnlTemplateNameAsync(request);
        }

        public System.Threading.Tasks.Task<GetOutputJnlTemplateName_Result> GetOutputJnlTemplateNameAsync(string pcodUserID)
        {
            GetOutputJnlTemplateName inValue = new GetOutputJnlTemplateName();
            inValue.pcodUserID = pcodUserID;
            return ((MoveContainerMgm_Port)(this)).GetOutputJnlTemplateNameAsync(inValue);
        }
    }
}
