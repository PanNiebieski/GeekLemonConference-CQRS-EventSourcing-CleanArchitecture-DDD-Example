using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain
{
    public class AppTime
    {
        public static Func<DateTime> CurrentTimeProvider
        { get; set; } = () => DateTime.Now;

        public static DateTime Now() => CurrentTimeProvider();
    }
}
