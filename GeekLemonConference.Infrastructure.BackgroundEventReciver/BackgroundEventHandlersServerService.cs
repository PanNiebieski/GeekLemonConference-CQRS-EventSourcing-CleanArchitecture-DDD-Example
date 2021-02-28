using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using ILogger = Serilog.ILogger;
using GeekLemonConference.Application.EventSourcing.Contracts;
using System.Reflection;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using System.Text;
using GeekLemonConference.Domain.DomainEvents;
using Newtonsoft.Json;
using GeekLemonConference.DomainEvents.Ddd;
using GeekLemonConference.Infrastructure.BackgroundEventHandlersServer.Subscribers;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer
{
    public class BackgroundEventHandlersServerService : BackgroundService
    {
        private readonly ILogger Logger;
        private readonly ISettings Settings;

        private readonly ISubscribeBase[] _subscribes;

        private string _ContentRootPath;

        public BackgroundEventHandlersServerService(ILogger logger, ISettings settings,
            IHostingEnvironment env,
            ISubscribeBase[] subscribes)
        {
            Settings = settings;
            Logger = logger;
            _subscribes = subscribes;


            _ContentRootPath = env.ContentRootPath;
        }


        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(_ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

            builder.Build().GetSection("RabbitMqSetting").Bind(connectionFactory);

            using IConnection conn = connectionFactory.CreateConnection();
            using IModel channel = conn.CreateModel();

            Console.WriteLine("CreateConnection CreateModel");

            try
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                foreach (var item in _subscribes)
                {
                    channel.QueueDeclare(
                        queue: item.QUEUE_Name,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,

                        arguments: null
                    );

                    Console.WriteLine($"QueueDeclare {item.QUEUE_Name}");
                }

                foreach (var item in _subscribes)
                {
                    item.StartSubing(channel);
                    Console.WriteLine($"StartSubing {item.QUEUE_Name}");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exception {ex}");
                Console.ForegroundColor = ConsoleColor.Gray;
                LogError(ex.Message);
            }

            int i = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                i++;
                Console.ForegroundColor = (ConsoleColor)i;
                Console.WriteLine($"Still Working and Still Waiting");
                Console.ForegroundColor = ConsoleColor.Gray;

                if (i == 15)
                {
                    Console.Clear();
                    i = 0;
                }


                await Task.Delay(Settings.Frequency, stoppingToken);
            }
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var item in _subscribes)
            {
                try
                {
                    item.Dispose();
                }
                catch (Exception)
                {

                }

            }

            await base.StopAsync(cancellationToken);
        }

        //private void LogPingReply(PingReply pingReply)
        //{
        //    Logger.Information($"PingReply status={pingReply.Status} roundTripTime={pingReply.RoundtripTime}");
        //}

        private void LogError(string error)
        {
            Logger.Error(error);
        }

    }
}
