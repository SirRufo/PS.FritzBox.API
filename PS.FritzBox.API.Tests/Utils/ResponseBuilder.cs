using System.Text;

namespace PS.FritzBox.API.Tests.Utils
{
    static class ResponseBuilder
    {
        private static StringBuilder AppendHeader( this StringBuilder sb )
        {
            sb.AppendLine( "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" );
            sb.AppendLine( "<s:Body>" );
            sb.AppendLine( "<u:GetCommonLinkPropertiesResponse xmlns:u=\"urn:dslforum-org:service:WANCommonInterfaceConfig:1\">" );
            return sb;
        }

        private static StringBuilder AppendFooter( this StringBuilder sb )
        {
            sb.AppendLine( "</u:GetCommonLinkPropertiesResponse>" );
            sb.AppendLine( "</s:Body>" );
            sb.AppendLine( "</s:Envelope>" );
            return sb;
        }

        private static StringBuilder AppendValue( this StringBuilder sb, string name, object value )
        {
            sb.AppendLine( string.Format( "<{0}>{1}</{0}>", name, value ) );
            return sb;
        }


        public static string ToXmlContent( this CommonLinkProperties source )
        {
            var sb = new StringBuilder();
            sb.AppendHeader();
            sb.AppendValue( "New" + nameof( source.WANAccessType ), source.WANAccessType );
            sb.AppendValue( "New" + nameof( source.Layer1UpstreamMaxBitRate ), source.Layer1UpstreamMaxBitRate );
            sb.AppendValue( "New" + nameof( source.Layer1DownstreamMaxBitRate ), source.Layer1DownstreamMaxBitRate );
            sb.AppendValue( "New" + nameof( source.PhysicalLinkStatus ), source.PhysicalLinkStatus );
            sb.AppendFooter();
            return sb.ToString();
        }

        public static string ToXmlContent( this ConnectionStatusInfo source )
        {
            var sb = new StringBuilder();
            sb.AppendHeader();
            sb.AppendValue( "New" + nameof( source.ConnectionStatus ), source.ConnectionStatus );
            sb.AppendValue( "New" + nameof( source.LastConnectionError ), source.LastConnectionError );
            sb.AppendValue( "New" + nameof( source.Uptime ), source.Uptime );
            sb.AppendFooter();
            return sb.ToString();
        }
        public static string ToXmlContent( this ConnectionTypeInfo source )
        {
            var sb = new StringBuilder();
            sb.AppendHeader();
            sb.AppendValue( "New" + nameof( source.ConnectionType ), source.ConnectionType );
            sb.AppendValue( "New" + nameof( source.PossibleConnectionTypes ), source.PossibleConnectionTypes );
            sb.AppendFooter();
            return sb.ToString();
        }
    }

}
