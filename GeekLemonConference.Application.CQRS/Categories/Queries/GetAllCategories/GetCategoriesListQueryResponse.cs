using FluentValidation.Results;
using GeekLemonConference.Application.CQRS.Queries.Categories.GetAllCategories;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Categories.Queries.GetAllCategories
{
    public class GetCategoriesListQueryResponse : BaseResponse
    {
        public List<CategoryInListViewModel> List { get; }

        public GetCategoriesListQueryResponse(List<CategoryInListViewModel> list) : base()
        {
            List = list;
        }

        public GetCategoriesListQueryResponse() : base()
        { }

        public GetCategoriesListQueryResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public GetCategoriesListQueryResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public GetCategoriesListQueryResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public GetCategoriesListQueryResponse(string message)
        : base(message)
        { }

        public GetCategoriesListQueryResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
