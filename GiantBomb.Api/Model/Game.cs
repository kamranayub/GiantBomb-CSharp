using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiantBomb.Api.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiDetailUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string Deck { get; set; }
        public string Description { get; set; }
        public int? ExpectedReleaseMonth { get; set; }
        public int? ExpectedReleaseQuarter { get; set; }
        public int? ExpectedReleaseYear { get; set; }
        public Image Image { get; set; }
        public List<Image> Images { get; set; }
        public int NumberOfUserReviews { get; set; }
        public DateTime? OriginalReleaseDate { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<Release> Releases { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class GameResult : GiantBombBase
    {
        public Game Results { get; set; }
    }

    public class GamesResult : GiantBombBase
    {
        public List<Game> Results { get; set; } 
    }
}
