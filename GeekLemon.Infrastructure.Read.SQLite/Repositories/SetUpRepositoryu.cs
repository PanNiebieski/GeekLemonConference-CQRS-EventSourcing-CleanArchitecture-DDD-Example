using Dapper;
using GeekLemonConference.Application.Contracts.Persistence;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemon.Persistence.Dapper.SQLite.Repositories
{
    public class SetUpRepository : ISetUpRepository
    {
        private IGeekLemonDBContext _geekLemonContext;

        public SetUpRepository(IGeekLemonDBContext geekLemonContext)
        {
            _geekLemonContext = geekLemonContext;
        }

        public void SetupCategories()
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            var table = connection
                .Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Categories';");

            var tableName = table.FirstOrDefault();

            if (!string.IsNullOrEmpty(tableName) && tableName == "Categories")
                return;

            connection.Execute(@"CREATE TABLE ""Categories""(
                ""ID""   INTEGER NOT NULL UNIQUE,
                ""DisplayName""   TEXT,
                ""Name""  TEXT,
                ""WhatWeAreLookingFor""   TEXT,
                PRIMARY KEY(""ID"" AUTOINCREMENT)))");
        }

        public void SetupJudges()
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            var table = connection
                .Query<string>("SELECT name FROM sqlite_master WHERE type='Judges' AND name = 'Judges';");

            var tableName = table.FirstOrDefault();

            if (!string.IsNullOrEmpty(tableName) && tableName == "Judges")
                return;

            connection.Execute(@"CREATE TABLE ""Judges"" (
                ""ID""    INTEGER NOT NULL UNIQUE,
                ""Login"" TEXT NOT NULL,
                ""Password""  TEXT NOT NULL,
                ""BirthDate"" TEXT NOT NULL,
                ""Name_First""    TEXT,
                ""Name_Last"" TEXT,
                ""Email_ForeConference""  TEXT,
                ""Email_ForSpeakers"" TEXT,
                ""Phone_ForSpekers"" TEXT,
                ""Phone_ForConference""   TEXT,
                ""CategoryID""    INTEGER NOT NULL,
                CONSTRAINT ""PK_Judges_KEY"" PRIMARY KEY(""ID"" AUTOINCREMENT),
                CONSTRAINT ""FK_CategoryId_Judges"" FOREIGN KEY(""CategoryID"") REFERENCES Categories); ");
        }

        public void SetupCallForSpeakes()
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            var table = connection
                .Query<string>("SELECT name FROM sqlite_master WHERE type='CallForSpeakes' AND name = 'CallForSpeakes';");

            var tableName = table.FirstOrDefault();

            if (!string.IsNullOrEmpty(tableName) && tableName == "CallForSpeakes")
                return;

            connection.Execute(@"CREATE TABLE ""CallForSpeakes"" (
                ""Id""    INTEGER NOT NULL UNIQUE,
                ""Number""    TEXT NOT NULL,
                ""Status""    INTEGER NOT NULL,
                ""PreliminaryDecision_DecisionBy""    NUMERIC,
                ""PreliminaryDecision_Date""  TEXT,
                ""FinalDecision_DecisionBy""  INTEGER,
                ""FinalDecision_Date""    TEXT,
                ""Speaker_Name_First""    TEXT NOT NULL,
                ""Speaker_Name_Last"" TEXT NOT NULL,
                ""Speaker_Adress_Country""    TEXT NOT NULL,
                ""Speaker_Adress_ZipCode""    TEXT NOT NULL,
                ""Speaker_Adress_City""   TEXT NOT NULL,
                ""Speaker_Adress_Street"" TEXT NOT NULL,
                ""Speaker_Websites_Facebook"" TEXT,
                ""Speaker_Websites_Twitter""  TEXT,
                ""Speaker_Websites_Instagram""    TEXT,
                ""Speaker_Websites_LinkedIn"" TEXT,
                ""Speaker_Websites_TikTok""   TEXT,
                ""Speaker_Websites_Youtube""  REAL,
                ""Speaker_Websites_FanPageOnFacebook""    REAL,
                ""Speaker_Websites_GitHub""   TEXT,
                ""Speaker_Websites_Blog"" TEXT,
                ""Speaker_BIO""   TEXT NOT NULL,
                ""Speaker_Phone"" TEXT,
                ""Speaker_Email"" TEXT NOT NULL,
                ""Speech_Title""  TEXT NOT NULL,
                ""Speech_Description""    TEXT NOT NULL,
                ""Speech_Tags""   TEXT NOT NULL,
                ""Speech_ForWhichAudience""   TEXT NOT NULL,
                ""Speech_TechnologyOrBussinessStory"" INTEGER NOT NULL,
                ""Registration_RegistrationDate"" TEXT,
                ""CategoryId""    INTEGER NOT NULL,
                ""Score_Score""   INTEGER,
                ""Score_RejectExplanation""  TEXT,
                ""Score_WarringExplanation""  TEXT,
                CONSTRAINT ""FK_JudgeID_CallForSpeech_FinalDecision_DecysionBy"" FOREIGN KEY(""FinalDecision_DecisionBy"") REFERENCES ""Judges"",
                CONSTRAINT ""FK_JudgeID_CallForSpeech_PreminaryDecision_DecysionBy"" FOREIGN KEY(""PreliminaryDecision_DecisionBy"") REFERENCES ""Judges"",
                CONSTRAINT ""FK_CategoryID_CallForSpeakes"" FOREIGN KEY(""CategoryId"") REFERENCES ""Categories"",
                CONSTRAINT ""PK_CallForSpeakes_PK"" PRIMARY KEY(""Id"" AUTOINCREMENT))");
        }
    }
}
