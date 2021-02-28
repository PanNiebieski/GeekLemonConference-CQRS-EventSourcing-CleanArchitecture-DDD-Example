using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Domain.Status
{
    public interface IExecutionOptions
    {
        bool ThrowExceptions { get; set; }
    }

    public class ExecutionOptions : IExecutionOptions
    {
        public bool _throwExceptions;

        public ExecutionOptions()
        {

        }

        public ExecutionOptions(bool throwExceptions)
        {
            _throwExceptions = throwExceptions;
        }


        public bool ThrowExceptions
        {
            get => _throwExceptions;
            set => _throwExceptions = value;
        }
    }
}
