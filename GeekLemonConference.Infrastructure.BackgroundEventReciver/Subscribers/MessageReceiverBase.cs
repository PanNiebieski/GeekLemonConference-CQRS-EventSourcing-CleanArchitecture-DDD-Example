using RabbitMQ.Client;
using System;
using System.Text;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers
{
    public class MessageReceiverBase : DefaultBasicConsumer
    {
        private readonly IModel _channel;

        private ISubscribeBase _messageReceiverBasez;

        public void Dispose()
        {
            _channel.Dispose();
        }


        public MessageReceiverBase(IModel channel, ISubscribeBase messageReceiverBasez)
        {
            _messageReceiverBasez = messageReceiverBasez;
            _channel = channel;
        }

        public override void HandleBasicDeliver(string consumerTag,
            ulong deliveryTag, bool redelivered, string exchange,
            string routingKey,
            IBasicProperties properties, ReadOnlyMemory<byte> body)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Consuming Message");
            Console.WriteLine(string.Concat("Message received from the exchange ", exchange));
            Console.WriteLine(string.Concat("Consumer tag: ", consumerTag));
            Console.WriteLine(string.Concat("Delivery tag: ", deliveryTag));
            Console.WriteLine(string.Concat("Routing tag: ", routingKey));

            var json = Encoding.UTF8.GetString(body.Span);
            Console.WriteLine(string.Concat("Message: ", json));
            Console.ForegroundColor = ConsoleColor.Gray;
            _messageReceiverBasez.HandleBasicDeliver
                (consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);



            // #ToFix
            //_channel.BasicAck(deliveryTag, false);
            //HandleEvent(DeserializeObject(json));



        }

        //public abstract DomainEvent DeserializeObject(string json);




    }
}
