using Education.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Education.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EEducationDbContext>
    {
        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Education.DAL.Migrations.Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
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
        }

        #region Temp methods

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

        #endregion
    }
}
