using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.EventStoreAndBus
{
    public class RabbitMqConfiguration
    {
        public const string Name = "RabbitMq";

        public string Hostname { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Uri { get; set; }

        public string Virtualhost { get; set; }
    }
}
