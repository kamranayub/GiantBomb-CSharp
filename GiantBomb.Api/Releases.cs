using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;

namespace GiantBomb.Api {
    public partial class GiantBombRestClient {

        /// <summary>
        /// Gets a release with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Release GetRelease(int id, string[] limitFields = null) {
            return GetSingleResource<Release>("release", id, limitFields);
        }

        /// <summary>
        /// Gets all releases for a game with the given ID (multiple requests)
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="limitFields"></param>
        /// <returns></returns>
        /// <remarks>
        /// Unfortunately, this requires multiple requests; 1 for the game, 1 for each of the releases
        /// </remarks>
        public IEnumerable<Release> GetReleasesForGame(int gameId, string[] limitFields = null) {
            var game = this.GetGame(gameId, new [] {"releases"});

            return GetReleasesForGame(game, limitFields);
        }

        /// <summary>
        /// Gets all releases for the given game (multiple requests)
        /// </summary>
        /// <param name="game"></param>
        /// <param name="limitFields"></param>
        /// <returns></returns>
        /// <remarks>
        /// Unfortunately, this requires multiple requests; 1 for each of the releases
        /// </remarks>
        public IEnumerable<Release> GetReleasesForGame(Game game, string[] limitFields = null) {
            if (game == null || game.Releases == null)
                yield break;

            foreach (var r in game.Releases)
                yield return GetRelease(r.Id, limitFields);
        }
    }
}
