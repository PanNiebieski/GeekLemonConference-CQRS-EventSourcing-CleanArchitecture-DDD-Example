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
    public static class Helper
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }





    public class MappingProfile : Profile
    {
        public MappingProfile()
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



            CreateMap<CallForSpeechIds, IdsDto>()
                .ForMember(a => a.Status, b => b.MapFrom(c => c.Status.ToString()));
            CreateMap<CategoryIds, IdsDto>()
               .ForMember(a => a.Status, b => b.MapFrom(c => c.Status.ToString()));
            CreateMap<JudgeIds, IdsDto>()
               .ForMember(a => a.Status, b => b.MapFrom(c => c.Status.ToString()));



            //Judge
            CreateMap<string, Login>().ConstructUsing(c => new Login(c));
            CreateMap<string, Password>().ConstructUsing(c => new Password(c));


            CreateMap<Judge, JudgeDto>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));
            CreateMap<JudgeDto, Judge>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new JudgeId(k.Id)));





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
            CreateMap<NameDto, Name>().ReverseMap();
            CreateMap<DecisionDto, Decision>().ReverseMap();
            CreateMap<SpeakerWebsitesDto, SpeakerWebsites>().ReverseMap();

            CreateMap<RegistrationDto, Registration>().ReverseMap();
            CreateMap<SpeakerWebsitesDto, SpeakerWebsites>().ReverseMap();

            CreateMap<Speaker, SpeakerDto>().ReverseMap();
            CreateMap<Speech, SpeechDto>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<CallForSpeechNumber, string>().ConstructUsing(o => o.Number);
            CreateMap<string, CallForSpeechNumber>().ConstructUsing(o => new CallForSpeechNumber(o));

            CreateMap<Contact, ContactDto>().ReverseMap();

            CreateMap<Category, CategoryInListViewModel>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));

            CreateMap<Category, CategoryDto>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));
            CreateMap<CategoryDto, Category>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new CategoryId(k.Id)));





            CreateMap<ScoreDto, CallForSpeechScoringResult>().
                ForMember(s => s.Score, o => o.MapFrom(k =>
                k.Score.ParseEnum<CallForSpeechMachineScore>()));

            CreateMap<CallForSpeechScoringResult, ScoreDto>().
                ForMember(s => s.Score, o => o.MapFrom(k => k.Score.ToString()));


            CreateMap<Judge, JudgesInListViewModel>()
                  .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));
            //.ForMember(s => s.EmailForConference,
            //o => o.MapFrom
            //(k => k.Emails.FirstOrDefault(e => e.Type == EmailType.FORCONFERENCE)));

            CreateMap<JudgesInListViewModel, Judge>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new JudgeId(k.Id)));

            CreateMap<JudgeViewModel, Judge>()
    .ForMember(s => s.Id, o => o.MapFrom(k => new JudgeId(k.Id)));

            CreateMap<Judge, JudgeViewModel>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value));







            CreateMap<NameDto, Name>().ReverseMap();



            CreateMap<UpdateJudgeCommand, Judge>();
            //.ForMember(s => s.Id, o => o.MapFrom(k => new JudgeId(k.UniqueId.)));

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


            CreateMap<ForWhichAudience, string>().ConvertUsing(c => c.ToString());
            CreateMap<TechnologyOrBussinessStory, string>().ConvertUsing(c => c.ToString());
            CreateMap<string, ForWhichAudience>()
                .ConvertUsing(c => c.ParseEnum<ForWhichAudience>());
            CreateMap<string, TechnologyOrBussinessStory>()
                .ConvertUsing(c => c.ParseEnum<TechnologyOrBussinessStory>());


            CreateMap<CallForSpeechAggregate, CallForSpeech>();

        }
    }
}
