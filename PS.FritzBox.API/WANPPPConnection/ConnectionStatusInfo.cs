using System;

namespace PS.FritzBox.API
{

    public enum ConnectionStatus
    {
        Unconfigured,
        Connecting,
        Connected,
        PendingDisconnect,
        Disconnecting,
        Disconnected,
    }

    public enum ConnectionError
    {
        ERROR_NONE,
        ERROR_COMMAND_ABORTED,
        ERROR_NOT_ENABLED_FOR_INTERNET,
        ERROR_USER_DISCONNECT,
        ERROR_ISP_DISCONNECT,
        ERROR_IDLE_DISCONNECT,
        ERROR_FORCED_DISCONNECT,
        ERROR_NO_CARRIER,
        ERROR_IP_CONFIGURATION,
        ERROR_UNKNOWN,
    }

    public class ConnectionStatusInfo
    {
        /// <summary>
        /// Gets the connection status
        /// </summary>
        public ConnectionStatus ConnectionStatus { get; internal set; }

        /// <summary>
        /// Gets the last connection error
        /// </summary>
        public ConnectionError LastConnectionError { get; internal set; }

        /// <summary>
        /// Gets the uptime
        /// </summary>
        public UInt32 Uptime { get; internal set; }
    }
}
