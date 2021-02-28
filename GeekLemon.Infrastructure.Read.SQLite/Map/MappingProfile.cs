using AutoMapper;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemon.Persistence.Dapper.SQLite.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CallForSpeech, CallForSpeechTemp>().
                ConvertUsing(new CallForSpeechTempTypeConverter());
            CreateMap<CallForSpeechTemp, CallForSpeech>().
                ConvertUsing(new CallForSpeechTypeConverter());

            CreateMap<Category, CategoryTemp>();
            CreateMap<CategoryTemp, Category>();
            CreateMap<CategoryId, int>().ConstructUsing(k => k.Value);
            CreateMap<int, CategoryId>().ConstructUsing(k => new CategoryId(k));
            CreateMap<CategoryUniqueId, string>().ConstructUsing(k => k.Value.ToString());
            CreateMap<string, CategoryUniqueId>().
                ConstructUsing(k => new CategoryUniqueId(Guid.Parse(k)));

            CreateMap<JudgeTemp, Judge>().
                ConvertUsing(new JudgeTypeConverter());
            CreateMap<Judge, JudgeTemp>().
                ConvertUsing(new JudgeTempTypeConverter());
        }
    }
}
