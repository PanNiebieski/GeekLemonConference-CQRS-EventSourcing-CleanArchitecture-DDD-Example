using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Dto
{
    public class ScoreDto
    {
        public string Score { get; set; }
        public string RejectExplanation { get; set; }
        public string WarringExplanation { get; set; }
    }

    public enum CallForSpeechMachineScoreDto
    {
        None = 0,
        Red = 1, //Rejected
        Yellow = 2, //WithWarrings
        Green = 3, //AllOkej
    }
}
