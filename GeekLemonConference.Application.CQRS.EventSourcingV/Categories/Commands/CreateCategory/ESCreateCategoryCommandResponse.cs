using FluentValidation.Results;
using GeekLemonConference.Application.CQRS.Responses;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.EventVersion.Categories.Commands.CreateCategory
{
    public class ESCreateCategoryCommandResponse : BaseResponse
    {
        public CategoryIds CategoryIds { get; set; }

        public ESCreateCategoryCommandResponse(CategoryIds categoryIds)
             : base()
        {
            CategoryIds = categoryIds;
            CategoryIds.Status = IdsStatus.DudeYouCantReturnCreatedIdWhenYouAreEventSourcing;
        }

        public ESCreateCategoryCommandResponse() : base()
        { }

        public ESCreateCategoryCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public ESCreateCategoryCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public ESCreateCategoryCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public ESCreateCategoryCommandResponse(string message)
        : base(message)
        { }

        public ESCreateCategoryCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
