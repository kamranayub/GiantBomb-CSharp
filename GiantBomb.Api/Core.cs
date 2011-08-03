using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using RestSharp;

namespace GiantBomb.Api
{
    public partial class GiantBombRestClient
    {
        private RestClient _client;

        /// <summary>
        /// Base URL of API (defaults to http://api.giantbomb.com)
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Your GiantBomb API token
        /// </summary>
        private string ApiKey { get; set; }

        public GiantBombRestClient(string apiToken)
        {
            BaseUrl = "http://api.giantbomb.com/";
            ApiKey = apiToken;

            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(assembly.FullName);
            var version = assemblyName.Version;

            _client = new RestClient();
            _client.UserAgent = "giantbomb-csharp/" + version;
            _client.BaseUrl = BaseUrl;            

            // API token is used on every request
            _client.AddDefaultParameter("api_key", ApiKey);
            _client.AddDefaultParameter("format", "json");
        }

#if FRAMEWORK
        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public T Execute<T>(RestRequest request) where T : new()
        {
        var response = _client.Execute<T>(request);
        return response.Data;
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public RestResponse Execute(RestRequest request)
        {
        return _client.Execute(request);
        }
#endif
    }
}
