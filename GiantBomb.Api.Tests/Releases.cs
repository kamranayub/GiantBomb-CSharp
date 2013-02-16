using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GiantBomb.Api.Tests {

    [TestFixture]
    public class Releases : TestBase {

        [Test]
        public void release_resource_should_return_release_for_29726() {
            int releaseId = 29726;

            var release = _client.GetRelease(releaseId);

            Assert.IsNotNull(release);
            Assert.AreEqual(release.Id, releaseId);
            Assert.IsTrue(release.Name.Contains("Morrowind"));
            Assert.IsTrue(release.DateAdded > DateTime.MinValue);
            Assert.IsTrue(release.DateLastUpdated > DateTime.MinValue);
            Assert.IsNotNull(release.Platform, "No platform");
            Assert.IsNotNull(release.Publishers, "No publishers");
            Assert.IsNotNull(release.Developers, "No developers");
        }

        [Test]
        public void GetReleases_should_return_releases_for_17367() {
            int gameId = 17367;

            var releases = _client.GetReleasesForGame(gameId, new [] { "id"});

            Assert.IsNotNull(releases);
            Assert.Greater(releases.Count(), 0);
            Assert.IsTrue(releases.Any(r => r.Id == 29726));
        }
    }
}
