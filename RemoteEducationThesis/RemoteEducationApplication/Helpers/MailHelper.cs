using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AppResources = RemoteEducationApplication.Properties.Resources;
using Outlook = Microsoft.Office.Interop.Outlook;

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
            public static string PasswordResetStyle = "#Style";
            public static string PasswordResetName = "#Name";
            public static string PasswordResetUsername = "#Username";
            public static string PasswordResetPassword = "#Password";
            public static string PasswordResetRequestDateTime = "#RequestDateTime";
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
        private static MailMessage GetPasswordResetMail(string password, string userName, string name, string fromAddres, DateTime dateRequested)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromAddres);
            mailMessage.To.Add(new MailAddress(userName));
            mailMessage.Subject = MailSubject;

            string localizedCulture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            mailMessage.Body = File.ReadAllText(String.Format(@"..\..\Resources\Mail\RecoveryMail.{0}.html", localizedCulture));
            mailMessage.Body = mailMessage.Body.Replace(MailParameters.PasswordResetStyle, 
                String.Concat("<style type='text/css'>", File.ReadAllText(@"..\..\Resources\Mail\MailStyle.css"), "</style>"));
            mailMessage.Body = mailMessage.Body.Replace(MailParameters.PasswordResetName, name);
            mailMessage.Body = mailMessage.Body.Replace(MailParameters.PasswordResetRequestDateTime, dateRequested.ToString());
            mailMessage.Body = mailMessage.Body.Replace(MailParameters.PasswordResetUsername, userName);
            mailMessage.Body = mailMessage.Body.Replace(MailParameters.PasswordResetPassword, password);

            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }

        /// <summary>
        /// Gets the outlook mail item.
        /// </summary>
        /// <param name="exceptionId">The <see cref="System.Int32"/> exception ID.</param>
        /// <param name="dateTimeOfException">The <see cref="System.DateTime"/> date when exception occured.</param>
        /// <returns>The <see cref="Microsoft.Office.Interop.Outlook.MailItem"/> instance.</returns>
        public static Outlook.MailItem GetExceptionMail(int? exceptionId, DateTime dateTimeOfException)
        {
            Outlook.Application outlookApp = new Outlook.Application();
            Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
            mailItem.To = App.Settings.ReportMailAdress;

            string mailSubject = String.Empty;

            if (exceptionId.HasValue)
                mailSubject = String.Format(AppResources.ExceptionMailSubject, exceptionId);
            else
                mailSubject = String.Format(AppResources.ExceptionMailSubject, dateTimeOfException);

            mailItem.Subject = mailSubject;
            mailItem.FlagIcon = Outlook.OlFlagIcon.olRedFlagIcon;
            mailItem.BodyFormat = Outlook.OlBodyFormat.olFormatRichText;

            return mailItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="mailTo"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task SendPasswordResetMail(string password, string mailTo, string name)
        {
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential networkCredential =
                smtpClient.Credentials.GetCredential(smtpClient.Host, smtpClient.Port, null);

            MailMessage mailMessage = 
                GetPasswordResetMail(password, mailTo, name, networkCredential.UserName, DateTime.Now);

            await smtpClient.SendMailAsync(mailMessage);
        }

        #endregion
    }
}
