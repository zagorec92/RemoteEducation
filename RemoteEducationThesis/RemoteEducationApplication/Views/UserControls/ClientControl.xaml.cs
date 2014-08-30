using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System.Windows.Controls;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ClientControl.xaml
    /// </summary>
    public partial class ClientControl : UserControl
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducationApplication.Views.UserControls.ClientControl"/> class.
        /// </summary>
        public ClientControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events & Delegates

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance containing the event data.</param>
        public delegate void ClientCloseEventHandler(object sender, ApplicationBarEventArgs e);

        /// <summary>
        /// ClientCloseEventHandler handler.
        /// </summary>
        public event ClientCloseEventHandler CloseClick;

        #endregion

        #region EventHandling

        /// <summary>
        /// Invokes RectangleClickEventHandler.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        public void OnCloseClick(string clientName)
        {
            if (CloseClick != null)
                CloseClick(this, new ApplicationBarEventArgs(objectName: clientName));
        }

        /// <summary>
        /// Handles the RectangleClick event of the appBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationBarEventArgs"/>
        /// instance containing the event data.</param>
        private void appBar_RectangleClick(object sender, ApplicationBarEventArgs e)
        {
            if (e.CommandName == ApplicationHelper.Commands.Close)
                OnCloseClick(tbkName.Text);
        }   

        #endregion  
    }
}
