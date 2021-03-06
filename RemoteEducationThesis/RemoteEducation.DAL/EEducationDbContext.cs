﻿using Education.Model.Entities;
using Education.Model.ETypeEntities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Education.DAL
{
	public class EEducationDbContext : DbContext
	{
		#region Properties

		/// <summary>
		/// Gets or sets the Users DbSet.
		/// </summary>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Gets or sets the UserDetails DbSet.
		/// </summary>
		public DbSet<UserDetails> UserDetails { get; set; }

		/// <summary>
		/// Gets or sets the Log DbSet.
		/// </summary>
		public DbSet<Log> Logs { get; set; }

		/// <summary>
		/// Gets or sets the Scores DbSet.
		/// </summary>
		public DbSet<Score> Scores { get; set; }

		/// <summary>
		/// Gets or sets the Questions DbSet.
		/// </summary>
		public DbSet<Question> Questions { get; set; }

		/// <summary>
		/// Gets or sets the Subjects DbSet.
		/// </summary>
		public DbSet<Subject> Subjects { get; set; }

		/// <summary>
		/// Gets or sets the Answers DbSet.
		/// </summary>
		public DbSet<Answer> Answers { get; set; }

		/// <summary>
		/// Gets or sets the ServerInfos DbSet.
		/// </summary>
		public DbSet<ServerInfo> ServerInfoes { get; set; }

		/// <summary>
		/// Gets or sets the SUbjectAttachments DbSet.
		/// </summary>
		public DbSet<SubjectAttachment> SubjectAttachments { get; set; }

		#region ETables

		/// <summary>
		/// Gets or sets the Roles DbSet.
		/// </summary>
		public DbSet<Role> Roles { get; set; }

		/// <summary>
		/// Gets or sets the LogTypes DbSet.
		/// </summary>
		public DbSet<LogType> LogTypes { get; set; }

		/// <summary>
		/// Gets or sets the AttachmnetTypes DbSet.
		/// </summary>
		public DbSet<AttachmentType> AttachmentTypes { get; set; }

		/// <summary>
		/// Gets or sets the QuestionTypes DbSet.
		/// </summary>
		public DbSet<QuestionType> QuestionTypes { get; set; }

		/// <summary>
		/// Gets or sets the Countries DbSet.
		/// </summary>
		public DbSet<Country> Countries { get; set; }

		#endregion

		#endregion

		/// <summary>
		/// Overrides the OnModelCreating method.
		/// </summary>
		/// <param name="modelBuilder">The <see cref="System.Data.Entity.DbModelBuilder"/> instance.</param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("EEducation");
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			base.OnModelCreating(modelBuilder);
		}
	}
}
