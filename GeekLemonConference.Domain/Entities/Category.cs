using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GeekLemonConference.Domain.Entities
{
    public class Category : Entity<CategoryId, CategoryUniqueId>
    {
        public string Name { get; init; }

        public string DisplayName { get; init; }

        public string WhatWeAreLookingFor { get; init; }



        public Category(CategoryId Id, string name, string displayName,
            string whatWeAreLookingFor) : base()
        {
            this.Id = Id;
            Name = name;
            DisplayName = displayName;
            UniqueId = CategoryUniqueId.NewUniqueId();
            WhatWeAreLookingFor = whatWeAreLookingFor;
            //Version = 1;
        }

        public void InjectUniqueId(CategoryUniqueId uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        //    public Category(CategoryId Id, string name, string displayName,
        //string whatWeAreLookingFor, CategoryUniqueId uniqueId)
        //    {
        //        this.Id = Id;
        //        Name = name;
        //        DisplayName = displayName;
        //        UniqueId = uniqueId;
        //        WhatWeAreLookingFor = whatWeAreLookingFor;
        //        //Version = 1;
        //    }

        public Category(CategoryId Id)
        {
            this.Id = Id;
            UniqueId = CategoryUniqueId.NewUniqueId();
            //Version = 1;
        }

        public Category()
        {
            UniqueId = CategoryUniqueId.NewUniqueId();
            this.Id = CategoryId.Empty();
            //Version = 1;
        }

        public CategoryIds Ids()
        {
            if (this.Id != null && this.Id.Value != default)
                return new CategoryIds()
                {
                    UniqueId = this.UniqueId,
                    CreatedId = this.Id
                };
            else
                return new CategoryIds()
                {
                    UniqueId = this.UniqueId,
                    CreatedId = this.Id,
                    Status = IdsStatus.DudeYouCantReturnCreatedIdWhenYouAreEventSourcing

                };
        }
    }
}
