using Dapper;
using GeekLemon.Infrastructure.Read.SQLite;
using GeekLemon.Infrastructure.Read.SQLite.Repositories;
using GeekLemon.Persistence.Dapper.SQLite;
using GeekLemonConference.Application.Contracts.Persistence.WithES;
using GeekLemonConference.Application.Contracts.Repository;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.CallForSpeeches;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Categories;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Interfaces.Judges;
using GeekLemonConference.Persistence.Dapper.SQLite.Methods.Judges;
using GeekLemonConference.Persistence.Dapper.SQLite.Repositories;
using GeekLemonConference.Persistence.Dapper.SQLite.Repositories.CallForSpeeches;
using GeekLemonConference.Persistence.Dapper.SQLite.SqlMapperTypeHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GeekLemonConference.Persistence.Dapper.SQLite
{
    public static partial class GeekLemonConferenceInstallers
    {
        public static IServiceCollection
            AddGeekLemonPersistenceDapperSQLiteServices
            (this IServiceCollection services,
            IConfiguration configuration)
        {
            var connection = configuration.
    GetConnectionString("GeekLemonConferenceConnectionString");
            var zEsConnection = configuration.
                GetConnectionString("ZEsGeekLemonConferenceConnectionString");

            services.AddTransient<IGeekLemonDBContext, GeekLemonDBContext>
                (
                    (services) =>
                    {
                        var c =
                        new GeekLemonDBContext(connection);
                        return c;
                    }
                );

            services.AddTransient<IZEsGeekLemonDBContext, ZEsGeekLemonDBContext>
            (
                (services) =>
                {
                    var c =
                    new ZEsGeekLemonDBContext(zEsConnection);
                    return c;
                }
            );

            //If Scoped or Singleton then Two version of repositories will use one version of this
            services.AddTransient<ICallForSpeechGetByIdDoer, CallForSpeechGetByIdDoer>();
            services.AddTransient<ICallForSpeechGetCollectionDoer, CallForSpeechGetCollectionDoer>();
            services.AddTransient<ICallForSpeechSaveAcceptenceDoer, CallForSpeechSaveAcceptenceDoer>();
            services.AddTransient<ICallForSpeechSaveEvaluatationDoer, CallForSpeechSaveEvaluatationDoer>();
            services.AddTransient<ICallForSpeechSavePreliminaryAcceptenceDoer, CallForSpeechSavePreliminaryAcceptenceDoer>();
            services.AddTransient<ICallForSpeechSaveRejectionDoer, CallForSpeechSaveRejectionDoer>();
            services.AddTransient<ICallForSpeechSubmitDoer, CallForSpeechSubmitDoer>();

            services.AddTransient<ICategoryAddDoer, CategoryAddDoer>();
            services.AddTransient<ICategoryGetAllDoer, CategoryGetAllDoer>();
            services.AddTransient<ICategoryDeleteDoer, CategoryDeleteDoer>();
            services.AddTransient<ICategoryGetByIdDoer, CategoryGetByIdDoer>();
            services.AddTransient<ICategoryUpdateDoer, CategoryUpdateDoer>();

            services.AddTransient<IJudgeAddDoer, JudgeAddDoer>();
            services.AddTransient<IJudgeUpdateDoer, JudgeUpdateDoer>();
            services.AddTransient<IJudgeDeleteDoer, JudgeDeleteDoer>();
            services.AddTransient<IJudgeGetAllDoer, JudgeGetAllDoer>();
            services.AddTransient<IJudgeGetByIdDoer, JudgeGetByIdDoer>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IJudgeRepository, JugdeRepository>();
            services.AddTransient<ICallForSpeechRepository, CallForSpeechRepository>();
            services.AddTransient<IZEsCategoryRepository, ZEsCategoryRepository>();
            services.AddTransient<IZEsJudgeRepository, ZEsJugdeRepository>();
            services.AddTransient<IZEsCallForSpeechRepository, ZEsCallForSpeechRepository>();

            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
            SqlMapper.AddTypeHandler(DateTimeHandler.Default);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
