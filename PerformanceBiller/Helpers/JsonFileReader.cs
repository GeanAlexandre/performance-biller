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

        public T Read<T>(string jsonFileName) => File.ReadAllText($"{_basePath}\\{jsonFileName}.json").Deserialize<T>();
    }
}
