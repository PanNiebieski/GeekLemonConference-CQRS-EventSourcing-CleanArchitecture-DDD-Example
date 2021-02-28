using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge
{
    public class JudgeViewModel
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }

        public string Login { get; set; }

        public NameDto Name { get; set; }

        public CategoryDto Category { get; set; }

        public List<EmailDto> Emails { get; set; }
        public DateTime Birthdate { get; set; }
        public List<PhoneDto> Phones { get; set; }

        public int Version { get; set; }


    }
}
