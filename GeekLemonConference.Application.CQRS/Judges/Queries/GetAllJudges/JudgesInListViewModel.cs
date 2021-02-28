using GeekLemonConference.Application.CQRS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.Judges.GetAllJudges
{
    public class JudgesInListViewModel
    {
        public int Id { get; set; }

        public NameDto Name { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDisplayName { get; set; }

        public int Version { get; set; }

        public Guid UniqueId { get; set; }
    }
}
