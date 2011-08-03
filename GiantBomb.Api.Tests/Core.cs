using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Tests.Support;
using NUnit.Framework;
using RestSharp;

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

    }
}
