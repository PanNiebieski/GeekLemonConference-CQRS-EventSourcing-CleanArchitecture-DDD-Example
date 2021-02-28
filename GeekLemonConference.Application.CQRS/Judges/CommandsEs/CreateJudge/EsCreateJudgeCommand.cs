using GeekLemonConference.Application.CQRS.Categories.CommandsEs.CreateCategory;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Judges.CommandsEs.CreateJudge
{
    public class EsCreateJudgeCommand : IRequest<EsCreateJudgeCommandResponse>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public NameDto Name { get; set; }
        public DateTime Birthdate { get; set; }


        public int CategoryId { get; set; }
        public CategoryDto Category
        {
            get
            {
                return new CategoryDto() { Id = CategoryId };
            }
        }
        internal int Version { get; set; }
        internal JudgeUniqueId UniqueId { get; }

        public EsCreateJudgeCommand()
        {
            UniqueId = JudgeUniqueId.New();
            Version = 0;
        }

    }
}
