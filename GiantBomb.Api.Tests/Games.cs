using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Model;
using Xunit;
using FluentAssertions;

namespace GiantBomb.Api.Tests {
    
    public class Games : TestBase {

        [Fact]
        public void game_resource_should_return_game_for_33394()
        {
            int gameId = 33394;

            var game = _client.GetGame(gameId);

            game.Should().NotBeNull();
            gameId.Should().Be(game.Id);
            game.Name.Should().Contain("Skyrim");
            game.OriginalReleaseDate.Should().NotBeNull();
            game.OriginalReleaseDate.Value.Should().BeSameDateAs(new DateTime(2011, 11, 11));
            game.Platforms.Should().NotBeNull();
            game.Releases.Should().NotBeNull();
            game.Image.Should().NotBeNull();
            game.Images.Should().NotBeNull();
            game.Developers.Should().NotBeNull();
            game.Publishers.Should().NotBeNull();
            game.Genres.Should().NotBeNull();
            game.SimilarGames.Should().NotBeNull();
        }

        [Fact]
        public void games_resource_should_return_list_of_games() {

            var games = _client.GetGames(pageSize: 2);

            games.Should().NotBeNull();
            games.Count().Should().BeGreaterThan(1);
        }

        [Fact]
        public void game_resource_should_limit_fields_to_id_for_33394() {
            int gameId = 33394;

            var game = _client.GetGame(gameId, new [] { "id" });

            game.Should().NotBeNull();
            gameId.Should().Be(game.Id);
            game.Name.Should().BeNull();
        }

        /// <summary>
        /// BUG: Handle single resource errors
        /// </summary>
        [Fact]
        public async void game_resource_should_return_not_found_for_nonexistent_game()
        {
            int gameId = 50871;

            try
            {
                var game = await _client.GetGameAsync(gameId);
            }
            catch (GiantBombApiException ex)
            {
                GiantBombBase.StatusObjectNotFound.Should().Be(ex.StatusCode);
            }
        }
    }
}
