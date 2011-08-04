using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GiantBomb.Api.Tests {
    public class Search : TestBase {
        [Test]
        public void search_resource_should_return_one_result_for_skyrim() {
            var result = _client.SearchForGames("skyrim").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(33394, result.First().Id);
        }

        [Test]
        public void search_resource_should_return_results_for_mass_effect() {
            var result = _client.SearchForGames("mass effect").ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 1);            
        }

        [Test]
        public void search_resource_should_return_all_results_for_rage() {
            var result = _client.SearchForAllGames("rage").ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(43, result.Count);
        }
    }
}
