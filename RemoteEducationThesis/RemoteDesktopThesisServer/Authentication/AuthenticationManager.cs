using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDesktopThesisServer.Authentication
{
    public class AuthenticationManager
    {
        /// <summary>
        /// Authentication exception parameters.
        /// </summary>
        public struct AuthenticateExParameters
        {
            public static string IsUsername = "Username";
            public static string IsPassword = "Password";
            public static string IsParametersEmpty = "Empty";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>Bool indicating if authentication is successful.</returns>
        public static bool AuthenticateUser(string username, string password)
        {
            if(username == String.Empty && password == String.Empty)
                throw new ArgumentException("Error message.", AuthenticateExParameters.IsParametersEmpty);

            bool isAuthenticated = false;

            //check validity of given parameters
            //if given parameters are not valid
            //  throw ArgumentException with message and param name (use AuthenticateExParameters struct)

            return isAuthenticated;
        }
    }
}
