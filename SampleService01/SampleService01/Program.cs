using ObjectModels.Extientions;
using ObjectModels.Models;
using SampleService01.DataGeneration;

namespace SampleService01
{
    public static class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Buyer> buyers
                = DataGenerator
                    .GenerateBuyersForever();

            foreach (Buyer buyer in buyers)
            {
                buyer.Dump();
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
        }
    }
}