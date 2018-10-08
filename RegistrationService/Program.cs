using System;
using DistribuedSystem.Common;
using DistribuedSystem.Common.Interfaces;
using MassTransit;

namespace RegistrationService
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host,
                    RabbitMqConstants.RegisterOrderServiceQueue, e =>
                    {
                        e.Consumer<RegisteredOrderCommandConsumer>();
                        //e.Handler<IRegisterOrderCommand>(c => Console.Out.WriteLineAsync(c.Message.DeliverAddress));  // mini handler
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
