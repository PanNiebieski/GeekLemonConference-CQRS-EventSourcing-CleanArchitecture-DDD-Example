using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.DomainEvents.Ddd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.DomainEvents.Categories
{
    public class CategoryUpdateEvent : DomainEvent
    {
        public CategoryUniqueId UniqueId { get; init; }

        public string Name { get; init; }

        public string DisplayName { get; init; }

        public string WhatWeAreLookingFor { get; init; }

        public CategoryUpdateEvent(CategoryUniqueId uniqueId,
            string Name, string displayName, string whatWeAreLookingFor,
            int version)
            : base(version)
        {

            UniqueId = uniqueId;
            Key = this.UniqueId.GetAggregateKey();
            DisplayName = displayName;
            WhatWeAreLookingFor = whatWeAreLookingFor;
        }
    }
}
