using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PS.FritzBox.API
{
    public class WANCommonInterfaceConfigClient : FritzTR64Client
    {
        public WANCommonInterfaceConfigClient(string url, int timeout) : base(url, timeout)
        {
        }

        /// <summary>
        /// Gets the control url
        /// </summary>
        protected override string ControlUrl => "/tr064/upnp/control/wancommonifconfig1";

        /// <summary>
        /// Gets the request namespace
        /// </summary>
        protected override string RequestNameSpace => "urn:dslforum-org:service:WANCommonInterfaceConfig:1";

        /// <summary>
        /// Method to get the common link properties
        /// </summary>
        /// <remarks>Internal invokes GetCommonLinkProperties on device</remarks>
        /// <returns>the common link properties</returns>
        public async Task<CommonLinkProperties> GetCommonLinkPropertiesAsync()
        {
            XDocument document = await this.InvokeAsync("GetCommonLinkProperties", null);

            CommonLinkProperties properties = new CommonLinkProperties();
            properties.WANAccessType = document.Descendants("NewWANAccessType").First().Value;
            properties.PhysicalLinkStatus = document.Descendants("NewPhysicalLinkStatus").First().Value;
            properties.Layer1DownstreamMaxBitRate = Convert.ToUInt32(document.Descendants("NewLayer1DownstreamMaxBitRate").First().Value);
            properties.Layer1UpstreamMaxBitRate = Convert.ToUInt32(document.Descendants("NewLayer1UpstreamMaxBitRate").First().Value);

            return properties;
        }

        /// <summary>
        /// Method to get the total bytes sent
        /// </summary>
        /// <remarks>Internal invokes GetTotalBytesSent on device</remarks>
        /// <returns>the total bytes sent</returns>
        public async Task<UInt32> GetTotalBytesSentAsync()
        {
            XDocument document = await this.InvokeAsync("GetTotalBytesSent", null);
            return Convert.ToUInt32(document.Descendants("NewTotalBytesSent").First().Value);
        }

        /// <summary>
        /// Method to get the total bytes received
        /// </summary>
        /// <remarks>Internal invokes GetTotalBytesReceived on device</remarks>
        /// <returns>the total bytes received</returns>
        public async Task<UInt32> GetTotalBytesReceivedAsync()
        {
            XDocument document = await this.InvokeAsync("GetTotalBytesReceived", null);
            return Convert.ToUInt32(document.Descendants("NewTotalBytesReceived").First().Value);
        }

        /// <summary>
        /// Method to get the total packets sent
        /// </summary>
        /// <remarks>Internal invokes GetTotalPacketsSent on device</remarks>
        /// <returns>the total packets sent</returns>
        public async Task<UInt32> GetTotalPacketsSentAsync()
        {
            XDocument document = await this.InvokeAsync("GetTotalPacketsSent", null);
            return Convert.ToUInt32(document.Descendants("NewTotalPacketsSent").First().Value);
        }

        /// <summary>
        /// Method to get the total packets received
        /// </summary>
        /// <remarks>Internal invokes GetTotalPacketsReceived on device</remarks>
        /// <returns>the total packets received</returns>
        public async Task<UInt32> GetTotalPacketsReceivedAsync()
        {
            XDocument document = await this.InvokeAsync("GetTotalPacketsReceived", null);
            return Convert.ToUInt32(document.Descendants("NewTotalPacketsReceived").First().Value);
        }

        /// <summary>
        /// Method to set the wan access type
        /// </summary>
        /// <remarks>Internal invokes X_AVM-DE_SetWANAccessType on device</remarks>
        /// <param name="accessType">the new wan access type</param>
        public async Task SetWANAccessTypeAsync(string accessType)
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_SetWANAccessType", new SoapRequestParameter("NewAccessType", accessType));
        }

        /// <summary>
        /// Method to get the online monitor
        /// </summary>
        /// <remarks>Internal invokes X_AVM-DE_GetOnlineMonitor on device</remarks>
        /// <param name="groupIndex">the group index to request the online monitor for</param>
        /// <returns>the online monitor info</returns>
        public async Task<OnlineMonitorInfo> GetOnlineMonitorAsync(UInt32 groupIndex)
        {
            XDocument document = await this.InvokeAsync("X_AVM-DE_GetOnlineMonitor", new SoapRequestParameter("NewSyncGroupIndex", groupIndex));

            OnlineMonitorInfo info = new OnlineMonitorInfo();
            info.SyncGroupMode = document.Descendants("NewSyncGroupMode").First().Value;
            info.SyncGroupName = document.Descendants("NewSyncGroupName").First().Value;
            info.TotalNumberSyncGroups = Convert.ToUInt32(document.Descendants("NewTotalNumberSyncGroups").First().Value);
            info.DownStream = await this.UpDownValuesToEnumerableAsync(document.Descendants("Newds_current_bps").First().Value);
            info.DownStream_Media = await this.UpDownValuesToEnumerableAsync(document.Descendants("Newmc_current_bps").First().Value);
            info.MaxUpStream = Convert.ToUInt32(document.Descendants("Newmax_us").First().Value);
            info.MaxDownStream = Convert.ToUInt32(document.Descendants("Newmax_ds").First().Value);
            info.UpStream = await this.UpDownValuesToEnumerableAsync(document.Descendants("Newus_current_bps").First().Value);
            info.UpstreamDefaultPrio = await this.UpDownValuesToEnumerableAsync(document.Descendants("Newprio_default_bps").First().Value);
            info.UpstreamHighPrio = await this.UpDownValuesToEnumerableAsync(document.Descendants("Newprio_high_bps").First().Value);
            info.UpstreamLowPrio = await this.UpDownValuesToEnumerableAsync(document.Descendants("Newprio_low_bps").First().Value);
            info.UpstreamRealtimePrio = await this.UpDownValuesToEnumerableAsync(document.Descendants("Newprio_realtime_bps").First().Value);

            return info;
        }

        /// <summary>
        /// Method to get values from string as IEnumerable<UInt32>
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private async Task<IEnumerable<UInt32>> UpDownValuesToEnumerableAsync(string values)
        {
            return values.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select((entry) => UInt32.Parse(entry.Trim())).AsEnumerable();
        }
    }
}
