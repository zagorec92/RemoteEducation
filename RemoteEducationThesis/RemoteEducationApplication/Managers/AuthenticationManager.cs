using Education.Application.Helpers;
using Education.DAL;
using Education.DAL.Providers;
using Education.Model.Entities;
using ExtensionLibrary.Collections.Extensions;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using ExtensionLibrary.Enums.Extensions;
using ExtensionLibrary.Security;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppResources = Education.Application.Properties.Resources;

namespace Education.Application.Managers
{
	public static class AuthenticationManager
	{
		#region Const

		public const int BYTE_SIZE_SALT = 32;
		public const int GEN_PASS_SIZE = 10;

		#endregion

		#region Struct

		/// <summary>
		/// Authentication exception parameters.
		/// </summary>
		public struct AuthenticationFailedParameters
		{
			public static string Username = "Username";
			public static string Password = "Password";
			public static string Parameters = "Empty";
			public static string Database = "Database";
		}

		#endregion

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		public static void Logout()
		{
			try
			{
				if (UserProvider.LoggedInUser != null)
				{
					string message = String.Format("User {0} has logged out.", UserProvider.LoggedInUser.Identifier);
					LogManager.Log(EnumCollection.LogType.Info, message);
				}
			}
			finally
			{
				UserProvider.LoggedInUser = null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public static int AuthenticateUser(string email, string password)
		{
			if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
				throw new ArgumentException(AppResources.ValidationMessageEmptyData, AuthenticationFailedParameters.Parameters);

			User user = UserProvider.Get(email);

			if (user == null)
				throw new ArgumentException(AppResources.ValidationMessageUsername, AuthenticationFailedParameters.Username);

			if (!CheckPassword(password, user.PasswordSalt, user.Password))
				throw new ArgumentException(AppResources.ValidationMessagePassword, AuthenticationFailedParameters.Password);

			UserProvider.LoggedInUser = user;
			LogManager.Log(EnumCollection.LogType.Info, String.Format("User {0} has logged in.", user.Identifier));

			return UserProvider.LoggedInUser.Roles.OrderByDescending(x => x.AuthorizationLevel).First().ID;

		}

		/// <summary>
		/// Creates authentication data for the <see cref="Education.Model.User"/> instance.
		/// </summary>
		/// <param name="user">The <see cref="Education.Model.Entities.User"/> instance.</param>
		private static string CreateUserAuthentication(User user)
		{
			string password = SecurityManager.GetRandomPassword(GEN_PASS_SIZE);

			user.PasswordSalt = SecurityManager.GenerateSalt(BYTE_SIZE_SALT);
			user.Password = SecurityManager.CreateSaltedPasswordHash(password, user.PasswordSalt);

			return password;
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
			byte[] enteredPasswordWithSaltHashed = SecurityManager.ComputeHash(enteredPasswordWithSaltBytes);

			enteredPasswordWithSaltHashed = UTF8Encoding.Default.GetBytes(Convert.ToBase64String(enteredPasswordWithSaltHashed));

			return userPasswordBytes.EqualsByByte(enteredPasswordWithSaltHashed);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="securityNum"></param>
		public static void RecoverPassword(string username, string securityNum)
		{
			User user = UserProvider.Get(username);
			int securityCode = securityNum.ToSafe<int>();

			if (user != null && user.SecurityCode == securityCode)
			{
				Task.Run(async () => await MailHelper.SendPasswordResetMail(CreateUserAuthentication(user), user.UserDetail.Email, user.UserDetail.FirstName));

				UserProvider.Save(user);
			}
			else
			{
				throw new ArgumentException(AppResources.ValidationMessagePasswordReset);
			}
		}

		#region Extensions

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static bool IsAdmin(this User user)
		{
			return user.Roles.Any(x => x.ID == EnumCollection.RoleType.Admin.GetValue());
		}

		#endregion

		#endregion
	}
}
