using System.Collections.Generic;
using System.Threading.Tasks;
using GiantBomb.Api.Model;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {

        public Game GetGame(int id, string[] limitFields = null)
        {
            return GetGameAsync(id, limitFields).WaitForResult();
        }

        public Task<Game> GetGameAsync(int id, string[] limitFields = null)
        {
            return GetSingleResourceAsync<Game>("game", ResourceTypes.Games, id, limitFields);
        }

        public IEnumerable<Game> GetGames(int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null)
        {
            return GetGamesAsync(page, pageSize, limitFields).WaitForResult();
        }

        public Task<IEnumerable<Game>> GetGamesAsync(int page = 1, int pageSize = GiantBombBase.DefaultLimit, string[] limitFields = null)
        {
            return GetListResourceAsync<Game>("games", page, pageSize, limitFields);
        }
    }
}
