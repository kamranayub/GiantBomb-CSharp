using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Tests.Support;
using NUnit.Framework;
using RestSharp;

namespace GiantBomb.Api.Tests
{
    public class Core
    {

        [Test]
        public void giantbomb_should_respond_to_request()
        {
            var token = Token.GetToken();
            var client = new GiantBombRestClient(token);

            var request = new RestRequest();            
            request.Resource = "games";

            var result = client.Execute(request);

            Assert.IsNotNull(result.Content);
        }

    }
}
