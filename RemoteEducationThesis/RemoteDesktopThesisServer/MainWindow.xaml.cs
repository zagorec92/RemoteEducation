using RemoteDesktopThesisServer.Client;
using RemoteDesktopThesisServer.Helpers;
using RemoteDesktopThesisServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace RemoteDesktopThesisServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Const

        private const string MaxNumberOfConnection = "Maximum number of connections exceeded.";

        #endregion

        #region Fields

        private List<ClientHandler> _connectedClients;

        #endregion

        #region Properties

        /// <summary>
        /// ConnectedClients
        /// </summary>
        public List<ClientHandler> ConnectedClients
        {
            get
            {
                if (_connectedClients == null)
                    return new List<ClientHandler>();

                return _connectedClients;
            }
            set { _connectedClients = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ServerHandler Server { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WarningMessage { get; set; }

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public async void Start()
        {
            IPAddress address = ConnectionHelper.GetLocalIPAddress();

            Server = new ServerHandler(2000, address);
            Server.MaxConnections = (int)ConnectionHelper.MaxConnection.Twenty;
            Server.Start();

            Task[] tasks = new Task[]
            {
                ListeningForConnections(),
                GetDesktopImage()
            };

            await Task.WhenAll(tasks);

            Server.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task ListeningForConnections()
        {
            ConnectedClients = new List<ClientHandler>();

            while (!Server.IsClosing)
            {
                Console.WriteLine("Checking if there is a pending connection.");
                if (Server.Pending())
                {
                    if (ConnectedClients.Count < (int)Server.MaxConnections)
                    {
                        ClientHandler client = Server.AcceptTcpClient() as ClientHandler;

                        if (client != null)
                            ConnectedClients.Add(client);
                    }
                    else
                    {
                        WarningMessage = MaxNumberOfConnection;
                    }
                }
                else
                {
                    Console.WriteLine("Waiting {0} seconds.", (int)ConnectionHelper.SleepTime.Moderate / 1000);
                    await Task.Delay((int)ConnectionHelper.SleepTime.Moderate);
                }
            }
        }

        private async Task GetDesktopImage()
        {
            if (ConnectedClients != null || ConnectedClients.Any())
            {
                foreach (ClientHandler client in ConnectedClients)
                {
                    Console.WriteLine("Getting desktop image from {0}.", client.LocalEndPoint);
                }
            }
        }
   

        #endregion
    }
}
