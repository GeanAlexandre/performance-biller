using Newtonsoft.Json;

namespace PerformanceBiller.Helpers
{
    public static class StringExtensions
    {
        public static TObject Deserialize<TObject>(this string serialized) => 
           JsonConvert.DeserializeObject<TObject>(serialized);
    }
}
