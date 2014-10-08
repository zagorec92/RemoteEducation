using Education.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Education.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EEducationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(EEducationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Roles.AddOrUpdate(r => r.Name,
                new Role { Name = "Admin", Description = "Admin permissions", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                new Role { Name = "Student", Description = "Student role", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                new Role { Name = "Professor", Description = "Professor role", DateCreated = DateTime.Now, DateModified = DateTime.Now });

            context.SaveChanges();

            Dictionary<string, string> testPasswords = new Dictionary<string, string>();

            #region 1

            UserDetails userDetails = new UserDetails();
            userDetails.Email = "ipericki1@tvz.hr";
            userDetails.PasswordSalt = GenerateSalt();

            string password = GetRandomPassword(8);
            userDetails.Password = CreateSaltedPasswordHash(password, userDetails.PasswordSalt);
            userDetails.DateCreated = userDetails.DateModified = DateTime.Now;

            context.UserDetails.AddOrUpdate(x => x.Email, userDetails);
            context.SaveChanges();

            User user = new User();
            user.FirstName = "Ivan";
            user.LastName = "Peri�ki";
            user.Roles = new List<Role>();
            user.Roles.Add(context.Roles.FirstOrDefault(x => x.Name == "Student"));
            user.UserDetail = userDetails;
            user.DateCreated = user.DateModified = DateTime.Now;

            testPasswords.Add(user.UserDetail.Email, password);

            context.Users.AddOrUpdate(u => u.LastName,
                user);

            #endregion

            #region 2

            userDetails = new UserDetails();
            userDetails.Email = "professor1@tvz.hr";
            userDetails.PasswordSalt = GenerateSalt();

            password = GetRandomPassword(8);
            userDetails.Password = CreateSaltedPasswordHash(password, userDetails.PasswordSalt);
            userDetails.DateCreated = userDetails.DateModified = DateTime.Now;

            context.UserDetails.AddOrUpdate(x => x.Email, userDetails);
            context.SaveChanges();

            user = new User();
            user.FirstName = "Profesor";
            user.LastName = "1";
            user.Roles = new List<Role>();
            user.Roles.Add(context.Roles.FirstOrDefault(x => x.Name == "Professor"));
            user.UserDetail = userDetails;
            user.DateCreated = user.DateModified = DateTime.Now;

            testPasswords.Add(user.UserDetail.Email, password);

            context.Users.AddOrUpdate(u => u.LastName,
                user);

            #endregion

            #region 3

            userDetails = new UserDetails();
            userDetails.Email = "student1@tvz.hr";
            userDetails.PasswordSalt = GenerateSalt();

            password = GetRandomPassword(8);
            userDetails.Password = CreateSaltedPasswordHash(password, userDetails.PasswordSalt);
            userDetails.DateCreated = userDetails.DateModified = DateTime.Now;

            context.UserDetails.AddOrUpdate(x => x.Email, userDetails);
            context.SaveChanges();

            user = new User();
            user.FirstName = "Luka";
            user.LastName = "Pancirov";
            user.Roles = new List<Role>();
            user.Roles.Add(context.Roles.FirstOrDefault(x => x.Name == "Student"));
            user.UserDetail = userDetails;
            user.DateCreated = user.DateModified = DateTime.Now;

            testPasswords.Add(user.UserDetail.Email, password);

            context.Users.AddOrUpdate(u => u.LastName,
                user);

            #endregion

            #region 4

            userDetails = new UserDetails();
            userDetails.Email = "student2@tvz.hr";
            userDetails.PasswordSalt = GenerateSalt();

            password = GetRandomPassword(8);
            userDetails.Password = CreateSaltedPasswordHash(password, userDetails.PasswordSalt);
            userDetails.DateCreated = userDetails.DateModified = DateTime.Now;

            context.UserDetails.AddOrUpdate(x => x.Email, userDetails);
            context.SaveChanges();

            user = new User();
            user.FirstName = "Dominik";
            user.LastName = "Hodja";
            user.Roles = new List<Role>();
            user.Roles.Add(context.Roles.FirstOrDefault(x => x.Name == "Student"));
            user.UserDetail = userDetails;
            user.DateCreated = user.DateModified = DateTime.Now;

            testPasswords.Add(user.UserDetail.Email, password);

            context.Users.AddOrUpdate(u => u.LastName,
                user);

            #endregion

            #region 5

            userDetails = new UserDetails();
            userDetails.Email = "student3@tvz.hr";
            userDetails.PasswordSalt = GenerateSalt();

            password = GetRandomPassword(8);
            userDetails.Password = CreateSaltedPasswordHash(password, userDetails.PasswordSalt);
            userDetails.DateCreated = userDetails.DateModified = DateTime.Now;

            context.UserDetails.AddOrUpdate(x => x.Email, userDetails);
            context.SaveChanges();

            user = new User();
            user.FirstName = "Hrvoje";
            user.LastName = "Brlek";
            user.Roles = new List<Role>();
            user.Roles.Add(context.Roles.FirstOrDefault(x => x.Name == "Student"));
            user.UserDetail = userDetails;
            user.DateCreated = user.DateModified = DateTime.Now;

            testPasswords.Add(user.UserDetail.Email, password);

            context.Users.AddOrUpdate(u => u.LastName,
                user);

            #endregion

            #region 6

            userDetails = new UserDetails();
            userDetails.Email = "student4@tvz.hr";
            userDetails.PasswordSalt = GenerateSalt();

            password = GetRandomPassword(8);
            userDetails.Password = CreateSaltedPasswordHash(password, userDetails.PasswordSalt);
            userDetails.DateCreated = userDetails.DateModified = DateTime.Now;

            context.UserDetails.AddOrUpdate(x => x.Email, userDetails);
            context.SaveChanges();

            user = new User();
            user.FirstName = "Ivan";
            user.LastName = "Horvat";
            user.Roles = new List<Role>();
            user.Roles.Add(context.Roles.FirstOrDefault(x => x.Name == "Student"));
            user.UserDetail = userDetails;
            user.DateCreated = user.DateModified = DateTime.Now;

            testPasswords.Add(user.UserDetail.Email, password);

            context.Users.AddOrUpdate(u => u.LastName,
                user);

            #endregion

            context.SaveChanges();

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = Path.Combine(path, "test_passwords.txt");

            foreach (var item in testPasswords)
                File.AppendAllLines(path, new string[] { item.Key, item.Value });
        }

        private static string GenerateSalt()
        {
            byte[] bytes = new byte[16];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        private static string GetRandomPassword(int size)
        {
            string generatedPassword = String.Empty;
            char ch;
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(random.Next(97, 122)));
                generatedPassword += ch.ToString();
            }

            return generatedPassword;
        }

        private static string CreateSaltedPasswordHash(string password, string salt)
        {
            byte[] passwordAndSaltBytes = UTF8Encoding.Default.GetBytes(password + salt);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] passwordWithSaltHashed = md5.ComputeHash(passwordAndSaltBytes);

            return Convert.ToBase64String(passwordWithSaltHashed);
        }
    }
}
