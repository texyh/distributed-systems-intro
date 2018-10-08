using System;

namespace NotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var rabbitMqManager = new RabbitMqManager())
            {
                rabbitMqManager.ListenForOrderRegisteredEvent();
                Console.WriteLine("Listening for RegisterOrderCommand..");
                Console.ReadKey();
            }
        }
    }
}
