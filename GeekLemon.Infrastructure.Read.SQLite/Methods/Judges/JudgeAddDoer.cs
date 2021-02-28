using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges
{
    //public class JudgeAddDoer : IAddDoer<Judge, JudgeId, JudgeUniqueId>
    public class JudgeAddDoer : BeforeDoer, IJudgeAddDoer
    {
        private readonly IMapper _mapper;


        public JudgeAddDoer(IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus<JudgeIds>> Run(Judge entity)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            var q = @"INSERT INTO Judges 
                (Login, Password, BirthDate, Name_First,
                 Name_Last,CategoryID,
                UniqueId, Version )
                VALUES(@Login, @Password, @BirthDate, 
                @Name_First, @Name_Last, @CategoryId, @UniqueId,@Version);

                SELECT seq From sqlite_sequence Where Name='Judges'";

            //var v = GetValuesFromJugde(entity);

            //var result = await connection.QueryAsync<int>(q,
            //     new
            //     {
            //         @Id = entity.Id,
            //         @Login = entity.Login,
            //         @Password = entity.Password,
            //         @BirthDate = v.Birthdate,
            //         @Name_First = entity.Name.First,
            //         @Name_Last = entity.Name.Last,
            //         @Email_ForeConference = v.EmailForeConference,
            //         @Email_ForSpeakers = v.EmailForSpeakers,
            //         @Phone_ForSpekers = v.PhoneForSpekers,
            //         @Phone_ForConference = v.PhoneForConference,
            //         @CategoryID = entity.Category.Id.Value
            //     });

            var temp = _mapper.Map<JudgeTemp>(entity);

            try
            {
                var result = await connection.QueryAsync<int>(q, temp);
                int createdId = result.FirstOrDefault();

                JudgeIds ids = new JudgeIds()
                {
                    CreatedId = new JudgeId(createdId),
                    UniqueId = entity.UniqueId
                };

                return ExecutionStatus
                    <JudgeIds>.
                    DbIfDefaultThenError(ids);
            }
            catch (Exception ex)
            {
                return ExecutionStatus<JudgeIds>
                    .DbError(ex.Message);
            }
        }
    }
}
