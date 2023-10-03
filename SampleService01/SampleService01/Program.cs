using ObjectModels.Extientions;
using ObjectModels.Models;
using RabbitMq.Publisher;
using RabbitMq.SharedProject.Messaging;
using SampleService01.DataGeneration;

namespace SampleService01
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            CreateOrderMessagePublisher createOrderMessagePublisher
                = new CreateOrderMessagePublisher(new RabbitMqConfiguration());

            IEnumerable<Buyer> buyers
                = DataGenerator
                    .GenerateBuyersForever();

            foreach (Buyer buyer in buyers)
            {
                buyer.Dump();

                await createOrderMessagePublisher.Send(buyer);

                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
        }
    }
}