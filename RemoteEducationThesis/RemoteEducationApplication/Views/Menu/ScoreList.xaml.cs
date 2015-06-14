using Education.Application.Managers;
using Education.Model;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System.Collections.ObjectModel;
using System.Windows;
using WPFFramework.Attributes;

namespace RemoteEducationApplication.Views.Menu
{
    /// <summary>
    /// Interaction logic for ScoreList.xaml
    /// </summary>
    [MenuWindow(UseDefaultConstructor = true)]
    public partial class ScoreList : WindowBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ScoreLog observable collection.
        /// </summary>
        public ObservableCollection<ScoreLog> Scores { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public ScoreList()
        {
            InitializeComponent();
            Loaded += ScoreList_Loaded;
        }

        #endregion

        #region EventHandling

        #region Window

        /// <summary>
        /// Handles the Loaded event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance
        /// containing the event data.</param>
        private void ScoreList_Loaded(object sender, RoutedEventArgs e)
        {
            Scores = new ObservableCollection<ScoreLog>(ScoreManager.GetScoreLogs());
            DataContext = this;
        }

        #endregion

        #region WindowBar

        /// <summary>
        /// Handles the AppBarClick event of the ApplicationBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RemoteEducationApplication.Shared.ApplicationEventArgs"/> instance
        /// containing the event data.</param>
        private void ApplicationBar_AppBarClick(object sender, ApplicationEventArgs e)
        {
            ApplicationHelper.ExecuteBasicCommand(e.CommandName, this);
        }

        #endregion

        #endregion
    }
}
