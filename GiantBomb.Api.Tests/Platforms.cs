using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GiantBomb.Api.Tests {
    
    public class Platforms : TestBase {

        [Fact]
        public void platform_resource_should_return_platform_for_94()
        {
            int platformId = 94;

            var platform = _client.GetPlatform(platformId);

            platform.Should().NotBeNull();
            platform.Id.Should().Be(platformId);
            platform.Name.Should().Be("PC");
            platform.Abbreviation.Should().Be("PC");
            platform.ApiDetailUrl.Should().NotBeNull();
            platform.DateAdded.Should().BeAfter(DateTime.MinValue);
            platform.DateLastUpdated.Should().BeAfter(DateTime.MinValue);
            platform.Deck.Should().NotBeNull();
            platform.Description.Should().NotBeNull();
            platform.Image.Should().NotBeNull();            
            platform.ReleaseDate.Should().NotBeNull();            
            platform.SiteDetailUrl.Should().NotBeNull();
        }

        [Fact]
        public void platforms_resource_should_return_list_of_platforms() {

            var platforms = _client.GetPlatforms(pageSize: 2);

            platforms.Should().NotBeNull();
            platforms.Count().Should().BeGreaterThan(1);
        }

        [Fact]
        public void platform_resource_should_limit_fields_to_id_for_94() {
            int platformId = 94;

            var platform = _client.GetPlatform(platformId, new[] { "id" });

            platform.Should().NotBeNull();
            platformId.Should().Be(platform.Id);
            platform.Name.Should().BeNull();
        }
    }
}
