﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DistribuedSystem.Common.Interfaces;
using MassTransit;

namespace NotificationService
{
    public class OrderRegisteredConsumer : IConsumer<IOrderRegisteredEvent>
    {
        public async Task Consume(ConsumeContext<IOrderRegisteredEvent> context)
        {
            //Send notification to user
            await Console.Out.WriteLineAsync($"Customer notification sent: " +
                                             $"Order id {context.Message.OrderId}");
        }
    }
}
