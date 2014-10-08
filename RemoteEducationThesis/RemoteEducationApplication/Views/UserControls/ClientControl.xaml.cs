using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Shared;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ClientControl.xaml
    /// </summary>
    public partial class ClientControl : UserControlBase
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RemoteEducationApplication.Views.UserControls.ClientControl"/> 
        /// class.
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
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/>
        /// instance containing the event data.</param>
        public delegate void ClientClickEventHandler(object sender, ApplicationEventArgs e);

        /// <summary>
        /// ClientClickEventHandler handler.
        /// </summary>
        public event ClientClickEventHandler ClientClick;

        #endregion

        #region EventHandling

        /// <summary>
        /// Invokes ClientClickEventHandler.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        public void OnCloseClick(int clientID, string commandName)
        {
            if (ClientClick != null)
                ClientClick(this, new ApplicationEventArgs(commandName, clientID));
        }

        /// <summary>
        /// Handles the RectangleClick event of the appBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/>
        /// instance containing the event data.</param>
        private void appBar_RectangleClick(object sender, ApplicationEventArgs e)
        {
            OnCloseClick(this.GetTag<int>(), e.CommandName);
        }   

        #endregion  
    }
}
