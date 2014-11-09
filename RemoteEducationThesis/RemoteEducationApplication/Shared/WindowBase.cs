using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AppSettings = RemoteEducationApplication.Properties.Settings;

namespace RemoteEducationApplication.Shared
{
    public class WindowBase : Window, INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Gets the application default settings.
        /// </summary>
        internal AppSettings AppSettings
        {
            get
            {
                return AppSettings.Default;
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region NotifyPropertyChanged

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Drag

        /// <summary>
        /// Handles the MouseLeftButtonDown event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance
        /// containing the event data.</param>
        private void WindowBase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #endregion
    }
}
