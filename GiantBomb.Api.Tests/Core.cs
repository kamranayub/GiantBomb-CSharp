using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;
using Xunit;
using FluentAssertions;
using GiantBomb.Api.Tests.Support;

namespace GiantBomb.Api.Tests
{
    public class Core : TestBase
    {
        [Fact]
        public void giantbomb_should_respond_to_request()
        {
            var request = new RestRequest();
            request.Resource = "games";

            var result = _client.Execute(request);

            result.Content.Should().NotBeNull();
        }

        [Fact]
        public async Task giantbomb_should_respond_to_request_async()
        {
            var request = new RestRequest();
            request.Resource = "games";

            var result = await _client.ExecuteAsync(request);

            result.Content.Should().NotBeNull();
        }

    }
}
