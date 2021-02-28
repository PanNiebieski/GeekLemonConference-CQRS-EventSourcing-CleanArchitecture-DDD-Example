//using AutoMapper;
//using Dapper;
//using GeekLemon.Persistence.Dapper.SQLite;
//using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
//using GeekLemonConference.Application.Contracts.Repository;
//using GeekLemonConference.Domain;
//using GeekLemonConference.Domain.Entities;
//using GeekLemonConference.Domain.ValueObjects;
//using GeekLemonConference.Domain.ValueObjects.Ids;
//using Microsoft.Data.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GeekLemon.Infrastructure.Read.SQLite
//{
//    public class JugdeRepoisotry : IJudgeRepository
//    {
//        private IGeekLemonDBContext _geekLemonContext;

//        private readonly IMapper _mapper;


//        public JugdeRepoisotry(IGeekLemonDBContext geekLemonContext,
//            IMapper mapper)
//        {
//            _geekLemonContext = geekLemonContext;
//            _mapper = mapper;
//        }

//        public async Task<ExecutionStatus<int>> AddAsync(Judge entity)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            var q = @"INSERT INTO Judges 
//                (Login, Password, BirthDate, Name_First,
//                 Name_Last, Email_ForeConference, Email_ForSpeakers,
//                Phone_ForSpekers, Phone_ForConference,CategoryID,
//                UniqueId, Version )
//                VALUES(@Login, @Password, @BirthDate, 
//                @Name_First, @Name_Last, @Email_ForeConference, @Email_ForSpeakers,
//                @Phone_ForSpekers, @Phone_ForConference, @CategoryId, @UniqueId,@Version);

//                SELECT last_insert_rowid();";

//            //var v = GetValuesFromJugde(entity);

//            //var result = await connection.QueryAsync<int>(q,
//            //     new
//            //     {
//            //         @Id = entity.Id,
//            //         @Login = entity.Login,
//            //         @Password = entity.Password,
//            //         @BirthDate = v.Birthdate,
//            //         @Name_First = entity.Name.First,
//            //         @Name_Last = entity.Name.Last,
//            //         @Email_ForeConference = v.EmailForeConference,
//            //         @Email_ForSpeakers = v.EmailForSpeakers,
//            //         @Phone_ForSpekers = v.PhoneForSpekers,
//            //         @Phone_ForConference = v.PhoneForConference,
//            //         @CategoryID = entity.Category.Id.Value
//            //     });

//            var temp = _mapper.Map<JudgeTemp>(entity);

//            try
//            {
//                var result = await connection.QueryAsync<int>(q, temp);
//                int id = result.FirstOrDefault();

//                return ExecutionStatus<int>.DbIfDefaultThenError(id);
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus<int>.DbError(ex.Message);
//            }
//        }

//        public async Task<ExecutionStatus> DeleteAsync(JudgeId entity)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            var q = "DELETE FROM Judges WHERE Id=@Id;";

//            try
//            {
//                var result = await connection.QueryAsync<int>(q,
//                 new
//                 {
//                     @Id = entity.Value
//                 });

//                return ExecutionStatus.DbOk();
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus.DbError(ex.Message);
//            }

//        }

//        public async Task<ExecutionStatus<IReadOnlyList<Judge>>> GetAllAsync()
//        {
//            using var connection = new SqliteConnection
//                (_geekLemonContext.ConnectionString);

//            var q = @$"SELECT j.Id, j.Login,j.Password,j.BirthDate,j.Name_First, j.Name_Last,
//                j.Email_ForeConference, j.Email_ForSpeakers, j.Phone_ForConference,
//                j.Phone_ForSpekers,j.CategoryId, j.UniqueId ,j.Version ,
//				c.Name AS {nameof(JudgeTemp.Category_Name)},
//                c.DisplayName AS {nameof(JudgeTemp.Category_DisplayName)}
//                ,c.WhatWeAreLookingFor AS {nameof(JudgeTemp.Category_WhatWeAreLookingFor)}
//                FROM Judges AS j
//                INNER JOIN Categories as C ON j.CategoryId = C.Id";

//            try
//            {
//                var r = await connection.QueryAsync<JudgeTemp>
//                    (q);

//                var rmaped = _mapper.Map<IEnumerable<Judge>>(r);

//                //var r2 = await connection.QueryAsync<Judge>
//                //($"SELECT Id,Login AS {nameof(Judge.Login.Value)}," +
//                //$"Password AS {nameof(Judge.Password.Value)}, " +
//                //$"BirthDate AS {nameof(Judge.Birthdate)}, " +
//                //$"Name_First AS {nameof(Judge.Name.First)}, " +
//                //$"Name_Last, AS {nameof(Judge.Name.Last)}" +
//                //$"Email_ForeConference AS {nameof(Judg)}" +
//                //$",Email_ForSpeakers AS {nameof(Judge.Login.Value)}," +
//                //$"Phone_ForConference AS {nameof(Judge.Login.Value)}," +
//                //$"Phone_ForSpekers AS {nameof(Judge.Login.Value)}, " +
//                //$"CategoryId AS {nameof(Judge.Login.Value)} FROM Judges; ");

