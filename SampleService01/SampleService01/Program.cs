using SampleService01.CodeGeneration;
using SampleService01.DataGeneration;
using SampleService01.DataGeneration.Models;

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