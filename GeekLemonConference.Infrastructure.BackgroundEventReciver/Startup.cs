using GeekLemonConference.Infrastructure.BackgroundEventHandlersServer;
using GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers;
using GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers.CallForSpeeches;
using GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers.Categories;
using GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers.Judges;
using GeekLemonConference.Persistence.Dapper.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.BackgroundEventReciver
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddGeekLemonPersistenceDapperSQLiteServices(Configuration);

            var seriFileLogger = new LoggerConfiguration().WriteTo.File(@"D:\Temp\").CreateLogger();
            services.AddSingleton<Serilog.ILogger>(seriFileLogger);

            services.AddSingleton<ISettings>(new Settings()
            {
                Timeout = TimeSpan.FromSeconds(5),
                Frequency = TimeSpan.FromSeconds(5),
            });

            services.AddTransient<ISubscribeBase, SubscribeCreateJudge>();
            services.AddTransient<ISubscribeBase, SubscribeUpdateJudge>();
            services.AddTransient<ISubscribeBase, SubscribeDeleteJudge>();
            services.AddTransient<ISubscribeBase, SubscribeCreatedCategory>();
            services.AddTransient<ISubscribeBase, SubscribeUpdateCategory>();
            services.AddTransient<ISubscribeBase, SubscribeSubmitCallForSpeech>();
            services.AddTransient<ISubscribeBase, SubscribeRejectCallForSpeech>();
            services.AddTransient<ISubscribeBase, SubscribePreminalAcceptCallForSpeech>();
            services.AddTransient<ISubscribeBase, SubscribeEvaluateCallForSpeech>();
            services.AddTransient<ISubscribeBase, SubscribeAcceptCallForSpeech>();

            services.AddTransient<ISubscribeBase[]>
                (p => p.GetServices<ISubscribeBase>().ToArray());

            services.AddHostedService<BackgroundEventHandlersServerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.
                    Response.WriteAsync
                    ("GeekLemonConference.Infrastructure.BackgroundEventHandlersServer Working");
                });
            });
        }
    }
}
