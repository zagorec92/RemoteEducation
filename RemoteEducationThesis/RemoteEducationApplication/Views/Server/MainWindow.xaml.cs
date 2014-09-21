using RemoteEducationApplication.Client;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Server;
using RemoteEducationApplication.Shared;
using RemoteEducationApplication.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
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
            public static double InitialWidth = 220;
            public static double InitialHeight = 200;
            public static double WideWidth = SystemParameters.PrimaryScreenWidth - 300;
            public static double WideHeight = SystemParameters.PrimaryScreenHeight - 185;
            public static double SideGridWidth = 250;
        }

        #endregion

        #region Fields

        private ObservableCollection<ClientHandler> _connectedClients;
        private DateTime _lastImageUpdate;
        private DateTime _lastConnectionUpdate;
        private int _clientNumber;
        private double _helperGridWidth;
        private bool _hasClients;

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
        /// 
        /// </summary>
        public double HelperGridWidth
        {
            get
            {
                return _helperGridWidth;
            }
            set
            {
                _helperGridWidth = value;
                NotifyPropertyChanged("HelperGridWidth");
            }
        }

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
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            ServerImage.Stop();
            ServerData.Stop();

            base.OnClosing(e);
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

            if (menuItem != null)
            {
                string tag = menuItem.GetTag();

                if (tag == ApplicationHelper.CommandTags.Close ||
                    tag == ApplicationHelper.CommandTags.Logoff)
                    ApplicationHelper.ExecuteBasicCommand(menuItem.GetTag());
                else if (tag == ApplicationHelper.CommandTags.Question)
                    SendQuestionIDToClient(QuestionHelper.CreateQuestionWithAnswers());
                else if (ApplicationHelper.IsThemeTag(tag))
                    ApplicationHelper.ChangeTheme(tag);
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
            MenuItem menuItem = sender as MenuItem;

            if (menuItem != null)
                ApplicationHelper.SetSelectedThemeName(menuItem);
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
            if (e.CommandName == ApplicationHelper.CommandTags.Close)
                CloseClient(e.ObjectName);
            else if (e.CommandName == ApplicationHelper.CommandTags.Expand ||
                e.CommandName == ApplicationHelper.CommandTags.Shrink)
                ChangeClientHeightAndWidth(e.ObjectName, e.CommandName);
            else if (e.CommandName == ApplicationHelper.CommandTags.Connect)
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
            HelperGridWidth = default(int);
        }

        /// <summary>
        /// Changes the client width and height.
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="commandName"></param>
        private void ChangeClientHeightAndWidth(string clientName, string commandName)
        {
            if (ConnectedClients.Count(x => x.Name == clientName) == 1)
            {
                if (commandName == ApplicationHelper.CommandTags.Expand && !HasClientExpanded)
                {
                    ClientHandler clientHandler = ConnectedClients.Single
                        (x => x.Name == clientName);

                    clientHandler.ListIndex = ConnectedClients.IndexOf(clientHandler);

                    ConnectedClients.MoveExtended(clientHandler.ListIndex, 0);

                    clientHandler.Width = ClientSizes.WideWidth;
                    clientHandler.Height = ClientSizes.WideHeight;
                    clientHandler.IsExpanded = true;

                    HasClientExpanded = true;
                    HelperGridWidth = ClientSizes.SideGridWidth;
                }
                else if (commandName == ApplicationHelper.CommandTags.Shrink)
                {
                    ClientHandler clientHandler = ConnectedClients.Single
                        (x => x.Name == clientName);

                    ConnectedClients.MoveExtended(0, clientHandler.ListIndex);

                    clientHandler.Width = ClientSizes.InitialWidth;
                    clientHandler.Height = ClientSizes.InitialHeight;
                    clientHandler.IsExpanded = false;

                    HasClientExpanded = false;
                    HelperGridWidth = default(int);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionId"></param>
        private void SendQuestionIDToClient(int questionId)
        {
            if(questionId != default(int))
            {
                foreach (var client in ConnectedClients)
                {
                    var stream = client.GetDataExchangeStream();
                    stream.Flush();
                    stream.WriteByte((byte)questionId);
                    stream.Flush();
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

            DatabaseHelper.SaveServerInfo(address);

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
                        client.Height = ClientSizes.InitialHeight;
                        client.Width = ClientSizes.InitialWidth;
                        client.TcpClient = ServerImage.AcceptTcpClient();
                        client.TcpClientDataExchange = ServerData.AcceptTcpClient();
                        client.StatusMessage = AppResources.ClientStatusImageWait;

                        if (client.TcpClient != null)
                            ConnectedClients.Add(client);

                        ClientNumber = ClientCount;

                        if (ClientNumber > 0 && !HasClients)
                            HasClients = true;

                        SendSleepTimeValue(client.GetClientStream());
                        client.Name =  GetUserIdentification(client.GetDataExchangeStream());                    
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
                        catch 
                        {
                            client.DesktopImage = null;
                            client.CloseClient();
                            clientsToRemove.Add(client);
                            client.StatusMessage = AppResources.ClientStatusDisconnected;
                        }
                    }
                    else
                    {
                        client.DesktopImage = null;
                        client.CloseClient();
                        clientsToRemove.Add(client);
                        client.StatusMessage = AppResources.ClientStatusDisconnected;
                    }
                }

                LastImageUpdate = DateTime.Now;

                await Task.Delay(ConnectionHelper.SleepTime.Moderate.GetValue());

                clientsToRemove.ForEach(x => ConnectedClients.Remove(x));
                ClientNumber = ClientCount;

                if (ClientNumber < 1 && HasClients)
                    HasClients = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        private void SendSleepTimeValue(NetworkStream stream)
        {
            int sleepTimeIndex = ExtensionMethods.GetIndexOfValue<ConnectionHelper.SleepTime>
                (ConnectionHelper.SleepTime.Moderate);
            stream.WriteByte((byte)sleepTimeIndex);
            stream.Flush();
        }

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

        #endregion
    }
}
