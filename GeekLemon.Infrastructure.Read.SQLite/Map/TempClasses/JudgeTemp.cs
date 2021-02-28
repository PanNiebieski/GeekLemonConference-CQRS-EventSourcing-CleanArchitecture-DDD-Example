using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemon.Persistence.Dapper.SQLite.TempClasses
{
    public class JudgeTemp
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public int Version { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string BirthDate { get; set; }
        public string Name_First { get; set; }
        public string Name_Last { get; set; }
        public string Email_ForeConference { get; set; }
        public string Email_ForSpeakers { get; set; }
        public string Phone_ForSpekers { get; set; }
        public string Phone_ForConference { get; set; }
        public int CategoryId { get; set; }

        public string Category_DisplayName { get; set; }
        public string Category_WhatWeAreLookingFor { get; set; }
        public string Category_Name { get; set; }
    }
}
