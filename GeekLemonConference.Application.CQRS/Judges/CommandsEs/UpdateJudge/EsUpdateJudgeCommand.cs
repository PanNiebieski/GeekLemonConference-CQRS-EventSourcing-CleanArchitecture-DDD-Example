using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Judges.Commands.UpdateJudge;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Judges.CommandsEs.UpdateJudge
{
    public class EsUpdateJudgeCommand : IRequest<EsUpdateJudgeCommandResponse>
    {
        public Guid UniqueId { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public NameDto Name { get; set; }
        public int CategoryId { get; set; }

        public int Version { get; set; }

        public CategoryDto Category
        {
            get
            {
                return new CategoryDto() { Id = CategoryId };
            }
        }

        //public List<EmailDto> Emails { get; set; }
        public DateTime Birthdate { get; set; }
        //public List<PhoneDto> Phones { get; set; }
    }
}
