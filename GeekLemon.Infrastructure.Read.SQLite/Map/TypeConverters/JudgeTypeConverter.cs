using AutoMapper;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Domain.ValueObjects.Security;
using System;
using System.Collections.Generic;

namespace GeekLemon.Persistence.Dapper.SQLite.Map
{
    public class JudgeTypeConverter : ITypeConverter<JudgeTemp, Judge>
    {
        public Judge Convert(JudgeTemp source, Judge destination, ResolutionContext context)
        {
            Category c = new Category(new CategoryId(source.CategoryId))
            {
                DisplayName = source.Category_DisplayName,
                Name = source.Category_Name,
                WhatWeAreLookingFor = source.Category_WhatWeAreLookingFor,
            };



            Login login = new Login(source.Login);
            Password password = new Password(source.Password);

            Name name = new Name(source.Name_First, source.Name_Last);

            Judge j = new Judge(source.Id, login, password, name, c);



            j.Birthdate = DateTime.Parse(source.BirthDate);



            if (source.UniqueId != "")
                j.UniqueId = new JudgeUniqueId(Guid.Parse(source.UniqueId));
            j.Version = source.Version;

            return j;
        }


    }
}
