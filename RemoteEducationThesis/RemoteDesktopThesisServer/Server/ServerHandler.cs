﻿using RemoteDesktopThesisServer.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RemoteDesktopThesisServer.Helpers;

namespace RemoteDesktopThesisServer.Server
{
    public class ServerHandler : TcpListener, ITcpConnectionBase
    {
        #region Fields

        private int _port;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port
        {
            get { return _port;  }
            set
            {
                if (value > 0)
                    _port = value;
                else
                    throw new ArgumentException("Port cannot be 0 or less.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AddressFamily AddressFamily 
        {
            get { return base.Server.AddressFamily;}
        }

        /// <summary>
        /// Gets a value that indicates whether a <see cref="System.Net.Sockets.Socket instance"/> is connected
        /// to a remote host as of the last Overload:System.Net.Sockets.Socket.Send or
        /// Overload:System.Net.Sockets.Socket.Receive operation.
        /// </summary>
        public bool Connected 
        {
            get { return base.Server.Connected; }
        }

        /// <summary>
        /// Gets or sets a value that specifies the amount of time after which a synchronous
        /// Overload:System.Net.Sockets.Socket.Receive call will time out.
        /// </summary>
        public int ReceiveTimeout 
        {
            get { return base.Server.ReceiveTimeout; }
            set { base.Server.ReceiveTimeout = value; }
        }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        public IPAddress IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the current number of connections.
        /// </summary>
        public int Connections { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of connections.
        /// </summary>
        public int MaxConnections { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating if server is closing.
        /// <value>Default true</value>
        /// </summary>
        public bool IsClosing { get; set; }

        #endregion

        #region EventHandlers

        /// <summary>
        /// PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes new instance of ServerHandler class with default port (1000).
        /// </summary>
        public ServerHandler()
            : base(1000)
        { }

        /// <summary>
        /// Initializes new instance of ServerHandler class.
        /// </summary>
        /// <param name="port">Port used for listening.</param>
        public ServerHandler(int port)
            : base(port)
        {
            if (port < 0)
                throw new ArgumentException("Port cannot be 0 or less.");

            Port = port;
        }

        /// <summary>
        /// Initializes new instance of ServerHandler class.
        /// </summary>
        /// <param name="endPoint"></param>
        public ServerHandler(IPEndPoint endPoint)
            : base (endPoint)
        { }

        /// <summary>
        /// Initializes new instance of ServerHandler class.
        /// </summary>
        /// <param name="port">Port used for listening.</param>
        /// <param name="address">IP address.</param>
        public ServerHandler(int port, IPAddress address)
            : base(address, port)
        {
            if (port < 0)
                throw new ArgumentException("Port cannot be 0 or less.");

            Port = port;
            IpAddress = address;
        }

        #endregion
    }
}
