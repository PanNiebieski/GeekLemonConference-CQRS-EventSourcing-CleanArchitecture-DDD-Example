using AutoMapper;
using GeekLemonConference.Application.CQRS.CallForSpeeches.Command.SubmitCallForSpeech;
using GeekLemonConference.Application.CQRS.CallForSpeeches.CommandsES.SubmitCallForSpeech;
using GeekLemonConference.Application.CQRS.Categories.Commands.UpdateCategory;
using GeekLemonConference.Application.CQRS.Categories.CommandsEs.CreateCategory;
using GeekLemonConference.Application.CQRS.Categories.CommandsEs.UpdateCategory;
using GeekLemonConference.Application.CQRS.Commands.Categories.CreateCategory;
using GeekLemonConference.Application.CQRS.Dto;
using GeekLemonConference.Application.CQRS.Judges.Commands.CreateJudge;
using GeekLemonConference.Application.CQRS.Judges.Commands.UpdateJudge;
using GeekLemonConference.Application.CQRS.Judges.CommandsEs.CreateJudge;
using GeekLemonConference.Application.CQRS.Judges.CommandsEs.UpdateJudge;
using GeekLemonConference.Application.CQRS.Judges.Queries.GetJudge;
using GeekLemonConference.Application.CQRS.Mapper.Dto;
using GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetAllCallForSpeeches;
using GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetCallForSpeech;
using GeekLemonConference.Application.CQRS.Queries.Categories.GetAllCategories;
using GeekLemonConference.Application.CQRS.Queries.Judges.GetAllJudges;
using GeekLemonConference.Application.EventSourcing.Aggregates;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Domain.ValueObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekLemonConference.Application.CQRS.Mapper
{
    public class MappingIds : Profile
    {
        public MappingIds()
        {
            CreateMap<int, JudgeId>().ConstructUsing(c => new JudgeId(c));
            CreateMap<int, CallForSpeechId>().ConstructUsing(c => new CallForSpeechId(c));
            CreateMap<int, CategoryId>().ConstructUsing(c => new CategoryId(c));

            CreateMap<JudgeId, int>().ConstructUsing(c => c.Value);
            CreateMap<CallForSpeechId, int>().ConstructUsing(c => c.Value);
            CreateMap<CategoryId, int>().ConstructUsing(c => c.Value);

            CreateMap<JudgeUniqueId, Guid>().ConstructUsing(c => c.Value);
            CreateMap<CategoryUniqueId, Guid>().ConstructUsing(c => c.Value);
            CreateMap<CallForSpeechUniqueId, Guid>().ConstructUsing(c => c.Value);

            CreateMap<Guid, CategoryUniqueId>().ConstructUsing(c =>
                new CategoryUniqueId(c));
            CreateMap<Guid, JudgeUniqueId>().ConstructUsing(c =>
                new JudgeUniqueId(c));
            CreateMap<Guid, CallForSpeechUniqueId>().ConstructUsing(c =>
                new CallForSpeechUniqueId(c));

            CreateMap<String, CategoryUniqueId>().ConstructUsing(c =>
            new CategoryUniqueId(Guid.Parse(c)));
            CreateMap<String, JudgeUniqueId>().ConstructUsing(c =>
            new JudgeUniqueId(Guid.Parse(c)));
            CreateMap<String, CallForSpeechUniqueId>().ConstructUsing(c =>
            new CallForSpeechUniqueId(Guid.Parse(c)));
        }
    }
}
