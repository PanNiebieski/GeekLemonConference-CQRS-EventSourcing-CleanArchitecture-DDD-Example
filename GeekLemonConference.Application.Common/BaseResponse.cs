using FluentValidation.Results;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.Common
{
    public abstract class BaseResponse
    {
        public ResponseStatus Status { get; set; }

        public string StatusInfo
        {
            get
            {
                return Status.ToString();
            }
        }

        public WhatHTTPCodeShouldBeRetruned WhatHTTPCodeToBeRetruned
        {
            get
            {
                if (this.Status == ResponseStatus.BussinesLogicError)
                    return WhatHTTPCodeShouldBeRetruned.Forbid;
                if (this.Status == ResponseStatus.NotFoundInDataBase)
                    return WhatHTTPCodeShouldBeRetruned.NotFound;
                if (this.Status == ResponseStatus.ValidationError ||
                    this.Status == ResponseStatus.BadQuery ||
                    this.Status == ResponseStatus.ConcurrencyOlderVersionSendedWhenNewerIsInEventStore)
                    return WhatHTTPCodeShouldBeRetruned.BadRequest;
                if (!this.Success)
                    return WhatHTTPCodeShouldBeRetruned.BadRequest;
                else
                    return WhatHTTPCodeShouldBeRetruned.Ok;
            }
        }

        public bool Success { get; set; }
        public string Message { get; set; }

        public List<string> ValidationErrors { get; set; }

        protected BaseResponse()
        {
            ValidationErrors = new List<string>();
            Success = true;
            Status = ResponseStatus.Success;
        }

        protected BaseResponse(ExecutionStatus status)
        {
            ValidationErrors = new List<string>();
            if (!status.Success)
            {
                Success = false;
                if (status.Where == WhereExecuted.DomainLogic)
                    Status = ResponseStatus.BussinesLogicError;
                if (status.Where == WhereExecuted.DataBase)
                    Status = ResponseStatus.DataBaseError;
                if (status.Where == WhereExecuted.DataBase
                    && status.Reason == Reason.ReturnedNull)
                    Status = ResponseStatus.NotFoundInDataBase;

                if (status.Reason == Reason.AggregateNotFoundInEventStore)
                    Status = ResponseStatus.AggregateNotFoundInEventStore;

                if (status.Reason == Reason.AggregateOrEventMissingIdInEventStore)
                    Status = ResponseStatus.AggregateOrEventMissingIdInEventStore;

                if (status.Reason == Reason.ConcurrencyOlderVersionSendedWhenNewerIsInEventStore)
                    Status = ResponseStatus.ConcurrencyOlderVersionSendedWhenNewerIsInEventStore;

                Message = status.Message;
            }
            else
            {
                Success = true;
                Status = ResponseStatus.Success;
            }
        }

        protected BaseResponse(ExecutionStatus status, string message)
        {
            ValidationErrors = new List<string>();
            if (!status.Success)
            {
                Success = false;
                if (status.Where == WhereExecuted.DomainLogic)
                    Status = ResponseStatus.BussinesLogicError;
                if (status.Where == WhereExecuted.DataBase)
                    Status = ResponseStatus.DataBaseError;
                if (status.Where == WhereExecuted.DataBase
                    && status.Reason == Reason.ReturnedNull)
                    Status = ResponseStatus.NotFoundInDataBase;

                if (status.Reason == Reason.AggregateNotFoundInEventStore)
                    Status = ResponseStatus.AggregateNotFoundInEventStore;

                if (status.Reason == Reason.AggregateOrEventMissingIdInEventStore)
                    Status = ResponseStatus.AggregateOrEventMissingIdInEventStore;

                if (status.Reason == Reason.ConcurrencyOlderVersionSendedWhenNewerIsInEventStore)
                    Status = ResponseStatus.ConcurrencyOlderVersionSendedWhenNewerIsInEventStore;

                Message = message;
                Message += status.Message;
            }
            else
            {
                Success = true;
                Status = ResponseStatus.Success;
            }
        }

        protected BaseResponse(string message = null)
        {
            ValidationErrors = new List<string>();
            Success = true;
            Message = message;
            Status = ResponseStatus.Success;
        }

        protected BaseResponse(string message, bool success)
        {
            ValidationErrors = new List<string>();
            Success = success;
            Message = message;
        }

        protected BaseResponse(ResponseStatus status)
        {
            ValidationErrors = new List<string>();
            Success = status != ResponseStatus.Success;
            Status = status;
        }

        protected BaseResponse(ValidationResult validationResult)
        {
            ValidationErrors = new List<String>();
            Success = validationResult.Errors.Count < 0;
            foreach (var item in validationResult.Errors)
            {
                ValidationErrors.Add(item.ErrorMessage);
            }

            if (!Success)
                Status = ResponseStatus.ValidationError;
            else
                Status = ResponseStatus.Success;
        }
    }
}
