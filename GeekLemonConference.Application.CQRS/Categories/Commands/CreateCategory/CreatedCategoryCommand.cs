using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Commands.Categories.CreateCategory
{
    public class CreatedCategoryCommand : IRequest<CreatedCategoryCommandResponse>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string WhatWeAreLookingFor { get; set; }

        internal int Version { get; }

        internal CategoryUniqueId UniqueId { get; }

        public CreatedCategoryCommand()
        {
            UniqueId = CategoryUniqueId.NewUniqueId();
            Version = 0;
        }
    }
}
