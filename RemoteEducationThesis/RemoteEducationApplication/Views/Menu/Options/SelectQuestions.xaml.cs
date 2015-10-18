using Education.Application.Helpers;
using Education.Application.Managers;
using Education.Application.Shared;
using Education.Model.Entities;
using ExtensionLibrary.Collections.Extensions;
using ExtensionLibrary.Controls.Extensions;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using ExtensionLibrary.Enums.Helpers;
using Education.Application.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using AppResources = Education.Application.Properties.Resources;

namespace Education.Application.Views.Menu.Options
{
	/// <summary>
	/// Interaction logic for SelectQuestions.xaml
	/// </summary>
	public partial class SelectQuestions : WindowBase
    {
        #region Fields

        private Visibility _detailsVisibility;

        #endregion

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
        public Visibility DetailsVisibility
        {
            get { return _detailsVisibility; }
            set
            {
                _detailsVisibility = value;
                OnPropertyChanged("DetailsVisibility");
            }
        }

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
        }

        #endregion

        #region EventHandling

        #region Window

        /// <summary>
        /// Handles the Loaded event of the SelectQuestions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private async void SelectQuestions_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Subjects = SubjectHelper.GetSubjects();
                Difficulties = new ObservableCollection<string>();
                SubjectQuestions = new ObservableCollection<Question>();
            });

            UploadedByName = String.Empty;
            DetailsVisibility = Visibility.Collapsed;

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
                if (x.GetTag() == AppResources.QuestionSelectDifficulty)
                    Difficulty = x.SelectedItem.ToSafe<double>();
                else
                    PopulateSubjectAndDifficulty(((Subject)x.SelectedItem).ID);
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
            QuestionManager.SendQuestionIDToClient(QuestionId, ClientManager.Clients);
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonDetails_Click(object sender, RoutedEventArgs e)
        {
            Question question = SubjectQuestions.First(x => x.ID == QuestionId);
            this.NavigateToChild(new QuestionDetails(question), WindowStartupLocation.CenterOwner);
        }

        #region RadioButton

        /// <summary>
        /// Handles the Checked event of the RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Window.RoutedEventArgs"/> instance containing the event data.</param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            sender.ExecuteIfNotNull<RadioButton>(x =>
            {
                QuestionId = x.GetTag<int>();
                DetailsVisibility = Visibility.Visible;
            });
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
            DetailsVisibility = Visibility.Hidden;
            ICollectionView view = CollectionViewSource.GetDefaultView(SubjectQuestions);

            if (view != null && (UploadedByName != null || Difficulty != default(double)))
            {
                view.Filter += (obj) =>
                {
                    Question question = (Question)obj;

                    return 
                        UploadedByName != String.Empty
                            ? (Difficulty != default(double))
                                ? question.UploadedByUser.UserDetail.FullName.Contains(UploadedByName)
                                && question.Difficulty == Difficulty
                                : question.UploadedByUser.UserDetail.FullName.Contains(UploadedByName)
                            : (Difficulty != default(double)) 
                                ? question.Difficulty == Difficulty 
                                : question != null;
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
                SortDescription sortDescription = view.SortDescriptions.FirstOrDefault(x => x.PropertyName == propertyName);

                if (sortDescription.PropertyName != null)
                    view.SortDescriptions.Remove(sortDescription);
                
                sortDescription = new SortDescription(propertyName, listSortDirection);

                //view.SortDescriptions.Clear();
                view.SortDescriptions.Insert(0, sortDescription);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        private void PopulateSubjectAndDifficulty(int subjectId)
        {
            Reset();
            Difficulties.Add(AppResources.QuestionSelectDifficultyDefault);

            List<Question> questions = QuestionManager.GetQuestionsBySubject(subjectId);
            List<double> difficulties = new List<double>();

            foreach(Question question in questions)
            {
                SubjectQuestions.Add(question);
                difficulties.AddDistinct(question.Difficulty);
            }

            string[] orderedDifficulties = difficulties
                .OrderByDescending(x => x)
                .Select(x => x.ToString())
                .ToArray();

            Difficulties.AddRangeDistinct(orderedDifficulties);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Reset()
        {
            DetailsVisibility = Visibility.Hidden;
            SubjectQuestions.Clear();
            Difficulties.Clear();
        }

        #endregion
    }
}
