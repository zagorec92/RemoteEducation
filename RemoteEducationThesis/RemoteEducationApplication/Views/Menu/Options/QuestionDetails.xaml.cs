﻿using Education.Model;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System.Windows;

namespace RemoteEducationApplication.Views.Menu.Options
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
