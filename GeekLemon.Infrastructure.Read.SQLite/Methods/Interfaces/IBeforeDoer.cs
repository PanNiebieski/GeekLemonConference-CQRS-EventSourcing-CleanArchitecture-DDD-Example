using GeekLemon.Persistence.Dapper.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces
{
    public interface IBeforeDoer
    {
        void ChangeDBContext(IGeekLemonDBContext context);
    }
}
