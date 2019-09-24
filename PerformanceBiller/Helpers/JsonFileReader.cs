using System.IO;

namespace PerformanceBiller.Helpers
{
    public class JsonFileReader
    {
        private readonly string _basePath;

        private JsonFileReader(string basePath)
        {
            _basePath = basePath;
        }

        public static JsonFileReader From(string basePath)
            => new JsonFileReader(basePath);

        public string Invoices()
            => File.ReadAllText($"{_basePath}\\invoices.json");

        public string Plays()
            => File.ReadAllText($"{_basePath}\\plays.json");
    }
}
