using Education.Application.Shared;
using WPFFramework.Controls;
using WPFFramework.App.Base;

namespace Education.Application.Views.UserControls
{
	/// <summary>
	/// Interaction logic for ClientControl.xaml
	/// </summary>
	public partial class ClientControl : WpfUserControl
	{
		#region Constructor

		/// <summary>
		/// Creates a new instance of the <see cref="Education.Application.Views.UserControls.ClientControl"/> 
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
		/// <param name="e">The <see cref="Education.Application.Shared.ApplicationBarEventArgs"/>
		/// instance containing the event data.</param>
		public delegate void ClientClickEventHandler(object sender, ApplicationBarEventArgs e);

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
				ClientClick(this, new ApplicationBarEventArgs(commandName, clientID));
		}

		/// <summary>
		/// Handles the RectangleClick event of the appBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="Education.Application.Shared.ApplicationBarEventArgs"/>
		/// instance containing the event data.</param>
		private void appBar_RectangleClick(object sender, ApplicationBarEventArgs e)
		{
			OnCloseClick(this.GetTag<int>(), e.CommandName);
		}

		#endregion
	}
}
