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
using System.Windows.Media;

namespace RemoteEducationApplication.Client
{
    public class ClientHandler : TcpClient, ITcpConnectionBase
    {
        #region Fields

        private bool _hasPicture;
        private int _port;
        private double _width;
        private double _height;
        private string _name;

        private ImageSource _desktopImage;       
        private TcpClient _tcpClient;
        
        #endregion

        #region Properties

        public TcpClient TcpClient
        {
            get 
            { 
                return _tcpClient; 
            }
            set
            {
                _tcpClient = value;
            }
        }

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
            get { return TcpClient.Client.Connected; }
        }

        /// <summary>
        /// Gets or sets the ControlIdentifier.
        /// </summary>
        public string ControlIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name 
        {
            get 
            { 
                return _name; 
            }
            set 
            {
                _name = value;
                OnPropertyChanged("Name");
            } 
        }

        /// <summary>
        /// Gets or sets the HostName.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the IsClosing.
        /// </summary>
        public bool IsClosing { get; set; }

        /// <summary>
        /// Gets or sets the LocalEndPoint.
        /// </summary>
        public EndPoint LocalEndPoint 
        {
            get { return base.Client.LocalEndPoint; }
        }

        /// <summary>
        /// Gets or sets the RemoteEndPoint.
        /// </summary>
        public EndPoint RemoteEndPoint 
        {
            get { return base.Client.RemoteEndPoint; }
        }

        /// <summary>
        /// Gets or sets the DesktopImage.
        /// </summary>
        public ImageSource DesktopImage 
        {
            get 
            { 
                return _desktopImage; 
            }
            set 
            {
                _desktopImage = value;
                HasPicture = true;
                OnPropertyChanged("DesktopImage");
            } 
        }

        /// <summary>
        /// Gets or sets the LastUpdate.
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Gets or sets the Precedence.
        /// </summary>
        public int Precedence { get; set; }

        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        public int DefaultIndex { get; set; }

        public bool IsExpanded { get; set; }

        public bool HasPicture
        {
            get
            {
                return _hasPicture;
            }
            set
            {
                _hasPicture = value;
                OnPropertyChanged("HasPicture");
            }
        }

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
        public ClientHandler(string name, int precedence = 0)
            : base()
        {
            Name = name;
            Precedence = precedence;
            TcpClient = new TcpClient();
        }

        public ClientHandler()
            : base() 
        {
            TcpClient = new TcpClient();
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
