using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteEducation.DAL;
using RemoteEducation.Model;
using RemoteEducation.DAL.Repositories;
using System.Security.Cryptography;
using RemoteEducationApplication.Extensions;

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
        /// 
        /// </summary>
        /// <param name="user"></param>
        public static void CreateUser(User user)
        {
            user.UserDetail.PasswordSalt = GenerateSalt();
            user.UserDetail.Password = CreateSaltedPasswordHash(user.UserDetail.Password,
                user.UserDetail.PasswordSalt);

            //save
            using (RemoteEducationDbContext context = new RemoteEducationDbContext())
            {
                UserRepository userRepository = new UserRepository(context);
                userRepository.InsertOrUpdate(user);

                //userRepository.Save();
            }
        }  

        /// <summary>
        /// Checks if the given password is equal to the current user password.
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="salt"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        private static bool CheckPassword(string enteredPassword, string salt, string userPassword)
        {
            byte[] enteredPasswordBytes = System.Text.UTF8Encoding.Default.GetBytes(enteredPassword.ToCharArray());
            byte[] saltBytes = System.Text.UTF8Encoding.Default.GetBytes(salt.ToCharArray());
            byte[] enteredPasswordBytesWithSalt = new byte[enteredPasswordBytes.Length + saltBytes.Length];
            byte[] userPasswordBytes = System.Text.UTF8Encoding.Default.GetBytes(userPassword.ToCharArray());

            for (int i = 0; i < enteredPasswordBytes.Length; i++)
                enteredPasswordBytesWithSalt[i] = enteredPasswordBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                enteredPasswordBytesWithSalt[enteredPasswordBytes.Length + i] = saltBytes[i];

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] enteredPasswordWithSaltHashed = md5.ComputeHash(enteredPasswordBytesWithSalt);

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
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string CreateSaltedPasswordHash(string password, string salt)
        {
            string passwordHash = String.Empty;

            //TODO

            return passwordHash;
        }

        #endregion
    }
}
