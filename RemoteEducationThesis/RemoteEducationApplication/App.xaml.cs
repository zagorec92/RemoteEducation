using RemoteEducation.DAL;
using RemoteEducation.DAL.Repositories;
using Education.Model;
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
        /// Handles Application Startup event.
        /// Overrides current culture settings.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
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
