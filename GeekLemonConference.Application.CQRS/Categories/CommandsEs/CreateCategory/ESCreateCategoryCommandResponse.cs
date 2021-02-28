using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Categories.CommandsEs.CreateCategory
{
    public class ESCreateCategoryCommandResponse : BaseResponse
    {
        public IdsDto CategoryIds { get; set; }

        public ESCreateCategoryCommandResponse(IdsDto Ids)
             : base()
        {
            CategoryIds = Ids;
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
