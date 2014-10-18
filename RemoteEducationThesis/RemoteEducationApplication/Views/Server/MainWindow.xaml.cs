﻿using RemoteEducationApplication.Client;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Server;
using RemoteEducationApplication.Shared;
using RemoteEducationApplication.Views.Menu;
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
using AppResources = RemoteEducationApplication.Properties.Resources;

namespace RemoteEducationApplication.Views.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        #region Struct

        /// <summary>
        /// 
        /// </summary>
        private struct ClientSizes
        {
            public const double InitialWidth = 240;
            public const double InitialHeight = 220;
            public static double WideWidth = SystemParameters.PrimaryScreenWidth - 300;
            public static double WideHeight = SystemParameters.PrimaryScreenHeight - 185;
            public const double SideGridWidth = 250;
        }

        #endregion

        #region Fields

        private ObservableCollection<ClientHandler> _connectedClients;
        private DateTime _lastImageUpdate;
        private DateTime _lastConnectionUpdate;
        private int _clientNumber;
        private double _sideGridWidth;
        private bool _hasClients;
        private bool _isFullScreen;

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

        public ObservableCollection<ClientHandler> SideClients { get; set; }

        //public List<ClientHandler> ClientServerConnections { get; set; }

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
        /// Gets or sets the date when connection update has occured.
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
        /// Gtes or sets the flag depending whether any client is connected or not.
        /// </summary>
        public bool HasClients
        {
            get
            {
                return _hasClients;
            }
            set
            {
                _hasClients = value;
                NotifyPropertyChanged("HasClients");
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if ClientControl is expanded.
        /// </summary>
        protected bool HasClientExpanded { get; set; }

        /// <summary>
        /// Gets or set the width of the side grid.
        /// </summary>
        public double SideGridWidth
        {
            get
            {
                return _sideGridWidth;
            }
            set
            {
                _sideGridWidth = value;
                NotifyPropertyChanged("SideGridWidth");
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the window if in full screen mode.
        /// </summary>
        public bool IsFullScreen
        {
            get
            {
                return _isFullScreen;
            }
            set
            {
                _isFullScreen = value;
                NotifyPropertyChanged("IsFullScreen");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducationApplication.MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            HandleFullScreenResize(false);
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        #endregion

        #region EventHandling

        #region Window

        /// <summary>
        /// Handles the Loaded event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectedClients = new ObservableCollection<ClientHandler>();
            SideClients = new ObservableCollection<ClientHandler>();
            ClientNumber = GetClientCount();

            for (int i = 0; i < 4; i++)
            {
                ClientHandler client = new ClientHandler()
                {
                    Name = String.Format("Client {0}", i),
                    HasPicture = true,
                    TotalScore = 0,
                    Height = ClientSizes.InitialHeight,
                    Width = ClientSizes.InitialWidth,
                    ID = i
                };

                //SideClients.Add(client);
                ConnectedClients.Add(client);
            }

            DataContext = this;
            IsFullScreen = false;

            //Start();
        }

        /// <summary>
        /// Overrides the OnClosing event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            //ServerImage.Stop();
            //ServerData.Stop();

            base.OnClosing(e);
        }

        #endregion

        #region Menu/Application bar

        /// <summary>
        /// Handles the RectangleClick event of the ApplicationBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/>
        /// instance containing the event data.</param>
        private void ApplicationBar_Click(object sender, ApplicationEventArgs e)
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

            if (menuItem != null)
            {
                string tag = menuItem.GetTag();

                if (tag == ApplicationHelper.CommandTags.Close ||
                    tag == ApplicationHelper.CommandTags.Logoff)
                    ApplicationHelper.ExecuteBasicCommand(menuItem.GetTag());
                else if (tag == ApplicationHelper.CommandTags.FullScreen)
                    HandleFullScreenResize(true);
                else if (tag == ApplicationHelper.CommandTags.Question)
                    QuestionHelper.SendQuestionIDToClient(QuestionHelper.CreateQuestionWithAnswers(), ConnectedClients);
                else if (ApplicationHelper.IsThemeTag(tag))
                    StyleHelper.ChangeTheme(tag);
                else if (tag.Contains(AppSettings.WindowIdentifier))
                    this.NavigateTo(tag.Remove(AppSettings.WindowIdentifier), false);
            }
        }

        /// <summary>
        /// Handles the SubmenuOpened event of the MenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance
        /// containing the event data.</param>
        private void MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            sender.ExecuteIfNotNull<MenuItem>(x => MenuHelper.SetSelectedItemInMenu(x, true));
        }

        #endregion

        #region Client

        /// <summary>
        /// Handles the CloseClick event of the Client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/>
        /// instance containing the event data.</param>
        private void Client_Click(object sender, ApplicationEventArgs e)
        {
            if (e.CommandName == ApplicationHelper.CommandTags.Close)
                CloseClient(e.ObjectID);
            else if (e.CommandName == ApplicationHelper.CommandTags.Expand ||
                e.CommandName == ApplicationHelper.CommandTags.Shrink)
                ChangeClientHeightAndWidth(e.ObjectID, e.CommandName);
            else if (e.CommandName == ApplicationHelper.CommandTags.Connect)
                throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the ClientMiniClick event of the ClientMiniControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/>
        /// instance containing the event data.</param>
        private void ClientMiniControl_ClientMiniClick(object sender, ApplicationEventArgs e)
        {
            if (e.CommandName == ApplicationHelper.CommandTags.Close)
                CloseClient(e.ObjectID);
            else if (e.CommandName == ApplicationHelper.CommandTags.Expand)
            {
                ChangeClientHeightAndWidth(ConnectedClients.Select(x => x.ID).First(), 
                    ApplicationHelper.CommandTags.Shrink);
                ChangeClientHeightAndWidth(e.ObjectID, e.CommandName);
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Window

        /// <summary>
        /// 
        /// </summary>
        private void HandleFullScreenResize(bool fullScreen, MenuItem menuItem = null)
        {
            if (fullScreen && WindowState != WindowState.Maximized)
            {
                Height = SystemParameters.PrimaryScreenHeight;
                WindowState = WindowState.Maximized;

                IsFullScreen = true;
            }
            else
            {
                Width = SystemParameters.WorkArea.Width;
                Height = SystemParameters.WorkArea.Height;
                Left = default(int);
                Top = default(int);
                WindowState = WindowState.Normal;

                IsFullScreen = false;
            }

            if(menuItem != null)
                MenuHelper.SetSelectedItemInMenu(menuItem, false);
        }

        #endregion

        #region Client

        /// <summary>
        /// Gets the count of connected clients.
        /// </summary>
        /// <exception cref="NullReferenceException">If <paramref name="ConnectedClients"/> is <c>null</c>.</exception>
        private int GetClientCount()
        {
            if (ConnectedClients != null)
                return ConnectedClients.Count;
            else
                throw new NullReferenceException();
        }

        /// <summary>
        /// Removes the client from the list.
        /// </summary>
        /// <param name="clientName"></param>
        private void CloseClient(int clientID)
        {
            ClientHandler clientHandler;

            if ((clientHandler = ConnectedClients.SingleOrDefault(x => x.ID == clientID)) != null)
            {
                clientHandler.CloseClient();
                ConnectedClients.Remove(clientHandler);

                if (HasClientExpanded)
                {
                    ConnectedClients.TakeAll(SideClients);
                    ConnectedClients.SortClients();

                    HasClientExpanded = false;
                    SideGridWidth = default(int);
                }
            }

            if((clientHandler = SideClients.SingleOrDefault(x => x.ID == clientID)) != null)
            {
                clientHandler.CloseClient();
                SideClients.Remove(clientHandler);
            }

            ClientNumber = GetClientCount();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientHandler"></param>
        /// <param name="isExpanded"></param>
        private void HandleClientResize(ClientHandler clientHandler, bool isExpanded)
        {
            if (isExpanded)
            {
                clientHandler.Width = ClientSizes.WideWidth;
                clientHandler.Height = ClientSizes.WideHeight;
                SideGridWidth = ClientSizes.SideGridWidth;

                clientHandler.IsExpanded = HasClientExpanded = true;
            }
            else
            {
                clientHandler.Width = ClientSizes.InitialWidth;
                clientHandler.Height = ClientSizes.InitialHeight;
                SideGridWidth = default(int);

                clientHandler.IsExpanded = HasClientExpanded = false;
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="clientID">Identification number of client.</param>
        /// <param name="commandName">Name of the command.</param>
        private void ChangeClientHeightAndWidth(int clientID, string commandName)
        {
            if (ConnectedClients.Any(x => x.ID == clientID))
            {
                if (commandName == ApplicationHelper.CommandTags.Expand && !HasClientExpanded)
                {
                    ClientHandler clientHandler = ConnectedClients.Single(x => x.ID == clientID);

                    HandleConnectedClientsExpand(clientHandler);
                    HandleClientResize(clientHandler, true);
                }
                else if (commandName == ApplicationHelper.CommandTags.Shrink)
                {
                    ClientHandler clientHandler = ConnectedClients.First();

                    if (SideClients.Any())
                        HandleConnectedClientsShrink();

                    HandleClientResize(clientHandler, false);
                }
            }
            else
            {
                if(commandName == ApplicationHelper.CommandTags.Expand)
                {
                    HandleConnectedClientsShrink();

                    ClientHandler clientHandler = ConnectedClients.Single(x => x.ID == clientID);

                    HandleConnectedClientsExpand(clientHandler);
                    HandleClientResize(clientHandler, true);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientHandler"></param>
        private void HandleConnectedClientsExpand(ClientHandler clientHandler)
        {
            ConnectedClients.MoveExtended(ConnectedClients.IndexOf(clientHandler), 0);
            SideClients.TakeExceptFirst(ConnectedClients);
            SideClients.SortClients();
        }

        /// <summary>
        /// 
        /// </summary>
        private void HandleConnectedClientsShrink()
        {
            ConnectedClients.TakeAll(SideClients);
            ConnectedClients.SortClients();
        }

        #endregion

        #region Start

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

            DatabaseHelper.SaveServerInfo(address);

            LastImageUpdate = LastConnectionUpdate = DateTime.Now;

            Task[] tasks = new Task[]
            {
                ListeningForConnections(),
                GetDesktopImage(),
                ExchangeDataWithClient()
            };
        }

        #endregion

        #region Async

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
                        client.Height = ClientSizes.InitialHeight;
                        client.Width = ClientSizes.InitialWidth;
                        client.TcpClient = ServerImage.AcceptTcpClient();
                        client.TcpClientDataExchange = ServerData.AcceptTcpClient();
                        client.StatusMessage = AppResources.ClientStatusImageWait;

                        if (client.TcpClient != null)
                            ConnectedClients.Add(client);

                        ClientNumber = GetClientCount();

                        if (ClientNumber > 0 && !HasClients)
                            HasClients = true;

                        ConnectionHelper.SendSleepTimeValue(client.GetClientStream());
                        client.Name = GetUserIdentification(client.GetDataExchangeStream());                    
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
                    try
                    {
                        if (client.IsClientConnected())
                        {
                            BinaryFormatter bFormatter = new BinaryFormatter();
                            Bitmap bitmap = bFormatter.Deserialize(client.GetClientStream()) as Bitmap;
                            client.DesktopImage = bitmap.GetImageSource();
                        }
                    }
                    catch
                    {
                        clientsToRemove.Add(ClientDisconnecting(client));
                    }
                }       

                LastImageUpdate = DateTime.Now;

                await Task.Delay(ConnectionHelper.SleepTime.Moderate.GetValue());

                HandleDisconnectedClients(clientsToRemove);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ExchangeDataWithClient()
        {
            List<ClientHandler> clientsToRemove = new List<ClientHandler>();

            while (ServerData.IsListening)
            {
                foreach (ClientHandler client in ConnectedClients)
                {
                    try
                    {
                        NetworkStream stream = client.GetDataExchangeStream();

                        if (stream.DataAvailable)
                            client.TotalScore += stream.ReadByte();
                    }
                    catch
                    {
                        clientsToRemove.Add(ClientDisconnecting(client));
                    }
                }

                await Task.Delay(ConnectionHelper.SleepTime.Moderate.GetValue());

                HandleDisconnectedClients(clientsToRemove);
            }
        }

        #region DataExchange

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private string GetUserIdentification(NetworkStream stream)
        {
            int length = stream.ReadByte();
            byte[] buffer = new byte[length];

            stream.Read(buffer, 0, length);
            return buffer.GetString();
        }

        #endregion

        /// <summary>
        /// Updates the client state and closes the client connection.
        /// </summary>
        /// <param name="client"></param>
        private ClientHandler ClientDisconnecting(ClientHandler client)
        {
            client.DesktopImage = null;
            client.CloseClient();
            client.StatusMessage = AppResources.ClientStatusDisconnected;

            return client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientsToRemove"></param>
        private void HandleDisconnectedClients(List<ClientHandler> clientsToRemove)
        {
            clientsToRemove.ForEach(x => ConnectedClients.Remove(x));
            ClientNumber = GetClientCount();

            if (ClientNumber < 1 && HasClients)
                HasClients = false;
        }

        #endregion

        #endregion
    }
}
