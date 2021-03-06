﻿namespace PS.FritzBox.API
{
    public class NATRSIPStatus
    {
        /// <summary>
        /// Gets or sets if rsip is available
        /// </summary>
        public bool RSIPAvailable { get; internal set; }

        /// <summary>
        /// Gets or sets if nat is enabled
        /// </summary>
        public bool NATEnabled { get; internal set; }
    }
}
