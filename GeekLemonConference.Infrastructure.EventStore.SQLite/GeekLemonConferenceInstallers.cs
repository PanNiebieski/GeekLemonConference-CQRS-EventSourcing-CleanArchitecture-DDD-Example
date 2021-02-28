
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Infrastructure.EventStore.SQLite.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GeekLemonConference.Infrastructure.EventStore.SQLite
{
    public static partial class GeekLemonConferenceInstallers
    {
        public static IServiceCollection
            AddEventStoreSqlLite
            (this IServiceCollection services,
            IConfiguration configuration)
        {
            var connection = configuration.
                GetConnectionString("EventStoreSQLiteConnectionString");

            services.AddScoped<IEventStoreSQLiteContext, EventStoreSQLiteContext>
                (
                    (services) =>
                    {
                        var c =
                        new EventStoreSQLiteContext(connection);
                        return c;
                    }
                );

            services.AddScoped<IEventStore, SqlLiteEventStore>();

            return services;
        }
    }
}
