using System;
using System.Text;
using DistribuedSystem.Common;
using DistribuedSystem.Common.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RegistrationService
{
    public class RabbitMqManager : IDisposable
    {
        private readonly IModel channel;
        private readonly IConnection connection;
        public RabbitMqManager()
        {
            var connectionFactory =
                new ConnectionFactory { Uri = new Uri (RabbitMqConstants.RabbitMqUri) };
                connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.ConfirmSelect();
            channel.BasicAcks += (o, args) => HandleAck(args);
            channel.BasicNacks += (o, args) => HandleNack(args);
        }

        private void HandleNack(BasicNackEventArgs args)
        {
            Console.WriteLine(args.DeliveryTag);
        }

        private void HandleAck(BasicAckEventArgs args)
        {
            Console.WriteLine(args.DeliveryTag);
        }

        public void ListenForRegisterOrderCommand()
        {
            channel.QueueDeclare(
                queue: RabbitMqConstants.RegisterOrderQueue,
                durable:false,
                exclusive:false,autoDelete:false, arguments:null
                );
            channel.BasicQos(prefetchSize:0, prefetchCount:1, global:false);

            var consumer = new RegisteredOrderCommandConsumer(this);
            channel.BasicConsume(
                queue: RabbitMqConstants.RegisterOrderQueue,
                autoAck: false,
                consumer: consumer);

        }

        public void SendOrderRegisteredEvent(IOrderRegisteredEvent command)
        {
            channel.ExchangeDeclare(
                exchange: RabbitMqConstants.OrderRegisteredExchange,
                type: ExchangeType.Fanout);
            channel.QueueDeclare(
                queue: RabbitMqConstants.OrderRegisteredNotificationQueue,
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);
            channel.QueueBind(
                queue: RabbitMqConstants.OrderRegisteredNotificationQueue,
                exchange: RabbitMqConstants.OrderRegisteredExchange,
                routingKey: "");

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = RabbitMqConstants.JsonMimeType;

            channel.BasicPublish(
                exchange: RabbitMqConstants.OrderRegisteredExchange,
                routingKey: "",
                basicProperties: messageProperties,
                body: Encoding.UTF8.GetBytes(serializedCommand));
        }

        public void SendAck(ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        public void Dispose()
        {
            if (!channel.IsClosed)
            {
                channel.Close();
                connection.Close();
            }
        }
    }
}
