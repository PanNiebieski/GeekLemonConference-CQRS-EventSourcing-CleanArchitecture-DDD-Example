using GeekLemonConference.Application.CQRS.EventVersion.Categories.Commands.CreateCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.EventVersion.Categories.Commands
{
    public class ESCreateCategoryCommand : IRequest<ESCreateCategoryCommandResponse>
    {
    }
}
