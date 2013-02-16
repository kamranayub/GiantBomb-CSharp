using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Tests.Support;
using NUnit.Framework;

namespace GiantBomb.Api.Tests {
    [TestFixture]
    public class TestBase
    {

        protected GiantBombRestClient _client;

        [SetUp]
        public void Setup()
        {
            var token = Token.GetToken();
            _client = new GiantBombRestClient(token, new Uri("http://www.giantbomb.com/api/"));
        }
    }
}
