using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Windows.Media;
using AuthManager = RemoteEducationApplication.Authentication.AuthenticationManager;
using RemoteEducationApplication.Extensions;

namespace RemoteEducationApplication.Client
{
    public class ClientHandler : INotifyPropertyChanged
    {
        #region Fields

        private bool _hasPicture;
        private bool _isExpanded;
        private int _port;
        private int _totalScore;
        private double _width;
        private double _height;
        private string _name;
        private string _statusMessage;

        private ImageSource _desktopImage;       
        private TcpClient _tcpClient;
        private TcpClient _tcpClientDataExchange;
        
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public TcpClient TcpClientDataExchange
        {
            get 
            {
                return _tcpClientDataExchange;
            }
            set
            {
                _tcpClientDataExchange = value;
            }
        }

        /// <summary>
        /// Port
        /// </summary>
        public int ClientPort
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
        public AddressFamily ClientAddressFamily
        {
            get { return TcpClient.Client.AddressFamily; }
        }

        /// <summary>
        /// Gets a value that indicates whether a <see cref="System.Net.Sockets.Socket instance"/> is connected
        /// to a remote host as of the last Overload:System.Net.Sockets.Socket.Send or
        /// Overload:System.Net.Sockets.Socket.Receive operation.
        /// </summary>
        public bool IsClientConnected
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
        public EndPoint ClientLocalEndPoint 
        {
            get { return TcpClient.Client.LocalEndPoint; }
        }

        /// <summary>
        /// Gets or sets the RemoteEndPoint.
        /// </summary>
        public EndPoint ClientRemoteEndPoint 
        {
            get { return TcpClient.Client.RemoteEndPoint; }
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

                if (_desktopImage != null)
                    HasPicture = true;
                else
                    HasPicture = false;

                OnPropertyChanged("DesktopImage");
            } 
        }

        /// <summary>
        /// Gets or sets the LastUpdate.
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public int DefaultIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsExpanded 
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
                OnPropertyChanged("StatusMessage");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ListIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalScore
        {
            get
            {
                return _totalScore;
            }
            set
            {
                _totalScore = value;
                OnPropertyChanged("TotalScore");
            }
        }

        /// <summary>
        /// Gets or sets the ID of the client.
        /// </summary>
        public int ID { get; set; }

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
        public ClientHandler(string name)
        {
            Name = name;
            TcpClient = new TcpClient();
        }

        public ClientHandler()
        {
            TcpClient = new TcpClient();
            TcpClientDataExchange = new TcpClient();    
        }

        #endregion

        #region Methods

        #region Client

        /// <summary>
        /// Gets the TcpClient stream.
        /// </summary>
        /// <returns></returns>
        public NetworkStream GetClientStream()
        {
            return TcpClient.GetStream();
        }

        /// <summary>
        /// Closes the TcpClient.
        /// </summary>
        public void CloseClient()
        {
            TcpClient.Close();
        }

        #endregion

        #region ClientDataExchange

        /// <summary>
        /// Gets the TcpClientDataExchange stream.
        /// </summary>
        /// <returns></returns>
        public NetworkStream GetDataExchangeStream()
        {
            return TcpClientDataExchange.GetStream();
        }

        /// <summary>
        /// Closes the TcpClientDataExchange.
        /// </summary>
        public void CloseDataExchange()
        {
            TcpClientDataExchange.Close();
        }

        /// <summary>
        /// Sends user name through the DataExchange socket.
        /// </summary>
        public void SendName()
        {
            NetworkStream dataStream = TcpClientDataExchange.GetStream();
            string userFullName = AuthManager.LoggedInUser.FullName;
            int lenght = userFullName.Length * 2;

            dataStream.WriteByte((byte)lenght);
            dataStream.Flush();

            dataStream.Write(userFullName.GetBytes(), 0, lenght);
        }

        #endregion

        #endregion
    }
}
