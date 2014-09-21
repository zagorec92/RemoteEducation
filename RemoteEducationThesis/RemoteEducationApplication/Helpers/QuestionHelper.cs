using Education.Model;
using Microsoft.Win32;
using RemoteEducation.DAL;
using RemoteEducation.DAL.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using RemoteEducationApplication.Extensions;

namespace RemoteEducationApplication.Helpers
{
    public static class QuestionHelper
    {
        private const string Filter = "HTML Files |*.html";
        private const string AnswerDelimiter = "@--";

        #region Methods

        /// <summary>
        /// 
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

                SaveQuestion(question);
                SaveAnswers(question.Answers);

                return question.ID;
            }

            return default(int);
        }

        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
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

        #endregion
    }
}
