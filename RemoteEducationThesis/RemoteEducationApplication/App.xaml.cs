using Education.Application.Helpers;
using Education.Application.Managers;
using Education.Application.Views.ExceptionViewer;
using Education.DAL;
using Education.DAL.Providers;
using Education.DAL.Repositories;
using WPFFramework.Controls.Helpers;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using WPFFramework.App;
using AppResources = Education.Application.Properties.Resources;
using AppSettings = Education.Application.Properties.Settings;
using Education.Model.Entities;

namespace Education.Application
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : WpfApplication
	{
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


		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public App()
			: base()
		{
			ShutdownMode = ShutdownMode.OnExplicitShutdown;
		}

		#endregion

		#region EventHandling

		/// <summary>
		/// Handles Application Startup event of the windows application.
		/// Overrides current culture settings.
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			StyleManager.ChangeTheme(CurrentThemeName);

			CurrentUICulture = new CultureInfo("hr-HR");
			ApplicationManager.InitializeCommandMappings();
			//FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
			//	new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
		}

		/// <summary>
		/// Handles the DispatcherUnhandledException event of the windows application.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = true;
			int? exceptionId = null;
			Exception exception = e.Exception;
			DateTime exceptionOccured = DateTime.Now;

			try
			{
				string message = exception.Message;
				string description = exception.InnerException == null ? String.Empty : exception.InnerException.Message;
				string stackTrace = exception.StackTrace;

				Log log = LogManager.CreateLog(EnumCollection.LogType.Error, message, description, stackTrace);
				LogManager.Log(log);

				exceptionId = log.ID;
				//else
				//	throw new Exception(AppResources.ExceptionDatabaseServer);
			}
			catch (Exception ex)
			{
				exception = ex;
				exceptionOccured = DateTime.Now;
			}
			finally
			{
				HandleWindowsOnCriticalException();
				WindowHelper.OpenMainWindowDialog<ExceptionWindow>(exceptionId, exception, exceptionOccured);
			}
		}

		/// <summary>
		/// Handles the Exit event of the windows application.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.ExitEventArgs"/> instance containing the event data.</param>
		private void Application_Exit(object sender, ExitEventArgs e)
		{
			AuthenticationManager.Logout();
			AppSettings.Default.Save();
		}

		#endregion

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		private void HandleWindowsOnCriticalException()
		{
			WpfMainWindow = null;
			base.CloseAllWindows();
		}

		#endregion
	}
}
