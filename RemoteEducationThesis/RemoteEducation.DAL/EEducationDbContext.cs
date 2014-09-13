using Education.Model;
using System;
using System.Data.Entity;

namespace RemoteEducation.DAL
{
    public class EEducationDbContext : DbContext, IDisposable
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }
        public DbSet<ScoreLog> ScoreLogs { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
