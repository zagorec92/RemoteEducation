using RemoteEducationApplication.Client;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Server;
using RemoteEducationApplication.Shared;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteEducationApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        #region Fields

        private ObservableCollection<ClientHandler> _connectedClients;
        private DateTime _lastRefresh;
        private string _statusMessage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the connected clients.
        /// </summary>
        public ObservableCollection<ClientHandler> ConnectedClients
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
        /// Gets or sets the server handler.
        /// </summary>
        public ServerHandler Server { get; set; }

        /// <summary>
        /// Gets or sets the warning message.
        /// </summary>
        public string WarningMessage { get; set; }

        /// <summary>
        /// Gets or sets the message to be displayed in status bar.
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
                NotifyPropertyChanged("StatusMessage");
            }
        }

        /// <summary>
        /// Gets or sets the last refresh date.
        /// </summary>
        public DateTime LastRefresh 
        { 
            get
            {
                return _lastRefresh;
            }
            set
            {
                _lastRefresh = value;
                NotifyPropertyChanged("LastRefresh");
            }
        }

        /// <summary>
        /// Gets the count of connected clients.
        /// </summary>
        /// <exception cref="NullReferenceException">If <paramref name="ConnectedClients"/> is <c>null</c>.</exception>
        public string ClientCount 
        {
            get
            {
                if (ConnectedClients != null)
                    return ConnectedClients.Count.ToString();
                else
                    throw new NullReferenceException();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducationApplication.MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ConnectedClients = new ObservableCollection<ClientHandler>();
            Random random = new Random();

            //test
            for (int i = 0; i < 20; i++)
                ConnectedClients.Add(new ClientHandler("test" + i + " ") { Precedence = random.Next(100) });

            ConnectedClients = new ObservableCollection<ClientHandler>(ConnectedClients.OrderByDescending(x => x.Precedence));
            StatusMessage = "Connections: " + ClientCount;

            DataContext = this;
            Start();
        }

        #endregion

        #region EventHandlers

        #region Menu/Application bar

        /// <summary>
        /// Handles the RectangleClick event of the ApplicationBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance containing the event data.</param>
        private void ApplicationBar_Click(object sender, ApplicationBarEventArgs e)
        {
            ApplicationHelper.ExecuteCommand(e.CommandName);
        }

        #endregion

        #region Client

        /// <summary>
        /// Handles the CloseClick event of the Client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance containing the event data.</param>
        private void Client_CloseClick(object sender, ApplicationBarEventArgs e)
        {
            ConnectedClients.Remove
                (ConnectedClients.Single(x => x.Name == e.ObjectName));

            StatusMessage = "Connections: " + ClientCount;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public async void Start()
        {
            //IPAddress address = ConnectionHelper.GetLocalIPAddress();

            //Server = new ServerHandler(2000, address);
            //Server.MaxConnections = (int)ConnectionHelper.MaxConnections.Twenty;
            //Server.Start();

            Task[] tasks = new Task[]
            {
                //ListeningForConnections(),
                GetDesktopImage()
            };

            await Task.WhenAll(tasks);

            //Server.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ListeningForConnections()
        {
            ConnectedClients = new ObservableCollection<ClientHandler>();

            while (!Server.IsClosing)
            {
                //Console.WriteLine("Checking if there is a pending connection.");
                if (Server.Pending())
                {
                    if (ConnectedClients.Count < (int)Server.MaxConnections)
                    {
                        ClientHandler client = Server.AcceptTcpClient() as ClientHandler;

                        if (client != null)
                            ConnectedClients.Add(client);
                    }
                }
                else
                {
                    //Console.WriteLine("Waiting {0} seconds.", ConnectionHelper.SleepTime.Moderate.GetValue() / 1000);
                    await Task.Delay(ConnectionHelper.SleepTime.Moderate.GetValue());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task GetDesktopImage()
        {
            int count = 0;

            while (ConnectedClients != null && ConnectedClients.Any())
            {
                foreach (ClientHandler client in ConnectedClients)
                    if(client != null && !client.IsClosing)
                        client.Name = client.Name.Substring(0, client.Name.IndexOf(' ') + 1) + count.ToString();

                count++;
                LastRefresh = DateTime.Now;

                await Task.Delay(ConnectionHelper.SleepTime.Short.GetValue());
            }
        }

        #endregion
    }
}
