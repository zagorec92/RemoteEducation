using RemoteEducationApplication.Client;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Server;
using RemoteEducationApplication.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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
        /// 
        /// </summary>
        public bool HasClients 
        { 
            get
            {
                return ClientCount > 0;
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
            if(Server.IsListening && e.CommandName == ApplicationHelper.Commands.Close)
                Server.Stop();

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
            Server.IsListening = true;
            Server.Start();

            LastImageUpdate = LastConnectionUpdate = DateTime.Now;

            Task[] tasks = new Task[]
            {
                ListeningForConnections(),
                GetDesktopImage()
            };
        }

        #region async

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ListeningForConnections()
        {
            while (Server.IsListening)
            {
                LastConnectionUpdate = DateTime.Now;

                if (Server.Pending())
                {
                    if (ConnectedClients.Count < Server.MaxConnections)
                    {
                        ClientHandler client = new ClientHandler();
                        client.Width = 200;
                        client.Height = 210;
                        client.HasPicture = false;
                        client.TcpClient = Server.AcceptTcpClient();

                        if (client.TcpClient != null)
                            ConnectedClients.Add(client);
                    }
                }
                else
                    await Task.Delay(ConnectionHelper.SleepTime.Short.GetValue());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task GetDesktopImage()
        {
            List<ClientHandler> clientsToRemove = new List<ClientHandler>();

            while (ConnectedClients != null)
            {
                byte[] byteArray = new byte[1];
                foreach (ClientHandler client in ConnectedClients)
                {
                    int read = client.TcpClient.Client.Receive(byteArray, SocketFlags.Peek);
                    if (client.Connected && read > 0)
                    {
                        BinaryFormatter bFormatter = new BinaryFormatter();
                        Bitmap bitmap = bFormatter.Deserialize(client.GetStream()) as Bitmap;
                        client.DesktopImage = bitmap.GetImageSource();
                    }
                    else
                    {
                        client.Close();
                        clientsToRemove.Add(client);
                    }
                }

                clientsToRemove.ForEach(x => ConnectedClients.Remove(x));
                LastImageUpdate = DateTime.Now;

                await Task.Delay(ConnectionHelper.SleepTime.Moderate.GetValue());
            }
        }

        #endregion

        #endregion
    }
}
