using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistribuedSystem.Common;
using DistribuedSystem.Common.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DistributedSystems.Web
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
            channel.BasicAcks += (o, args) => HandleAck(args);
            channel.BasicNacks += (o, args) => HandleNack(args);
        }

        private void HandleNack(BasicNackEventArgs args)
        {
            Console.WriteLine(args.DeliveryTag  );
        }

        private void HandleAck(BasicAckEventArgs args)
        {
            Console.WriteLine(args.DeliveryTag);
        }

        public void SendRegisterOrderCommand(IRegisterOrderCommand command)
        {
            channel.ExchangeDeclare(
                exchange: RabbitMqConstants.RegisterOrderExchange,
                type: ExchangeType.Direct);
            channel.QueueDeclare(
                queue: RabbitMqConstants.RegisterOrderQueue, durable: false,
                exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(
                queue: RabbitMqConstants.RegisterOrderQueue,
                exchange: RabbitMqConstants.RegisterOrderExchange,
                routingKey: "");

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType =
                RabbitMqConstants.JsonMimeType;
            messageProperties.CorrelationId = Guid.NewGuid().ToString();

            channel.BasicPublish(
                exchange: RabbitMqConstants.RegisterOrderExchange,
                routingKey: "",
                basicProperties: messageProperties,
                body: Encoding.UTF8.GetBytes(serializedCommand));
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
