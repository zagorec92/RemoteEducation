using System;
using WPFFramework.Attributes;

namespace Education.DAL
{
	public static class EnumCollection
	{
		#region Enum

		#region SortDirection

		/// <summary>
		/// Sort directions
		/// </summary>
		public enum SortDirection
		{
			/// <summary>
			/// Ascending direction - from first to last
			/// </summary>
			Ascending,

			/// <summary>
			/// Descending direction - from last to first
			/// </summary>
			Descending
		}

		#endregion

		#region Role

		/// <summary>
		/// Role types
		/// </summary>
		public enum RoleType
		{
			/// <summary>
			/// No role
			/// </summary>
			None,

			/// <summary>
			/// Admin role
			/// </summary>
			Admin,

			/// <summary>
			/// Professor role
			/// </summary>
			Professor,

			/// <summary>
			/// Student role
			/// </summary>
			Student
		}

		#endregion

		#region QuestionType

		public enum QuestionType
		{
			/// <summary>
			/// No type
			/// </summary>
			None,

			/// <summary>
			/// Yes or no
			/// </summary>
			YesNo,

			/// <summary>
			/// Multiple choice
			/// </summary>
			MultipleChoice,

			/// <summary>
			/// Numeric value
			/// </summary>
			NumericValue,

			/// <summary>
			/// Math expression
			/// </summary>
			MathExpression,

			/// <summary>
			/// Essay
			/// </summary>
			Essay,

			/// <summary>
			/// Programming/scripting language code
			/// </summary>
			Code
		}

		#endregion

		#region LogType

		public enum LogType
		{
			/// <summary>
			/// None
			/// </summary>
			None,

			/// <summary>
			/// Info
			/// </summary>
			Error,

			/// <summary>
			/// Error
			/// </summary>
			Info
		}

		#endregion

		#region AttachmentType

		public enum AttachmentType
		{
			/// <summary>
			/// None
			/// </summary>
			None,

			/// <summary>
			/// Text file
			/// </summary>
			[KeyMap(".txt")]
			TXT,

			/// <summary>
			/// JPEG image
			/// </summary>
			[KeyMap(".jpeg")]
			JPEG,

			/// <summary>
			/// PNG image
			/// </summary>
			[KeyMap(".png")]
			PNG,

			/// <summary>
			/// Portable document format
			/// </summary>
			[KeyMap(".pdf")]
			PDF,

			/// <summary>
			/// Microsoft Office Open XML Format Document
			/// </summary>
			[KeyMap(".docx")]
			DOCX,

			/// <summary>
			/// Microsoft Office Open XML Format Spreadsheet
			/// </summary>
			[KeyMap(".xlsx")]
			XLSX,

			/// <summary>
			/// Microsoft PowerPoint Presentation
			/// </summary>
			[KeyMap(".pptx")]
			PPTX
		}

		#endregion

		#region SleepTime

		public enum SleepTime
		{
			/// <summary>
			/// 1000 ms
			/// </summary>
			MS1000 = 1000,

			/// <summary>
			/// 2500 ms
			/// </summary>
			MS2500 = 2500,

			/// <summary>
			/// 5000 ms
			/// </summary>
			MS5000 = 5000,

			/// <summary>
			/// 10000 ms
			/// </summary>
			MS10000 = 10000,

			/// <summary>
			/// 20000 ms
			/// </summary>
			MS20000 = 20000,

			/// <summary>
			/// 40000 ms
			/// </summary>
			MS40000 = 40000
		}

		#endregion

		#region ResourceDictionaryIndex

		/// <summary>
		/// Resource dictionary indexes from App.xaml.
		/// </summary>
		public enum ResourceDictionaryIndex
		{
			/// <summary>
			/// FontSize resource dictionary.
			/// </summary>
			FontSize,

			/// <summary>
			/// Brushes resource dictionary.
			/// </summary>
			Brushes,

			/// <summary>
			/// DefaultTheme resource dictionary.
			/// </summary>
			Theme,

			/// <summary>
			/// GlobalStyle resource dictionary.
			/// </summary>
			GlobalStyle,

			/// <summary>
			/// LoginStyle resource dictionary.
			/// </summary>
			LoginStyle,

			/// <summary>
			/// MainWindowStyle resource dictionary.
			/// </summary>
			MainWindowStyle,

			/// <summary>
			/// ClientWindowStyle resource dictionary.
			/// </summary>
			ClientWindowStyle,

			/// <summary>
			/// UserControlStyle resource dictionary.
			/// </summary>
			UserControlStyle,

			/// <summary>
			/// ClockControlStyle resource dictionary
			/// </summary>
			ClockControlStyle,

			/// <summary>
			/// LoginStoryboards resource dictionary.
			/// </summary>
			LoginStoryboards
		}

		#endregion

		#endregion
	}
}
