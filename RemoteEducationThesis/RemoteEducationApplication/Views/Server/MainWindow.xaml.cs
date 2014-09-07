using RemoteEducationApplication.Client;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Server;
using RemoteEducationApplication.Shared;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AppSettings = RemoteEducationApplication.Properties.Settings;

namespace RemoteEducationApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        #region Fields

        private ObservableCollection<ClientHandler> _connectedClients;
        private DateTime _lastImageUpdate;
        private DateTime _lastConnectionUpdate;
        private int _clientNumber;

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
            set 
            { 
                _connectedClients = value;          
            }
        }

        /// <summary>
        /// Gets or sets the server handler.
        /// </summary>
        public ServerHandler Server { get; set; }

        /// <summary>
        /// Gets or sets the last refresh date.
        /// </summary>
        public DateTime LastImageUpdate 
        { 
            get
            {
                return _lastImageUpdate;
            }
            set
            {
                _lastImageUpdate = value;
                NotifyPropertyChanged("LastImageUpdate");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastConnectionUpdate
        {
            get
            {
                return _lastConnectionUpdate;
            }
            set
            {
                _lastConnectionUpdate = value;
                NotifyPropertyChanged("LastConnectionUpdate");
            }
        }

        /// <summary>
        /// Gets the count of connected clients.
        /// </summary>
        /// <exception cref="NullReferenceException">If <paramref name="ConnectedClients"/> is <c>null</c>.</exception>
        public int ClientCount 
        {
            get
            {
                if (ConnectedClients != null)
                    return ConnectedClients.Count;
                else
                    throw new NullReferenceException();           
            }
        }

        /// <summary>
        /// Gets or sets the current client count number.
        /// </summary>
        public int ClientNumber 
        { 
            get
            {
                return _clientNumber;
            }
            set
            {
                _clientNumber = value;
                NotifyPropertyChanged("ClientNumber");
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if ClientControl is expanded.
        /// </summary>
        protected bool HasClientExpanded { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducationApplication.MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            HandleFullScreenResize();
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        #endregion

        #region EventHandlers

        #region Window

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {            
            ConnectedClients = new ObservableCollection<ClientHandler>();
            ClientNumber = ClientCount;
            DataContext = this;

            Start();
        }

        /// <summary>
        /// Overrides the OnClosing event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            //close all connections
        }

        #endregion

        #region Menu/Application bar

        /// <summary>
        /// Handles the RectangleClick event of the ApplicationBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance containing the event data.</param>
        private void ApplicationBar_Click(object sender, ApplicationBarEventArgs e)
        {
            ApplicationHelper.ExecuteBasicCommand(e.CommandName);
        }

        /// <summary>
        /// Handles the Click event of the MenuItem element.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SubMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;

            if(menuItem != null)
            {
                if (menuItem.GetTag() == ApplicationHelper.Commands.Close)
                    ApplicationHelper.ExecuteBasicCommand(menuItem.GetTag());
            }
        }

        #endregion

        #region Client

        /// <summary>
        /// Handles the CloseClick event of the Client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance containing the event data.</param>
        private void Client_Click(object sender, ApplicationBarEventArgs e)
        {
            if (e.CommandName == ApplicationHelper.Commands.Close)
                CloseClient(e.ObjectName);
            else if(e.CommandName == ApplicationHelper.Commands.Expand ||
                e.CommandName == ApplicationHelper.Commands.Shrink)
                ChangeClientHeightAndWidth(e.ObjectName, e.CommandName);
            else if(e.CommandName == ApplicationHelper.Commands.Connect)
            {

            }
        }

        #endregion

        #endregion

        #region Methods

        #region Window

        /// <summary>
        /// 
        /// </summary>
        private void HandleFullScreenResize()
        {
            Width = System.Windows.SystemParameters.WorkArea.Width;
            Height = System.Windows.SystemParameters.WorkArea.Height;
            Left = default(int);
            Top = default(int);
            WindowState = WindowState.Normal;
        }

        #endregion

        #region Client

        /// <summary>
        /// Remove the client from the list.
        /// </summary>
        /// <param name="clientName"></param>
        private void CloseClient(string clientName)
        {
            if (ConnectedClients.Count(x => x.Name == clientName) == 1)
            {
                ClientHandler clientHandler = 
                    ConnectedClients.Single(x => x.Name == clientName);
                clientHandler.Close();
                ConnectedClients.Remove(clientHandler);
            }

            ClientNumber = ClientCount;
            HasClientExpanded = false;
        }

        /// <summary>
        /// Changes the client width and height.
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="commandName"></param>
        private void ChangeClientHeightAndWidth(string clientName, string commandName)
        {
            //width and height values are temporarily hardcoded

            if (ConnectedClients.Count(x => x.Name == clientName) == 1)
            {
                if (commandName == ApplicationHelper.Commands.Expand && !HasClientExpanded)
                {
                    ClientHandler clientHandler = ConnectedClients.Single
                        (x => x.Name == clientName);

                    ConnectedClients.Remove(clientHandler);

                    clientHandler.Width = 1024;
                    clientHandler.Height = 680;
                    clientHandler.IsExpanded = true;
                    
                    ConnectedClients.Insert(0, clientHandler);
                    HasClientExpanded = true;
                }
                else if (commandName == ApplicationHelper.Commands.Shrink)
                {
                    ClientHandler clientHandler = ConnectedClients.Single
                        (x => x.Name == clientName);

                    ConnectedClients.Remove(clientHandler);

                    clientHandler.Width = 200;
                    clientHandler.Height = 190;
                    clientHandler.IsExpanded = false;

                    ClientHandler client = ConnectedClients.
                        Where(x => x.Precedence > clientHandler.Precedence).LastOrDefault();                    
                    int indexOfClient = client == null ? -1 : ConnectedClients.IndexOf(client);

                    ConnectedClients.Insert(indexOfClient + 1, clientHandler);

                    HasClientExpanded = false;
                }          
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            IPAddress address = ConnectionHelper.GetLocalIPAddress();

            Server = new ServerHandler(new IPEndPoint(address, AppSettings.Default.DefaultPort));
            Server.MaxConnections = ConnectionHelper.MaxConnections.Twenty.GetValue();
            Server.Start();

            LastImageUpdate = LastConnectionUpdate = DateTime.Now;

            Task[] tasks = new Task[]
            {
                ListeningForConnections(),
                GetDesktopImage()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ListeningForConnections()
        {
            //ConnectedClients = new ObservableCollection<ClientHandler>();

            //while (true)
            //{
            //    await Task.Delay(ConnectionHelper.SleepTime.Short.GetValue());
            //    //ConnectedClients.Add(new ClientHandler("name" + (i++)));

            //    Random random = new Random();

            //    ConnectedClients.Add(new ClientHandler("test" + ClientCount + " ")
            //    {
            //        Precedence = random.Next(100),
            //        Width = 200,
            //        Height = 190
            //    });
            //}
            Random random = new Random();
            while (Server.IsListening)
            {
                LastConnectionUpdate = DateTime.Now;

                if (Server.Pending())
                {
                    if (ConnectedClients.Count < (int)Server.MaxConnections)
                    {
                        ClientHandler client = new ClientHandler("test" + ClientCount + 1 + " ") 
                        {
                            Precedence = random.Next(),
                            Height = 190,
                            Width = 200
                        };
                        client.TcpClient = Server.AcceptTcpClient();

                        int readCount = 2;
                        var data = new byte[5];

                        StringBuilder dataString = new StringBuilder();
                        using (var ns = client.TcpClient.GetStream())
                        {
                            while (ns.DataAvailable)
                            {
                                ns.Read(data, 0, readCount);
                                dataString.Append(Encoding.UTF8.GetString(data, 0, readCount));
                                readCount = Convert.ToInt32(dataString);
                            } 
                        }


                        if (client.TcpClient != null)
                            ConnectedClients.Add(client);
                    }
                }
                else
                {
                    //Console.WriteLine("Waiting {0} seconds.", ConnectionHelper.SleepTime.Moderate.GetValue() / 1000);
                    await Task.Delay(ConnectionHelper.SleepTime.Short.GetValue());
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

            while (ConnectedClients != null)
            {
                if (ConnectedClients.Any())
                {
                    foreach (ClientHandler client in ConnectedClients)
                        if (client != null && !client.IsClosing && client.Connected)
                        {
                            client.Name = client.Name.Substring(0, client.Name.IndexOf(' ') + 1) + count.ToString();
                        }
                        else if(!client.Connected)
                        {
                            ConnectedClients.Remove(client);
                        }

                    count++;
                }

                LastImageUpdate = DateTime.Now;
                await Task.Delay(ConnectionHelper.SleepTime.Long.GetValue());
            }
        }

        #endregion
    }
}
