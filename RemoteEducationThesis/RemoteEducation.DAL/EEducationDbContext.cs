using Education.Model;
using System.Data.Entity;

namespace Education.DAL
{
    public class EEducationDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the Users DbSet.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the UserDetails DbSet.
        /// </summary>
        public DbSet<UserDetails> UserDetails { get; set; }

        /// <summary>
        /// Gets or sets the Roles DbSet.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the ApplicationLogs DbSet.
        /// </summary>
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }

        /// <summary>
        /// Gets or sets the ScoreLogs DbSet.
        /// </summary>
        public DbSet<ScoreLog> ScoreLogs { get; set; }

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
        public DbSet<ServerInfo> ServerInfos { get; set; }
    }
}
