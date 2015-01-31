using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System;
using System.Windows;
using Outlook = Microsoft.Office.Interop.Outlook;
using AppResources = RemoteEducationApplication.Properties.Resources;

namespace RemoteEducationApplication.Views.ExceptionViewer
{
    /// <summary>
    /// Interaction logic for ExceptionWindow.xaml
    /// </summary>
    public partial class ExceptionWindow : WindowBase
    {
        private int? ExceptionId { get; set; }
        
        private DateTime DateTimeOfException { get; set; }

        public string ShortDescription { get; set; }

        public string Message { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExceptionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MailHelper.GetExceptionMail(ExceptionId, DateTimeOfException).Display();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationBar_AppBarClick(object sender, ApplicationEventArgs e)
        {
            ApplicationHelper.ExecuteBasicCommand(e.CommandName);
        }
    }
}
