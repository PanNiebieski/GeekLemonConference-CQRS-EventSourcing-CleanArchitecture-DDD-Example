using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemon.Persistence.Dapper.SQLite.TempClasses
{
    public class CategoryTemp
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public int Version { get; set; }

        public string Name { get; init; }

        public string DisplayName { get; init; }

        public string WhatWeAreLookingFor { get; init; }
    }
}
