using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GiantBomb.Api.Model;
using GiantBomb.Api.Serialization;
using RestSharp;
using RestSharp.Serializers.Json;

namespace GiantBomb.Api
{
    public partial class GiantBombRestClient : IGiantBombRestClient
    {
        private readonly RestClient _client;

        /// <summary>
        /// Base URL of API (defaults to http://www.giantbomb.com/api/)
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Your GiantBomb API token
        /// </summary>
        private string ApiKey { get; }


        /// <summary>
        /// Create a new Rest client with your API token and custom base URL
        /// </summary>
        /// <param name="apiToken">Your secret API token</param>
        /// <param name="baseUrl">The base API URL, for example, pre-release API versions</param>
        public GiantBombRestClient(string apiToken, Uri baseUrl)
        {
            BaseUrl = baseUrl.ToString();
            ApiKey = apiToken;

            var version = typeof(GiantBombRestClient).GetTypeInfo().Assembly.GetName().Version;

            var clientOptions = new RestClientOptions(baseUrl)
            {
                UserAgent = "giantbomb-csharp/" + version
            };

            _client = new RestClient(clientOptions);
            
            // API token is used on every request
            _client.AddDefaultParameter("api_key", ApiKey);
            _client.AddDefaultParameter("format", "json");
            var jsonOptions = new System.Text.Json.JsonSerializerOptions()
            {
                PropertyNamingPolicy = SnakeCaseNamingPolicy.SnakeCase,
                DictionaryKeyPolicy = SnakeCaseNamingPolicy.SnakeCase,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
            };

            jsonOptions.Converters.Add(new DateTimeConverter());
            jsonOptions.Converters.Add(new BoolConverter());
            _client.UseSerializer(() => new SystemTextJsonSerializer(jsonOptions));
        }

        public GiantBombRestClient(string apiToken)
            : this(apiToken, new Uri("https://www.giantbomb.com/api/"))
        {

        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public virtual T Execute<T>(RestRequest request) where T : new()
        {
            return ExecuteAsync<T>(request).Result;
        }

        /// <summary>
        /// Execute a manual REST request (async)
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public virtual async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            RestResponse response = null;

            try
            {
                response = await _client.ExecuteAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new GiantBombHttpException(
                    "Unable to retrieve response from GiantBomb.", ex, response?.Content);
            }

            // Deserialize original requested type
            RestResponse<T> deserializedResponse = null;
            Exception deserializeEx = null;
            try
            {
                deserializedResponse = _client.Deserialize<T>(response);
            }
            catch (Exception dex)
            {
                deserializeEx = dex;
            }

            // Check ErrorException
            if (deserializedResponse.ErrorException != null)
            {
                deserializeEx = deserializedResponse.ErrorException;
            }

            T data = deserializedResponse.Data;
            if (data == null)
            {
                // handle GiantBomb raw errors without result wrapper
                try
                {
                    var responseData = _client.Deserialize<GiantBombBase>(response).Data;

                    if (responseData != null && !String.IsNullOrWhiteSpace(responseData.Error) && responseData.Error != GiantBombBase.ErrorOK)
                    {
                        throw new GiantBombApiException(responseData.StatusCode, responseData.Error + Environment.NewLine + response.Content);
                    }
                }
                catch (Exception ex)
                {
                    if (ex is GiantBombApiException)
                    {
                        throw;
                    }
                    else
                    {
                        throw new GiantBombHttpException("Could not deserialize response", ex);
                    }
                }

                throw new GiantBombHttpException("Bad content", deserializeEx, response.Content);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new GiantBombHttpException($"GiantBomb returned status code {response.StatusCode}: {response.StatusDescription}", response.Content);
            }

            return data;
        }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public virtual RestResponse Execute(RestRequest request)
        {
            return ExecuteAsync(request).Result;
        }

        /// <summary>
        /// Execute a manual REST request (async)
        /// </summary>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        public virtual async Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            return await _client.ExecuteAsync(request).ConfigureAwait(false);
        }

        public virtual RestRequest GetListResource(string resource, int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] fieldList = null, IDictionary<string, SortDirection> sortOptions = null, IDictionary<string, object> filterOptions = null)
        {
            if (pageSize > GiantBombBase.DefaultLimit)
                throw new ArgumentOutOfRangeException("pageSize", "Page size cannot be greater than " + GiantBombBase.DefaultLimit + ".");

            var request = new RestRequest
            {
                Resource = resource + "/"
            };

            if (page > 1)
            {

                // HACK: Giant Bomb uses `page` for search instead of `offset`
                if (resource == "search")
                {
                    request.AddParameter("page", page);
                }
                else
                {
                    request.AddParameter("offset", pageSize * (page - 1));
                }
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

            foreach (var kv in sortOptions)
                sortDictionary.Add(kv.Key, kv.Value == SortDirection.Ascending ? "asc" : "desc");

            return BuildKeyValueListForUrl(sortDictionary);
        }

        public virtual RestRequest GetSingleResource(string resource, int resourceId, int id, string[] fieldList = null)
        {
            var request = new RestRequest
            {
                Resource = resource + "/{ResourceId}-{Id}/"
            };

            request.AddUrlSegment("ResourceId", resourceId.ToString(CultureInfo.InvariantCulture));
            request.AddUrlSegment("Id", id.ToString(CultureInfo.InvariantCulture));

            if (fieldList != null)
                request.AddParameter("field_list", String.Join(",", fieldList));

            return request;
        }

        public virtual IEnumerable<TResult> GetListResource<TResult>(string resource, int page = 1,
            int pageSize = GiantBombBase.DefaultLimit, string[] fieldList = null,
            IDictionary<string, SortDirection> sortOptions = null, IDictionary<string, object> filterOptions = null)
            where TResult : new()
        {
            return GetListResourceAsync<TResult>(resource, page, pageSize, fieldList, sortOptions, filterOptions).Result;
        }

        public virtual async Task<IEnumerable<TResult>> GetListResourceAsync<TResult>(string resource, int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] fieldList = null, IDictionary<string, SortDirection> sortOptions = null, IDictionary<string, object> filterOptions = null) where TResult : new()
        {
            var request = GetListResource(resource, page, pageSize, fieldList, sortOptions, filterOptions);
            var results = await ExecuteAsync<GiantBombResults<TResult>>(request).ConfigureAwait(false);

            if (results != null && results.StatusCode == GiantBombBase.StatusOk)
                return results.Results;

            if (results != null && results.StatusCode != GiantBombBase.StatusOk)
                throw new GiantBombApiException(results.StatusCode, results.Error);

            return null;
        }

        public virtual TResult GetSingleResource<TResult>(string resource, int resourceId, int id,
            string[] fieldList = null) where TResult : class, new()
        {
            return GetSingleResourceAsync<TResult>(resource, resourceId, id, fieldList).Result;
        }

        public virtual async Task<TResult> GetSingleResourceAsync<TResult>(string resource, int resourceId, int id, string[] fieldList = null) where TResult : class, new()
        {
            var request = GetSingleResource(resource, resourceId, id, fieldList);
            var result = await ExecuteAsync<GiantBombResult<TResult>>(request).ConfigureAwait(false);

            if (result != null && result.StatusCode == GiantBombBase.StatusOk)
                return result.Results;

            if (result != null && result.StatusCode != GiantBombBase.StatusOk)
                throw new GiantBombApiException(result.StatusCode, result.Error);

            return null;
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
