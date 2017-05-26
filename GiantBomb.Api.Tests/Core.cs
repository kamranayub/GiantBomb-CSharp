using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp.Portable;

namespace GiantBomb.Api.Tests
{
    public class Core : TestBase
    {

        [Test]
        public void giantbomb_should_respond_to_request()
        {
            var request = new RestRequest();
            request.Resource = "games";

            var result = _client.Execute(request);

            Assert.IsNotNull(result.Content);
        }

        [Test]
        public async Task giantbomb_should_respond_to_request_async()
        {
            var request = new RestRequest();
            request.Resource = "games";

            var result = await _client.ExecuteAsync(request);

            Assert.IsNotNull(result.Content);
        }

    }
}
