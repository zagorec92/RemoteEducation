using Education.Application.Shared;
using ExtensionLibrary.Controls.Extensions;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using System.Windows.Input;
using System.Windows.Shapes;
using WPFFramework.App.Base;

namespace Education.Application.Views.UserControls
{
	/// <summary>
	/// Interaction logic for MiniWindowBar.xaml
	/// </summary>
	public partial class MiniWindowBar : WpfUserControl
    {
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Education.Application.Views.UserControls.MiniWindowBar"/>
		/// class.
		/// </summary>
		public MiniWindowBar()
        {
            InitializeComponent();
        }

		#endregion

		#region Events & Delegates

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">The <see cref="Education.Application.Shared.ApplicationEventArgs"/>
		/// instance containing the event data.</param>
		public delegate void RectangleClickEventHandler(object sender, ApplicationEventArgs e);

        /// <summary>
        /// RectangleClickEvent handler.
        /// </summary>
        public event RectangleClickEventHandler WindowBarClick;

        #endregion

        #region EventHandling

        /// <summary>
        /// Invokes RectangleClickEventHandler.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        protected void OnAppBarClick(string commandName)
        {
            if (WindowBarClick != null)
                WindowBarClick(this, new ApplicationEventArgs(commandName: commandName));
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Rectangle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance
        /// containing the event data.</param>
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sender.ExecuteIfNotNull<Rectangle>(x => OnAppBarClick(x.GetTag()));
        }

        #endregion
    }
}
