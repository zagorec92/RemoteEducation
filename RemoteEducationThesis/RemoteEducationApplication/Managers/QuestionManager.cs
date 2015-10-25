using Education.Application.Client;
using Education.DAL.Providers;
using Education.Model.Entities;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace Education.Application.Managers
{
	public class QuestionManager
	{
		#region Const

		/// <summary>
		/// Represents a constant value for file extensions.
		/// </summary>
		internal const string Filter = "HTML Files |*.html";

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
			public const string FormClose = "<input type='submit' value='Submit answers' " +
				"style='float:right;margin-right: 20px;'/></form>";
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the current question.
		/// </summary>
		public static Question SelectedQuestion { get; set; }

		#endregion

		/// <summary>
		/// Shows OpenFileDialog, reads question and answers from a given file and saves them into database.
		/// </summary>
		/// <returns>Question identification number if succeded, default <see cref="System.Int32"/> value otherwise.</returns>
		public static int CreateQuestionWithAnswers(string fileName)
		{
			string htmlContent = File.ReadAllText(fileName);
			int retVal = default(int);

			if (!String.IsNullOrEmpty(htmlContent))
			{
				Question question = new Question();

				question.Answers = ParseAnswersFromContent(htmlContent);
				question.Content = WrapInFormTags(CleanHtmlContent(htmlContent));
				question.AutomaticEvaluation = true;

				QuestionProvider.Save(question);
				AnswerProvider.Save(question.Answers);

				retVal = question.ID;
			}

			return retVal;
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
					Points = value.Substring(scoreIndex).To<int>()
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
			List<Answer> correctAnswers = SelectedQuestion.Answers.ToList();
			int score = default(int);

			for (int i = 0; i < answers.Count; i++)
			{
				var correctAnswer = correctAnswers[i].Content;
				var givenAnswer = answers[i];

				if (correctAnswer.Equals(givenAnswer))
					score += correctAnswers[i].Points;
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
	}
}
