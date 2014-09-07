using RemoteEducation.DAL;
using RemoteEducation.DAL.Repositories;
using Education.Model;
using RemoteEducationApplication.Extensions;
using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace RemoteEducationApplication.Authentication
{
    public class AuthenticationManager
    {
        #region Const

        private const int BYTE_SIZE_SALT = 16;
        private const int GEN_PASS_SIZE = 8;

        #endregion

        #region Struct

        /// <summary>
        /// Exception error messages.
        /// </summary>
        private struct ErrorMessages
        {
            public const string InvalidParameters = "Parameter is empty.";
            public const string InvalidUsername = "Incorrect username.";
            public const string InvalidPassword = "Incorrect password.";
        }

        /// <summary>
        /// Authentication exception parameters.
        /// </summary>
        public struct AuthenticateExParameters
        {
            public static string IsUsername = "Username";
            public static string IsPassword = "Password";
            public static string IsParameters = "Empty";
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public static int AuthenticateUser(string username, string password)
        {
            if(username == String.Empty && password == String.Empty)
                throw new ArgumentException(ErrorMessages.InvalidParameters, AuthenticateExParameters.IsParameters);

            using(EEducationDbContext context = new EEducationDbContext())
            {
                UserRepository userRepository = new UserRepository(context);
                User user = userRepository.GetAll()
                    .Include(x => x.Roles)
                    .Where(x => x.UserDetail.Username == username)
                    .Single();

                if (user == null)
                    throw new ArgumentException(ErrorMessages.InvalidUsername, AuthenticateExParameters.IsUsername);

                if (!CheckPassword(password, user.UserDetail.PasswordSalt, user.UserDetail.Password))
                    throw new ArgumentException(ErrorMessages.InvalidPassword, AuthenticateExParameters.IsPassword);

                return user.Roles.First().ID;
            }

        }

        /// <summary>
        /// Creates authentication data for the <see cref="RemoteEducation.Model.User"/> instance.
        /// </summary>
        /// <param name="user"></param>
        public static void CreateUserAuthentication(User user)
        {
            user.UserDetail.PasswordSalt = GenerateSalt();
            user.UserDetail.Password = CreateSaltedPasswordHash(user.UserDetail.Password,
                user.UserDetail.PasswordSalt);

            //save
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
        /// <param name="enteredPassword">Entered password.</param>
        /// <param name="salt">Password salt.</param>
        /// <param name="userPassword">User password.</param>
        /// <returns></returns>
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
        /// Generates pseudo-random salt.
        /// </summary>
        /// <returns></returns>
        private static string GenerateSalt()
        {
            byte[] bytes = new byte[BYTE_SIZE_SALT];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Creates hashed password with salt.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string CreateSaltedPasswordHash(string password, string salt)
        {
            byte[] passwordAndSaltBytes = UTF8Encoding.Default.GetBytes(password + salt);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] passwordWithSaltHashed = md5.ComputeHash(passwordAndSaltBytes);

            return Convert.ToBase64String(passwordWithSaltHashed);
        }

        public static void RecoverPassword(string username, string email)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string GetRandomPassword(int size)
        {
            string generatedPassword = String.Empty;
            char ch;
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));                 
                generatedPassword += ch.ToString();
            }

            return generatedPassword;
        }

        #endregion
    }
}
