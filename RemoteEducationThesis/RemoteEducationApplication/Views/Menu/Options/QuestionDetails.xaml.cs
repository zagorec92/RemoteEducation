using Education.Application.Helpers;
using Education.Application.Managers;
using Education.Application.Shared;
using Education.Model.Entities;
using System.Windows;

namespace Education.Application.Views.Menu.Options
{
	/// <summary>
	/// Interaction logic for QuestionDetails.xaml
	/// </summary>
	public partial class QuestionDetails : WindowBase
	{
		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public Question Question { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public QuestionDetails()
		{
			InitializeComponent();
			Loaded += QuestionDetails_Loaded;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="question"></param>
		public QuestionDetails(Question question)
			: this()
		{
			Question = question;
		}

		#endregion

		#region EventHandling

		#region Window

		/// <summary>
		/// Handles the Loaded event of the QuestionDetails control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void QuestionDetails_Loaded(object sender, RoutedEventArgs e)
		{
			DataContext = Question;
		}

		#endregion

		#region WindowBar

		/// <summary>
		/// Handles the AppBarClick event of the ApplicationBar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="Education.Application.Shared.ApplicationBarEventArgs"/> instance containing the event data.</param>
		private void ApplicationBar_AppBarClick(object sender, ApplicationBarEventArgs e)
		{
			ApplicationManager.ExecuteBasicCommand(e.CommandName, this);
		}

		#endregion

		#endregion
	}
}
