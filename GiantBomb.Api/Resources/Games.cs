using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;
using RestSharp;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {
        public Game GetGame(int id, string[] limitFields = null) {
            return GetSingleResource<Game>("game", ResourceTypes.Games, id, limitFields);
        }

        public IEnumerable<Game> GetGames(int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null) {
            var liteGames = GetListResource<Game>("games", page, pageSize, limitFields);

            if (liteGames == null) return null;

            return liteGames;
        }
    }
}
