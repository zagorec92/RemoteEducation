using System.ComponentModel;
using System.Windows.Controls;

namespace RemoteEducationApplication.Shared
{
    public class UserControlBase : UserControl, INotifyPropertyChanged
    {
        #region Event

        /// <summary>
        /// PropertyChanged
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
