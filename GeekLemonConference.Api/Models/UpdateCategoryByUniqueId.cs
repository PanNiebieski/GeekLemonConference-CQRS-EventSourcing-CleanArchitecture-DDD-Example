using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Api.Models
{
    public class UpdateCategoryByUniqueId
    {
        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string WhatWeAreLookingFor { get; set; }
    }
}
