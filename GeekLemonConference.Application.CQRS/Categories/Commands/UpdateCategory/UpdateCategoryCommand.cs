using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<UpdateCategoryCommandResponse>
    {
        public Guid? UniqueId { get; set; }
        public int? Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string WhatWeAreLookingFor { get; set; }
    }


}
