using System.ComponentModel;
using System.Net.Sockets;

namespace RemoteEducationApplication.Shared
{
    /// <summary>
    /// Shared properties of <see cref="System.Net.Sockets.Socket"/> instances.
    /// </summary>
    public interface ITcpConnectionBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Gets or sets the Address family.
        /// </summary>
        AddressFamily AddressFamily { get; }

        /// <summary>
        /// Gets or sets the closing flag.
        /// </summary>
        bool IsClosing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool Connected { get; }
    }
}
