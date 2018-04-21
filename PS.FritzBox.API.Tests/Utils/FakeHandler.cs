using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.FritzBox.API.Tests.Utils
{
    class FakeHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            var args = new FakeSendArgs( request );
            OnSending( args );
            if ( args.Error != null )
            {
                return Task.FromException<HttpResponseMessage>( args.Error );
            }
            return Task.FromResult( args.Response );
        }

        public event EventHandler<FakeSendArgs> Sending;
        protected virtual void OnSending( FakeSendArgs args )
        {
            Sending?.Invoke( this, args );
        }

    }

}
