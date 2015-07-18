using Education.Application.Managers.Authentication;
using Education.DAL;
using Education.DAL.Repositories;
using Education.Model.Entities;
using ExtensionLibrary.Enums.Extensions;
using System;

namespace Education.Application.Managers
{
	public static class LogManager
    {
        public static int Log(LogRepository.LogType logType, string message, string description, string stackTrace)
        {
            int logId = 0;

            using (EEducationDbContext context = new EEducationDbContext())
            {
                LogRepository applicationLogRepository = new LogRepository(context);

                Log log = new Log();
                log.UserIdentifier = AuthenticationManager.LoggedInUser.Identifier;
                log.LogTypeID = logType.GetValue();
                log.Message = message;
                log.Description = description;
                log.StackTrace = stackTrace;
                log.DateModified = DateTime.Now;

                if (applicationLogRepository.InsertOrUpdate(log))
                    applicationLogRepository.Save();

                if (log.ID != default(int))
                    logId = log.ID;
            }

            return logId;
        }
    }
}
