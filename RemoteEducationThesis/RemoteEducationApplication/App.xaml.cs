using Education.Model;
using Education.DAL;
using Education.DAL.Repositories;
using RemoteEducationApplication.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace RemoteEducationApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 
        /// </summary>
        public static string CurrentThemeName { get; set; }

        /// <summary>
        /// Handles Application Startup event.
        /// Overrides current culture settings.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CurrentThemeName = "Dark";
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
            FrameworkElement.LanguageProperty.
                OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                    CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        /// <summary>
        /// Handles the DispatcherUnhandledException of the current Application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/>
        /// instance conatining the event data.</param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            using(EEducationDbContext context = new EEducationDbContext())
            {
                ApplicationLogRepository appLogRepository = new ApplicationLogRepository(context); ;

                ApplicationLog applicationLog = new ApplicationLog()
                {
                    Name = e.Exception.Message,
                    Description = e.Exception.InnerException == null ? String.Empty : e.Exception.InnerException.Message,
                    StackTrace = e.Exception.StackTrace
                };

                if (appLogRepository.InsertOrUpdate(applicationLog))
                    appLogRepository.Save();
            }
        }
    }
}
