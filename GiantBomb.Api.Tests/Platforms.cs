using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GiantBomb.Api.Tests {

    [TestFixture]
    public class Platforms : TestBase {

        [Test]
        public void platform_resource_should_return_platform_for_94()
        {
            int platformId = 94;

            var platform = _client.GetPlatform(platformId);

            Assert.IsNotNull(platform);
            Assert.AreEqual(platformId, platform.Id);
            Assert.AreEqual("PC", platform.Name);
            Assert.AreEqual("PC", platform.Abbreviation);
            Assert.IsNotNull(platform.ApiDetailUrl);
            Assert.IsNotNull(platform.DateAdded);
            Assert.IsNotNull(platform.DateLastUpdated);
            Assert.IsNotNull(platform.Deck);
            Assert.IsNotNull(platform.Description);
            Assert.IsNotNull(platform.Image);            
            Assert.IsNotNull(platform.ReleaseDate);            
            Assert.IsNotNull(platform.SiteDetailUrl);
        }

        [Test]
        public void platforms_resource_should_return_list_of_platforms() {

            var platforms = _client.GetPlatforms(pageSize: 20);

            Assert.IsNotNull(platforms);
            Assert.IsTrue(platforms.Count() > 1);
        }
    }
}
