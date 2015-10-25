using System.Windows.Input;
using WPFFramework.App.Base;

namespace Education.Application.Shared
{
	public class WindowBase : WpfWindow
	{
		#region Drag

		/// <summary>
		/// Handles the MouseLeftButtonDown event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
		private void WindowBase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		#endregion
	}
}
