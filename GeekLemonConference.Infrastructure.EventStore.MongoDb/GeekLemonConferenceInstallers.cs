
using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Infrastructure.EventStore.MongoDb;
using GeekLemonConference.Infrastructure.EventStorePlugin.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GeekLemonConference.Infrastructure.EventStorePlugin.MongoDb
{
    public static partial class GeekLemonConferenceInstallers
    {
        public static IServiceCollection
            AddEventStoreMongoDb
            (this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddScoped<IMongoDbContext, MongoDbContext>();

            services.AddScoped<IEventStore, MongoDbEventStore>();

            return services;
        }
    }
}
