using GeekLemonConference.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Tests.Asserts
{
    public static class LoanApplicationAssertExtension
    {
        public static CallForSpeechAssert Should(this CallForSpeech cfs)
            => new CallForSpeechAssert(cfs);
    }
}
