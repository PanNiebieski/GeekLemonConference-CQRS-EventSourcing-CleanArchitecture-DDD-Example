using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class CallForSpeechScoringResult : ValueObject<CallForSpeechScoringResult>
    {
        public CallForSpeechMachineScore Score { get; }
        public string RejectExplanation { get; }
        public string WarringExplanation { get; }

        //[JsonConstructor]
        public CallForSpeechScoringResult(CallForSpeechMachineScore score,
            string rejectexlanation)
        {
            Score = score;
            RejectExplanation = rejectexlanation;
        }

        public CallForSpeechScoringResult(CallForSpeechMachineScore score,
    string rejectexlanation, string warringExplanation)
        {
            Score = score;
            RejectExplanation = rejectexlanation;
            WarringExplanation = warringExplanation;
        }

        //To satisfy EF Core
        protected CallForSpeechScoringResult()
        {
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Score;
            yield return RejectExplanation;
        }

        public static CallForSpeechScoringResult Green()
        {
            return new CallForSpeechScoringResult(CallForSpeechMachineScore.Green, "", "");
        }

        public static CallForSpeechScoringResult Red(string[] messages)
        {
            return new CallForSpeechScoringResult(CallForSpeechMachineScore.Red, string.Join(Environment.NewLine, messages), "");
        }

        public static CallForSpeechScoringResult Yellow(string[] messages)
        {
            return new CallForSpeechScoringResult(CallForSpeechMachineScore.Yellow, "", string.Join(Environment.NewLine, messages));
        }

        public bool IsRed()
        {
            return Score == CallForSpeechMachineScore.Red;
        }

        public bool IsYellow()
        {
            return Score == CallForSpeechMachineScore.Yellow;
        }

        public bool IsGreen()
        {
            return Score == CallForSpeechMachineScore.Green;
        }
    }
}
