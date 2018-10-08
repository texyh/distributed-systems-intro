using System;
using System.Collections.Generic;
using System.Text;
using DistribuedSystem.Common;
using DistribuedSystem.Common.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing.Impl;
using RegistrationService.Messages;

namespace RegistrationService
{
    class RegisteredOrderCommandConsumer : DefaultBasicConsumer
    {
        private readonly RabbitMqManager rabbitMqManager;

        public RegisteredOrderCommandConsumer(
            RabbitMqManager rabbitMqManager)
        {
            this.rabbitMqManager = rabbitMqManager;
        }

        public override void HandleBasicDeliver(
            string consumerTag, ulong deliveryTag,
            bool redelivered, string exchange, string routingKey,
            IBasicProperties properties, byte[] body)
        {
            if (properties.ContentType != RabbitMqConstants.JsonMimeType)
                throw new ArgumentException(
                    $"Can't handle content type {properties.ContentType}");

            var message = Encoding.UTF8.GetString(body);
            var commandObj =
                JsonConvert.DeserializeObject<RegisterOrderCommand>(
                    message);
            Consume(commandObj, properties.CorrelationId);
            rabbitMqManager.SendAck(deliveryTag);
        }

        private void Consume(IRegisterOrderCommand command, string corrId)
        {
            //Store order registration and get Id
            var id = corrId;

            Console.WriteLine($"Order with id {id} registered");
            Console.WriteLine("Publishing order registered event");

            //notify subscribers that a order is registered
            var orderRegisteredEvent = new OrderRegisteredEvent(command, id);
            //publish event
            rabbitMqManager.SendOrderRegisteredEvent(orderRegisteredEvent);
        }
    }
}
