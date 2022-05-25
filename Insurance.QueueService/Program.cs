using InsuranceQueue;
using InsuranceQueue.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Insurance.QueueService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Build a config object, using env vars and JSON providers.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            await Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    // Add a messsage broker using MassTransit
                    services.AddMassTransit(busRegistrationConfigurator =>
                    {
                        busRegistrationConfigurator.AddConsumer<GetInsuranceOfferConsumer>();

                        // Azure Service Bus message broker
                        busRegistrationConfigurator.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });
                            //string azureServiceBusConnectionString = config["RabbitMqConnectionString"];
                            //cfg.Host(azureServiceBusConnectionString);

                            //// Configure the Azure Service Bus topics, subsciptions, and the underlying queues for each subscription
                            //cfg.ConfigureEndpoints(context);
                        });

                        // Add Saga State Machines
                        const string redisConfigurationString = "127.0.0.1";
                        // Passing a definition allows us to configure 
                        busRegistrationConfigurator.AddSagaStateMachine<GetOfferProcessStateMachine, GetOfferProcessState>(typeof(GetOfferProcessStateMachineDefinition))
                           // Redis repository to store state instances. By default, redis runs on localhost.
                           .RedisRepository(r =>
                           {
                               r.DatabaseConfiguration(redisConfigurationString);

                               r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                           });


                    });
                })
                .Build()
                .RunAsync();
        }
    }
}
