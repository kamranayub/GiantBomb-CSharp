using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp.Portable.Deserializers;

namespace GiantBomb.Api
{
    class GiantBombDeserializer : JsonDeserializer
    {
        protected override void ConfigureSerializer(JsonSerializer serializer)
        {
            serializer.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            serializer.ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
        }
    }
}
