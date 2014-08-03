using System.Net;
using System.Net.Sockets;

namespace RemoteDesktopThesisServer.Helpers
{
    public static class ConnectionHelper
    {
        #region Enum

        /// <summary>
        /// Maximum number of connections.
        /// </summary>
        public enum MaxConnection
        {
            Ten = 10, 
            Twenty = 20,
            Thirty = 30,
            Forty = 40,
            Fifty = 50,
        }

        /// <summary>
        /// Sleep time during connection listening in milliseconds.
        /// </summary>
        public enum ServerSleepTime
        {
            Shortest = 2500,
            Short = 5000,
            Moderate = 10000,
            Long = 20000,
            Longest = 40000
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
