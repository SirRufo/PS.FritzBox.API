﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    /// <summary>
    /// class for getting informations about the wan ppp connection
    /// </summary>
    public class WANPPPConnectionClient : FritzTR64Client
    {
        public WANPPPConnectionClient( string url, int timeout ) : base( url, timeout )
        {
        }

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/tr064/upnp/control/wanpppconn1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:WANPPPConnection:1";

        /// <summary>
        /// Method to get the wan ppp connection info
        /// </summary>
        /// <returns></returns>
        public async Task<WANPPPConnectionInfo> GetInfoAsync()
        {
            WANPPPConnectionInfo info = new WANPPPConnectionInfo();
            XDocument document = await this.InvokeAsync( "GetInfo", null );

            // connection status values
            info.ConnectionStatus.ConnectionStatus = document.Descendants( "NewConnectionStatus" ).First().Value;
            info.ConnectionStatus.LastConnectionError = document.Descendants( "NewLastConnectionError" ).First().Value;
            info.ConnectionStatus.Uptime = Convert.ToUInt32( document.Descendants( "NewUptime" ).First().Value );
            // connecton type values
            info.ConnectionType.ConnectionType = document.Descendants( "NewConnectionType" ).First().Value;
            info.ConnectionType.PossibleConnectionTypes = document.Descendants( "NewPossibleConnectionTypes" ).First().Value;

            // link layer max bitrate values
            info.LinkLayerMaxBitRates.DownstreamMaxBitRate = Convert.ToUInt32( document.Descendants( "NewDownstreamMaxBitRate" ).First().Value );
            info.LinkLayerMaxBitRates.UpstreamMaxBitRate = Convert.ToUInt32( document.Descendants( "NewUpstreamMaxBitRate" ).First().Value );

            // nat rsip
            info.NATRSIPStatus.RSIPAvailable = Convert.ToBoolean( Convert.ToInt32( document.Descendants( "NewRSIPAvailable" ).First().Value ) );
            info.NATRSIPStatus.NATEnabled = Convert.ToBoolean( Convert.ToInt32( document.Descendants( "NewNATEnabled" ).First().Value ) );

            info.ExternalIPAddress = document.Descendants( "NewExternalIPAddress" ).First().Value;
            info.IdleDisconnectTime = Convert.ToUInt32( document.Descendants( "NewIdleDisconnectTime" ).First().Value );
            info.Name = document.Descendants( "NewName" ).First().Value;
            info.TransportType = document.Descendants( "NewTransportType" ).First().Value;
            info.UserName = document.Descendants( "NewUserName" ).First().Value;

            info.PPPoEACName = document.Descendants( "NewPPPoEACName" ).First().Value;
            info.PPPoEServiceName = document.Descendants( "NewPPPoEServiceName" ).First().Value;
            info.RemoteIPAddress = document.Descendants( "NewRemoteIPAddress" ).First().Value;
            info.RouteProtocolRx = document.Descendants( "NewRouteProtocolRx" ).First().Value;

            return info;
        }

        /// <summary>
        /// Method to get the connection type info
        /// </summary>
        /// <returns>the connection type info</returns>
        public async Task<ConnectionTypeInfo> GetConnectionTypeInfoAsync()
        {
            ConnectionTypeInfo info = new ConnectionTypeInfo();

            XDocument document = await this.InvokeAsync( "GetConnectionTypeInfo", null );

            info.ConnectionType = document.Descendants( "NewConnectionType" ).First().Value;
            info.PossibleConnectionTypes = document.Descendants( "NewPossibleConnectionTypes" ).First().Value;

            return info;
        }

        /// <summary>
        /// Method to set the connection type
        /// </summary>
        /// <param name="connectionType">the new connection type</param>
        public async Task SetConnectionTypeAsync( string connectionType )
        {
            var parameter = new SoapRequestParameter( "NewConnectionType", connectionType );
            XDocument document = await this.InvokeAsync( "SetConnectionType", parameter );
        }

        public async Task<ConnectionStatusInfo> GetStatusInfoAsync()
        {
            ConnectionStatusInfo info = new ConnectionStatusInfo();

            XDocument document = await this.InvokeAsync( "GetStatusInfo", null );

            info.ConnectionStatus = document.Descendants( "NewConnectionStatus" ).First().Value;
            info.LastConnectionError = document.Descendants( "NewLastConnectionError" ).First().Value;
            info.Uptime = Convert.ToUInt32( document.Descendants( "NewUptime" ).First().Value );
            return info;
        }

        /// <summary>
        /// Method to get the username
        /// </summary>
        /// <returns>the user name</returns>
        public async Task<string> GetUserNameAsync()
        {
            XDocument document = await this.InvokeAsync( "GetUserName", null );
            return document.Descendants( "NewUserName" ).First().Value;
        }

        /// <summary>
        /// Method to set the user name
        /// </summary>
        /// <param name="userName">the new username</param>
        public async Task SetUserNameAsync( string userName )
        {
            var parameter = new SoapRequestParameter( "NewUserName", userName );
            XDocument document = await this.InvokeAsync( "SetUserName", parameter );
        }

        /// <summary>
        /// Method to set the password
        /// </summary>
        /// <param name="password">the new password</param>
        public async Task SetPasswordAsync( string password )
        {
            var parameter = new SoapRequestParameter( "NewPassword", password );
            XDocument document = await this.InvokeAsync( "SetPassword", parameter );
        }

        /// <summary>
        /// Method to get the nat rsip status
        /// </summary>
        /// <returns>the nat rsipstatus</returns>
        public async Task<NATRSIPStatus> GetNATRSIPStatusAsync()
        {
            NATRSIPStatus status = new NATRSIPStatus();
            XDocument document = await this.InvokeAsync( "GetNATRSIPStatus", null );

            status.NATEnabled = document.Descendants( "NewNATEnabled" ).First().Value == "1";
            status.RSIPAvailable = document.Descendants( "NewRSIPAvailable" ).First().Value == "1";

            return status;
        }

        /// <summary>
        /// Method to force the termination of the ppp connection
        /// </summary>
        public async Task ForceTerminationAsync()
        {
            XDocument document = await this.InvokeAsync( "ForceTermination", null );
        }

        /// <summary>
        /// Method to request a ppp connection
        /// </summary>
        public async Task RequestConnectionAsync()
        {
            XDocument document = await this.InvokeAsync( "RequestConnection", null );
        }

        /// <summary>
        /// Method to get the external ip address
        /// </summary>
        /// <returns>the external ip address</returns>
        public async Task<string> GetExternalIPAddressAsync()
        {
            XDocument document = await this.InvokeAsync( "GetExternalIPAddress", null );
            return document.Descendants( "NewExternalIPAddress" ).First().Value;
        }

        /// <summary>
        /// Method to get the dns servers
        /// </summary>
        /// <returns>the dns servers</returns>
        public async Task<string> GetDNSServersAsync()
        {
            XDocument document = await this.InvokeAsync( "X_GetDNSServers", null );
            return document.Descendants( "NewDNSServers" ).First().Value;
        }

        /// <summary>
        /// Method to set the dns servers
        /// </summary>
        /// <param name="dnsServers">the dns servers</param>
        public async Task SetDNSServersAsync( string dnsServers )
        {
            var parameter = new SoapRequestParameter( "NewDNSServers", dnsServers );
            XDocument document = await this.InvokeAsync( "X_SetDNSServers", parameter );
        }

        /// <summary>
        /// Method to get the link layer max bitrates
        /// </summary>
        /// <returns>the link layer max bitrates</returns>
        public async Task<LinkLayerMaxBitRates> GetLinkLayerMaxBitRatesAsync()
        {
            LinkLayerMaxBitRates bitRates = new LinkLayerMaxBitRates();
            XDocument document = await this.InvokeAsync( "GetLinkLayerMaxBitRates", null );

            bitRates.DownstreamMaxBitRate = Convert.ToUInt32( document.Descendants( "NewDownstreamMaxBitRate" ).First().Value );
            bitRates.UpstreamMaxBitRate = Convert.ToUInt32( document.Descendants( "NewUpstreamMaxBitRate" ).First().Value );
            return bitRates;
        }
    }
}
