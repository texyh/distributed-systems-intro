using System;
using System.Collections.Generic;
using System.Text;
using DistribuedSystem.Common.Interfaces;

namespace NotificationService
{
    public class OrderRegisteredConsumer
    {
        public void Consume(IOrderRegisteredEvent registeredEvent)
        {
            //Send notification to user
            Console.WriteLine($"Customer notification sent: Order id {registeredEvent.OrderId} registered");
        }
    }
}
