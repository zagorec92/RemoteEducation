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
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RemoteEducationApplication.Views.Server
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

        public List<ClientHandler> ClientServerConnections { get; set; }

        /// <summary>
        /// Gets or sets the server handler.
        /// </summary>
        public ServerHandler ServerImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ServerHandler ServerData { get; set; }

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
            if (ServerImage.IsListening && e.CommandName == ApplicationHelper.Commands.Close)
                ServerImage.Stop();

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

            if (menuItem != null)
            {
                string tag = menuItem.GetTag();

                if (tag == ApplicationHelper.Commands.Close)
                    ApplicationHelper.ExecuteBasicCommand(menuItem.GetTag());
                else if (tag == ApplicationHelper.Commands.Question)
                    QuestionHelper.CreateQuestionWithAnswers();
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
            else if (e.CommandName == ApplicationHelper.Commands.Expand ||
                e.CommandName == ApplicationHelper.Commands.Shrink)
                ChangeClientHeightAndWidth(e.ObjectName, e.CommandName);
            else if (e.CommandName == ApplicationHelper.Commands.Connect)
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
                clientHandler.CloseClient();
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
                    clientHandler.Height = 210;
                    clientHandler.IsExpanded = false;

                    //ClientHandler client = ConnectedClients.
                    //    Where(x => x.Precedence > clientHandler.Precedence).LastOrDefault();                    
                    //int indexOfClient = client == null ? -1 : ConnectedClients.IndexOf(client);

                    //ConnectedClients.Insert(indexOfClient + 1, clientHandler);

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

            ServerImage = new ServerHandler(new IPEndPoint(address, AppSettings.DefaultServerImagePort));
            ServerData = new ServerHandler(new IPEndPoint(address, AppSettings.DefaultServerDataPort));
            ServerImage.MaxConnections = ServerData.MaxConnections =
                ConnectionHelper.MaxConnections.Twenty.GetValue();
            ServerImage.IsListening = ServerData.IsListening = true;

            ServerImage.Start();
            ServerData.Start();

            ///DatabaseHelper.SaveServerInfo(address);

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
            while (ServerImage.IsListening)
            {
                LastConnectionUpdate = DateTime.Now;
                if (ServerImage.Pending())
                {
                    if (ConnectedClients.Count < ServerImage.MaxConnections)
                    {
                        ClientHandler client = new ClientHandler();
                        client.Height = 200;
                        client.Width = 220;
                        client.TcpClient = ServerImage.AcceptTcpClient();
                        client.TcpClientDataExchange = ServerData.AcceptTcpClient();

                        if (client.TcpClient != null)
                            ConnectedClients.Add(client);

                        ClientNumber = ClientCount;

                        var stream = client.GetClientStream();
                        stream.WriteByte(2);
                        stream.Flush();

                        stream = client.TcpClientDataExchange.GetStream();
                        int length = stream.ReadByte();
                        byte[] buffer = new byte[length];

                        stream.Read(buffer, 0, length);
                        string value = buffer.GetString();
                        client.Name = value;                      
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
                foreach (ClientHandler client in ConnectedClients)
                {
                    if (client.ClientConnected)
                    {
                        try
                        {
                            BinaryFormatter bFormatter = new BinaryFormatter();
                            Bitmap bitmap = bFormatter.Deserialize(client.GetClientStream()) as Bitmap;
                            client.DesktopImage = bitmap.GetImageSource();
                        }
                        catch { }
                    }
                    else
                    {
                        client.CloseClient();
                        clientsToRemove.Add(client);
                    }
                }

                clientsToRemove.ForEach(x => ConnectedClients.Remove(x));
                ClientNumber = ClientCount;
                LastImageUpdate = DateTime.Now;

                await Task.Delay(ConnectionHelper.SleepTime.Moderate.GetValue());
            }
        }

        #endregion

        #endregion
    }
}
