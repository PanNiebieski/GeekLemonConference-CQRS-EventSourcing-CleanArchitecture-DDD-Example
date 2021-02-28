using GeekLemonConference.Application.CQRS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Api.Models
{
    public class UpdateJudgeById
    {
        public int Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public NameDto Name { get; set; }

        public int CategoryId { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
