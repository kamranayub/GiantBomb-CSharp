using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiantBomb.Api.Tests.Support;
using FluentAssertions;

namespace GiantBomb.Api.Tests {
    public class TestBase
    {
        protected GiantBombRestClient _client;
        
        public TestBase()
        {
            var token = Token.GetToken();

            token.Should().NotBeNull("because token should be in directory {0} or environment variable {1}", 
                Token.SearchPath, Token.EnvironmentVariable);

            _client = new GiantBombRestClient(token);
        }
    }
}
