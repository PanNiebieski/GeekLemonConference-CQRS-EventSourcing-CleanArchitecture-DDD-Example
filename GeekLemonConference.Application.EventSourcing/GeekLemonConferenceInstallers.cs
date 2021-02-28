
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace GeekLemonConference.Application
{
    public static partial class GeekLemonConferenceInstallers
    {
        public static IServiceCollection AddGeekLemonConferenceEventSourcing
            (this IServiceCollection services, IConfiguration Configuration)
        {

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
