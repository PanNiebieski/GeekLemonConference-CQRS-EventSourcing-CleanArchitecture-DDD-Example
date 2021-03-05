using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace GeekLemonConference.Application.CQRS.Categories.CommandsEs.CreateCategory
{

    public class ESCreateCategoryCommand : IRequest<ESCreateCategoryCommandResponse>
    {

        public string Name { get; set; }


        public string DisplayName { get; set; }


        public string WhatWeAreLookingFor { get; set; }

        public int Version { get; private set; }

        internal CategoryUniqueId UniqueId { get; private set; }

        public ESCreateCategoryCommand()
        {
            UniqueId = CategoryUniqueId.NewUniqueId();
            Version = 0;
        }
    }
}
