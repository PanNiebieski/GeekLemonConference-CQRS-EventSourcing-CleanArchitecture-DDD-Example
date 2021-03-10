using AutoMapper;
using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Domain.ValueObjects.Security;
using GeekLemonConference.DomainEvents.CallForSpeeches;
using GeekLemonConference.DomainEvents.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Mapper
{


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryCreatedEvent, Category>();
            CreateMap<CategoryUpdatedEvent, Category>();

            CreateMap<JudgeCreatedEvent, Judge>();
            CreateMap<JudgeUpdatedEvent, Judge>();
            CreateMap<JudgeDeletedEvent, Judge>();

            CreateMap<CallForSpeechAcceptedEvent, CallForSpeech>();
            CreateMap<CallForSpeechEvaulatedEvent, CallForSpeech>();
            CreateMap<CallForSpeechPreliminaryAcceptedEvent, CallForSpeech>();
            CreateMap<CallForSpeechRejectedEvent, CallForSpeech>();
            CreateMap<CallForSpeechSubmitedEvent, CallForSpeech>();

        }
    }
}
