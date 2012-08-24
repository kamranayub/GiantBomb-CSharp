using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GiantBomb.Api.Tests
{
    [TestFixture]
    public class Regions : TestBase
    {

        [Test]
        public void region_resource_should_return_region_for_id_1()
        {
            int regionId = 1;

            var region = _client.GetRegion(regionId);

            Assert.IsNotNull(region);
            Assert.AreEqual(regionId, region.Id);
            Assert.AreEqual("United States", region.Name);            
            Assert.IsNotNull(region.ApiDetailUrl);
            Assert.IsNotNull(region.DateAdded);
            Assert.IsNotNull(region.DateLastUpdated);
        }

        [Test]
        public void regions_resource_should_return_list_of_regions()
        {

            var regions = _client.GetRegions(pageSize: 20);

            Assert.IsNotNull(regions);
            Assert.IsTrue(regions.Count() > 1);
        }

        [Test]
        public void region_resource_should_limit_fields_to_id_for_1()
        {
            int regionId = 1;

            var region = _client.GetRegion(regionId, new[] { "id" });

            Assert.IsNotNull(region);
            Assert.AreEqual(regionId, region.Id);
            Assert.IsNullOrEmpty(region.Name);
        }
    }
}
