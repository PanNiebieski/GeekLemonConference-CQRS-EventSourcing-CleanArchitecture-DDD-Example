using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Judges.Commands.CreateJudge
{
    public class CreateJudgeCommand : IRequest<CreateJudgeCommandResponse>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public NameDto Name { get; set; }

        internal int Version { get; set; }

        internal JudgeUniqueId UniqueId { get; }

        public CreateJudgeCommand()
        {
            UniqueId = JudgeUniqueId.New();
            Version = 1;
        }

        public int CategoryId { get; set; }

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
