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
    public class MappingDtos : Profile
    {
        public MappingDtos()
        {


            CreateMap<CallForSpeechIds, IdsDto>()
                .ForMember(a => a.Status, b => b.MapFrom(c => c.Status.ToString()));
            CreateMap<CategoryIds, IdsDto>()
               .ForMember(a => a.Status, b => b.MapFrom(c => c.Status.ToString()));
            CreateMap<JudgeIds, IdsDto>()
               .ForMember(a => a.Status, b => b.MapFrom(c => c.Status.ToString()));

            CreateMap<Judge, JudgeDto>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));
            CreateMap<JudgeDto, Judge>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new JudgeId(k.Id)));

            CreateMap<Speaker, SpeakerDto>().ReverseMap();
            CreateMap<Speech, SpeechDto>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<NameDto, Name>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<RegistrationDto, Registration>().ReverseMap();
            CreateMap<SpeakerWebsitesDto, SpeakerWebsites>().ReverseMap();

            CreateMap<ScoreDto, CallForSpeechScoringResult>().
                ForMember(s => s.Score, o => o.MapFrom(k =>
                k.Score.ParseEnum<CallForSpeechMachineScore>()));

            CreateMap<CallForSpeechScoringResult, ScoreDto>().
                ForMember(s => s.Score, o => o.MapFrom(k => k.Score.ToString()));

            CreateMap<DecisionDto, Decision>().ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));
            CreateMap<CategoryDto, Category>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new CategoryId(k.Id)));

        }
    }
}
