using System;

namespace RegistrationService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var manager = new RabbitMqManager())
            {
                manager.ListenForRegisterOrderCommand();
                Console.WriteLine("Listening for order registerd event");
                Console.ReadKey();
            }
        }
    }
}
