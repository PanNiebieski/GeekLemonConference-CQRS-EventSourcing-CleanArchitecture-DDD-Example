using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Persistence.Dapper.SQLite.Methods
{
    public abstract class BeforeDoer : IBeforeDoer
    {
        protected IGeekLemonDBContext _geekLemonContext;

        public void ChangeDBContext(IGeekLemonDBContext context)
        {
            _geekLemonContext = context;
        }
    }
}
