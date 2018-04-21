using System;
using System.Net.Http;

namespace PS.FritzBox.API.Tests.Utils
{
    class FakeSendArgs
    {
        public FakeSendArgs( HttpRequestMessage request )
        {
            Request = request;
        }

        public HttpRequestMessage Request { get; }
        public HttpResponseMessage Response { get; set; } = new HttpResponseMessage() { Content = new StringContent( "<dummy/>" ) };
        public Exception Error { get; set; }
    }

}
