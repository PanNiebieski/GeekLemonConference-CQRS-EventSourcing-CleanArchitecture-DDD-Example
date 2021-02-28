using FluentValidation.Results;
using GeekLemonConference.Application.Common;
using GeekLemonConference.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Categories.CommandsEs.UpdateCategory
{
    public class EsUpdateCategoryCommandResponse : BaseResponse
    {

        public EsUpdateCategoryCommandResponse() : base()
        { }

        public EsUpdateCategoryCommandResponse(ExecutionStatus status)
            : base(status)
        {

        }

        public EsUpdateCategoryCommandResponse(ExecutionStatus status, string message)
    : base(status, message)
        {

        }


        public EsUpdateCategoryCommandResponse(ValidationResult validationResult)
            : base(validationResult)
        { }

        public EsUpdateCategoryCommandResponse(string message)
        : base(message)
        { }

        public EsUpdateCategoryCommandResponse(string message, bool success)
            : base(message, success)
        { }
    }
}
