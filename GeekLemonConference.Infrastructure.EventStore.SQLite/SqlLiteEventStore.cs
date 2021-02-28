using Dapper;
using GeekLemonConference.Application.EventSourcing;
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.DomainEvents.Ddd;
using GeekLemonConference.Infrastructure.EventStore.SQLite.Config;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.EventStore.SQLite
{
    public class SqlLiteEventStore : IEventStore
    {
        private IEventStoreSQLiteContext _geekLemonContext;

        public SqlLiteEventStore(IEventStoreSQLiteContext context)
        {
            _geekLemonContext = context;
        }

        public List<DomainEvent> Get(AggregateKey aggregateId, int fromVersion)
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);

            try
            {
                var r = connection.Query<EventTemp>
                (@"SELECT Id,Key, Value, AssemblyQualifiedName, Version FROM EventSTORE
                    WHERE Key = @aggregateId and Version > @Version;", new
                {
                    @aggregateId = aggregateId.Id,
                    @Version = fromVersion
                });

                List<DomainEvent> de = new List<DomainEvent>();

                foreach (var item in r)
                {
                    Assembly asm = typeof(DomainEvent).Assembly;
                    Type type = TypeRecon.ReconstructType(item.AssemblyQualifiedName, true, asm);

                    var domain = JsonConvert.
                        DeserializeObject(item.Value, type);

                    de.Add(domain as DomainEvent);
                }

                return de;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Save(DomainEvent @event)
        {
            using var connection = new SqliteConnection
                (_geekLemonContext.ConnectionString);


            try
            {
                var q = @"INSERT INTO EventSTORE(Key, Value,
                    AssemblyQualifiedName
                    ,Version)
                    VALUES (@Key, @Value, @AssemblyQualifiedName,@Version);";


                var result = connection.Execute(q, new
                {
                    @Key = @event.Key.Id,
                    @Value = JsonConvert.SerializeObject(@event),
                    @AssemblyQualifiedName = @event.GetType().AssemblyQualifiedName,
                    @Version = @event.Version,
                }
                );

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
