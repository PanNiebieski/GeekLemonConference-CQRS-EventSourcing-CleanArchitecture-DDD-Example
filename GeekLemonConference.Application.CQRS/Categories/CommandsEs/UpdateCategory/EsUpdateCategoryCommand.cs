using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Categories.CommandsEs.UpdateCategory
{
    public class EsUpdateCategoryCommand
        : IRequest<EsUpdateCategoryCommandResponse>
    {
        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string WhatWeAreLookingFor { get; set; }

        public int Version { get; set; }
    }
}
