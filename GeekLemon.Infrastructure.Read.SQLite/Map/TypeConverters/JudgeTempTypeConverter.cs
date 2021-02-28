using AutoMapper;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects;
using System;

namespace GeekLemon.Persistence.Dapper.SQLite.Map
{
    public class JudgeTempTypeConverter : ITypeConverter<Judge, JudgeTemp>
    {
        private class ValuesFromJugde
        {
            public string Birthdate { get; set; }
            public string EmailForeConference { get; set; }
            public string EmailForSpeakers { get; set; }
            public string PhoneForSpekers { get; set; }
            public string PhoneForConference { get; set; }
        }

        public JudgeTemp Convert(Judge source, JudgeTemp destination, ResolutionContext context)
        {
            var birthdate = source.Birthdate.ToString("dd-MM-yyyy");
            ValuesFromJugde v = new ValuesFromJugde();

            //foreach (var item in source.Emails)
            //{
            //    if (item.Type == EmailType.FORCONFERENCE)
            //        v.EmailForeConference = item.Value;

            //    if (item.Type == EmailType.FORSPEAKERS)
            //        v.EmailForSpeakers = item.Value;
            //}

            //foreach (var item in source.Phones)
            //{
            //    if (item.Type == PhoneType.FORCONFERENCE)
            //        v.PhoneForConference = "(" + item.AreaCode + ")" + item.Number;

            //    if (item.Type == PhoneType.FORSPEAKERS)
            //        v.PhoneForSpekers = "(" + item.AreaCode + ")" + item.Number;
            //}

            int id = 0;

            if (source.Id != null)
                id = source.Id.Value;

            string uniqueId = "";

            if (source.UniqueId != null)
                uniqueId = source.UniqueId.Value.ToString();

            JudgeTemp j = new JudgeTemp()
            {
                Id = id,
                BirthDate = birthdate,
                CategoryId = source.Category.Id.Value,
                Email_ForeConference = v.EmailForeConference,
                Email_ForSpeakers = v.EmailForSpeakers,
                Phone_ForConference = v.PhoneForConference,
                Phone_ForSpekers = v.PhoneForSpekers,
                Login = source.Login,
                Name_First = source.Name.First,
                Name_Last = source.Name.Last,
                Password = source.Password,
                Version = source.Version,
                UniqueId = uniqueId,
            };

            return j;
        }
    }
}
