using RemoteEducationApplication.Client;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using RemoteEducationApplication.Extensions;
using System.Collections.ObjectModel;
using RemoteEducationApplication.Views.UserControls;

namespace RemoteEducationApplication
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

        private static ObservableCollection<ClientHandler> _connectedClients;

        #endregion

        #region Properties

        /// <summary>
        /// ConnectedClients
        /// </summary>
        public static ObservableCollection<ClientHandler> ConnectedClients
        {
            get
            {
                if (_connectedClients == null)
                    return new ObservableCollection<ClientHandler>();

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

        /// <summary>
        /// Initializes a new <see cref="RemoteEducationApplication.MainWindow"/> instance.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ConnectedClients = new ObservableCollection<ClientHandler>();

            //test
            for (int i = 0; i < 20; i++)
                ConnectedClients.Add(new ClientHandler("test" + i));
            
            DataContext = this;
        }

        #endregion

        #region EventHandlers

        #region Menu/Application bar

        /// <summary>
        /// Handles the RectangleClick event of the ApplicationBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.View.UserControls.RectangleEventArgs"/>
        /// instance conatining the event data.</param>
        private void ApplicationBar_RectangleClick(object sender, RectangleEventArgs e)
        {
            ApplicationHelper.ExecuteCommand(e.CommandName);
        }

        #endregion

        #endregion

        #region Methods

        public async void Start()
        {
            IPAddress address = ConnectionHelper.GetLocalIPAddress();

            Server = new ServerHandler(2000, address);
            Server.MaxConnections = (int)ConnectionHelper.MaxConnections.Twenty;
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
            ConnectedClients = new ObservableCollection<ClientHandler>();

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
