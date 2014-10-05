using Education.Model;
using Microsoft.Win32;
using Education.DAL;
using Education.DAL.Repositories;
using RemoteEducationApplication.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RemoteEducationApplication.Helpers
{
    public static class QuestionHelper
    {
        #region Const

        private const string Filter = "HTML Files |*.html";
        private const string AnswerDelimiter = "@--";

        #endregion

        #region Struct

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

        public static Question CurrentQuestion { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Shows OpenFileDialog, reads question and answers from a given file and saves them into database.
        /// </summary>
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
                question.Content = CleanHtmlContent(htmlContent);
                question.Content = WrapInFormTags(question.Content);

                SaveQuestion(question);
                SaveAnswers(question.Answers);

                return question.ID;
            }

            return default(int);
        }

        /// <summary>
        /// Saves question in database.
        /// </summary>
        /// <param name="question"></param>
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
                //throw exception
            }
        }

        /// <summary>
        /// Saves answers in database.
        /// </summary>
        /// <param name="answers"></param>
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
        /// <param name="questionId"></param>
        /// <returns></returns>
        public static Question GetQuestion (int questionId)
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
        /// Saves the content to a file on disk and returns a path to the file.
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        public static Uri GetQuestionContentUri(string htmlContent)
        {
            string filePath = @"C:\Users\" + Environment.UserName + @"\AppData\LocalLow\Temp\question.htm";
            File.WriteAllText(filePath, htmlContent);
            return new Uri(filePath);
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

        #endregion
    }
}
