using System;
using System.Collections.Generic;
using System.Text;

namespace DistribuedSystem.Common
{
    public class RabbitMqConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/learningDistributedSystems/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string RegisterOrderServiceQueue = "registerorder.service";
        public const string NotificationServiceQueue = "notification.service";
        public const string ShippingServiceQueue = "shipping.service";
    }
}
