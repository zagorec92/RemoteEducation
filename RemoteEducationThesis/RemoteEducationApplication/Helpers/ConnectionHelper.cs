using System.Net.Sockets;
using WpfDesktopFramework.Enums.Helpers;

namespace RemoteEducationApplication.Helpers
{
    public abstract class ConnectionHelper : BaseHelper
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

        #region Methods

        #region SendSleepTime

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public static void SendSleepTimeValue(NetworkStream stream)
        {
            int sleepTimeIndex = EnumHelper.GetIndexOfValue<ConnectionHelper.SleepTime>
                (ConnectionHelper.SleepTime.Moderate);
            stream.WriteByte((byte)sleepTimeIndex);
            stream.Flush();
        }

        #endregion

        #endregion
    }
}
