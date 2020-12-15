using Microsoft.EntityFrameworkCore;
using NiksoftCore.DataAccess;

namespace NiksoftCore.SystemBase.Service
{
    public class SystemBaseDbContext : NikDbContext, ISystemUnitOfWork
    {
        public SystemBaseDbContext() : base("name=SystemBase")
        {
        }

        DbSet<SystemSetting> SystemSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SystemSettingMap());
        }
    }
}
