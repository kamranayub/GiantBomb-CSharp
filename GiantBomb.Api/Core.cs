using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using GiantBomb.Api.Model;
using RestSharp;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient : IGiantBombRestClient {
        private readonly RestClient _client;

        /// <summary>
        /// Base URL of API (defaults to http://api.giantbomb.com)
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Your GiantBomb API token
        /// </summary>
        private string ApiKey { get; set; }        

        /// <summary>
        /// Create a new Rest client with your API token and custom base URL
        /// </summary>
        /// <param name="apiToken">Your secret API token</param>
        /// <param name="baseUrl">The base API URL, for example, pre-release API versions</param>
        public GiantBombRestClient(string apiToken, Uri baseUrl) {
            BaseUrl = baseUrl.ToString();
            ApiKey = apiToken;

            var assembly = Assembly.GetExecutingAssembly();
            var version = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            _client = new RestClient
                          {
                              UserAgent = "giantbomb-csharp/" + version, 
                              BaseUrl = BaseUrl
                          };

            // API token is used on every request
            _client.AddDefaultParameter("api_key", ApiKey);
            _client.AddDefaultParameter("format", "json");
        }

        public GiantBombRestClient(string apiToken)
            : this(apiToken, new Uri("http://api.giantbomb.com")) {

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
        public IRestResponse Execute(RestRequest request) {
            return _client.Execute(request);
        }
#endif

        public virtual RestRequest GetListResource(string resource, int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] fieldList = null, IDictionary<string, SortDirection> sortOptions = null, IDictionary<string, object> filterOptions = null) {
            if (pageSize > 20)
                throw new ArgumentOutOfRangeException("pageSize", "Page size cannot be greater than 20.");

            var request = new RestRequest {
                Resource = resource + "/",
                DateFormat = "yyyy-MM-dd HH:mm:ss"
            };

            if (page > 1) {
                request.AddParameter("offset", pageSize * (page - 1));
            }

            request.AddParameter("limit", pageSize);

            if (fieldList != null)
                request.AddParameter("field_list", String.Join(",", fieldList));

            if (sortOptions != null)
                request.AddParameter("sort", BuildKeyValueListForUrl(sortOptions));

            if (filterOptions != null)
                request.AddParameter("filter", BuildKeyValueListForUrl(filterOptions));

            return request;
        }

        private string BuildKeyValueListForUrl(IEnumerable<KeyValuePair<string, object>> dictionary)
        {
            // format is like <key>:<value>,<key>:<value>
            return String.Join(",", (from pair in dictionary
                   select pair.Key + ":" + pair.Value).ToArray());
        }

        private string BuildKeyValueListForUrl(IEnumerable<KeyValuePair<string, SortDirection>> sortOptions)
        {

            var sortDictionary = new Dictionary<string, object>();

            foreach(var kv in sortOptions)
                sortDictionary.Add(kv.Key, kv.Value == SortDirection.Ascending ? "asc" : "desc");

            return BuildKeyValueListForUrl(sortDictionary);
        }

        public virtual IEnumerable<TResult> GetListResource<TResult>(string resource, int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] fieldList = null, IDictionary<string, SortDirection> sortOptions = null, IDictionary<string, object> filterOptions = null) where TResult : new() {
            var request = GetListResource(resource, page, pageSize, fieldList, sortOptions, filterOptions);
            var results = Execute<GiantBombResults<TResult>>(request);

            if (results != null && results.StatusCode == GiantBombBase.StatusOk)
                return results.Results;

            return null;
        }

        public virtual RestRequest GetSingleResource(string resource, int resourceId, int id, string[] fieldList = null) {
            var request = new RestRequest {
                Resource = resource + "/{ResourceId}-{Id}/",
                DateFormat = "yyyy-MM-dd HH:mm:ss"
            };

            request.AddUrlSegment("ResourceId", resourceId.ToString(CultureInfo.InvariantCulture));
            request.AddUrlSegment("Id", id.ToString(CultureInfo.InvariantCulture));

            if (fieldList != null)
                request.AddParameter("field_list", String.Join(",", fieldList));

            return request;
        }

        public virtual TResult GetSingleResource<TResult>(string resource, int resourceId, int id, string[] fieldList = null) where TResult : class, new() {
            var request = GetSingleResource(resource, resourceId, id, fieldList);
            var result = Execute<GiantBombResult<TResult>>(request);

            if (result != null && result.StatusCode == GiantBombBase.StatusOk)
                return result.Results;

            return null;
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
