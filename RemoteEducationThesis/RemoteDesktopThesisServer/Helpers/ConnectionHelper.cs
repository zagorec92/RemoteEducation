using System.Net;
using System.Net.Sockets;

namespace RemoteDesktopThesisServer.Helpers
{
    public abstract class ConnectionHelper : ApplicationHelper
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
