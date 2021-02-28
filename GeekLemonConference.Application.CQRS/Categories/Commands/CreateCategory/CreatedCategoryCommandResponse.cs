using FluentValidation.Results;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekLemonConference.Application.CQRS.Mapper.Dto;

namespace GeekLemonConference.Application.CQRS.Commands.Categories.CreateCategory
{
    public class CreatedCategoryCommandResponse : BaseResponse
    {
        public IdsDto CategoryIds { get; set; }

        public CreatedCategoryCommandResponse(IdsDto categoryIds)
             : base()
        {
            CategoryIds = categoryIds;
        }

        public CreatedCategoryCommandResponse() : base()
        { }

        public CreatedCategoryCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public CreatedCategoryCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public CreatedCategoryCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public CreatedCategoryCommandResponse(string message)
        : base(message)
        { }

        public CreatedCategoryCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
