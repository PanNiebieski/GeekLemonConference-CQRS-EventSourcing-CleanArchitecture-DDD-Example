using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Judges;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemon.Infrastructure.Read.SQLite
{
    public class JugdeRepository : IJudgeRepository
    {

        private IJudgeAddDoer _judgeAddDoer;
        private IJudgeUpdateDoer _judgeUpdateDoer;
        private IJudgeDeleteDoer _judgeDeleteDoer;
        private IJudgeGetAllDoer _judgeGetAllDoer;
        private IJudgeGetByIdDoer _judgeGetByIdDoer;


        public JugdeRepository(IJudgeAddDoer judgeAddDoer,
            IJudgeUpdateDoer judgeUpdateDoer, IJudgeDeleteDoer judgeDeleteDoer,
            IJudgeGetAllDoer judgeGetAllDoer, IJudgeGetByIdDoer judgeGetByIdDoer)
        {
            _judgeAddDoer = judgeAddDoer;
            _judgeUpdateDoer = judgeUpdateDoer;
            _judgeDeleteDoer = judgeDeleteDoer;
            _judgeGetAllDoer = judgeGetAllDoer;
            _judgeGetByIdDoer = judgeGetByIdDoer;
        }

        public void ChangeContext(IGeekLemonDBContext geekLemonDB)
        {
            _judgeAddDoer.ChangeDBContext(geekLemonDB);
            _judgeUpdateDoer.ChangeDBContext(geekLemonDB);
            _judgeDeleteDoer.ChangeDBContext(geekLemonDB);
            _judgeGetAllDoer.ChangeDBContext(geekLemonDB);
            _judgeGetByIdDoer.ChangeDBContext(geekLemonDB);
        }

        public Task<ExecutionStatus<JudgeIds>> AddAsync(Judge entity)
        {
            return _judgeAddDoer.Run(entity);
        }

        public Task<ExecutionStatus> DeleteAsync(JudgeId entity)
        {
            return _judgeDeleteDoer.Run(entity);
        }

        public Task<ExecutionStatus> DeleteAsync(JudgeUniqueId id)
        {
            return _judgeDeleteDoer.Run(id);
        }

        public Task<ExecutionStatus<IReadOnlyList<Judge>>> GetAllAsync()
        {
            return _judgeGetAllDoer.Run();
        }

        public Task<ExecutionStatus<Judge>> GetByIdAsync(JudgeId id)
        {
            return _judgeGetByIdDoer.Run(id);
        }

        public Task<ExecutionStatus<Judge>> GetByIdAsync(JudgeUniqueId id)
        {
            return _judgeGetByIdDoer.Run(id);
        }

        public Task<ExecutionStatus> UpdateByUniqueIdAsync(Judge entity)
        {
            return _judgeUpdateDoer.Run(entity, ByWhatId.UniqueId);
        }

        public Task<ExecutionStatus> UpdateByIdAsync(Judge entity)
        {
            return _judgeUpdateDoer.Run(entity, ByWhatId.CreatedId);
        }
    }
}
