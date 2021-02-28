using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.Common
{
    public class ResponseMessagesOption : IResponseMessagesOption
    {
        public const string Name = "ResponseMessages";

        public bool ShowDeveloperMessages { get; set; }
    }

    public interface IResponseMessagesOption
    {
        bool ShowDeveloperMessages { get; set; }
    }
}
