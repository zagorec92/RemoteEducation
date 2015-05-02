using Education.DAL;
using Education.DAL.Repositories;
using Education.Model;
using Microsoft.Win32;
using RemoteEducationApplication.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using ExtensionLibrary.DataTypes.Converters.Extensions;

namespace RemoteEducationApplication.Helpers
{
    public static class QuestionHelper
    {
        #region Const

        /// <summary>
        /// Represents a constant value for file extensions.
        /// </summary>
        private const string Filter = "HTML Files |*.html";

        /// <summary>
        /// Represents a constant value for answer delimiter string.
        /// </summary>
        private const string AnswerDelimiter = "@--";

        #endregion

        #region Struct

        /// <summary>
        /// Represents a holder for helper html tags in form of <see cref="System.String"/> values.
        /// </summary>
        private struct HtmlTags
        {
            public const string BodyOpen = "<body";
            public const string BodyClose = "</body>";
            public const string FormOpen = "<form id=\"questionForm\" method=\"get\">";
            public const string FormClose = "<input type='submit' value='Submit answers' "+ 
                "style='float:right;margin-right: 20px;'/></form>";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current question.
        /// </summary>
        public static Question CurrentQuestion { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Shows OpenFileDialog, reads question and answers from a given file and saves them into database.
        /// </summary>
        /// <returns>Question identification number if succeded, default <see cref="System.Int32"/> value otherwise.</returns>
        public static int CreateQuestionWithAnswers()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = Filter.Substring(Filter.IndexOf('.'));
            ofd.Filter = Filter;

            Question question = new Question();

            if (ofd.ShowDialog().GetValueOrDefault())
            {
                string htmlContent = File.ReadAllText(ofd.FileName);
                
                question.Answers = ParseAnswersFromContent(htmlContent);
                question.Content = WrapInFormTags(CleanHtmlContent(htmlContent));

                SaveQuestion(question);
                SaveAnswers(question.Answers);

                return question.ID;
            }

            return default(int);
        }

        /// <summary>
        /// Saves question in database.
        /// </summary>
        /// <param name="question">The <see cref="Education.Model.Question"/> instance.</param>
        private static void SaveQuestion(Question question)
        {
            if (!question.Content.Equals(String.Empty))
            {
                using (EEducationDbContext context = new EEducationDbContext())
                {
                    QuestionRepository questionRepository = new QuestionRepository(context);

                    if (questionRepository.InsertOrUpdate(question))
                        questionRepository.Save();
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// Saves answers in database.
        /// </summary>
        /// <typeparam name="T">Type derived from <see cref="Education.Model.Answer"/> class.</typeparam>
        /// <param name="answers">The <see cref="System.Collections.Generic.ICollection{T}"/> collection containing
        /// instances of type T.</param>
        private static void SaveAnswers(ICollection<Answer> answers)
        {
            using (EEducationDbContext context = new EEducationDbContext())
            {
                AnswerRepository answerRepository = new AnswerRepository(context);

                bool isValid = false;

                foreach (Answer answer in answers)
                {
                    isValid = answerRepository.InsertOrUpdate(answer);

                    if (!isValid)
                        break;
                }

                if (isValid)
                    answerRepository.Save();
            }
        }

        /// <summary>
        /// Gets the question by ID.
        /// </summary>
        /// <param name="questionId">The <see cref="System.Int32"/> value.</param>
        /// <returns>The <see cref="Education.Model.Question"/> instance if succeded, null otherwise.</returns>
        public static Question GetQuestion(int questionId)
        {
            using(EEducationDbContext context = new EEducationDbContext())
            {
                QuestionRepository questionRepository = new QuestionRepository(context);
                Question question = questionRepository.Get(questionId);

                if (question != null)
                    return question;
                else
                    return null;
            }
        }

        /// <summary>
        /// Removes extra characters from html file.
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private static string CleanHtmlContent(string htmlContent)
        {
            char[] charactersToRemove = new char[] { '\n', '\r' };
            string clearedLineBreaks = String.Concat(htmlContent.Where(x => !charactersToRemove.Contains(x)));

            return clearedLineBreaks.Substring(0, clearedLineBreaks.IndexOf(AnswerDelimiter));
        }

        /// <summary>
        /// Wraps body content in form tags.
        /// </summary>
        /// <param name="htmlContent"></param>
        private static string WrapInFormTags(string htmlContent)
        {
            htmlContent = htmlContent.Insert(
                htmlContent.IndexOf(">", htmlContent.IndexOf(HtmlTags.BodyOpen)) + 1, HtmlTags.FormOpen);
            htmlContent = htmlContent.Insert(
                htmlContent.IndexOf(HtmlTags.BodyClose), HtmlTags.FormClose);

            return htmlContent;
        }

        /// <summary>
        /// Gets the answers from html content.
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private static ICollection<Answer> ParseAnswersFromContent(string htmlContent)
        {
            List<Answer> answers = new List<Answer>();

            string answersToParse = htmlContent.Remove(0, htmlContent.IndexOf(AnswerDelimiter) + AnswerDelimiter.Length);
            string[] answerValues = answersToParse.Split(';');

            foreach (string value in answerValues)
            {
                int scoreIndex = value.IndexOf(AnswerDelimiter[0]);

                answers.Add(new Answer()
                {
                    Content = value.Substring(0, scoreIndex++),
                    Score = value.Substring(scoreIndex).To<Int32>()
                });
                
            }

            return answers;
        }

        /// <summary>
        /// Checks if given answers are correct. Includes scores.
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        public static int CheckAnswers(Dictionary<int, String> answers)
        {
            List<Answer> correctAnswers = CurrentQuestion.Answers.ToList();
            int score = default(int);

            for (int i = 0; i < answers.Count; i++)
            {
                var correctAnswer = correctAnswers[i].Content;
                var givenAnswer = answers[i];

                if (correctAnswer.Equals(givenAnswer))
                    score += correctAnswers[i].Score;
            }

            return score;
        }

        /// <summary>
        /// Sends question identification number to every client.
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="clients"></param>
        public static void SendQuestionIDToClient(int questionId, ObservableCollection<ClientHandler> clients)
        {
            if (questionId != default(int))
            {
                foreach (var client in clients)
                {
                    NetworkStream stream = client.GetDataExchangeStream();
                    stream.Flush();
                    stream.WriteByte((byte)questionId);
                    stream.Flush();
                }
            }
        }

        #endregion
    }
}
