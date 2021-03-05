using GeekLemonConference.Application.CQRS.Mapper.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Dto
{
    public class JudgeDto
    {
        public Guid UniqueId { get; set; }
        public int Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public NameDto Name { get; set; }

        public CategoryDto Category { get; set; }

        public int Version { get; set; }

        public DateTime Birthdate { get; }

    }
}
