using System.Collections.Generic;

namespace Infrastructure.Data.Models.Responses
{
    public class BaseResponse
    {
        public ResponseStatus Status { get; set; }
        public TranslationCode? MessageTranslationCode { get; set; }
        public string? LogMessage { get; set; }
        public string? CustomMessage { get; set; }
    }

    public enum TranslationCode
    {
        UnknownError,
        DataLoadError,
        StartTaskSuccess,
        StartTaskError,
        AcceptOrderSuccess,
        FinalizeOrderSuccess,
        AcceptOrderError,
        FinalizeOrderError,
        DeleteLocationDeviceSuccess,
        UploadDocumentSuccess,
        UploadCommentSuccess,
        ActiveTasks,
        ConstructionStartedTransferOrder,
        TransferOrderSuccess,
        CancelTaskSuccess,
        CancelTaskError,
        RefreshPageError,
        CancelOrderSuccess,
        CancelOrderError,
        ProposeCancelOrderSuccess,
        ProposeCancelOrderError,
        DenyOrderCancellationSuccess,
        DenyOrderCancellationError,
        RaiseConstructionInfoNeededSuccess,
        FinalizeConstructionInfoNeededTaskSuccess,
        UpdateBusinessGroupSuccess,
        SaveCentrexConfigSuccess,
        SaveCfsVirtualNumbersSuccess,
        SaveTrunkConfigSuccess,
        MarkNotificationsAsReadSuccess,
        MarkNotificationsAsUnreadSuccess,
        SaveNumerationsSuccess,
        StoreCpeDataSuccess,
        StoreCpeDataError,
        SiebelOrderUnready,
        SiebelOrdersUnready,
        ConstructionProblem,
        OrderOnHoldSuccess,
        OrderResumeSuccess,
        AddRegionSuccess,
        UpdateRegionSuccess,
        DeleteRegionSuccess,
        UpdateTechnicianTimesSuccess,
        WbsRequestError,
        NumerationNotEnabled,
        UpdateOnHoldReasonSucces,
        UnassignOrderSuccess,
        UnassignOrderError,
        ProvisioningRollbackDone
    }

    public enum ResponseStatus
    {
        Success,
        Error,
        UnhandledException
    }
}
