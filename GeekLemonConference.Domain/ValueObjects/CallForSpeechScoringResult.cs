using GeekLemonConference.Domain.Ddd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class CallForSpeechScoringResult : ValueObject<CallForSpeechScoringResult>
    {
        [JsonProperty("score")]
        public CallForSpeechMachineScore Score { get; set; }


        public string RejectExplanation { get; set; }
        public string WarringExplanation { get; set; }

        //[JsonConstructor]
        public CallForSpeechScoringResult(CallForSpeechMachineScore score,
            string rejectexlanation)
        {
            Score = score;
            RejectExplanation = rejectexlanation;
        }

        [JsonConstructor]
        public CallForSpeechScoringResult(CallForSpeechMachineScore score,
    string rejectExplanation, string warringExplanation)
        {
            Score = score;
            RejectExplanation = rejectExplanation;
            WarringExplanation = warringExplanation;
        }

        //To satisfy EF Core
        public CallForSpeechScoringResult()
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
