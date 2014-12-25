using Education.DAL;
using Education.DAL.Repositories;
using Education.Model;
using RemoteEducationApplication.Helpers;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using AppSettings = RemoteEducationApplication.Properties.Settings;

namespace RemoteEducationApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Const

        internal const string MenuWindowPath = "RemoteEducationApplication.Views.Menu.";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current theme name.
        /// </summary>
        public static string CurrentThemeName 
        { 
            get
            {
                return AppSettings.Default.CurrentThemeName;
            }
            set
            {
                AppSettings.Default.CurrentThemeName = value;
            }
        }

        #endregion

        /// <summary>
        /// Handles Application Startup event. of the Application window.
        /// Overrides current culture settings.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StyleHelper.ChangeTheme(CurrentThemeName);

            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
            FrameworkElement.LanguageProperty.
                OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                    CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        /// <summary>
        /// Handles the DispatcherUnhandledException event of the Application window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/>
        /// instance containing the event data.</param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                using (EEducationDbContext context = new EEducationDbContext())
                {
                    ApplicationLogRepository appLogRepository = new ApplicationLogRepository(context); ;

                    ApplicationLog applicationLog = new ApplicationLog()
                    {
                        Name = e.Exception.Message,
                        Description = e.Exception.InnerException == null ? 
                            String.Empty : e.Exception.InnerException.Message,
                        StackTrace = e.Exception.StackTrace
                    };

                    if (appLogRepository.InsertOrUpdate(applicationLog))
                        appLogRepository.Save();
                }
            }
            catch
            {
                //display dialog
            }
        }

        /// <summary>
        /// Handles the Exit event of the Application window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.ExitEventArgs"/> instance containing the event data.</param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AppSettings.Default.Save();
        }
    }
}
