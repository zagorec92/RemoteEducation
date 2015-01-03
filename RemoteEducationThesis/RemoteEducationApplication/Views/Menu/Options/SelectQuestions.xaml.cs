﻿using Education.DAL;
using Education.DAL.Repositories;
using Education.Model;
using RemoteEducationApplication.Client;
using RemoteEducationApplication.Helpers;
using RemoteEducationApplication.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfDesktopFramework.Controls.Extensions;
using WpfDesktopFramework.DataTypes.Converters.Extensions;
using WpfDesktopFramework.Enums.Helpers;
using AppResources = RemoteEducationApplication.Properties.Resources;

namespace RemoteEducationApplication.Views.Menu.Options
{
    /// <summary>
    /// Interaction logic for SelectQuestions.xaml
    /// </summary>
    public partial class SelectQuestions : WindowBase
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public List<Subject> Subjects { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Question> SubjectQuestions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> Difficulties { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UploadedByName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private double Difficulty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private int QuestionId { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instace of the SelectQuestions class.
        /// </summary>
        public SelectQuestions()
        {
            InitializeComponent();
            Loaded += SelectQuestions_Loaded;
            SubjectQuestions = new ObservableCollection<Question>();
        }

        #endregion

        #region EventHandling

        #region Window

        /// <summary>
        /// Handles the Loaded event of the SelectQuestions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SelectQuestions_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() => 
            {
                Subjects = SubjectHelper.GetSubjects();
                Difficulties = new ObservableCollection<string>();
                SubjectQuestions = new ObservableCollection<Question>();
            }));

            UploadedByName = String.Empty;
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

        #region ComboBox

        /// <summary>
        /// Handles the SelectionChanged event of the ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Window.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ComboBox_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sender.ExecuteIfNotNull<ComboBox>(x => 
            {
                if(x.GetTag() == AppResources.QuestionSelectDifficulty.Remove(AppResources.QuestionSelectDifficulty.Length - 1))
                {
                    Difficulty = x.SelectedItem.ToNullable<double>();
                }
                else
                {
                    PopulateDifficulty(x);
                }
            });

            Filter();
        }

        /// <summary>
        /// Handles the SelectionChanged of the ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ComboBox_Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sender.ExecuteIfNotNull<ComboBox>(x =>
            {
                string tag = ((ComboBoxItem)x.SelectedItem).GetTag();
                ListSortDirection listSortDirection = EnumHelper.GetValueByName<ListSortDirection>(tag);

                Sort(x.GetTag(), listSortDirection);
            });
        }

        #endregion

        #region TextBox

        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sender.ExecuteIfNotNull<TextBox>(x => UploadedByName = x.Text);
            Filter();
        }

        #endregion

        #region Button

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            QuestionHelper.SendQuestionIDToClient(QuestionId, ClientManager.Clients);
        }

        #region RadioButton

        /// <summary>
        /// Handles the Checked event of the RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Window.RoutedEventArgs"/> instance containing the event data.</param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            sender.ExecuteIfNotNull<RadioButton>(x => QuestionId = x.GetTag<int>());
        }

        #endregion

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        private void Filter()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(SubjectQuestions);

            if (view != null && (UploadedByName != null || Difficulty != default(double)))
            {
                view.Filter += (obj) =>
                {
                    Question question = (Question)obj;

                    return UploadedByName != String.Empty ?
                        (Difficulty != default(double)) ? question.UploadedByUser.FullName.Contains(UploadedByName)
                        && question.Difficulty == Difficulty : question.UploadedByUser.FullName.Contains(UploadedByName)
                        : (Difficulty != default(double)) ? question.Difficulty == Difficulty : question != null;
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="listSortDirection"></param>
        private void Sort(string propertyName, ListSortDirection listSortDirection)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(SubjectQuestions);

            if(view != null)
            {
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(propertyName, listSortDirection));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        private void PopulateDifficulty(ComboBox comboBox)
        {
            int subjectId = ((Subject)comboBox.SelectedItem).ID;

            using (EEducationDbContext context = new EEducationDbContext())
            {
                QuestionRepository questionRepository = new QuestionRepository(context);
                Difficulties.Add(AppResources.QuestionSelectDifficultyDefault);

                foreach (Question question in questionRepository.GetBySubject(subjectId))
                {
                    SubjectQuestions.Add(question);

                    if (!Difficulties.Contains(question.Difficulty.ToString()))
                        Difficulties.Add(question.Difficulty.ToString());
                }
            }
        }

        #endregion
    }
}