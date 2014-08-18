using RemoteEducationApplication.Extensions;
using RemoteEducationApplication.Helpers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace RemoteEducationApplication.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ApplicationBar.xaml
    /// </summary>
    public partial class ApplicationBar : UserControl
    {
        public ApplicationBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Rectangle element.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/>
        /// instance containing the event data.</param>
        private void Rectangle_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                string commandName = rectangle.GetTag();

                if (commandName == ApplicationHelper.Commands.Close)
                    ApplicationHelper.Close();
                else if (commandName == ApplicationHelper.Commands.Minimize)
                    ApplicationHelper.Minimize();
            }
        }
    }
}
