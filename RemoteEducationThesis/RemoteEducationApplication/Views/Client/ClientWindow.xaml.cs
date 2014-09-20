using RemoteEducationApplication.Authentication;
using RemoteEducationApplication.Client;
using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Input;
using AppResources = RemoteEducationApplication.Properties.Resources;
using WaitingTime = RemoteEducationApplication.Helpers.ConnectionHelper.SleepTime;
using AuthManager = RemoteEducationApplication.Authentication.AuthenticationManager;

namespace RemoteEducationApplication.Views.Client
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : WindowBase
    {
        #region Fields

        private string _connectionStatus;
        private string _processStatus;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ConnectionStatus = AppResources.ClientWindowsDisconnected;
            ProcessStatus = AppResources.ClientWindowProcessWaiting;
            DataContext = this;
            ScreenshotHelper.InitializeBitmap();

            Start();
        }

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
                    GetDataFromServer()
                };
            }

            return isConnected;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBar_AppBarClick(object sender, ApplicationBarEventArgs e)
        {
            ApplicationHelper.ExecuteBasicCommand(e.CommandName);
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

        #region Methods

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
                    break;
                }

                await Task.Delay(SleepTime.GetValue());
            }

            ConnectionStatus = AppResources.ClientWindowsDisconnected;
            ProcessStatus = AppResources.ClientWindowProcessWaiting;

            bool isConnected = false;

            while (!isConnected)
                isConnected = await Connect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task GetDataFromServer()
        {
            NetworkStream stream = Client.GetDataExchangeStream();

            while(true)
            {
                try
                {
                    if (stream.DataAvailable)
                    {
                        int id = stream.ReadByte();
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
    }
}
