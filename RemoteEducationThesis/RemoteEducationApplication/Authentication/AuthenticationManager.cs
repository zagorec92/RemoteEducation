﻿using RemoteEducation.DAL;
using RemoteEducation.DAL.Repositories;
using RemoteEducation.Model;
using RemoteEducationApplication.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace RemoteEducationApplication.Authentication
{
    public class AuthenticationManager
    {
        #region Const

        private const int BYTE_SIZE_SALT = 16;

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
        public static void AuthenticateUser(string username, string password)
        {
            if(username == String.Empty && password == String.Empty)
                throw new ArgumentException(ErrorMessages.InvalidParameters, AuthenticateExParameters.IsParameters);

            using(RemoteEducationDbContext context = new RemoteEducationDbContext())
            {
                UserRepository userRepository = new UserRepository(context);
                User user = userRepository.Get(username);

                if (user == null)
                    throw new ArgumentException(ErrorMessages.InvalidUsername, AuthenticateExParameters.IsUsername);

                if (!CheckPassword(password, user.UserDetail.PasswordSalt, user.UserDetail.Password))
                    throw new ArgumentException(ErrorMessages.InvalidPassword, AuthenticateExParameters.IsPassword);
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
            using (RemoteEducationDbContext context = new RemoteEducationDbContext())
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

        #endregion
    }
}