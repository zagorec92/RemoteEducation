using Education.Application.Shared;
using ExtensionLibrary.Controls.Extensions;
using WPFFramework.App.Base;

namespace Education.Application.Views.UserControls
{
	/// <summary>
	/// Interaction logic for ClientMiniWindow.xaml
	/// </summary>
	public partial class ClientMiniControl : WpfUserControl
    {
		#region Constructor

		/// <summary>
		/// Initializes new instance of the <see cref="Education.Application.Views.UserControls.ClientMiniWindow"/> class.
		/// </summary>
		public ClientMiniControl()
        {
            InitializeComponent();
        }

		#endregion

		#region Event & Delegates

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">The <see cref="Education.Application.Shared.ApplicationEventArgs"/>
		/// instance containing the event data.</param>
		public delegate void ClientMiniClickEventHandler(object sender, ApplicationEventArgs e);

        /// <summary>
        /// ClientMiniClickEventHandler handler.
        /// </summary>
        public event ClientMiniClickEventHandler ClientMiniClick;

        #endregion

        #region EventHandling

        /// <summary>
        /// Invokes ClientMiniClickEventHandler.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="commandName"></param>
        private void OnClientClick(int clientID, string commandName)
        {
            if (ClientMiniClick != null)
                ClientMiniClick(this, new ApplicationEventArgs(commandName, clientID));
        }

		/// <summary>
		/// Handles the WindowBarClick event of the ApplicationBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="Education.Application.Shared.ApplicationEventArgs"/>
		/// instance containing the event data.</param>
		private void ApplicationBar_WindowBarClick(object sender, ApplicationEventArgs e)
        {
            OnClientClick(this.GetTag<int>(), e.CommandName);
        }
        
        #endregion
    }
}
