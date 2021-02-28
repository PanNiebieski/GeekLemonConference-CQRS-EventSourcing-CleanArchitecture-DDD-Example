using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.DomainEvents.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.EventSourcing.Aggregates
{
    public class CategoryAggregate : AggregateRoot
    {
        public string Name { get; private set; }

        public string DisplayName { get; private set; }

        public string WhatWeAreLookingFor { get; private set; }

        public CategoryUniqueId UniqueId { get; private set; }

        private void Apply(CategoryCreateEvent e)
        {
            Version = e.Version;
            Name = e.Name;
            DisplayName = e.DisplayName;
            UniqueId = e.UniqueId;
            WhatWeAreLookingFor = e.WhatWeAreLookingFor;
            this.Key = e.UniqueId.GetAggregateKey();
        }

        private void Apply(CategoryUpdateEvent e)
        {
            Version = e.Version++;
            Name = e.Name;
            DisplayName = e.DisplayName;
            UniqueId = e.UniqueId;
            WhatWeAreLookingFor = e.WhatWeAreLookingFor;
            this.Key = e.UniqueId.GetAggregateKey();
        }

        public CategoryAggregate(Category cc)
        {
            var c = new CategoryCreateEvent(cc.UniqueId, cc.Name,
                cc.DisplayName, cc.WhatWeAreLookingFor,
                cc.Version);

            ApplyChange(c);
        }

        public CategoryAggregate()
        {

        }

        public void Update(Category cc)
        {
            var c = new CategoryUpdateEvent(cc.UniqueId, cc.Name,
                cc.DisplayName, cc.WhatWeAreLookingFor,
                cc.Version);

            ApplyChange(c);
        }

    }
}
