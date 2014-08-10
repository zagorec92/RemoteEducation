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

namespace RemoteEducationApplication.Authentication
{
    public class AuthenticationManager
    {
        public enum ErrorCodes
        {
            UsernameError = 1,
            PasswordError = 2
        }

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

                //test
                if (!user.UserDetail.Password.Equals(password))
                    throw new ArgumentException(ErrorMessages.InvalidPassword, AuthenticateExParameters.IsPassword);
            }
        }
    }
}
