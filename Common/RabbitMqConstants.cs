using System;
using System.Collections.Generic;
using System.Text;

namespace DistribuedSystem.Common
{
    public class RabbitMqConstants
    {
        public const string RabbitMqUri = 
            "amqp://guest:guest@localhost:5672/";
        public const string JsonMimeType = 
            "application/json";

        public const string RegisterOrderExchange = 
            "distributedsystem.registerorder.exchange";
        public const string RegisterOrderQueue = 
            "distributedsystem.registerorder.queue";

        public const string OrderRegisteredExchange = 
            "distributedsystem.orderregistered.exchange";
        public const string OrderRegisteredNotificationQueue = 
            "distributedsystem.orderregistered.notification.queue";
    }
}
