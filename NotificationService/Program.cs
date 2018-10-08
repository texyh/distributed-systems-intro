using System;
using DistribuedSystem.Common;
using MassTransit;

namespace NotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.NotificationServiceQueue, e =>
                {
                    e.Consumer<OrderRegisteredConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Listening for Order registered events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
