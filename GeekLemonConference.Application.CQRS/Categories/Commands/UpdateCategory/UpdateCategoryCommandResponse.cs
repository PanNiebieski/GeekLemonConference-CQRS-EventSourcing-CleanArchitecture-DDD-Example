using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandResponse : BaseResponse
    {


        public UpdateCategoryCommandResponse() : base()
        { }

        public UpdateCategoryCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public UpdateCategoryCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public UpdateCategoryCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public UpdateCategoryCommandResponse(string message)
        : base(message)
        { }

        public UpdateCategoryCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
