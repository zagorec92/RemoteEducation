﻿using Education.DAL;
using Education.DAL.Repositories;
using Education.Model;
using RemoteEducationApplication.Extensions;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WpfDesktopFramework.Extensions;

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
            public const string NoConnection = "There is no connection to the database.";
        }

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
            if(email == String.Empty && password == String.Empty)
                throw new ArgumentException(ErrorMessages.InvalidParameters, AuthenticateExParameters.IsParameters);

            using(EEducationDbContext context = new EEducationDbContext())
            {
                UserRepository userRepository = new UserRepository(context);
                User user = userRepository.GetByEmail(email);

                if (user == null)
                    throw new ArgumentException(ErrorMessages.InvalidUsername, AuthenticateExParameters.IsUsername);

                if (!CheckPassword(password, user.UserDetail.PasswordSalt, user.UserDetail.Password))
                    throw new ArgumentException(ErrorMessages.InvalidPassword, AuthenticateExParameters.IsPassword);

                LoggedInUser = user;

                return user.Roles.First().ID;
            }
        }

        /// <summary>
        /// Creates authentication data for the <see cref="Education.Model.User"/> instance.
        /// </summary>
        /// <param name="user">The <see cref="Education.Model.User"/> instance.</param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
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
