using GeekLemonConference.Application.EventSourcing.Contracts;
using GeekLemonConference.Domain.Ddd;
using GeekLemonConference.Domain.DomainEvents;
using GeekLemonConference.DomainEvents.Ddd;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using GeekLemonConference.Infrastructure.EventStoreAndBus;
using Microsoft.Extensions.Configuration;

namespace GeekLemonConference.Infrastructure.EventStoreAndBus.Bus
{
    public class GeekLemonEventPublisher : IEventPublisher
    {
        private readonly ConnectionFactory connectionFactory;

        public GeekLemonEventPublisher(IHostingEnvironment env/*, AMQPEventSubscriber aMQPEventSubscriber*/)
        {
            connectionFactory = new ConnectionFactory();

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddEnvironmentVariables();

            builder.Build().GetSection("RabbitMqSetting").Bind(connectionFactory);
        }



        public void Publish<T>(T @event) where T : DomainEvent
        {
            using (IConnection conn = connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    var queue = @event.WhatRabbitMQQueue();

                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null

                    );

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }


    }
}
