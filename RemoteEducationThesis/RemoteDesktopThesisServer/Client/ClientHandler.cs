using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RemoteEducationApplication.Shared;

namespace RemoteEducationApplication.Client
{
    public class ClientHandler : TcpClient, ITcpConnectionBase
    {
        #region Fields

        private int _port;
        private Bitmap _desktopImage;

        #endregion

        #region Properties

        /// <summary>
        /// Port
        /// </summary>
        public int Port
        {
            get { return _port; }
            set
            {
                if (value > 0)
                    _port = value;
                else
                    throw new ArgumentException("Port cannot be 0 or less.");
            }
        }

        /// <summary>
        /// AddressFamily
        /// </summary>
        public AddressFamily AddressFamily
        {
            get { return base.Client.AddressFamily; }
        }

        /// <summary>
        /// Gets a value that indicates whether a <see cref="System.Net.Sockets.Socket instance"/> is connected
        /// to a remote host as of the last Overload:System.Net.Sockets.Socket.Send or
        /// Overload:System.Net.Sockets.Socket.Receive operation.
        /// </summary>
        public new bool Connected
        {
            get { return base.Client.Connected; }
        }

        /// <summary>
        /// ControlIdentifier
        /// </summary>
        public string ControlIdentifier { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// HostName
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// IsClosing
        /// </summary>
        public bool IsClosing { get; set; }

        /// <summary>
        /// LocalEndPoint
        /// </summary>
        public EndPoint LocalEndPoint 
        {
            get { return base.Client.LocalEndPoint; }
        }

        /// <summary>
        /// RemoteEndPoint
        /// </summary>
        public EndPoint RemoteEndPoint 
        {
            get { return base.Client.RemoteEndPoint; }
        }

        /// <summary>
        /// DesktopImage
        /// </summary>
        public Bitmap DesktopImage 
        {
            get { return _desktopImage; }
            set 
            {
                _desktopImage = value;
                OnPropertyChanged("DesktopImage");
            } 
        }

        /// <summary>
        /// LastUpdate
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// UpdateInterval
        /// </summary>
        public int UpdateInterval { get; set; }

        /// <summary>
        /// Precedence
        /// </summary>
        public int Precedence { get; set; }

        #endregion

        #region EventHandlers

        /// <summary>
        /// Handles the OnPropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Events

        /// <summary>
        /// Invokes OnPropertyChanged.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Invokes PropertyChanged event handler.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> 
        /// instance conatining the event data.</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of ClientHandler class.
        /// </summary>
        /// <param name="name">Client name.</param>
        /// <param name="updateInterval">Refresh interval in milliseconds. Optional.</param>
        /// <param name="precedence">Client precedence. Optional.</param>
        public ClientHandler(string name, int updateInterval = -1, int precedence = 0)
            : base()
        {
            Name = name;
            UpdateInterval = updateInterval;
            Precedence = precedence;
        }


        #region base

        /// <summary>
        /// Initializes a new instance of ClientHandler class.
        /// </summary>
        /// <param name="endPoint"></param>
        public ClientHandler(IPEndPoint endPoint)
            : base(endPoint)
        { }

        /// <summary>
        /// Initializes a new instance of ClientHandler class.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="name"></param>
        /// <param name="port"></param>
        public ClientHandler(string hostname, string name, int port)
            : base(hostname, port)
        {
            HostName = hostname;
            Port = port;
            Name = name;
        }

        #endregion

        #endregion
    }
}
