using System.Text.Json;

namespace ObjectModels
{
    public static class ObjectModelsExtentions
    {
        public static void Dump(this object obj)
            => Console.WriteLine(obj.DumpString());

        private static string DumpString(this object obj)
            => JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = true });
    }
}
