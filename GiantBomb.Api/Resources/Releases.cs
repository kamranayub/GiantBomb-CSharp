using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {

        public Release GetRelease(int id, string[] limitFields = null) {

            // Workaround until Issue#9 gets fixed
            var filter = new Dictionary<string, object>()
                             {
                                 {"id", id}
                             };

            return GetListResource<Release>("releases", fieldList: limitFields, filterOptions: filter).FirstOrDefault();

            // TODO Use once GB fixes their release API
            // return GetSingleResource<Release>("release", ResourceTypes.Releases, id, limitFields);
        }

        public IEnumerable<Release> GetReleasesForGame(int gameId, string[] limitFields = null)
        {
            var filter = new Dictionary<string, object>()
                             {
                                 {"game", gameId}
                             };

            return GetListResource<Release>("releases", fieldList: limitFields, filterOptions: filter);
        }

        public IEnumerable<Release> GetReleasesForGame(Game game, string[] limitFields = null)
        {
            return GetReleasesForGame(game.Id, limitFields);
        }
    }
}
