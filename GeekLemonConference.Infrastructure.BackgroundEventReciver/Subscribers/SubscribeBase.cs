using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain;
using GeekLemonConference.DomainEvents.Ddd;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers
{
    public abstract class SubscribeBase : ISubscribeBase
    {
        private MessageReceiverBase Consumer { get; set; }
        private IModel _channel;

        public abstract string QUEUE_Name { get; }

        public SubscribeBase()
        {


        }

        public void StartSubing(IModel channel)
        {
            _channel = channel;
            Consumer = new MessageReceiverBase(channel, this);
            channel.BasicConsume(this.QUEUE_Name,
                               false, Consumer);
        }

        public abstract DomainEvent DeserializeObject(string json);


        public async Task HandleBasicDeliver(string consumerTag, ulong deliveryTag,
            bool redelivered, string exchange, string routingKey, IBasicProperties
            properties, ReadOnlyMemory<byte> body)
        {
            var json = Encoding.UTF8.GetString(body.Span);
            var obj = DeserializeObject(json);
            var status = await HandleEvent(obj);

            if (status.Success)
            {
                try
                {
                    _channel.BasicAck(deliveryTag, false);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

        }

        public void Dispose()
        {
            Consumer.Dispose();
        }

        public abstract Task<ExecutionStatus> HandleEvent(DomainEvent @event);

    }
}
