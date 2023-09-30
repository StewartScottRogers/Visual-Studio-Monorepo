using SampleService01.CodeGeneration;
using SampleService01.DataGeneration;
using SampleService01.DataGeneration.Models;

namespace SampleService01.UnitTests
{
    [TestClass]
    public class DataGeneratorUnitTests
    {
        [TestMethod]
        public void DataGeneratorUnitTest()
        {
            IEnumerable<Buyer> buyers
                = DataGenerator
                    .GenerateBuyersForever()
                        .Take(10);

            foreach (Buyer buyer in buyers)
                buyer.Dump();
        }
    }
}