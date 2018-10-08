using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DistribuedSystem.Common.Interfaces;
using MassTransit;

namespace ShipmentService
{
    public class OrderRegisteredConsumer : IConsumer<IOrderRegisteredEvent>
    {
        public async Task Consume(ConsumeContext<IOrderRegisteredEvent> context)
        {
            await Console.Out.WriteLineAsync($"Order with id {context.Message.OrderId} recieved");
        }
    }
}
