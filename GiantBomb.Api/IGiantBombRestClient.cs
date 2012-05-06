using System.Collections.Generic;
using GiantBomb.Api.Model;
using RestSharp;

namespace GiantBomb.Api
{
    public interface IGiantBombRestClient
    {
        Platform GetPlatform(int id, string[] limitFields = null);

        /// <summary>
        /// Gets list of games
        /// </summary>        
        /// <returns></returns>
        IEnumerable<Platform> GetPlatforms(int page = 1, int pageSize = 20, string[] limitFields = null);

        /// <summary>
        /// Gets a game with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Game GetGame(int id, string[] limitFields = null);

        /// <summary>
        /// Gets list of games
        /// </summary>        
        /// <returns></returns>
        IEnumerable<Game> GetGames(int page = 1, int pageSize = 20, string[] limitFields = null);

        /// <summary>
        /// Base URL of API (defaults to http://api.giantbomb.com)
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <typeparam name="T">The type of object to create and populate with the returned data.</typeparam>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        T Execute<T>(RestRequest request) where T : new();

        /// <summary>
        /// Execute a manual REST request
        /// </summary>
        /// <param name="request">The RestRequest to execute (will use client credentials)</param>
        IRestResponse Execute(RestRequest request);

        /// <summary>
        /// Searches for a game by keyword and gets paged results
        /// </summary>
        /// <param name="query">Keyword</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<Game> SearchForGames(string query, int page = 1, int pageSize = 20, string[] limitFields = null);

        /// <summary>
        /// Searches for a game by keyword and recursively gets all results to enable sorting, filtering, etc.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<Game> SearchForAllGames(string query, string[] limitFields = null);
    }
}