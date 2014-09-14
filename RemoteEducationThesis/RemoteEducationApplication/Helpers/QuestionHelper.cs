using Education.Model;
using Microsoft.Win32;
using RemoteEducation.DAL;
using RemoteEducation.DAL.Repositories;
using System;
using System.IO;
using System.Linq;

namespace RemoteEducationApplication.Helpers
{
    public static class QuestionHelper
    {
        private const string Filter = "HTML Files |*.html";

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public static void OpenQuestion()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = Filter.Substring(Filter.IndexOf('.'));
            ofd.Filter = Filter;

            Question question = new Question();

            if(ofd.ShowDialog().GetValueOrDefault())
                question.Content = CleanHtmlContent(File.ReadAllText(ofd.FileName));

            if(!question.Content.Equals(String.Empty))
            {
                using(EEducationDbContext context = new EEducationDbContext())
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
        /// Removes extra characters from html file.
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private static string CleanHtmlContent(string htmlContent)
        {
            char[] charactersToRemove = new char[] { '\n', '\r' };

            return String.Concat(htmlContent.Where(x => !charactersToRemove.Contains(x)));
        }

        #endregion
    }
}
