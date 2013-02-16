using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;

namespace GiantBomb.Api {
    public class FastJsonDeserializer : IDeserializer {
        public T Deserialize<T>(IRestResponse response) {
            if (String.IsNullOrEmpty(response.Content)) return default(T);

            return fastJSON.JSON.Instance.ToObject<T>(response.Content);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
    }
}
