using System;
using System.Collections.Generic;
using System.Linq;

namespace GiantBomb.Api.Model {
    public class Game {
        public int Id { get; set; }

        /// <summary>
        /// Newline-delimited list of aliases
        /// </summary>
        public string Aliases { get; set; }
        public string Name { get; set; }
        public string ApiDetailUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string Deck { get; set; }
        public string Description { get; set; }
        public List<Developer> Developers { get; set; }
        public int? ExpectedReleaseDay { get; set; }
        public int? ExpectedReleaseMonth { get; set; }
        public int? ExpectedReleaseQuarter { get; set; }
        public int? ExpectedReleaseYear { get; set; }
        public List<Genre> Genres { get; set; }
        public Image Image { get; set; }
        public List<Image> Images { get; set; }
        public int NumberOfUserReviews { get; set; }
        public string OriginalGameRating { get; set; }
        public DateTime? OriginalReleaseDate { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Release> Releases { get; set; }
        public string SiteDetailUrl { get; set; }
        public List<Game> SimilarGames { get; set; }
    }

    /// <summary>
    /// Used in lightweight responses where some properties are flattened
    /// </summary>
    internal class GameLite {
        #region Same as Game
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiDetailUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string Deck { get; set; }
        public string Description { get; set; }
        public int? ExpectedReleaseDay { get; set; }
        public int? ExpectedReleaseMonth { get; set; }
        public int? ExpectedReleaseQuarter { get; set; }
        public int? ExpectedReleaseYear { get; set; }
        public Image Image { get; set; }
        public int NumberOfUserReviews { get; set; }
        public DateTime? OriginalReleaseDate { get; set; }
        public string SiteDetailUrl { get; set; }
        #endregion
        public string Platforms { get; set; }        
    }

    internal static class GameExtensions {
        internal static Game ToGame(this GameLite game) {
            var g = new Game() {
                Id = game.Id,
                Name = game.Name,
                ApiDetailUrl = game.ApiDetailUrl,
                DateAdded = game.DateAdded,
                DateLastUpdated = game.DateLastUpdated,
                Deck = game.Deck,
                Description = game.Description,
                ExpectedReleaseDay = game.ExpectedReleaseDay,
                ExpectedReleaseMonth = game.ExpectedReleaseMonth,
                ExpectedReleaseQuarter = game.ExpectedReleaseQuarter,
                ExpectedReleaseYear = game.ExpectedReleaseYear,
                Image = game.Image,
                NumberOfUserReviews = game.NumberOfUserReviews,
                OriginalReleaseDate = game.OriginalReleaseDate,
                SiteDetailUrl = game.SiteDetailUrl
            };

            // Platforms
            if (!String.IsNullOrWhiteSpace(game.Platforms)) {
                g.Platforms =
                    game.Platforms.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => new Platform() { Name = p }).ToList();
            }

            return g;
        }
    }
}
