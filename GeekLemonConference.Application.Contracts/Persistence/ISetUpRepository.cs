using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.Contracts.Persistence
{
    public interface ISetUpRepository
    {
        void SetupCategories();


        void SetupJudges();


        void SetupCallForSpeakes();

    }
}
