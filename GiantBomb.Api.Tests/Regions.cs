using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GiantBomb.Api.Tests
{
    public class Regions : TestBase
    {

        [Fact]
        public void region_resource_should_return_region_for_id_1()
        {
            int regionId = 1;

            var region = _client.GetRegion(regionId);

            region.Should().NotBeNull();
            region.Id.Should().Be(regionId);
            region.Name.Should().Be("United States");            
            region.ApiDetailUrl.Should().NotBeNull();
            region.DateAdded.Should().BeAfter(DateTime.MinValue);
            region.DateLastUpdated.Should().BeAfter(DateTime.MinValue);
        }

        [Fact]
        public void regions_resource_should_return_list_of_regions()
        {

            var regions = _client.GetRegions(pageSize: 2);

            regions.Should().NotBeNull();
            regions.Count().Should().BeGreaterThan(1);
        }

        [Fact]
        public void region_resource_should_limit_fields_to_id_for_1()
        {
            int regionId = 1;

            var region = _client.GetRegion(regionId, new[] { "id" });

            region.Should().NotBeNull();
            region.Id.Should().Be(regionId);
            region.Name.Should().BeNull();
        }
    }
}
