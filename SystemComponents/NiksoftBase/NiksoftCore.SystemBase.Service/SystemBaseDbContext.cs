﻿using Microsoft.EntityFrameworkCore;
using NiksoftCore.DataAccess;

namespace NiksoftCore.SystemBase.Service
{
    public class SystemBaseDbContext : NikDbContext, ISystemUnitOfWork
    {

        public SystemBaseDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<PortalLanguage> PortalLanguages { get; set; }
        public DbSet<PanelMenu> PanelMenus { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SystemSettingMap());
            builder.ApplyConfiguration(new PortalLanguageMap());
            builder.ApplyConfiguration(new PanelMenuMap());
            builder.ApplyConfiguration(new UserProfileMap());
        }
    }
}
