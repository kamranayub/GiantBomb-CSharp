using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;
using RestSharp;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {
        public IEnumerable<Game> SearchForGames(string query, int page = 1, int pageSize = 20) {
            var result = InternalSearchForGames(query, page, pageSize);

            if (result.StatusCode == GiantBombBase.StatusOk)
                return result.Results;

            return null;
        }

        protected GiantBombResults<Game> InternalSearchForGames(string query, int page = 1, int pageSize = 20) {
            var request = GetListResource("search", page, pageSize);

            request.AddParameter("query", query);
            request.AddParameter("resources", "game");

            return Execute<GiantBombResults<Game>>(request);
        }

        public IEnumerable<Game> SearchForAllGames(string query) {
            var results = new List<Game>();
            var result = InternalSearchForGames(query);

            if (result.StatusCode != GiantBombBase.StatusOk)
                return null;

            results.AddRange(result.Results);

            if (result.NumberOfTotalResults > result.Limit) {
                double remaining = Math.Ceiling(Convert.ToDouble(result.NumberOfTotalResults) / Convert.ToDouble(result.Limit));

                // Start on page 2
                for (var i = 2; i <= remaining; i++) {
                    result = InternalSearchForGames(query, i);

                    if (result.StatusCode != GiantBombBase.StatusOk)
                        break;

                    results.AddRange(result.Results);
                }
            }

            return results;
        }
    }
}
