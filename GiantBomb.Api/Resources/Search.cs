using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiantBomb.Api.Model;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {

        public IEnumerable<Game> SearchForGames(string query, int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null)
        {
            return SearchForGamesAsync(query, page, pageSize, limitFields).WaitForResult();
        }

        public Task<IEnumerable<Game>> SearchForGamesAsync(string query, int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null)
        {
            return InternalSearchForGames(query, page, pageSize, limitFields)
                .ContinueWith<IEnumerable<Game>>(task =>
                {
                    var result = task.Result;
                    if (result.StatusCode == GiantBombBase.StatusOk)
                        return result.Results;

                    return null;
                });
        }

        public IEnumerable<Game> SearchForAllGames(string query, string[] limitFields = null)
        {
            return SearchForAllGamesAsync(query, limitFields).WaitForResult();
        }

        public Task<IEnumerable<Game>> SearchForAllGamesAsync(string query, string[] limitFields = null) {
            var results = new List<Game>();
            return InternalSearchForGames(query, limitFields: limitFields).
                ContinueWith<IEnumerable<Game>>(task =>
                {
                    var result = task.Result;
                    if (result == null || result.StatusCode != GiantBombBase.StatusOk)
                        return null;

                    results.AddRange(result.Results);

                    if (result.NumberOfTotalResults > result.Limit)
                    {
                        double remaining = Math.Ceiling(Convert.ToDouble(result.NumberOfTotalResults) / Convert.ToDouble(result.Limit));

                        // Start on page 2
                        for (var i = 2; i <= remaining; i++)
                        {
                            // HACK: This is not ideal. We'd like this to be a continue chain, but that is a future
                            // enhancement.
                            result = InternalSearchForGames(query, i, result.Limit, limitFields).WaitForResult();
                            if (result.StatusCode != GiantBombBase.StatusOk)
                                break;

                            results.AddRange(result.Results);
                        }
                    }

                    // FIX: Clean duplicates that GiantBomb returns
                    // Can only do it if we have IDs in the resultset
                    if (limitFields == null || limitFields.Contains("id"))
                    {
                        results = results.Distinct(new GameDistinctComparer()).ToList();
                    }

                    return results;
                });
        }

        internal Task<GiantBombResults<Game>> InternalSearchForGames(string query, int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null)
        {
            var request = GetListResource("search", page, pageSize, limitFields);

            request.AddParameter("query", query);
            request.AddParameter("resources", "game");

            return ExecuteAsync<GiantBombResults<Game>>(request);
        }
    }
}
