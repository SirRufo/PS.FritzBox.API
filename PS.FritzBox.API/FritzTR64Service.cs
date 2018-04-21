using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;

[assembly: InternalsVisibleTo( "PS.FritzBox.API.Tests" )]

namespace PS.FritzBox.API
{
    /// <summary>
    /// base class for fritzbox tr64 services
    /// </summary>
    public abstract class FritzTR64Client
    {
        private readonly HttpClientHandler _handler;
        #region Construction / Destruction

        /// <summary>
        /// constructor for the tr64 service
        /// </summary>
        /// <param name="url">the service url</param>
        /// <param name="timeout">the timeout in milliseconds</param>
        public FritzTR64Client( string url, int timeout ) : this( url, timeout, new HttpClientHandler() )
        {
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <param name="handler"></param>
        public FritzTR64Client( string url, int timeout, HttpClientHandler handler )
        {
            this.Url = String.Concat( url, this.ControlUrl );
            this.Timeout = timeout;
            _handler = handler;
        }

        #endregion

        /// <summary>
        /// Gets or sets the service url
        /// </summary>
        public string Url { get; internal set; }

        /// <summary>
        /// gets or sets the request timeout
        /// </summary>
        public int Timeout { get; internal set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// gets or sets the control url
        /// </summary>
        protected abstract string ControlUrl { get; }

        /// <summary>
        /// Gets or sets the request namespace
        /// </summary>
        protected abstract string RequestNameSpace { get; }

        /// <summary>
        /// Method to invoke the given action
        /// </summary>
        /// <param name="Action"></param>
        /// <returns></returns>
        internal async Task<XDocument> InvokeAsync( string action, params SoapRequestParameter[] parameter )
        {
            SoapClient client = new SoapClient( _handler );
            SoapRequestParameters parameters = new SoapRequestParameters();

            parameters.UserName = this.UserName;
            parameters.Password = this.Password;

            parameters.RequestNameSpace = this.RequestNameSpace;
            parameters.SoapAction = $"{this.RequestNameSpace}#{action}";
            parameters.Action = $"{action}";
            if ( parameter != null )
                parameters.Parameters.AddRange( parameter );

            XDocument soapResult = await client.InvokeAsync( this.Url, parameters ).ConfigureAwait( false );

            this.ParseSoapFault( soapResult );

            return soapResult;
        }

        internal void ParseSoapFault( XDocument document )
        {
            if ( document.Descendants( "Fault" ).Count() > 0 )
            {
                string code = document.Descendants( "faultcode" ).First().Value;
                string text = document.Descendants( "faultstring" ).First().Value;

                throw new SoapFaultException( code, text );
            }
        }
    }
}