//                return ExecutionStatus<IReadOnlyList<Judge>>.DbOk(rmaped.ToList().AsReadOnly());
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus<IReadOnlyList<Judge>>.DbError(ex.Message);
//            }


//        }

//        public async Task<ExecutionStatus<Judge>> GetByIdAsync(int id)
//        {
//            using var connection = new SqliteConnection
//                (_geekLemonContext.ConnectionString);

//            var q = @$"SELECT j.Id,j.Login,j.Password,j.BirthDate,j.Name_First, j.Name_Last,
//                j.Email_ForeConference, j.Email_ForSpeakers, j.Phone_ForConference,
//                j.Phone_ForSpekers,j.CategoryId ,j.UniqueId ,j.Version ,
//                c.Name AS {nameof(JudgeTemp.Category_Name)},
//                c.DisplayName AS {nameof(JudgeTemp.Category_DisplayName)}
//                ,c.WhatWeAreLookingFor AS {nameof(JudgeTemp.Category_WhatWeAreLookingFor)}
//                FROM Judges AS j
//                INNER JOIN Categories as C ON j.CategoryId = C.Id
//                WHERE j.Id = @Id;";

//            try
//            {
//                var r = await connection.
//                 QueryFirstOrDefaultAsync<JudgeTemp>
//                 (q, new
//                 {
//                     @Id = id,
//                 });

//                var rmaped = _mapper.Map<Judge>(r);
//                return ExecutionStatus<Judge>.DbIfDefaultThenError(rmaped);
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus<Judge>.DbError(ex);
//            }

//        }

//        public async Task<ExecutionStatus> UpdateAsync(Judge entity)
//        {
//            using var connection = new SqliteConnection(_geekLemonContext.ConnectionString);

//            //Nie ma SQL INjection
//            var q = @"UPDATE Judges 
//                SET Login = @Login, Password = @Password, BirthDate=@BirthDate  
//                ,Name_First = @Name_First, Name_Last = @Name_Last, 

//                Email_ForeConference=@Email_ForeConference,Email_ForSpeakers = @Email_ForSpeakers,
//                Phone_ForConference = @Phone_ForConference
//                WHERE Id = @Id; ";

//            var temp = _mapper.Map<JudgeTemp>(entity);
//            try
//            {
//                var result = await connection.ExecuteAsync(q, temp);

//                return ExecutionStatus.DbOk();
//            }
//            catch (Exception ex)
//            {
//                return ExecutionStatus.DbError(ex.Message);
//            }


//            //var v = GetValuesFromJugde(entity);

//            //var result = await connection.QueryAsync<int>(q,
//            //     new
//            //     {
//            //         @Id = entity.Id,
//            //         @Login = entity.Login,
//            //         @Password = entity.Password,
//            //         @BirthDate = v.Birthdate,
//            //         @Name_First = entity.Name.First,
//            //         @Name_Last = entity.Name.Last,
//            //         @Email_ForeConference = v.EmailForeConference,
//            //         @Email_ForSpeakers = v.EmailForSpeakers,
//            //         @Phone_ForSpekers = v.PhoneForSpekers,
//            //         @Phone_ForConference = v.PhoneForConference,
//            //         @CategoryID = entity.Category.Id.Value
//            //     });
//        }

//        public Task<ExecutionStatus> DeleteAsyncByUniqueId(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExecutionStatus<Judge>> GetByUniqueIdAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        Task<ExecutionStatus<Ids>> IAsyncRepository<Judge>.AddAsync(Judge entity)
//        {
//            throw new NotImplementedException();
//        }

//        //public ValuesFromJugde GetValuesFromJugde(Judge entity)
//        //{
//        //    var birthdate = entity.Birthdate.ToString("dd-MM-yyyy");
//        //    ValuesFromJugde v = new ValuesFromJugde();

//        //    foreach (var item in entity.Emails)
//        //    {
//        //        if (item.Type == EmailType.FORCONFERENCE)
//        //            v.EmailForeConference = item.Value;

//        //        if (item.Type == EmailType.FORSPEAKERS)
//        //            v.EmailForSpeakers = item.Value;
//        //    }

//        //    foreach (var item in entity.Phones)
//        //    {
//        //        if (item.Type == PhoneType.FORCONFERENCE)
//        //            v.PhoneForConference = "(" + item.AreaCode + ")" + item.Number;

//        //        if (item.Type == PhoneType.FORSPEAKERS)
//        //            v.PhoneForSpekers = "(" + item.AreaCode + ")" + item.Number;
//        //    }

//        //    return v;
//        //}

//        //public class ValuesFromJugde
//        //{
//        //    public string Birthdate { get; set; }
//        //    public string EmailForeConference { get; set; }
//        //    public string EmailForSpeakers { get; set; }
//        //    public string PhoneForSpekers { get; set; }
//        //    public string PhoneForConference { get; set; }
//        //}
//    }
//}
