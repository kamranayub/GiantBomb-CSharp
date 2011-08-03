using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;
using RestSharp;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {

        /// <summary>
        /// Gets a game with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Game GetGame(int id) {
            var request = new RestRequest {
                Resource = "game/{GameId}//",
                RequestFormat = DataFormat.Json,
                Method = Method.GET
            };

            request.AddUrlSegment("GameId", id.ToString());

            var result = Execute<GameResult>(request);

            if (result.StatusCode == GiantBombBase.StatusOk)
                return result.Results;
            else {
                return null;
            }
        }

        /// <summary>
        /// Gets list of games
        /// </summary>        
        /// <returns></returns>
        public IEnumerable<Game> GetGames(int page = 1, int pageSize = 25) {
            var request = new RestRequest {
                Resource = "games//",
                RequestFormat = DataFormat.Json,
                Method = Method.GET
            };

            if (page > 1) {
                request.AddParameter("offset", pageSize * (page - 1));
            }

            request.AddParameter("limit", pageSize);

            var result = Execute<GamesResult>(request);

            if (result.StatusCode == GiantBombBase.StatusOk)
                return result.Results;
            else {
                return null;
            }
        }
    }
}
