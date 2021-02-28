using GeekLemonConference.Domain.Status;

namespace GeekLemonConference.Domain
{
    public static class ExecutionFlow
    {
        public static IExecutionOptions Options { get; set; }

        static ExecutionFlow()
        {
            Options = new ExecutionOptions();
        }
    }
}



