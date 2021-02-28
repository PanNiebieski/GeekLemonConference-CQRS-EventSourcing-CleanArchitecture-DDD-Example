//using AutoMapper;
//using GeekLemonConference.Application.EventSourcing.Aggregates;
//using GeekLemonConference.Domain.DomainEvents;
//using GeekLemonConference.Domain.Entities;
//using GeekLemonConference.Domain.Entity;
//using GeekLemonConference.Domain.ValueObjects;
//using GeekLemonConference.Domain.ValueObjects.Ids;
//using GeekLemonConference.Domain.ValueObjects.Security;
//using GeekLemonConference.DomainEvents.Categories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace GeekLemonConference.Application.CQRS.Mapper
//{

//    public class MappingProfileEvent : Profile
//    {
//        public MappingProfileEvent()
//        {
//            CreateMap<CategoryUpdateEvent, Category>();
//            CreateMap<Category, CategoryUpdateEvent>();
//            CreateMap<CategoryCreateEvent, Category>();
//            CreateMap<Category, CategoryCreateEvent>();
//        }
//    }
//}
