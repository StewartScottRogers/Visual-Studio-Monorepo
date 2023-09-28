namespace SampleService01.UnitTests
{
    [TestClass]
    public class SampleUnitTests
    {
        [TestMethod]
        public void GetDisplayNameUnitTest()
        {
            string displayName = Program.GetDisplayName();    
            Assert.IsNotNull(displayName); 
            Console.WriteLine(displayName);
        }
    }
}