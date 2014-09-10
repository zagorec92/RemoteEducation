using RemoteEducationApplication.Client;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System.Drawing;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RemoteEducationApplication.Views.Client
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : WindowBase
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        protected ClientHandler Client { get; set; }

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
        private async void ClientWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = this;
            ScreenshotHelper.InitializeBitmap();

            //izvuci podatke iz baze o konekciji
            Client = new ClientHandler();
            Client.TcpClient.Connect(ConnectionHelper.GetLocalIPAddress(), 10000);

            //clientHandler.TcpClient.Connect();

            if (Client.TcpClient.Connected)
            {
                //razmjeni informaciju o trajanju čekanja

                await SendImage();
            }
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
            byte[] byteArray = new byte[1];
            NetworkStream ns = Client.TcpClient.GetStream();
            BinaryFormatter bFormatter = new BinaryFormatter();

            while (true)
            {
                try
                {
                    Bitmap bitmap = ScreenshotHelper.TakeScreenshot();
                    bFormatter.Serialize(ns, bitmap);

                    await Task.Delay(10000);
                }
                catch
                {
                    break;
                }
            }
        }

        #endregion
    }
}
