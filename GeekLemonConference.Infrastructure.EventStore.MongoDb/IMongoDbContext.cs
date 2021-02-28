using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.EventStorePlugin.MongoDb
{
    public interface IMongoDbContext
    {
        IMongoDatabase Database { get; }

        IClientSessionHandle Session { get; }

        IClientSessionHandle StartSession
            ();
    }
}
