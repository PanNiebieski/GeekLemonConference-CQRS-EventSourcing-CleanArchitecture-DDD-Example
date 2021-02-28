using Microsoft.Extensions.DependencyInjection;
using System;

namespace GeekLemonConference.Application.CQRS.EventSourcingV
{
    public partial class GeekLemonConferenceInstallers
    {
        public static IServiceCollection AddGeekLemonConferenceCQRSEventVersion
    (this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddEventStoreAndBus(Configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IScoringRulesFactory, ScoringRulesFactory>();

            var eventSourcingOptions = new EventSourcingOptions();

            Configuration.GetSection(EventSourcingOptions.EventSourcing)
                .Bind(eventSourcingOptions);

            services.AddSingleton<IEventSourcingOptions, EventSourcingOptions>
                (
                    (services) =>
                    {
                        return eventSourcingOptions;
                    }
                );

            //if (eventSourcingOptions.TurnOn)
            //{
            //    services.AddScoped<IUpdateCategoryCommandDoer, UpdateCategoryCommandDoerEventSourcing>();
            //    services.AddScoped<ICreatedCategoryCommandDoer, CreatedCategoryCommandDoerEventSourcing>();
            //    services.AddEventStoreAndBus(Configuration);
            //}
            //else
            //{
            //    services.AddScoped<IUpdateCategoryCommandDoer, UpdateCategoryCommandDoerNormal>();
            //    services.AddScoped<ICreatedCategoryCommandDoer, CreatedCategoryCommandDoerNormal>();
            //}


            var responseMessagesOption = new ResponseMessagesOption();

            Configuration.GetSection(ResponseMessagesOption.Name)
                .Bind(eventSourcingOptions);

            //services.AddSingleton<IResponseMessagesOption, ResponseMessagesOption>
            //    (
            //        (services) =>
            //        {
            //            return responseMessagesOption;
            //        }
            //    );
            BaseResponse.ResponseMessagesOption = responseMessagesOption;



            return services;
        }
    }
}
