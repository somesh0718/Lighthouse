using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Platform;
using System;
using System.Linq;

namespace Igmite.Lighthouse.Mappers
{
    public static class ErrorLogMapper
    {
        public static ErrorLogModel ToModel(this ErrorLog errorLog)
        {
            if (errorLog == null)
                return null;

            ErrorLogModel errorLogModel = new ErrorLogModel
            {
                ErrorLogId = errorLog.ErrorLogId,
                ModuleName = errorLog.ModuleName,
                ErrorCode = errorLog.ErrorCode,
                ErrorSeverity = errorLog.ErrorSeverity,
                ErrorState = errorLog.ErrorState,
                ErrorProcedure = errorLog.ErrorProcedure,
                ErrorLine = errorLog.ErrorLine,
                ErrorTime = errorLog.ErrorTime,
                ErrorType = errorLog.ErrorType,
                ErrorLocation = errorLog.ErrorLocation,
                ErrorMessage = errorLog.ErrorMessage,
                StackTrace = errorLog.StackTrace,
                ErrorStatus = errorLog.ErrorStatus,
                IsResolved = errorLog.IsResolved,
                CreatedBy = errorLog.CreatedBy,
                CreatedOn = errorLog.CreatedOn,
                UpdatedBy = errorLog.UpdatedBy,
                UpdatedOn = errorLog.UpdatedOn,
                IsActive = errorLog.IsActive
            };

            return errorLogModel;
        }
        public static ErrorLog FromModel(this ErrorLogModel errorLogModel, ErrorLog errorLog)
        {
            errorLog.ErrorLogId = errorLogModel.ErrorLogId;
            errorLog.ModuleName = errorLogModel.ModuleName;
            errorLog.ErrorCode = errorLogModel.ErrorCode;
            errorLog.ErrorSeverity = errorLogModel.ErrorSeverity;
            errorLog.ErrorState = errorLogModel.ErrorState;
            errorLog.ErrorProcedure = errorLogModel.ErrorProcedure;
            errorLog.ErrorLine = errorLogModel.ErrorLine;
            errorLog.ErrorTime = errorLogModel.ErrorTime;
            errorLog.ErrorType = errorLogModel.ErrorType;
            errorLog.ErrorLocation = errorLogModel.ErrorLocation;
            errorLog.ErrorMessage = errorLogModel.ErrorMessage;
            errorLog.StackTrace = errorLogModel.StackTrace;
            errorLog.ErrorStatus = errorLogModel.ErrorStatus;
            errorLog.IsResolved = errorLogModel.IsResolved;
            errorLog.IsActive = errorLogModel.IsActive;
            errorLog.RequestType = errorLogModel.RequestType;
            errorLog.SetAuditValues(errorLogModel.RequestType);

            return errorLog;
        }
    }
}
