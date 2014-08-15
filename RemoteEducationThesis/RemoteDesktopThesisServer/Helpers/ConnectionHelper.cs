﻿using System.Net;
using System.Net.Sockets;

namespace RemoteEducationApplication.Helpers
{
    public sealed class ConnectionHelper : ApplicationHelper
    {
        #region Enum

        /// <summary>
        /// Maximum number of connections.
        /// </summary>
        public enum MaxConnections
        {
            /// <summary>
            /// 10
            /// </summary>
            Ten = 10,
 
            /// <summary>
            /// 20
            /// </summary>
            Twenty = 20,

            /// <summary>
            /// 30
            /// </summary>
            Thirty = 30,

            /// <summary>
            /// 40
            /// </summary>
            Forty = 40,

            /// <summary>
            /// 50
            /// </summary>
            Fifty = 50,
        }

        #endregion

        #region GetIPAddress

        /// <summary>
        /// Gets the local IP address.
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIPAddress()
        {
            IPAddress ipAddress = null;
            IPAddress[] ipAddressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            foreach (IPAddress ip in ipAddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    ipAddress = ip;

            return ipAddress;
        }
        
        #endregion
    }
}
