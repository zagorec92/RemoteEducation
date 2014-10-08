using Education.Model;
using RemoteEducationApplication.Client;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
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
        /// 
        /// </summary>
        protected ClientHandler Client { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IPAddress IpAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public WaitingTime SleepTime { get; set; }

        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Initialize();
            DataContext = this;

            Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #endregion

        #region WebBrowser

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wb_Navigated(object sender, NavigationEventArgs e)
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
                        WebBrowserHelper.GetParsedUrlParameters(urlParameters);

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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBar_AppBarClick(object sender, ApplicationEventArgs e)
        {
            ApplicationHelper.ExecuteBasicCommand(e.CommandName);
        }

        #endregion

        #endregion

        #region Methods

        #region Initialize

        /// <summary>
        /// 
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

                SleepTime = ExtensionMethods.GetValueByIndex<WaitingTime>(waitingLengthIndex);
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
