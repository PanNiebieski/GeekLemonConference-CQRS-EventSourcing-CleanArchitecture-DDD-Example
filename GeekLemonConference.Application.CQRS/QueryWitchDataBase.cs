using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS
{
    public enum QueryWitchDataBase
    {
        NormalCQRS = 0,
        WithEventSourcing = 1
    }
}
