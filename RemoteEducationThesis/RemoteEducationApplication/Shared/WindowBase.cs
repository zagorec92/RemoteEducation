using RemoteEducationApplication.Views.ExceptionViewer;
using System.Windows;
using System.Windows.Input;
using WPFFramework.App.Base;
using AppSettings = Education.Application.Properties.Settings;

namespace RemoteEducationApplication.Shared
{
    public class WindowBase : WpfWindowBase
    {
        #region Properties

        /// <summary>
        /// Gets the application default settings.
        /// </summary>
        internal AppSettings AppSettings
        {
            get { return AppSettings.Default; }
        }

        #endregion

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
