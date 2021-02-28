using AutoMapper;

using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Domain.ValueObjects.Security;
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
            CreateMap<CategoryCreateEvent, Category>();
            CreateMap<CategoryUpdateEvent, Category>();
        }
    }
}
