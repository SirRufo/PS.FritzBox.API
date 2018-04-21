using System.Net.Http;
using System.Threading.Tasks;
using PS.FritzBox.API.Tests.Utils;
using Xunit;

namespace PS.FritzBox.API.Tests
{
    public class WANCommonInterfaceConfigClientTests
    {
        [Fact]
        public async Task GetCommonLinkPropertiesAsync_Test1()
        {
            var expected = new CommonLinkProperties
            {
                WANAccessType = "DSL",
                Layer1UpstreamMaxBitRate = 1024,
                Layer1DownstreamMaxBitRate = 6192,
                PhysicalLinkStatus = "Up",
            };
            var handler = new FakeHandler();
            handler.Sending += ( s, e ) =>
            {
                e.Response.Content = new StringContent( expected.ToXmlContent() );
            };
            var clt = new WANCommonInterfaceConfigClient( "https://fritz.box:453", 5000, handler );
            var result = await clt.GetCommonLinkPropertiesAsync().ConfigureAwait( false );
            Assert.Equal( expected.WANAccessType, result.WANAccessType );
            Assert.Equal( expected.Layer1UpstreamMaxBitRate, result.Layer1UpstreamMaxBitRate );
            Assert.Equal( expected.Layer1DownstreamMaxBitRate, result.Layer1DownstreamMaxBitRate );
            Assert.Equal( expected.PhysicalLinkStatus, result.PhysicalLinkStatus );
        }
    }

}
