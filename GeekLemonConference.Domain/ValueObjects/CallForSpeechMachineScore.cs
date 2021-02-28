using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public enum CallForSpeechMachineScore
    {
        None = 0,
        Red = 1, //Rejected
        Yellow = 2, //WithWarrings
        Green = 3, //AllOkej

    }
}
