using GeekLemonConference.Application.CQRS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Api.Models
{
    public class UpdateJudgeByUniqueId
    {
        public Guid UniqueId { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public NameDto Name { get; set; }

        public int CategoryId { get; set; }
        public int Version { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
