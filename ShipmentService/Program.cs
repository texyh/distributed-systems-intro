using System;
using DistribuedSystem.Common;
using MassTransit;

namespace ShipmentService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.ShippingServiceQueue, e =>
                {
                    e.Consumer<OrderRegisteredConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Listening for Register order commands.. " +
                              "Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
