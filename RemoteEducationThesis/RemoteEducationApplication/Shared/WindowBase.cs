using System.ComponentModel;
using System.Windows;

namespace RemoteEducationApplication.Shared
{
    public class WindowBase : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
