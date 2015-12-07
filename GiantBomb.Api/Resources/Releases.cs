using System.Collections.Generic;
using System.Threading.Tasks;
using GiantBomb.Api.Model;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {

        public Release GetRelease(int id, string[] limitFields = null)
        {
            return GetReleaseAsync(id, limitFields).WaitForResult();
        }

        public Task<Release> GetReleaseAsync(int id, string[] limitFields = null) {
            return GetSingleResourceAsync<Release>("release", ResourceTypes.Releases, id, limitFields);
        }

        public IEnumerable<Release> GetReleasesForGame(int gameId, string[] limitFields = null)
        {
            return GetReleasesForGameAsync(gameId, limitFields).WaitForResult();
        }

        public Task<IEnumerable<Release>> GetReleasesForGameAsync(int gameId, string[] limitFields = null)
        {
            var filter = new Dictionary<string, object>()
                             {
                                 {"game", gameId}
                             };

            return GetListResourceAsync<Release>("releases", fieldList: limitFields, filterOptions: filter);
        }

        public IEnumerable<Release> GetReleasesForGame(Game game, string[] limitFields = null)
        {
            return GetReleasesForGame(game.Id, limitFields);
        }

        public Task<IEnumerable<Release>> GetReleasesForGameAsync(Game game, string[] limitFields = null)
        {
            return GetReleasesForGameAsync(game.Id, limitFields);
        }
    }
}
