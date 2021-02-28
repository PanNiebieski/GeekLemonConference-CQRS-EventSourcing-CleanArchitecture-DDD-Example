using GeekLemonConference.Application.CQRS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetCallForSpeech
{
    public class CallForSpeechViewModel
    {
        public int Id { get; set; }
        public ScoreDto Score { get; set; }
        public SpeakerDto Speaker { get; set; }
        public SpeechDto Speech { get; set; }
        public DecisionDto PreliminaryDecision { get; set; }
        public DecisionDto FinalDecision { get; set; }
        public CategoryDto Category { get; set; }
        public string Status { get; set; }

        public int Version { get; set; }

        public Guid UniqueId { get; set; }
    }
}
