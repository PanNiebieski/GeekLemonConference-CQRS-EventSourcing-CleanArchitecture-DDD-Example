using AutoMapper;
using Dapper;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemon.Persistence.Dapper.SQLite.Repositories;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.CallForSpeeches;
using GeekLemonConference.Persistence.Dapper.SQLite.Repositories.CallForSpeeches;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Repositories
{
    public class CallForSpeechRepository : ICallForSpeechRepository
    {
        private ICallForSpeechGetByIdDoer _callForSpeechGetByIdDoer;
        private ICallForSpeechGetCollectionDoer _callForSpeechGetCollectionDoer;
        private ICallForSpeechSaveAcceptenceDoer _callForSpeechSaveAcceptenceDoer;
        private ICallForSpeechSaveEvaluatationDoer _callForSpeechSaveEvaluatationDoer;
        private ICallForSpeechSavePreliminaryAcceptenceDoer _callForSpeechSavePreliminaryAcceptenceDoer;
        private ICallForSpeechSaveRejectionDoer _callForSpeechSaveRejectionDoer;
        private ICallForSpeechSubmitDoer _callForSpeechSubmitDoer;

        public CallForSpeechRepository(ICallForSpeechGetByIdDoer callForSpeechGetByIdDoer,
            ICallForSpeechGetCollectionDoer callForSpeechGetCollectionDoer,
            ICallForSpeechSaveAcceptenceDoer callForSpeechSaveAcceptenceDoer,
            ICallForSpeechSaveEvaluatationDoer callForSpeechSaveEvaluatationDoer,
            ICallForSpeechSavePreliminaryAcceptenceDoer callForSpeechSavePreliminaryAcceptenceDoer,
            ICallForSpeechSaveRejectionDoer callForSpeechSaveRejectionDoer,
            ICallForSpeechSubmitDoer callForSpeechSubmitDoer)
        {
            _callForSpeechGetByIdDoer = callForSpeechGetByIdDoer;
            _callForSpeechGetCollectionDoer = callForSpeechGetCollectionDoer;
            _callForSpeechSaveAcceptenceDoer = callForSpeechSaveAcceptenceDoer;
            _callForSpeechSaveEvaluatationDoer = callForSpeechSaveEvaluatationDoer;
            _callForSpeechSavePreliminaryAcceptenceDoer = callForSpeechSavePreliminaryAcceptenceDoer;
            _callForSpeechSaveRejectionDoer = callForSpeechSaveRejectionDoer;
            _callForSpeechSubmitDoer = callForSpeechSubmitDoer;
        }

        public void ChangeContext(IGeekLemonDBContext geekLemonDB)
        {
            _callForSpeechGetByIdDoer.ChangeDBContext(geekLemonDB);
            _callForSpeechGetCollectionDoer.ChangeDBContext(geekLemonDB);
            _callForSpeechSaveAcceptenceDoer.ChangeDBContext(geekLemonDB);
            _callForSpeechSaveEvaluatationDoer.ChangeDBContext(geekLemonDB);
            _callForSpeechSavePreliminaryAcceptenceDoer.ChangeDBContext(geekLemonDB);
            _callForSpeechSaveRejectionDoer.ChangeDBContext(geekLemonDB);
            _callForSpeechSubmitDoer.ChangeDBContext(geekLemonDB);
        }

        public Task<ExecutionStatus<CallForSpeech>> GetByIdAsync(CallForSpeechId id)
        {
            return _callForSpeechGetByIdDoer.Run(id);
        }

        public Task<ExecutionStatus<CallForSpeech>> GetByIdAsync(CallForSpeechUniqueId id)
        {
            return _callForSpeechGetByIdDoer.Run(id);
        }

        public Task<ExecutionStatus<IReadOnlyList<CallForSpeech>>> GetCollectionAsync(FilterCallForSpeechStyles filtrer)
        {
            return _callForSpeechGetCollectionDoer.Run(filtrer);
        }

        public Task<ExecutionStatus> SaveAcceptenceAsync(CallForSpeechId id, JudgeId judge, CallForSpeechStatus status)
        {
            return _callForSpeechSaveAcceptenceDoer.Run(id, judge, status);
        }

        public Task<ExecutionStatus> SaveAcceptenceAsync(CallForSpeechUniqueId id, JudgeId judge, CallForSpeechStatus status)
        {
            return _callForSpeechSaveAcceptenceDoer.Run(id, judge, status);
        }

        public Task<ExecutionStatus> SaveEvaluatationAsync(CallForSpeechId id, CallForSpeechScoringResult score, CallForSpeechStatus status)
        {
            return _callForSpeechSaveEvaluatationDoer.Run(id, score, status);
        }

        public Task<ExecutionStatus> SaveEvaluatationAsync(CallForSpeechUniqueId id, CallForSpeechScoringResult score, CallForSpeechStatus status)
        {
            return _callForSpeechSaveEvaluatationDoer.Run(id, score, status);
        }

        public Task<ExecutionStatus> SavePreliminaryAcceptenceAsync(CallForSpeechId id, JudgeId judge, CallForSpeechStatus status)
        {
            return _callForSpeechSavePreliminaryAcceptenceDoer.Run(id, judge, status);
        }

        public Task<ExecutionStatus> SavePreliminaryAcceptenceAsync(CallForSpeechUniqueId id, JudgeId judge, CallForSpeechStatus status)
        {
            return _callForSpeechSavePreliminaryAcceptenceDoer.Run(id, judge, status);
        }

        public Task<ExecutionStatus> SaveRejectionAsync(CallForSpeechId id, JudgeId judge, CallForSpeechStatus status)
        {
            return _callForSpeechSaveRejectionDoer.Run(id, judge, status);
        }

        public Task<ExecutionStatus> SaveRejectionAsync(CallForSpeechUniqueId id, JudgeId judge, CallForSpeechStatus status)
        {
            return _callForSpeechSaveRejectionDoer.Run(id, judge, status);
        }

        public Task<ExecutionStatus<CallForSpeechIds>> SubmitAsync(CallForSpeech callForSpeech)
        {
            return _callForSpeechSubmitDoer.Run(callForSpeech);
        }
    }
}
