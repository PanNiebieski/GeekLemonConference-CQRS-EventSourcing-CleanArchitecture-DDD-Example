using GeekLemon.Infrastructure.Write.MongoDB;
using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.DomainEvents.CallForSpeeches;
using GeekLemonConference.DomainEvents.Categories;
using GeekLemonConference.DomainEvents.Ddd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.EventStoreAndBus
{
    public static class DomainEventHelper
    {
        public static string WhatRabbitMQQueue(this DomainEvent @event)
        {
            string g = @event switch
            {
                JudgeUpdatedEvent => Constants.QUEUE_JUDGE_UPDATED,
                JudgeCreatedEvent => Constants.QUEUE_JUDGE_CREATED,
                JudgeDeletedEvent => Constants.QUEUE_JUDGE_DELETED,
                CategoryCreateEvent => Constants.QUEUE_CATEGORY_CREATED,
                CategoryUpdateEvent => Constants.QUEUE_CATEGORY_UPDATED,
                CallForSpeechAcceptedEvent => Constants.QUEUE_CALLFORSPEECH_ACCEPT,
                CallForSpeechEvaulatedEvent => Constants.QUEUE_CALLFORSPEECH_EVALUATE,
                CallForSpeechPreliminaryAcceptEvent => Constants.QUEUE_CALLFORSPEECH_PRELIMINARY_ACCEPT,
                CallForSpeechRejectedEvent => Constants.QUEUE_CALLFORSPEECH_REJECTC,
                CallForSpeechSubmitEvent => Constants.QUEUE_CALLFORSPEECH_SUBMITC,
                _ => throw new NotImplementedException(),
            };

            return g;
        }
    }
}
