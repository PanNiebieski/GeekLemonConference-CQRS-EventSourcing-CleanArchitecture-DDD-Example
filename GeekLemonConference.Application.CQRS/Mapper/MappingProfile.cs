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






    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatedCategoryCommand, Category>();
            CreateMap<EsUpdateCategoryCommand, Category>();

            CreateMap<UpdateCategoryCommand, CategoryDto>();
            CreateMap<ESCreateCategoryCommand, Category>();

            CreateMap<CreateJudgeCommand, Judge>();
            CreateMap<UpdateJudgeCommand, JudgeDto>();
            CreateMap<JudgeDto, Judge>();

            CreateMap<EsCreateJudgeCommand, Judge>();
            CreateMap<EsUpdateJudgeCommand, JudgeDto>();

            CreateMap<SubmitCallForSpeechCommand, CallForSpeech>();
            CreateMap<EsSubmitCallForSpeechCommand, CallForSpeech>();

            CreateMap<Category, CategoryInListViewModel>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));

            CreateMap<Judge, JudgesInListViewModel>()
                  .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));

            CreateMap<JudgesInListViewModel, Judge>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new JudgeId(k.Id)));

            CreateMap<JudgeViewModel, Judge>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new JudgeId(k.Id)));

            CreateMap<Judge, JudgeViewModel>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));

            CreateMap<UpdateJudgeCommand, Judge>();

            CreateMap<CallForSpeech, CallForSpeechViewModel>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value))
                .ForMember(s => s.Status, o => o.MapFrom(k => k.Status.ToString()));

            CreateMap<CallForSpeechViewModel, CallForSpeech>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new CallForSpeechId(k.Id)))
                .ForMember(s => s.Status, o => o.MapFrom(k =>
                k.Status.ParseEnum<CallForSpeechStatus>()));


            CreateMap<CallForSpeech, CallForSpeechInListViewModel>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value))
                .ForMember(s => s.Status, o => o.MapFrom(k => k.Status.ToString()));

            CreateMap<CallForSpeechInListViewModel, CallForSpeech>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new CallForSpeechId(k.Id)))
                .ForMember(s => s.Status, o => o.MapFrom(k =>
                k.Status.ParseEnum<CallForSpeechStatus>()));


            CreateMap<CallForSpeechAggregate, CallForSpeech>();
        }
    }
}
