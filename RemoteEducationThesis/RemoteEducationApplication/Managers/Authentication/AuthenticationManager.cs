using Education.Application.Helpers;
using Education.DAL;
using Education.DAL.Repositories;
using Education.Model.Entities;
using ExtensionLibrary.Collections.Extensions;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using ExtensionLibrary.Enums.Extensions;
using ExtensionLibrary.Security;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppResources = Education.Application.Properties.Resources;

namespace Education.Application.Managers.Authentication
{
	public static class AuthenticationManager
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

		#if !DEBUG

		/// <summary>
		/// 
		/// </summary>
		public static void Logout()
		{
			try
			{
				string message = String.Format("User {0} has logged out.", AuthenticationManager.LoggedInUser.Identifier);
				LogManager.Log(LogRepository.LogType.Info, message, null, null);
			}
			finally
			{
				LoggedInUser = null;
			}
		}

		#endif
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="username">The <see cref="System.String"/> value representing the username.</param>
		/// <param name="password">The <see cref="System.String"/> value representing the password.</param>
		public static int AuthenticateUser(string email, string password)
		{
			if(String.IsNullOrEmpty(email)|| String.IsNullOrEmpty(password))
				throw new ArgumentException(AppResources.ValidationMessageEmptyData, AuthenticateExParameters.IsParameters);
	
			using(EEducationDbContext context = new EEducationDbContext())
			{
				UserRepository userRepository = new UserRepository(context);
				User user = userRepository.GetByUsername(email);

				if (user == null)
					throw new ArgumentException(AppResources.ValidationMessageUsername, AuthenticateExParameters.IsUsername);

				if (!CheckPassword(password, user.PasswordSalt, user.Password))
					throw new ArgumentException(AppResources.ValidationMessagePassword, AuthenticateExParameters.IsPassword);

				LoggedInUser = user;

				return LoggedInUser.Roles.FirstOrDefault().ID;
			}
		}
		
		/// <summary>
		/// Creates authentication data for the <see cref="Education.Model.User"/> instance.
		/// </summary>
		/// <param name="user">The <see cref="Education.Model.User"/> instance.</param>
		public static void CreateUserAuthentication(User user)
		{
			user.PasswordSalt = SecurityManager.GenerateSalt(BYTE_SIZE_SALT);
			user.Password = SecurityManager.CreateSaltedPasswordHash(user.Password, user.PasswordSalt);
	
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
			byte[] enteredPasswordWithSaltHashed = SecurityManager.GetMD5Hash(enteredPasswordWithSaltBytes);
	
			enteredPasswordWithSaltHashed = UTF8Encoding.Default.GetBytes(
				Convert.ToBase64String(enteredPasswordWithSaltHashed));
	
			return userPasswordBytes.EqualsByByte(enteredPasswordWithSaltHashed);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="securityNum"></param>
		public static void RecoverPassword(string username, string securityNum)
		{
		    using (EEducationDbContext context = new EEducationDbContext())
		    {
		        UserRepository userRepository = new UserRepository(context);
		        User user = userRepository.GetByUsername(username);
		        int securityCode = securityNum.ToSafe<int>();
		
		        if (user != null && user.SecurityCode == securityCode)
		        {
		            string password = SecurityManager.GetRandomPassword(GEN_PASS_SIZE);
		            user.PasswordSalt = SecurityManager.GenerateSalt(BYTE_SIZE_SALT);
		            user.Password = SecurityManager.CreateSaltedPasswordHash(password, user.PasswordSalt);
		
		            Task.Run(async () => await MailHelper.SendPasswordResetMail(password, user.UserDetail.Email, user.UserDetail.FirstName));
		
		            if (userRepository.InsertOrUpdate(user))
		                userRepository.Save();
		        }
		        else
		        {
		            throw new ArgumentException(AppResources.ValidationMessagePasswordReset);
		        }
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
