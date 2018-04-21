using System;

namespace PS.FritzBox.API
{
    public enum ConnectionType
    {
        Unconfigured,
        IP_Routed,
        IP_Bridged,
    }

    [Flags]
    public enum PossibleConnectionTypes
    {
        Unconfigured,
        IP_Routed,
        IP_Bridged,
    }

    public class ConnectionTypeInfo
    {
        /// <summary>
        /// Gets the connection type
        /// </summary>
        public ConnectionType ConnectionType { get; internal set; }

        /// <summary>
        /// Gets the possible connection types
        /// </summary>
        public PossibleConnectionTypes PossibleConnectionTypes { get; internal set; }
    }
}
