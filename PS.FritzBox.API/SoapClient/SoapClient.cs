﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class for accessing soap actions
    /// </summary>
    internal class SoapClient
    {
        /// <summary>
        /// Method to execute the soap request
        /// </summary>
        /// <param name="parameters">the request parameters</param>
        /// <returns>the result of the call</returns>
        public async Task<XDocument> InvokeAsync( string url, SoapRequestParameters parameters )
        {
            string envelope = this.CreateEnvelope( parameters );
            return await this.ExecuteAsync( envelope, url, parameters );
        }

        /// <summary>
        /// Method to create the envelope
        /// </summary>
        /// <param name="parameters">the request parameters</param>
        /// <returns></returns>
        private string CreateEnvelope( SoapRequestParameters parameters )
        {
            var sb = new StringBuilder();
            sb.Append( @"<?xml version='1.0' encoding='UTF-8'?>
                         <soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
                         xmlns:xsd='http://www.w3.org/2001/XMLSchema'
                         xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
                         <soap:Body>" );
            sb.Append( $" <{parameters.Action} xmlns='{parameters.RequestNameSpace}'>" );
            foreach ( SoapRequestParameter parameter in parameters.Parameters )
                sb.Append( $"<{parameter.ParameterName}>{parameter.ParameterValue}</{parameter.ParameterName}>" );
            sb.Append( $"</{parameters.Action}>" );
            sb.Append( @"</soap:Body></soap:Envelope>" );

            return sb.ToString();
        }


        /// <summary>
        /// Method to execute a given soap request
        /// </summary>
        /// <param name="xmlSOAP">the soap request</param>
        /// <param name="url">the soap url</param>
        /// <param name="parameters">the parameters</param>
        /// <returns></returns>
        private async Task<XDocument> ExecuteAsync( string xmlSOAP, string url, SoapRequestParameters parameters )
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            handler.Credentials = parameters.Credentials;

            using ( System.Net.Http.HttpClient client = new HttpClient( handler ) )
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri( url ),
                    Method = HttpMethod.Post
                };

                request.Content = new StringContent( xmlSOAP, Encoding.UTF8, "text/xml" );
                request.Headers.Clear();
                client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "text/xml" ) );
                request.Content.Headers.ContentType = new MediaTypeHeaderValue( "text/xml" );
                request.Headers.Add( "SOAPAction", $"{parameters.SoapAction}" );

                HttpResponseMessage response = await client.SendAsync( request );

                if ( !response.IsSuccessStatusCode )
                {
                    throw new Exception( response.ReasonPhrase );
                }

                Stream stream = await response.Content.ReadAsStreamAsync();
                var sr = new StreamReader( stream );
                var soapResponse = XDocument.Load( sr );

                return soapResponse;
            }
        }
    }
}
