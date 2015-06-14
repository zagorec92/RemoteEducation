using ExtensionLibrary.Controls.Helpers;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System;
using System.Windows;
using AppResources = Education.Application.Properties.Resources;

namespace RemoteEducationApplication.Views.ExceptionViewer
{
    /// <summary>
    /// Interaction logic for ExceptionWindow.xaml
    /// </summary>
    public partial class ExceptionWindow : WindowBase
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private int? ExceptionId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        private DateTime DateTimeOfException { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public ExceptionWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            Loaded += ExceptionWindow_Loaded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortDescription"></param>
        /// <param name="dateTimeOfException"></param>
        public ExceptionWindow(int? exceptionId, string shortDescription, DateTime dateTimeOfException)
            : this()
        {
            this.Topmost = true;
            ExceptionId = exceptionId;
            ShortDescription = shortDescription;
            DateTimeOfException = dateTimeOfException;

            string messageTemp = String.Format(AppResources.ExceptionWindowMessage, DateTimeOfException.TimeOfDay);
            Message = messageTemp.Substring(0, messageTemp.IndexOf('.') + 1);
        }

        #endregion

        #region +EventHanlding

        /// <summary>
        /// Handles the Loaded event of the ExceptionWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ExceptionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MailHelper.GetExceptionMail(ExceptionId, DateTimeOfException).Display();
            App.Minimize();
        }

        /// <summary>
        /// Handles the AppBarClick event of the ApplicationBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/> instance containing the event data.</param>
        private void ApplicationBar_AppBarClick(object sender, ApplicationEventArgs e)
        {
            if (e.CommandName == ApplicationHelper.CommandTags.Minimize)
                ApplicationHelper.ExecuteBasicCommand(e.CommandName, this);
            else
                ApplicationHelper.ExecuteBasicCommand(e.CommandName);
        }

        #endregion
    }
}
