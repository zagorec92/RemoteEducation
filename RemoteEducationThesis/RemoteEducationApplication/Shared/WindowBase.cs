using System.ComponentModel;
using System.Windows;
using AppSettings = RemoteEducationApplication.Properties.Settings;

namespace RemoteEducationApplication.Shared
{
    public class WindowBase : Window, INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Gets the application default settings.
        /// </summary>
        public AppSettings AppSettings
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
    }
}
