using Education.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Education.DAL.Repositories;
using System.Xml;
using System.Reflection;
using ExtensionLibrary.DataTypes.Converters.Extensions;
using ExtensionLibrary.Enums.Extensions;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Migrations.Model;

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
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
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
    }

    internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        #region Table

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            SetCreatedDate(createTableOperation.Columns);
            base.Generate(createTableOperation);
        }

        protected override void Generate(AlterTableOperation alterTableOperation)
        {
            SetCreatedDate(alterTableOperation.Columns);
            base.Generate(alterTableOperation);
        }

        #endregion

        #region Column

        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            SetCreatedDate(addColumnOperation.Column);
            base.Generate(addColumnOperation);
        }

        protected override void Generate(AlterColumnOperation alterColumnOperation)
        {
            SetCreatedDate(alterColumnOperation.Column);
            base.Generate(alterColumnOperation);
        }

        #endregion

        private static void SetCreatedDate(IEnumerable<ColumnModel> columnModels)
        {
            foreach (ColumnModel columnModel in columnModels)
                SetCreatedDate(columnModel);
        }

        private static void SetCreatedDate(PropertyModel model)
        {
            if (model.Name == "DateCreated")
                model.DefaultValueSql = "GETDATE()";
        }
    }
}
