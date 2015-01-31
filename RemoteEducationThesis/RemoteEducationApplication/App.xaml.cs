﻿using Education.DAL;
using Education.DAL.Repositories;
using Education.Model;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Views.ExceptionViewer;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using WpfDesktopFramework.App;
using WpfDesktopFramework.Controls.Helpers;
using WpfDesktopFramework.Exceptions.Helpers;
using AppSettings = RemoteEducationApplication.Properties.Settings;

namespace RemoteEducationApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : WpfApplication
    {
        #region Const

        internal const string MenuWindowPath = "RemoteEducationApplication.Views.Menu.";

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        internal static AppSettings Settings
        {
            get { return AppSettings.Default; }
        }

        /// <summary>
        /// Gets or sets the current theme name.
        /// </summary>
        internal static string CurrentThemeName 
        { 
            get { return Settings.CurrentThemeName; }
            set { Settings.CurrentThemeName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal static CultureInfo CurrentUICulture
        {
            get { return CultureInfo.DefaultThreadCurrentUICulture; }
            set { CultureInfo.DefaultThreadCurrentUICulture = value; }
        }

        #endregion

        #region Constructor

        public App()
            : base()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        #endregion

        /// <summary>
        /// Handles Application Startup event of the windows application.
        /// Overrides current culture settings.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StyleHelper.ChangeTheme(CurrentThemeName);

            CurrentUICulture = new CultureInfo("hr-HR");
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        /// <summary>
        /// Handles the DispatcherUnhandledException event of the windows application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/>
        /// instance containing the event data.</param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            int? exceptionId = null;
            Exception exception = e.Exception;
            DateTime exceptionOccured = DateTime.Now;

            try
            {
                using (EEducationDbContext context = new EEducationDbContext())
                {
                    ApplicationLogRepository appLogRepository = new ApplicationLogRepository(context); ;

                    ApplicationLog applicationLog = new ApplicationLog()
                    {
                        Name = exception.Message,
                        Description = exception.InnerException == null ? String.Empty : exception.InnerException.Message,
                        StackTrace = exception.StackTrace,
                        DateCreated = exceptionOccured,
                        DateModified = exceptionOccured
                    };

                    if (appLogRepository.InsertOrUpdate(applicationLog))
                        appLogRepository.Save();

                    if (applicationLog.ID != default(int))
                        exceptionId = applicationLog.ID;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                exceptionOccured = DateTime.Now;
            }
            finally
            {
                WindowHelper.OpenDialog<ExceptionWindow>(exceptionId, ExceptionHelper.GetShortMessage(exception), exceptionOccured);
            }
        }

        /// <summary>
        /// Handles the Exit event of the windows application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.ExitEventArgs"/> instance containing the event data.</param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AppSettings.Default.Save();
        }
    }
}
