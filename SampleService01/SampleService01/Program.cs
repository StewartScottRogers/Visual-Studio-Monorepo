namespace SampleService01
{
    public static class Program
    {
        static void Main(string[] args)
        {
            long counter = 0;
            while (true)
            {
                counter++;
                Console.WriteLine($"Hello, {nameof(counter)}: {counter:0000000000}");
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
        }

        public static string GetDisplayName() => $"{nameof(SampleService01)}";
    }
}