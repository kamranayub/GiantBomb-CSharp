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
        public Game GetGame(int id, string[] limitFields = null) {
            return GetSingleResource<Game>("game", id, limitFields);
        }

        /// <summary>
        /// Gets list of games
        /// </summary>        
        /// <returns></returns>
        public IEnumerable<Game> GetGames(int page = 1, int pageSize = 20, string[] limitFields = null) {
            return GetListResource<Game>("games", page, pageSize, limitFields);
        }
    }
}
