using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DistribuedSystem.Common;
using DistribuedSystem.Common.Interfaces;
using MassTransit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing.Impl;
using RegistrationService.Messages;

namespace RegistrationService
{
    class RegisteredOrderCommandConsumer : IConsumer<IRegisterOrderCommand>
    {
        public async Task Consume(ConsumeContext<IRegisterOrderCommand> context)
        {
            var command = context.Message;

            //Store order registration and get Id
            var id = Guid.NewGuid().ToString();

            await Console.Out.WriteLineAsync($"Order with id {id} registered");

            //notify subscribers that a order is registered
            var orderRegisteredEvent = new OrderRegisteredEvent(command, id);
            //publish event
            await context.Publish<IOrderRegisteredEvent>(orderRegisteredEvent);
        }
    }
}
