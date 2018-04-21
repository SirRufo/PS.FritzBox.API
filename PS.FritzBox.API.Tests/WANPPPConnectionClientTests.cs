﻿using System.Net.Http;
using System.Threading.Tasks;
using PS.FritzBox.API.Tests.Utils;
using Xunit;

namespace PS.FritzBox.API.Tests
{
    public class WANPPPConnectionClientTests
    {
        [Fact]
        public async Task GetStatusInfoAsync_Test1()
        {
            var expected = new ConnectionStatusInfo
            {
                ConnectionStatus = "Moin",
                LastConnectionError = "NONE",
                Uptime = 1000,
            };
            var handler = new FakeHandler();
            handler.Sending += ( s, e ) => { e.Response.Content = new StringContent( expected.ToXmlContent() ); };

            var clt = new WANPPPConnectionClient( "https://fritz.box", 5000, handler );
            var actual = await clt.GetStatusInfoAsync();

            Assert.Equal( expected.ConnectionStatus, actual.ConnectionStatus );
            Assert.Equal( expected.LastConnectionError, actual.LastConnectionError );
            Assert.Equal( expected.Uptime, actual.Uptime );
        }
    }
}
