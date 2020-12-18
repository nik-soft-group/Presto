using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NiksoftCore.DataAccess;

namespace NiksoftCore.SystemBase.Service
{
    public class SystemBaseDbContext : NikDbContext, ISystemUnitOfWork
    {

        public SystemBaseDbContext(string connectionString) : base(connectionString)
        {
        }

        DbSet<SystemSetting> SystemSettings { get; set; }
        DbSet<PortalLanguage> PortalLanguages { get; set; }
        DbSet<PanelMenu> PanelMenus { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SystemSettingMap());
            builder.ApplyConfiguration(new PortalLanguageMap());
            builder.ApplyConfiguration(new PanelMenuMap());
        }
    }
}
