using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NiksoftCore.DataAccess;

namespace NiksoftCore.SystemBase.Service
{
    public class SystemBaseDbContext : NikDbContext, ISystemUnitOfWork
    {
        private string conString;

        public SystemBaseDbContext(string connectionString) : base(connectionString)
        {
            conString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString);
        }

        DbSet<SystemSetting> SystemSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SystemSettingMap());
        }
    }
}
