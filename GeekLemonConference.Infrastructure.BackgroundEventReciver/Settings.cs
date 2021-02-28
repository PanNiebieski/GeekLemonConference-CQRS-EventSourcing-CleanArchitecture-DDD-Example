using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer
{
    public interface ISettings
    {
        TimeSpan Frequency { get; set; }
        TimeSpan Timeout { get; set; }
    }

    public class Settings : ISettings
    {
        public TimeSpan Frequency { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}
