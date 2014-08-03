using RemoteDesktopThesisServer.ApplicationManagement;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;

namespace RemoteDesktopThesisServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// Handles the MouseLeftButtonDown event of Rectangle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/>
        /// instance containing the event data.</param>
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                string commandName = rectangle.Tag.ToString();

                if (commandName == "Close")
                {
                    Application.Current.Shutdown();
                }
                else if (commandName == "Minimize")
                {
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
                }
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of Rectangle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/>
        /// instance containing the event data.</param>
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
                rectangle.Fill = new SolidColorBrush(Colors.Red);
        }

        /// <summary>
        /// Handles the MouseLeave event of Rectangle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/>
        /// instance containing the event data.</param>
        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
                rectangle.Fill = new SolidColorBrush(Colors.White);
        }

        #endregion
    }
}
