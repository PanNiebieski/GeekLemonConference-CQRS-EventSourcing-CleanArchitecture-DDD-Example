using GeekLemonConference.Application.Contracts;
using GeekLemonConference.Application.Contracts.Scoring;
using GeekLemonConference.Application.CQRS;
using GeekLemonConference.Application.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GeekLemonConference.Application.CQRS.Categories.Commands.UpdateCategory;

namespace GeekLemonConference.Application.CQRS
{
    public static partial class GeekLemonConferenceInstallers
    {
        public static IServiceCollection AddGeekLemonConferenceCQRS
            (this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IScoringRulesFactory, ScoringRulesFactory>();

            //var responseMessagesOption = new ResponseMessagesOption();

            //services.AddSingleton<IResponseMessagesOption, ResponseMessagesOption>
            //    (
            //        (services) =>
            //        {
            //            return responseMessagesOption;
            //        }
            //    );
            //BaseResponse.ResponseMessagesOption = responseMessagesOption;

            return services;
        }
    }
}
