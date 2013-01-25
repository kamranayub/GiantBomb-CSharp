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
            Assert.AreEqual(new DateTime(2011, 11, 11), game.OriginalReleaseDate.Value);
            Assert.IsNotNull(game.Platforms);
            Assert.IsNotNull(game.Releases);
            Assert.IsNotNull(game.Image);
            Assert.IsNotNull(game.Images);
            Assert.IsNotNull(game.Developers);
            Assert.IsNotNull(game.Publishers);
            Assert.IsNotNull(game.Genres);
            Assert.IsNotNull(game.SimilarGames);
        }

        [Test]
        public void games_resource_should_return_list_of_games() {

            var games = _client.GetGames();

            Assert.IsNotNull(games);
            Assert.IsTrue(games.Count() > 1);
        }

        [Test]
        public void game_resource_should_limit_fields_to_id_for_33394() {
            int gameId = 33394;

            var game = _client.GetGame(gameId, new [] { "id" });

            Assert.IsNotNull(game);
            Assert.AreEqual(gameId, game.Id);
            Assert.IsNullOrEmpty(game.Name);            
        }
    }
}
