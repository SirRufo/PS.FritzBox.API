﻿using System;

namespace PS.FritzBox.API
{
    public class SoapFaultException : Exception
    {
        public SoapFaultException( string faultCode, string faultString ) : base( String.Format( "{0}; {1}", faultCode, faultString ) )
        {
            this.FaultCode = faultCode;
            this.FaultString = faultString;
        }

        /// <summary>
        /// Gets or sets the fault code
        /// </summary>
        public string FaultCode { get; internal set; }

        /// <summary>
        /// gets or sets the fault string
        /// </summary>
        public string FaultString { get; internal set; }
    }
}
