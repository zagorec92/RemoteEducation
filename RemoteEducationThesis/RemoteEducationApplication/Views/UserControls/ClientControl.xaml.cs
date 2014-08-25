using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RemoteEducationApplication.Views.UserControls;
using RemoteEducationApplication.Client;
using RemoteEducationApplication.Helpers;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ClientControl.xaml
    /// </summary>
    public partial class ClientControl : UserControl
    {
        public ClientControl()
        {
            InitializeComponent();
            appBar.RectangleClick += appBar_RectangleClick;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void appBar_RectangleClick(object sender, RectangleEventArgs e)
        {
            if(e.CommandName == ApplicationHelper.Commands.Close)
                MainWindow.ConnectedClients.Remove
                    (MainWindow.ConnectedClients.Single(x => x.Name == tbkName.Text));
        }

        
    }
}
