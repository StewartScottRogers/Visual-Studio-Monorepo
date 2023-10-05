using ObjectModels.Extientions;
using ObjectModels.Models;
using RabbitMq.Listener;
using RabbitMq.Publisher;

using RabbitMq.SharedProject.Messaging;
using SampleService01.DataGeneration;

namespace SampleService01
{
    public static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RabbitMqConfiguration.DumpEnvironmentVariables();

                RabbitMqConfiguration rabbitMqConfiguration
                    = new RabbitMqConfiguration();

                CreateOrderMessagePublisher createOrderMessagePublisher
                    = new CreateOrderMessagePublisher(rabbitMqConfiguration);

                Task.Delay(TimeSpan.FromSeconds(3)).Wait();

                CreateOrderMessageListener createOrderMessageListener
                    = new CreateOrderMessageListener(
                        rabbitMqConfiguration, (buyer) =>
                        {
                            buyer.Dump();
                        }
                    );

                IEnumerable<Buyer> buyers
                    = DataGenerator
                        .GenerateBuyersForever();

                foreach (Buyer buyer in buyers)
                {
                    //buyer.Dump();

                    createOrderMessagePublisher.Send(buyer);

                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                throw;
            }
        }
    }
}