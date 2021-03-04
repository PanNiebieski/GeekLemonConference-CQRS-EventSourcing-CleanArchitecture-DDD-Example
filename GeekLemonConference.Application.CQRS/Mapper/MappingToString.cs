using AutoMapper;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Mapper
{
    public class MappingToString : Profile
    {
        public MappingToString()
        {
            //Judge
            CreateMap<string, Login>().ConstructUsing(c => new Login(c));
            CreateMap<string, Password>().ConstructUsing(c => new Password(c));

            CreateMap<CallForSpeechNumber, string>().ConstructUsing(o => o.Number);
            CreateMap<string, CallForSpeechNumber>().ConstructUsing(o => new CallForSpeechNumber(o));

            CreateMap<ForWhichAudience, string>().ConvertUsing(c => c.ToString());
            CreateMap<TechnologyOrBussinessStory, string>().ConvertUsing(c => c.ToString());
            CreateMap<string, ForWhichAudience>()
                .ConvertUsing(c => c.ParseEnum<ForWhichAudience>());
            CreateMap<string, TechnologyOrBussinessStory>()
                .ConvertUsing(c => c.ParseEnum<TechnologyOrBussinessStory>());
        }
    }

    public static class Helper
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
