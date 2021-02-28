using GeekLemonConference.Application.Contracts.Persistence;
using GeekLemonConference.Domain;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.Contracts.Repository
{
    public interface ICallForSpeechRepository
    {
        Task<ExecutionStatus<IReadOnlyList<CallForSpeech>>> GetCollectionAsync(
    FilterCallForSpeechStyles filtrer);

        Task<ExecutionStatus<CallForSpeech>> GetByIdAsync(CallForSpeechId id);

        Task<ExecutionStatus<CallForSpeech>> GetByIdAsync(CallForSpeechUniqueId id);

        Task<ExecutionStatus> SavePreliminaryAcceptenceAsync(CallForSpeechId id,
            JudgeId judge, CallForSpeechStatus status);

        Task<ExecutionStatus> SaveAcceptenceAsync(CallForSpeechId id,
            JudgeId judge, CallForSpeechStatus status);

        Task<ExecutionStatus<CallForSpeechIds>> SubmitAsync(CallForSpeech callForSpeech);

        Task<ExecutionStatus> SavePreliminaryAcceptenceAsync(CallForSpeechUniqueId id,
    JudgeId judge, CallForSpeechStatus status);

        Task<ExecutionStatus> SaveAcceptenceAsync(CallForSpeechUniqueId id,
            JudgeId judge, CallForSpeechStatus status);

        Task<ExecutionStatus> SaveRejectionAsync(CallForSpeechUniqueId id,
            JudgeId judge, CallForSpeechStatus status);

        Task<ExecutionStatus> SaveRejectionAsync(CallForSpeechId id,
    JudgeId judge, CallForSpeechStatus status);

        Task<ExecutionStatus> SaveEvaluatationAsync(CallForSpeechUniqueId id,
            CallForSpeechScoringResult score, CallForSpeechStatus status);

        Task<ExecutionStatus> SaveEvaluatationAsync(CallForSpeechId id,
    CallForSpeechScoringResult score, CallForSpeechStatus status);
    }

    public enum FilterCallForSpeechStyles
    {
        All = 100,
        New = 0,
        EvaluatedByMachine = 1,
        PreliminaryAcceptedByJudge = 2,
        AcceptedByJudge = 3,
        AcceptedByJudgeButHasProblems = 4,
        Rejected = 5,
    }
}
