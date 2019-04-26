using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serialization;

namespace GiantBomb.Api
{
    internal sealed class GiantBombDeserializer : IRestSerializer
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public string Serialize(object obj) =>
            JsonConvert.SerializeObject(obj, _settings);

        public string Serialize(Parameter parameter) =>
            JsonConvert.SerializeObject(parameter.Value, _settings);

        public T Deserialize<T>(IRestResponse response) =>
            JsonConvert.DeserializeObject<T>(response.Content, _settings);

        public string[] SupportedContentTypes { get; } =
        {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };

        public string ContentType { get; set; } = "application/json";

        public DataFormat DataFormat { get; } = DataFormat.Json;
    }
}
