﻿using Education.Model;
using RemoteEducationApplication.Client;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfDesktopFramework.Controls.Extensions;
using WpfDesktopFramework.Controls.Helpers;
using WpfDesktopFramework.Enums.Extensions;
using WpfDesktopFramework.Enums.Helpers;
using AppResources = RemoteEducationApplication.Properties.Resources;
using WaitingTime = RemoteEducationApplication.Helpers.ConnectionHelper.SleepTime;

namespace RemoteEducationApplication.Views.Client
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : WindowBase
    {
        #region Struct

        /// <summary>
        /// 
        /// </summary>
        private struct ClientSizes
        {
            public const double InitialHeight = 125;
            public const double InitialWidth = 250;
            public const double QuestionHeight = 425;
            public const double QuestionWidth = 500;
        }

        #endregion

        #region Fields

        private bool _hasAnswered;

        private string _connectionStatus;
        private string _processStatus;

        private double _clientHeight;
        private double _clientWidth;

        private Uri _questionSource;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        protected ClientHandler Client { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        protected IPAddress IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the sleep time.
        /// </summary>
        public WaitingTime SleepTime { get; set; }

        /// <summary>
        /// Gets or sets the connection status.
        /// </summary>
        public string ConnectionStatus
        {
            get
            {
                return _connectionStatus;
            }
            set
            {
                _connectionStatus = value;
                NotifyPropertyChanged("ConnectionStatus");
            }
        }

        /// <summary>
        /// Gets or sets the process status.
        /// </summary>
        public string ProcessStatus
        {
            get
            {
                return _processStatus;
            }
            set
            {
                _processStatus = value;
                NotifyPropertyChanged("ProcessStatus");
            }
        }

        /// <summary>
        /// Gets or sets the client height.
        /// </summary>
        public double ClientHeight
        {
            get
            {
                return _clientHeight;
            }
            set
            {
                _clientHeight = value;
                NotifyPropertyChanged("ClientHeight");
            }
        }

        /// <summary>
        /// Gets or sets the client width.
        /// </summary>
        public double ClientWidth
        {
            get
            {
                return _clientWidth;
            }
            set
            {
                _clientWidth = value;
                NotifyPropertyChanged("ClientWidth");
            }
        }

        /// <summary>
        /// Gets or sets the question source.
        /// </summary>
        public Uri QuestionSource
        {
            get
            {
                return _questionSource;
            }
            set
            {
                _questionSource = value;
                NotifyPropertyChanged("QuestionSource");
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if the question has been answered.
        /// </summary>
        public bool HasAnswered
        {
            get
            {
                return _hasAnswered;
            }
            set
            {
                _hasAnswered = value;
                NotifyPropertyChanged("HasAnswered");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteEducationApplication.View.Client.ClientHandler"/> class.
        /// </summary>
        public ClientWindow()
        {
            InitializeComponent();
            Loaded += ClientWindow_Loaded;
        }

        #endregion

        #region EventHandling

        #region Window

        /// <summary>
        /// Handles the Loaded event of the ClientWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance 
        /// containing the event data.</param>
        private void ClientWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
            DataContext = this;

            Start();
        }

        #endregion

        #region WebBrowser

        /// <summary>
        /// Handles the Navigated event of the WebBrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Navigation.NavigationEventArgs"/> 
        /// instance containing the event data.</param>
        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            WebBrowser webBrowser = sender as WebBrowser;

            if (webBrowser != null)
            {
                string urlParameters = webBrowser.GetQueryParams();

                if (urlParameters.Length > 0)
                {
                    webBrowser.Visibility = System.Windows.Visibility.Collapsed;
                    ClientHeight = ClientSizes.InitialHeight;
                    ClientWidth = ClientSizes.InitialWidth;

                    Dictionary<int, String> urlParams =
                        WebBrowserHelper.GetUrlParameters(urlParameters);

                    Client.TotalScore += QuestionHelper.CheckAnswers(urlParams);
                    ScoreHelper.SaveUserScore(Client.TotalScore);
                    HasAnswered = true;
                }
                else
                    webBrowser.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #endregion

        #region ApplicationBar

        /// <summary>
        /// Handles the AppBarClick event of the ApplicationBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/>
        /// instance containing the event data.</param>
        private void ApplicationBar_AppBarClick(object sender, ApplicationEventArgs e)
        {
            ApplicationHelper.ExecuteBasicCommand(e.CommandName);
        }

        #endregion

        #endregion

        #region Methods

        #region Initialize

        /// <summary>
        /// Initializes default values.
        /// </summary>
        private void Initialize()
        {
            ConnectionStatus = AppResources.ClientWindowsDisconnected;
            ProcessStatus = AppResources.ClientWindowProcessWaiting;
            ClientHeight = ClientSizes.InitialHeight;
            ClientWidth = ClientSizes.InitialWidth;
            ScreenshotHelper.InitializeBitmap();
        }

        #endregion

        #region Start

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            Client = new ClientHandler();
            IpAddress = DatabaseHelper.GetLastIPAddress();

            Task[] tasks = new Task[]
            {
                Connect()
            };
        }

        #endregion

        #region Connect

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> Connect()
        {
            int timeout = 0;
            bool isConnected = false;

            ConnectionStatus = AppResources.ClientWindowConnectTry;

            while (timeout < AppSettings.DefaultTimeout)
            {
                try
                {
                    Client.TcpClient.Connect(IpAddress, AppSettings.DefaultServerImagePort);
                    Client.TcpClientDataExchange.Connect(IpAddress, AppSettings.DefaultServerDataPort);
                    ConnectionStatus = AppResources.ClientWindowConnected;
                    break;
                }
                catch
                {
                    timeout++;
                }

                await Task.Delay(WaitingTime.Short.GetValue());
            }

            if (Client.TcpClient.Connected)
            {
                var stream = Client.GetClientStream();
                int waitingLengthIndex = stream.ReadByte();

                SleepTime = EnumHelper.GetValueByIndex<WaitingTime>(waitingLengthIndex);
                isConnected = true;

                Client.SendName();

                Task[] tasks = new Task[]
                {
                    SendImage(),
                    ExchangeDataWithServer()
                };
            }

            return isConnected;
        }

        #endregion

        #region SendImage

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task SendImage()
        {
            NetworkStream ns = Client.GetClientStream();
            BinaryFormatter bFormatter = new BinaryFormatter();
            ProcessStatus = AppResources.ClientWindowSendingImage;

            while (true)
            {
                try
                {
                    Bitmap bitmap = ScreenshotHelper.TakeScreenshot();
                    bFormatter.Serialize(ns, bitmap);
                }
                catch
                {
                    Client.CloseDataExchange();
                    Client.CloseClient();
                    break;
                }

                await Task.Delay(SleepTime.GetValue());
            }

            ConnectionStatus = AppResources.ClientWindowsDisconnected;
            ProcessStatus = AppResources.ClientWindowProcessWaiting;

            bool isConnected = false;
            Client = new ClientHandler();

            while (!isConnected)
                isConnected = await Connect();
        }

        #endregion

        #region ExchangeDataWithServer

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task ExchangeDataWithServer()
        {
            NetworkStream stream = Client.GetDataExchangeStream();

            while (true)
            {
                try
                {
                    if (stream.DataAvailable)
                    {
                        int id = stream.ReadByte();

                        Question question = QuestionHelper.GetQuestion(id);
                        ClientHeight = ClientSizes.QuestionHeight;
                        ClientWidth = ClientSizes.QuestionWidth;
                        QuestionSource = QuestionHelper.GetQuestionContentUri(question.Content);
                        HasAnswered = false;
                    }
                    else if (HasAnswered)
                    {
                        stream.WriteByte((byte)Client.TotalScore);
                        stream.Flush();
                        HasAnswered = false;
                    }
                    else
                        await Task.Delay(SleepTime.GetValue());
                }
                catch
                {
                    break;
                }
            }

            return;
        }

        #endregion

        #endregion
    }
}
