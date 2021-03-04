using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.Categories.GetAllCategories
{
    public class CategoryInListViewModel
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }

        public string Name { get; init; }

        public string DisplayName { get; init; }

        public string WhatWeAreLookingFor { get; init; }
    }
}
