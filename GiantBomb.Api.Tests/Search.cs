using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GiantBomb.Api.Tests {
    public class Search : TestBase {
        [Fact]
        public void search_resource_should_return_one_result_for_skyrim() {
            var result = _client.SearchForGames("skyrim").ToList();

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result.First().Id.Should().Be(33394);
            result.First().Platforms.Should().NotBeNull();
            result.First().Platforms.First().Should().NotBeNull();
            result.First().Platforms.First().Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public void search_resource_should_return_results_for_mass_effect() {
            var result = _client.SearchForGames("mass effect").ToList();

            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(1);
        }

        [Fact]
        public void search_resource_should_return_all_results_for_rage() {
            var result = _client.SearchForAllGames("rage").ToList();

            result.Should().NotBeNull();
            result.Count.Should().BeGreaterOrEqualTo(43);
        }

        [Fact]
        public void search_resource_should_return_all_results_for_okami() {
            var result = _client.SearchForAllGames("Ōkami").ToList();

            result.Should().NotBeNull();
            result.Count.Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public void search_resource_should_limit_fields_to_id_for_one_result() {
            var result = _client.SearchForGames("skyrim", limitFields: new [] { "id" }).ToList();

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result.First().Id.Should().BeGreaterThan(0);
            result.Select(g => g.Name).Should().OnlyContain(x => x == null);
        }

        [Fact]
        public void search_resource_should_limit_fields_to_id_for_all_result() {
            var result = _client.SearchForAllGames("rage", limitFields: new[] { "id" }).ToList();

            result.Should().NotBeNull();
            result.Count.Should().BeGreaterOrEqualTo(43);
            result.Select(g => g.Id).Should().OnlyContain(x => x > 0);
            result.Select(g => g.Name).Should().OnlyContain(x => x == null);
        }

        /// <summary>
        /// BUGFIX: "mario" returns dup resultset
        /// </summary>
        [Fact]
        public void search_resource_should_not_return_duplicates_for_mario() {
            var result = _client.SearchForAllGames("mario", limitFields: new[] { "id" }).ToList();

            result.Should().NotBeNull();
            result.Select(r => r.Id).Should().OnlyHaveUniqueItems();            
        }
    }
}
