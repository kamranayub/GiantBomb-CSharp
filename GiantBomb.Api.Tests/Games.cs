using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GiantBomb.Api.Tests {

    [TestFixture]
    public class Games : TestBase {

        [Test]
        public void game_resource_should_return_game_for_33394()
        {
            int gameId = 33394;

            var game = _client.GetGame(gameId);

            Assert.IsNotNull(game);
            Assert.AreEqual(gameId, game.Id);
            Assert.IsTrue(game.Name.Contains("Skyrim"));
            Assert.IsNotNull(game.OriginalReleaseDate);
            Assert.IsNotNull(game.Platforms);
            Assert.IsNotNull(game.Releases);
            Assert.IsNotNull(game.Image);
            Assert.IsNotNull(game.Images);
        }

        [Test]
        public void games_resource_should_return_list_of_games() {

            var games = _client.GetGames(pageSize: 20);

            Assert.IsNotNull(games);
            Assert.IsTrue(games.Count() > 1);
        }
    }
}
