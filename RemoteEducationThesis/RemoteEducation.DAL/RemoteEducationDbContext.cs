using RemoteEducation.Model;
using System;
using System.Data.Entity;

namespace RemoteEducation.DAL
{
    public class RemoteEducationDbContext : DbContext, IDisposable
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }
    }
}
