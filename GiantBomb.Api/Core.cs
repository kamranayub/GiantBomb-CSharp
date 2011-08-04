using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GiantBomb.Api.Model;
using RestSharp;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient : IGiantBombRestClient {
        private RestClient _client;

        /// <summary>
        /// Base URL of API (defaults to http://api.giantbomb.com)
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Your GiantBomb API token
        /// </summary>
        private string ApiKey { get; set; }

        public GiantBombRestClient(string apiToken) {
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
        public T Execute<T>(RestRequest request) where T : new() {
            var response = _client.Execute<T>(request);
            return response.Data;
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public RestResponse Execute(RestRequest request) {
            return _client.Execute(request);
        }
#endif

        protected RestRequest GetListResource(string resource, int page = 1, int pageSize = 20, string[] fieldList = null) {
            if (pageSize > 20)
                throw new ArgumentOutOfRangeException("pageSize", "Page size cannot be greater than 20.");

            var request = new RestRequest {
                Resource = resource + "//",
                DateFormat = "yyyy-MM-dd HH:mm:ss"
            };

            if (page > 1) {
                request.AddParameter("offset", pageSize * (page - 1));
            }

            request.AddParameter("limit", pageSize);

            if (fieldList != null)
                request.AddParameter("field_list", String.Join(",", fieldList));

            return request;
        }

        protected IEnumerable<TResult> GetListResource<TResult>(string resource, int page = 1, int pageSize = 20, string[] fieldList = null) where TResult : new() {
            var request = GetListResource(resource, page, pageSize, fieldList);
            var results = Execute<GiantBombResults<TResult>>(request);

            if (results != null && results.StatusCode == GiantBombBase.StatusOk)
                return results.Results;

            return null;
        }

        protected RestRequest GetSingleResource(string resource, int id, string[] fieldList = null) {
            var request = new RestRequest {
                Resource = resource + "/{Id}//",
                DateFormat = "yyyy-MM-dd HH:mm:ss"
            };

            request.AddUrlSegment("Id", id.ToString());

            if (fieldList != null)
                request.AddParameter("field_list", String.Join(",", fieldList));

            return request;
        }

        protected TResult GetSingleResource<TResult>(string resource, int id, string[] fieldList = null) where TResult : class, new() {
            var request = GetSingleResource(resource, id, fieldList);
            var result = Execute<GiantBombResult<TResult>>(request);

            if (result != null && result.StatusCode == GiantBombBase.StatusOk)
                return result.Results;

            return null;
        }
    }
}
