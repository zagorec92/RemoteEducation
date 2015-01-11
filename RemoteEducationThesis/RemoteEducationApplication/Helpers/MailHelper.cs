using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RemoteEducationApplication.Helpers
{
    public static class MailHelper
    {
        #region Const

        private const string MailSubject = "EEducation password reset";

        #endregion

        #region Struct

        private struct MailParameters
        {
            public static string Username = "#Username";
            public static string Password = "#Password";
            public static string RequestDateTime = "#RequestDateTime";
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userName"></param>
        /// <param name="fromAddres"></param>
        /// <param name="dateRequested"></param>
        /// <returns></returns>
        private static MailMessage GetPasswordResetMail(string password, string userName, string fromAddres, DateTime dateRequested)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromAddres);
            mailMessage.To.Add(new MailAddress(userName));
            mailMessage.Subject = MailSubject;
            mailMessage.Body = File.ReadAllText(@"..\..\Resources\Mail\RecoveryMail.html");

            mailMessage.Body = mailMessage.Body.Replace(MailParameters.Username, userName);
            mailMessage.Body = mailMessage.Body.Replace(MailParameters.Password, password);
            mailMessage.Body = mailMessage.Body.Replace(MailParameters.RequestDateTime, dateRequested.ToString());

            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="mailTo"></param>
        /// <returns></returns>
        public static async Task SendMail(string password, string mailTo)
        {
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential networkCredential =
                smtpClient.Credentials.GetCredential(smtpClient.Host, smtpClient.Port, null);

            MailMessage mailMessage = 
                GetPasswordResetMail(password, mailTo, networkCredential.UserName, DateTime.Now);

            await smtpClient.SendMailAsync(mailMessage);
        }

        #endregion
    }
}
