using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Judges;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges
{
    //public class JudgeUpdateDoer : IUpdateDoer<Judge>
    public class JudgeUpdateDoer : BeforeDoer, IJudgeUpdateDoer
    {



        private readonly IMapper _mapper;


        public JudgeUpdateDoer(IGeekLemonDBContext geekLemonContext,
            IMapper mapper)
        {
            _geekLemonContext = geekLemonContext;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus> Run(Judge entity, ByWhatId byWhatId)
        {
            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

            //Nie ma SQL INjection
            var q2 = @"UPDATE Judges 
                SET Login = @Login, Password = @Password, BirthDate=@BirthDate  
                ,Name_First = @Name_First, Name_Last = @Name_Last
                WHERE Id = @Id; ";
            var q = @"UPDATE Judges 
                SET Login = @Login, Password = @Password, BirthDate=@BirthDate  
                ,Name_First = @Name_First, Name_Last = @Name_Last
                WHERE UniqueId = @UniqueId; ";

            var temp = _mapper.Map<JudgeTemp>(entity);
            try
            {
                if (byWhatId == ByWhatId.CreatedId)
                    q = q2;

                var result = await connection.ExecuteAsync(q, temp);

                return ExecutionStatus.DbOk();
            }
            catch (Exception ex)
            {
                return ExecutionStatus.DbError(ex.Message);
            }
        }
    }
}
