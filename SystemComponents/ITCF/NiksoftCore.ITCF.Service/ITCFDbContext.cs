using Microsoft.EntityFrameworkCore;
using NiksoftCore.DataAccess;

namespace NiksoftCore.ITCF.Service
{
    public class ITCFDbContext : NikDbContext, IITCFUnitOfWork
    {

        public ITCFDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<UserLegalForm> UserLegalForms { get; set; }
        public DbSet<BusinessCategory> BusinessCategories { get; set; }
        public DbSet<IndustrialPark> IndustrialParks { get; set; }
        public DbSet<Business> Businesses { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserLegalFormMap());
            builder.ApplyConfiguration(new BusinessCategoryMap());
            builder.ApplyConfiguration(new IndustrialParkMap());
            builder.ApplyConfiguration(new BusinessMap());
            
        }
    }
}
