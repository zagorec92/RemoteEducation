using System.ComponentModel;
using System.Windows;

namespace RemoteEducationApplication.Shared
{
    public class WindowBase : Window, INotifyPropertyChanged
    {
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
