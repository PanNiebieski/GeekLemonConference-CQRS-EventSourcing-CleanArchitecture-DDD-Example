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

namespace GeekLemonConference.Application.CQRS.Judges.Commands.UpdateJudge
{
    public class UpdateJudgeCommand : IRequest<UpdateJudgeCommandResponse>
    {
        public Guid? UniqueId { get; set; }
        public int? Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public NameDto Name { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto Category
        {
            get
            {
                return new CategoryDto() { Id = CategoryId };
            }
        }

        public DateTime Birthdate { get; set; }
    }
}
