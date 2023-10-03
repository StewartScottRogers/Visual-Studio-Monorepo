using ObjectModels.Extientions;
using ObjectModels.Models;
using SampleService01.DataGeneration;

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