using FluentValidation.Results;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Categories.Queries.GetCategory
{
    public class GetCategoryQueryResponse : BaseResponse
    {
        public CategoryDto Category { get; }

        public GetCategoryQueryResponse(CategoryDto cat)
            : base()
        {
            Category = cat;
        }

        public GetCategoryQueryResponse() : base()
        { }

        public GetCategoryQueryResponse(ExecutionStatus status)
            : base(status)
        {

        }



        public GetCategoryQueryResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public GetCategoryQueryResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public GetCategoryQueryResponse(string message)
        : base(message)
        { }

        public GetCategoryQueryResponse(string message, bool success)
            : base(message, success)
        { }
    }

}
