using Education.DAL;
using Education.DAL.Providers;
using ExtensionLibrary.Enums.Extensions;
using System;
using System.Diagnostics;
using LogEntity = Education.Model.Entities.Log;

namespace Education.Application.Managers
{
	public static class LogManager
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="log"></param>
		[Conditional("LOG")]
		[Conditional("RELEASE")]
		public static void Log(LogEntity log)
		{
			LogProvider.Save(log);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logType"></param>
		/// <param name="message"></param>
		[Conditional("LOG")]
		[Conditional("RELEASE")]
		public static void Log(EnumCollection.LogType logType, string message)
		{
			LogEntity log = CreateLog(logType, message, null, null);
			Log(log);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logType"></param>
		/// <param name="message"></param>
		/// <param name="description"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public static LogEntity CreateLog(EnumCollection.LogType logType, string message, string description, string stackTrace)
		{
			return new LogEntity()
			{
				UserIdentifier = UserProvider.LoggedInUser.Identifier,
				LogTypeID = logType.GetValue(),
				Message = message,
				Description = description,
				StackTrace = stackTrace,
				DateModified = DateTime.Now
			};
		}

	}
}
