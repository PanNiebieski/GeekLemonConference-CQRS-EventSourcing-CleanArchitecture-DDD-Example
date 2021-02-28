using GeekLemonConference.Domain;
using GeekLemonConference.DomainEvents.Ddd;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers
{
    public interface ISubscribeBase
    {
        string QUEUE_Name { get; }
        DomainEvent DeserializeObject(string json);

        Task HandleBasicDeliver(string consumerTag,
            ulong deliveryTag, bool redelivered, string exchange,
            string routingKey,
            IBasicProperties properties, ReadOnlyMemory<byte> body);

        void StartSubing(IModel channel);

        void Dispose();

        Task<ExecutionStatus> HandleEvent(DomainEvent @event);
    }
}
