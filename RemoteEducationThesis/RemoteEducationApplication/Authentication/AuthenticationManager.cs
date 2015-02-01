using Education.DAL;
using Education.DAL.Repositories;
using Education.Model;
using RemoteEducationApplication.Helpers;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WpfDesktopFramework.Collections.Extensions;
using WpfDesktopFramework.Security;
using WpfDesktopFramework.DataTypes.Converters.Extensions;
using AppResources = RemoteEducationApplication.Properties.Resources;

namespace RemoteEducationApplication.Authentication
{
    public class AuthenticationManager
    {
        #region Const

        private const int BYTE_SIZE_SALT = 16;
        private const int GEN_PASS_SIZE = 10;

        #endregion

        #region Struct

        /// <summary>
        /// Authentication exception parameters.
        /// </summary>
        public struct AuthenticateExParameters
        {
            public static string IsUsername = "Username";
            public static string IsPassword = "Password";
            public static string IsParameters = "Empty";
            public static string IsDatabase = "Database";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        public static User LoggedInUser { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username">The <see cref="System.String"/> value representing the username.</param>
        /// <param name="password">The <see cref="System.String"/> value representing the password.</param>
        public static int AuthenticateUser(string email, string password)
        {
            if(email == String.Empty || password == String.Empty)
                throw new ArgumentException(AppResources.ValidationMessageEmptyData, AuthenticateExParameters.IsParameters);

            using(EEducationDbContext context = new EEducationDbContext())
            {
                UserRepository userRepository = new UserRepository(context);
                User user = userRepository.GetByEmail(email);

                if (user == null)
                    throw new ArgumentException(AppResources.ValidationMessageUsername, AuthenticateExParameters.IsUsername);

                if (!CheckPassword(password, user.UserDetail.PasswordSalt, user.UserDetail.Password))
                    throw new ArgumentException(AppResources.ValidationMessagePassword, AuthenticateExParameters.IsPassword);

                LoggedInUser = user;

                return user.Roles.FirstOrDefault().ID;
            }
        }

        /// <summary>
        /// Creates authentication data for the <see cref="Education.Model.User"/> instance.
        /// </summary>
        /// <param name="user">The <see cref="Education.Model.User"/> instance.</param>
        public static void CreateUserAuthentication(User user)
        {
            user.UserDetail.PasswordSalt = SecurityManager.GenerateSalt(BYTE_SIZE_SALT);
            user.UserDetail.Password = SecurityManager.CreateSaltedPasswordHash(user.UserDetail.Password,
                user.UserDetail.PasswordSalt);

            using (EEducationDbContext context = new EEducationDbContext())
            {
                UserRepository userRepository = new UserRepository(context);

                userRepository.InsertOrUpdate(user);
                userRepository.Save();
            }
        }  

        /// <summary>
        /// Checks if the given password is equal to the current user password.
        /// </summary>
        /// <param name="enteredPassword">The <see cref="System.String"/> value representing entered password.</param>
        /// <param name="salt">The <see cref="System.String"/> value representing password salt.</param>
        /// <param name="userPassword">The <see cref="System.String"/> value representing user password.</param>
        /// <returns>True if passwords match, false otherwise.</returns>
        private static bool CheckPassword(string enteredPassword, string salt, string userPassword)
        {
            byte[] enteredPasswordWithSaltBytes = UTF8Encoding.Default.GetBytes(enteredPassword + salt);
            byte[] userPasswordBytes = UTF8Encoding.Default.GetBytes(userPassword);
            
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] enteredPasswordWithSaltHashed = md5.ComputeHash(enteredPasswordWithSaltBytes);

            enteredPasswordWithSaltHashed = UTF8Encoding.Default.GetBytes(
                Convert.ToBase64String(enteredPasswordWithSaltHashed));

            return userPasswordBytes.EqualsByByte(enteredPasswordWithSaltHashed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="securityNum"></param>
        public static void RecoverPassword(string email)
        {
            using (EEducationDbContext context = new EEducationDbContext())
            {
                UserRepository userRepository = new UserRepository(context);
                User user = userRepository.GetByEmail(email);

                if (user != null)
                {
                    string password = SecurityManager.GetRandomPassword(GEN_PASS_SIZE);
                    user.UserDetail.PasswordSalt = SecurityManager.GenerateSalt(BYTE_SIZE_SALT);
                    user.UserDetail.Password = SecurityManager.CreateSaltedPasswordHash(password, user.UserDetail.PasswordSalt);

                    Task.Run(async () => await MailHelper.SendPasswordResetMail(password, user.UserDetail.Email, user.FirstName));

                    if (userRepository.InsertOrUpdate(user))
                        userRepository.Save();
                }
                else
                {
                    throw new ArgumentException(Properties.Resources.ValidationMessagePasswordReset);
                }
            }
        }

        #endregion
    }
}
