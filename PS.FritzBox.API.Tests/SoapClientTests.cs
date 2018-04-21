using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PS.FritzBox.API.Tests.Utils;
using Xunit;

namespace PS.FritzBox.API.Tests
{
    public class SoapClientTests
    {
        [Fact]
        public async Task Test1()
        {
            var requests = new List<HttpRequestMessage>();
            var handler = new FakeHandler();
            handler.Sending += ( s, e ) =>
            {
                requests.Add( e.Request );
            };
            var clt = new SoapClient( handler );
            var response = await clt.InvokeAsync( "https://example.com", new SoapRequestParameters() );

            Assert.Single( requests );
            var r = requests.First();
            Assert.Equal( HttpMethod.Post, r.Method );
            Assert.Equal( new Uri( "https://example.com" ), r.RequestUri );
        }
    }
}
