using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace GiantBomb.Api.Tests {
    
    public class Releases : TestBase {

        [Fact]
        public void release_resource_should_return_release_for_29726() {
            int releaseId = 29726;

            var release = _client.GetRelease(releaseId);

            release.Should().NotBeNull();
            release.Id.Should().Be(releaseId);
            release.Name.Should().Contain("Morrowind");
            release.DateAdded.Should().BeAfter(DateTime.MinValue);
            release.DateLastUpdated.Should().BeAfter(DateTime.MinValue);
            release.Platform.Should().NotBeNull();
            release.Publishers.Should().NotBeNull();
            release.Developers.Should().NotBeNull();
        }

        [Fact]
        public void GetReleases_should_return_releases_for_17367() {
            int gameId = 17367;

            var releases = _client.GetReleasesForGame(gameId, new [] { "id"});

            releases.Should().NotBeNull();
            releases.Count().Should().BeGreaterThan(0);
            releases.Select(r => r.Id).Should().Contain(29726);            
        }
    }
}
